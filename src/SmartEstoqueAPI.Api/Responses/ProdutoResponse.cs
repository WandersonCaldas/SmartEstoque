namespace SmartEstoqueAPI.Api.Responses;

public class ProdutoResponse
{
    public long Id { get; set; }

    public long CategoriaId { get; set; }

    public string Categoria { get; set; } = string.Empty;

    public long FornecedorId { get; set; }

    public string Fornecedor { get; set; } = string.Empty;

    public string Nome { get; set; } = string.Empty;

    public string? Descricao { get; set; }

    public decimal QuantidadeAtual { get; set; }

    public decimal EstoqueMinimo { get; set; }

    public decimal ValorUnitario { get; set; }

    public bool Ativo { get; set; }
}

public class ProdutoSaldoResponse
{
    public long Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public decimal QuantidadeAtual { get; set; }

    public decimal EstoqueMinimo { get; set; }

    public bool EstoqueBaixo { get; set; }
}