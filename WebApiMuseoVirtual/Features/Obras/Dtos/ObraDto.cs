namespace WebApiMuseoVirtual.Features.Obras.Dtos;

public class CrearObraRequest
{
    public int IdCategoria { get; set; }
    public int IdTecnica { get; set; }
    public int? IdMovimiento { get; set; }
    public int IdEstadoConservacion { get; set; }
    public int? IdMoneda { get; set; }
    public string Titulo { get; set; } = null!;
    public short? AnioCreacion { get; set; }
    public string? Descripcion { get; set; }
    public decimal? AltoCm { get; set; }
    public decimal? AnchoCm { get; set; }
    public decimal? ProfundidadCm { get; set; }
    public bool EnVenta { get; set; }
    public decimal? Precio { get; set; }
    public DateOnly FechaIngreso { get; set; }
}

public class EditarObraRequest
{
    public int IdCategoria { get; set; }
    public int IdTecnica { get; set; }
    public int? IdMovimiento { get; set; }
    public int IdEstadoConservacion { get; set; }
    public int? IdMoneda { get; set; }
    public string Titulo { get; set; } = null!;
    public short? AnioCreacion { get; set; }
    public string? Descripcion { get; set; }
    public decimal? AltoCm { get; set; }
    public decimal? AnchoCm { get; set; }
    public decimal? ProfundidadCm { get; set; }
    public bool EnVenta { get; set; }
    public decimal? Precio { get; set; }
}

public class ObraResponse
{
    public int IdObra { get; set; }
    public string Titulo { get; set; } = null!;
    public short? AnioCreacion { get; set; }
    public string? Descripcion { get; set; }
    public string Categoria { get; set; } = null!;
    public string Tecnica { get; set; } = null!;
    public string? Movimiento { get; set; }
    public string EstadoConservacion { get; set; } = null!;
    public decimal? AltoCm { get; set; }
    public decimal? AnchoCm { get; set; }
    public decimal? ProfundidadCm { get; set; }
    public bool EnVenta { get; set; }
    public decimal? Precio { get; set; }
    public string? Moneda { get; set; }
    public DateOnly FechaIngreso { get; set; }
    public bool Activa { get; set; }
}

public class CatalogoObraResponse
{
    public int IdObra { get; set; }
    public string Titulo { get; set; } = null!;
    public short? AnioCreacion { get; set; }
    public string? Artista { get; set; }
    public string Categoria { get; set; } = null!;
    public string Tecnica { get; set; } = null!;
    public string? Movimiento { get; set; }
    public string EstadoConservacion { get; set; } = null!;
    public bool EnVenta { get; set; }
    public decimal? Precio { get; set; }
    public string? Moneda { get; set; }
}
