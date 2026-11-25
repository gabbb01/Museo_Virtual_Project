using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Catálogo de permisos del sistema
/// </summary>
public partial class Permiso
{
    /// <summary>
    /// Identificador único del permiso
    /// </summary>
    public int IdPermiso { get; set; }

    /// <summary>
    /// Código interno del permiso (ej. OBRA_CREAR)
    /// </summary>
    public string Codigo { get; set; } = null!;

    /// <summary>
    /// Descripción de la acción que autoriza
    /// </summary>
    public string? Descripcion { get; set; }

    public virtual ICollection<Rol> IdRols { get; set; } = new List<Rol>();
}
