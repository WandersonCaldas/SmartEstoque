namespace SmartEstoqueAPI.Api.Responses;

public class CategoriaResponse
{
    public long Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string? Descricao { get; set; }

    public bool Ativo { get; set; }
}