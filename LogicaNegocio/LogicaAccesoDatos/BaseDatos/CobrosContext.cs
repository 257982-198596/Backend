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

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }

        public DbSet<Documento> Documentos { get; set; }

        public DbSet<Pais> Paises { get; set; }

        public CobrosContext(DbContextOptions<CobrosContext> opciones) : base(opciones)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CobroRecibido>()
            .Property(c => c.Monto)
            .HasPrecision(18, 2);

            modelBuilder.Entity<ServicioDelCliente>()
            .Property(s => s.Precio)
            .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }

    }
}
