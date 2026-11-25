-- ============================================================================
-- PROYECTO: MUSEO VIRTUAL (BASE DE DATOS I - UNAH)
-- MOTOR: MICROSOFT SQL SERVER (T-SQL)
-- ARCHIVO 2: SCRIPT COMPLETO (DDL, COMENTARIOS, VISTAS Y CARGA MASIVA)
-- ============================================================================

USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = N'museo_virtual')
BEGIN
    ALTER DATABASE museo_virtual SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE museo_virtual;
END
GO

CREATE DATABASE museo_virtual;
GO

USE museo_virtual;
GO

-- ============================================================================
-- 1. CREACIÓN DE TABLAS, TIPOS DE DATOS OPTIMIZADOS Y CONSTRAINTS NOMBRADOS
-- ============================================================================

-- 1.1. pais
CREATE TABLE dbo.pais (
    id_pais INT IDENTITY(1,1) NOT NULL,
    nombre VARCHAR(80) NOT NULL,
    codigo_iso CHAR(2) NOT NULL,
    CONSTRAINT pk_pais PRIMARY KEY (id_pais),
    CONSTRAINT uq_pais_nombre UNIQUE (nombre),
    CONSTRAINT uq_pais_iso UNIQUE (codigo_iso)
);
GO

-- 1.2. ciudad
CREATE TABLE dbo.ciudad (
    id_ciudad INT IDENTITY(1,1) NOT NULL,
    id_pais INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    CONSTRAINT pk_ciudad PRIMARY KEY (id_ciudad),
    CONSTRAINT uq_ciudad_pais_nombre UNIQUE (id_pais, nombre),
    CONSTRAINT fk_ciudad_pais FOREIGN KEY (id_pais) REFERENCES dbo.pais(id_pais)
);
GO

-- 1.3. moneda
CREATE TABLE dbo.moneda (
    id_moneda INT IDENTITY(1,1) NOT NULL,
    codigo CHAR(3) NOT NULL,
    nombre VARCHAR(50) NOT NULL,
    simbolo VARCHAR(5) NOT NULL,
    CONSTRAINT pk_moneda PRIMARY KEY (id_moneda),
    CONSTRAINT uq_moneda_codigo UNIQUE (codigo)
);
GO

-- 1.4. metodo_pago
CREATE TABLE dbo.metodo_pago (
    id_metodo_pago INT IDENTITY(1,1) NOT NULL,
    nombre VARCHAR(50) NOT NULL,
    descripcion VARCHAR(150) NULL,
    activo BIT NOT NULL CONSTRAINT df_metodo_pago_activo DEFAULT (1),
    CONSTRAINT pk_metodo_pago PRIMARY KEY (id_metodo_pago),
    CONSTRAINT uq_metodo_pago_nombre UNIQUE (nombre)
);
GO

-- 1.5. rol
CREATE TABLE dbo.rol (
    id_rol INT IDENTITY(1,1) NOT NULL,
    nombre VARCHAR(40) NOT NULL,
    descripcion VARCHAR(150) NULL,
    CONSTRAINT pk_rol PRIMARY KEY (id_rol),
    CONSTRAINT uq_rol_nombre UNIQUE (nombre)
);
GO

-- 1.6. permiso
CREATE TABLE dbo.permiso (
    id_permiso INT IDENTITY(1,1) NOT NULL,
    codigo VARCHAR(60) NOT NULL,
    descripcion VARCHAR(150) NULL,
    CONSTRAINT pk_permiso PRIMARY KEY (id_permiso),
    CONSTRAINT uq_permiso_codigo UNIQUE (codigo)
);
GO

-- 1.7. rol_permiso
CREATE TABLE dbo.rol_permiso (
    id_rol INT NOT NULL,
    id_permiso INT NOT NULL,
    CONSTRAINT pk_rol_permiso PRIMARY KEY (id_rol, id_permiso),
    CONSTRAINT fk_rolperm_rol FOREIGN KEY (id_rol) REFERENCES dbo.rol(id_rol) ON DELETE CASCADE,
    CONSTRAINT fk_rolperm_permiso FOREIGN KEY (id_permiso) REFERENCES dbo.permiso(id_permiso) ON DELETE CASCADE
);
GO

-- 1.8. persona
CREATE TABLE dbo.persona (
    id_persona INT IDENTITY(1,1) NOT NULL,
    id_ciudad INT NULL,
    pnombre VARCHAR(45) NOT NULL,
    snombre VARCHAR(45) NULL,
    papellido VARCHAR(45) NOT NULL,
    sapellido VARCHAR(45) NULL,
    identificacion VARCHAR(20) NULL,
    fecha_nacimiento DATE NULL,
    telefono VARCHAR(20) NULL,
    direccion VARCHAR(200) NULL,
    fecha_creacion DATETIME2(0) NOT NULL CONSTRAINT df_persona_fecha_creacion DEFAULT (SYSDATETIME()),
    CONSTRAINT pk_persona PRIMARY KEY (id_persona),
    CONSTRAINT fk_persona_ciudad FOREIGN KEY (id_ciudad) REFERENCES dbo.ciudad(id_ciudad)
);
CREATE UNIQUE INDEX uq_persona_identificacion ON dbo.persona(identificacion) WHERE identificacion IS NOT NULL;
GO

-- 1.9. usuario
CREATE TABLE dbo.usuario (
    id_persona INT NOT NULL,
    id_rol INT NOT NULL,
    correo VARCHAR(120) NOT NULL,
    contrasena_hash VARCHAR(255) NOT NULL,
    fecha_registro DATETIME2(0) NOT NULL CONSTRAINT df_usuario_fecha_registro DEFAULT (SYSDATETIME()),
    activo BIT NOT NULL CONSTRAINT df_usuario_activo DEFAULT (1),
    CONSTRAINT pk_usuario PRIMARY KEY (id_persona),
    CONSTRAINT uq_usuario_correo UNIQUE (correo),
    CONSTRAINT fk_usuario_persona FOREIGN KEY (id_persona) REFERENCES dbo.persona(id_persona) ON DELETE CASCADE,
    CONSTRAINT fk_usuario_rol FOREIGN KEY (id_rol) REFERENCES dbo.rol(id_rol)
);
GO

-- 1.10. bitacora_acceso
CREATE TABLE dbo.bitacora_acceso (
    id_bitacora INT IDENTITY(1,1) NOT NULL,
    id_usuario INT NOT NULL,
    fecha_hora DATETIME2(0) NOT NULL CONSTRAINT df_bitacora_fecha_hora DEFAULT (SYSDATETIME()),
    ip_origen VARCHAR(45) NULL,
    accion VARCHAR(50) NOT NULL,
    exitoso BIT NOT NULL CONSTRAINT df_bitacora_exitoso DEFAULT (1),
    CONSTRAINT pk_bitacora_acceso PRIMARY KEY (id_bitacora),
    CONSTRAINT fk_bitacora_usuario FOREIGN KEY (id_usuario) REFERENCES dbo.usuario(id_persona)
);
GO

-- 1.11. artista
CREATE TABLE dbo.artista (
    id_persona INT NOT NULL,
    id_pais INT NOT NULL,
    nombre_artistico VARCHAR(120) NOT NULL,
    fecha_fallecimiento DATE NULL,
    biografia VARCHAR(1000) NULL,
    sitio_web VARCHAR(200) NULL,
    CONSTRAINT pk_artista PRIMARY KEY (id_persona),
    CONSTRAINT fk_artista_persona FOREIGN KEY (id_persona) REFERENCES dbo.persona(id_persona) ON DELETE CASCADE,
    CONSTRAINT fk_artista_pais FOREIGN KEY (id_pais) REFERENCES dbo.pais(id_pais)
);
GO

-- 1.12. movimiento_artistico
CREATE TABLE dbo.movimiento_artistico (
    id_movimiento INT IDENTITY(1,1) NOT NULL,
    nombre VARCHAR(80) NOT NULL,
    descripcion VARCHAR(1000) NULL,
    anio_inicio SMALLINT NULL,
    anio_fin SMALLINT NULL,
    CONSTRAINT pk_movimiento_artistico PRIMARY KEY (id_movimiento),
    CONSTRAINT uq_movimiento_nombre UNIQUE (nombre),
    CONSTRAINT ck_movimiento_anios CHECK (anio_fin IS NULL OR anio_inicio IS NULL OR anio_fin >= anio_inicio)
);
GO

-- 1.13. tecnica
CREATE TABLE dbo.tecnica (
    id_tecnica INT IDENTITY(1,1) NOT NULL,
    nombre VARCHAR(80) NOT NULL,
    descripcion VARCHAR(200) NULL,
    CONSTRAINT pk_tecnica PRIMARY KEY (id_tecnica),
    CONSTRAINT uq_tecnica_nombre UNIQUE (nombre)
);
GO

-- 1.14. categoria_obra
CREATE TABLE dbo.categoria_obra (
    id_categoria INT IDENTITY(1,1) NOT NULL,
    nombre VARCHAR(60) NOT NULL,
    descripcion VARCHAR(200) NULL,
    CONSTRAINT pk_categoria_obra PRIMARY KEY (id_categoria),
    CONSTRAINT uq_categoria_nombre UNIQUE (nombre)
);
GO

-- 1.15. estado_conservacion
CREATE TABLE dbo.estado_conservacion (
    id_estado_conservacion INT IDENTITY(1,1) NOT NULL,
    nombre VARCHAR(50) NOT NULL,
    descripcion VARCHAR(200) NULL,
    CONSTRAINT pk_estado_conservacion PRIMARY KEY (id_estado_conservacion),
    CONSTRAINT uq_estcons_nombre UNIQUE (nombre)
);
GO

-- 1.16. obra
CREATE TABLE dbo.obra (
    id_obra INT IDENTITY(1,1) NOT NULL,
    id_categoria INT NOT NULL,
    id_tecnica INT NOT NULL,
    id_movimiento INT NULL,
    id_estado_conservacion INT NOT NULL,
    id_moneda INT NULL,
    titulo VARCHAR(200) NOT NULL,
    anio_creacion SMALLINT NULL,
    descripcion VARCHAR(1000) NULL,
    alto_cm DECIMAL(8,2) NULL,
    ancho_cm DECIMAL(8,2) NULL,
    profundidad_cm DECIMAL(8,2) NULL,
    en_venta BIT NOT NULL CONSTRAINT df_obra_en_venta DEFAULT (0),
    precio DECIMAL(12,2) NULL,
    fecha_ingreso DATE NOT NULL,
    activa BIT NOT NULL CONSTRAINT df_obra_activa DEFAULT (1),
    CONSTRAINT pk_obra PRIMARY KEY (id_obra),
    CONSTRAINT ck_obra_precio CHECK (precio IS NULL OR precio >= 0),
    CONSTRAINT ck_obra_dimensiones CHECK ((alto_cm IS NULL OR alto_cm > 0) AND (ancho_cm IS NULL OR ancho_cm > 0) AND (profundidad_cm IS NULL OR profundidad_cm > 0)),
    CONSTRAINT fk_obra_categoria FOREIGN KEY (id_categoria) REFERENCES dbo.categoria_obra(id_categoria),
    CONSTRAINT fk_obra_tecnica FOREIGN KEY (id_tecnica) REFERENCES dbo.tecnica(id_tecnica),
    CONSTRAINT fk_obra_movimiento FOREIGN KEY (id_movimiento) REFERENCES dbo.movimiento_artistico(id_movimiento),
    CONSTRAINT fk_obra_estcons FOREIGN KEY (id_estado_conservacion) REFERENCES dbo.estado_conservacion(id_estado_conservacion),
    CONSTRAINT fk_obra_moneda FOREIGN KEY (id_moneda) REFERENCES dbo.moneda(id_moneda)
);
GO

