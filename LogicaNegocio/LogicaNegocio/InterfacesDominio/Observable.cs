using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace LogicaNegocio.InterfacesDominio
{
    public abstract class Observable<T>
    {
        private readonly List<IObservador<T>> _observadores = new List<IObservador<T>>();

        public void AgregarObservador(IObservador<T> observador)
        {
            _observadores.Add(observador);
        }

        public void EliminarObservador(IObservador<T> observador)
        {
            _observadores.Remove(observador);
        }

        protected void NotificarObservadores(T obj, string evento)
        {
            foreach (var observador in _observadores)
            {
                observador.Actualizar(obj, evento);
            }
        }
    }
}
