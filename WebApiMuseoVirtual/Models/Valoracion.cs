using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Valoraciones y comentarios de los usuarios sobre las obras
/// </summary>
public partial class Valoracion
{
    /// <summary>
    /// Identificador único de la valoración
    /// </summary>
    public int IdValoracion { get; set; }

    /// <summary>
    /// Usuario que valora
    /// </summary>
    public int IdUsuario { get; set; }

    /// <summary>
    /// Obra valorada
    /// </summary>
    public int IdObra { get; set; }

    /// <summary>
    /// Puntuación de 1 a 5
    /// </summary>
    public byte Puntuacion { get; set; }

    /// <summary>
    /// Comentario del usuario
    /// </summary>
    public string? Comentario { get; set; }

    /// <summary>
    /// Fecha y hora de la valoración
    /// </summary>
    public DateTime Fecha { get; set; }

    public virtual Obra IdObraNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
