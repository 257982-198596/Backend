using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Dominio
{
    public class ServicioDelCliente : IValidar
    {

        public int Id { get; set; }

        public Servicio ServicioContratado { get; set; }

        public Decimal Precio { get; set; }

        public Moneda MonedaDelServicio { get; set; }

        public DateOnly FechaInicio { get; set; }

        public DateOnly FechaVencimiento { get; set; }

        public Frecuencia FrecuenciaDelServicio { get; set; }

        public EstadoServicioDelCliente EstadoDelServicioDelCliente { get; set; }

        public void Validar()
        {
            throw new NotImplementedException();
        }
    }
}