-- 1.17. obra_artista
CREATE TABLE dbo.obra_artista (
    id_obra INT NOT NULL,
    id_artista INT NOT NULL,
    rol_artista VARCHAR(40) NOT NULL CONSTRAINT df_obra_artista_rol DEFAULT ('Autor'),
    CONSTRAINT pk_obra_artista PRIMARY KEY (id_obra, id_artista),
    CONSTRAINT fk_obart_obra FOREIGN KEY (id_obra) REFERENCES dbo.obra(id_obra) ON DELETE CASCADE,
    CONSTRAINT fk_obart_artista FOREIGN KEY (id_artista) REFERENCES dbo.artista(id_persona)
);
GO

-- 1.18. obra_multimedia
CREATE TABLE dbo.obra_multimedia (
    id_multimedia INT IDENTITY(1,1) NOT NULL,
    id_obra INT NOT NULL,
    tipo VARCHAR(20) NOT NULL,
    url VARCHAR(255) NOT NULL,
    descripcion VARCHAR(200) NULL,
    es_principal BIT NOT NULL CONSTRAINT df_obra_multimedia_principal DEFAULT (0),
    CONSTRAINT pk_obra_multimedia PRIMARY KEY (id_multimedia),
    CONSTRAINT ck_obra_multimedia_tipo CHECK (tipo IN ('imagen', 'video', 'audio', 'modelo_3d')),
    CONSTRAINT fk_multi_obra FOREIGN KEY (id_obra) REFERENCES dbo.obra(id_obra) ON DELETE CASCADE
);
GO

-- 1.19. historial_conservacion
CREATE TABLE dbo.historial_conservacion (
    id_historial INT IDENTITY(1,1) NOT NULL,
    id_obra INT NOT NULL,
    id_estado_conservacion INT NOT NULL,
    fecha DATE NOT NULL,
    responsable VARCHAR(100) NOT NULL,
    observaciones VARCHAR(1000) NULL,
    CONSTRAINT pk_historial_conservacion PRIMARY KEY (id_historial),
    CONSTRAINT fk_histcons_obra FOREIGN KEY (id_obra) REFERENCES dbo.obra(id_obra),
    CONSTRAINT fk_histcons_estado FOREIGN KEY (id_estado_conservacion) REFERENCES dbo.estado_conservacion(id_estado_conservacion)
);
GO

-- 1.20. sala_virtual
CREATE TABLE dbo.sala_virtual (
    id_sala INT IDENTITY(1,1) NOT NULL,
    nombre VARCHAR(80) NOT NULL,
    descripcion VARCHAR(250) NULL,
    capacidad_visitantes INT NOT NULL,
    url_entorno_3d VARCHAR(255) NULL,
    activa BIT NOT NULL CONSTRAINT df_sala_virtual_activa DEFAULT (1),
    CONSTRAINT pk_sala_virtual PRIMARY KEY (id_sala),
    CONSTRAINT uq_sala_nombre UNIQUE (nombre),
    CONSTRAINT ck_sala_capacidad CHECK (capacidad_visitantes > 0)
);
GO

-- 1.21. curador
CREATE TABLE dbo.curador (
    id_persona INT NOT NULL,
    especialidad VARCHAR(100) NULL,
    biografia VARCHAR(1000) NULL,
    CONSTRAINT pk_curador PRIMARY KEY (id_persona),
    CONSTRAINT fk_curador_persona FOREIGN KEY (id_persona) REFERENCES dbo.persona(id_persona) ON DELETE CASCADE
);
GO

-- 1.22. exposicion
CREATE TABLE dbo.exposicion (
    id_exposicion INT IDENTITY(1,1) NOT NULL,
    id_sala INT NOT NULL,
    titulo VARCHAR(150) NOT NULL,
    descripcion VARCHAR(1000) NULL,
    fecha_inicio DATE NOT NULL,
    fecha_fin DATE NULL,
    es_permanente BIT NOT NULL CONSTRAINT df_exposicion_permanente DEFAULT (0),
    estado VARCHAR(20) NOT NULL CONSTRAINT df_exposicion_estado DEFAULT ('planificada'),
    CONSTRAINT pk_exposicion PRIMARY KEY (id_exposicion),
    CONSTRAINT ck_exposicion_estado CHECK (estado IN ('planificada', 'activa', 'finalizada')),
    CONSTRAINT ck_exposicion_fechas CHECK (fecha_fin IS NULL OR fecha_fin >= fecha_inicio),
    CONSTRAINT fk_expo_sala FOREIGN KEY (id_sala) REFERENCES dbo.sala_virtual(id_sala)
);
GO

-- 1.23. exposicion_curador
CREATE TABLE dbo.exposicion_curador (
    id_exposicion INT NOT NULL,
    id_curador INT NOT NULL,
    CONSTRAINT pk_exposicion_curador PRIMARY KEY (id_exposicion, id_curador),
    CONSTRAINT fk_expcur_expo FOREIGN KEY (id_exposicion) REFERENCES dbo.exposicion(id_exposicion) ON DELETE CASCADE,
    CONSTRAINT fk_expcur_curador FOREIGN KEY (id_curador) REFERENCES dbo.curador(id_persona)
);
GO

-- 1.24. exposicion_obra
CREATE TABLE dbo.exposicion_obra (
    id_exposicion INT NOT NULL,
    id_obra INT NOT NULL,
    orden_visualizacion INT NOT NULL CONSTRAINT df_exposicion_obra_orden DEFAULT (1),
    posicion_en_sala VARCHAR(50) NULL,
    CONSTRAINT pk_exposicion_obra PRIMARY KEY (id_exposicion, id_obra),
    CONSTRAINT fk_expobra_expo FOREIGN KEY (id_exposicion) REFERENCES dbo.exposicion(id_exposicion) ON DELETE CASCADE,
    CONSTRAINT fk_expobra_obra FOREIGN KEY (id_obra) REFERENCES dbo.obra(id_obra)
);
GO

-- 1.25. recorrido_virtual
CREATE TABLE dbo.recorrido_virtual (
    id_recorrido INT IDENTITY(1,1) NOT NULL,
    id_exposicion INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    descripcion VARCHAR(250) NULL,
    duracion_estimada_min INT NULL,
    CONSTRAINT pk_recorrido_virtual PRIMARY KEY (id_recorrido),
    CONSTRAINT fk_recorrido_expo FOREIGN KEY (id_exposicion) REFERENCES dbo.exposicion(id_exposicion)
);
GO

-- 1.26. recorrido_parada
CREATE TABLE dbo.recorrido_parada (
    id_parada INT IDENTITY(1,1) NOT NULL,
    id_recorrido INT NOT NULL,
    id_obra INT NOT NULL,
    orden INT NOT NULL,
    narracion VARCHAR(1000) NULL,
    CONSTRAINT pk_recorrido_parada PRIMARY KEY (id_parada),
    CONSTRAINT uq_parada_orden UNIQUE (id_recorrido, orden),
    CONSTRAINT fk_parada_recorrido FOREIGN KEY (id_recorrido) REFERENCES dbo.recorrido_virtual(id_recorrido) ON DELETE CASCADE,
    CONSTRAINT fk_parada_obra FOREIGN KEY (id_obra) REFERENCES dbo.obra(id_obra)
);
GO

-- 1.27. tipo_evento
CREATE TABLE dbo.tipo_evento (
    id_tipo_evento INT IDENTITY(1,1) NOT NULL,
    nombre VARCHAR(60) NOT NULL,
    descripcion VARCHAR(200) NULL,
    CONSTRAINT pk_tipo_evento PRIMARY KEY (id_tipo_evento),
    CONSTRAINT uq_tipoevento_nombre UNIQUE (nombre)
);
GO

-- 1.28. evento
CREATE TABLE dbo.evento (
    id_evento INT IDENTITY(1,1) NOT NULL,
    id_tipo_evento INT NOT NULL,
    id_exposicion INT NULL,
    titulo VARCHAR(150) NOT NULL,
    descripcion VARCHAR(1000) NULL,
    fecha_hora_inicio DATETIME2(0) NOT NULL,
    fecha_hora_fin DATETIME2(0) NOT NULL,
    cupo_maximo INT NULL,
    enlace_transmision VARCHAR(255) NULL,
    estado VARCHAR(20) NOT NULL CONSTRAINT df_evento_estado DEFAULT ('programado'),
    CONSTRAINT pk_evento PRIMARY KEY (id_evento),
    CONSTRAINT ck_evento_estado CHECK (estado IN ('programado', 'en_curso', 'finalizado', 'cancelado')),
    CONSTRAINT ck_evento_fechas CHECK (fecha_hora_fin > fecha_hora_inicio),
    CONSTRAINT fk_evento_tipo FOREIGN KEY (id_tipo_evento) REFERENCES dbo.tipo_evento(id_tipo_evento),
    CONSTRAINT fk_evento_expo FOREIGN KEY (id_exposicion) REFERENCES dbo.exposicion(id_exposicion)
);
GO

-- 1.29. tipo_entrada
CREATE TABLE dbo.tipo_entrada (
    id_tipo_entrada INT IDENTITY(1,1) NOT NULL,
    id_moneda INT NOT NULL,
    nombre VARCHAR(50) NOT NULL,
    descripcion VARCHAR(150) NULL,
    precio DECIMAL(10,2) NOT NULL CONSTRAINT df_tipo_entrada_precio DEFAULT (0),
    CONSTRAINT pk_tipo_entrada PRIMARY KEY (id_tipo_entrada),
    CONSTRAINT uq_tipoentrada_nombre UNIQUE (nombre),
    CONSTRAINT ck_tipo_entrada_precio CHECK (precio >= 0),
    CONSTRAINT fk_tipoent_moneda FOREIGN KEY (id_moneda) REFERENCES dbo.moneda(id_moneda)
);
GO

-- 1.30. entrada
CREATE TABLE dbo.entrada (
    id_entrada INT IDENTITY(1,1) NOT NULL,
    id_tipo_entrada INT NOT NULL,
    id_usuario INT NOT NULL,
    id_exposicion INT NOT NULL,
    id_evento INT NULL,
    codigo_qr VARCHAR(64) NOT NULL,
    fecha_compra DATETIME2(0) NOT NULL CONSTRAINT df_entrada_fecha_compra DEFAULT (SYSDATETIME()),
    fecha_visita DATE NOT NULL,
    usada BIT NOT NULL CONSTRAINT df_entrada_usada DEFAULT (0),
    CONSTRAINT pk_entrada PRIMARY KEY (id_entrada),
    CONSTRAINT uq_entrada_qr UNIQUE (codigo_qr),
    CONSTRAINT fk_entrada_tipo FOREIGN KEY (id_tipo_entrada) REFERENCES dbo.tipo_entrada(id_tipo_entrada),
    CONSTRAINT fk_entrada_usuario FOREIGN KEY (id_usuario) REFERENCES dbo.usuario(id_persona),
    CONSTRAINT fk_entrada_expo FOREIGN KEY (id_exposicion) REFERENCES dbo.exposicion(id_exposicion),
    CONSTRAINT fk_entrada_evento FOREIGN KEY (id_evento) REFERENCES dbo.evento(id_evento)
);
GO

-- 1.31. cliente
CREATE TABLE dbo.cliente (
    id_persona INT NOT NULL,
    rtn VARCHAR(20) NULL,
    direccion_envio VARCHAR(200) NULL,
    fecha_alta DATETIME2(0) NOT NULL CONSTRAINT df_cliente_fecha_alta DEFAULT (SYSDATETIME()),
    CONSTRAINT pk_cliente PRIMARY KEY (id_persona),
    CONSTRAINT fk_cliente_persona FOREIGN KEY (id_persona) REFERENCES dbo.persona(id_persona) ON DELETE CASCADE
);
CREATE UNIQUE INDEX uq_cliente_rtn ON dbo.cliente(rtn) WHERE rtn IS NOT NULL;
GO

-- 1.32. estado_orden
CREATE TABLE dbo.estado_orden (
    id_estado_orden INT IDENTITY(1,1) NOT NULL,
    nombre VARCHAR(40) NOT NULL,
    descripcion VARCHAR(150) NULL,
    CONSTRAINT pk_estado_orden PRIMARY KEY (id_estado_orden),
    CONSTRAINT uq_estorden_nombre UNIQUE (nombre)
);
GO

