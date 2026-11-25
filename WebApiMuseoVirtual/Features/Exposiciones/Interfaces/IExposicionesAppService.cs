using WebApiMuseoVirtual.Features.Exposiciones.Dtos;

namespace WebApiMuseoVirtual.Features.Exposiciones.Interfaces;

public interface IExposicionesAppService
{
    Task<IEnumerable<ExposicionResponse>> GetAllAsync();
    Task<ExposicionResponse?> GetByIdAsync(int id);
    Task<ExposicionResponse> CreateAsync(CrearExposicionRequest request);
    Task<ExposicionResponse> UpdateAsync(int id, EditarExposicionRequest request);
    Task AddObraAsync(int idExposicion, AgregarObraExposicionRequest request);
    Task RemoveObraAsync(int idExposicion, int idObra);
    Task<IEnumerable<ObraEnExposicionResponse>> GetObrasAsync(int idExposicion);
}
