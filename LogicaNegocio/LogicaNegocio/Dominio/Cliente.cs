using Excepciones;
using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.Dominio
{
    public class Cliente : EntidadComercial, IValidar
    {
        public int Id { get; set; }

        public Documento DocumentoCliente { get; set; }

        public int DocumentoId { get; set; }

        public String NumDocumento { get; set; }

        public EstadoCliente Estado { get; set; }

        public List<CobroRecibido> CobrosDelCliente { get; set; }

        public List<ServicioDelCliente> ServiciosDelCliente { get; set; }

        public List<Notificacion> NotificacionesDelCliente { get; set; }


        public void Validar()
        {
            
        }
    }
}
