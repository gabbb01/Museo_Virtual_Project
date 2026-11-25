using Microsoft.EntityFrameworkCore;
using WebApiMuseoVirtual.Infrastructure.Interfaces;
using WebApiMuseoVirtual.Models;

namespace WebApiMuseoVirtual.Infrastructure.Repository;

public class ExposicionesRepository : IExposicionesRepository
{
    private readonly MuseoVirtualContext _context;

    public ExposicionesRepository(MuseoVirtualContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<VwExposicionesActiva>> GetAllAsync()
    {
        return await _context.VwExposicionesActivas.ToListAsync();
    }

    public async Task<Exposicion?> GetByIdAsync(int id)
    {
        return await _context.Exposicions
            .Include(e => e.IdSalaNavigation)
            .Include(e => e.ExposicionObras)
                .ThenInclude(eo => eo.IdObraNavigation)
            .Include(e => e.IdCuradors)
            .FirstOrDefaultAsync(e => e.IdExposicion == id);
    }

    public async Task<Exposicion> CreateAsync(Exposicion exposicion)
    {
        _context.Exposicions.Add(exposicion);
        await _context.SaveChangesAsync();
        return exposicion;
    }

    public async Task<Exposicion> UpdateAsync(Exposicion exposicion)
    {
        _context.Exposicions.Update(exposicion);
        await _context.SaveChangesAsync();
        return exposicion;
    }

    public async Task AddObraAsync(int idExposicion, int idObra, int orden, string? posicion)
    {
        var exists = await _context.ExposicionObras
            .AnyAsync(eo => eo.IdExposicion == idExposicion && eo.IdObra == idObra);

        if (!exists)
        {
            var exposicionObra = new ExposicionObra
            {
                IdExposicion = idExposicion,
                IdObra = idObra,
                OrdenVisualizacion = orden,
                PosicionEnSala = posicion
            };
            _context.ExposicionObras.Add(exposicionObra);
            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoveObraAsync(int idExposicion, int idObra)
    {
        var link = await _context.ExposicionObras
            .FirstOrDefaultAsync(eo => eo.IdExposicion == idExposicion && eo.IdObra == idObra);

        if (link != null)
        {
            _context.ExposicionObras.Remove(link);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<ExposicionObra>> GetObrasAsync(int idExposicion)
    {
        return await _context.ExposicionObras
            .Include(eo => eo.IdObraNavigation)
            .Where(eo => eo.IdExposicion == idExposicion)
            .OrderBy(eo => eo.OrdenVisualizacion)
            .ToListAsync();
    }
}
