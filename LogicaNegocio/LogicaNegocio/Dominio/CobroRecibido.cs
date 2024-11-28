using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.Dominio
{
    public class CobroRecibido : IValidar
    {
        public int Id { get; set; }

        public decimal Monto { get; set; }

        public Moneda MonedaDelCobro { get; set; }

        public int MonedaDelCobroId { get; set; }

        public MedioDePago MedioPago { get; set; }

        public int MedioPagoId { get; set; }

        public ServicioDelCliente ServicioDelCliente { get; set; }

        public int ServicioDelClienteId { get; set; }

        public void Validar()
        {
            
        }
    }
}
