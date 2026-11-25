using WebApiMuseoVirtual.Models;

namespace WebApiMuseoVirtual.Infrastructure.Interfaces;

public interface IExposicionesRepository
{
    Task<IEnumerable<VwExposicionesActiva>> GetAllAsync();
    Task<Exposicion?> GetByIdAsync(int id);
    Task<Exposicion> CreateAsync(Exposicion exposicion);
    Task<Exposicion> UpdateAsync(Exposicion exposicion);
    Task AddObraAsync(int idExposicion, int idObra, int orden, string? posicion);
    Task RemoveObraAsync(int idExposicion, int idObra);
    Task<IEnumerable<ExposicionObra>> GetObrasAsync(int idExposicion);
}
