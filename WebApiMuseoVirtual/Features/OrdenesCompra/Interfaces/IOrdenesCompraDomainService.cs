using WebApiMuseoVirtual.Features.OrdenesCompra.Dtos;
using WebApiMuseoVirtual.Models;

namespace WebApiMuseoVirtual.Features.OrdenesCompra.Interfaces;

public interface IOrdenesCompraDomainService
{
    OrdenCompraResponse MapToResponse(OrdenCompra orden);
    HistorialOrdenResponse MapHistorialToResponse(VwResumenOrdenesCompra vw);
}
