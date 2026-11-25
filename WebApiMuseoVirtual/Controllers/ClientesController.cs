using Microsoft.AspNetCore.Mvc;
using WebApiMuseoVirtual.Features.Clientes.Dtos;
using WebApiMuseoVirtual.Features.Clientes.Interfaces;

namespace WebApiMuseoVirtual.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IClientesAppService _appService;

    public ClientesController(IClientesAppService appService)
    {
        _appService = appService;
    }

    /// <summary>Listar clientes con filtros opcionales.</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? idCiudad, [FromQuery] bool? tieneCompras)
    {
        var clientes = await _appService.GetAllAsync(idCiudad, tieneCompras);
        return Ok(clientes);
    }

    /// <summary>Obtener cliente por ID.</summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var cliente = await _appService.GetByIdAsync(id);
        if (cliente == null) return NotFound(new { message = $"Cliente {id} no encontrado." });
        return Ok(cliente);
    }

    /// <summary>Registrar nuevo cliente (crea Persona + Cliente).</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CrearClienteRequest request)
    {
        var creado = await _appService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = creado.IdPersona }, creado);
    }

    /// <summary>Editar datos de un cliente.</summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] EditarClienteRequest request)
    {
        try
        {
            var actualizado = await _appService.UpdateAsync(id, request);
            return Ok(actualizado);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    /// <summary>Eliminar cliente.</summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _appService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
