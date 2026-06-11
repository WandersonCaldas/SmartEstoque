namespace SmartEstoqueAPI.Api.Responses;

public class DashboardResponse
{
    public int TotalCategorias { get; set; }

    public int TotalFornecedores { get; set; }

    public int TotalProdutos { get; set; }

    public int ProdutosAtivos { get; set; }

    public int ProdutosComEstoqueBaixo { get; set; }

    public decimal ValorTotalEstoque { get; set; }

    public int TotalMovimentacoes { get; set; }
}