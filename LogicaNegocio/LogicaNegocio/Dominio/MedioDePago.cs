using Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Dominio
{
    public class MedioDePago
    {

        public int Id { get; set; }

        public String Nombre { get; set; }

        public void Validar()
        {
            ValidarNombre();
        }

        private void ValidarNombre()
        {
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                throw new MedioDePagoException("El nombre del medio de pago es obligatorio");
            }
        }
    }
}
