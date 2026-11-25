using WebApiMuseoVirtual.Features.Obras.Dtos;
using WebApiMuseoVirtual.Features.Obras.Interfaces;
using WebApiMuseoVirtual.Infrastructure.Interfaces;

namespace WebApiMuseoVirtual.Features.Obras.AppServices;

public class ObrasAppService : IObrasAppService
{
    private readonly IObrasRepository _repository;
    private readonly IObrasDomainService _domainService;

    public ObrasAppService(IObrasRepository repository, IObrasDomainService domainService)
    {
        _repository = repository;
        _domainService = domainService;
    }

    public async Task<IEnumerable<ObraResponse>> GetAllAsync(bool? activa)
    {
        var obras = await _repository.GetAllAsync(activa);
        return obras.Select(o => _domainService.MapToResponse(o));
    }

    public async Task<ObraResponse?> GetByIdAsync(int id)
    {
        var obra = await _repository.GetByIdAsync(id);
        return obra == null ? null : _domainService.MapToResponse(obra);
    }

    public async Task<ObraResponse> CreateAsync(CrearObraRequest request)
    {
        var obra = _domainService.MapFromCreateRequest(request);
        var creada = await _repository.CreateAsync(obra);
        var resultado = await _repository.GetByIdAsync(creada.IdObra);
        return _domainService.MapToResponse(resultado!);
    }

    public async Task<ObraResponse> UpdateAsync(int id, EditarObraRequest request)
    {
        var obra = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Obra {id} no encontrada.");
        _domainService.ApplyUpdateRequest(obra, request);
        var actualizada = await _repository.UpdateAsync(obra);
        var resultado = await _repository.GetByIdAsync(actualizada.IdObra);
        return _domainService.MapToResponse(resultado!);
    }

    public async Task<ObraResponse> SetActivaAsync(int id, bool activa)
    {
        var obra = await _repository.SetActivaAsync(id, activa);
        var resultado = await _repository.GetByIdAsync(obra.IdObra);
        return _domainService.MapToResponse(resultado!);
    }

    public async Task<IEnumerable<CatalogoObraResponse>> GetCatalogoAsync()
    {
        var catalogo = await _repository.GetCatalogoAsync();
        return catalogo.Select(c => _domainService.MapCatalogoToResponse(c));
    }
}
