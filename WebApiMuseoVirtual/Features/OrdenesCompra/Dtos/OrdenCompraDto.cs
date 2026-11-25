namespace WebApiMuseoVirtual.Features.OrdenesCompra.Dtos;

public class CrearOrdenRequest
{
    public int IdCliente { get; set; }
    public int IdMoneda { get; set; }
    public int IdMetodoPago { get; set; }
    public string? Referencia { get; set; }
    public List<DetalleOrdenRequest> Detalles { get; set; } = new();
}

public class DetalleOrdenRequest
{
    public int IdObra { get; set; }
    public int Cantidad { get; set; } = 1;
}

public class OrdenCompraResponse
{
    public int IdOrden { get; set; }
    public DateTime FechaOrden { get; set; }
    public string Cliente { get; set; } = null!;
    public string EstadoOrden { get; set; } = null!;
    public string Moneda { get; set; } = null!;
    public decimal Subtotal { get; set; }
    public decimal Impuesto { get; set; }
    public decimal Total { get; set; }
}

public class HistorialOrdenResponse
{
    public int IdOrden { get; set; }
    public DateTime FechaOrden { get; set; }
    public string Cliente { get; set; } = null!;
    public string? Rtn { get; set; }
    public string EstadoOrden { get; set; } = null!;
    public string Moneda { get; set; } = null!;
    public decimal Subtotal { get; set; }
    public decimal Impuesto { get; set; }
    public decimal Total { get; set; }
}
