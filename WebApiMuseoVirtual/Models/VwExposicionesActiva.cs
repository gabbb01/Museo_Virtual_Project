using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

public partial class VwExposicionesActiva
{
    public int IdExposicion { get; set; }

    public string Exposicion { get; set; } = null!;

    public string SalaVirtual { get; set; } = null!;

    public int CapacidadVisitantes { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public bool EsPermanente { get; set; }

    public int? TotalObrasExhibidas { get; set; }
}
