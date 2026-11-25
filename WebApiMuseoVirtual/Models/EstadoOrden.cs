using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Catálogo de estados de una orden de compra
/// </summary>
public partial class EstadoOrden
{
    /// <summary>
    /// Identificador único del estado de orden
    /// </summary>
    public int IdEstadoOrden { get; set; }

    /// <summary>
    /// Nombre del estado (pendiente, pagada, enviada, cancelada)
    /// </summary>
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Descripción del estado
    /// </summary>
    public string? Descripcion { get; set; }

    public virtual ICollection<OrdenCompra> OrdenCompras { get; set; } = new List<OrdenCompra>();
}
