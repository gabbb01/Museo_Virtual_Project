using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Imágenes, videos, audios y modelos 3D de cada obra
/// </summary>
public partial class ObraMultimedium
{
    /// <summary>
    /// Identificador único del archivo multimedia
    /// </summary>
    public int IdMultimedia { get; set; }

    /// <summary>
    /// Obra a la que pertenece el archivo
    /// </summary>
    public int IdObra { get; set; }

    /// <summary>
    /// Tipo de archivo multimedia
    /// </summary>
    public string Tipo { get; set; } = null!;

    /// <summary>
    /// Ubicación (URL) del archivo
    /// </summary>
    public string Url { get; set; } = null!;

    /// <summary>
    /// Descripción o pie de imagen
    /// </summary>
    public string? Descripcion { get; set; }

    /// <summary>
    /// Indica si es el archivo principal de la obra
    /// </summary>
    public bool EsPrincipal { get; set; }

    public virtual Obra IdObraNavigation { get; set; } = null!;
}
