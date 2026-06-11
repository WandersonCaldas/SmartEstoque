using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartEstoqueAPI.Api.Requests;
using SmartEstoqueAPI.Api.Responses;
using SmartEstoqueAPI.Domain.Entities;
using SmartEstoqueAPI.Infrastructure.Data;

namespace SmartEstoqueAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ProdutosController(
        AppDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoResponse>>> Get()
    {
        var produtos = await _context.Produtos
            .Include(x => x.Categoria)
            .Include(x => x.Fornecedor)
            .AsNoTracking()
            .ToListAsync();

        return Ok(_mapper.Map<IEnumerable<ProdutoResponse>>(produtos));
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<ProdutoResponse>> GetById(long id)
    {
        var produto = await _context.Produtos
            .Include(x => x.Categoria)
            .Include(x => x.Fornecedor)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (produto == null)
            return NotFound();

        return Ok(_mapper.Map<ProdutoResponse>(produto));
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoResponse>> Post(ProdutoRequest request)
    {
        var categoriaExiste = await _context.Categorias
            .AnyAsync(x => x.Id == request.CategoriaId);

        if (!categoriaExiste)
            return BadRequest("Categoria não encontrada.");

        var fornecedorExiste = await _context.Fornecedores
            .AnyAsync(x => x.Id == request.FornecedorId);

        if (!fornecedorExiste)
            return BadRequest("Fornecedor não encontrado.");

        var produto = _mapper.Map<Produto>(request);

        _context.Produtos.Add(produto);

        await _context.SaveChangesAsync();

        produto = await _context.Produtos
            .Include(x => x.Categoria)
            .Include(x => x.Fornecedor)
            .FirstAsync(x => x.Id == produto.Id);

        return CreatedAtAction(
            nameof(GetById),
            new { id = produto.Id },
            _mapper.Map<ProdutoResponse>(produto));
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Put(long id, ProdutoRequest request)
    {
        var produto = await _context.Produtos
            .FirstOrDefaultAsync(x => x.Id == id);

        if (produto == null)
            return NotFound();

        var categoriaExiste = await _context.Categorias
            .AnyAsync(x => x.Id == request.CategoriaId);

        if (!categoriaExiste)
            return BadRequest("Categoria não encontrada.");

        var fornecedorExiste = await _context.Fornecedores
            .AnyAsync(x => x.Id == request.FornecedorId);

        if (!fornecedorExiste)
            return BadRequest("Fornecedor não encontrado.");

        _mapper.Map(request, produto);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var produto = await _context.Produtos
            .FirstOrDefaultAsync(x => x.Id == id);

        if (produto == null)
            return NotFound();

        _context.Produtos.Remove(produto);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("estoque-baixo")]
    public async Task<ActionResult<IEnumerable<ProdutoResponse>>> GetEstoqueBaixo()
    {
        var produtos = await _context.Produtos
            .Include(x => x.Categoria)
            .Include(x => x.Fornecedor)
            .AsNoTracking()
            .Where(x => x.Ativo && x.QuantidadeAtual <= x.EstoqueMinimo)
            .OrderBy(x => x.Nome)
            .ToListAsync();

        return Ok(_mapper.Map<IEnumerable<ProdutoResponse>>(produtos));
    }

    [HttpGet("saldo")]
    public async Task<ActionResult<IEnumerable<ProdutoSaldoResponse>>> GetSaldo()
    {
        var produtos = await _context.Produtos
            .AsNoTracking()
            .Where(x => x.Ativo)
            .OrderBy(x => x.Nome)
            .Select(x => new ProdutoSaldoResponse
            {
                Id = x.Id,
                Nome = x.Nome,
                QuantidadeAtual = x.QuantidadeAtual,
                EstoqueMinimo = x.EstoqueMinimo,
                EstoqueBaixo = x.QuantidadeAtual <= x.EstoqueMinimo
            })
            .ToListAsync();

        return Ok(produtos);
    }
}