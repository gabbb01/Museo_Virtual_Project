using WebApiMuseoVirtual.Features.Autenticacion.Dtos;
using WebApiMuseoVirtual.Models;

namespace WebApiMuseoVirtual.Features.Autenticacion.Interfaces;

public interface IAuthDomainService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hash);
    (Persona persona, Usuario usuario) MapFromRegistroRequest(RegistroRequest request);
    LoginResponse MapToLoginResponse(VwUsuariosRole vw);
    RegistroResponse MapToRegistroResponse(VwUsuariosRole vw);
}
