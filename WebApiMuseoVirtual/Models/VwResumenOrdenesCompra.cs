using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

public partial class VwResumenOrdenesCompra
{
    public int IdOrden { get; set; }

    public DateTime FechaOrden { get; set; }

    public string Cliente { get; set; } = null!;

    public string? Rtn { get; set; }

    public string EstadoOrden { get; set; } = null!;

    public string Moneda { get; set; } = null!;

    public decimal Subtotal { get; set; }

    public decimal Impuesto { get; set; }

    public decimal Total { get; set; }
}
