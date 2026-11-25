using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Catálogo de métodos de pago (tarjeta, transferencia, etc.)
/// </summary>
public partial class MetodoPago
{
    /// <summary>
    /// Identificador único del método de pago
    /// </summary>
    public int IdMetodoPago { get; set; }

    /// <summary>
    /// Nombre del método de pago
    /// </summary>
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Descripción del método de pago
    /// </summary>
    public string? Descripcion { get; set; }

    /// <summary>
    /// Indica si el método está habilitado (1=sí, 0=no)
    /// </summary>
    public bool Activo { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
