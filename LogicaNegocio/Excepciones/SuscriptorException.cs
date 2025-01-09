using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excepciones
{
    public class SuscriptorException : Exception
    {
        public SuscriptorException() { }

        public SuscriptorException(string mensaje) : base(mensaje) { }

        public SuscriptorException(string mensaje, Exception inner) : base(mensaje, inner) { }
    }
}
