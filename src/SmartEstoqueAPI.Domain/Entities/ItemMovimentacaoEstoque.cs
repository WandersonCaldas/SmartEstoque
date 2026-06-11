namespace SmartEstoqueAPI.Domain.Entities;

public class ItemMovimentacaoEstoque
{
    public long Id { get; set; }

    public long MovimentacaoEstoqueId { get; set; }

    public long ProdutoId { get; set; }

    public decimal Quantidade { get; set; }

    public decimal ValorUnitario { get; set; }

    public MovimentacaoEstoque MovimentacaoEstoque { get; set; } = null!;

    public Produto Produto { get; set; } = null!;
}