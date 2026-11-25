using WebApiMuseoVirtual.Features.Eventos.Dtos;
using WebApiMuseoVirtual.Models;

namespace WebApiMuseoVirtual.Features.Eventos.Interfaces;

public interface IEventosDomainService
{
    EventoResponse MapToResponse(Evento evento);
    Evento MapFromCreateRequest(CrearEventoRequest request);
    Entradum MapEntradaFromRequest(CrearEntradaRequest request);
    EntradaResponse MapEntradaToResponse(Entradum entrada);
}
