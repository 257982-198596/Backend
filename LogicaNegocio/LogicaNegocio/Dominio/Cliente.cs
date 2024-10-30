using Excepciones;
using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.Dominio
{
    public class Cliente : Usuario, IValidar
    {
        public int Id { get; set; }

        public String NombreEmpresa { get; set; }


        public Documento DocumentoCliente { get; set; }

        public String NumDocumento { get; set; }

        public String PersonaContacto { get; set; }

        public String TelefonoMovil { get; set; }

        public String Direccion { get; set; }

        public EstadoCliente Estado { get; set; }

        public List<CobroRecibido> CobrosDelCliente { get; set; }

        public List<ServicioDelCliente> ServiciosDelCliente { get; set; }

        public List<Notificacion> NotificacionesDelCliente { get; set; }


        public void Validar()
        {
            throw new NotImplementedException();
        }
    }
}
