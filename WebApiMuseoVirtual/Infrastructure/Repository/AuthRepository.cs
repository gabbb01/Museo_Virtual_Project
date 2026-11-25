using Microsoft.EntityFrameworkCore;
using WebApiMuseoVirtual.Infrastructure.Interfaces;
using WebApiMuseoVirtual.Models;

namespace WebApiMuseoVirtual.Infrastructure.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly MuseoVirtualContext _context;

    public AuthRepository(MuseoVirtualContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> GetByCorreoAsync(string correo)
    {
        return await _context.Usuarios
            .Include(u => u.IdPersonaNavigation)
            .Include(u => u.IdRolNavigation)
            .FirstOrDefaultAsync(u => u.Correo == correo && u.Activo);
    }

    public async Task<Usuario> RegisterAsync(Persona persona, Usuario usuario)
    {
        _context.Personas.Add(persona);
        await _context.SaveChangesAsync();

        usuario.IdPersona = persona.IdPersona;
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return usuario;
    }

    public async Task<VwUsuariosRole?> GetUsuarioRoleByPersonaAsync(int idPersona)
    {
        return await _context.VwUsuariosRoles
            .FirstOrDefaultAsync(v => v.IdPersona == idPersona);
    }
}
