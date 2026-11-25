using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

public partial class VwAuditoriaAcceso
{
    public int IdBitacora { get; set; }

    public DateTime FechaHora { get; set; }

    public string Correo { get; set; } = null!;

    public string Usuario { get; set; } = null!;

    public string? IpOrigen { get; set; }

    public string Accion { get; set; } = null!;

    public bool Exitoso { get; set; }
}
