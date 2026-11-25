using WebApiMuseoVirtual.Features.Eventos.Dtos;
using WebApiMuseoVirtual.Features.Eventos.Interfaces;
using WebApiMuseoVirtual.Models;

namespace WebApiMuseoVirtual.Features.Eventos.DomainServices;

public class EventosDomainService : IEventosDomainService
{
    public EventoResponse MapToResponse(Evento evento)
    {
        return new EventoResponse
        {
            IdEvento = evento.IdEvento,
            Titulo = evento.Titulo,
            Descripcion = evento.Descripcion,
            TipoEvento = evento.IdTipoEventoNavigation?.Nombre ?? string.Empty,
            Exposicion = evento.IdExposicionNavigation?.Titulo,
            FechaHoraInicio = evento.FechaHoraInicio,
            FechaHoraFin = evento.FechaHoraFin,
            CupoMaximo = evento.CupoMaximo,
            EnlaceTransmision = evento.EnlaceTransmision,
            Estado = evento.Estado
        };
    }

    public Evento MapFromCreateRequest(CrearEventoRequest request)
    {
        return new Evento
        {
            IdTipoEvento = request.IdTipoEvento,
            IdExposicion = request.IdExposicion,
            Titulo = request.Titulo,
            Descripcion = request.Descripcion,
            FechaHoraInicio = request.FechaHoraInicio,
            FechaHoraFin = request.FechaHoraFin,
            CupoMaximo = request.CupoMaximo,
            EnlaceTransmision = request.EnlaceTransmision,
            Estado = "programado"
        };
    }

    public Entradum MapEntradaFromRequest(CrearEntradaRequest request)
    {
        return new Entradum
        {
            IdTipoEntrada = request.IdTipoEntrada,
            IdUsuario = request.IdUsuario,
            IdExposicion = request.IdExposicion,
            IdEvento = request.IdEvento,
            FechaVisita = request.FechaVisita,
            FechaCompra = DateTime.Now,
            Usada = false,
            CodigoQr = string.Empty // será asignado por el repositorio
        };
    }

    public EntradaResponse MapEntradaToResponse(Entradum entrada)
    {
        return new EntradaResponse
        {
            IdEntrada = entrada.IdEntrada,
            CodigoQr = entrada.CodigoQr,
            TipoEntrada = entrada.IdTipoEntradaNavigation?.Nombre ?? string.Empty,
            Exposicion = entrada.IdExposicionNavigation?.Titulo ?? string.Empty,
            Evento = entrada.IdEventoNavigation?.Titulo,
            FechaVisita = entrada.FechaVisita,
            FechaCompra = entrada.FechaCompra,
            Usada = entrada.Usada
        };
    }
}