-- 1.33. orden_compra
CREATE TABLE dbo.orden_compra (
    id_orden INT IDENTITY(1,1) NOT NULL,
    id_cliente INT NOT NULL,
    id_estado_orden INT NOT NULL,
    id_moneda INT NOT NULL,
    fecha_orden DATETIME2(0) NOT NULL CONSTRAINT df_orden_compra_fecha DEFAULT (SYSDATETIME()),
    subtotal DECIMAL(12,2) NOT NULL,
    impuesto DECIMAL(12,2) NOT NULL CONSTRAINT df_orden_compra_impuesto DEFAULT (0),
    total DECIMAL(12,2) NOT NULL,
    CONSTRAINT pk_orden_compra PRIMARY KEY (id_orden),
    CONSTRAINT ck_orden_montos CHECK (subtotal >= 0 AND impuesto >= 0 AND total >= 0),
    CONSTRAINT fk_orden_cliente FOREIGN KEY (id_cliente) REFERENCES dbo.cliente(id_persona),
    CONSTRAINT fk_orden_estado FOREIGN KEY (id_estado_orden) REFERENCES dbo.estado_orden(id_estado_orden),
    CONSTRAINT fk_orden_moneda FOREIGN KEY (id_moneda) REFERENCES dbo.moneda(id_moneda)
);
GO

-- 1.34. detalle_orden
CREATE TABLE dbo.detalle_orden (
    id_detalle INT IDENTITY(1,1) NOT NULL,
    id_orden INT NOT NULL,
    id_obra INT NOT NULL,
    cantidad INT NOT NULL CONSTRAINT df_detalle_orden_cantidad DEFAULT (1),
    precio_unitario DECIMAL(12,2) NOT NULL,
    CONSTRAINT pk_detalle_orden PRIMARY KEY (id_detalle),
    CONSTRAINT uq_detalle_orden_obra UNIQUE (id_orden, id_obra),
    CONSTRAINT ck_detalle_cantidad CHECK (cantidad > 0),
    CONSTRAINT ck_detalle_precio CHECK (precio_unitario >= 0),
    CONSTRAINT fk_detalle_orden FOREIGN KEY (id_orden) REFERENCES dbo.orden_compra(id_orden) ON DELETE CASCADE,
    CONSTRAINT fk_detalle_obra FOREIGN KEY (id_obra) REFERENCES dbo.obra(id_obra)
);
GO

-- 1.35. pago
CREATE TABLE dbo.pago (
    id_pago INT IDENTITY(1,1) NOT NULL,
    id_orden INT NOT NULL,
    id_metodo_pago INT NOT NULL,
    monto DECIMAL(12,2) NOT NULL,
    fecha_pago DATETIME2(0) NOT NULL CONSTRAINT df_pago_fecha DEFAULT (SYSDATETIME()),
    referencia VARCHAR(60) NULL,
    estado VARCHAR(20) NOT NULL CONSTRAINT df_pago_estado DEFAULT ('pendiente'),
    CONSTRAINT pk_pago PRIMARY KEY (id_pago),
    CONSTRAINT ck_pago_monto CHECK (monto > 0),
    CONSTRAINT ck_pago_estado CHECK (estado IN ('pendiente', 'aprobado', 'rechazado')),
    CONSTRAINT fk_pago_orden FOREIGN KEY (id_orden) REFERENCES dbo.orden_compra(id_orden),
    CONSTRAINT fk_pago_metodo FOREIGN KEY (id_metodo_pago) REFERENCES dbo.metodo_pago(id_metodo_pago)
);
GO

-- 1.36. favorito
CREATE TABLE dbo.favorito (
    id_usuario INT NOT NULL,
    id_obra INT NOT NULL,
    fecha_agregado DATETIME2(0) NOT NULL CONSTRAINT df_favorito_fecha DEFAULT (SYSDATETIME()),
    CONSTRAINT pk_favorito PRIMARY KEY (id_usuario, id_obra),
    CONSTRAINT fk_fav_usuario FOREIGN KEY (id_usuario) REFERENCES dbo.usuario(id_persona) ON DELETE CASCADE,
    CONSTRAINT fk_fav_obra FOREIGN KEY (id_obra) REFERENCES dbo.obra(id_obra) ON DELETE CASCADE
);
GO

-- 1.37. valoracion
CREATE TABLE dbo.valoracion (
    id_valoracion INT IDENTITY(1,1) NOT NULL,
    id_usuario INT NOT NULL,
    id_obra INT NOT NULL,
    puntuacion TINYINT NOT NULL,
    comentario VARCHAR(1000) NULL,
    fecha DATETIME2(0) NOT NULL CONSTRAINT df_valoracion_fecha DEFAULT (SYSDATETIME()),
    CONSTRAINT pk_valoracion PRIMARY KEY (id_valoracion),
    CONSTRAINT uq_valoracion_usuario_obra UNIQUE (id_usuario, id_obra),
    CONSTRAINT ck_valoracion_puntuacion CHECK (puntuacion BETWEEN 1 AND 5),
    CONSTRAINT fk_valor_usuario FOREIGN KEY (id_usuario) REFERENCES dbo.usuario(id_persona),
    CONSTRAINT fk_valor_obra FOREIGN KEY (id_obra) REFERENCES dbo.obra(id_obra)
);
GO

-- ============================================================================
-- 2. COMENTARIOS EN TODAS LAS TABLAS Y COLUMNAS (MS_Description)
-- ============================================================================
CREATE OR ALTER PROCEDURE dbo.sp_comentar_objeto
    @tabla SYSNAME,
    @columna SYSNAME = NULL,
    @descripcion SQL_VARIANT
AS
BEGIN
    SET NOCOUNT ON;
    IF @columna IS NULL
        EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=@descripcion,
            @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=@tabla;
    ELSE
        EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=@descripcion,
            @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=@tabla,
            @level2type=N'COLUMN', @level2name=@columna;
END;
GO

EXEC dbo.sp_comentar_objeto 'pais', NULL, 'Catálogo de países';
EXEC dbo.sp_comentar_objeto 'pais', 'id_pais', 'Identificador único del país';
EXEC dbo.sp_comentar_objeto 'pais', 'nombre', 'Nombre del país';
EXEC dbo.sp_comentar_objeto 'pais', 'codigo_iso', 'Código ISO de 2 letras del país';

EXEC dbo.sp_comentar_objeto 'ciudad', NULL, 'Catálogo de ciudades por país';
EXEC dbo.sp_comentar_objeto 'ciudad', 'id_ciudad', 'Identificador único de la ciudad';
EXEC dbo.sp_comentar_objeto 'ciudad', 'id_pais', 'País al que pertenece la ciudad';
EXEC dbo.sp_comentar_objeto 'ciudad', 'nombre', 'Nombre de la ciudad';

EXEC dbo.sp_comentar_objeto 'moneda', NULL, 'Catálogo de monedas para precios y pagos';
EXEC dbo.sp_comentar_objeto 'moneda', 'id_moneda', 'Identificador único de la moneda';
EXEC dbo.sp_comentar_objeto 'moneda', 'codigo', 'Código ISO 4217 de la moneda (HNL, USD)';
EXEC dbo.sp_comentar_objeto 'moneda', 'nombre', 'Nombre de la moneda';
EXEC dbo.sp_comentar_objeto 'moneda', 'simbolo', 'Símbolo de la moneda';

EXEC dbo.sp_comentar_objeto 'metodo_pago', NULL, 'Catálogo de métodos de pago (tarjeta, transferencia, etc.)';
EXEC dbo.sp_comentar_objeto 'metodo_pago', 'id_metodo_pago', 'Identificador único del método de pago';
EXEC dbo.sp_comentar_objeto 'metodo_pago', 'nombre', 'Nombre del método de pago';
EXEC dbo.sp_comentar_objeto 'metodo_pago', 'descripcion', 'Descripción del método de pago';
EXEC dbo.sp_comentar_objeto 'metodo_pago', 'activo', 'Indica si el método está habilitado (1=sí, 0=no)';

EXEC dbo.sp_comentar_objeto 'rol', NULL, 'Catálogo de roles del sistema';
EXEC dbo.sp_comentar_objeto 'rol', 'id_rol', 'Identificador único del rol';
EXEC dbo.sp_comentar_objeto 'rol', 'nombre', 'Nombre del rol (Administrador, Curador, Visitante)';
EXEC dbo.sp_comentar_objeto 'rol', 'descripcion', 'Descripción de las responsabilidades del rol';

EXEC dbo.sp_comentar_objeto 'permiso', NULL, 'Catálogo de permisos del sistema';
EXEC dbo.sp_comentar_objeto 'permiso', 'id_permiso', 'Identificador único del permiso';
EXEC dbo.sp_comentar_objeto 'permiso', 'codigo', 'Código interno del permiso (ej. OBRA_CREAR)';
EXEC dbo.sp_comentar_objeto 'permiso', 'descripcion', 'Descripción de la acción que autoriza';

EXEC dbo.sp_comentar_objeto 'rol_permiso', NULL, 'Tabla intermedia que asigna permisos a roles (N:M)';
EXEC dbo.sp_comentar_objeto 'rol_permiso', 'id_rol', 'Rol al que se asigna el permiso';
EXEC dbo.sp_comentar_objeto 'rol_permiso', 'id_permiso', 'Permiso asignado al rol';

EXEC dbo.sp_comentar_objeto 'persona', NULL, 'Supertipo: datos comunes de toda persona (usuarios, artistas, curadores y clientes)';
EXEC dbo.sp_comentar_objeto 'persona', 'id_persona', 'Identificador único de la persona (supertipo)';
EXEC dbo.sp_comentar_objeto 'persona', 'id_ciudad', 'Ciudad de residencia de la persona';
EXEC dbo.sp_comentar_objeto 'persona', 'pnombre', 'Primer nombre de la persona';
EXEC dbo.sp_comentar_objeto 'persona', 'snombre', 'Segundo nombre de la persona (opcional)';
EXEC dbo.sp_comentar_objeto 'persona', 'papellido', 'Primer apellido de la persona';
EXEC dbo.sp_comentar_objeto 'persona', 'sapellido', 'Segundo apellido de la persona (opcional)';
EXEC dbo.sp_comentar_objeto 'persona', 'identificacion', 'Número de identidad o pasaporte';
EXEC dbo.sp_comentar_objeto 'persona', 'fecha_nacimiento', 'Fecha de nacimiento';
EXEC dbo.sp_comentar_objeto 'persona', 'telefono', 'Teléfono de contacto';
EXEC dbo.sp_comentar_objeto 'persona', 'direccion', 'Dirección de domicilio';
EXEC dbo.sp_comentar_objeto 'persona', 'fecha_creacion', 'Fecha y hora de creación del registro';

EXEC dbo.sp_comentar_objeto 'usuario', NULL, 'Subtipo de persona: cuenta de acceso a la plataforma';
EXEC dbo.sp_comentar_objeto 'usuario', 'id_persona', 'PK y FK hacia persona (subtipo de persona)';
EXEC dbo.sp_comentar_objeto 'usuario', 'id_rol', 'Rol asignado al usuario';
EXEC dbo.sp_comentar_objeto 'usuario', 'correo', 'Correo electrónico (se usa para iniciar sesión)';
EXEC dbo.sp_comentar_objeto 'usuario', 'contrasena_hash', 'Contraseña cifrada (hash)';
EXEC dbo.sp_comentar_objeto 'usuario', 'fecha_registro', 'Fecha y hora de registro en la plataforma';
EXEC dbo.sp_comentar_objeto 'usuario', 'activo', 'Indica si la cuenta está activa (1=sí, 0=no)';

