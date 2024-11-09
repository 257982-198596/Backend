using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadoCliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoCliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadoNotificacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoNotificacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadoServicioDelCliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoServicioDelCliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Frecuencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frecuencia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedioDePago",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedioDePago", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Moneda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moneda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suscriptores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RUT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonaContacto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suscriptores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suscriptores_Paises_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Paises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentoId = table.Column<int>(type: "int", nullable: false),
                    NumDocumento = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EstadoId = table.Column<int>(type: "int", nullable: true),
                    SuscriptorId = table.Column<int>(type: "int", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonaContacto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_Documentos_DocumentoId",
                        column: x => x.DocumentoId,
                        principalTable: "Documentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clientes_EstadoCliente_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "EstadoCliente",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Clientes_Paises_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Paises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clientes_Suscriptores_SuscriptorId",
                        column: x => x.SuscriptorId,
                        principalTable: "Suscriptores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    SuscriptorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servicios_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servicios_Suscriptores_SuscriptorId",
                        column: x => x.SuscriptorId,
                        principalTable: "Suscriptores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CobroRecibido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MonedaDelCobroId = table.Column<int>(type: "int", nullable: true),
                    MedioPagoId = table.Column<int>(type: "int", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CobroRecibido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CobroRecibido_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CobroRecibido_MedioDePago_MedioPagoId",
                        column: x => x.MedioPagoId,
                        principalTable: "MedioDePago",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CobroRecibido_Moneda_MonedaDelCobroId",
                        column: x => x.MonedaDelCobroId,
                        principalTable: "Moneda",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notificacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaEnvio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Mensaje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoDeNotificacionId = table.Column<int>(type: "int", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notificacion_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notificacion_EstadoNotificacion_EstadoDeNotificacionId",
                        column: x => x.EstadoDeNotificacionId,
                        principalTable: "EstadoNotificacion",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServicioDelCliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServicioContratadoId = table.Column<int>(type: "int", nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MonedaDelServicioId = table.Column<int>(type: "int", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FrecuenciaDelServicioId = table.Column<int>(type: "int", nullable: true),
                    EstadoDelServicioDelClienteId = table.Column<int>(type: "int", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicioDelCliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicioDelCliente_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServicioDelCliente_EstadoServicioDelCliente_EstadoDelServicioDelClienteId",
                        column: x => x.EstadoDelServicioDelClienteId,
                        principalTable: "EstadoServicioDelCliente",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServicioDelCliente_Frecuencia_FrecuenciaDelServicioId",
                        column: x => x.FrecuenciaDelServicioId,
                        principalTable: "Frecuencia",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServicioDelCliente_Moneda_MonedaDelServicioId",
                        column: x => x.MonedaDelServicioId,
                        principalTable: "Moneda",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServicioDelCliente_Servicios_ServicioContratadoId",
                        column: x => x.ServicioContratadoId,
                        principalTable: "Servicios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_DocumentoId",
                table: "Clientes",
                column: "DocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_EstadoId",
                table: "Clientes",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_NumDocumento",
                table: "Clientes",
                column: "NumDocumento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_PaisId",
                table: "Clientes",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_SuscriptorId",
                table: "Clientes",
                column: "SuscriptorId");

            migrationBuilder.CreateIndex(
                name: "IX_CobroRecibido_ClienteId",
                table: "CobroRecibido",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_CobroRecibido_MedioPagoId",
                table: "CobroRecibido",
                column: "MedioPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_CobroRecibido_MonedaDelCobroId",
                table: "CobroRecibido",
                column: "MonedaDelCobroId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacion_ClienteId",
                table: "Notificacion",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacion_EstadoDeNotificacionId",
                table: "Notificacion",
                column: "EstadoDeNotificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicioDelCliente_ClienteId",
                table: "ServicioDelCliente",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicioDelCliente_EstadoDelServicioDelClienteId",
                table: "ServicioDelCliente",
                column: "EstadoDelServicioDelClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicioDelCliente_FrecuenciaDelServicioId",
                table: "ServicioDelCliente",
                column: "FrecuenciaDelServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicioDelCliente_MonedaDelServicioId",
                table: "ServicioDelCliente",
                column: "MonedaDelServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicioDelCliente_ServicioContratadoId",
                table: "ServicioDelCliente",
                column: "ServicioContratadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_CategoriaId",
                table: "Servicios",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_SuscriptorId",
                table: "Servicios",
                column: "SuscriptorId");

            migrationBuilder.CreateIndex(
                name: "IX_Suscriptores_PaisId",
                table: "Suscriptores",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RolId",
                table: "Usuarios",
                column: "RolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CobroRecibido");

            migrationBuilder.DropTable(
                name: "Notificacion");

            migrationBuilder.DropTable(
                name: "ServicioDelCliente");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "MedioDePago");

            migrationBuilder.DropTable(
                name: "EstadoNotificacion");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "EstadoServicioDelCliente");

            migrationBuilder.DropTable(
                name: "Frecuencia");

            migrationBuilder.DropTable(
                name: "Moneda");

            migrationBuilder.DropTable(
                name: "Servicios");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Documentos");

            migrationBuilder.DropTable(
                name: "EstadoCliente");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Suscriptores");

            migrationBuilder.DropTable(
                name: "Paises");
        }
    }
}
