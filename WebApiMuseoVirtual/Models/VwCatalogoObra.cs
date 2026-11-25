using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

public partial class VwCatalogoObra
{
    public int IdObra { get; set; }

    public string Titulo { get; set; } = null!;

    public short? AnioCreacion { get; set; }

    public string? Artista { get; set; }

    public string Categoria { get; set; } = null!;

    public string Tecnica { get; set; } = null!;

    public string? Movimiento { get; set; }

    public string EstadoConservacion { get; set; } = null!;

    public bool EnVenta { get; set; }

    public decimal? Precio { get; set; }

    public string? Moneda { get; set; }
}
