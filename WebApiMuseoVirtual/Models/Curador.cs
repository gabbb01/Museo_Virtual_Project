using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Subtipo de persona: curadores responsables de las exposiciones
/// </summary>
public partial class Curador
{
    /// <summary>
    /// PK y FK hacia persona (subtipo de persona)
    /// </summary>
    public int IdPersona { get; set; }

    /// <summary>
    /// Área de especialidad del curador
    /// </summary>
    public string? Especialidad { get; set; }

    /// <summary>
    /// Reseña profesional del curador
    /// </summary>
    public string? Biografia { get; set; }

    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual ICollection<Exposicion> IdExposicions { get; set; } = new List<Exposicion>();
}
