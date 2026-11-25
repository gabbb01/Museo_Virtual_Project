using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Obras de arte de la colección
/// </summary>
public partial class Obra
{
    /// <summary>
    /// Identificador único de la obra
    /// </summary>
    public int IdObra { get; set; }

    /// <summary>
    /// Categoría de la obra
    /// </summary>
    public int IdCategoria { get; set; }

    /// <summary>
    /// Técnica utilizada
    /// </summary>
    public int IdTecnica { get; set; }

    /// <summary>
    /// Movimiento artístico al que pertenece
    /// </summary>
    public int? IdMovimiento { get; set; }

    /// <summary>
    /// Estado de conservación actual
    /// </summary>
    public int IdEstadoConservacion { get; set; }

    /// <summary>
    /// Moneda del precio (solo si está a la venta)
    /// </summary>
    public int? IdMoneda { get; set; }

    /// <summary>
    /// Título de la obra
    /// </summary>
    public string Titulo { get; set; } = null!;

    /// <summary>
    /// Año de creación
    /// </summary>
    public short? AnioCreacion { get; set; }

    /// <summary>
    /// Descripción de la obra
    /// </summary>
    public string? Descripcion { get; set; }

    /// <summary>
    /// Alto en centímetros
    /// </summary>
    public decimal? AltoCm { get; set; }

    /// <summary>
    /// Ancho en centímetros
    /// </summary>
    public decimal? AnchoCm { get; set; }

    /// <summary>
    /// Profundidad en centímetros (esculturas)
    /// </summary>
    public decimal? ProfundidadCm { get; set; }

    /// <summary>
    /// Indica si la obra está a la venta (1=sí, 0=no)
    /// </summary>
    public bool EnVenta { get; set; }

    /// <summary>
    /// Precio de venta
    /// </summary>
    public decimal? Precio { get; set; }

    /// <summary>
    /// Fecha de ingreso a la colección
    /// </summary>
    public DateOnly FechaIngreso { get; set; }

    /// <summary>
    /// Indica si la obra está visible (1=sí, 0=no)
    /// </summary>
    public bool Activa { get; set; }

    public virtual ICollection<DetalleOrden> DetalleOrdens { get; set; } = new List<DetalleOrden>();

    public virtual ICollection<ExposicionObra> ExposicionObras { get; set; } = new List<ExposicionObra>();

    public virtual ICollection<Favorito> Favoritos { get; set; } = new List<Favorito>();

    public virtual ICollection<HistorialConservacion> HistorialConservacions { get; set; } = new List<HistorialConservacion>();

    public virtual CategoriaObra IdCategoriaNavigation { get; set; } = null!;

    public virtual EstadoConservacion IdEstadoConservacionNavigation { get; set; } = null!;

    public virtual Monedum? IdMonedaNavigation { get; set; }

    public virtual MovimientoArtistico? IdMovimientoNavigation { get; set; }

    public virtual Tecnica IdTecnicaNavigation { get; set; } = null!;

    public virtual ICollection<ObraArtistum> ObraArtista { get; set; } = new List<ObraArtistum>();

    public virtual ICollection<ObraMultimedium> ObraMultimedia { get; set; } = new List<ObraMultimedium>();

    public virtual ICollection<RecorridoParadum> RecorridoParada { get; set; } = new List<RecorridoParadum>();

    public virtual ICollection<Valoracion> Valoracions { get; set; } = new List<Valoracion>();
}
