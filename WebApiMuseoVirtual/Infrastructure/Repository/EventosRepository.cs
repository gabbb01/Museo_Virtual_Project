using Microsoft.EntityFrameworkCore;
using WebApiMuseoVirtual.Infrastructure.Interfaces;
using WebApiMuseoVirtual.Models;

namespace WebApiMuseoVirtual.Infrastructure.Repository;

public class EventosRepository : IEventosRepository
{
    private readonly MuseoVirtualContext _context;

    public EventosRepository(MuseoVirtualContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Evento>> GetAllAsync()
    {
        return await _context.Eventos
            .Include(e => e.IdTipoEventoNavigation)
            .Include(e => e.IdExposicionNavigation)
            .ToListAsync();
    }

    public async Task<Evento?> GetByIdAsync(int id)
    {
        return await _context.Eventos
            .Include(e => e.IdTipoEventoNavigation)
            .Include(e => e.IdExposicionNavigation)
            .FirstOrDefaultAsync(e => e.IdEvento == id);
    }

    public async Task<Evento> CreateAsync(Evento evento)
    {
        _context.Eventos.Add(evento);
        await _context.SaveChangesAsync();
        return evento;
    }

    public async Task<Entradum> CreateEntradaAsync(Entradum entrada)
    {
        entrada.CodigoQr = Guid.NewGuid().ToString("N");
        _context.Entrada.Add(entrada);
        await _context.SaveChangesAsync();
        return entrada;
    }

    public async Task<IEnumerable<Entradum>> GetEntradasByUsuarioAsync(int idUsuario)
    {
        return await _context.Entrada
            .Include(e => e.IdExposicionNavigation)
            .Include(e => e.IdEventoNavigation)
            .Include(e => e.IdTipoEntradaNavigation)
            .Where(e => e.IdUsuario == idUsuario)
            .ToListAsync();
    }
}