EXEC dbo.sp_comentar_objeto 'bitacora_acceso', NULL, 'Registro de accesos y acciones de los usuarios';
EXEC dbo.sp_comentar_objeto 'bitacora_acceso', 'id_bitacora', 'Identificador único del registro de bitácora';
EXEC dbo.sp_comentar_objeto 'bitacora_acceso', 'id_usuario', 'Usuario que realizó la acción';
EXEC dbo.sp_comentar_objeto 'bitacora_acceso', 'fecha_hora', 'Fecha y hora de la acción';
EXEC dbo.sp_comentar_objeto 'bitacora_acceso', 'ip_origen', 'Dirección IP de origen (IPv4 o IPv6)';
EXEC dbo.sp_comentar_objeto 'bitacora_acceso', 'accion', 'Acción realizada (LOGIN, LOGOUT, etc.)';
EXEC dbo.sp_comentar_objeto 'bitacora_acceso', 'exitoso', 'Indica si la acción fue exitosa (1=sí, 0=no)';

EXEC dbo.sp_comentar_objeto 'artista', NULL, 'Subtipo de persona: artistas autores de las obras';
EXEC dbo.sp_comentar_objeto 'artista', 'id_persona', 'PK y FK hacia persona (subtipo de persona)';
EXEC dbo.sp_comentar_objeto 'artista', 'id_pais', 'País de nacionalidad del artista';
EXEC dbo.sp_comentar_objeto 'artista', 'nombre_artistico', 'Nombre artístico del autor';
EXEC dbo.sp_comentar_objeto 'artista', 'fecha_fallecimiento', 'Fecha de fallecimiento (NULL si vive)';
EXEC dbo.sp_comentar_objeto 'artista', 'biografia', 'Reseña biográfica del artista';
EXEC dbo.sp_comentar_objeto 'artista', 'sitio_web', 'Sitio web oficial o portafolio';

EXEC dbo.sp_comentar_objeto 'movimiento_artistico', NULL, 'Catálogo de movimientos artísticos';
EXEC dbo.sp_comentar_objeto 'movimiento_artistico', 'id_movimiento', 'Identificador único del movimiento artístico';
EXEC dbo.sp_comentar_objeto 'movimiento_artistico', 'nombre', 'Nombre del movimiento (Impresionismo, Cubismo)';
EXEC dbo.sp_comentar_objeto 'movimiento_artistico', 'descripcion', 'Descripción del movimiento';
EXEC dbo.sp_comentar_objeto 'movimiento_artistico', 'anio_inicio', 'Año aproximado de inicio';
EXEC dbo.sp_comentar_objeto 'movimiento_artistico', 'anio_fin', 'Año aproximado de fin';

EXEC dbo.sp_comentar_objeto 'tecnica', NULL, 'Catálogo de técnicas artísticas';
EXEC dbo.sp_comentar_objeto 'tecnica', 'id_tecnica', 'Identificador único de la técnica';
EXEC dbo.sp_comentar_objeto 'tecnica', 'nombre', 'Nombre de la técnica (óleo, acuarela, grabado)';
EXEC dbo.sp_comentar_objeto 'tecnica', 'descripcion', 'Descripción de la técnica';

EXEC dbo.sp_comentar_objeto 'categoria_obra', NULL, 'Catálogo de categorías de obras';
EXEC dbo.sp_comentar_objeto 'categoria_obra', 'id_categoria', 'Identificador único de la categoría';
EXEC dbo.sp_comentar_objeto 'categoria_obra', 'nombre', 'Nombre de la categoría (pintura, escultura, fotografía)';
EXEC dbo.sp_comentar_objeto 'categoria_obra', 'descripcion', 'Descripción de la categoría';

EXEC dbo.sp_comentar_objeto 'estado_conservacion', NULL, 'Catálogo de estados de conservación de las obras';
EXEC dbo.sp_comentar_objeto 'estado_conservacion', 'id_estado_conservacion', 'Identificador único del estado de conservación';
EXEC dbo.sp_comentar_objeto 'estado_conservacion', 'nombre', 'Nombre del estado (excelente, bueno, en restauración)';
EXEC dbo.sp_comentar_objeto 'estado_conservacion', 'descripcion', 'Descripción del estado';

EXEC dbo.sp_comentar_objeto 'obra', NULL, 'Obras de arte de la colección';
EXEC dbo.sp_comentar_objeto 'obra', 'id_obra', 'Identificador único de la obra';
EXEC dbo.sp_comentar_objeto 'obra', 'id_categoria', 'Categoría de la obra';
EXEC dbo.sp_comentar_objeto 'obra', 'id_tecnica', 'Técnica utilizada';
EXEC dbo.sp_comentar_objeto 'obra', 'id_movimiento', 'Movimiento artístico al que pertenece';
EXEC dbo.sp_comentar_objeto 'obra', 'id_estado_conservacion', 'Estado de conservación actual';
EXEC dbo.sp_comentar_objeto 'obra', 'id_moneda', 'Moneda del precio (solo si está a la venta)';
EXEC dbo.sp_comentar_objeto 'obra', 'titulo', 'Título de la obra';
EXEC dbo.sp_comentar_objeto 'obra', 'anio_creacion', 'Año de creación';
EXEC dbo.sp_comentar_objeto 'obra', 'descripcion', 'Descripción de la obra';
EXEC dbo.sp_comentar_objeto 'obra', 'alto_cm', 'Alto en centímetros';
EXEC dbo.sp_comentar_objeto 'obra', 'ancho_cm', 'Ancho en centímetros';
EXEC dbo.sp_comentar_objeto 'obra', 'profundidad_cm', 'Profundidad en centímetros (esculturas)';
EXEC dbo.sp_comentar_objeto 'obra', 'en_venta', 'Indica si la obra está a la venta (1=sí, 0=no)';
EXEC dbo.sp_comentar_objeto 'obra', 'precio', 'Precio de venta';
EXEC dbo.sp_comentar_objeto 'obra', 'fecha_ingreso', 'Fecha de ingreso a la colección';
EXEC dbo.sp_comentar_objeto 'obra', 'activa', 'Indica si la obra está visible (1=sí, 0=no)';

EXEC dbo.sp_comentar_objeto 'obra_artista', NULL, 'Relación N:M entre obras y artistas (obras colaborativas)';
EXEC dbo.sp_comentar_objeto 'obra_artista', 'id_obra', 'Obra';
EXEC dbo.sp_comentar_objeto 'obra_artista', 'id_artista', 'Artista participante';
EXEC dbo.sp_comentar_objeto 'obra_artista', 'rol_artista', 'Rol del artista en la obra (Autor, Coautor)';

EXEC dbo.sp_comentar_objeto 'obra_multimedia', NULL, 'Imágenes, videos, audios y modelos 3D de cada obra';
EXEC dbo.sp_comentar_objeto 'obra_multimedia', 'id_multimedia', 'Identificador único del archivo multimedia';
EXEC dbo.sp_comentar_objeto 'obra_multimedia', 'id_obra', 'Obra a la que pertenece el archivo';
EXEC dbo.sp_comentar_objeto 'obra_multimedia', 'tipo', 'Tipo de archivo multimedia';
EXEC dbo.sp_comentar_objeto 'obra_multimedia', 'url', 'Ubicación (URL) del archivo';
EXEC dbo.sp_comentar_objeto 'obra_multimedia', 'descripcion', 'Descripción o pie de imagen';
EXEC dbo.sp_comentar_objeto 'obra_multimedia', 'es_principal', 'Indica si es el archivo principal de la obra';

EXEC dbo.sp_comentar_objeto 'historial_conservacion', NULL, 'Historial de revisiones y restauraciones de cada obra';
EXEC dbo.sp_comentar_objeto 'historial_conservacion', 'id_historial', 'Identificador único del registro de conservación';
EXEC dbo.sp_comentar_objeto 'historial_conservacion', 'id_obra', 'Obra evaluada';
EXEC dbo.sp_comentar_objeto 'historial_conservacion', 'id_estado_conservacion', 'Estado determinado en la revisión';
EXEC dbo.sp_comentar_objeto 'historial_conservacion', 'fecha', 'Fecha de la revisión o restauración';
EXEC dbo.sp_comentar_objeto 'historial_conservacion', 'responsable', 'Persona que realizó la revisión';
EXEC dbo.sp_comentar_objeto 'historial_conservacion', 'observaciones', 'Observaciones o trabajos realizados';

EXEC dbo.sp_comentar_objeto 'sala_virtual', NULL, 'Salas virtuales donde se montan las exposiciones';
EXEC dbo.sp_comentar_objeto 'sala_virtual', 'id_sala', 'Identificador único de la sala';
EXEC dbo.sp_comentar_objeto 'sala_virtual', 'nombre', 'Nombre de la sala virtual';
EXEC dbo.sp_comentar_objeto 'sala_virtual', 'descripcion', 'Descripción de la sala';
EXEC dbo.sp_comentar_objeto 'sala_virtual', 'capacidad_visitantes', 'Máximo de visitantes simultáneos';
EXEC dbo.sp_comentar_objeto 'sala_virtual', 'url_entorno_3d', 'URL del entorno 3D de la sala';
EXEC dbo.sp_comentar_objeto 'sala_virtual', 'activa', 'Indica si la sala está disponible';

EXEC dbo.sp_comentar_objeto 'curador', NULL, 'Subtipo de persona: curadores responsables de las exposiciones';
EXEC dbo.sp_comentar_objeto 'curador', 'id_persona', 'PK y FK hacia persona (subtipo de persona)';
EXEC dbo.sp_comentar_objeto 'curador', 'especialidad', 'Área de especialidad del curador';
EXEC dbo.sp_comentar_objeto 'curador', 'biografia', 'Reseña profesional del curador';

EXEC dbo.sp_comentar_objeto 'exposicion', NULL, 'Exposiciones temporales y permanentes';
EXEC dbo.sp_comentar_objeto 'exposicion', 'id_exposicion', 'Identificador único de la exposición';
EXEC dbo.sp_comentar_objeto 'exposicion', 'id_sala', 'Sala virtual donde se presenta';
EXEC dbo.sp_comentar_objeto 'exposicion', 'titulo', 'Título de la exposición';
EXEC dbo.sp_comentar_objeto 'exposicion', 'descripcion', 'Descripción o concepto de la exposición';
EXEC dbo.sp_comentar_objeto 'exposicion', 'fecha_inicio', 'Fecha de apertura';
EXEC dbo.sp_comentar_objeto 'exposicion', 'fecha_fin', 'Fecha de cierre (NULL si es permanente)';
EXEC dbo.sp_comentar_objeto 'exposicion', 'es_permanente', 'Indica si la exposición es permanente';
EXEC dbo.sp_comentar_objeto 'exposicion', 'estado', 'Estado actual de la exposición';

EXEC dbo.sp_comentar_objeto 'exposicion_curador', NULL, 'Relación N:M entre exposiciones y curadores';
EXEC dbo.sp_comentar_objeto 'exposicion_curador', 'id_exposicion', 'Exposición';
EXEC dbo.sp_comentar_objeto 'exposicion_curador', 'id_curador', 'Curador asignado';

EXEC dbo.sp_comentar_objeto 'exposicion_obra', NULL, 'Relación N:M entre exposiciones y obras exhibidas';
EXEC dbo.sp_comentar_objeto 'exposicion_obra', 'id_exposicion', 'Exposición';
EXEC dbo.sp_comentar_objeto 'exposicion_obra', 'id_obra', 'Obra exhibida';
EXEC dbo.sp_comentar_objeto 'exposicion_obra', 'orden_visualizacion', 'Orden en que se muestra la obra';
EXEC dbo.sp_comentar_objeto 'exposicion_obra', 'posicion_en_sala', 'Ubicación de la obra dentro de la sala (ej. Pared norte)';

EXEC dbo.sp_comentar_objeto 'recorrido_virtual', NULL, 'Recorridos guiados dentro de una exposición';
EXEC dbo.sp_comentar_objeto 'recorrido_virtual', 'id_recorrido', 'Identificador único del recorrido';
EXEC dbo.sp_comentar_objeto 'recorrido_virtual', 'id_exposicion', 'Exposición que recorre';
EXEC dbo.sp_comentar_objeto 'recorrido_virtual', 'nombre', 'Nombre del recorrido guiado';
EXEC dbo.sp_comentar_objeto 'recorrido_virtual', 'descripcion', 'Descripción del recorrido';
EXEC dbo.sp_comentar_objeto 'recorrido_virtual', 'duracion_estimada_min', 'Duración estimada en minutos';

