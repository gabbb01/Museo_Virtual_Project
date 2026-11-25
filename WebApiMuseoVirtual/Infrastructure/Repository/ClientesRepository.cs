using Microsoft.EntityFrameworkCore;
using WebApiMuseoVirtual.Infrastructure.Interfaces;
using WebApiMuseoVirtual.Models;

namespace WebApiMuseoVirtual.Infrastructure.Repository;

public class ClientesRepository : IClientesRepository
{
    private readonly MuseoVirtualContext _context;

    public ClientesRepository(MuseoVirtualContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cliente>> GetAllAsync(int? idCiudad, bool? tieneCompras)
    {
        var query = _context.Clientes
            .Include(c => c.IdPersonaNavigation)
            .Include(c => c.OrdenCompras)
            .AsQueryable();

        if (idCiudad.HasValue)
            query = query.Where(c => c.IdPersonaNavigation.IdCiudad == idCiudad.Value);

        if (tieneCompras.HasValue)
        {
            if (tieneCompras.Value)
                query = query.Where(c => c.OrdenCompras.Any());
            else
                query = query.Where(c => !c.OrdenCompras.Any());
        }

        return await query.ToListAsync();
    }

    public async Task<Cliente?> GetByIdAsync(int id)
    {
        return await _context.Clientes
            .Include(c => c.IdPersonaNavigation)
                .ThenInclude(p => p.IdCiudadNavigation)
            .Include(c => c.OrdenCompras)
            .FirstOrDefaultAsync(c => c.IdPersona == id);
    }

    public async Task<Cliente> CreateAsync(Persona persona, Cliente cliente)
    {
        _context.Personas.Add(persona);
        await _context.SaveChangesAsync();

        cliente.IdPersona = persona.IdPersona;
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        return cliente;
    }

    public async Task<Cliente> UpdateAsync(Cliente cliente)
    {
        var persona = cliente.IdPersonaNavigation;
        if (persona != null)
            _context.Personas.Update(persona);

        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task DeleteAsync(int id)
    {
        var cliente = await _context.Clientes
            .Include(c => c.IdPersonaNavigation)
            .FirstOrDefaultAsync(c => c.IdPersona == id);

        if (cliente != null)
        {
            _context.Clientes.Remove(cliente);
            if (cliente.IdPersonaNavigation != null)
                _context.Personas.Remove(cliente.IdPersonaNavigation);
            await _context.SaveChangesAsync();
        }
    }
}
