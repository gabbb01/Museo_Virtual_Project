using Microsoft.EntityFrameworkCore;
using WebApiMuseoVirtual.Infrastructure.Interfaces;
using WebApiMuseoVirtual.Models;

namespace WebApiMuseoVirtual.Infrastructure.Repository;

public class OrdenesCompraRepository : IOrdenesCompraRepository
{
    private readonly MuseoVirtualContext _context;

    public OrdenesCompraRepository(MuseoVirtualContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<VwResumenOrdenesCompra>> GetHistorialClienteAsync(int idCliente)
    {
        // Obtenemos los ids de órdenes del cliente y luego filtramos la vista
        var idsOrdenes = await _context.OrdenCompras
            .Where(o => o.IdCliente == idCliente)
            .Select(o => o.IdOrden)
            .ToListAsync();

        return await _context.VwResumenOrdenesCompras
            .Where(r => idsOrdenes.Contains(r.IdOrden))
            .ToListAsync();
    }

    public async Task<OrdenCompra?> GetByIdAsync(int id)
    {
        return await _context.OrdenCompras
            .Include(o => o.DetalleOrdens)
                .ThenInclude(d => d.IdObraNavigation)
            .Include(o => o.Pagos)
            .Include(o => o.IdEstadoOrdenNavigation)
            .Include(o => o.IdMonedaNavigation)
            .FirstOrDefaultAsync(o => o.IdOrden == id);
    }

    public async Task<OrdenCompra> CreateAsync(OrdenCompra orden, IEnumerable<DetalleOrden> detalles, Pago pago)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        _context.OrdenCompras.Add(orden);
        await _context.SaveChangesAsync();

        foreach (var detalle in detalles)
        {
            detalle.IdOrden = orden.IdOrden;
            _context.DetalleOrdens.Add(detalle);
        }
        await _context.SaveChangesAsync();

        pago.IdOrden = orden.IdOrden;
        pago.Monto = orden.Total;
        _context.Pagos.Add(pago);
        await _context.SaveChangesAsync();

        await transaction.CommitAsync();
        return orden;
    }

    public async Task<EstadoOrden?> GetEstadoOrdenByNombreAsync(string nombre)
    {
        return await _context.EstadoOrdens
            .FirstOrDefaultAsync(e => e.Nombre.ToLower() == nombre.ToLower());
    }

    public async Task<Obra?> GetObraForOrderAsync(int idObra)
    {
        return await _context.Obras
            .FirstOrDefaultAsync(o => o.IdObra == idObra && o.EnVenta && o.Activa);
    }
}
