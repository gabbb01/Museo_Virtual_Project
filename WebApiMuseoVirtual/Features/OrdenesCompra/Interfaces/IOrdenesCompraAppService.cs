using WebApiMuseoVirtual.Features.OrdenesCompra.Dtos;

namespace WebApiMuseoVirtual.Features.OrdenesCompra.Interfaces;

public interface IOrdenesCompraAppService
{
    Task<IEnumerable<HistorialOrdenResponse>> GetHistorialClienteAsync(int idCliente);
    Task<OrdenCompraResponse?> GetByIdAsync(int id);
    Task<OrdenCompraResponse> CreateAsync(CrearOrdenRequest request);
}
