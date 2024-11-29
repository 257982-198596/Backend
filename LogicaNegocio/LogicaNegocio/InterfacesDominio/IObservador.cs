using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LogicaNegocio.InterfacesDominio
{
    public interface IObservador<T>
    {
        void Actualizar(T obj, string evento);

        enum Eventos { AltaCobro };
    }
}
