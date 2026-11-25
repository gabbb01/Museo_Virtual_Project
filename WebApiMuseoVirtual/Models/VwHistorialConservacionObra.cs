using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

public partial class VwHistorialConservacionObra
{
    public int IdHistorial { get; set; }

    public string Obra { get; set; } = null!;

    public string EstadoRegistrado { get; set; } = null!;

    public DateOnly FechaRevision { get; set; }

    public string Responsable { get; set; } = null!;

    public string? Observaciones { get; set; }
}
