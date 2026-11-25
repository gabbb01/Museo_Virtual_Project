using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Historial de revisiones y restauraciones de cada obra
/// </summary>
public partial class HistorialConservacion
{
    /// <summary>
    /// Identificador único del registro de conservación
    /// </summary>
    public int IdHistorial { get; set; }

    /// <summary>
    /// Obra evaluada
    /// </summary>
    public int IdObra { get; set; }

    /// <summary>
    /// Estado determinado en la revisión
    /// </summary>
    public int IdEstadoConservacion { get; set; }

    /// <summary>
    /// Fecha de la revisión o restauración
    /// </summary>
    public DateOnly Fecha { get; set; }

    /// <summary>
    /// Persona que realizó la revisión
    /// </summary>
    public string Responsable { get; set; } = null!;

    /// <summary>
    /// Observaciones o trabajos realizados
    /// </summary>
    public string? Observaciones { get; set; }

    public virtual EstadoConservacion IdEstadoConservacionNavigation { get; set; } = null!;

    public virtual Obra IdObraNavigation { get; set; } = null!;
}
