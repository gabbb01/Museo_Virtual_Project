using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Catálogo de roles del sistema
/// </summary>
public partial class Rol
{
    /// <summary>
    /// Identificador único del rol
    /// </summary>
    public int IdRol { get; set; }

    /// <summary>
    /// Nombre del rol (Administrador, Curador, Visitante)
    /// </summary>
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Descripción de las responsabilidades del rol
    /// </summary>
    public string? Descripcion { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

    public virtual ICollection<Permiso> IdPermisos { get; set; } = new List<Permiso>();
}
