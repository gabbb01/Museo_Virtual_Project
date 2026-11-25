using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Entradas adquiridas por los visitantes
/// </summary>
public partial class Entradum
{
    /// <summary>
    /// Identificador único de la entrada
    /// </summary>
    public int IdEntrada { get; set; }

    /// <summary>
    /// Tipo de entrada adquirida
    /// </summary>
    public int IdTipoEntrada { get; set; }

    /// <summary>
    /// Usuario que adquiere la entrada
    /// </summary>
    public int IdUsuario { get; set; }

    /// <summary>
    /// Exposición a la que da acceso
    /// </summary>
    public int IdExposicion { get; set; }

    /// <summary>
    /// Evento específico (opcional)
    /// </summary>
    public int? IdEvento { get; set; }

    /// <summary>
    /// Código único de validación de la entrada
    /// </summary>
    public string CodigoQr { get; set; } = null!;

    /// <summary>
    /// Fecha y hora de compra
    /// </summary>
    public DateTime FechaCompra { get; set; }

    /// <summary>
    /// Fecha para la que es válida
    /// </summary>
    public DateOnly FechaVisita { get; set; }

    /// <summary>
    /// Indica si ya fue utilizada
    /// </summary>
    public bool Usada { get; set; }

    public virtual Evento? IdEventoNavigation { get; set; }

    public virtual Exposicion IdExposicionNavigation { get; set; } = null!;

    public virtual TipoEntradum IdTipoEntradaNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
