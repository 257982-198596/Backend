using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excepciones
{
    public class DocumentoException : Exception
    {

        public DocumentoException() { }

        public DocumentoException(string mensaje) : base(mensaje) { }

        public DocumentoException(string mensaje, Exception inner) : base(mensaje, inner) { }
    
    }
}
