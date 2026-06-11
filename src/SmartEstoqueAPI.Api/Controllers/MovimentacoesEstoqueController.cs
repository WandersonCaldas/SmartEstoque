using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartEstoqueAPI.Api.Requests;
using SmartEstoqueAPI.Api.Responses;
using SmartEstoqueAPI.Domain.Entities;
using SmartEstoqueAPI.Domain.Enums;
using SmartEstoqueAPI.Infrastructure.Data;

namespace SmartEstoqueAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovimentacoesEstoqueController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public MovimentacoesEstoqueController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovimentacaoEstoqueResponse>>> Get(DateTime? dataInicial, DateTime? dataFinal)
    {
        var query = _context.MovimentacoesEstoque
        .Include(x => x.Itens)
            .ThenInclude(x => x.Produto)
        .AsNoTracking()
        .AsQueryable();

        if (dataInicial.HasValue)
            query = query.Where(x => x.DataMovimentacao >= dataInicial.Value);

        if (dataFinal.HasValue)
            query = query.Where(x => x.DataMovimentacao <= dataFinal.Value);

        var movimentacoes = await query
        .OrderByDescending(x => x.DataMovimentacao)
        .ToListAsync();

        return Ok(_mapper.Map<IEnumerable<MovimentacaoEstoqueResponse>>(movimentacoes));
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<MovimentacaoEstoqueResponse>> GetById(long id)
    {
        var movimentacao = await _context.MovimentacoesEstoque
            .Include(x => x.Itens)
                .ThenInclude(x => x.Produto)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (movimentacao == null)
            return NotFound();

        return Ok(_mapper.Map<MovimentacaoEstoqueResponse>(movimentacao));
    }

    [HttpPost]
    public async Task<ActionResult<MovimentacaoEstoqueResponse>> Post(MovimentacaoEstoqueRequest request)
    {
        if (request.Itens == null || !request.Itens.Any())
            return BadRequest("Informe pelo menos um item.");

        if (!Enum.IsDefined(typeof(TipoMovimentacao), request.TipoMovimentacao))
            return BadRequest("Tipo de movimentação inválido.");

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var movimentacao = new MovimentacaoEstoque
            {
                TipoMovimentacao = request.TipoMovimentacao,
                Observacao = request.Observacao,
                DataMovimentacao = DateTime.Now
            };

            foreach (var itemRequest in request.Itens)
            {
                if (itemRequest.Quantidade <= 0)
                    return BadRequest("A quantidade deve ser maior que zero.");

                var produto = await _context.Produtos
                    .FirstOrDefaultAsync(x => x.Id == itemRequest.ProdutoId);

                if (produto == null)
                    return BadRequest($"Produto {itemRequest.ProdutoId} não encontrado.");

                if (request.TipoMovimentacao == TipoMovimentacao.Entrada)
                {
                    produto.QuantidadeAtual += itemRequest.Quantidade;
                }
                else if (request.TipoMovimentacao == TipoMovimentacao.Saida)
                {
                    if (produto.QuantidadeAtual < itemRequest.Quantidade)
                        return BadRequest($"Estoque insuficiente para o produto {produto.Nome}.");

                    produto.QuantidadeAtual -= itemRequest.Quantidade;
                }
                else if (request.TipoMovimentacao == TipoMovimentacao.Ajuste)
                {
                    produto.QuantidadeAtual = itemRequest.Quantidade;
                }

                movimentacao.Itens.Add(new ItemMovimentacaoEstoque
                {
                    ProdutoId = produto.Id,
                    Quantidade = itemRequest.Quantidade,
                    ValorUnitario = itemRequest.ValorUnitario
                });
            }

            _context.MovimentacoesEstoque.Add(movimentacao);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            var movimentacaoCriada = await _context.MovimentacoesEstoque
                .Include(x => x.Itens)
                    .ThenInclude(x => x.Produto)
                .AsNoTracking()
                .FirstAsync(x => x.Id == movimentacao.Id);

            return CreatedAtAction(
                nameof(GetById),
                new { id = movimentacaoCriada.Id },
                _mapper.Map<MovimentacaoEstoqueResponse>(movimentacaoCriada));
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}