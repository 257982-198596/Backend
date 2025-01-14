using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioNotificaciones : IRepositorio<Notificacion>
    {
        IEnumerable<Notificacion> GenerarNotificacionesPendientes();

        int ContarNotificacionesEnviadas(int clienteId, DateTime desdeFecha);

        IEnumerable<Notificacion> FindBySuscriptorId(int suscriptorId);

    }
}