EXEC dbo.sp_comentar_objeto 'recorrido_parada', NULL, 'Paradas (obras) de cada recorrido guiado';
EXEC dbo.sp_comentar_objeto 'recorrido_parada', 'id_parada', 'Identificador único de la parada';
EXEC dbo.sp_comentar_objeto 'recorrido_parada', 'id_recorrido', 'Recorrido al que pertenece';
EXEC dbo.sp_comentar_objeto 'recorrido_parada', 'id_obra', 'Obra que se visita en la parada';
EXEC dbo.sp_comentar_objeto 'recorrido_parada', 'orden', 'Número de orden de la parada';
EXEC dbo.sp_comentar_objeto 'recorrido_parada', 'narracion', 'Texto o guion de la narración de la parada';

EXEC dbo.sp_comentar_objeto 'tipo_evento', NULL, 'Catálogo de tipos de evento';
EXEC dbo.sp_comentar_objeto 'tipo_evento', 'id_tipo_evento', 'Identificador único del tipo de evento';
EXEC dbo.sp_comentar_objeto 'tipo_evento', 'nombre', 'Nombre del tipo (inauguración, taller, conferencia)';
EXEC dbo.sp_comentar_objeto 'tipo_evento', 'descripcion', 'Descripción del tipo de evento';

EXEC dbo.sp_comentar_objeto 'evento', NULL, 'Eventos virtuales del museo';
EXEC dbo.sp_comentar_objeto 'evento', 'id_evento', 'Identificador único del evento';
EXEC dbo.sp_comentar_objeto 'evento', 'id_tipo_evento', 'Tipo de evento';
EXEC dbo.sp_comentar_objeto 'evento', 'id_exposicion', 'Exposición asociada (opcional)';
EXEC dbo.sp_comentar_objeto 'evento', 'titulo', 'Título del evento';
EXEC dbo.sp_comentar_objeto 'evento', 'descripcion', 'Descripción del evento';
EXEC dbo.sp_comentar_objeto 'evento', 'fecha_hora_inicio', 'Fecha y hora de inicio';
EXEC dbo.sp_comentar_objeto 'evento', 'fecha_hora_fin', 'Fecha y hora de finalización';
EXEC dbo.sp_comentar_objeto 'evento', 'cupo_maximo', 'Cupo máximo de asistentes';
EXEC dbo.sp_comentar_objeto 'evento', 'enlace_transmision', 'Enlace de transmisión en línea';
EXEC dbo.sp_comentar_objeto 'evento', 'estado', 'Estado del evento';

EXEC dbo.sp_comentar_objeto 'tipo_entrada', NULL, 'Catálogo de tipos de entrada y su precio';
EXEC dbo.sp_comentar_objeto 'tipo_entrada', 'id_tipo_entrada', 'Identificador único del tipo de entrada';
EXEC dbo.sp_comentar_objeto 'tipo_entrada', 'id_moneda', 'Moneda del precio';
EXEC dbo.sp_comentar_objeto 'tipo_entrada', 'nombre', 'Nombre del tipo (general, estudiante, VIP)';
EXEC dbo.sp_comentar_objeto 'tipo_entrada', 'descripcion', 'Descripción del beneficio';
EXEC dbo.sp_comentar_objeto 'tipo_entrada', 'precio', 'Precio de la entrada (0 = gratuita)';

EXEC dbo.sp_comentar_objeto 'entrada', NULL, 'Entradas adquiridas por los visitantes';
EXEC dbo.sp_comentar_objeto 'entrada', 'id_entrada', 'Identificador único de la entrada';
EXEC dbo.sp_comentar_objeto 'entrada', 'id_tipo_entrada', 'Tipo de entrada adquirida';
EXEC dbo.sp_comentar_objeto 'entrada', 'id_usuario', 'Usuario que adquiere la entrada';
EXEC dbo.sp_comentar_objeto 'entrada', 'id_exposicion', 'Exposición a la que da acceso';
EXEC dbo.sp_comentar_objeto 'entrada', 'id_evento', 'Evento específico (opcional)';
EXEC dbo.sp_comentar_objeto 'entrada', 'codigo_qr', 'Código único de validación de la entrada';
EXEC dbo.sp_comentar_objeto 'entrada', 'fecha_compra', 'Fecha y hora de compra';
EXEC dbo.sp_comentar_objeto 'entrada', 'fecha_visita', 'Fecha para la que es válida';
EXEC dbo.sp_comentar_objeto 'entrada', 'usada', 'Indica si ya fue utilizada';

EXEC dbo.sp_comentar_objeto 'cliente', NULL, 'Subtipo de persona: clientes compradores de obras';
EXEC dbo.sp_comentar_objeto 'cliente', 'id_persona', 'PK y FK hacia persona (subtipo de persona)';
EXEC dbo.sp_comentar_objeto 'cliente', 'rtn', 'RTN para facturación';
EXEC dbo.sp_comentar_objeto 'cliente', 'direccion_envio', 'Dirección de envío de las obras compradas';
EXEC dbo.sp_comentar_objeto 'cliente', 'fecha_alta', 'Fecha de alta como cliente';

EXEC dbo.sp_comentar_objeto 'estado_orden', NULL, 'Catálogo de estados de una orden de compra';
EXEC dbo.sp_comentar_objeto 'estado_orden', 'id_estado_orden', 'Identificador único del estado de orden';
EXEC dbo.sp_comentar_objeto 'estado_orden', 'nombre', 'Nombre del estado (pendiente, pagada, enviada, cancelada)';
EXEC dbo.sp_comentar_objeto 'estado_orden', 'descripcion', 'Descripción del estado';

EXEC dbo.sp_comentar_objeto 'orden_compra', NULL, 'Órdenes de compra de obras (encabezado)';
EXEC dbo.sp_comentar_objeto 'orden_compra', 'id_orden', 'Identificador único de la orden';
EXEC dbo.sp_comentar_objeto 'orden_compra', 'id_cliente', 'Cliente que realiza la compra';
EXEC dbo.sp_comentar_objeto 'orden_compra', 'id_estado_orden', 'Estado actual de la orden';
EXEC dbo.sp_comentar_objeto 'orden_compra', 'id_moneda', 'Moneda de la transacción';
EXEC dbo.sp_comentar_objeto 'orden_compra', 'fecha_orden', 'Fecha y hora de la orden';
EXEC dbo.sp_comentar_objeto 'orden_compra', 'subtotal', 'Suma de los detalles sin impuesto';
EXEC dbo.sp_comentar_objeto 'orden_compra', 'impuesto', 'Impuesto aplicado';
EXEC dbo.sp_comentar_objeto 'orden_compra', 'total', 'Total a pagar';

EXEC dbo.sp_comentar_objeto 'detalle_orden', NULL, 'Detalle de obras incluidas en cada orden';
EXEC dbo.sp_comentar_objeto 'detalle_orden', 'id_detalle', 'Identificador único del detalle';
EXEC dbo.sp_comentar_objeto 'detalle_orden', 'id_orden', 'Orden a la que pertenece';
EXEC dbo.sp_comentar_objeto 'detalle_orden', 'id_obra', 'Obra comprada';
EXEC dbo.sp_comentar_objeto 'detalle_orden', 'cantidad', 'Cantidad (1 para obras únicas)';
EXEC dbo.sp_comentar_objeto 'detalle_orden', 'precio_unitario', 'Precio de la obra al momento de la compra';

EXEC dbo.sp_comentar_objeto 'pago', NULL, 'Pagos realizados por las órdenes de compra';
EXEC dbo.sp_comentar_objeto 'pago', 'id_pago', 'Identificador único del pago';
EXEC dbo.sp_comentar_objeto 'pago', 'id_orden', 'Orden que se paga';
EXEC dbo.sp_comentar_objeto 'pago', 'id_metodo_pago', 'Método de pago utilizado';
EXEC dbo.sp_comentar_objeto 'pago', 'monto', 'Monto pagado';
EXEC dbo.sp_comentar_objeto 'pago', 'fecha_pago', 'Fecha y hora del pago';
EXEC dbo.sp_comentar_objeto 'pago', 'referencia', 'Número de referencia o transacción';
EXEC dbo.sp_comentar_objeto 'pago', 'estado', 'Estado del pago';

EXEC dbo.sp_comentar_objeto 'favorito', NULL, 'Obras favoritas de cada usuario (N:M)';
EXEC dbo.sp_comentar_objeto 'favorito', 'id_usuario', 'Usuario que marca la obra';
EXEC dbo.sp_comentar_objeto 'favorito', 'id_obra', 'Obra marcada como favorita';
EXEC dbo.sp_comentar_objeto 'favorito', 'fecha_agregado', 'Fecha y hora en que se agregó';

EXEC dbo.sp_comentar_objeto 'valoracion', NULL, 'Valoraciones y comentarios de los usuarios sobre las obras';
EXEC dbo.sp_comentar_objeto 'valoracion', 'id_valoracion', 'Identificador único de la valoración';
EXEC dbo.sp_comentar_objeto 'valoracion', 'id_usuario', 'Usuario que valora';
EXEC dbo.sp_comentar_objeto 'valoracion', 'id_obra', 'Obra valorada';
EXEC dbo.sp_comentar_objeto 'valoracion', 'puntuacion', 'Puntuación de 1 a 5';
EXEC dbo.sp_comentar_objeto 'valoracion', 'comentario', 'Comentario del usuario';
EXEC dbo.sp_comentar_objeto 'valoracion', 'fecha', 'Fecha y hora de la valoración';

DROP PROCEDURE dbo.sp_comentar_objeto;
GO

-- ============================================================================
-- 3. VISTAS DEL SISTEMA (10 VISTAS REQUERIDAS)
-- ============================================================================

-- Vista 1: Catálogo completo de obras con artista, categoría, técnica y movimiento
CREATE VIEW dbo.vw_catalogo_obras AS
SELECT 
    o.id_obra,
    o.titulo,
    o.anio_creacion,
    a.nombre_artistico AS artista,
    c.nombre AS categoria,
    t.nombre AS tecnica,
    m.nombre AS movimiento,
    ec.nombre AS estado_conservacion,
    o.en_venta,
    o.precio,
    mon.codigo AS moneda
FROM dbo.obra o
INNER JOIN dbo.categoria_obra c ON o.id_categoria = c.id_categoria
INNER JOIN dbo.tecnica t ON o.id_tecnica = t.id_tecnica
LEFT JOIN dbo.movimiento_artistico m ON o.id_movimiento = m.id_movimiento
INNER JOIN dbo.estado_conservacion ec ON o.id_estado_conservacion = ec.id_estado_conservacion
LEFT JOIN dbo.moneda mon ON o.id_moneda = mon.id_moneda
LEFT JOIN dbo.obra_artista oa ON o.id_obra = oa.id_obra AND oa.rol_artista = 'Autor'
LEFT JOIN dbo.artista a ON oa.id_artista = a.id_persona
WHERE o.activa = 1;
GO

-- Vista 2: Obras disponibles para la venta
CREATE VIEW dbo.vw_obras_en_venta AS
SELECT 
    o.id_obra,
    o.titulo,
    a.nombre_artistico AS artista,
    c.nombre AS categoria,
    o.precio,
    mon.simbolo + ' ' + CAST(o.precio AS VARCHAR(20)) + ' ' + mon.codigo AS precio_formateado
FROM dbo.obra o
INNER JOIN dbo.categoria_obra c ON o.id_categoria = c.id_categoria
INNER JOIN dbo.moneda mon ON o.id_moneda = mon.id_moneda
LEFT JOIN dbo.obra_artista oa ON o.id_obra = oa.id_obra
LEFT JOIN dbo.artista a ON oa.id_artista = a.id_persona
WHERE o.en_venta = 1 AND o.activa = 1;
GO

-- Vista 3: Exposiciones activas y salas virtuales asignadas
CREATE VIEW dbo.vw_exposiciones_activas AS
SELECT 
    e.id_exposicion,
    e.titulo AS exposicion,
    s.nombre AS sala_virtual,
    s.capacidad_visitantes,
    e.fecha_inicio,
    e.fecha_fin,
    e.es_permanente,
    COUNT(eo.id_obra) AS total_obras_exhibidas
