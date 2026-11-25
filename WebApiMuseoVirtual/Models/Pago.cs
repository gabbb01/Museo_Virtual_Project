using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Pagos realizados por las órdenes de compra
/// </summary>
public partial class Pago
{
    /// <summary>
    /// Identificador único del pago
    /// </summary>
    public int IdPago { get; set; }

    /// <summary>
    /// Orden que se paga
    /// </summary>
    public int IdOrden { get; set; }

    /// <summary>
    /// Método de pago utilizado
    /// </summary>
    public int IdMetodoPago { get; set; }

    /// <summary>
    /// Monto pagado
    /// </summary>
    public decimal Monto { get; set; }

    /// <summary>
    /// Fecha y hora del pago
    /// </summary>
    public DateTime FechaPago { get; set; }

    /// <summary>
    /// Número de referencia o transacción
    /// </summary>
    public string? Referencia { get; set; }

    /// <summary>
    /// Estado del pago
    /// </summary>
    public string Estado { get; set; } = null!;

    public virtual MetodoPago IdMetodoPagoNavigation { get; set; } = null!;

    public virtual OrdenCompra IdOrdenNavigation { get; set; } = null!;
}
