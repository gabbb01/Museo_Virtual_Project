namespace WebApiMuseoVirtual.Features.Eventos.Dtos;

public class CrearEventoRequest
{
    public int IdTipoEvento { get; set; }
    public int? IdExposicion { get; set; }
    public string Titulo { get; set; } = null!;
    public string? Descripcion { get; set; }
    public DateTime FechaHoraInicio { get; set; }
    public DateTime FechaHoraFin { get; set; }
    public int? CupoMaximo { get; set; }
    public string? EnlaceTransmision { get; set; }
}

public class EventoResponse
{
    public int IdEvento { get; set; }
    public string Titulo { get; set; } = null!;
    public string? Descripcion { get; set; }
    public string TipoEvento { get; set; } = null!;
    public string? Exposicion { get; set; }
    public DateTime FechaHoraInicio { get; set; }
    public DateTime FechaHoraFin { get; set; }
    public int? CupoMaximo { get; set; }
    public string? EnlaceTransmision { get; set; }
    public string Estado { get; set; } = null!;
}
