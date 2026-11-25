using WebApiMuseoVirtual.Features.Obras.Dtos;
using WebApiMuseoVirtual.Features.Obras.Interfaces;
using WebApiMuseoVirtual.Models;

namespace WebApiMuseoVirtual.Features.Obras.DomainServices;

public class ObrasDomainService : IObrasDomainService
{
    public ObraResponse MapToResponse(Obra obra)
    {
        return new ObraResponse
        {
            IdObra = obra.IdObra,
            Titulo = obra.Titulo,
            AnioCreacion = obra.AnioCreacion,
            Descripcion = obra.Descripcion,
            Categoria = obra.IdCategoriaNavigation?.Nombre ?? string.Empty,
            Tecnica = obra.IdTecnicaNavigation?.Nombre ?? string.Empty,
            Movimiento = obra.IdMovimientoNavigation?.Nombre,
            EstadoConservacion = obra.IdEstadoConservacionNavigation?.Nombre ?? string.Empty,
            AltoCm = obra.AltoCm,
            AnchoCm = obra.AnchoCm,
            ProfundidadCm = obra.ProfundidadCm,
            EnVenta = obra.EnVenta,
            Precio = obra.Precio,
            Moneda = obra.IdMonedaNavigation?.Codigo,
            FechaIngreso = obra.FechaIngreso,
            Activa = obra.Activa
        };
    }

    public Obra MapFromCreateRequest(CrearObraRequest request)
    {
        return new Obra
        {
            IdCategoria = request.IdCategoria,
            IdTecnica = request.IdTecnica,
            IdMovimiento = request.IdMovimiento,
            IdEstadoConservacion = request.IdEstadoConservacion,
            IdMoneda = request.IdMoneda,
            Titulo = request.Titulo,
            AnioCreacion = request.AnioCreacion,
            Descripcion = request.Descripcion,
            AltoCm = request.AltoCm,
            AnchoCm = request.AnchoCm,
            ProfundidadCm = request.ProfundidadCm,
            EnVenta = request.EnVenta,
            Precio = request.Precio,
            FechaIngreso = request.FechaIngreso,
            Activa = true
        };
    }

    public void ApplyUpdateRequest(Obra obra, EditarObraRequest request)
    {
        obra.IdCategoria = request.IdCategoria;
        obra.IdTecnica = request.IdTecnica;
        obra.IdMovimiento = request.IdMovimiento;
        obra.IdEstadoConservacion = request.IdEstadoConservacion;
        obra.IdMoneda = request.IdMoneda;
        obra.Titulo = request.Titulo;
        obra.AnioCreacion = request.AnioCreacion;
        obra.Descripcion = request.Descripcion;
        obra.AltoCm = request.AltoCm;
        obra.AnchoCm = request.AnchoCm;
        obra.ProfundidadCm = request.ProfundidadCm;
        obra.EnVenta = request.EnVenta;
        obra.Precio = request.Precio;
    }

    public CatalogoObraResponse MapCatalogoToResponse(VwCatalogoObra catalogo)
    {
        return new CatalogoObraResponse
        {
            IdObra = catalogo.IdObra,
            Titulo = catalogo.Titulo,
            AnioCreacion = catalogo.AnioCreacion,
            Artista = catalogo.Artista,
            Categoria = catalogo.Categoria,
            Tecnica = catalogo.Tecnica,
            Movimiento = catalogo.Movimiento,
            EstadoConservacion = catalogo.EstadoConservacion,
            EnVenta = catalogo.EnVenta,
            Precio = catalogo.Precio,
            Moneda = catalogo.Moneda
        };
    }
}
