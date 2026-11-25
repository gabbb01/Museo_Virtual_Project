using WebApiMuseoVirtual.Models;

namespace WebApiMuseoVirtual.Infrastructure.Interfaces;

public interface IObrasRepository
{
    Task<IEnumerable<Obra>> GetAllAsync(bool? activa);
    Task<Obra?> GetByIdAsync(int id);
    Task<Obra> CreateAsync(Obra obra);
    Task<Obra> UpdateAsync(Obra obra);
    Task<Obra> SetActivaAsync(int id, bool activa);
    Task<IEnumerable<VwCatalogoObra>> GetCatalogoAsync();
}
