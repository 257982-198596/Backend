using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excepciones
{
    public class ServicioDelClienteException : Exception
    {

        public ServicioDelClienteException()
        {
        }

        public ServicioDelClienteException(string mensajeError) : base(mensajeError)
        {
        }

        public ServicioDelClienteException(string mensajeError, Exception inner) : base(mensajeError, inner)
        {
        }
    }
}
