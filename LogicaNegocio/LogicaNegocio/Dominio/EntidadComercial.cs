using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Dominio
{
    public abstract class EntidadComercial
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string PersonaContacto { get; set; }

        public Pais Pais { get; set; }

        public int PaisId { get; set; }
    }
}
