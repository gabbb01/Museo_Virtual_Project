using Microsoft.AspNetCore.Mvc;
using WebApiMuseoVirtual.Features.Autenticacion.Dtos;
using WebApiMuseoVirtual.Features.Autenticacion.Interfaces;

namespace WebApiMuseoVirtual.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthAppService _appService;

    public AuthController(IAuthAppService appService)
    {
        _appService = appService;
    }

    /// <summary>Login: valida credenciales y retorna datos del usuario y rol.</summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var resultado = await _appService.LoginAsync(request);
        if (resultado == null)
            return Unauthorized(new { message = "Correo o contraseña incorrectos." });
        return Ok(resultado);
    }

    /// <summary>Registro: crea Persona + Usuario.</summary>
    [HttpPost("registro")]
    public async Task<IActionResult> Registro([FromBody] RegistroRequest request)
    {
        var creado = await _appService.RegistroAsync(request);
        return CreatedAtAction(null, creado);
    }
}
