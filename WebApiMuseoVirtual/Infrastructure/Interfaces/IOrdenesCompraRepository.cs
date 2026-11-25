using WebApiMuseoVirtual.Models;

namespace WebApiMuseoVirtual.Infrastructure.Interfaces;

public interface IOrdenesCompraRepository
{
    Task<IEnumerable<VwResumenOrdenesCompra>> GetHistorialClienteAsync(int idCliente);
    Task<OrdenCompra?> GetByIdAsync(int id);
    Task<OrdenCompra> CreateAsync(OrdenCompra orden, IEnumerable<DetalleOrden> detalles, Pago pago);
    Task<EstadoOrden?> GetEstadoOrdenByNombreAsync(string nombre);
    Task<Obra?> GetObraForOrderAsync(int idObra);
}
