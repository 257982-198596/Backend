using Excepciones;
using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace LogicaNegocio.Dominio
{
    
    public class Cliente : EntidadComercial, IValidar
    {
        public int Id { get; set; }

        public Documento DocumentoCliente { get; set; }

        [Required(ErrorMessage = "El número de documento es obligatorio")]
        public int DocumentoId { get; set; }

        [Required]
        [RegularExpression("([0-9]+)", ErrorMessage = "El documento tiene que ser numérico")]
        public String NumDocumento { get; set; }

        public EstadoCliente Estado { get; set; }

        public List<CobroRecibido> CobrosDelCliente { get; set; }

        public List<ServicioDelCliente> ServiciosDelCliente { get; set; }

        //public List<Notificacion> NotificacionesDelCliente { get; set; }

        public int SuscriptorId { get; set; }
        public void Validar()
        {
            ValidarNombre();
            ValidarNumDocumento();
            ValidarTelefono();
            ValidarDireccion();
            ValidarPersonaContacto();
            ValidarPais();
            ValidarEmail();
            ValidarPassword();
        }

        

        private void ValidarNumDocumento()
        {
            if (string.IsNullOrWhiteSpace(NumDocumento))
            {
                throw new ClienteException("El número de documento es obligatorio");
            }
            if (!Regex.IsMatch(NumDocumento, @"^\d+$"))
            {
                throw new ClienteException("El número de documento debe ser numérico");
            }
            this.DocumentoCliente.ValidaTipoDocumento(NumDocumento);
            
        }

        

        

        

        
    }
}
