using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Dominio
{
    public class Suscriptor : EntidadComercial
    {

        public int Id { get; set; }

        public string RUT { get; set; }

        public List<Cliente> ClientesDelSuscriptor { get; set; }

        public List<Servicio> ServiciosDelSuscriptor { get; set; }

        public List<Categoria> CategoriasDelSuscriptor { get; set; }

        public void Validar()
        {

        }
    }
}
