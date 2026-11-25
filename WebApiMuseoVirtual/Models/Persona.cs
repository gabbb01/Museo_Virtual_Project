using System;
using System.Collections.Generic;

namespace WebApiMuseoVirtual.Models;

/// <summary>
/// Supertipo: datos comunes de toda persona (usuarios, artistas, curadores y clientes)
/// </summary>
public partial class Persona
{
    /// <summary>
    /// Identificador único de la persona (supertipo)
    /// </summary>
    public int IdPersona { get; set; }

    /// <summary>
    /// Ciudad de residencia de la persona
    /// </summary>
    public int? IdCiudad { get; set; }

    /// <summary>
    /// Primer nombre de la persona
    /// </summary>
    public string Pnombre { get; set; } = null!;

    /// <summary>
    /// Segundo nombre de la persona (opcional)
    /// </summary>
    public string? Snombre { get; set; }

    /// <summary>
    /// Primer apellido de la persona
    /// </summary>
    public string Papellido { get; set; } = null!;

    /// <summary>
    /// Segundo apellido de la persona (opcional)
    /// </summary>
    public string? Sapellido { get; set; }

    /// <summary>
    /// Número de identidad o pasaporte
    /// </summary>
    public string? Identificacion { get; set; }

    /// <summary>
    /// Fecha de nacimiento
    /// </summary>
    public DateOnly? FechaNacimiento { get; set; }

    /// <summary>
    /// Teléfono de contacto
    /// </summary>
    public string? Telefono { get; set; }

    /// <summary>
    /// Dirección de domicilio
    /// </summary>
    public string? Direccion { get; set; }

    /// <summary>
    /// Fecha y hora de creación del registro
    /// </summary>
    public DateTime FechaCreacion { get; set; }

    public virtual Artistum? Artistum { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual Curador? Curador { get; set; }

    public virtual Ciudad? IdCiudadNavigation { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
