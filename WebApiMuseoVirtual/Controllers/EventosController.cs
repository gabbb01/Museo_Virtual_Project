using Microsoft.AspNetCore.Mvc;
using WebApiMuseoVirtual.Features.Eventos.Dtos;
using WebApiMuseoVirtual.Features.Eventos.Interfaces;

namespace WebApiMuseoVirtual.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventosController : ControllerBase
{
    private readonly IEventosAppService _appService;

    public EventosController(IEventosAppService appService)
    {
        _appService = appService;
    }

    /// <summary>Listar todos los eventos.</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var eventos = await _appService.GetAllAsync();
        return Ok(eventos);
    }

    /// <summary>Obtener evento por ID.</summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var evento = await _appService.GetByIdAsync(id);
        if (evento == null) return NotFound(new { message = $"Evento {id} no encontrado." });
        return Ok(evento);
    }

    /// <summary>Crear evento.</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CrearEventoRequest request)
    {
        var creado = await _appService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = creado.IdEvento }, creado);
    }

    /// <summary>Crear entrada para una exposición/evento (genera QR).</summary>
    [HttpPost("entradas")]
    public async Task<IActionResult> CrearEntrada([FromBody] CrearEntradaRequest request)
    {
        var entrada = await _appService.CrearEntradaAsync(request);
        return CreatedAtAction(null, entrada);
    }

    /// <summary>Consultar entradas de un usuario.</summary>
    [HttpGet("entradas/usuario/{idUsuario}")]
    public async Task<IActionResult> GetEntradasByUsuario(int idUsuario)
    {
        var entradas = await _appService.GetEntradasByUsuarioAsync(idUsuario);
        return Ok(entradas);
    }
}
