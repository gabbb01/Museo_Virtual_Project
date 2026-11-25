using WebApiMuseoVirtual.Features.OrdenesCompra.Dtos;
using WebApiMuseoVirtual.Features.OrdenesCompra.Interfaces;
using WebApiMuseoVirtual.Models;

namespace WebApiMuseoVirtual.Features.OrdenesCompra.DomainServices;

public class OrdenesCompraDomainService : IOrdenesCompraDomainService
{
    public OrdenCompraResponse MapToResponse(OrdenCompra orden)
    {
        var persona = orden.IdClienteNavigation?.IdPersonaNavigation;
        string nombreCliente = persona != null
            ? $"{persona.Pnombre} {persona.Papellido}".Trim()
            : string.Empty;

        return new OrdenCompraResponse
        {
            IdOrden = orden.IdOrden,
            FechaOrden = orden.FechaOrden,
            Cliente = nombreCliente,
            EstadoOrden = orden.IdEstadoOrdenNavigation?.Nombre ?? string.Empty,
            Moneda = orden.IdMonedaNavigation?.Codigo ?? string.Empty,
            Subtotal = orden.Subtotal,
            Impuesto = orden.Impuesto,
            Total = orden.Total
        };
    }

    public HistorialOrdenResponse MapHistorialToResponse(VwResumenOrdenesCompra vw)
    {
        return new HistorialOrdenResponse
        {
            IdOrden = vw.IdOrden,
            FechaOrden = vw.FechaOrden,
            Cliente = vw.Cliente,
            Rtn = vw.Rtn,
            EstadoOrden = vw.EstadoOrden,
            Moneda = vw.Moneda,
            Subtotal = vw.Subtotal,
            Impuesto = vw.Impuesto,
            Total = vw.Total
        };
    }
}
