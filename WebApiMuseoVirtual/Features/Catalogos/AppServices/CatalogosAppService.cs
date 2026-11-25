using WebApiMuseoVirtual.Features.Catalogos.Interfaces;
using WebApiMuseoVirtual.Infrastructure.Interfaces;
using WebApiMuseoVirtual.Models;

namespace WebApiMuseoVirtual.Features.Catalogos.AppServices;

public class CatalogosAppService : ICatalogosAppService
{
    private readonly ICatalogosRepository _repository;

    public CatalogosAppService(ICatalogosRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CategoriaObra>> GetCategoriasAsync()
        => await _repository.GetCategoriasAsync();

    public async Task<IEnumerable<Tecnica>> GetTecnicasAsync()
        => await _repository.GetTecnicasAsync();

    public async Task<IEnumerable<MovimientoArtistico>> GetMovimientosAsync()
        => await _repository.GetMovimientosAsync();

    public async Task<IEnumerable<EstadoConservacion>> GetEstadosConservacionAsync()
        => await _repository.GetEstadosConservacionAsync();

    public async Task<IEnumerable<SalaVirtual>> GetSalasVirtualesAsync()
        => await _repository.GetSalasVirtualesAsync();

    public async Task<IEnumerable<MetodoPago>> GetMetodosPagoAsync()
        => await _repository.GetMetodosPagoAsync();

    public async Task<IEnumerable<TipoEntradum>> GetTiposEntradaAsync()
        => await _repository.GetTiposEntradaAsync();

    public async Task<IEnumerable<TipoEvento>> GetTiposEventoAsync()
        => await _repository.GetTiposEventoAsync();

    public async Task<IEnumerable<Monedum>> GetMonedasAsync()
        => await _repository.GetMonedasAsync();

    public async Task<IEnumerable<Rol>> GetRolesAsync()
        => await _repository.GetRolesAsync();
}
