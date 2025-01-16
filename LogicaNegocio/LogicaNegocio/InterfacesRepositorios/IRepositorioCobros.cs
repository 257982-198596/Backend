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

        IEnumerable<CobroRecibido> FindBySuscriptorId(int suscriptorId);

        Dictionary<int, decimal> SumaCobrosPorMes(int suscriptorId, int year);

        Dictionary<int, decimal> SumaCobrosPorMesYServicio(int suscriptorId, int year, int servicioId);

        Dictionary<int, decimal> SumaCobrosPorMesYCliente(int suscriptorId, int year, int clienteId);

    }
}
