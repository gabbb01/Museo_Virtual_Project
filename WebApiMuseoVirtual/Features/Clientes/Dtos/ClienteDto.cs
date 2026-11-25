namespace WebApiMuseoVirtual.Features.Clientes.Dtos;

public class CrearClienteRequest
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
    public string? Rtn { get; set; }
    public string? DireccionEnvio { get; set; }
}

public class EditarClienteRequest
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
    public string? Rtn { get; set; }
    public string? DireccionEnvio { get; set; }
}

public class ClienteResponse
{
    public int IdPersona { get; set; }
    public string NombreCompleto { get; set; } = null!;
    public string? Identificacion { get; set; }
    public DateOnly? FechaNacimiento { get; set; }
    public string? Telefono { get; set; }
    public string? Direccion { get; set; }
    public int? IdCiudad { get; set; }
    public string? Ciudad { get; set; }
    public string? Rtn { get; set; }
    public string? DireccionEnvio { get; set; }
    public DateTime FechaAlta { get; set; }
    public int TotalCompras { get; set; }
}
