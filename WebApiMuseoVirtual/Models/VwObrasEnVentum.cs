using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

public partial class VwObrasEnVentum
{
    public int IdObra { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Artista { get; set; }

    public string Categoria { get; set; } = null!;

    public decimal? Precio { get; set; }

    public string? PrecioFormateado { get; set; }
}
