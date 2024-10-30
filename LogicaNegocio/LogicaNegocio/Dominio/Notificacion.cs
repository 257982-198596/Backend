using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Dominio
{
    public class Notificacion
    {

        public int Id { get; set; }

        public DateTime FechaEnvio { get; set; }

        public String Mensaje { get; set; }

        public EstadoNotificacion EstadoDeNotificacion { get; set; }

    }
}
