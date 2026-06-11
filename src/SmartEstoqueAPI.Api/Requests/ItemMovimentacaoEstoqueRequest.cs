namespace SmartEstoqueAPI.Api.Requests;

public class ItemMovimentacaoEstoqueRequest
{
    public long ProdutoId { get; set; }

    public decimal Quantidade { get; set; }

    public decimal ValorUnitario { get; set; }
}