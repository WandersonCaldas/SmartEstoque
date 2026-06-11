using SmartEstoqueAPI.Domain.Enums;

namespace SmartEstoqueAPI.Domain.Entities;

public class MovimentacaoEstoque
{
    public long Id { get; set; }

    public TipoMovimentacao TipoMovimentacao { get; set; }

    public DateTime DataMovimentacao { get; set; } = DateTime.Now;

    public string? Observacao { get; set; }

    public ICollection<ItemMovimentacaoEstoque> Itens { get; set; } = new List<ItemMovimentacaoEstoque>();
}