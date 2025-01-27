using Excepciones;
using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LogicaNegocio.Dominio
{
    public class CobroRecibido : IValidar
    {
        public int Id { get; set; }

        public DateTime FechaDePago { get; set; }

        public decimal Monto { get; set; }

        public Moneda MonedaDelCobro { get; set; }

        public int MonedaDelCobroId { get; set; }

        public MedioDePago MedioPago { get; set; }

        public int MedioPagoId { get; set; }

        public ServicioDelCliente ServicioDelCliente { get; set; }

        public int ServicioDelClienteId { get; set; }



        public void Validar()
        {
            ValidarCliente();
            ValidarFechaDePago();
            ValidarMonto();
            ValidarMoneda();
            ValidarMedioDePago();
        }


        private void ValidarCliente()
        {
            if (ServicioDelClienteId == 0)
            {
                throw new CobroRecibidoException("Debe seleccionar un cliente");
            }
        }

        

        private void ValidarMonto()
        {
            if (Monto <= 0)
            {
                throw new CobroRecibidoException("Debe ingresar un monto válido");
            }
            if (!Regex.IsMatch(Monto.ToString(), @"^\d+(\.\d{1,2})?$"))
            {
                throw new CobroRecibidoException("El monto debe ser un número válido");
            }

        }

        private void ValidarMoneda()
        {
            if (MonedaDelCobroId == 0)
            {
                throw new CobroRecibidoException("La moneda es un campo obligatorio");
            }
            
        }

        private void ValidarMedioDePago()
        {
            if (MedioPagoId == 0)
            {
                throw new CobroRecibidoException("Debe seleccionar un medio de pago");
            }

        }

        private void ValidarFechaDePago()
        {
            if (FechaDePago > DateTime.Now)
            {
                throw new CobroRecibidoException("La fecha de pago no puede ser una fecha futura");
            }
        }

        //public void Notificar()
        //{
        //    NotificarObservadores(this);
        //}

    }
}
