using LogicaNegocio.Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.BaseDatos
{
    public class CobrosContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Suscriptor> Suscriptores { get; set; }
        public DbSet<Servicio> Servicios { get; set; }

        public DbSet<CobroRecibido> CobrosRecibidos { get; set; }

        public DbSet<MedioDePago> MediosDePago { get; set; }
        public DbSet<ServicioDelCliente> ServiciosDelCliente { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }

        public DbSet<Documento> Documentos { get; set; }

        public DbSet<Pais> Paises { get; set; }

        public DbSet<Frecuencia> Frecuencias { get; set; }

        public DbSet<Moneda> Monedas { get; set; }

        public DbSet<Notificacion> Notificaciones { get; set; }

        public DbSet<EstadoServicioDelCliente> EstadosServiciosDelClientes { get; set; }

        public DbSet<EstadoCliente> EstadosDelCliente { get; set; }

        public DbSet<EstadoNotificacion> EstadosDeNotificacion { get; set; }

        public CobrosContext(DbContextOptions<CobrosContext> opciones) : base(opciones)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //para guardar decimal en DB
            modelBuilder.Entity<CobroRecibido>()
            .Property(c => c.Monto)
            .HasPrecision(18, 2);

            modelBuilder.Entity<ServicioDelCliente>()
            .Property(s => s.Precio)
            .HasPrecision(18, 2);

            //Claves UNIQUE
            modelBuilder.Entity<Cliente>()
            .HasIndex(c => c.NumDocumento)
            .IsUnique();

            modelBuilder.Entity<Notificacion>()
            .HasOne(n => n.EstadoDeNotificacion)
            .WithMany()
            .HasForeignKey(n => n.EstadoDeNotificacionId)
            .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

    }
}
