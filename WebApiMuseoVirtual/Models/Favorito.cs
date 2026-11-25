using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Obras favoritas de cada usuario (N:M)
/// </summary>
public partial class Favorito
{
    /// <summary>
    /// Usuario que marca la obra
    /// </summary>
    public int IdUsuario { get; set; }

    /// <summary>
    /// Obra marcada como favorita
    /// </summary>
    public int IdObra { get; set; }

    /// <summary>
    /// Fecha y hora en que se agregó
    /// </summary>
    public DateTime FechaAgregado { get; set; }

    public virtual Obra IdObraNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
