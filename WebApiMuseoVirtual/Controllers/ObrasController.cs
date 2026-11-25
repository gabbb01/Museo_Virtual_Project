using Microsoft.AspNetCore.Mvc;
using WebApiMuseoVirtual.Features.Obras.Dtos;
using WebApiMuseoVirtual.Features.Obras.Interfaces;

namespace WebApiMuseoVirtual.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ObrasController : ControllerBase
{
    private readonly IObrasAppService _appService;

    public ObrasController(IObrasAppService appService)
    {
        _appService = appService;
    }

    /// <summary>Listar obras. Filtrar por activa=true|false.</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool? activa)
    {
        var obras = await _appService.GetAllAsync(activa);
        return Ok(obras);
    }

    /// <summary>Catálogo público de obras (usa vista vw_catalogo_obra).</summary>
    [HttpGet("catalogo")]
    public async Task<IActionResult> GetCatalogo()
    {
        var catalogo = await _appService.GetCatalogoAsync();
        return Ok(catalogo);
    }

    /// <summary>Obtener obra por ID.</summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var obra = await _appService.GetByIdAsync(id);
        if (obra == null) return NotFound(new { message = $"Obra {id} no encontrada." });
        return Ok(obra);
    }

    /// <summary>Crear nueva obra.</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CrearObraRequest request)
    {
        var creada = await _appService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = creada.IdObra }, creada);
    }

    /// <summary>Editar obra.</summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] EditarObraRequest request)
    {
        try
        {
            var actualizada = await _appService.UpdateAsync(id, request);
            return Ok(actualizada);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    /// <summary>Activar o desactivar obra.</summary>
    [HttpPatch("{id}/activa")]
    public async Task<IActionResult> SetActiva(int id, [FromQuery] bool activa)
    {
        try
        {
            var obra = await _appService.SetActivaAsync(id, activa);
            return Ok(obra);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
