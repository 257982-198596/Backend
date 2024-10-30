using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Dominio
{
    public class Frecuencia
    {

        public int Id { get; set; }

        public String Nombre { get; set; }

        public List<int> Rutina { get; set; }
    }
}
