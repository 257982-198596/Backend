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

        public int EstadoDeNotificacionId { get; set; }

        public Cliente ClienteNotificado { get; set; }

        public int ClienteNotificadoId { get; set; }

        public ServicioDelCliente ServicioNotificado { get; set; }

        public int ServicioNotificadoId { get; set; }

        public Notificacion()
        {
            
        }
        public Notificacion(DateTime fecha, String mensaje)
        {
            FechaEnvio = fecha;
            Mensaje = mensaje;
        }

    }
}
