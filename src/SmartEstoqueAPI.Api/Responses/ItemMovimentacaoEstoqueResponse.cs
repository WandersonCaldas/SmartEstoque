namespace SmartEstoqueAPI.Api.Responses;

public class ItemMovimentacaoEstoqueResponse
{
    public long ProdutoId { get; set; }

    public string Produto { get; set; } = string.Empty;

    public decimal Quantidade { get; set; }

    public decimal ValorUnitario { get; set; }
}