using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excepciones
{
    public class ServicioException : Exception
    {
        public ServicioException() { }

        public ServicioException(string mensaje) : base(mensaje) { }

        public ServicioException(string mensaje, Exception inner) : base(mensaje, inner) { }
    }
}
