using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Catálogo de técnicas artísticas
/// </summary>
public partial class Tecnica
{
    /// <summary>
    /// Identificador único de la técnica
    /// </summary>
    public int IdTecnica { get; set; }

    /// <summary>
    /// Nombre de la técnica (óleo, acuarela, grabado)
    /// </summary>
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Descripción de la técnica
    /// </summary>
    public string? Descripcion { get; set; }

    public virtual ICollection<Obra> Obras { get; set; } = new List<Obra>();
}
