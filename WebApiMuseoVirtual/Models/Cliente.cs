using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Subtipo de persona: clientes compradores de obras
/// </summary>
public partial class Cliente
{
    /// <summary>
    /// PK y FK hacia persona (subtipo de persona)
    /// </summary>
    public int IdPersona { get; set; }

    /// <summary>
    /// RTN para facturación
    /// </summary>
    public string? Rtn { get; set; }

    /// <summary>
    /// Dirección de envío de las obras compradas
    /// </summary>
    public string? DireccionEnvio { get; set; }

    /// <summary>
    /// Fecha de alta como cliente
    /// </summary>
    public DateTime FechaAlta { get; set; }

    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual ICollection<OrdenCompra> OrdenCompras { get; set; } = new List<OrdenCompra>();
}
