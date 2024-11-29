using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.BaseDatos
{
    public class ObservadorService
    {
        private readonly IRepositorioCobros _repositorioCobros;
        private readonly IRepositorioServiciosDelCliente _repositorioServicios;
        private readonly IRepositorioClientes _repositorioClientes;

        public ObservadorService(IRepositorioCobros repositorioCobros, IRepositorioServiciosDelCliente repositorioServicios)
        {
            _repositorioCobros = repositorioCobros;
            _repositorioServicios = repositorioServicios;

            _repositorioCobros.AgregarObservador(_repositorioServicios);
            _repositorioCobros.AgregarObservador(_repositorioClientes);
        }
    }

}
