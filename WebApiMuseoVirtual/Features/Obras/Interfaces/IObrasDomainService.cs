using WebApiMuseoVirtual.Features.Obras.Dtos;
using WebApiMuseoVirtual.Models;

namespace WebApiMuseoVirtual.Features.Obras.Interfaces;

public interface IObrasDomainService
{
    ObraResponse MapToResponse(Obra obra);
    Obra MapFromCreateRequest(CrearObraRequest request);
    void ApplyUpdateRequest(Obra obra, EditarObraRequest request);
    CatalogoObraResponse MapCatalogoToResponse(VwCatalogoObra catalogo);
}
