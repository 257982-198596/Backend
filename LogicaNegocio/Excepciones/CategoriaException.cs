using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excepciones
{
    public class CategoriaException : Exception
    {

        public CategoriaException() { }

        public CategoriaException(string mensaje) : base(mensaje) { }

        public CategoriaException(string mensaje, Exception inner) : base(mensaje, inner) { }
    
    }
}
