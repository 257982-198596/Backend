﻿// <auto-generated />
using System;
using LogicaAccesoDatos.BaseDatos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    [DbContext(typeof(CobrosContext))]
    [Migration("20241206155003_FechaDePagoACOBRO")]
    partial class FechaDePagoACOBRO
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.35")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LogicaNegocio.Dominio.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Direccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DocumentoId")
                        .HasColumnType("int");

                    b.Property<int?>("EstadoId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumDocumento")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PaisId")
                        .HasColumnType("int");

                    b.Property<string>("PersonaContacto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SuscriptorId")
                        .HasColumnType("int");

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UsuarioLoginId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DocumentoId");

                    b.HasIndex("EstadoId");

                    b.HasIndex("NumDocumento")
                        .IsUnique();

                    b.HasIndex("PaisId");

                    b.HasIndex("SuscriptorId");

                    b.HasIndex("UsuarioLoginId");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.CobroRecibido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ClienteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaDePago")
                        .HasColumnType("datetime2");

                    b.Property<int>("MedioPagoId")
                        .HasColumnType("int");

                    b.Property<int>("MonedaDelCobroId")
                        .HasColumnType("int");

                    b.Property<decimal>("Monto")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ServicioDelClienteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("MedioPagoId");

                    b.HasIndex("MonedaDelCobroId");

                    b.HasIndex("ServicioDelClienteId");

                    b.ToTable("CobrosRecibidos");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Documento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Documentos");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.EstadoCliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EstadosDelCliente");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.EstadoNotificacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EstadoNotificacion");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.EstadoServicioDelCliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EstadosServiciosDelClientes");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Frecuencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Frecuencias");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.MedioDePago", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MediosDePago");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Moneda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Monedas");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Notificacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ClienteId")
                        .HasColumnType("int");

                    b.Property<int?>("EstadoDeNotificacionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaEnvio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Mensaje")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("EstadoDeNotificacionId");

                    b.ToTable("Notificacion");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Pais", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Paises");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Servicio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SuscriptorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("SuscriptorId");

                    b.ToTable("Servicios");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.ServicioDelCliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EstadoDelServicioDelClienteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaVencimiento")
                        .HasColumnType("datetime2");

                    b.Property<int>("FrecuenciaDelServicioId")
                        .HasColumnType("int");

                    b.Property<int>("MonedaDelServicioId")
                        .HasColumnType("int");

                    b.Property<decimal>("Precio")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ServicioContratadoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("EstadoDelServicioDelClienteId");

                    b.HasIndex("FrecuenciaDelServicioId");

                    b.HasIndex("MonedaDelServicioId");

                    b.HasIndex("ServicioContratadoId");

                    b.ToTable("ServiciosDelCliente");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Suscriptor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Direccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaisId")
                        .HasColumnType("int");

                    b.Property<string>("PersonaContacto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RUT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UsuarioLoginId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PaisId");

                    b.HasIndex("UsuarioLoginId");

                    b.ToTable("Suscriptores");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RolId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Cliente", b =>
                {
                    b.HasOne("LogicaNegocio.Dominio.Documento", "DocumentoCliente")
                        .WithMany()
                        .HasForeignKey("DocumentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogicaNegocio.Dominio.EstadoCliente", "Estado")
                        .WithMany()
                        .HasForeignKey("EstadoId");

                    b.HasOne("LogicaNegocio.Dominio.Pais", "Pais")
                        .WithMany()
                        .HasForeignKey("PaisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogicaNegocio.Dominio.Suscriptor", null)
                        .WithMany("ClientesDelSuscriptor")
                        .HasForeignKey("SuscriptorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogicaNegocio.Dominio.Usuario", "UsuarioLogin")
                        .WithMany()
                        .HasForeignKey("UsuarioLoginId");

                    b.Navigation("DocumentoCliente");

                    b.Navigation("Estado");

                    b.Navigation("Pais");

                    b.Navigation("UsuarioLogin");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.CobroRecibido", b =>
                {
                    b.HasOne("LogicaNegocio.Dominio.Cliente", null)
                        .WithMany("CobrosDelCliente")
                        .HasForeignKey("ClienteId");

                    b.HasOne("LogicaNegocio.Dominio.MedioDePago", "MedioPago")
                        .WithMany()
                        .HasForeignKey("MedioPagoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogicaNegocio.Dominio.Moneda", "MonedaDelCobro")
                        .WithMany()
                        .HasForeignKey("MonedaDelCobroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogicaNegocio.Dominio.ServicioDelCliente", "ServicioDelCliente")
                        .WithMany()
                        .HasForeignKey("ServicioDelClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MedioPago");

                    b.Navigation("MonedaDelCobro");

                    b.Navigation("ServicioDelCliente");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Notificacion", b =>
                {
                    b.HasOne("LogicaNegocio.Dominio.Cliente", null)
                        .WithMany("NotificacionesDelCliente")
                        .HasForeignKey("ClienteId");

                    b.HasOne("LogicaNegocio.Dominio.EstadoNotificacion", "EstadoDeNotificacion")
                        .WithMany()
                        .HasForeignKey("EstadoDeNotificacionId");

                    b.Navigation("EstadoDeNotificacion");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Servicio", b =>
                {
                    b.HasOne("LogicaNegocio.Dominio.Categoria", "CategoriaDelServicio")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogicaNegocio.Dominio.Suscriptor", null)
                        .WithMany("ServiciosDelSuscriptor")
                        .HasForeignKey("SuscriptorId");

                    b.Navigation("CategoriaDelServicio");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.ServicioDelCliente", b =>
                {
                    b.HasOne("LogicaNegocio.Dominio.Cliente", "Cliente")
                        .WithMany("ServiciosDelCliente")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogicaNegocio.Dominio.EstadoServicioDelCliente", "EstadoDelServicioDelCliente")
                        .WithMany()
                        .HasForeignKey("EstadoDelServicioDelClienteId");

                    b.HasOne("LogicaNegocio.Dominio.Frecuencia", "FrecuenciaDelServicio")
                        .WithMany()
                        .HasForeignKey("FrecuenciaDelServicioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogicaNegocio.Dominio.Moneda", "MonedaDelServicio")
                        .WithMany()
                        .HasForeignKey("MonedaDelServicioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogicaNegocio.Dominio.Servicio", "ServicioContratado")
                        .WithMany()
                        .HasForeignKey("ServicioContratadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("EstadoDelServicioDelCliente");

                    b.Navigation("FrecuenciaDelServicio");

                    b.Navigation("MonedaDelServicio");

                    b.Navigation("ServicioContratado");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Suscriptor", b =>
                {
                    b.HasOne("LogicaNegocio.Dominio.Pais", "Pais")
                        .WithMany()
                        .HasForeignKey("PaisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogicaNegocio.Dominio.Usuario", "UsuarioLogin")
                        .WithMany()
                        .HasForeignKey("UsuarioLoginId");

                    b.Navigation("Pais");

                    b.Navigation("UsuarioLogin");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Usuario", b =>
                {
                    b.HasOne("LogicaNegocio.Dominio.Rol", "RolDeUsuario")
                        .WithMany()
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RolDeUsuario");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Cliente", b =>
                {
                    b.Navigation("CobrosDelCliente");

                    b.Navigation("NotificacionesDelCliente");

                    b.Navigation("ServiciosDelCliente");
                });

            modelBuilder.Entity("LogicaNegocio.Dominio.Suscriptor", b =>
                {
                    b.Navigation("ClientesDelSuscriptor");

                    b.Navigation("ServiciosDelSuscriptor");
                });
#pragma warning restore 612, 618
        }
    }
}
