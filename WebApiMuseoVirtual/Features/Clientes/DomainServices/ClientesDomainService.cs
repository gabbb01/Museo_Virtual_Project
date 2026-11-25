using WebApiMuseoVirtual.Features.Clientes.Dtos;
using WebApiMuseoVirtual.Features.Clientes.Interfaces;
using WebApiMuseoVirtual.Models;

namespace WebApiMuseoVirtual.Features.Clientes.DomainServices;

public class ClientesDomainService : IClientesDomainService
{
    public ClienteResponse MapToResponse(Cliente cliente)
    {
        var persona = cliente.IdPersonaNavigation;
        return new ClienteResponse
        {
            IdPersona = cliente.IdPersona,
            NombreCompleto = $"{persona.Pnombre} {persona.Snombre ?? ""} {persona.Papellido} {persona.Sapellido ?? ""}".Trim(),
            Identificacion = persona.Identificacion,
            FechaNacimiento = persona.FechaNacimiento,
            Telefono = persona.Telefono,
            Direccion = persona.Direccion,
            IdCiudad = persona.IdCiudad,
            Ciudad = persona.IdCiudadNavigation?.Nombre,
            Rtn = cliente.Rtn,
            DireccionEnvio = cliente.DireccionEnvio,
            FechaAlta = cliente.FechaAlta,
            TotalCompras = cliente.OrdenCompras.Count
        };
    }

    public (Persona persona, Cliente cliente) MapFromCreateRequest(CrearClienteRequest request)
    {
        var persona = new Persona
        {
            Pnombre = request.Pnombre,
            Snombre = request.Snombre,
            Papellido = request.Papellido,
            Sapellido = request.Sapellido,
            Identificacion = request.Identificacion,
            FechaNacimiento = request.FechaNacimiento,
            Telefono = request.Telefono,
            Direccion = request.Direccion,
            IdCiudad = request.IdCiudad,
            FechaCreacion = DateTime.Now
        };

        var cliente = new Cliente
        {
            Rtn = request.Rtn,
            DireccionEnvio = request.DireccionEnvio,
            FechaAlta = DateTime.Now
        };

        return (persona, cliente);
    }

    public void ApplyUpdateRequest(Cliente cliente, EditarClienteRequest request)
    {
        var persona = cliente.IdPersonaNavigation;
        persona.Pnombre = request.Pnombre;
        persona.Snombre = request.Snombre;
        persona.Papellido = request.Papellido;
        persona.Sapellido = request.Sapellido;
        persona.Identificacion = request.Identificacion;
        persona.FechaNacimiento = request.FechaNacimiento;
        persona.Telefono = request.Telefono;
        persona.Direccion = request.Direccion;
        persona.IdCiudad = request.IdCiudad;

        cliente.Rtn = request.Rtn;
        cliente.DireccionEnvio = request.DireccionEnvio;
    }
}
