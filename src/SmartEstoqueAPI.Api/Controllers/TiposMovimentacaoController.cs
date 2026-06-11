using Microsoft.AspNetCore.Mvc;
using SmartEstoqueAPI.Domain.Enums;

namespace SmartEstoqueAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TiposMovimentacaoController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var tipos = Enum.GetValues<TipoMovimentacao>()
            .Select(x => new
            {
                Id = (int)x,
                Nome = x.ToString()
            })
            .ToList();

        return Ok(tipos);
    }
}