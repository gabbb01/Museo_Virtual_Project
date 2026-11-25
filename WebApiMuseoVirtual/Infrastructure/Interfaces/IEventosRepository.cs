using WebApiMuseoVirtual.Models;

namespace WebApiMuseoVirtual.Infrastructure.Interfaces;

public interface IEventosRepository
{
    Task<IEnumerable<Evento>> GetAllAsync();
    Task<Evento?> GetByIdAsync(int id);
    Task<Evento> CreateAsync(Evento evento);
    Task<Entradum> CreateEntradaAsync(Entradum entrada);
    Task<IEnumerable<Entradum>> GetEntradasByUsuarioAsync(int idUsuario);
}
