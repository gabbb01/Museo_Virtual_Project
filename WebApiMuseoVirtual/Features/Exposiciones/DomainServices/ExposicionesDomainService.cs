using WebApiMuseoVirtual.Features.Exposiciones.Dtos;
using WebApiMuseoVirtual.Features.Exposiciones.Interfaces;
using WebApiMuseoVirtual.Models;

namespace WebApiMuseoVirtual.Features.Exposiciones.DomainServices;

public class ExposicionesDomainService : IExposicionesDomainService
{
    public ExposicionResponse MapToResponse(VwExposicionesActiva vw)
    {
        return new ExposicionResponse
        {
            IdExposicion = vw.IdExposicion,
            Titulo = vw.Exposicion,
            SalaVirtual = vw.SalaVirtual,
            CapacidadVisitantes = vw.CapacidadVisitantes,
            FechaInicio = vw.FechaInicio,
            FechaFin = vw.FechaFin,
            EsPermanente = vw.EsPermanente,
            TotalObrasExhibidas = vw.TotalObrasExhibidas
        };
    }

    public Exposicion MapFromCreateRequest(CrearExposicionRequest request)
    {
        return new Exposicion
        {
            IdSala = request.IdSala,
            Titulo = request.Titulo,
            Descripcion = request.Descripcion,
            FechaInicio = request.FechaInicio,
            FechaFin = request.FechaFin,
            EsPermanente = request.EsPermanente,
            Estado = "planificada"
        };
    }

    public void ApplyUpdateRequest(Exposicion exposicion, EditarExposicionRequest request)
    {
        exposicion.IdSala = request.IdSala;
        exposicion.Titulo = request.Titulo;
        exposicion.Descripcion = request.Descripcion;
        exposicion.FechaInicio = request.FechaInicio;
        exposicion.FechaFin = request.FechaFin;
        exposicion.EsPermanente = request.EsPermanente;
        exposicion.Estado = request.Estado;
    }

    public ObraEnExposicionResponse MapObraToResponse(ExposicionObra eo)
    {
        return new ObraEnExposicionResponse
        {
            IdObra = eo.IdObra,
            Titulo = eo.IdObraNavigation?.Titulo ?? string.Empty,
            OrdenVisualizacion = eo.OrdenVisualizacion,
            PosicionEnSala = eo.PosicionEnSala
        };
    }
}
