using WebApiMuseoVirtual.Features.Clientes.Dtos;
using WebApiMuseoVirtual.Features.Clientes.Interfaces;
using WebApiMuseoVirtual.Infrastructure.Interfaces;

namespace WebApiMuseoVirtual.Features.Clientes.AppServices;

public class ClientesAppService : IClientesAppService
{
    private readonly IClientesRepository _repository;
    private readonly IClientesDomainService _domainService;

    public ClientesAppService(IClientesRepository repository, IClientesDomainService domainService)
    {
        _repository = repository;
        _domainService = domainService;
    }

    public async Task<IEnumerable<ClienteResponse>> GetAllAsync(int? idCiudad, bool? tieneCompras)
    {
        var clientes = await _repository.GetAllAsync(idCiudad, tieneCompras);
        return clientes.Select(c => _domainService.MapToResponse(c));
    }

    public async Task<ClienteResponse?> GetByIdAsync(int id)
    {
        var cliente = await _repository.GetByIdAsync(id);
        return cliente == null ? null : _domainService.MapToResponse(cliente);
    }

    public async Task<ClienteResponse> CreateAsync(CrearClienteRequest request)
    {
        var (persona, cliente) = _domainService.MapFromCreateRequest(request);
        var creado = await _repository.CreateAsync(persona, cliente);
        var resultado = await _repository.GetByIdAsync(creado.IdPersona);
        return _domainService.MapToResponse(resultado!);
    }

    public async Task<ClienteResponse> UpdateAsync(int id, EditarClienteRequest request)
    {
        var cliente = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Cliente {id} no encontrado.");

        _domainService.ApplyUpdateRequest(cliente, request);
        var actualizado = await _repository.UpdateAsync(cliente);
        var resultado = await _repository.GetByIdAsync(actualizado.IdPersona);
        return _domainService.MapToResponse(resultado!);
    }

    public async Task DeleteAsync(int id)
    {
        var cliente = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Cliente {id} no encontrado.");
        await _repository.DeleteAsync(id);
    }
}
