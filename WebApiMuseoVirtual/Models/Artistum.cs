using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Subtipo de persona: artistas autores de las obras
/// </summary>
public partial class Artistum
{
    /// <summary>
    /// PK y FK hacia persona (subtipo de persona)
    /// </summary>
    public int IdPersona { get; set; }

    /// <summary>
    /// País de nacionalidad del artista
    /// </summary>
    public int IdPais { get; set; }

    /// <summary>
    /// Nombre artístico del autor
    /// </summary>
    public string NombreArtistico { get; set; } = null!;

    /// <summary>
    /// Fecha de fallecimiento (NULL si vive)
    /// </summary>
    public DateOnly? FechaFallecimiento { get; set; }

    /// <summary>
    /// Reseña biográfica del artista
    /// </summary>
    public string? Biografia { get; set; }

    /// <summary>
    /// Sitio web oficial o portafolio
    /// </summary>
    public string? SitioWeb { get; set; }

    public virtual Pai IdPaisNavigation { get; set; } = null!;

    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual ICollection<ObraArtistum> ObraArtista { get; set; } = new List<ObraArtistum>();
}
