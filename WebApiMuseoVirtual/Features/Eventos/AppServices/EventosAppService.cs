using WebApiMuseoVirtual.Features.Eventos.Dtos;
using WebApiMuseoVirtual.Features.Eventos.Interfaces;
using WebApiMuseoVirtual.Infrastructure.Interfaces;

namespace WebApiMuseoVirtual.Features.Eventos.AppServices;

public class EventosAppService : IEventosAppService
{
    private readonly IEventosRepository _repository;
    private readonly IEventosDomainService _domainService;

    public EventosAppService(IEventosRepository repository, IEventosDomainService domainService)
    {
        _repository = repository;
        _domainService = domainService;
    }

    public async Task<IEnumerable<EventoResponse>> GetAllAsync()
    {
        var eventos = await _repository.GetAllAsync();
        return eventos.Select(e => _domainService.MapToResponse(e));
    }

    public async Task<EventoResponse?> GetByIdAsync(int id)
    {
        var evento = await _repository.GetByIdAsync(id);
        return evento == null ? null : _domainService.MapToResponse(evento);
    }

    public async Task<EventoResponse> CreateAsync(CrearEventoRequest request)
    {
        var evento = _domainService.MapFromCreateRequest(request);
        var creado = await _repository.CreateAsync(evento);
        var resultado = await _repository.GetByIdAsync(creado.IdEvento);
        return _domainService.MapToResponse(resultado!);
    }

    public async Task<EntradaResponse> CrearEntradaAsync(CrearEntradaRequest request)
    {
        var entrada = _domainService.MapEntradaFromRequest(request);
        var creada = await _repository.CreateEntradaAsync(entrada);
        return _domainService.MapEntradaToResponse(creada);
    }

    public async Task<IEnumerable<EntradaResponse>> GetEntradasByUsuarioAsync(int idUsuario)
    {
        var entradas = await _repository.GetEntradasByUsuarioAsync(idUsuario);
        return entradas.Select(e => _domainService.MapEntradaToResponse(e));
    }
}
