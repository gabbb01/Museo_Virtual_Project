using WebApiMuseoVirtual.Features.Autenticacion.Dtos;

namespace WebApiMuseoVirtual.Features.Autenticacion.Interfaces;

public interface IAuthAppService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
    Task<RegistroResponse> RegistroAsync(RegistroRequest request);
}
