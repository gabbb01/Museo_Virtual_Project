using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

public partial class VwEntradasPorExposicion
{
    public int IdExposicion { get; set; }

    public string Exposicion { get; set; } = null!;

    public string TipoEntrada { get; set; } = null!;

    public int? TotalEntradas { get; set; }

    public int? EntradasUtilizadas { get; set; }

    public decimal? RecaudacionEstimada { get; set; }
}
