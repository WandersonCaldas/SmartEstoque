namespace SmartEstoqueAPI.Domain.Entities;

public class Produto
{
    public long Id { get; set; }

    public long CategoriaId { get; set; }

    public long FornecedorId { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string? Descricao { get; set; }

    public decimal QuantidadeAtual { get; set; }

    public decimal EstoqueMinimo { get; set; }

    public decimal ValorUnitario { get; set; }

    public bool Ativo { get; set; } = true;

    public Categoria Categoria { get; set; } = null!;

    public Fornecedor Fornecedor { get; set; } = null!;

    public ICollection<ItemMovimentacaoEstoque> ItensMovimentacao { get; set; } = new List<ItemMovimentacaoEstoque>();
}