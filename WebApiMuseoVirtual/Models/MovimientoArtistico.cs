using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Catálogo de movimientos artísticos
/// </summary>
public partial class MovimientoArtistico
{
    /// <summary>
    /// Identificador único del movimiento artístico
    /// </summary>
    public int IdMovimiento { get; set; }

    /// <summary>
    /// Nombre del movimiento (Impresionismo, Cubismo)
    /// </summary>
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Descripción del movimiento
    /// </summary>
    public string? Descripcion { get; set; }

    /// <summary>
    /// Año aproximado de inicio
    /// </summary>
    public short? AnioInicio { get; set; }

    /// <summary>
    /// Año aproximado de fin
    /// </summary>
    public short? AnioFin { get; set; }

    public virtual ICollection<Obra> Obras { get; set; } = new List<Obra>();
}