FROM dbo.exposicion e
INNER JOIN dbo.sala_virtual s ON e.id_sala = s.id_sala
LEFT JOIN dbo.exposicion_obra eo ON e.id_exposicion = eo.id_exposicion
WHERE e.estado = 'activa'
GROUP BY e.id_exposicion, e.titulo, s.nombre, s.capacidad_visitantes, e.fecha_inicio, e.fecha_fin, e.es_permanente;
GO

-- Vista 4: Perfil completo de usuarios y roles
CREATE VIEW dbo.vw_usuarios_roles AS
SELECT 
    u.id_persona,
    p.pnombre + ' ' + p.papellido AS nombre_completo,
    p.identificacion,
    u.correo,
    r.nombre AS rol,
    ciu.nombre AS ciudad,
    pa.nombre AS pais,
    u.fecha_registro,
    u.activo
FROM dbo.usuario u
INNER JOIN dbo.persona p ON u.id_persona = p.id_persona
INNER JOIN dbo.rol r ON u.id_rol = r.id_rol
LEFT JOIN dbo.ciudad ciu ON p.id_ciudad = ciu.id_ciudad
LEFT JOIN dbo.pais pa ON ciu.id_pais = pa.id_pais;
GO

-- Vista 5: Ventas y órdenes de compra con detalle de cliente
CREATE VIEW dbo.vw_resumen_ordenes_compra AS
SELECT 
    oc.id_orden,
    oc.fecha_orden,
    p.pnombre + ' ' + p.papellido AS cliente,
    c.rtn,
    eo.nombre AS estado_orden,
    mon.codigo AS moneda,
    oc.subtotal,
    oc.impuesto,
    oc.total
FROM dbo.orden_compra oc
INNER JOIN dbo.cliente c ON oc.id_cliente = c.id_persona
INNER JOIN dbo.persona p ON c.id_persona = p.id_persona
INNER JOIN dbo.estado_orden eo ON oc.id_estado_orden = eo.id_estado_orden
INNER JOIN dbo.moneda mon ON oc.id_moneda = mon.id_moneda;
GO

-- Vista 6: Ranking de obras mejor valoradas por los visitantes
CREATE VIEW dbo.vw_ranking_valoraciones_obras AS
SELECT 
    o.id_obra,
    o.titulo,
    COUNT(v.id_valoracion) AS cantidad_resenas,
    CAST(AVG(CAST(v.puntuacion AS DECIMAL(4,2))) AS DECIMAL(4,2)) AS promedio_puntuacion
FROM dbo.obra o
INNER JOIN dbo.valoracion v ON o.id_obra = v.id_obra
GROUP BY o.id_obra, o.titulo;
GO

-- Vista 7: Control de entradas vendidas por exposición
CREATE VIEW dbo.vw_entradas_por_exposicion AS
SELECT 
    ex.id_exposicion,
    ex.titulo AS exposicion,
    te.nombre AS tipo_entrada,
    COUNT(en.id_entrada) AS total_entradas,
    SUM(CASE WHEN en.usada = 1 THEN 1 ELSE 0 END) AS entradas_utilizadas,
    SUM(te.precio) AS recaudacion_estimada
FROM dbo.entrada en
INNER JOIN dbo.exposicion ex ON en.id_exposicion = ex.id_exposicion
INNER JOIN dbo.tipo_entrada te ON en.id_tipo_entrada = te.id_tipo_entrada
GROUP BY ex.id_exposicion, ex.titulo, te.nombre;
GO

-- Vista 8: Itinerario de paradas de los recorridos virtuales
CREATE VIEW dbo.vw_itinerario_recorridos AS
SELECT 
    rv.id_recorrido,
    rv.nombre AS recorrido,
    ex.titulo AS exposicion,
    rp.orden AS numero_parada,
    o.titulo AS obra_parada,
    rp.narracion
FROM dbo.recorrido_virtual rv
INNER JOIN dbo.exposicion ex ON rv.id_exposicion = ex.id_exposicion
INNER JOIN dbo.recorrido_parada rp ON rv.id_recorrido = rp.id_recorrido
INNER JOIN dbo.obra o ON rp.id_obra = o.id_obra;
GO

-- Vista 9: Historial de conservación y restauración de obras
CREATE VIEW dbo.vw_historial_conservacion_obras AS
SELECT 
    hc.id_historial,
    o.titulo AS obra,
    ec.nombre AS estado_registrado,
    hc.fecha AS fecha_revision,
    hc.responsable,
    hc.observaciones
FROM dbo.historial_conservacion hc
INNER JOIN dbo.obra o ON hc.id_obra = o.id_obra
INNER JOIN dbo.estado_conservacion ec ON hc.id_estado_conservacion = ec.id_estado_conservacion;
GO

-- Vista 10: Auditoría de accesos al sistema
CREATE VIEW dbo.vw_auditoria_accesos AS
SELECT 
    ba.id_bitacora,
    ba.fecha_hora,
    u.correo,
    p.pnombre + ' ' + p.papellido AS usuario,
    ba.ip_origen,
    ba.accion,
    ba.exitoso
FROM dbo.bitacora_acceso ba
INNER JOIN dbo.usuario u ON ba.id_usuario = u.id_persona
INNER JOIN dbo.persona p ON u.id_persona = p.id_persona;
GO

-- ============================================================================
-- 4. CARGA MASIVA DE DATOS DE PRUEBA (CATÁLOGOS >= 15, TRANSACCIONALES >= 30)
-- ============================================================================
SET NOCOUNT ON;

-- 4.1. Catálogo: pais (15 registros)
INSERT INTO dbo.pais (nombre, codigo_iso) VALUES
('Honduras', 'HN'), ('Guatemala', 'GT'), ('El Salvador', 'SV'), ('Nicaragua', 'NI'), ('Costa Rica', 'CR'),
('Panamá', 'PA'), ('México', 'MX'), ('Estados Unidos', 'US'), ('España', 'ES'), ('Francia', 'FR'),
('Italia', 'IT'), ('Alemania', 'DE'), ('Reino Unido', 'GB'), ('Japón', 'JP'), ('Argentina', 'AR');

-- 4.2. Catálogo: ciudad (15 registros)
INSERT INTO dbo.ciudad (id_pais, nombre) VALUES
(1, 'Tegucigalpa'), (1, 'San Pedro Sula'), (1, 'Comayagua'), (2, 'Ciudad de Guatemala'), (3, 'San Salvador'),
(4, 'Managua'), (5, 'San José'), (6, 'Ciudad de Panamá'), (7, 'Ciudad de México'), (8, 'Nueva York'),
(9, 'Madrid'), (10, 'París'), (11, 'Florencia'), (12, 'Berlín'), (15, 'Buenos Aires');

-- 4.3. Catálogo: moneda (15 registros)
INSERT INTO dbo.moneda (codigo, nombre, simbolo) VALUES
('HNL', 'Lempira Hondureño', 'L'), ('USD', 'Dólar Estadounidense', '$'), ('EUR', 'Euro', '€'),
('GTQ', 'Quetzal Guatemalteco', 'Q'), ('CRC', 'Colón Costarricense', '₡'), ('NIO', 'Córdoba Nicaragüense', 'C$'),
('MXN', 'Peso Mexicano', '$'), ('GBP', 'Libra Esterlina', '£'), ('JPY', 'Yen Japonés', '¥'),
('ARS', 'Peso Argentino', '$'), ('CAD', 'Dólar Canadiense', 'C$'), ('CHF', 'Franco Suizo', 'CHF'),
('BRL', 'Real Brasileño', 'R$'), ('COP', 'Peso Colombiano', '$'), ('PEN', 'Sol Peruano', 'S/');

-- 4.4. Catálogo: metodo_pago (15 registros)
INSERT INTO dbo.metodo_pago (nombre, descripcion, activo) VALUES
('Tarjeta de Crédito Visa', 'Pago con tarjeta Visa nacional o internacional', 1),
('Tarjeta de Crédito Mastercard', 'Pago con tarjeta Mastercard', 1),
('Tarjeta de Débito', 'Débito directo a cuenta bancaria', 1),
('Transferencia ACH', 'Transferencia interbancaria local', 1),
('Transferencia Internacional SWIFT', 'Transferencia bancaria internacional', 1),
('PayPal', 'Pasarela de pago digital PayPal', 1),
('Apple Pay', 'Pago móvil Apple Pay', 1),
('Google Pay', 'Pago móvil Google Pay', 1),
('Depósito Bancario', 'Depósito directo en ventanilla', 1),
('Billetera Tigo Money', 'Pago mediante billetera electrónica local', 1),
('Billetera Tengo', 'Pago mediante red Tengo', 1),
('Cheque Certificado', 'Cheque de caja certificado', 1),
('Criptomoneda USDC', 'Pago con stablecoin USDC', 1),
('Tarjeta American Express', 'Pago con tarjeta Amex', 1),
('Efectivo en Taquilla Física', 'Pago presencial en sede asociada', 0);

-- 4.5. Catálogo: rol (15 registros)
INSERT INTO dbo.rol (nombre, descripcion) VALUES
('Administrador', 'Control total del sistema'), ('Curador Jefe', 'Dirección de exposiciones y colecciones'),
('Curador Asociado', 'Gestión de salas y montajes'), ('Restaurador', 'Registro de conservación de obras'),
('Visitante Registrado', 'Acceso a recorridos y compras'), ('Visitante VIP', 'Acceso a eventos exclusivos'),
('Artista Verificado', 'Gestión de portafolio propio'), ('Auditor', 'Consulta de bitácoras y reportes'),
('Gestor de Tienda', 'Administración de órdenes y ventas'), ('Coordinador de Eventos', 'Programación de talleres y charlas'),
('Guía Virtual', 'Creación de recorridos narrados'), ('Soporte Técnico', 'Atención a usuarios'),
('Investigador', 'Acceso a fichas técnicas completas'), ('Prensa', 'Acceso a material promocional'),
('Invitado Especial', 'Acceso temporal de cortesía');

-- 4.6. Catálogo: permiso (15 registros)
INSERT INTO dbo.permiso (codigo, descripcion) VALUES
('OBRA_CREAR', 'Registrar nuevas obras'), ('OBRA_EDITAR', 'Modificar fichas de obras'),
('OBRA_ELIMINAR', 'Dar de baja obras'), ('EXPO_CREAR', 'Crear exposiciones'),
('EXPO_PUBLICAR', 'Activar exposiciones en salas'), ('USUARIO_GESTION', 'Administrar cuentas de usuario'),
('ROL_ASIGNAR', 'Modificar roles y permisos'), ('ORDEN_VER', 'Consultar órdenes de compra'),
('ORDEN_ACTUALIZAR', 'Cambiar estado de órdenes'), ('PAGO_VERIFICAR', 'Aprobar o rechazar pagos'),
('CONSERVACION_REG', 'Registrar historial de conservación'), ('EVENTO_CREAR', 'Programar eventos virtuales'),
('RECORRIDO_EDITAR', 'Configurar paradas de recorridos'), ('BITACORA_VER', 'Consultar registros de auditoría'),
('REPORTE_EXPORTAR', 'Descargar reportes del sistema');

-- 4.7. Tabla intermedia: rol_permiso (15 registros)
INSERT INTO dbo.rol_permiso (id_rol, id_permiso) VALUES
(1,1),(1,2),(1,3),(1,4),(1,5),(1,6),(1,7),(1,8),(1,9),(1,10),(1,11),(1,12),(1,13),(1,14),(1,15);

