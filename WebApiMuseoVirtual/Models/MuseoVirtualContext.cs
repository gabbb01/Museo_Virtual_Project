using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApiMuseoVirtual.Models;

public partial class MuseoVirtualContext : DbContext
{
    public MuseoVirtualContext()
    {
    }

    public MuseoVirtualContext(DbContextOptions<MuseoVirtualContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Artistum> Artista { get; set; }

    public virtual DbSet<BitacoraAcceso> BitacoraAccesos { get; set; }

    public virtual DbSet<CategoriaObra> CategoriaObras { get; set; }

    public virtual DbSet<Ciudad> Ciudads { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Curador> Curadors { get; set; }

    public virtual DbSet<DetalleOrden> DetalleOrdens { get; set; }

    public virtual DbSet<Entradum> Entrada { get; set; }

    public virtual DbSet<EstadoConservacion> EstadoConservacions { get; set; }

    public virtual DbSet<EstadoOrden> EstadoOrdens { get; set; }

    public virtual DbSet<Evento> Eventos { get; set; }

    public virtual DbSet<Exposicion> Exposicions { get; set; }

    public virtual DbSet<ExposicionObra> ExposicionObras { get; set; }

    public virtual DbSet<Favorito> Favoritos { get; set; }

    public virtual DbSet<HistorialConservacion> HistorialConservacions { get; set; }

    public virtual DbSet<MetodoPago> MetodoPagos { get; set; }

    public virtual DbSet<Monedum> Moneda { get; set; }

    public virtual DbSet<MovimientoArtistico> MovimientoArtisticos { get; set; }

    public virtual DbSet<Obra> Obras { get; set; }

    public virtual DbSet<ObraArtistum> ObraArtista { get; set; }

    public virtual DbSet<ObraMultimedium> ObraMultimedia { get; set; }

    public virtual DbSet<OrdenCompra> OrdenCompras { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Pai> Pais { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<RecorridoParadum> RecorridoParada { get; set; }

    public virtual DbSet<RecorridoVirtual> RecorridoVirtuals { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<SalaVirtual> SalaVirtuals { get; set; }

    public virtual DbSet<Tecnica> Tecnicas { get; set; }

    public virtual DbSet<TipoEntradum> TipoEntrada { get; set; }

    public virtual DbSet<TipoEvento> TipoEventos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Valoracion> Valoracions { get; set; }

    public virtual DbSet<VwAuditoriaAcceso> VwAuditoriaAccesos { get; set; }

    public virtual DbSet<VwCatalogoObra> VwCatalogoObras { get; set; }

    public virtual DbSet<VwEntradasPorExposicion> VwEntradasPorExposicions { get; set; }

    public virtual DbSet<VwExposicionesActiva> VwExposicionesActivas { get; set; }

    public virtual DbSet<VwHistorialConservacionObra> VwHistorialConservacionObras { get; set; }

    public virtual DbSet<VwItinerarioRecorrido> VwItinerarioRecorridos { get; set; }

    public virtual DbSet<VwObrasEnVentum> VwObrasEnVenta { get; set; }

    public virtual DbSet<VwRankingValoracionesObra> VwRankingValoracionesObras { get; set; }

    public virtual DbSet<VwResumenOrdenesCompra> VwResumenOrdenesCompras { get; set; }

    public virtual DbSet<VwUsuariosRole> VwUsuariosRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artistum>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("pk_artista");

            entity.ToTable("artista", tb => tb.HasComment("Subtipo de persona: artistas autores de las obras"));

            entity.Property(e => e.IdPersona)
                .ValueGeneratedNever()
                .HasComment("PK y FK hacia persona (subtipo de persona)")
                .HasColumnName("id_persona");
            entity.Property(e => e.Biografia)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasComment("Reseña biográfica del artista")
                .HasColumnName("biografia");
            entity.Property(e => e.FechaFallecimiento)
                .HasComment("Fecha de fallecimiento (NULL si vive)")
                .HasColumnName("fecha_fallecimiento");
            entity.Property(e => e.IdPais)
                .HasComment("País de nacionalidad del artista")
                .HasColumnName("id_pais");
            entity.Property(e => e.NombreArtistico)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasComment("Nombre artístico del autor")
                .HasColumnName("nombre_artistico");
            entity.Property(e => e.SitioWeb)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("Sitio web oficial o portafolio")
                .HasColumnName("sitio_web");

            entity.HasOne(d => d.IdPaisNavigation).WithMany(p => p.Artista)
                .HasForeignKey(d => d.IdPais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_artista_pais");

            entity.HasOne(d => d.IdPersonaNavigation).WithOne(p => p.Artistum)
                .HasForeignKey<Artistum>(d => d.IdPersona)
                .HasConstraintName("fk_artista_persona");
        });

        modelBuilder.Entity<BitacoraAcceso>(entity =>
        {
            entity.HasKey(e => e.IdBitacora).HasName("pk_bitacora_acceso");

            entity.ToTable("bitacora_acceso", tb => tb.HasComment("Registro de accesos y acciones de los usuarios"));

            entity.Property(e => e.IdBitacora)
                .HasComment("Identificador único del registro de bitácora")
                .HasColumnName("id_bitacora");
            entity.Property(e => e.Accion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Acción realizada (LOGIN, LOGOUT, etc.)")
                .HasColumnName("accion");
            entity.Property(e => e.Exitoso)
                .HasComment("Indica si la acción fue exitosa (1=sí, 0=no)")
                .HasDefaultValue(true, "df_bitacora_exitoso")
                .HasColumnName("exitoso");
            entity.Property(e => e.FechaHora)
                .HasPrecision(0)
                .HasComment("Fecha y hora de la acción")
                .HasDefaultValueSql("(sysdatetime())", "df_bitacora_fecha_hora")
                .HasColumnName("fecha_hora");
            entity.Property(e => e.IdUsuario)
                .HasComment("Usuario que realizó la acción")
                .HasColumnName("id_usuario");
            entity.Property(e => e.IpOrigen)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasComment("Dirección IP de origen (IPv4 o IPv6)")
                .HasColumnName("ip_origen");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.BitacoraAccesos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_bitacora_usuario");
        });

        modelBuilder.Entity<CategoriaObra>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("pk_categoria_obra");

            entity.ToTable("categoria_obra", tb => tb.HasComment("Catálogo de categorías de obras"));

            entity.HasIndex(e => e.Nombre, "uq_categoria_nombre").IsUnique();

            entity.Property(e => e.IdCategoria)
                .HasComment("Identificador único de la categoría")
                .HasColumnName("id_categoria");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("Descripción de la categoría")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasComment("Nombre de la categoría (pintura, escultura, fotografía)")
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Ciudad>(entity =>
        {
            entity.HasKey(e => e.IdCiudad).HasName("pk_ciudad");

            entity.ToTable("ciudad", tb => tb.HasComment("Catálogo de ciudades por país"));

            entity.HasIndex(e => new { e.IdPais, e.Nombre }, "uq_ciudad_pais_nombre").IsUnique();

            entity.Property(e => e.IdCiudad)
                .HasComment("Identificador único de la ciudad")
                .HasColumnName("id_ciudad");
            entity.Property(e => e.IdPais)
                .HasComment("País al que pertenece la ciudad")
                .HasColumnName("id_pais");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("Nombre de la ciudad")
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdPaisNavigation).WithMany(p => p.Ciudads)
                .HasForeignKey(d => d.IdPais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ciudad_pais");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("pk_cliente");

            entity.ToTable("cliente", tb => tb.HasComment("Subtipo de persona: clientes compradores de obras"));

            entity.HasIndex(e => e.Rtn, "uq_cliente_rtn")
                .IsUnique()
                .HasFilter("([rtn] IS NOT NULL)");

            entity.Property(e => e.IdPersona)
                .ValueGeneratedNever()
                .HasComment("PK y FK hacia persona (subtipo de persona)")
                .HasColumnName("id_persona");
            entity.Property(e => e.DireccionEnvio)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("Dirección de envío de las obras compradas")
                .HasColumnName("direccion_envio");
            entity.Property(e => e.FechaAlta)
                .HasPrecision(0)
                .HasComment("Fecha de alta como cliente")
                .HasDefaultValueSql("(sysdatetime())", "df_cliente_fecha_alta")
                .HasColumnName("fecha_alta");
            entity.Property(e => e.Rtn)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("RTN para facturación")
                .HasColumnName("rtn");

            entity.HasOne(d => d.IdPersonaNavigation).WithOne(p => p.Cliente)
                .HasForeignKey<Cliente>(d => d.IdPersona)
                .HasConstraintName("fk_cliente_persona");
        });

        modelBuilder.Entity<Curador>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("pk_curador");

            entity.ToTable("curador", tb => tb.HasComment("Subtipo de persona: curadores responsables de las exposiciones"));

            entity.Property(e => e.IdPersona)
                .ValueGeneratedNever()
                .HasComment("PK y FK hacia persona (subtipo de persona)")
                .HasColumnName("id_persona");
            entity.Property(e => e.Biografia)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasComment("Reseña profesional del curador")
                .HasColumnName("biografia");
            entity.Property(e => e.Especialidad)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("Área de especialidad del curador")
                .HasColumnName("especialidad");

            entity.HasOne(d => d.IdPersonaNavigation).WithOne(p => p.Curador)
                .HasForeignKey<Curador>(d => d.IdPersona)
                .HasConstraintName("fk_curador_persona");
        });

        modelBuilder.Entity<DetalleOrden>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("pk_detalle_orden");

            entity.ToTable("detalle_orden", tb => tb.HasComment("Detalle de obras incluidas en cada orden"));

            entity.HasIndex(e => new { e.IdOrden, e.IdObra }, "uq_detalle_orden_obra").IsUnique();

            entity.Property(e => e.IdDetalle)
                .HasComment("Identificador único del detalle")
                .HasColumnName("id_detalle");
            entity.Property(e => e.Cantidad)
                .HasComment("Cantidad (1 para obras únicas)")
                .HasDefaultValue(1, "df_detalle_orden_cantidad")
                .HasColumnName("cantidad");
            entity.Property(e => e.IdObra)
                .HasComment("Obra comprada")
                .HasColumnName("id_obra");
            entity.Property(e => e.IdOrden)
                .HasComment("Orden a la que pertenece")
                .HasColumnName("id_orden");
            entity.Property(e => e.PrecioUnitario)
                .HasComment("Precio de la obra al momento de la compra")
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("precio_unitario");

            entity.HasOne(d => d.IdObraNavigation).WithMany(p => p.DetalleOrdens)
                .HasForeignKey(d => d.IdObra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_detalle_obra");

            entity.HasOne(d => d.IdOrdenNavigation).WithMany(p => p.DetalleOrdens)
                .HasForeignKey(d => d.IdOrden)
                .HasConstraintName("fk_detalle_orden");
        });

        modelBuilder.Entity<Entradum>(entity =>
        {
            entity.HasKey(e => e.IdEntrada).HasName("pk_entrada");

            entity.ToTable("entrada", tb => tb.HasComment("Entradas adquiridas por los visitantes"));

            entity.HasIndex(e => e.CodigoQr, "uq_entrada_qr").IsUnique();

            entity.Property(e => e.IdEntrada)
                .HasComment("Identificador único de la entrada")
                .HasColumnName("id_entrada");
            entity.Property(e => e.CodigoQr)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasComment("Código único de validación de la entrada")
                .HasColumnName("codigo_qr");
            entity.Property(e => e.FechaCompra)
                .HasPrecision(0)
                .HasComment("Fecha y hora de compra")
                .HasDefaultValueSql("(sysdatetime())", "df_entrada_fecha_compra")
                .HasColumnName("fecha_compra");
            entity.Property(e => e.FechaVisita)
                .HasComment("Fecha para la que es válida")
                .HasColumnName("fecha_visita");
            entity.Property(e => e.IdEvento)
                .HasComment("Evento específico (opcional)")
                .HasColumnName("id_evento");
            entity.Property(e => e.IdExposicion)
                .HasComment("Exposición a la que da acceso")
                .HasColumnName("id_exposicion");
            entity.Property(e => e.IdTipoEntrada)
                .HasComment("Tipo de entrada adquirida")
                .HasColumnName("id_tipo_entrada");
            entity.Property(e => e.IdUsuario)
                .HasComment("Usuario que adquiere la entrada")
                .HasColumnName("id_usuario");
            entity.Property(e => e.Usada)
                .HasComment("Indica si ya fue utilizada")
                .HasColumnName("usada");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Entrada)
                .HasForeignKey(d => d.IdEvento)
                .HasConstraintName("fk_entrada_evento");

            entity.HasOne(d => d.IdExposicionNavigation).WithMany(p => p.Entrada)
                .HasForeignKey(d => d.IdExposicion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_entrada_expo");

            entity.HasOne(d => d.IdTipoEntradaNavigation).WithMany(p => p.Entrada)
                .HasForeignKey(d => d.IdTipoEntrada)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_entrada_tipo");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Entrada)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_entrada_usuario");
        });

        modelBuilder.Entity<EstadoConservacion>(entity =>
        {
            entity.HasKey(e => e.IdEstadoConservacion).HasName("pk_estado_conservacion");

            entity.ToTable("estado_conservacion", tb => tb.HasComment("Catálogo de estados de conservación de las obras"));

            entity.HasIndex(e => e.Nombre, "uq_estcons_nombre").IsUnique();

            entity.Property(e => e.IdEstadoConservacion)
                .HasComment("Identificador único del estado de conservación")
                .HasColumnName("id_estado_conservacion");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("Descripción del estado")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Nombre del estado (excelente, bueno, en restauración)")
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<EstadoOrden>(entity =>
        {
            entity.HasKey(e => e.IdEstadoOrden).HasName("pk_estado_orden");

            entity.ToTable("estado_orden", tb => tb.HasComment("Catálogo de estados de una orden de compra"));

            entity.HasIndex(e => e.Nombre, "uq_estorden_nombre").IsUnique();

            entity.Property(e => e.IdEstadoOrden)
                .HasComment("Identificador único del estado de orden")
                .HasColumnName("id_estado_orden");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasComment("Descripción del estado")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasComment("Nombre del estado (pendiente, pagada, enviada, cancelada)")
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.IdEvento).HasName("pk_evento");

            entity.ToTable("evento", tb => tb.HasComment("Eventos virtuales del museo"));

            entity.Property(e => e.IdEvento)
                .HasComment("Identificador único del evento")
                .HasColumnName("id_evento");
            entity.Property(e => e.CupoMaximo)
                .HasComment("Cupo máximo de asistentes")
                .HasColumnName("cupo_maximo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasComment("Descripción del evento")
                .HasColumnName("descripcion");
            entity.Property(e => e.EnlaceTransmision)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("Enlace de transmisión en línea")
                .HasColumnName("enlace_transmision");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("Estado del evento")
                .HasDefaultValue("programado", "df_evento_estado")
                .HasColumnName("estado");
            entity.Property(e => e.FechaHoraFin)
                .HasPrecision(0)
                .HasComment("Fecha y hora de finalización")
                .HasColumnName("fecha_hora_fin");
            entity.Property(e => e.FechaHoraInicio)
                .HasPrecision(0)
                .HasComment("Fecha y hora de inicio")
                .HasColumnName("fecha_hora_inicio");
            entity.Property(e => e.IdExposicion)
                .HasComment("Exposición asociada (opcional)")
                .HasColumnName("id_exposicion");
            entity.Property(e => e.IdTipoEvento)
                .HasComment("Tipo de evento")
                .HasColumnName("id_tipo_evento");
            entity.Property(e => e.Titulo)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasComment("Título del evento")
                .HasColumnName("titulo");

            entity.HasOne(d => d.IdExposicionNavigation).WithMany(p => p.Eventos)
                .HasForeignKey(d => d.IdExposicion)
                .HasConstraintName("fk_evento_expo");

            entity.HasOne(d => d.IdTipoEventoNavigation).WithMany(p => p.Eventos)
                .HasForeignKey(d => d.IdTipoEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_evento_tipo");
        });

        modelBuilder.Entity<Exposicion>(entity =>
        {
            entity.HasKey(e => e.IdExposicion).HasName("pk_exposicion");

            entity.ToTable("exposicion", tb => tb.HasComment("Exposiciones temporales y permanentes"));

            entity.Property(e => e.IdExposicion)
                .HasComment("Identificador único de la exposición")
                .HasColumnName("id_exposicion");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasComment("Descripción o concepto de la exposición")
                .HasColumnName("descripcion");
            entity.Property(e => e.EsPermanente)
                .HasComment("Indica si la exposición es permanente")
                .HasColumnName("es_permanente");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("Estado actual de la exposición")
                .HasDefaultValue("planificada", "df_exposicion_estado")
                .HasColumnName("estado");
            entity.Property(e => e.FechaFin)
                .HasComment("Fecha de cierre (NULL si es permanente)")
                .HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio)
                .HasComment("Fecha de apertura")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.IdSala)
                .HasComment("Sala virtual donde se presenta")
                .HasColumnName("id_sala");
            entity.Property(e => e.Titulo)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasComment("Título de la exposición")
                .HasColumnName("titulo");

            entity.HasOne(d => d.IdSalaNavigation).WithMany(p => p.Exposicions)
                .HasForeignKey(d => d.IdSala)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_expo_sala");

            entity.HasMany(d => d.IdCuradors).WithMany(p => p.IdExposicions)
                .UsingEntity<Dictionary<string, object>>(
                    "ExposicionCurador",
                    r => r.HasOne<Curador>().WithMany()
                        .HasForeignKey("IdCurador")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_expcur_curador"),
                    l => l.HasOne<Exposicion>().WithMany()
                        .HasForeignKey("IdExposicion")
                        .HasConstraintName("fk_expcur_expo"),
                    j =>
                    {
                        j.HasKey("IdExposicion", "IdCurador").HasName("pk_exposicion_curador");
                        j.ToTable("exposicion_curador", tb => tb.HasComment("Relación N:M entre exposiciones y curadores"));
                        j.IndexerProperty<int>("IdExposicion")
                            .HasComment("Exposición")
                            .HasColumnName("id_exposicion");
                        j.IndexerProperty<int>("IdCurador")
                            .HasComment("Curador asignado")
                            .HasColumnName("id_curador");
                    });
        });

        modelBuilder.Entity<ExposicionObra>(entity =>
        {
            entity.HasKey(e => new { e.IdExposicion, e.IdObra }).HasName("pk_exposicion_obra");

            entity.ToTable("exposicion_obra", tb => tb.HasComment("Relación N:M entre exposiciones y obras exhibidas"));

            entity.Property(e => e.IdExposicion)
                .HasComment("Exposición")
                .HasColumnName("id_exposicion");
            entity.Property(e => e.IdObra)
                .HasComment("Obra exhibida")
                .HasColumnName("id_obra");
            entity.Property(e => e.OrdenVisualizacion)
                .HasComment("Orden en que se muestra la obra")
                .HasDefaultValue(1, "df_exposicion_obra_orden")
                .HasColumnName("orden_visualizacion");
            entity.Property(e => e.PosicionEnSala)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Ubicación de la obra dentro de la sala (ej. Pared norte)")
                .HasColumnName("posicion_en_sala");

            entity.HasOne(d => d.IdExposicionNavigation).WithMany(p => p.ExposicionObras)
                .HasForeignKey(d => d.IdExposicion)
                .HasConstraintName("fk_expobra_expo");

            entity.HasOne(d => d.IdObraNavigation).WithMany(p => p.ExposicionObras)
                .HasForeignKey(d => d.IdObra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_expobra_obra");
        });

        modelBuilder.Entity<Favorito>(entity =>
        {
            entity.HasKey(e => new { e.IdUsuario, e.IdObra }).HasName("pk_favorito");

            entity.ToTable("favorito", tb => tb.HasComment("Obras favoritas de cada usuario (N:M)"));

            entity.Property(e => e.IdUsuario)
                .HasComment("Usuario que marca la obra")
                .HasColumnName("id_usuario");
            entity.Property(e => e.IdObra)
                .HasComment("Obra marcada como favorita")
                .HasColumnName("id_obra");
            entity.Property(e => e.FechaAgregado)
                .HasPrecision(0)
                .HasComment("Fecha y hora en que se agregó")
                .HasDefaultValueSql("(sysdatetime())", "df_favorito_fecha")
                .HasColumnName("fecha_agregado");

            entity.HasOne(d => d.IdObraNavigation).WithMany(p => p.Favoritos)
                .HasForeignKey(d => d.IdObra)
                .HasConstraintName("fk_fav_obra");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Favoritos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("fk_fav_usuario");
        });

        modelBuilder.Entity<HistorialConservacion>(entity =>
        {
            entity.HasKey(e => e.IdHistorial).HasName("pk_historial_conservacion");

            entity.ToTable("historial_conservacion", tb => tb.HasComment("Historial de revisiones y restauraciones de cada obra"));

            entity.Property(e => e.IdHistorial)
                .HasComment("Identificador único del registro de conservación")
                .HasColumnName("id_historial");
            entity.Property(e => e.Fecha)
                .HasComment("Fecha de la revisión o restauración")
                .HasColumnName("fecha");
            entity.Property(e => e.IdEstadoConservacion)
                .HasComment("Estado determinado en la revisión")
                .HasColumnName("id_estado_conservacion");
            entity.Property(e => e.IdObra)
                .HasComment("Obra evaluada")
                .HasColumnName("id_obra");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasComment("Observaciones o trabajos realizados")
                .HasColumnName("observaciones");
            entity.Property(e => e.Responsable)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("Persona que realizó la revisión")
                .HasColumnName("responsable");

            entity.HasOne(d => d.IdEstadoConservacionNavigation).WithMany(p => p.HistorialConservacions)
                .HasForeignKey(d => d.IdEstadoConservacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_histcons_estado");

            entity.HasOne(d => d.IdObraNavigation).WithMany(p => p.HistorialConservacions)
                .HasForeignKey(d => d.IdObra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_histcons_obra");
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodoPago).HasName("pk_metodo_pago");

            entity.ToTable("metodo_pago", tb => tb.HasComment("Catálogo de métodos de pago (tarjeta, transferencia, etc.)"));

            entity.HasIndex(e => e.Nombre, "uq_metodo_pago_nombre").IsUnique();

            entity.Property(e => e.IdMetodoPago)
                .HasComment("Identificador único del método de pago")
                .HasColumnName("id_metodo_pago");
            entity.Property(e => e.Activo)
                .HasComment("Indica si el método está habilitado (1=sí, 0=no)")
                .HasDefaultValue(true, "df_metodo_pago_activo")
                .HasColumnName("activo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasComment("Descripción del método de pago")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Nombre del método de pago")
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Monedum>(entity =>
        {
            entity.HasKey(e => e.IdMoneda).HasName("pk_moneda");

            entity.ToTable("moneda", tb => tb.HasComment("Catálogo de monedas para precios y pagos"));

            entity.HasIndex(e => e.Codigo, "uq_moneda_codigo").IsUnique();

            entity.Property(e => e.IdMoneda)
                .HasComment("Identificador único de la moneda")
                .HasColumnName("id_moneda");
            entity.Property(e => e.Codigo)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("Código ISO 4217 de la moneda (HNL, USD)")
                .HasColumnName("codigo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Nombre de la moneda")
                .HasColumnName("nombre");
            entity.Property(e => e.Simbolo)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasComment("Símbolo de la moneda")
                .HasColumnName("simbolo");
        });

        modelBuilder.Entity<MovimientoArtistico>(entity =>
        {
            entity.HasKey(e => e.IdMovimiento).HasName("pk_movimiento_artistico");

            entity.ToTable("movimiento_artistico", tb => tb.HasComment("Catálogo de movimientos artísticos"));

            entity.HasIndex(e => e.Nombre, "uq_movimiento_nombre").IsUnique();

            entity.Property(e => e.IdMovimiento)
                .HasComment("Identificador único del movimiento artístico")
                .HasColumnName("id_movimiento");
            entity.Property(e => e.AnioFin)
                .HasComment("Año aproximado de fin")
                .HasColumnName("anio_fin");
            entity.Property(e => e.AnioInicio)
                .HasComment("Año aproximado de inicio")
                .HasColumnName("anio_inicio");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasComment("Descripción del movimiento")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasComment("Nombre del movimiento (Impresionismo, Cubismo)")
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Obra>(entity =>
        {
            entity.HasKey(e => e.IdObra).HasName("pk_obra");

            entity.ToTable("obra", tb => tb.HasComment("Obras de arte de la colección"));

            entity.Property(e => e.IdObra)
                .HasComment("Identificador único de la obra")
                .HasColumnName("id_obra");
            entity.Property(e => e.Activa)
                .HasComment("Indica si la obra está visible (1=sí, 0=no)")
                .HasDefaultValue(true, "df_obra_activa")
                .HasColumnName("activa");
            entity.Property(e => e.AltoCm)
                .HasComment("Alto en centímetros")
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("alto_cm");
            entity.Property(e => e.AnchoCm)
                .HasComment("Ancho en centímetros")
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("ancho_cm");
            entity.Property(e => e.AnioCreacion)
                .HasComment("Año de creación")
                .HasColumnName("anio_creacion");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasComment("Descripción de la obra")
                .HasColumnName("descripcion");
            entity.Property(e => e.EnVenta)
                .HasComment("Indica si la obra está a la venta (1=sí, 0=no)")
                .HasColumnName("en_venta");
            entity.Property(e => e.FechaIngreso)
                .HasComment("Fecha de ingreso a la colección")
                .HasColumnName("fecha_ingreso");
            entity.Property(e => e.IdCategoria)
                .HasComment("Categoría de la obra")
                .HasColumnName("id_categoria");
            entity.Property(e => e.IdEstadoConservacion)
                .HasComment("Estado de conservación actual")
                .HasColumnName("id_estado_conservacion");
            entity.Property(e => e.IdMoneda)
                .HasComment("Moneda del precio (solo si está a la venta)")
                .HasColumnName("id_moneda");
            entity.Property(e => e.IdMovimiento)
                .HasComment("Movimiento artístico al que pertenece")
                .HasColumnName("id_movimiento");
            entity.Property(e => e.IdTecnica)
                .HasComment("Técnica utilizada")
                .HasColumnName("id_tecnica");
            entity.Property(e => e.Precio)
                .HasComment("Precio de venta")
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.ProfundidadCm)
                .HasComment("Profundidad en centímetros (esculturas)")
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("profundidad_cm");
            entity.Property(e => e.Titulo)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("Título de la obra")
                .HasColumnName("titulo");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Obras)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_obra_categoria");

            entity.HasOne(d => d.IdEstadoConservacionNavigation).WithMany(p => p.Obras)
                .HasForeignKey(d => d.IdEstadoConservacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_obra_estcons");

            entity.HasOne(d => d.IdMonedaNavigation).WithMany(p => p.Obras)
                .HasForeignKey(d => d.IdMoneda)
                .HasConstraintName("fk_obra_moneda");

            entity.HasOne(d => d.IdMovimientoNavigation).WithMany(p => p.Obras)
                .HasForeignKey(d => d.IdMovimiento)
                .HasConstraintName("fk_obra_movimiento");

            entity.HasOne(d => d.IdTecnicaNavigation).WithMany(p => p.Obras)
                .HasForeignKey(d => d.IdTecnica)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_obra_tecnica");
        });

        modelBuilder.Entity<ObraArtistum>(entity =>
        {
            entity.HasKey(e => new { e.IdObra, e.IdArtista }).HasName("pk_obra_artista");

            entity.ToTable("obra_artista", tb => tb.HasComment("Relación N:M entre obras y artistas (obras colaborativas)"));

            entity.Property(e => e.IdObra)
                .HasComment("Obra")
                .HasColumnName("id_obra");
            entity.Property(e => e.IdArtista)
                .HasComment("Artista participante")
                .HasColumnName("id_artista");
            entity.Property(e => e.RolArtista)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasComment("Rol del artista en la obra (Autor, Coautor)")
                .HasDefaultValue("Autor", "df_obra_artista_rol")
                .HasColumnName("rol_artista");

            entity.HasOne(d => d.IdArtistaNavigation).WithMany(p => p.ObraArtista)
                .HasForeignKey(d => d.IdArtista)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_obart_artista");

            entity.HasOne(d => d.IdObraNavigation).WithMany(p => p.ObraArtista)
                .HasForeignKey(d => d.IdObra)
                .HasConstraintName("fk_obart_obra");
        });

        modelBuilder.Entity<ObraMultimedium>(entity =>
        {
            entity.HasKey(e => e.IdMultimedia).HasName("pk_obra_multimedia");

            entity.ToTable("obra_multimedia", tb => tb.HasComment("Imágenes, videos, audios y modelos 3D de cada obra"));

            entity.Property(e => e.IdMultimedia)
                .HasComment("Identificador único del archivo multimedia")
                .HasColumnName("id_multimedia");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("Descripción o pie de imagen")
                .HasColumnName("descripcion");
            entity.Property(e => e.EsPrincipal)
                .HasComment("Indica si es el archivo principal de la obra")
                .HasColumnName("es_principal");
            entity.Property(e => e.IdObra)
                .HasComment("Obra a la que pertenece el archivo")
                .HasColumnName("id_obra");
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("Tipo de archivo multimedia")
                .HasColumnName("tipo");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("Ubicación (URL) del archivo")
                .HasColumnName("url");

            entity.HasOne(d => d.IdObraNavigation).WithMany(p => p.ObraMultimedia)
                .HasForeignKey(d => d.IdObra)
                .HasConstraintName("fk_multi_obra");
        });

        modelBuilder.Entity<OrdenCompra>(entity =>
        {
            entity.HasKey(e => e.IdOrden).HasName("pk_orden_compra");

            entity.ToTable("orden_compra", tb => tb.HasComment("Órdenes de compra de obras (encabezado)"));

            entity.Property(e => e.IdOrden)
                .HasComment("Identificador único de la orden")
                .HasColumnName("id_orden");
            entity.Property(e => e.FechaOrden)
                .HasPrecision(0)
                .HasComment("Fecha y hora de la orden")
                .HasDefaultValueSql("(sysdatetime())", "df_orden_compra_fecha")
                .HasColumnName("fecha_orden");
            entity.Property(e => e.IdCliente)
                .HasComment("Cliente que realiza la compra")
                .HasColumnName("id_cliente");
            entity.Property(e => e.IdEstadoOrden)
                .HasComment("Estado actual de la orden")
                .HasColumnName("id_estado_orden");
            entity.Property(e => e.IdMoneda)
                .HasComment("Moneda de la transacción")
                .HasColumnName("id_moneda");
            entity.Property(e => e.Impuesto)
                .HasComment("Impuesto aplicado")
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("impuesto");
            entity.Property(e => e.Subtotal)
                .HasComment("Suma de los detalles sin impuesto")
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("subtotal");
            entity.Property(e => e.Total)
                .HasComment("Total a pagar")
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.OrdenCompras)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orden_cliente");

            entity.HasOne(d => d.IdEstadoOrdenNavigation).WithMany(p => p.OrdenCompras)
                .HasForeignKey(d => d.IdEstadoOrden)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orden_estado");

            entity.HasOne(d => d.IdMonedaNavigation).WithMany(p => p.OrdenCompras)
                .HasForeignKey(d => d.IdMoneda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orden_moneda");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("pk_pago");

            entity.ToTable("pago", tb => tb.HasComment("Pagos realizados por las órdenes de compra"));

            entity.Property(e => e.IdPago)
                .HasComment("Identificador único del pago")
                .HasColumnName("id_pago");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("Estado del pago")
                .HasDefaultValue("pendiente", "df_pago_estado")
                .HasColumnName("estado");
            entity.Property(e => e.FechaPago)
                .HasPrecision(0)
                .HasComment("Fecha y hora del pago")
                .HasDefaultValueSql("(sysdatetime())", "df_pago_fecha")
                .HasColumnName("fecha_pago");
            entity.Property(e => e.IdMetodoPago)
                .HasComment("Método de pago utilizado")
                .HasColumnName("id_metodo_pago");
            entity.Property(e => e.IdOrden)
                .HasComment("Orden que se paga")
                .HasColumnName("id_orden");
            entity.Property(e => e.Monto)
                .HasComment("Monto pagado")
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("monto");
            entity.Property(e => e.Referencia)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasComment("Número de referencia o transacción")
                .HasColumnName("referencia");

            entity.HasOne(d => d.IdMetodoPagoNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdMetodoPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pago_metodo");

            entity.HasOne(d => d.IdOrdenNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdOrden)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pago_orden");
        });

        modelBuilder.Entity<Pai>(entity =>
        {
            entity.HasKey(e => e.IdPais).HasName("pk_pais");

            entity.ToTable("pais", tb => tb.HasComment("Catálogo de países"));

            entity.HasIndex(e => e.CodigoIso, "uq_pais_iso").IsUnique();

            entity.HasIndex(e => e.Nombre, "uq_pais_nombre").IsUnique();

            entity.Property(e => e.IdPais)
                .HasComment("Identificador único del país")
                .HasColumnName("id_pais");
            entity.Property(e => e.CodigoIso)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("Código ISO de 2 letras del país")
                .HasColumnName("codigo_iso");
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasComment("Nombre del país")
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso).HasName("pk_permiso");

            entity.ToTable("permiso", tb => tb.HasComment("Catálogo de permisos del sistema"));

            entity.HasIndex(e => e.Codigo, "uq_permiso_codigo").IsUnique();

            entity.Property(e => e.IdPermiso)
                .HasComment("Identificador único del permiso")
                .HasColumnName("id_permiso");
            entity.Property(e => e.Codigo)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasComment("Código interno del permiso (ej. OBRA_CREAR)")
                .HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasComment("Descripción de la acción que autoriza")
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("pk_persona");

            entity.ToTable("persona", tb => tb.HasComment("Supertipo: datos comunes de toda persona (usuarios, artistas, curadores y clientes)"));

            entity.HasIndex(e => e.Identificacion, "uq_persona_identificacion")
                .IsUnique()
                .HasFilter("([identificacion] IS NOT NULL)");

            entity.Property(e => e.IdPersona)
                .HasComment("Identificador único de la persona (supertipo)")
                .HasColumnName("id_persona");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("Dirección de domicilio")
                .HasColumnName("direccion");
            entity.Property(e => e.FechaCreacion)
                .HasPrecision(0)
                .HasComment("Fecha y hora de creación del registro")
                .HasDefaultValueSql("(sysdatetime())", "df_persona_fecha_creacion")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaNacimiento)
                .HasComment("Fecha de nacimiento")
                .HasColumnName("fecha_nacimiento");
            entity.Property(e => e.IdCiudad)
                .HasComment("Ciudad de residencia de la persona")
                .HasColumnName("id_ciudad");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("Número de identidad o pasaporte")
                .HasColumnName("identificacion");
            entity.Property(e => e.Papellido)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasComment("Primer apellido de la persona")
                .HasColumnName("papellido");
            entity.Property(e => e.Pnombre)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasComment("Primer nombre de la persona")
                .HasColumnName("pnombre");
            entity.Property(e => e.Sapellido)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasComment("Segundo apellido de la persona (opcional)")
                .HasColumnName("sapellido");
            entity.Property(e => e.Snombre)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasComment("Segundo nombre de la persona (opcional)")
                .HasColumnName("snombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("Teléfono de contacto")
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdCiudadNavigation).WithMany(p => p.Personas)
                .HasForeignKey(d => d.IdCiudad)
                .HasConstraintName("fk_persona_ciudad");
        });

        modelBuilder.Entity<RecorridoParadum>(entity =>
        {
            entity.HasKey(e => e.IdParada).HasName("pk_recorrido_parada");

            entity.ToTable("recorrido_parada", tb => tb.HasComment("Paradas (obras) de cada recorrido guiado"));

            entity.HasIndex(e => new { e.IdRecorrido, e.Orden }, "uq_parada_orden").IsUnique();

            entity.Property(e => e.IdParada)
                .HasComment("Identificador único de la parada")
                .HasColumnName("id_parada");
            entity.Property(e => e.IdObra)
                .HasComment("Obra que se visita en la parada")
                .HasColumnName("id_obra");
            entity.Property(e => e.IdRecorrido)
                .HasComment("Recorrido al que pertenece")
                .HasColumnName("id_recorrido");
            entity.Property(e => e.Narracion)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasComment("Texto o guion de la narración de la parada")
                .HasColumnName("narracion");
            entity.Property(e => e.Orden)
                .HasComment("Número de orden de la parada")
                .HasColumnName("orden");

            entity.HasOne(d => d.IdObraNavigation).WithMany(p => p.RecorridoParada)
                .HasForeignKey(d => d.IdObra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_parada_obra");

            entity.HasOne(d => d.IdRecorridoNavigation).WithMany(p => p.RecorridoParada)
                .HasForeignKey(d => d.IdRecorrido)
                .HasConstraintName("fk_parada_recorrido");
        });

        modelBuilder.Entity<RecorridoVirtual>(entity =>
        {
            entity.HasKey(e => e.IdRecorrido).HasName("pk_recorrido_virtual");

            entity.ToTable("recorrido_virtual", tb => tb.HasComment("Recorridos guiados dentro de una exposición"));

            entity.Property(e => e.IdRecorrido)
                .HasComment("Identificador único del recorrido")
                .HasColumnName("id_recorrido");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasComment("Descripción del recorrido")
                .HasColumnName("descripcion");
            entity.Property(e => e.DuracionEstimadaMin)
                .HasComment("Duración estimada en minutos")
                .HasColumnName("duracion_estimada_min");
            entity.Property(e => e.IdExposicion)
                .HasComment("Exposición que recorre")
                .HasColumnName("id_exposicion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("Nombre del recorrido guiado")
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdExposicionNavigation).WithMany(p => p.RecorridoVirtuals)
                .HasForeignKey(d => d.IdExposicion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_recorrido_expo");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("pk_rol");

            entity.ToTable("rol", tb => tb.HasComment("Catálogo de roles del sistema"));

            entity.HasIndex(e => e.Nombre, "uq_rol_nombre").IsUnique();

            entity.Property(e => e.IdRol)
                .HasComment("Identificador único del rol")
                .HasColumnName("id_rol");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasComment("Descripción de las responsabilidades del rol")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasComment("Nombre del rol (Administrador, Curador, Visitante)")
                .HasColumnName("nombre");

            entity.HasMany(d => d.IdPermisos).WithMany(p => p.IdRols)
                .UsingEntity<Dictionary<string, object>>(
                    "RolPermiso",
                    r => r.HasOne<Permiso>().WithMany()
                        .HasForeignKey("IdPermiso")
                        .HasConstraintName("fk_rolperm_permiso"),
                    l => l.HasOne<Rol>().WithMany()
                        .HasForeignKey("IdRol")
                        .HasConstraintName("fk_rolperm_rol"),
                    j =>
                    {
                        j.HasKey("IdRol", "IdPermiso").HasName("pk_rol_permiso");
                        j.ToTable("rol_permiso", tb => tb.HasComment("Tabla intermedia que asigna permisos a roles (N:M)"));
                        j.IndexerProperty<int>("IdRol")
                            .HasComment("Rol al que se asigna el permiso")
                            .HasColumnName("id_rol");
                        j.IndexerProperty<int>("IdPermiso")
                            .HasComment("Permiso asignado al rol")
                            .HasColumnName("id_permiso");
                    });
        });

        modelBuilder.Entity<SalaVirtual>(entity =>
        {
            entity.HasKey(e => e.IdSala).HasName("pk_sala_virtual");

            entity.ToTable("sala_virtual", tb => tb.HasComment("Salas virtuales donde se montan las exposiciones"));

            entity.HasIndex(e => e.Nombre, "uq_sala_nombre").IsUnique();

            entity.Property(e => e.IdSala)
                .HasComment("Identificador único de la sala")
                .HasColumnName("id_sala");
            entity.Property(e => e.Activa)
                .HasComment("Indica si la sala está disponible")
                .HasDefaultValue(true, "df_sala_virtual_activa")
                .HasColumnName("activa");
            entity.Property(e => e.CapacidadVisitantes)
                .HasComment("Máximo de visitantes simultáneos")
                .HasColumnName("capacidad_visitantes");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasComment("Descripción de la sala")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasComment("Nombre de la sala virtual")
                .HasColumnName("nombre");
            entity.Property(e => e.UrlEntorno3d)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("URL del entorno 3D de la sala")
                .HasColumnName("url_entorno_3d");
        });

        modelBuilder.Entity<Tecnica>(entity =>
        {
            entity.HasKey(e => e.IdTecnica).HasName("pk_tecnica");

            entity.ToTable("tecnica", tb => tb.HasComment("Catálogo de técnicas artísticas"));

            entity.HasIndex(e => e.Nombre, "uq_tecnica_nombre").IsUnique();

            entity.Property(e => e.IdTecnica)
                .HasComment("Identificador único de la técnica")
                .HasColumnName("id_tecnica");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("Descripción de la técnica")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasComment("Nombre de la técnica (óleo, acuarela, grabado)")
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoEntradum>(entity =>
        {
            entity.HasKey(e => e.IdTipoEntrada).HasName("pk_tipo_entrada");

            entity.ToTable("tipo_entrada", tb => tb.HasComment("Catálogo de tipos de entrada y su precio"));

            entity.HasIndex(e => e.Nombre, "uq_tipoentrada_nombre").IsUnique();

            entity.Property(e => e.IdTipoEntrada)
                .HasComment("Identificador único del tipo de entrada")
                .HasColumnName("id_tipo_entrada");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasComment("Descripción del beneficio")
                .HasColumnName("descripcion");
            entity.Property(e => e.IdMoneda)
                .HasComment("Moneda del precio")
                .HasColumnName("id_moneda");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Nombre del tipo (general, estudiante, VIP)")
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasComment("Precio de la entrada (0 = gratuita)")
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");

            entity.HasOne(d => d.IdMonedaNavigation).WithMany(p => p.TipoEntrada)
                .HasForeignKey(d => d.IdMoneda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tipoent_moneda");
        });

        modelBuilder.Entity<TipoEvento>(entity =>
        {
            entity.HasKey(e => e.IdTipoEvento).HasName("pk_tipo_evento");

            entity.ToTable("tipo_evento", tb => tb.HasComment("Catálogo de tipos de evento"));

            entity.HasIndex(e => e.Nombre, "uq_tipoevento_nombre").IsUnique();

            entity.Property(e => e.IdTipoEvento)
                .HasComment("Identificador único del tipo de evento")
                .HasColumnName("id_tipo_evento");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("Descripción del tipo de evento")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasComment("Nombre del tipo (inauguración, taller, conferencia)")
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("pk_usuario");

            entity.ToTable("usuario", tb => tb.HasComment("Subtipo de persona: cuenta de acceso a la plataforma"));

            entity.HasIndex(e => e.Correo, "uq_usuario_correo").IsUnique();

            entity.Property(e => e.IdPersona)
                .ValueGeneratedNever()
                .HasComment("PK y FK hacia persona (subtipo de persona)")
                .HasColumnName("id_persona");
            entity.Property(e => e.Activo)
                .HasComment("Indica si la cuenta está activa (1=sí, 0=no)")
                .HasDefaultValue(true, "df_usuario_activo")
                .HasColumnName("activo");
            entity.Property(e => e.ContrasenaHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("Contraseña cifrada (hash)")
                .HasColumnName("contrasena_hash");
            entity.Property(e => e.Correo)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasComment("Correo electrónico (se usa para iniciar sesión)")
                .HasColumnName("correo");
            entity.Property(e => e.FechaRegistro)
                .HasPrecision(0)
                .HasComment("Fecha y hora de registro en la plataforma")
                .HasDefaultValueSql("(sysdatetime())", "df_usuario_fecha_registro")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.IdRol)
                .HasComment("Rol asignado al usuario")
                .HasColumnName("id_rol");

            entity.HasOne(d => d.IdPersonaNavigation).WithOne(p => p.Usuario)
                .HasForeignKey<Usuario>(d => d.IdPersona)
                .HasConstraintName("fk_usuario_persona");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usuario_rol");
        });

        modelBuilder.Entity<Valoracion>(entity =>
        {
            entity.HasKey(e => e.IdValoracion).HasName("pk_valoracion");

            entity.ToTable("valoracion", tb => tb.HasComment("Valoraciones y comentarios de los usuarios sobre las obras"));

            entity.HasIndex(e => new { e.IdUsuario, e.IdObra }, "uq_valoracion_usuario_obra").IsUnique();

            entity.Property(e => e.IdValoracion)
                .HasComment("Identificador único de la valoración")
                .HasColumnName("id_valoracion");
            entity.Property(e => e.Comentario)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasComment("Comentario del usuario")
                .HasColumnName("comentario");
            entity.Property(e => e.Fecha)
                .HasPrecision(0)
                .HasComment("Fecha y hora de la valoración")
                .HasDefaultValueSql("(sysdatetime())", "df_valoracion_fecha")
                .HasColumnName("fecha");
            entity.Property(e => e.IdObra)
                .HasComment("Obra valorada")
                .HasColumnName("id_obra");
            entity.Property(e => e.IdUsuario)
                .HasComment("Usuario que valora")
                .HasColumnName("id_usuario");
            entity.Property(e => e.Puntuacion)
                .HasComment("Puntuación de 1 a 5")
                .HasColumnName("puntuacion");

            entity.HasOne(d => d.IdObraNavigation).WithMany(p => p.Valoracions)
                .HasForeignKey(d => d.IdObra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_valor_obra");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Valoracions)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_valor_usuario");
        });

        modelBuilder.Entity<VwAuditoriaAcceso>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_auditoria_accesos");

            entity.Property(e => e.Accion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("accion");
            entity.Property(e => e.Correo)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Exitoso).HasColumnName("exitoso");
            entity.Property(e => e.FechaHora)
                .HasPrecision(0)
                .HasColumnName("fecha_hora");
            entity.Property(e => e.IdBitacora).HasColumnName("id_bitacora");
            entity.Property(e => e.IpOrigen)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("ip_origen");
            entity.Property(e => e.Usuario)
                .HasMaxLength(91)
                .IsUnicode(false)
                .HasColumnName("usuario");
        });

        modelBuilder.Entity<VwCatalogoObra>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_catalogo_obras");

            entity.Property(e => e.AnioCreacion).HasColumnName("anio_creacion");
            entity.Property(e => e.Artista)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("artista");
            entity.Property(e => e.Categoria)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("categoria");
            entity.Property(e => e.EnVenta).HasColumnName("en_venta");
            entity.Property(e => e.EstadoConservacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado_conservacion");
            entity.Property(e => e.IdObra).HasColumnName("id_obra");
            entity.Property(e => e.Moneda)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("moneda");
            entity.Property(e => e.Movimiento)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("movimiento");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Tecnica)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("tecnica");
            entity.Property(e => e.Titulo)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("titulo");
        });

        modelBuilder.Entity<VwEntradasPorExposicion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_entradas_por_exposicion");

            entity.Property(e => e.EntradasUtilizadas).HasColumnName("entradas_utilizadas");
            entity.Property(e => e.Exposicion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("exposicion");
            entity.Property(e => e.IdExposicion).HasColumnName("id_exposicion");
            entity.Property(e => e.RecaudacionEstimada)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("recaudacion_estimada");
            entity.Property(e => e.TipoEntrada)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_entrada");
            entity.Property(e => e.TotalEntradas).HasColumnName("total_entradas");
        });

        modelBuilder.Entity<VwExposicionesActiva>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_exposiciones_activas");

            entity.Property(e => e.CapacidadVisitantes).HasColumnName("capacidad_visitantes");
            entity.Property(e => e.EsPermanente).HasColumnName("es_permanente");
            entity.Property(e => e.Exposicion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("exposicion");
            entity.Property(e => e.FechaFin).HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.IdExposicion).HasColumnName("id_exposicion");
            entity.Property(e => e.SalaVirtual)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("sala_virtual");
            entity.Property(e => e.TotalObrasExhibidas).HasColumnName("total_obras_exhibidas");
        });

        modelBuilder.Entity<VwHistorialConservacionObra>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_historial_conservacion_obras");

            entity.Property(e => e.EstadoRegistrado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado_registrado");
            entity.Property(e => e.FechaRevision).HasColumnName("fecha_revision");
            entity.Property(e => e.IdHistorial).HasColumnName("id_historial");
            entity.Property(e => e.Obra)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("obra");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("observaciones");
            entity.Property(e => e.Responsable)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("responsable");
        });

        modelBuilder.Entity<VwItinerarioRecorrido>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_itinerario_recorridos");

            entity.Property(e => e.Exposicion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("exposicion");
            entity.Property(e => e.IdRecorrido).HasColumnName("id_recorrido");
            entity.Property(e => e.Narracion)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("narracion");
            entity.Property(e => e.NumeroParada).HasColumnName("numero_parada");
            entity.Property(e => e.ObraParada)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("obra_parada");
            entity.Property(e => e.Recorrido)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("recorrido");
        });

        modelBuilder.Entity<VwObrasEnVentum>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_obras_en_venta");

            entity.Property(e => e.Artista)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("artista");
            entity.Property(e => e.Categoria)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("categoria");
            entity.Property(e => e.IdObra).HasColumnName("id_obra");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.PrecioFormateado)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("precio_formateado");
            entity.Property(e => e.Titulo)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("titulo");
        });

        modelBuilder.Entity<VwRankingValoracionesObra>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_ranking_valoraciones_obras");

            entity.Property(e => e.CantidadResenas).HasColumnName("cantidad_resenas");
            entity.Property(e => e.IdObra).HasColumnName("id_obra");
            entity.Property(e => e.PromedioPuntuacion)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("promedio_puntuacion");
            entity.Property(e => e.Titulo)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("titulo");
        });

        modelBuilder.Entity<VwResumenOrdenesCompra>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_resumen_ordenes_compra");

            entity.Property(e => e.Cliente)
                .HasMaxLength(91)
                .IsUnicode(false)
                .HasColumnName("cliente");
            entity.Property(e => e.EstadoOrden)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("estado_orden");
            entity.Property(e => e.FechaOrden)
                .HasPrecision(0)
                .HasColumnName("fecha_orden");
            entity.Property(e => e.IdOrden).HasColumnName("id_orden");
            entity.Property(e => e.Impuesto)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("impuesto");
            entity.Property(e => e.Moneda)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("moneda");
            entity.Property(e => e.Rtn)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("rtn");
            entity.Property(e => e.Subtotal)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("subtotal");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("total");
        });

        modelBuilder.Entity<VwUsuariosRole>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_usuarios_roles");

            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ciudad");
            entity.Property(e => e.Correo)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.FechaRegistro)
                .HasPrecision(0)
                .HasColumnName("fecha_registro");
            entity.Property(e => e.IdPersona).HasColumnName("id_persona");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("identificacion");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(91)
                .IsUnicode(false)
                .HasColumnName("nombre_completo");
            entity.Property(e => e.Pais)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("pais");
            entity.Property(e => e.Rol)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
