using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excepciones
{
    public  class MonedaException : Exception
    {

        public MonedaException() { }

        public MonedaException(string mensaje) : base(mensaje) { }

        public MonedaException(string mensaje, Exception inner) : base(mensaje, inner) { }
    }
}