-- 4.8. Catálogo: movimiento_artistico (15 registros)
INSERT INTO dbo.movimiento_artistico (nombre, descripcion, anio_inicio, anio_fin) VALUES
('Renacimiento', 'Movimiento cultural europeo', 1400, 1600),
('Barroco', 'Estilo ornamentado y dramático', 1600, 1750),
('Neoclasicismo', 'Retorno a los valores clásicos', 1750, 1830),
('Romanticismo', 'Énfasis en la emoción y naturaleza', 1800, 1850),
('Realismo', 'Representación objetiva de la vida', 1840, 1880),
('Impresionismo', 'Captura de la luz y el instante', 1860, 1890),
('Postimpresionismo', 'Evolución expresiva del impresionismo', 1886, 1905),
('Modernismo', 'Ruptura con el academicismo tradicional', 1890, 1910),
('Expresionismo', 'Deformación subjetiva de la realidad', 1905, 1925),
('Cubismo', 'Perspectivas múltiples y geometría', 1907, 1914),
('Futurismo', 'Exaltación de la velocidad y tecnología', 1909, 1944),
('Dadaísmo', 'Movimiento antiarte y absurdo', 1916, 1924),
('Surrealismo', 'Exploración del inconsciente y sueños', 1924, 1966),
('Expresionismo Abstracto', 'Arte no figurativo de posguerra', 1943, 1965),
('Arte Contemporáneo', 'Expresiones artísticas actuales', 1970, NULL);

-- 4.9. Catálogo: tecnica (15 registros)
INSERT INTO dbo.tecnica (nombre, descripcion) VALUES
('Óleo sobre lienzo', 'Pigmentos aglutinados con aceites sobre tela'),
('Acrílico', 'Pintura de secado rápido a base de polímeros'),
('Acuarela', 'Pigmentos diluidos en agua sobre papel'),
('Temple', 'Pigmentos mezclados con yema de huevo'),
('Fresco', 'Pintura aplicada sobre yeso húmedo'),
('Grabado en metal', 'Estampación mediante planchas metálicas'),
('Litografía', 'Impresión sobre piedra caliza'),
('Serigrafía', 'Estampación a través de malla tensada'),
('Talla en madera', 'Escultura sustractiva en madera'),
('Fundición en bronce', 'Escultura metálica por moldeado'),
('Talla en mármol', 'Escultura en piedra caliza metamórfica'),
('Fotografía analógica', 'Captura química sobre película'),
('Fotografía digital', 'Captura electrónica de alta resolución'),
('Arte generativo 3D', 'Modelado y renderizado tridimensional'),
('Técnica mixta', 'Combinación de múltiples materiales');

-- 4.10. Catálogo: categoria_obra (15 registros)
INSERT INTO dbo.categoria_obra (nombre, descripcion) VALUES
('Pintura', 'Obras pictóricas bidimensionales'), ('Escultura', 'Obras tridimensionales físicas'),
('Fotografía', 'Obras fotográficas artísticas'), ('Grabado', 'Obra gráfica seriada'),
('Dibujo', 'Obras sobre papel a lápiz, tinta o carboncillo'), ('Arte Digital', 'Creaciones nativas digitales'),
('Instalación Virtual', 'Experiencias espaciales interactivas'), ('Cerámica', 'Piezas artísticas en barro o porcelana'),
('Textil', 'Tapices y arte en fibras'), ('Mosaico', 'Composición con teselas'),
('Collage', 'Ensamblaje de diversos materiales'), ('Arte Cinético', 'Obras con movimiento real o aparente'),
('Miniatura', 'Pintura de formato reducido'), ('Muralismo', 'Obra monumental arquitectónica'),
('Modelado 3D', 'Esculturas digitales navegables');

-- 4.11. Catálogo: estado_conservacion (15 registros)
INSERT INTO dbo.estado_conservacion (nombre, descripcion) VALUES
('Excelente', 'Sin daños visibles ni deterioro'), ('Muy Bueno', 'Desgaste mínimo acorde a su antigüedad'),
('Bueno', 'Estable, requiere monitoreo rutinario'), ('Regular', 'Presenta leves alteraciones superficiales'),
('En Observación', 'Bajo seguimiento preventivo'), ('En Restauración', 'Intervención activa en taller'),
('Requiere Limpieza', 'Acumulación superficial de polvo o barniz oxidado'), ('Frágil', 'Manipulación estrictamente restringida'),
('Estabilizado', 'Tratamiento de emergencia completado'), ('Deterioro Leve', 'Pequeñas fisuras o craquelado'),
('Deterioro Moderado', 'Pérdida parcial de capa pictórica'), ('Digitalizado', 'Respaldo de alta resolución completado'),
('En Cuarentena', 'Aislado por control ambiental'), ('Óptimo para Exhibición', 'Aprobado para sala principal'),
('Baja Temporal', 'Retirado de exhibición por mantenimiento');

-- 4.12. Catálogo: sala_virtual (15 registros)
INSERT INTO dbo.sala_virtual (nombre, descripcion, capacidad_visitantes, url_entorno_3d, activa) VALUES
('Sala Maya y Mesoamérica', 'Galería de arte prehispánico y contemporáneo', 200, 'https://museo.hn/3d/sala-01', 1),
('Sala Renacimiento Europeo', 'Clásicos europeos en alta definición', 150, 'https://museo.hn/3d/sala-02', 1),
('Sala Impresionismo y Luz', 'Galería panorámica con iluminación dinámica', 180, 'https://museo.hn/3d/sala-03', 1),
('Pabellón Arte Hondureño', 'Maestros de la plástica nacional', 250, 'https://museo.hn/3d/sala-04', 1),
('Galería Vanguardias Siglo XX', 'Cubismo, surrealismo y abstracción', 160, 'https://museo.hn/3d/sala-05', 1),
('Rotonda de Escultura', 'Espacio circular 360 grados para piezas 3D', 120, 'https://museo.hn/3d/sala-06', 1),
('Sala Fotografía Documental', 'Exhibición de archivo fotográfico', 140, 'https://museo.hn/3d/sala-07', 1),
('Laboratorio Arte Digital', 'Instalaciones inmersivas y generativas', 300, 'https://museo.hn/3d/sala-08', 1),
('Sala Barroco Colonial', 'Arte sacro y colonial latinoamericano', 130, 'https://museo.hn/3d/sala-09', 1),
('Galería de Grabado y Dibujo', 'Obra sobre papel con zoom microscópico', 100, 'https://museo.hn/3d/sala-10', 1),
('Auditorio Virtual Principal', 'Espacio para inauguraciones y conferencias', 500, 'https://museo.hn/3d/sala-11', 1),
('Sala Temporales Norte', 'Exposiciones itinerantes internacionales', 200, 'https://museo.hn/3d/sala-12', 1),
('Sala Temporales Sur', 'Muestras de artistas emergentes', 200, 'https://museo.hn/3d/sala-13', 1),
('Cámara de Conservación', 'Vista interactiva de procesos de restauración', 80, 'https://museo.hn/3d/sala-14', 1),
('Archivo Histórico', 'Documentos y bocetos originales', 90, 'https://museo.hn/3d/sala-15', 1);

-- 4.13. Catálogo: tipo_evento (15 registros)
INSERT INTO dbo.tipo_evento (nombre, descripcion) VALUES
('Inauguración', 'Apertura oficial de exposición'), ('Conferencia Magistral', 'Charla con expertos internacionales'),
('Taller Práctico', 'Sesión interactiva de técnicas artísticas'), ('Visita Guiada en Vivo', 'Recorrido comentado en tiempo real'),
('Mesa Redonda', 'Debate entre curadores y críticos'), ('Encuentro con el Artista', 'Entrevista y preguntas con autores'),
('Seminario de Historia del Arte', 'Ciclo académico especializado'), ('Subasta Benéfica', 'Puja virtual de obras seleccionadas'),
('Concierto de Cámara', 'Presentación musical ambientada en sala'), ('Lanzamiento de Catálogo', 'Presentación editorial digital'),
('Taller Infantil', 'Actividades educativas para niños'), ('Simposio de Restauración', 'Ponencias técnicas de conservación'),
('Proyección Documental', 'Cine arte y biografías'), ('Noche de Museos', 'Jornada extendida con eventos simultáneos'),
('Clausura de Exposición', 'Cierre especial de muestra temporal');

-- 4.14. Catálogo: tipo_entrada (15 registros)
INSERT INTO dbo.tipo_entrada (id_moneda, nombre, descripcion, precio) VALUES
(1, 'General Nacional', 'Acceso general a exposiciones activas', 100.00),
(1, 'Estudiante Nacional', 'Tarifa preferencial con carné', 50.00),
(1, 'Tercera Edad Nacional', 'Acceso con descuento ley', 40.00),
(1, 'Gratuita Domingo', 'Acceso libre dominical', 0.00),
(2, 'General Internacional', 'Acceso estándar visitantes extranjeros', 10.00),
(2, 'Estudiante Internacional', 'Tarifa estudiante extranjero', 5.00),
(2, 'Pase VIP All-Access', 'Incluye eventos y recorridos exclusivos', 25.00),
(1, 'Pase Familiar (4 personas)', 'Acceso grupal familiar', 300.00),
(1, 'Grupo Escolar', 'Tarifa por alumno en grupo', 25.00),
(2, 'Investigador Acreditado', 'Acceso a archivo y fichas técnicas', 15.00),
(1, 'Taller Práctico', 'Entrada con derecho a taller en vivo', 250.00),
(2, 'Conferencia Magistral', 'Acceso a ponencia internacional', 8.00),
(1, 'Noche de Museos', 'Pase especial jornada nocturna', 150.00),
(2, 'Membresía Mensual', 'Acceso ilimitado durante 30 días', 30.00),
(1, 'Cortesía Institucional', 'Pase emitido por invitación', 0.00);

-- 4.15. Catálogo: estado_orden (15 registros)
INSERT INTO dbo.estado_orden (nombre, descripcion) VALUES
('Pendiente de Pago', 'Orden creada esperando confirmación de pago'),
('Pago en Verificación', 'Comprobante recibido en revisión'),
('Pagada', 'Pago aprobado correctamente'),
('En Preparación', 'Embalaje especializado y certificación'),
('Certificado Emitido', 'Certificado de autenticidad generado'),
('Lista para Envío', 'Empacada y asignada a courier'),
('Enviada', 'En tránsito hacia el destino'),
('En Aduana', 'Proceso de revisión aduanera internacional'),
('En Reparto Local', 'En ruta final de entrega'),
('Entregada', 'Recibida conforme por el cliente'),
('Completada', 'Orden cerrada sin incidencias'),
('Cancelada por Cliente', 'Anulada antes del envío'),
('Cancelada por Falta de Pago', 'Expiró el tiempo de pago'),
('Reembolsada', 'Monto devuelto al cliente'),
('En Disputa', 'Revisión por reclamo de transporte');
GO

-- ============================================================================
-- 4.16. CARGA DE 60 PERSONAS (PARA NUTRIR 35 USUARIOS, 30 ARTISTAS, 15 CURADORES, 30 CLIENTES)
-- ============================================================================
DECLARE @i INT = 1;
WHILE @i <= 60
BEGIN
    INSERT INTO dbo.persona (id_ciudad, pnombre, snombre, papellido, sapellido, identificacion, fecha_nacimiento, telefono, direccion)
    VALUES (
        ((@i - 1) % 15) + 1,
        'Nombre' + CAST(@i AS VARCHAR(5)),
        CASE WHEN @i % 5 = 0 THEN NULL ELSE 'Segundo' + CAST(@i AS VARCHAR(5)) END,
        'Apellido' + CAST(@i AS VARCHAR(5)),
        CASE WHEN @i % 7 = 0 THEN NULL ELSE 'Materno' + CAST(@i AS VARCHAR(5)) END,
        '0801-1990-' + RIGHT('00000' + CAST(@i AS VARCHAR(5)), 5),
        DATEADD(DAY, -(@i * 120 + 7000), CAST(GETDATE() AS DATE)),
        '+504 9900-' + RIGHT('0000' + CAST(@i AS VARCHAR(4)), 4),
        'Colonia Residencial Bloque ' + CAST(@i AS VARCHAR(5))
    );
    SET @i = @i + 1;
END;
GO

