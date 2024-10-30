using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Dominio
{
    public class Suscriptor
    {

        public int Id { get; set; }

        public String NombreEmpresa { get; set; }

        public String RUT { get; set; }

        public String Telefono { get; set; }

        public String Direccion { get; set; }

        public String PersonaContacto { get; set; }

        public List<Cliente> ClientesDelSuscriptor { get; set; }
    }
}
