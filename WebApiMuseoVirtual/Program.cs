using Microsoft.EntityFrameworkCore;
using WebApiMuseoVirtual.Features.Autenticacion.AppServices;
using WebApiMuseoVirtual.Features.Autenticacion.DomainServices;
using WebApiMuseoVirtual.Features.Autenticacion.Interfaces;
using WebApiMuseoVirtual.Features.Catalogos.AppServices;
using WebApiMuseoVirtual.Features.Catalogos.Interfaces;
using WebApiMuseoVirtual.Features.Clientes.AppServices;
using WebApiMuseoVirtual.Features.Clientes.DomainServices;
using WebApiMuseoVirtual.Features.Clientes.Interfaces;
using WebApiMuseoVirtual.Features.Eventos.AppServices;
using WebApiMuseoVirtual.Features.Eventos.DomainServices;
using WebApiMuseoVirtual.Features.Eventos.Interfaces;
using WebApiMuseoVirtual.Features.Exposiciones.AppServices;
using WebApiMuseoVirtual.Features.Exposiciones.DomainServices;
using WebApiMuseoVirtual.Features.Exposiciones.Interfaces;
using WebApiMuseoVirtual.Features.Obras.AppServices;
using WebApiMuseoVirtual.Features.Obras.DomainServices;
using WebApiMuseoVirtual.Features.Obras.Interfaces;
using WebApiMuseoVirtual.Features.OrdenesCompra.AppServices;
using WebApiMuseoVirtual.Features.OrdenesCompra.DomainServices;
using WebApiMuseoVirtual.Features.OrdenesCompra.Interfaces;
using WebApiMuseoVirtual.Infrastructure.Interfaces;
using WebApiMuseoVirtual.Infrastructure.Repository;
using WebApiMuseoVirtual.Models;

var builder = WebApplication.CreateBuilder(args);

// ─── DbContext ──────────────────────────────────────────────────────────────
builder.Services.AddDbContext<MuseoVirtualContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conexion")));

// ─── CORS ───────────────────────────────────────────────────────────────────
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// ─── Repositories ───────────────────────────────────────────────────────────
builder.Services.AddScoped<IClientesRepository, ClientesRepository>();
builder.Services.AddScoped<IObrasRepository, ObrasRepository>();
builder.Services.AddScoped<IExposicionesRepository, ExposicionesRepository>();
builder.Services.AddScoped<IEventosRepository, EventosRepository>();
builder.Services.AddScoped<IOrdenesCompraRepository, OrdenesCompraRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ICatalogosRepository, CatalogosRepository>();

// ─── Domain Services ────────────────────────────────────────────────────────
builder.Services.AddScoped<IClientesDomainService, ClientesDomainService>();
builder.Services.AddScoped<IObrasDomainService, ObrasDomainService>();
builder.Services.AddScoped<IExposicionesDomainService, ExposicionesDomainService>();
builder.Services.AddScoped<IEventosDomainService, EventosDomainService>();
builder.Services.AddScoped<IOrdenesCompraDomainService, OrdenesCompraDomainService>();
builder.Services.AddScoped<IAuthDomainService, AuthDomainService>();

// ─── App Services ───────────────────────────────────────────────────────────
builder.Services.AddScoped<IClientesAppService, ClientesAppService>();
builder.Services.AddScoped<IObrasAppService, ObrasAppService>();
builder.Services.AddScoped<IExposicionesAppService, ExposicionesAppService>();
builder.Services.AddScoped<IEventosAppService, EventosAppService>();
builder.Services.AddScoped<IOrdenesCompraAppService, OrdenesCompraAppService>();
builder.Services.AddScoped<IAuthAppService, AuthAppService>();
builder.Services.AddScoped<ICatalogosAppService, CatalogosAppService>();

// ─── Controllers + OpenAPI ──────────────────────────────────────────────────
builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        opt.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddOpenApi();

var app = builder.Build();

// ─── Middleware ──────────────────────────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
