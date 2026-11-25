using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Catálogo de tipos de evento
/// </summary>
public partial class TipoEvento
{
    /// <summary>
    /// Identificador único del tipo de evento
    /// </summary>
    public int IdTipoEvento { get; set; }

    /// <summary>
    /// Nombre del tipo (inauguración, taller, conferencia)
    /// </summary>
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Descripción del tipo de evento
    /// </summary>
    public string? Descripcion { get; set; }

    public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();
}
