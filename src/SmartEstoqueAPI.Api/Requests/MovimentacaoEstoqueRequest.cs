using SmartEstoqueAPI.Domain.Enums;

namespace SmartEstoqueAPI.Api.Requests;

public class MovimentacaoEstoqueRequest
{
    public TipoMovimentacao TipoMovimentacao { get; set; }

    public string? Observacao { get; set; }

    public List<ItemMovimentacaoEstoqueRequest> Itens { get; set; } = [];
}