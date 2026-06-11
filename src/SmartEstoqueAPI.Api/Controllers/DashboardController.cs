using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartEstoqueAPI.Api.Responses;
using SmartEstoqueAPI.Infrastructure.Data;

namespace SmartEstoqueAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly AppDbContext _context;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<DashboardResponse>> Get()
    {
        var response = new DashboardResponse
        {
            TotalCategorias = await _context.Categorias.CountAsync(),

            TotalFornecedores = await _context.Fornecedores.CountAsync(),

            TotalProdutos = await _context.Produtos.CountAsync(),

            ProdutosAtivos = await _context.Produtos
                .CountAsync(x => x.Ativo),

            ProdutosComEstoqueBaixo = await _context.Produtos
                .CountAsync(x => x.Ativo && x.QuantidadeAtual <= x.EstoqueMinimo),

            ValorTotalEstoque = await _context.Produtos
                .Where(x => x.Ativo)
                .SumAsync(x => x.QuantidadeAtual * x.ValorUnitario),

            TotalMovimentacoes = await _context.MovimentacoesEstoque.CountAsync()
        };

        return Ok(response);
    }
}