using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnrolApp.Persistence.Migrations
{
    public partial class d : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AS_TrackingSolicitud",
                schema: "dbo");

            migrationBuilder.AlterColumn<Guid>(
                name: "cargoPadreId",
                schema: "dbo",
                table: "WF_Cargo",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "estado",
                schema: "dbo",
                table: "OG_Departamento",
                type: "varchar(1)",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1)",
                oldMaxLength: 1)
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AddColumn<string>(
                name: "codigoHomologacion",
                schema: "dbo",
                table: "OG_Departamento",
                type: "varchar",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AddColumn<string>(
                name: "nombreHomologacion",
                schema: "dbo",
                table: "OG_Departamento",
                type: "varchar",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.CreateTable(
                name: "AS_Localidad",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idEmpresa = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    latitud = table.Column<double>(type: "float", maxLength: 8, nullable: false),
                    longitud = table.Column<double>(type: "float", maxLength: 8, nullable: false),
                    radio = table.Column<double>(type: "float", maxLength: 8, nullable: false),
                    descripcion = table.Column<string>(type: "varchar", nullable: true),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_Localidad", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AS_LocalidadColaborador",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idLocalidad = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idColaborador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    estado = table.Column<string>(type: "varchar", nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_LocalidadColaborador", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AS_SolicitudVacacion",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codOrganizacion = table.Column<int>(type: "int", nullable: true),
                    idEstadoSolicitud = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    numeroSolicitud = table.Column<int>(type: "int", nullable: false),
                    idSolicitante = table.Column<int>(type: "int", nullable: false),
                    idBeneficiario = table.Column<int>(type: "int", nullable: false),
                    identificacionEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaDesde = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaHasta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    cantidadDias = table.Column<int>(type: "int", nullable: false),
                    codigoEmpleadoReemplazo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    aplicaDescuento = table.Column<bool>(type: "bit", nullable: false),
                    periodoOrigen = table.Column<int>(type: "int", nullable: true),
                    diasVacacionesOrigen = table.Column<int>(type: "int", nullable: true),
                    diasTomadosOrigen = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_SolicitudVacacion", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MK_Categoria",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "varchar", nullable: true),
                    descripcion = table.Column<string>(type: "varchar", nullable: true),
                    estado = table.Column<string>(type: "varchar", nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MK_Categoria", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MK_TipoContenido",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "varchar", nullable: true),
                    descripcion = table.Column<string>(type: "varchar", nullable: true),
                    estado = table.Column<string>(type: "varchar", nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MK_TipoContenido", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SG_Cargo",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    departamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nombre = table.Column<string>(type: "varchar", nullable: true),
                    descripcion = table.Column<string>(type: "varchar", nullable: true),
                    codigoHomologacion = table.Column<string>(type: "varchar", nullable: true),
                    estado = table.Column<string>(type: "varchar", nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SG_Cargo", x => x.id);
                    table.ForeignKey(
                        name: "FK_SG_Cargo_OG_Departamento_departamentoId",
                        column: x => x.departamentoId,
                        principalSchema: "dbo",
                        principalTable: "OG_Departamento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SG_CargoEjes",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    identificacion = table.Column<string>(type: "varchar", nullable: true),
                    idUdn = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idCargo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    estado = table.Column<string>(type: "varchar", nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SG_CargoEjes", x => x.id);
                    table.ForeignKey(
                        name: "FK_SG_CargoEjes_OG_Empresa_idUdn",
                        column: x => x.idUdn,
                        principalSchema: "dbo",
                        principalTable: "OG_Empresa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SG_CargoEjes_WF_Cargo_idCargo",
                        column: x => x.idCargo,
                        principalSchema: "dbo",
                        principalTable: "WF_Cargo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SG_Rol",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    canalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "varchar", nullable: true),
                    nombre = table.Column<string>(type: "varchar", nullable: true),
                    descripcion = table.Column<string>(type: "varchar", nullable: true),
                    rolPadreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    estado = table.Column<string>(type: "varchar", nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SG_Rol", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Turno",
                columns: table => new
                {
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idTurno = table.Column<int>(type: "int", nullable: false),
                    codigoTurno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    descripcionTurno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tipoTurno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    turnoEntrada = table.Column<DateTime>(type: "datetime2", nullable: true),
                    turnoSalida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    receso = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turno", x => x.fecha);
                });

            migrationBuilder.CreateTable(
                name: "V_COLABORADORES_CONVIVENCIA",
                schema: "dbo",
                columns: table => new
                {
                    DFiIngreso = table.Column<DateTime>(type: "datetime", nullable: false),
                    codUdn = table.Column<string>(type: "varchar", nullable: true),
                    desUdn = table.Column<string>(type: "varchar", nullable: true),
                    codArea = table.Column<string>(type: "varchar", nullable: true),
                    desArea = table.Column<string>(type: "varchar", nullable: true),
                    codCentroCosto = table.Column<string>(type: "varchar", nullable: true),
                    desCentroCosto = table.Column<string>(type: "varchar", nullable: true),
                    codSubcentroCosto = table.Column<string>(type: "varchar", nullable: true),
                    desSubcentroCosto = table.Column<string>(type: "varchar", nullable: true),
                    identificacion = table.Column<string>(type: "varchar", nullable: true),
                    Empleado = table.Column<string>(type: "varchar", nullable: true),
                    codigoBiometrico = table.Column<string>(type: "varchar", nullable: true),
                    codCargo = table.Column<string>(type: "varchar", nullable: true),
                    desCargo = table.Column<string>(type: "varchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V_COLABORADORES_CONVIVENCIA", x => x.DFiIngreso);
                });

            migrationBuilder.CreateTable(
                name: "WF_TipoJustificacion",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false, defaultValue: "A"),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WF_TipoJustificacion", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "WF_TipoPermiso",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false, defaultValue: "A")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WF_TipoPermiso", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MK_Contenido",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tipoContenidoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    portadaUrl = table.Column<string>(type: "nvarchar", nullable: true),
                    posterUrl = table.Column<string>(type: "nvarchar", nullable: true),
                    contenidoUrl = table.Column<string>(type: "nvarchar", nullable: true),
                    nombreCorto = table.Column<string>(type: "varchar", nullable: true),
                    nombreLargo = table.Column<string>(type: "varchar", nullable: true),
                    orden = table.Column<int>(type: "int", nullable: false),
                    fechaPublicacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaCaducidad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaVigenciaPortada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    descripcion = table.Column<string>(type: "varchar", nullable: true),
                    comentario = table.Column<string>(type: "varchar", nullable: true),
                    estado = table.Column<string>(type: "varchar", nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MK_Contenido", x => x.id);
                    table.ForeignKey(
                        name: "FK_MK_Contenido_MK_TipoContenido_tipoContenidoId",
                        column: x => x.tipoContenidoId,
                        principalSchema: "dbo",
                        principalTable: "MK_TipoContenido",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SG_RolCargo",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    rolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    cargoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    estado = table.Column<string>(type: "varchar", nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SG_RolCargo", x => x.id);
                    table.ForeignKey(
                        name: "FK_SG_RolCargo_SG_Cargo_cargoId",
                        column: x => x.cargoId,
                        principalSchema: "dbo",
                        principalTable: "SG_Cargo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SG_RolCargo_SG_Rol_rolId",
                        column: x => x.rolId,
                        principalSchema: "dbo",
                        principalTable: "SG_Rol",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AS_TurnoColaborador",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idTurno = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idColaborador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fechaAsginacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    estado = table.Column<string>(type: "varchar", nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TurnoFecha = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_TurnoColaborador", x => x.id);
                    table.ForeignKey(
                        name: "FK_AS_TurnoColaborador_Turno_TurnoFecha",
                        column: x => x.TurnoFecha,
                        principalTable: "Turno",
                        principalColumn: "fecha");
                });

            migrationBuilder.CreateTable(
                name: "AS_SolicitudJustificacion",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codOrganizacion = table.Column<int>(type: "int", nullable: true),
                    idTipoJustificacion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idEstadoSolicitud = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    identBeneficiario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    identificacionEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    aplicaDescuento = table.Column<bool>(type: "bit", nullable: false),
                    idMarcacion = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    idMarcacionG = table.Column<int>(type: "int", nullable: true),
                    comentarios = table.Column<string>(type: "varchar(255)", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_SolicitudJustificacion", x => x.id);
                    table.ForeignKey(
                        name: "FK_AS_SolicitudJustificacion_WF_TipoJustificacion_idTipoJustificacion",
                        column: x => x.idTipoJustificacion,
                        principalSchema: "dbo",
                        principalTable: "WF_TipoJustificacion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AS_SolicitudPermiso",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codOrganizacion = table.Column<int>(type: "int", nullable: true),
                    idTipoPermiso = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idEstadoSolicitud = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    numeroSolicitud = table.Column<int>(name: "numeroSolicitud ", type: "int", nullable: false),
                    idSolicitante = table.Column<int>(name: "idSolicitante ", type: "int", nullable: false),
                    idBeneficiario = table.Column<int>(name: "idBeneficiario ", type: "int", nullable: false),
                    identificacionEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    porHoras = table.Column<bool>(name: "porHoras ", type: "bit", nullable: true),
                    fechaDesde = table.Column<DateTime>(name: "fechaDesde ", type: "datetime2", nullable: false),
                    horaInicio = table.Column<string>(name: "horaInicio ", type: "nvarchar(max)", nullable: true),
                    fechaHasta = table.Column<DateTime>(name: "fechaHasta ", type: "datetime2", nullable: false),
                    horaFin = table.Column<string>(name: "horaFin ", type: "nvarchar(max)", nullable: true),
                    cantidadHoras = table.Column<DateTime>(name: "cantidadHoras ", type: "datetime2", nullable: true),
                    cantidadDias = table.Column<int>(name: "cantidadDias ", type: "int", nullable: true),
                    observacion = table.Column<string>(name: "observacion ", type: "nvarchar(max)", nullable: true),
                    fechaCreacion = table.Column<DateTime>(name: "fechaCreacion ", type: "datetime2", nullable: false),
                    aplicaDescuento = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_SolicitudPermiso", x => x.id);
                    table.ForeignKey(
                        name: "FK_AS_SolicitudPermiso_WF_TipoPermiso_idTipoPermiso",
                        column: x => x.idTipoPermiso,
                        principalSchema: "dbo",
                        principalTable: "WF_TipoPermiso",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MK_ContenidoCategoria",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    contenidoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    categoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    estado = table.Column<string>(type: "varchar", nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MK_ContenidoCategoria", x => x.id);
                    table.ForeignKey(
                        name: "FK_MK_ContenidoCategoria_MK_Categoria_categoriaId",
                        column: x => x.categoriaId,
                        principalSchema: "dbo",
                        principalTable: "MK_Categoria",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MK_ContenidoCategoria_MK_Contenido_contenidoId",
                        column: x => x.contenidoId,
                        principalSchema: "dbo",
                        principalTable: "MK_Contenido",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MK_RolContenido",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    rolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    contenidoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    estado = table.Column<string>(type: "varchar", nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MK_RolContenido", x => x.id);
                    table.ForeignKey(
                        name: "FK_MK_RolContenido_MK_Contenido_contenidoId",
                        column: x => x.contenidoId,
                        principalSchema: "dbo",
                        principalTable: "MK_Contenido",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AS_SolicitudJustificacion_idTipoJustificacion",
                schema: "dbo",
                table: "AS_SolicitudJustificacion",
                column: "idTipoJustificacion");

            migrationBuilder.CreateIndex(
                name: "IX_AS_SolicitudPermiso_idTipoPermiso",
                schema: "dbo",
                table: "AS_SolicitudPermiso",
                column: "idTipoPermiso");

            migrationBuilder.CreateIndex(
                name: "IX_AS_TurnoColaborador_TurnoFecha",
                schema: "dbo",
                table: "AS_TurnoColaborador",
                column: "TurnoFecha");

            migrationBuilder.CreateIndex(
                name: "IX_MK_Contenido_tipoContenidoId",
                schema: "dbo",
                table: "MK_Contenido",
                column: "tipoContenidoId");

            migrationBuilder.CreateIndex(
                name: "IX_MK_ContenidoCategoria_categoriaId",
                schema: "dbo",
                table: "MK_ContenidoCategoria",
                column: "categoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_MK_ContenidoCategoria_contenidoId",
                schema: "dbo",
                table: "MK_ContenidoCategoria",
                column: "contenidoId");

            migrationBuilder.CreateIndex(
                name: "IX_MK_RolContenido_contenidoId",
                schema: "dbo",
                table: "MK_RolContenido",
                column: "contenidoId");

            migrationBuilder.CreateIndex(
                name: "IX_SG_Cargo_departamentoId",
                schema: "dbo",
                table: "SG_Cargo",
                column: "departamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_SG_CargoEjes_idCargo",
                schema: "dbo",
                table: "SG_CargoEjes",
                column: "idCargo");

            migrationBuilder.CreateIndex(
                name: "IX_SG_CargoEjes_idUdn",
                schema: "dbo",
                table: "SG_CargoEjes",
                column: "idUdn");

            migrationBuilder.CreateIndex(
                name: "IX_SG_RolCargo_cargoId",
                schema: "dbo",
                table: "SG_RolCargo",
                column: "cargoId");

            migrationBuilder.CreateIndex(
                name: "IX_SG_RolCargo_rolId",
                schema: "dbo",
                table: "SG_RolCargo",
                column: "rolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AS_Localidad",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AS_LocalidadColaborador",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AS_SolicitudJustificacion",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AS_SolicitudPermiso",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AS_SolicitudVacacion",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AS_TurnoColaborador",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MK_ContenidoCategoria",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MK_RolContenido",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SG_CargoEjes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SG_RolCargo",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "V_COLABORADORES_CONVIVENCIA",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "WF_TipoJustificacion",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "WF_TipoPermiso",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Turno");

            migrationBuilder.DropTable(
                name: "MK_Categoria",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MK_Contenido",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SG_Cargo",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SG_Rol",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MK_TipoContenido",
                schema: "dbo");

            migrationBuilder.DropColumn(
                name: "codigoHomologacion",
                schema: "dbo",
                table: "OG_Departamento");

            migrationBuilder.DropColumn(
                name: "nombreHomologacion",
                schema: "dbo",
                table: "OG_Departamento");

            migrationBuilder.AlterColumn<Guid>(
                name: "cargoPadreId",
                schema: "dbo",
                table: "WF_Cargo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "estado",
                schema: "dbo",
                table: "OG_Departamento",
                type: "varchar(1)",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1)",
                oldMaxLength: 1)
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.CreateTable(
                name: "AS_TrackingSolicitud",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    solicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tipoFeatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    identificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    colaboradorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    estadoSolicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_TrackingSolicitud", x => x.id);
                });
        }
    }
}
