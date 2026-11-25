using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Catálogo de ciudades por país
/// </summary>
public partial class Ciudad
{
    /// <summary>
    /// Identificador único de la ciudad
    /// </summary>
    public int IdCiudad { get; set; }

    /// <summary>
    /// País al que pertenece la ciudad
    /// </summary>
    public int IdPais { get; set; }

    /// <summary>
    /// Nombre de la ciudad
    /// </summary>
    public string Nombre { get; set; } = null!;

    public virtual Pai IdPaisNavigation { get; set; } = null!;

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
