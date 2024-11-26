using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excepciones
{
    public class FrecuenciaException : Exception
    {

        public FrecuenciaException() { }

        public FrecuenciaException(string mensaje) : base(mensaje) { }

        public FrecuenciaException(string mensaje, Exception inner) : base(mensaje, inner) { }

    }
}
