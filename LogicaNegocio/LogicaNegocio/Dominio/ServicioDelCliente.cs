using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Dominio
{
    public class ServicioDelCliente : IValidar
    {

        
        public int Id { get; set; }

        public Servicio ServicioContratado { get; set; }

        [Required]
        public int ServicioContratadoId { get; set; }

        public Cliente Cliente { get; set; }

        [Required]
        public int ClienteId { get; set; }

        public Decimal Precio { get; set; }


        public string Descripcion { get; set; }

        public Moneda MonedaDelServicio { get; set; }

        [Required]
        public int MonedaDelServicioId { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaVencimiento { get; set; }

        public Frecuencia FrecuenciaDelServicio { get; set; }

        [Required]
        public int FrecuenciaDelServicioId { get; set; }

        public EstadoServicioDelCliente EstadoDelServicioDelCliente { get; set; }



        public void Actualizar(CobroRecibido obj)
        {
            this.EstadoDelServicioDelCliente = new EstadoServicioDelCliente { Nombre = "Pago" };

        }



        public void Validar()
        {
            
        }

        
    }
}
