using WebApiMuseoVirtual.Features.Obras.Dtos;

namespace WebApiMuseoVirtual.Features.Obras.Interfaces;

public interface IObrasAppService
{
    Task<IEnumerable<ObraResponse>> GetAllAsync(bool? activa);
    Task<ObraResponse?> GetByIdAsync(int id);
    Task<ObraResponse> CreateAsync(CrearObraRequest request);
    Task<ObraResponse> UpdateAsync(int id, EditarObraRequest request);
    Task<ObraResponse> SetActivaAsync(int id, bool activa);
    Task<IEnumerable<CatalogoObraResponse>> GetCatalogoAsync();
}
