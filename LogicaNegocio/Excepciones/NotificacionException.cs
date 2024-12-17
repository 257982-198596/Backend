using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excepciones
{
    public class NotificacionException : Exception
    {

        public NotificacionException() { }

        public NotificacionException(string mensaje) : base(mensaje) { }

        public NotificacionException(string mensaje, Exception inner) : base(mensaje, inner) { }

    }
  
}
