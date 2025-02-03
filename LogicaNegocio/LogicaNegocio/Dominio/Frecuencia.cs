using Excepciones;
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

        //public List<int> Rutina { get; set; }

        public void ValidarFechaMaxima(DateTime fechaInicio)
        {
            DateTime fechaMaxima;

            switch (this.Nombre)
            {
                case "Anual":
                    fechaMaxima = fechaInicio.AddYears(1);
                    break;
                case "Mensual":
                    fechaMaxima = fechaInicio.AddMonths(1);
                    break;
                case "Trimestral":
                    fechaMaxima = fechaInicio.AddMonths(3);
                    break;
                case "Semestral":
                    fechaMaxima = fechaInicio.AddMonths(6);
                    break;
                default:
                    throw new FrecuenciaException($"Frecuencia '{this.Nombre}' no soportada.");
            }

            if (fechaMaxima < DateTime.Today)
            {
                throw new FrecuenciaException("El servicio quedaria vencido con la fecha y frecuencia seleccionada. Contacte al Administrador");
            }
        }

        public void CalcularVencimiento(ServicioDelCliente nuevoServicio)
        {
            if (nuevoServicio == null) return;

            switch (this.Nombre)
            {
                case "Anual":
                    nuevoServicio.FechaVencimiento = nuevoServicio.FechaInicio.AddYears(1);
                    break;
                case "Mensual":
                    nuevoServicio.FechaVencimiento = nuevoServicio.FechaInicio.AddMonths(1);
                    break;
                case "Trimestral":
                    nuevoServicio.FechaVencimiento = nuevoServicio.FechaInicio.AddMonths(3);
                    break;
                case "Semestral":
                    nuevoServicio.FechaVencimiento = nuevoServicio.FechaInicio.AddMonths(6);
                    break;
                default:
                    throw new FrecuenciaException($"Frecuencia '{this.Nombre}' no soportada.");
            }
        }


    }
}
