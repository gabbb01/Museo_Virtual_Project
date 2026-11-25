using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Relación N:M entre exposiciones y obras exhibidas
/// </summary>
public partial class ExposicionObra
{
    /// <summary>
    /// Exposición
    /// </summary>
    public int IdExposicion { get; set; }

    /// <summary>
    /// Obra exhibida
    /// </summary>
    public int IdObra { get; set; }

    /// <summary>
    /// Orden en que se muestra la obra
    /// </summary>
    public int OrdenVisualizacion { get; set; }

    /// <summary>
    /// Ubicación de la obra dentro de la sala (ej. Pared norte)
    /// </summary>
    public string? PosicionEnSala { get; set; }

    public virtual Exposicion IdExposicionNavigation { get; set; } = null!;

    public virtual Obra IdObraNavigation { get; set; } = null!;
}