-- 4.17. usuario (35 registros)
DECLARE @u INT = 1;
WHILE @u <= 35
BEGIN
    INSERT INTO dbo.usuario (id_persona, id_rol, correo, contrasena_hash, activo)
    VALUES (
        @u,
        ((@u - 1) % 15) + 1,
        'usuario' + CAST(@u AS VARCHAR(5)) + '@museovirtual.hn',
        'HASH_SHA256_PASS_' + CAST(@u AS VARCHAR(5)),
        1
    );
    SET @u = @u + 1;
END;
GO

-- 4.18. bitacora_acceso (35 registros)
DECLARE @b INT = 1;
WHILE @b <= 35
BEGIN
    INSERT INTO dbo.bitacora_acceso (id_usuario, ip_origen, accion, exitoso)
    VALUES (
        @b,
        '192.168.1.' + CAST(@b + 10 AS VARCHAR(5)),
        CASE WHEN @b % 2 = 0 THEN 'LOGIN' ELSE 'CONSULTA_OBRA' END,
        1
    );
    SET @b = @b + 1;
END;
GO

-- 4.19. artista (30 registros: personas 16 a 45)
DECLARE @a INT = 16;
WHILE @a <= 45
BEGIN
    INSERT INTO dbo.artista (id_persona, id_pais, nombre_artistico, biografia, sitio_web)
    VALUES (
        @a,
        ((@a - 1) % 15) + 1,
        'Maestro Artista ' + CAST(@a AS VARCHAR(5)),
        'Trayectoria artística destacada en exposiciones internacionales #' + CAST(@a AS VARCHAR(5)),
        'https://arte-portafolio' + CAST(@a AS VARCHAR(5)) + '.com'
    );
    SET @a = @a + 1;
END;
GO

-- 4.20. curador (15 registros: personas 46 a 60)
DECLARE @c INT = 46;
WHILE @c <= 60
BEGIN
    INSERT INTO dbo.curador (id_persona, especialidad, biografia)
    VALUES (
        @c,
        'Curaduría en Arte Contemporáneo y Clásico #' + CAST(@c AS VARCHAR(5)),
        'Especialista con amplia experiencia museográfica.'
    );
    SET @c = @c + 1;
END;
GO

-- 4.21. cliente (30 registros: personas 1 a 30)
DECLARE @cl INT = 1;
WHILE @cl <= 30
BEGIN
    INSERT INTO dbo.cliente (id_persona, rtn, direccion_envio)
    VALUES (
        @cl,
        '080119900' + RIGHT('00000' + CAST(@cl AS VARCHAR(5)), 5),
        'Avenida Principal, Casa #' + CAST(@cl AS VARCHAR(5)) + ', Tegucigalpa'
    );
    SET @cl = @cl + 1;
END;
GO

-- 4.22. obra (35 registros)
DECLARE @ob INT = 1;
WHILE @ob <= 35
BEGIN
    INSERT INTO dbo.obra (id_categoria, id_tecnica, id_movimiento, id_estado_conservacion, id_moneda, titulo, anio_creacion, descripcion, alto_cm, ancho_cm, profundidad_cm, en_venta, precio, fecha_ingreso, activa)
    VALUES (
        ((@ob - 1) % 15) + 1,
        ((@ob - 1) % 15) + 1,
        ((@ob - 1) % 15) + 1,
        ((@ob - 1) % 15) + 1,
        1,
        'Obra Maestra #' + CAST(@ob AS VARCHAR(5)),
        1950 + @ob,
        'Descripción detallada de la pieza artística #' + CAST(@ob AS VARCHAR(5)),
        80.50 + @ob,
        60.00 + @ob,
        CASE WHEN @ob % 3 = 0 THEN 25.00 ELSE NULL END,
        1,
        5000.00 + (@ob * 350),
        DATEADD(DAY, -(@ob * 10), CAST(GETDATE() AS DATE)),
        1
    );
    SET @ob = @ob + 1;
END;
GO

-- 4.23. obra_artista (35 registros)
DECLARE @oa INT = 1;
WHILE @oa <= 35
BEGIN
    INSERT INTO dbo.obra_artista (id_obra, id_artista, rol_artista)
    VALUES (@oa, ((@oa - 1) % 30) + 16, 'Autor');
    SET @oa = @oa + 1;
END;
GO

-- 4.24. obra_multimedia (35 registros)
DECLARE @om INT = 1;
WHILE @om <= 35
BEGIN
    INSERT INTO dbo.obra_multimedia (id_obra, tipo, url, descripcion, es_principal)
    VALUES (
        @om,
        CASE (@om % 4) WHEN 0 THEN 'imagen' WHEN 1 THEN 'modelo_3d' WHEN 2 THEN 'video' ELSE 'audio' END,
        'https://cdn.museovirtual.hn/obras/media_' + CAST(@om AS VARCHAR(5)) + '.dat',
        'Recurso multimedia oficial de obra #' + CAST(@om AS VARCHAR(5)),
        1
    );
    SET @om = @om + 1;
END;
GO

-- 4.25. historial_conservacion (30 registros)
DECLARE @hc INT = 1;
WHILE @hc <= 30
BEGIN
    INSERT INTO dbo.historial_conservacion (id_obra, id_estado_conservacion, fecha, responsable, observaciones)
    VALUES (
        @hc,
        ((@hc - 1) % 15) + 1,
        DATEADD(DAY, -(@hc * 5), CAST(GETDATE() AS DATE)),
        'Lic. Restaurador ' + CAST(((@hc - 1) % 5) + 1 AS VARCHAR(5)),
        'Inspección periódica preventiva sin anomalías críticas.'
    );
    SET @hc = @hc + 1;
END;
GO

-- 4.26. exposicion (15 registros)
DECLARE @ex INT = 1;
WHILE @ex <= 15
BEGIN
    INSERT INTO dbo.exposicion (id_sala, titulo, descripcion, fecha_inicio, fecha_fin, es_permanente, estado)
    VALUES (
        @ex,
        'Exposición Temática #' + CAST(@ex AS VARCHAR(5)),
        'Muestra curatorial especializada #' + CAST(@ex AS VARCHAR(5)),
        DATEADD(MONTH, -2, CAST(GETDATE() AS DATE)),
        DATEADD(MONTH, 4, CAST(GETDATE() AS DATE)),
        CASE WHEN @ex <= 5 THEN 1 ELSE 0 END,
        'activa'
    );
    SET @ex = @ex + 1;
END;
GO

-- 4.27. exposicion_curador (15 registros)
DECLARE @ec INT = 1;
WHILE @ec <= 15
BEGIN
    INSERT INTO dbo.exposicion_curador (id_exposicion, id_curador)
    VALUES (@ec, @ec + 45);
    SET @ec = @ec + 1;
END;
GO

-- 4.28. exposicion_obra (30 registros)
DECLARE @eo INT = 1;
WHILE @eo <= 30
BEGIN
    INSERT INTO dbo.exposicion_obra (id_exposicion, id_obra, orden_visualizacion, posicion_en_sala)
    VALUES (
        ((@eo - 1) % 15) + 1,
        @eo,
        @eo,
        'Pared Sector ' + CAST(((@eo - 1) % 4) + 1 AS VARCHAR(5))
    );
    SET @eo = @eo + 1;
END;
GO

-- 4.29. recorrido_virtual (15 registros)
DECLARE @rv INT = 1;
WHILE @rv <= 15
BEGIN
    INSERT INTO dbo.recorrido_virtual (id_exposicion, nombre, descripcion, duracion_estimada_min)
    VALUES (@rv, 'Recorrido Guiado #' + CAST(@rv AS VARCHAR(5)), 'Tour narrado paso a paso', 45);
    SET @rv = @rv + 1;
END;
GO

-- 4.30. recorrido_parada (30 registros)
DECLARE @rp INT = 1;
WHILE @rp <= 30
BEGIN
    INSERT INTO dbo.recorrido_parada (id_recorrido, id_obra, orden, narracion)
    VALUES (
        ((@rp - 1) % 15) + 1,
        @rp,
        (@rp - 1) / 15 + 1,
        'Narración histórica y técnica de la parada #' + CAST(@rp AS VARCHAR(5))
    );
    SET @rp = @rp + 1;
END;
GO

-- 4.31. evento (15 registros)
DECLARE @ev INT = 1;
WHILE @ev <= 15
BEGIN
    INSERT INTO dbo.evento (id_tipo_evento, id_exposicion, titulo, descripcion, fecha_hora_inicio, fecha_hora_fin, cupo_maximo, enlace_transmision, estado)
    VALUES (
        @ev,
        @ev,
        'Evento Cultural Virtual #' + CAST(@ev AS VARCHAR(5)),
        'Actividad académica abierta al público',
        DATEADD(DAY, @ev, SYSDATETIME()),
        DATEADD(HOUR, 2, DATEADD(DAY, @ev, SYSDATETIME())),
        200,
        'https://stream.museovirtual.hn/live/' + CAST(@ev AS VARCHAR(5)),
        'programado'
    );
    SET @ev = @ev + 1;
END;
GO

-- 4.32. entrada (35 registros)
DECLARE @en INT = 1;
WHILE @en <= 35
BEGIN
    INSERT INTO dbo.entrada (id_tipo_entrada, id_usuario, id_exposicion, id_evento, codigo_qr, fecha_visita, usada)
    VALUES (
        ((@en - 1) % 15) + 1,
        @en,
        ((@en - 1) % 15) + 1,
        ((@en - 1) % 15) + 1,
        'QR-MUSEO-2026-' + RIGHT('000000' + CAST(@en AS VARCHAR(6)), 6),
        DATEADD(DAY, @en % 10, CAST(GETDATE() AS DATE)),
        CASE WHEN @en % 2 = 0 THEN 1 ELSE 0 END
    );
    SET @en = @en + 1;
END;
GO

-- 4.33. orden_compra (30 registros)
DECLARE @oc INT = 1;
WHILE @oc <= 30
BEGIN
    DECLARE @sub DECIMAL(12,2) = 5000.00 + (@oc * 350);
    DECLARE @imp DECIMAL(12,2) = ROUND(@sub * 0.15, 2);
    INSERT INTO dbo.orden_compra (id_cliente, id_estado_orden, id_moneda, subtotal, impuesto, total)
    VALUES (
        @oc,
        3,
        1,
        @sub,
        @imp,
        @sub + @imp
    );
    SET @oc = @oc + 1;
END;
GO

-- 4.34. detalle_orden (30 registros)
DECLARE @do INT = 1;
WHILE @do <= 30
BEGIN
    INSERT INTO dbo.detalle_orden (id_orden, id_obra, cantidad, precio_unitario)
    VALUES (@do, @do, 1, 5000.00 + (@do * 350));
    SET @do = @do + 1;
END;
GO

-- 4.35. pago (30 registros)
DECLARE @pg INT = 1;
WHILE @pg <= 30
BEGIN
    DECLARE @monto_orden DECIMAL(12,2) = (SELECT total FROM dbo.orden_compra WHERE id_orden = @pg);
    INSERT INTO dbo.pago (id_orden, id_metodo_pago, monto, referencia, estado)
    VALUES (
        @pg,
        ((@pg - 1) % 14) + 1,
        @monto_orden,
        'REF-TXN-' + RIGHT('000000' + CAST(@pg AS VARCHAR(6)), 6),
        'aprobado'
    );
    SET @pg = @pg + 1;
END;
GO

-- 4.36. favorito (30 registros)
DECLARE @fv INT = 1;
WHILE @fv <= 30
BEGIN
    INSERT INTO dbo.favorito (id_usuario, id_obra)
    VALUES (@fv, @fv);
    SET @fv = @fv + 1;
END;
GO

-- 4.37. valoracion (30 registros)
DECLARE @vl INT = 1;
WHILE @vl <= 30
BEGIN
    INSERT INTO dbo.valoracion (id_usuario, id_obra, puntuacion, comentario)
    VALUES (
        @vl,
        @vl,
        ((@vl - 1) % 5) + 1,
        'Excelente calidad visual y curaduría de la obra #' + CAST(@vl AS VARCHAR(5))
    );
    SET @vl = @vl + 1;
END;
GO

PRINT 'Base de datos museo_virtual creada, comentada, con vistas y poblada exitosamente.';
GO
