using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Eventos virtuales del museo
/// </summary>
public partial class Evento
{
    /// <summary>
    /// Identificador único del evento
    /// </summary>
    public int IdEvento { get; set; }

    /// <summary>
    /// Tipo de evento
    /// </summary>
    public int IdTipoEvento { get; set; }

    /// <summary>
    /// Exposición asociada (opcional)
    /// </summary>
    public int? IdExposicion { get; set; }

    /// <summary>
    /// Título del evento
    /// </summary>
    public string Titulo { get; set; } = null!;

    /// <summary>
    /// Descripción del evento
    /// </summary>
    public string? Descripcion { get; set; }

    /// <summary>
    /// Fecha y hora de inicio
    /// </summary>
    public DateTime FechaHoraInicio { get; set; }

    /// <summary>
    /// Fecha y hora de finalización
    /// </summary>
    public DateTime FechaHoraFin { get; set; }

    /// <summary>
    /// Cupo máximo de asistentes
    /// </summary>
    public int? CupoMaximo { get; set; }

    /// <summary>
    /// Enlace de transmisión en línea
    /// </summary>
    public string? EnlaceTransmision { get; set; }

    /// <summary>
    /// Estado del evento
    /// </summary>
    public string Estado { get; set; } = null!;

    public virtual ICollection<Entradum> Entrada { get; set; } = new List<Entradum>();

    public virtual Exposicion? IdExposicionNavigation { get; set; }

    public virtual TipoEvento IdTipoEventoNavigation { get; set; } = null!;
}
