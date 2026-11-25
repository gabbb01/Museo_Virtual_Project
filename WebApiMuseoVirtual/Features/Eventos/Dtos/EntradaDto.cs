namespace WebApiMuseoVirtual.Features.Eventos.Dtos;

public class CrearEntradaRequest
{
    public int IdTipoEntrada { get; set; }
    public int IdUsuario { get; set; }
    public int IdExposicion { get; set; }
    public int? IdEvento { get; set; }
    public DateOnly FechaVisita { get; set; }
}

public class EntradaResponse
{
    public int IdEntrada { get; set; }
    public string CodigoQr { get; set; } = null!;
    public string TipoEntrada { get; set; } = null!;
    public string Exposicion { get; set; } = null!;
    public string? Evento { get; set; }
    public DateOnly FechaVisita { get; set; }
    public DateTime FechaCompra { get; set; }
    public bool Usada { get; set; }
}
