using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Catálogo de estados de conservación de las obras
/// </summary>
public partial class EstadoConservacion
{
    /// <summary>
    /// Identificador único del estado de conservación
    /// </summary>
    public int IdEstadoConservacion { get; set; }

    /// <summary>
    /// Nombre del estado (excelente, bueno, en restauración)
    /// </summary>
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Descripción del estado
    /// </summary>
    public string? Descripcion { get; set; }

    public virtual ICollection<HistorialConservacion> HistorialConservacions { get; set; } = new List<HistorialConservacion>();

    public virtual ICollection<Obra> Obras { get; set; } = new List<Obra>();
}
