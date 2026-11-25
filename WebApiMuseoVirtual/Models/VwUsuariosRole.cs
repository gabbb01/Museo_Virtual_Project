using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

public partial class VwUsuariosRole
{
    public int IdPersona { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public string? Identificacion { get; set; }

    public string Correo { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public string? Ciudad { get; set; }

    public string? Pais { get; set; }

    public DateTime FechaRegistro { get; set; }

    public bool Activo { get; set; }
}
