using System;

namespace Excepciones
{
    public class ClienteException : Exception
    {

        public ClienteException() { }

        public ClienteException(string mensaje) : base(mensaje) { }

        public ClienteException(string mensaje, Exception inner) : base(mensaje, inner) { }

    }
}
