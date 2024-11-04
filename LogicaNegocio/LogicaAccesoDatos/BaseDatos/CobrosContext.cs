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

        public CobrosContext(DbContextOptions<CobrosContext> opciones) : base(opciones)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

        }

    }
}
