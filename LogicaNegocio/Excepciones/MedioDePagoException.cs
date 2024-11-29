using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excepciones
{
    public class MedioDePagoException : Exception
    {

        public MedioDePagoException() { }

        public MedioDePagoException(string mensaje) : base(mensaje) { }

        public MedioDePagoException(string mensaje, Exception inner) : base(mensaje, inner) { }

    }
    
}
