using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Paradas (obras) de cada recorrido guiado
/// </summary>
public partial class RecorridoParadum
{
    /// <summary>
    /// Identificador único de la parada
    /// </summary>
    public int IdParada { get; set; }

    /// <summary>
    /// Recorrido al que pertenece
    /// </summary>
    public int IdRecorrido { get; set; }

    /// <summary>
    /// Obra que se visita en la parada
    /// </summary>
    public int IdObra { get; set; }

    /// <summary>
    /// Número de orden de la parada
    /// </summary>
    public int Orden { get; set; }

    /// <summary>
    /// Texto o guion de la narración de la parada
    /// </summary>
    public string? Narracion { get; set; }

    public virtual Obra IdObraNavigation { get; set; } = null!;

    public virtual RecorridoVirtual IdRecorridoNavigation { get; set; } = null!;
}
