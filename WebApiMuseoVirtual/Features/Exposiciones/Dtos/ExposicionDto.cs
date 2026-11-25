namespace WebApiMuseoVirtual.Features.Exposiciones.Dtos;

public class CrearExposicionRequest
{
    public int IdSala { get; set; }
    public string Titulo { get; set; } = null!;
    public string? Descripcion { get; set; }
    public DateOnly FechaInicio { get; set; }
    public DateOnly? FechaFin { get; set; }
    public bool EsPermanente { get; set; }
}

public class EditarExposicionRequest
{
    public int IdSala { get; set; }
    public string Titulo { get; set; } = null!;
    public string? Descripcion { get; set; }
    public DateOnly FechaInicio { get; set; }
    public DateOnly? FechaFin { get; set; }
    public bool EsPermanente { get; set; }
    public string Estado { get; set; } = null!;
}

public class ExposicionResponse
{
    public int IdExposicion { get; set; }
    public string Titulo { get; set; } = null!;
    public string? Descripcion { get; set; }
    public string SalaVirtual { get; set; } = null!;
    public int CapacidadVisitantes { get; set; }
    public DateOnly FechaInicio { get; set; }
    public DateOnly? FechaFin { get; set; }
    public bool EsPermanente { get; set; }
    public int? TotalObrasExhibidas { get; set; }
}

public class AgregarObraExposicionRequest
{
    public int IdObra { get; set; }
    public int OrdenVisualizacion { get; set; } = 1;
    public string? PosicionEnSala { get; set; }
}

public class ObraEnExposicionResponse
{
    public int IdObra { get; set; }
    public string Titulo { get; set; } = null!;
    public int OrdenVisualizacion { get; set; }
    public string? PosicionEnSala { get; set; }
}
