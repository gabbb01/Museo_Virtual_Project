using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Relación N:M entre obras y artistas (obras colaborativas)
/// </summary>
public partial class ObraArtistum
{
    /// <summary>
    /// Obra
    /// </summary>
    public int IdObra { get; set; }

    /// <summary>
    /// Artista participante
    /// </summary>
    public int IdArtista { get; set; }

    /// <summary>
    /// Rol del artista en la obra (Autor, Coautor)
    /// </summary>
    public string RolArtista { get; set; } = null!;

    public virtual Artistum IdArtistaNavigation { get; set; } = null!;

    public virtual Obra IdObraNavigation { get; set; } = null!;
}
