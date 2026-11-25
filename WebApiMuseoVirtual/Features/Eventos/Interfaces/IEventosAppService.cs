using WebApiMuseoVirtual.Features.Eventos.Dtos;

namespace WebApiMuseoVirtual.Features.Eventos.Interfaces;

public interface IEventosAppService
{
    Task<IEnumerable<EventoResponse>> GetAllAsync();
    Task<EventoResponse?> GetByIdAsync(int id);
    Task<EventoResponse> CreateAsync(CrearEventoRequest request);
    Task<EntradaResponse> CrearEntradaAsync(CrearEntradaRequest request);
    Task<IEnumerable<EntradaResponse>> GetEntradasByUsuarioAsync(int idUsuario);
}
