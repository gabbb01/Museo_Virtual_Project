using WebApiMuseoVirtual.Features.Exposiciones.Dtos;
using WebApiMuseoVirtual.Features.Exposiciones.Interfaces;
using WebApiMuseoVirtual.Infrastructure.Interfaces;

namespace WebApiMuseoVirtual.Features.Exposiciones.AppServices;

public class ExposicionesAppService : IExposicionesAppService
{
    private readonly IExposicionesRepository _repository;
    private readonly IExposicionesDomainService _domainService;

    public ExposicionesAppService(IExposicionesRepository repository, IExposicionesDomainService domainService)
    {
        _repository = repository;
        _domainService = domainService;
    }

    public async Task<IEnumerable<ExposicionResponse>> GetAllAsync()
    {
        var exposiciones = await _repository.GetAllAsync();
        return exposiciones.Select(e => _domainService.MapToResponse(e));
    }

    public async Task<ExposicionResponse?> GetByIdAsync(int id)
    {
        // Reutilizamos la vista para consistencia en el response
        var todas = await _repository.GetAllAsync();
        var vw = todas.FirstOrDefault(e => e.IdExposicion == id);
        return vw == null ? null : _domainService.MapToResponse(vw);
    }

    public async Task<ExposicionResponse> CreateAsync(CrearExposicionRequest request)
    {
        var exposicion = _domainService.MapFromCreateRequest(request);
        var creada = await _repository.CreateAsync(exposicion);
        var todas = await _repository.GetAllAsync();
        var vw = todas.FirstOrDefault(e => e.IdExposicion == creada.IdExposicion);
        return _domainService.MapToResponse(vw!);
    }

    public async Task<ExposicionResponse> UpdateAsync(int id, EditarExposicionRequest request)
    {
        var exposicion = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Exposición {id} no encontrada.");
        _domainService.ApplyUpdateRequest(exposicion, request);
        await _repository.UpdateAsync(exposicion);
        var todas = await _repository.GetAllAsync();
        var vw = todas.FirstOrDefault(e => e.IdExposicion == id);
        return _domainService.MapToResponse(vw!);
    }

    public async Task AddObraAsync(int idExposicion, AgregarObraExposicionRequest request)
    {
        await _repository.AddObraAsync(idExposicion, request.IdObra, request.OrdenVisualizacion, request.PosicionEnSala);
    }

    public async Task RemoveObraAsync(int idExposicion, int idObra)
    {
        await _repository.RemoveObraAsync(idExposicion, idObra);
    }

    public async Task<IEnumerable<ObraEnExposicionResponse>> GetObrasAsync(int idExposicion)
    {
        var obras = await _repository.GetObrasAsync(idExposicion);
        return obras.Select(eo => _domainService.MapObraToResponse(eo));
    }
}
