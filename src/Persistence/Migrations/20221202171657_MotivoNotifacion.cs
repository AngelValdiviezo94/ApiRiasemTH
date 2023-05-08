using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnrolApp.Persistence.Migrations
{
    public partial class MotivoNotifacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "AS_TrackingSolicitud",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tipoFeatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    solicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    identificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    colaboradorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    estadoSolicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AS_TrackingSolicitud", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CL_TipoRelacion",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    descripcion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CL_TipoRelacion", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CL_TipoSuscriptor",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    descripcion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CL_TipoSuscriptor", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "NT_Clasificacion",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    descripcion = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    uriImage = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    orden = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false, defaultValue: "A")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NT_Clasificacion", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "NT_Plantilla",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    mensaje = table.Column<string>(type: "varchar(800)", maxLength: 800, nullable: false),
                    resumen = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    relevante = table.Column<bool>(type: "bit", maxLength: 1, nullable: false, defaultValue: false),
                    requiereAccion = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    uriImage = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false, defaultValue: "A"),
                    MensajeHtml = table.Column<string>(type: "varchar(800)", maxLength: 800, nullable: true),
                    requiereNivelDetalle = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    requiereEvalVariables = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NT_Plantilla", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "OG_GrupoEmpresarial",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    logo = table.Column<byte[]>(type: "varbinary", nullable: true),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OG_GrupoEmpresarial", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "WF_Feature",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    canalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    nombre = table.Column<string>(type: "varchar", nullable: true),
                    descripcion = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    orden = table.Column<int>(type: "int", nullable: true),
                    estado = table.Column<string>(type: "varchar", nullable: false, defaultValue: "A"),
                    usuarioCreacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WF_Feature", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "OG_Empresa",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    grupoEmpresarialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    ruc = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false),
                    nombreComercial = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    razonSocial = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    logo = table.Column<byte[]>(type: "varbinary", nullable: true),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OG_Empresa", x => x.id);
                    table.ForeignKey(
                        name: "FK_OG_Empresa_OG_GrupoEmpresarial_grupoEmpresarialId",
                        column: x => x.grupoEmpresarialId,
                        principalSchema: "dbo",
                        principalTable: "OG_GrupoEmpresarial",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NT_Evento",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    featureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    descripcion = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false, defaultValue: "A")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NT_Evento", x => x.id);
                    table.ForeignKey(
                        name: "FK_NT_Evento_WF_Feature_featureId",
                        column: x => x.featureId,
                        principalSchema: "dbo",
                        principalTable: "WF_Feature",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OG_Area",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    empresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OG_Area", x => x.id);
                    table.ForeignKey(
                        name: "FK_OG_Area_OG_Empresa_empresaId",
                        column: x => x.empresaId,
                        principalSchema: "dbo",
                        principalTable: "OG_Empresa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NT_EventoDifusion",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    eventoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    clasificacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    plantillaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    descripcion = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    uriImage = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false, defaultValue: "A")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NT_EventoDifusion", x => x.id);
                    table.ForeignKey(
                        name: "FK_NT_EventoDifusion_NT_Clasificacion_clasificacionId",
                        column: x => x.clasificacionId,
                        principalSchema: "dbo",
                        principalTable: "NT_Clasificacion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NT_EventoDifusion_NT_Evento_eventoId",
                        column: x => x.eventoId,
                        principalSchema: "dbo",
                        principalTable: "NT_Evento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NT_EventoDifusion_NT_Plantilla_plantillaId",
                        column: x => x.plantillaId,
                        principalSchema: "dbo",
                        principalTable: "NT_Plantilla",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OG_Departamento",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    areaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OG_Departamento", x => x.id);
                    table.ForeignKey(
                        name: "FK_OG_Departamento_OG_Area_areaId",
                        column: x => x.areaId,
                        principalSchema: "dbo",
                        principalTable: "OG_Area",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CL_Prospecto",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tipoRelacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tipoSuscriptorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    departamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tipoIdentificacion = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false),
                    identificacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    nombres = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    apellidos = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    alias = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    celular = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    tipoIdentificacionFamiliar = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    indentificacionFamiliar = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: true),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CL_Prospecto", x => x.id);
                    table.ForeignKey(
                        name: "FK_CL_Prospecto_CL_TipoRelacion_tipoRelacionId",
                        column: x => x.tipoRelacionId,
                        principalSchema: "dbo",
                        principalTable: "CL_TipoRelacion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CL_Prospecto_CL_TipoSuscriptor_tipoSuscriptorId",
                        column: x => x.tipoSuscriptorId,
                        principalSchema: "dbo",
                        principalTable: "CL_TipoSuscriptor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CL_Prospecto_OG_Departamento_departamentoId",
                        column: x => x.departamentoId,
                        principalSchema: "dbo",
                        principalTable: "OG_Departamento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WF_Cargo",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    departamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nombre = table.Column<string>(type: "varchar", nullable: true),
                    descripcion = table.Column<string>(type: "varchar", nullable: true),
                    cargoPadreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    estado = table.Column<string>(type: "varchar", nullable: true),
                    usuarioCreacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioModificacion = table.Column<string>(type: "varchar", nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WF_Cargo", x => x.id);
                    table.ForeignKey(
                        name: "FK_WF_Cargo_OG_Departamento_departamentoId",
                        column: x => x.departamentoId,
                        principalSchema: "dbo",
                        principalTable: "OG_Departamento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CL_Cliente",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigoIntegracion = table.Column<string>(type: "varchar", nullable: true),
                    codigoConvivencia = table.Column<string>(type: "varchar", nullable: true),
                    tipoIdentificacion = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false),
                    identificacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    nombres = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    apellidos = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    alias = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    latitud = table.Column<double>(type: "float", nullable: false),
                    longitud = table.Column<double>(type: "float", nullable: false),
                    direccion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    celular = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    tipoIdentificacionFamiliar = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    indentificacionFamiliar = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    correo = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    fechaNacimiento = table.Column<DateTime>(type: "datetime", nullable: true),
                    genero = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "NOW()"),
                    servicioActivo = table.Column<bool>(type: "bit", nullable: false),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false),
                    dispositivoId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    cargoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    clientePadreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    nombreUsuario = table.Column<string>(type: "varchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CL_Cliente", x => x.id);
                    table.ForeignKey(
                        name: "FK_CL_Cliente_WF_Cargo_cargoId",
                        column: x => x.cargoId,
                        principalSchema: "dbo",
                        principalTable: "WF_Cargo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NT_BitacoraNotificacion",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    eventoDifusionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    clienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    solicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    referenciaClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    mensaje = table.Column<string>(type: "varchar(800)", maxLength: 800, nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    estadoLeido = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false, defaultValue: "N"),
                    estado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false, defaultValue: "A"),
                    tipoSolicitud = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    mensajeHtml = table.Column<string>(type: "varchar(800)", maxLength: 800, nullable: true),
                    identificacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    resumen = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NT_BitacoraNotificacion", x => x.id);
                    table.ForeignKey(
                        name: "FK_NT_BitacoraNotificacion_CL_Cliente_clienteId",
                        column: x => x.clienteId,
                        principalSchema: "dbo",
                        principalTable: "CL_Cliente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NT_BitacoraNotificacion_NT_EventoDifusion_eventoDifusionId",
                        column: x => x.eventoDifusionId,
                        principalSchema: "dbo",
                        principalTable: "NT_EventoDifusion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NT_NotificacionMotivo",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tipoFeatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tipoMotivoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    aprobadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioCreacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuarioModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estado = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NT_NotificacionMotivo", x => x.id);
                    table.ForeignKey(
                        name: "FK_NT_NotificacionMotivo_CL_Cliente_aprobadorId",
                        column: x => x.aprobadorId,
                        principalSchema: "dbo",
                        principalTable: "CL_Cliente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NT_NotificacionMotivo_WF_Feature_tipoFeatureId",
                        column: x => x.tipoFeatureId,
                        principalSchema: "dbo",
                        principalTable: "WF_Feature",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CL_Cliente_cargoId",
                schema: "dbo",
                table: "CL_Cliente",
                column: "cargoId");

            migrationBuilder.CreateIndex(
                name: "IX_CL_Prospecto_departamentoId",
                schema: "dbo",
                table: "CL_Prospecto",
                column: "departamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_CL_Prospecto_tipoRelacionId",
                schema: "dbo",
                table: "CL_Prospecto",
                column: "tipoRelacionId");

            migrationBuilder.CreateIndex(
                name: "IX_CL_Prospecto_tipoSuscriptorId",
                schema: "dbo",
                table: "CL_Prospecto",
                column: "tipoSuscriptorId");

            migrationBuilder.CreateIndex(
                name: "IX_NT_BitacoraNotificacion_clienteId",
                schema: "dbo",
                table: "NT_BitacoraNotificacion",
                column: "clienteId");

            migrationBuilder.CreateIndex(
                name: "IX_NT_BitacoraNotificacion_eventoDifusionId",
                schema: "dbo",
                table: "NT_BitacoraNotificacion",
                column: "eventoDifusionId");

            migrationBuilder.CreateIndex(
                name: "IX_NT_Evento_featureId",
                schema: "dbo",
                table: "NT_Evento",
                column: "featureId");

            migrationBuilder.CreateIndex(
                name: "IX_NT_EventoDifusion_clasificacionId",
                schema: "dbo",
                table: "NT_EventoDifusion",
                column: "clasificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_NT_EventoDifusion_eventoId",
                schema: "dbo",
                table: "NT_EventoDifusion",
                column: "eventoId");

            migrationBuilder.CreateIndex(
                name: "IX_NT_EventoDifusion_plantillaId",
                schema: "dbo",
                table: "NT_EventoDifusion",
                column: "plantillaId");

            migrationBuilder.CreateIndex(
                name: "IX_NT_NotificacionMotivo_aprobadorId",
                schema: "dbo",
                table: "NT_NotificacionMotivo",
                column: "aprobadorId");

            migrationBuilder.CreateIndex(
                name: "IX_NT_NotificacionMotivo_tipoFeatureId",
                schema: "dbo",
                table: "NT_NotificacionMotivo",
                column: "tipoFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_OG_Area_empresaId",
                schema: "dbo",
                table: "OG_Area",
                column: "empresaId");

            migrationBuilder.CreateIndex(
                name: "IX_OG_Departamento_areaId",
                schema: "dbo",
                table: "OG_Departamento",
                column: "areaId");

            migrationBuilder.CreateIndex(
                name: "IX_OG_Empresa_grupoEmpresarialId",
                schema: "dbo",
                table: "OG_Empresa",
                column: "grupoEmpresarialId");

            migrationBuilder.CreateIndex(
                name: "IX_WF_Cargo_departamentoId",
                schema: "dbo",
                table: "WF_Cargo",
                column: "departamentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AS_TrackingSolicitud",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CL_Prospecto",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "NT_BitacoraNotificacion",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "NT_NotificacionMotivo",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CL_TipoRelacion",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CL_TipoSuscriptor",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "NT_EventoDifusion",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CL_Cliente",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "NT_Clasificacion",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "NT_Evento",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "NT_Plantilla",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "WF_Cargo",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "WF_Feature",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OG_Departamento",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OG_Area",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OG_Empresa",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OG_GrupoEmpresarial",
                schema: "dbo");
        }
    }
}
