using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Catálogo de categorías de obras
/// </summary>
public partial class CategoriaObra
{
    /// <summary>
    /// Identificador único de la categoría
    /// </summary>
    public int IdCategoria { get; set; }

    /// <summary>
    /// Nombre de la categoría (pintura, escultura, fotografía)
    /// </summary>
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Descripción de la categoría
    /// </summary>
    public string? Descripcion { get; set; }

    public virtual ICollection<Obra> Obras { get; set; } = new List<Obra>();
}
