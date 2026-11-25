using WebApiMuseoVirtual.Models;

namespace WebApiMuseoVirtual.Infrastructure.Interfaces;

public interface ICatalogosRepository
{
    Task<IEnumerable<CategoriaObra>> GetCategoriasAsync();
    Task<IEnumerable<Tecnica>> GetTecnicasAsync();
    Task<IEnumerable<MovimientoArtistico>> GetMovimientosAsync();
    Task<IEnumerable<EstadoConservacion>> GetEstadosConservacionAsync();
    Task<IEnumerable<SalaVirtual>> GetSalasVirtualesAsync();
    Task<IEnumerable<MetodoPago>> GetMetodosPagoAsync();
    Task<IEnumerable<TipoEntradum>> GetTiposEntradaAsync();
    Task<IEnumerable<TipoEvento>> GetTiposEventoAsync();
    Task<IEnumerable<Monedum>> GetMonedasAsync();
    Task<IEnumerable<Rol>> GetRolesAsync();
}
