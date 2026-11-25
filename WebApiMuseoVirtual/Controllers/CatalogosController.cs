using Microsoft.AspNetCore.Mvc;
using WebApiMuseoVirtual.Features.Catalogos.Interfaces;

namespace WebApiMuseoVirtual.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogosController : ControllerBase
{
    private readonly ICatalogosAppService _appService;

    public CatalogosController(ICatalogosAppService appService)
    {
        _appService = appService;
    }

    /// <summary>GET categorías de obra.</summary>
    [HttpGet("categorias")]
    public async Task<IActionResult> GetCategorias()
        => Ok(await _appService.GetCategoriasAsync());

    /// <summary>GET técnicas artísticas.</summary>
    [HttpGet("tecnicas")]
    public async Task<IActionResult> GetTecnicas()
        => Ok(await _appService.GetTecnicasAsync());

    /// <summary>GET movimientos artísticos.</summary>
    [HttpGet("movimientos")]
    public async Task<IActionResult> GetMovimientos()
        => Ok(await _appService.GetMovimientosAsync());

    /// <summary>GET estados de conservación.</summary>
    [HttpGet("estados-conservacion")]
    public async Task<IActionResult> GetEstadosConservacion()
        => Ok(await _appService.GetEstadosConservacionAsync());

    /// <summary>GET salas virtuales activas.</summary>
    [HttpGet("salas-virtuales")]
    public async Task<IActionResult> GetSalasVirtuales()
        => Ok(await _appService.GetSalasVirtualesAsync());

    /// <summary>GET métodos de pago habilitados.</summary>
    [HttpGet("metodos-pago")]
    public async Task<IActionResult> GetMetodosPago()
        => Ok(await _appService.GetMetodosPagoAsync());

    /// <summary>GET tipos de entrada.</summary>
    [HttpGet("tipos-entrada")]
    public async Task<IActionResult> GetTiposEntrada()
        => Ok(await _appService.GetTiposEntradaAsync());

    /// <summary>GET tipos de evento.</summary>
    [HttpGet("tipos-evento")]
    public async Task<IActionResult> GetTiposEvento()
        => Ok(await _appService.GetTiposEventoAsync());

    /// <summary>GET monedas.</summary>
    [HttpGet("monedas")]
    public async Task<IActionResult> GetMonedas()
        => Ok(await _appService.GetMonedasAsync());

    /// <summary>GET roles del sistema.</summary>
    [HttpGet("roles")]
    public async Task<IActionResult> GetRoles()
        => Ok(await _appService.GetRolesAsync());
}
