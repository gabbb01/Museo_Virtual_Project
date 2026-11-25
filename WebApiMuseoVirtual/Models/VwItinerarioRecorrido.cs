using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

public partial class VwItinerarioRecorrido
{
    public int IdRecorrido { get; set; }

    public string Recorrido { get; set; } = null!;

    public string Exposicion { get; set; } = null!;

    public int NumeroParada { get; set; }

    public string ObraParada { get; set; } = null!;

    public string? Narracion { get; set; }
}
