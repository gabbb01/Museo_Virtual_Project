using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Registro de accesos y acciones de los usuarios
/// </summary>
public partial class BitacoraAcceso
{
    /// <summary>
    /// Identificador único del registro de bitácora
    /// </summary>
    public int IdBitacora { get; set; }

    /// <summary>
    /// Usuario que realizó la acción
    /// </summary>
    public int IdUsuario { get; set; }

    /// <summary>
    /// Fecha y hora de la acción
    /// </summary>
    public DateTime FechaHora { get; set; }

    /// <summary>
    /// Dirección IP de origen (IPv4 o IPv6)
    /// </summary>
    public string? IpOrigen { get; set; }

    /// <summary>
    /// Acción realizada (LOGIN, LOGOUT, etc.)
    /// </summary>
    public string Accion { get; set; } = null!;

    /// <summary>
    /// Indica si la acción fue exitosa (1=sí, 0=no)
    /// </summary>
    public bool Exitoso { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
