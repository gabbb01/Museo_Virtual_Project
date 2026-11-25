using System.Security.Cryptography;
using System.Text;
using WebApiMuseoVirtual.Features.Autenticacion.Dtos;
using WebApiMuseoVirtual.Features.Autenticacion.Interfaces;
using WebApiMuseoVirtual.Models;

namespace WebApiMuseoVirtual.Features.Autenticacion.DomainServices;

public class AuthDomainService : IAuthDomainService
{
    public string HashPassword(string password)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(bytes).ToLower();
    }

    public bool VerifyPassword(string password, string hash)
    {
        return string.Equals(HashPassword(password), hash, StringComparison.OrdinalIgnoreCase);
    }

    public (Persona persona, Usuario usuario) MapFromRegistroRequest(RegistroRequest request)
    {
        var persona = new Persona
        {
            Pnombre = request.Pnombre,
            Snombre = request.Snombre,
            Papellido = request.Papellido,
            Sapellido = request.Sapellido,
            Identificacion = request.Identificacion,
            FechaNacimiento = request.FechaNacimiento,
            Telefono = request.Telefono,
            Direccion = request.Direccion,
            IdCiudad = request.IdCiudad,
            FechaCreacion = DateTime.Now
        };

        var usuario = new Usuario
        {
            Correo = request.Correo,
            ContrasenaHash = HashPassword(request.Contrasena),
            IdRol = request.IdRol,
            FechaRegistro = DateTime.Now,
            Activo = true
        };

        return (persona, usuario);
    }

    public LoginResponse MapToLoginResponse(VwUsuariosRole vw)
    {
        return new LoginResponse
        {
            IdPersona = vw.IdPersona,
            NombreCompleto = vw.NombreCompleto,
            Correo = vw.Correo,
            Rol = vw.Rol,
            Ciudad = vw.Ciudad,
            Pais = vw.Pais,
            FechaRegistro = vw.FechaRegistro
        };
    }

    public RegistroResponse MapToRegistroResponse(VwUsuariosRole vw)
    {
        return new RegistroResponse
        {
            IdPersona = vw.IdPersona,
            NombreCompleto = vw.NombreCompleto,
            Correo = vw.Correo,
            Rol = vw.Rol
        };
    }
}
