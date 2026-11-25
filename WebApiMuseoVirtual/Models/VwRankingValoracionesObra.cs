using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

public partial class VwRankingValoracionesObra
{
    public int IdObra { get; set; }

    public string Titulo { get; set; } = null!;

    public int? CantidadResenas { get; set; }

    public decimal? PromedioPuntuacion { get; set; }
}
