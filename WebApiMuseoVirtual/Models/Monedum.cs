using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Catálogo de monedas para precios y pagos
/// </summary>
public partial class Monedum
{
    /// <summary>
    /// Identificador único de la moneda
    /// </summary>
    public int IdMoneda { get; set; }

    /// <summary>
    /// Código ISO 4217 de la moneda (HNL, USD)
    /// </summary>
    public string Codigo { get; set; } = null!;

    /// <summary>
    /// Nombre de la moneda
    /// </summary>
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Símbolo de la moneda
    /// </summary>
    public string Simbolo { get; set; } = null!;

    public virtual ICollection<Obra> Obras { get; set; } = new List<Obra>();

    public virtual ICollection<OrdenCompra> OrdenCompras { get; set; } = new List<OrdenCompra>();

    public virtual ICollection<TipoEntradum> TipoEntrada { get; set; } = new List<TipoEntradum>();
}
