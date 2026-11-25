using WebApiMuseoVirtual.Features.Clientes.Dtos;

namespace WebApiMuseoVirtual.Features.Clientes.Interfaces;

public interface IClientesAppService
{
    Task<IEnumerable<ClienteResponse>> GetAllAsync(int? idCiudad, bool? tieneCompras);
    Task<ClienteResponse?> GetByIdAsync(int id);
    Task<ClienteResponse> CreateAsync(CrearClienteRequest request);
    Task<ClienteResponse> UpdateAsync(int id, EditarClienteRequest request);
    Task DeleteAsync(int id);
}
