using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excepciones
{
    public class CobroRecibidoException : Exception
    {

        public CobroRecibidoException() { }

        public CobroRecibidoException(string mensaje) : base(mensaje) { }

        public CobroRecibidoException(string mensaje, Exception inner) : base(mensaje, inner) { }

    
    }
}
