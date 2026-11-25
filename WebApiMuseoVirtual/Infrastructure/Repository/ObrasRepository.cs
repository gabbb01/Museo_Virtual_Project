using Microsoft.EntityFrameworkCore;
using WebApiMuseoVirtual.Infrastructure.Interfaces;
using WebApiMuseoVirtual.Models;

namespace WebApiMuseoVirtual.Infrastructure.Repository;

public class ObrasRepository : IObrasRepository
{
    private readonly MuseoVirtualContext _context;

    public ObrasRepository(MuseoVirtualContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Obra>> GetAllAsync(bool? activa)
    {
        var query = _context.Obras
            .Include(o => o.IdCategoriaNavigation)
            .Include(o => o.IdTecnicaNavigation)
            .Include(o => o.IdMovimientoNavigation)
            .Include(o => o.IdEstadoConservacionNavigation)
            .AsQueryable();

        if (activa.HasValue)
            query = query.Where(o => o.Activa == activa.Value);

        return await query.ToListAsync();
    }

    public async Task<Obra?> GetByIdAsync(int id)
    {
        return await _context.Obras
            .Include(o => o.IdCategoriaNavigation)
            .Include(o => o.IdTecnicaNavigation)
            .Include(o => o.IdMovimientoNavigation)
            .Include(o => o.IdEstadoConservacionNavigation)
            .Include(o => o.IdMonedaNavigation)
            .FirstOrDefaultAsync(o => o.IdObra == id);
    }

    public async Task<Obra> CreateAsync(Obra obra)
    {
        _context.Obras.Add(obra);
        await _context.SaveChangesAsync();
        return obra;
    }

    public async Task<Obra> UpdateAsync(Obra obra)
    {
        _context.Obras.Update(obra);
        await _context.SaveChangesAsync();
        return obra;
    }

    public async Task<Obra> SetActivaAsync(int id, bool activa)
    {
        var obra = await _context.Obras.FindAsync(id)
            ?? throw new KeyNotFoundException($"Obra {id} no encontrada.");
        obra.Activa = activa;
        await _context.SaveChangesAsync();
        return obra;
    }

    public async Task<IEnumerable<VwCatalogoObra>> GetCatalogoAsync()
    {
        return await _context.VwCatalogoObras.ToListAsync();
    }
}
