using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Órdenes de compra de obras (encabezado)
/// </summary>
public partial class OrdenCompra
{
    /// <summary>
    /// Identificador único de la orden
    /// </summary>
    public int IdOrden { get; set; }

    /// <summary>
    /// Cliente que realiza la compra
    /// </summary>
    public int IdCliente { get; set; }

    /// <summary>
    /// Estado actual de la orden
    /// </summary>
    public int IdEstadoOrden { get; set; }

    /// <summary>
    /// Moneda de la transacción
    /// </summary>
    public int IdMoneda { get; set; }

    /// <summary>
    /// Fecha y hora de la orden
    /// </summary>
    public DateTime FechaOrden { get; set; }

    /// <summary>
    /// Suma de los detalles sin impuesto
    /// </summary>
    public decimal Subtotal { get; set; }

    /// <summary>
    /// Impuesto aplicado
    /// </summary>
    public decimal Impuesto { get; set; }

    /// <summary>
    /// Total a pagar
    /// </summary>
    public decimal Total { get; set; }

    public virtual ICollection<DetalleOrden> DetalleOrdens { get; set; } = new List<DetalleOrden>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual EstadoOrden IdEstadoOrdenNavigation { get; set; } = null!;

    public virtual Monedum IdMonedaNavigation { get; set; } = null!;

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
