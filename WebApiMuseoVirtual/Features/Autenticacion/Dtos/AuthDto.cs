namespace WebApiMuseoVirtual.Features.Autenticacion.Dtos;

public class LoginRequest
{
    public string Correo { get; set; } = null!;
    public string Contrasena { get; set; } = null!;
}

public class LoginResponse
{
    public int IdPersona { get; set; }
    public string NombreCompleto { get; set; } = null!;
    public string Correo { get; set; } = null!;
    public string Rol { get; set; } = null!;
    public string? Ciudad { get; set; }
    public string? Pais { get; set; }
    public DateTime FechaRegistro { get; set; }
}

public class RegistroRequest
{
    public string Pnombre { get; set; } = null!;
    public string? Snombre { get; set; }
    public string Papellido { get; set; } = null!;
    public string? Sapellido { get; set; }
    public string? Identificacion { get; set; }
    public DateOnly? FechaNacimiento { get; set; }
    public string? Telefono { get; set; }
    public string? Direccion { get; set; }
    public int? IdCiudad { get; set; }
    public string Correo { get; set; } = null!;
    public string Contrasena { get; set; } = null!;
    public int IdRol { get; set; }
}

public class RegistroResponse
{
    public int IdPersona { get; set; }
    public string NombreCompleto { get; set; } = null!;
    public string Correo { get; set; } = null!;
    public string Rol { get; set; } = null!;
}
