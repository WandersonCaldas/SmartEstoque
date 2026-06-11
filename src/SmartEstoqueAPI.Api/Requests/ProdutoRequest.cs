namespace SmartEstoqueAPI.Api.Requests;

public class ProdutoRequest
{
    public long CategoriaId { get; set; }

    public long FornecedorId { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string? Descricao { get; set; }

    public decimal QuantidadeAtual { get; set; }

    public decimal EstoqueMinimo { get; set; }

    public decimal ValorUnitario { get; set; }

    public bool Ativo { get; set; } = true;
}