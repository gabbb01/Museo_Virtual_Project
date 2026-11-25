using WebApiMuseoVirtual.Features.Exposiciones.Dtos;
using WebApiMuseoVirtual.Models;

namespace WebApiMuseoVirtual.Features.Exposiciones.Interfaces;

public interface IExposicionesDomainService
{
    ExposicionResponse MapToResponse(VwExposicionesActiva vw);
    Exposicion MapFromCreateRequest(CrearExposicionRequest request);
    void ApplyUpdateRequest(Exposicion exposicion, EditarExposicionRequest request);
    ObraEnExposicionResponse MapObraToResponse(ExposicionObra eo);
}
