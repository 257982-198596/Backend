using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioCobros : IRepositorio<CobroRecibido>
    {
        //public void AgregarObservador(IObservador<IRepositorioServiciosDelCliente> observador);
        void AgregarObservador(IRepositorioServiciosDelCliente repositorioServicios);

        void AgregarObservador(IRepositorioClientes repositorioClientes);

    }
}
