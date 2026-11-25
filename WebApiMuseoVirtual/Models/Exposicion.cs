using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Exposiciones temporales y permanentes
/// </summary>
public partial class Exposicion
{
    /// <summary>
    /// Identificador único de la exposición
    /// </summary>
    public int IdExposicion { get; set; }

    /// <summary>
    /// Sala virtual donde se presenta
    /// </summary>
    public int IdSala { get; set; }

    /// <summary>
    /// Título de la exposición
    /// </summary>
    public string Titulo { get; set; } = null!;

    /// <summary>
    /// Descripción o concepto de la exposición
    /// </summary>
    public string? Descripcion { get; set; }

    /// <summary>
    /// Fecha de apertura
    /// </summary>
    public DateOnly FechaInicio { get; set; }

    /// <summary>
    /// Fecha de cierre (NULL si es permanente)
    /// </summary>
    public DateOnly? FechaFin { get; set; }

    /// <summary>
    /// Indica si la exposición es permanente
    /// </summary>
    public bool EsPermanente { get; set; }

    /// <summary>
    /// Estado actual de la exposición
    /// </summary>
    public string Estado { get; set; } = null!;

    public virtual ICollection<Entradum> Entrada { get; set; } = new List<Entradum>();

    public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();

    public virtual ICollection<ExposicionObra> ExposicionObras { get; set; } = new List<ExposicionObra>();

    public virtual SalaVirtual IdSalaNavigation { get; set; } = null!;

    public virtual ICollection<RecorridoVirtual> RecorridoVirtuals { get; set; } = new List<RecorridoVirtual>();

    public virtual ICollection<Curador> IdCuradors { get; set; } = new List<Curador>();
}
