using WebApiMuseoVirtual.Features.OrdenesCompra.Dtos;
using WebApiMuseoVirtual.Features.OrdenesCompra.Interfaces;
using WebApiMuseoVirtual.Infrastructure.Interfaces;
using WebApiMuseoVirtual.Models;

namespace WebApiMuseoVirtual.Features.OrdenesCompra.AppServices;

public class OrdenesCompraAppService : IOrdenesCompraAppService
{
    private readonly IOrdenesCompraRepository _repository;
    private readonly IOrdenesCompraDomainService _domainService;

    public OrdenesCompraAppService(IOrdenesCompraRepository repository, IOrdenesCompraDomainService domainService)
    {
        _repository = repository;
        _domainService = domainService;
    }

    public async Task<IEnumerable<HistorialOrdenResponse>> GetHistorialClienteAsync(int idCliente)
    {
        var historial = await _repository.GetHistorialClienteAsync(idCliente);
        return historial.Select(h => _domainService.MapHistorialToResponse(h));
    }

    public async Task<OrdenCompraResponse?> GetByIdAsync(int id)
    {
        var orden = await _repository.GetByIdAsync(id);
        return orden == null ? null : _domainService.MapToResponse(orden);
    }

    public async Task<OrdenCompraResponse> CreateAsync(CrearOrdenRequest request)
    {
        // Buscar estado "pendiente"
        var estadoPendiente = await _repository.GetEstadoOrdenByNombreAsync("pendiente")
            ?? throw new InvalidOperationException("Estado de orden 'pendiente' no encontrado en catálogo.");

        // Construir detalles y calcular totales
        var detalles = new List<DetalleOrden>();
        decimal subtotal = 0;

        foreach (var item in request.Detalles)
        {
            var obra = await _repository.GetObraForOrderAsync(item.IdObra)
                ?? throw new InvalidOperationException($"La obra {item.IdObra} no está disponible para compra.");

            var precioUnitario = obra.Precio ?? 0;
            subtotal += precioUnitario * item.Cantidad;

            detalles.Add(new DetalleOrden
            {
                IdObra = item.IdObra,
                Cantidad = item.Cantidad,
                PrecioUnitario = precioUnitario
            });
        }

        decimal impuesto = subtotal * 0.15m;
        decimal total = subtotal + impuesto;

        var orden = new OrdenCompra
        {
            IdCliente = request.IdCliente,
            IdEstadoOrden = estadoPendiente.IdEstadoOrden,
            IdMoneda = request.IdMoneda,
            FechaOrden = DateTime.Now,
            Subtotal = subtotal,
            Impuesto = impuesto,
            Total = total
        };

        var pago = new Pago
        {
            IdMetodoPago = request.IdMetodoPago,
            Monto = total,
            FechaPago = DateTime.Now,
            Referencia = request.Referencia,
            Estado = "completado"
        };

        var creada = await _repository.CreateAsync(orden, detalles, pago);
        var resultado = await _repository.GetByIdAsync(creada.IdOrden);
        return _domainService.MapToResponse(resultado!);
    }
}
