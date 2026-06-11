using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartEstoqueAPI.Api.Requests;
using SmartEstoqueAPI.Api.Responses;
using SmartEstoqueAPI.Domain.Entities;
using SmartEstoqueAPI.Infrastructure.Data;

namespace SmartEstoqueAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CategoriasController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaResponse>>> Get()
    {
        var categorias = await _context.Categorias
            .AsNoTracking()
            .ToListAsync();

        var response = _mapper.Map<IEnumerable<CategoriaResponse>>(categorias);

        return Ok(response);
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<CategoriaResponse>> GetById(long id)
    {
        var categoria = await _context.Categorias
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (categoria == null)
            return NotFound();

        var response = _mapper.Map<CategoriaResponse>(categoria);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaResponse>> Post(CategoriaRequest request)
    {
        var categoria = _mapper.Map<Categoria>(request);

        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        var response = _mapper.Map<CategoriaResponse>(categoria);

        return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, response);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Put(long id, CategoriaRequest request)
    {
        var categoria = await _context.Categorias.FindAsync(id);

        if (categoria == null)
            return NotFound();

        _mapper.Map(request, categoria);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var categoria = await _context.Categorias.FindAsync(id);

        if (categoria == null)
            return NotFound();

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}