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
public class FornecedoresController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public FornecedoresController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FornecedorResponse>>> Get()
    {
        var fornecedores = await _context.Fornecedores
            .AsNoTracking()
            .ToListAsync();

        return Ok(_mapper.Map<IEnumerable<FornecedorResponse>>(fornecedores));
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<FornecedorResponse>> GetById(long id)
    {
        var fornecedor = await _context.Fornecedores
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (fornecedor == null)
            return NotFound();

        return Ok(_mapper.Map<FornecedorResponse>(fornecedor));
    }

    [HttpPost]
    public async Task<ActionResult<FornecedorResponse>> Post(FornecedorRequest request)
    {
        var fornecedor = _mapper.Map<Fornecedor>(request);

        _context.Fornecedores.Add(fornecedor);
        await _context.SaveChangesAsync();

        var response = _mapper.Map<FornecedorResponse>(fornecedor);

        return CreatedAtAction(nameof(GetById), new { id = fornecedor.Id }, response);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Put(long id, FornecedorRequest request)
    {
        var fornecedor = await _context.Fornecedores.FindAsync(id);

        if (fornecedor == null)
            return NotFound();

        _mapper.Map(request, fornecedor);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var fornecedor = await _context.Fornecedores.FindAsync(id);

        if (fornecedor == null)
            return NotFound();

        _context.Fornecedores.Remove(fornecedor);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}