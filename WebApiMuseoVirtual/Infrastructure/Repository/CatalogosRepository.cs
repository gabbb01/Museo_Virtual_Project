using Microsoft.EntityFrameworkCore;
using WebApiMuseoVirtual.Infrastructure.Interfaces;
using WebApiMuseoVirtual.Models;

namespace WebApiMuseoVirtual.Infrastructure.Repository;

public class CatalogosRepository : ICatalogosRepository
{
    private readonly MuseoVirtualContext _context;

    public CatalogosRepository(MuseoVirtualContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CategoriaObra>> GetCategoriasAsync()
        => await _context.CategoriaObras.OrderBy(c => c.Nombre).ToListAsync();

    public async Task<IEnumerable<Tecnica>> GetTecnicasAsync()
        => await _context.Tecnicas.OrderBy(t => t.Nombre).ToListAsync();

    public async Task<IEnumerable<MovimientoArtistico>> GetMovimientosAsync()
        => await _context.MovimientoArtisticos.OrderBy(m => m.Nombre).ToListAsync();

    public async Task<IEnumerable<EstadoConservacion>> GetEstadosConservacionAsync()
        => await _context.EstadoConservacions.OrderBy(e => e.Nombre).ToListAsync();

    public async Task<IEnumerable<SalaVirtual>> GetSalasVirtualesAsync()
        => await _context.SalaVirtuals.Where(s => s.Activa).OrderBy(s => s.Nombre).ToListAsync();

    public async Task<IEnumerable<MetodoPago>> GetMetodosPagoAsync()
        => await _context.MetodoPagos.Where(m => m.Activo).OrderBy(m => m.Nombre).ToListAsync();

    public async Task<IEnumerable<TipoEntradum>> GetTiposEntradaAsync()
        => await _context.TipoEntrada.OrderBy(t => t.Nombre).ToListAsync();

    public async Task<IEnumerable<TipoEvento>> GetTiposEventoAsync()
        => await _context.TipoEventos.OrderBy(t => t.Nombre).ToListAsync();

    public async Task<IEnumerable<Monedum>> GetMonedasAsync()
        => await _context.Moneda.OrderBy(m => m.Codigo).ToListAsync();

    public async Task<IEnumerable<Rol>> GetRolesAsync()
        => await _context.Rols.OrderBy(r => r.Nombre).ToListAsync();
}
