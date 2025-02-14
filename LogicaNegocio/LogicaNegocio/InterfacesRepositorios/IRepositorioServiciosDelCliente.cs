﻿using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioServiciosDelCliente : IRepositorio<ServicioDelCliente>
    {
        IEnumerable<ServicioDelCliente> ServiciosDeUnCliente(int idCliente);

        IEnumerable<ServicioDelCliente> ServiciosActivosDeUnCliente(int idCliente);

        IEnumerable<ServicioDelCliente> ServiciosPagosDeUnCliente(int idCliente);

        ServicioDelCliente ObtenerProximoServicioActivoAVencerse(int idCliente);

        public decimal CalcularIngresosProximos365Dias(int idCliente);

        IEnumerable<ServicioDelCliente> ServiciosDeClientesDeUnSuscriptor(int idSuscriptor);

        IEnumerable<ServicioDelCliente> ServiciosDeClientesDeUnSuscriptorQueVencenEsteMes(int idSuscriptor);

        //metodo para modificar estado del servicio a Vencido
        IEnumerable<ServicioDelCliente> MarcarServiciosComoVencidos();

        // indicadores para reporte vencimientos del mes corriente
        Dictionary<string, decimal> ObtenerIndicadoresServiciosVencenEsteMes(int idSuscriptor);

        void CancelarServicioDelCliente(int idServicioDelCliente);


    }
}
