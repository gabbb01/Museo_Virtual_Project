using WebApiMuseoVirtual.Features.Autenticacion.Dtos;
using WebApiMuseoVirtual.Features.Autenticacion.Interfaces;
using WebApiMuseoVirtual.Infrastructure.Interfaces;

namespace WebApiMuseoVirtual.Features.Autenticacion.AppServices;

public class AuthAppService : IAuthAppService
{
    private readonly IAuthRepository _repository;
    private readonly IAuthDomainService _domainService;

    public AuthAppService(IAuthRepository repository, IAuthDomainService domainService)
    {
        _repository = repository;
        _domainService = domainService;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var usuario = await _repository.GetByCorreoAsync(request.Correo);
        if (usuario == null)
            return null;

        var hash = _domainService.HashPassword(request.Contrasena);
        if (!string.Equals(usuario.ContrasenaHash, hash, StringComparison.OrdinalIgnoreCase))
            return null;

        var vw = await _repository.GetUsuarioRoleByPersonaAsync(usuario.IdPersona);
        return vw == null ? null : _domainService.MapToLoginResponse(vw);
    }

    public async Task<RegistroResponse> RegistroAsync(RegistroRequest request)
    {
        var (persona, usuario) = _domainService.MapFromRegistroRequest(request);
        var creado = await _repository.RegisterAsync(persona, usuario);
        var vw = await _repository.GetUsuarioRoleByPersonaAsync(creado.IdPersona);
        return _domainService.MapToRegistroResponse(vw!);
    }
}
