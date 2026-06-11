namespace SmartEstoqueAPI.Api.Requests;

public class CategoriaRequest
{
    public string Nome { get; set; } = string.Empty;

    public string? Descricao { get; set; }

    public bool Ativo { get; set; } = true;
}