using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Subtipo de persona: cuenta de acceso a la plataforma
/// </summary>
public partial class Usuario
{
    /// <summary>
    /// PK y FK hacia persona (subtipo de persona)
    /// </summary>
    public int IdPersona { get; set; }

    /// <summary>
    /// Rol asignado al usuario
    /// </summary>
    public int IdRol { get; set; }

    /// <summary>
    /// Correo electrónico (se usa para iniciar sesión)
    /// </summary>
    public string Correo { get; set; } = null!;

    /// <summary>
    /// Contraseña cifrada (hash)
    /// </summary>
    public string ContrasenaHash { get; set; } = null!;

    /// <summary>
    /// Fecha y hora de registro en la plataforma
    /// </summary>
    public DateTime FechaRegistro { get; set; }

    /// <summary>
    /// Indica si la cuenta está activa (1=sí, 0=no)
    /// </summary>
    public bool Activo { get; set; }

    public virtual ICollection<BitacoraAcceso> BitacoraAccesos { get; set; } = new List<BitacoraAcceso>();

    public virtual ICollection<Entradum> Entrada { get; set; } = new List<Entradum>();

    public virtual ICollection<Favorito> Favoritos { get; set; } = new List<Favorito>();

    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual Rol IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Valoracion> Valoracions { get; set; } = new List<Valoracion>();
}
