using WebApiMuseoVirtual.Models;

namespace WebApiMuseoVirtual.Infrastructure.Interfaces;

public interface IClientesRepository
{
    Task<IEnumerable<Cliente>> GetAllAsync(int? idCiudad, bool? tieneCompras);
    Task<Cliente?> GetByIdAsync(int id);
    Task<Cliente> CreateAsync(Persona persona, Cliente cliente);
    Task<Cliente> UpdateAsync(Cliente cliente);
    Task DeleteAsync(int id);
}
