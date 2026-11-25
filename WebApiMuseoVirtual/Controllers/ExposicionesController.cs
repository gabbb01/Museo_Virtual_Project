using Microsoft.AspNetCore.Mvc;
using WebApiMuseoVirtual.Features.Exposiciones.Dtos;
using WebApiMuseoVirtual.Features.Exposiciones.Interfaces;

namespace WebApiMuseoVirtual.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExposicionesController : ControllerBase
{
    private readonly IExposicionesAppService _appService;

    public ExposicionesController(IExposicionesAppService appService)
    {
        _appService = appService;
    }

    /// <summary>Listar exposiciones activas con sala y estado.</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var exposiciones = await _appService.GetAllAsync();
        return Ok(exposiciones);
    }

    /// <summary>Obtener exposición por ID.</summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var exposicion = await _appService.GetByIdAsync(id);
        if (exposicion == null) return NotFound(new { message = $"Exposición {id} no encontrada." });
        return Ok(exposicion);
    }

    /// <summary>Crear exposición.</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CrearExposicionRequest request)
    {
        var creada = await _appService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = creada.IdExposicion }, creada);
    }

    /// <summary>Editar exposición.</summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] EditarExposicionRequest request)
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

    /// <summary>Listar obras de una exposición.</summary>
    [HttpGet("{id}/obras")]
    public async Task<IActionResult> GetObras(int id)
    {
        var obras = await _appService.GetObrasAsync(id);
        return Ok(obras);
    }

    /// <summary>Agregar obra a una exposición.</summary>
    [HttpPost("{id}/obras")]
    public async Task<IActionResult> AddObra(int id, [FromBody] AgregarObraExposicionRequest request)
    {
        await _appService.AddObraAsync(id, request);
        return Ok(new { message = "Obra agregada a la exposición." });
    }

    /// <summary>Quitar obra de una exposición.</summary>
    [HttpDelete("{id}/obras/{idObra}")]
    public async Task<IActionResult> RemoveObra(int id, int idObra)
    {
        await _appService.RemoveObraAsync(id, idObra);
        return NoContent();
    }
}
