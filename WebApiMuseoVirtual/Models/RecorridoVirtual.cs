using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Recorridos guiados dentro de una exposición
/// </summary>
public partial class RecorridoVirtual
{
    /// <summary>
    /// Identificador único del recorrido
    /// </summary>
    public int IdRecorrido { get; set; }

    /// <summary>
    /// Exposición que recorre
    /// </summary>
    public int IdExposicion { get; set; }

    /// <summary>
    /// Nombre del recorrido guiado
    /// </summary>
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Descripción del recorrido
    /// </summary>
    public string? Descripcion { get; set; }

    /// <summary>
    /// Duración estimada en minutos
    /// </summary>
    public int? DuracionEstimadaMin { get; set; }

    public virtual Exposicion IdExposicionNavigation { get; set; } = null!;

    public virtual ICollection<RecorridoParadum> RecorridoParada { get; set; } = new List<RecorridoParadum>();
}
