using WebApiMuseoVirtual.Features.Clientes.Dtos;
using WebApiMuseoVirtual.Models;

namespace WebApiMuseoVirtual.Features.Clientes.Interfaces;

public interface IClientesDomainService
{
    ClienteResponse MapToResponse(Cliente cliente);
    (Persona persona, Cliente cliente) MapFromCreateRequest(CrearClienteRequest request);
    void ApplyUpdateRequest(Cliente cliente, EditarClienteRequest request);
}
