using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Catálogo de tipos de entrada y su precio
/// </summary>
public partial class TipoEntradum
{
    /// <summary>
    /// Identificador único del tipo de entrada
    /// </summary>
    public int IdTipoEntrada { get; set; }

    /// <summary>
    /// Moneda del precio
    /// </summary>
    public int IdMoneda { get; set; }

    /// <summary>
    /// Nombre del tipo (general, estudiante, VIP)
    /// </summary>
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Descripción del beneficio
    /// </summary>
    public string? Descripcion { get; set; }

    /// <summary>
    /// Precio de la entrada (0 = gratuita)
    /// </summary>
    public decimal Precio { get; set; }

    public virtual ICollection<Entradum> Entrada { get; set; } = new List<Entradum>();

    public virtual Monedum IdMonedaNavigation { get; set; } = null!;
}
