using SmartEstoqueAPI.Domain.Enums;

namespace SmartEstoqueAPI.Api.Responses;

public class MovimentacaoEstoqueResponse
{
    public long Id { get; set; }

    public TipoMovimentacao TipoMovimentacao { get; set; }

    public DateTime DataMovimentacao { get; set; }

    public string? Observacao { get; set; }

    public List<ItemMovimentacaoEstoqueResponse> Itens { get; set; } = [];
}