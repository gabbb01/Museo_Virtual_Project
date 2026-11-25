using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Salas virtuales donde se montan las exposiciones
/// </summary>
public partial class SalaVirtual
{
    /// <summary>
    /// Identificador único de la sala
    /// </summary>
    public int IdSala { get; set; }

    /// <summary>
    /// Nombre de la sala virtual
    /// </summary>
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Descripción de la sala
    /// </summary>
    public string? Descripcion { get; set; }

    /// <summary>
    /// Máximo de visitantes simultáneos
    /// </summary>
    public int CapacidadVisitantes { get; set; }

    /// <summary>
    /// URL del entorno 3D de la sala
    /// </summary>
    public string? UrlEntorno3d { get; set; }

    /// <summary>
    /// Indica si la sala está disponible
    /// </summary>
    public bool Activa { get; set; }

    public virtual ICollection<Exposicion> Exposicions { get; set; } = new List<Exposicion>();
}
