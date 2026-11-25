using Microsoft.AspNetCore.Mvc;
using WebApiMuseoVirtual.Features.OrdenesCompra.Dtos;
using WebApiMuseoVirtual.Features.OrdenesCompra.Interfaces;

namespace WebApiMuseoVirtual.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdenesCompraController : ControllerBase
{
    private readonly IOrdenesCompraAppService _appService;

    public OrdenesCompraController(IOrdenesCompraAppService appService)
    {
        _appService = appService;
    }

    /// <summary>Historial de órdenes de un cliente.</summary>
    [HttpGet("cliente/{idCliente}")]
    public async Task<IActionResult> GetHistorial(int idCliente)
    {
        var historial = await _appService.GetHistorialClienteAsync(idCliente);
        return Ok(historial);
    }

    /// <summary>Obtener detalle de una orden.</summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var orden = await _appService.GetByIdAsync(id);
        if (orden == null) return NotFound(new { message = $"Orden {id} no encontrada." });
        return Ok(orden);
    }

    /// <summary>Crear orden de compra con detalles y registrar pago.</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CrearOrdenRequest request)
    {
        try
        {
            var creada = await _appService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = creada.IdOrden }, creada);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
