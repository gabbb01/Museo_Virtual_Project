using WebApiMuseoVirtual.Models;

namespace WebApiMuseoVirtual.Infrastructure.Interfaces;

public interface IAuthRepository
{
    Task<Usuario?> GetByCorreoAsync(string correo);
    Task<Usuario> RegisterAsync(Persona persona, Usuario usuario);
    Task<VwUsuariosRole?> GetUsuarioRoleByPersonaAsync(int idPersona);
}
