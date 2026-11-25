using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Detalle de obras incluidas en cada orden
/// </summary>
public partial class DetalleOrden
{
    /// <summary>
    /// Identificador único del detalle
    /// </summary>
    public int IdDetalle { get; set; }

    /// <summary>
    /// Orden a la que pertenece
    /// </summary>
    public int IdOrden { get; set; }

    /// <summary>
    /// Obra comprada
    /// </summary>
    public int IdObra { get; set; }

    /// <summary>
    /// Cantidad (1 para obras únicas)
    /// </summary>
    public int Cantidad { get; set; }

    /// <summary>
    /// Precio de la obra al momento de la compra
    /// </summary>
    public decimal PrecioUnitario { get; set; }

    public virtual Obra IdObraNavigation { get; set; } = null!;

    public virtual OrdenCompra IdOrdenNavigation { get; set; } = null!;
}
