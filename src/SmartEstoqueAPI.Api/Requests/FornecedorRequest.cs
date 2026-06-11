namespace SmartEstoqueAPI.Api.Requests;

public class FornecedorRequest
{
    public string RazaoSocial { get; set; } = string.Empty;

    public string? NomeFantasia { get; set; }

    public string? Cnpj { get; set; }

    public string? Email { get; set; }

    public string? Telefone { get; set; }

    public bool Ativo { get; set; } = true;
}