using Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogicaNegocio.Dominio
{
    public class Suscriptor : EntidadComercial
    {

        public int Id { get; set; }

        public string RUT { get; set; }

        public List<Cliente> ClientesDelSuscriptor { get; set; }

        public List<Servicio> ServiciosDelSuscriptor { get; set; }

        public List<Categoria> CategoriasDelSuscriptor { get; set; }

        public void Validar()
        {
            ValidarNombre();
            ValidarRUT();
            ValidarTelefono();
            ValidarDireccion();
            ValidarPersonaContacto();
            ValidarPais();
            ValidarEmail();
            ValidarPassword();
        }

        private void ValidarRUT()
        {
            if (string.IsNullOrWhiteSpace(RUT))
            {
                throw new SuscriptorException("El número de documento es obligatorio");
            }
            if (!Regex.IsMatch(RUT, @"^\d+$"))
            {
                throw new SuscriptorException("El número de documento debe ser numérico");
            }
            if (RUT.Length != 12)
            {
                throw new SuscriptorException("El número de RUT debe tener exactamente 12 caracteres");
            }


        }
    }
}
