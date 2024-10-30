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

        public MedioDePago MedioPago { get; set; }

        public void Validar()
        {
            throw new NotImplementedException();
        }
    }
}
