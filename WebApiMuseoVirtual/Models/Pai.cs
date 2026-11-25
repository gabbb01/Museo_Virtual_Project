using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Catálogo de países
/// </summary>
public partial class Pai
{
    /// <summary>
    /// Identificador único del país
    /// </summary>
    public int IdPais { get; set; }

    /// <summary>
    /// Nombre del país
    /// </summary>
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Código ISO de 2 letras del país
    /// </summary>
    public string CodigoIso { get; set; } = null!;

    public virtual ICollection<Artistum> Artista { get; set; } = new List<Artistum>();

    public virtual ICollection<Ciudad> Ciudads { get; set; } = new List<Ciudad>();
}
