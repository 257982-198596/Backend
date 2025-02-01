using Excepciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogicaNegocio.Dominio
{
    public abstract class EntidadComercial
    {
        [MinLength(3, ErrorMessage = "El nombre tiene que tener por lo menos 3 caracteres")]
        public string Nombre { get; set; }

        [RegularExpression("([0-9]+)", ErrorMessage = "El teléfono tiene que ser numérico")]
        [MinLength(7, ErrorMessage = "El teléfono tiene que tener por lo menos 7 caracteres")]
        public string Telefono { get; set; }

        [MinLength(5, ErrorMessage = "La dirección tiene que tener por lo menos 5 caracteres")]
        public string Direccion { get; set; }
        [MinLength(5, ErrorMessage = "La persona tiene que tener por lo menos 5 caracteres")]
        public string PersonaContacto { get; set; }

        public Pais Pais { get; set; }

        public int PaisId { get; set; }

        public Usuario UsuarioLogin { get; set; }

        protected void ValidarPais()
        {
            if (PaisId == 0)
            {
                throw new Exception("El país de origen es obligatorio");
            }
        }

        protected void ValidarEmail()
        {
            if (UsuarioLogin == null)
            {
                throw new Exception("El usuario de login es obligatorio");
            }
            UsuarioLogin.ValidarEmail();
        }

        protected void ValidarPassword()
        {
            if (UsuarioLogin == null)
            {
                throw new Exception("El usuario de login es obligatorio");
            }
            UsuarioLogin.ValidarContrasena(UsuarioLogin.Password);
        }

        protected void ValidarNombre()
        {
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                throw new Exception("El nombre es obligatorio");
            }
            if (Nombre.Length <= 3)
            {
                throw new Exception("El nombre debe tener más de 3 caracteres");
            }
        }

        protected void ValidarTelefono()
        {
            if (string.IsNullOrWhiteSpace(Telefono))
            {
                throw new Exception("El número de teléfono es obligatorio");
            }
            if (Telefono.Length <= 7)
            {
                throw new Exception("El teléfono debe tener más de 7 caracteres");
            }
            if (Telefono.Length >= 15)
            {
                throw new Exception("El teléfono debe tener menos de 15 caracteres");
            }
            if (!Regex.IsMatch(Telefono, @"^\d+$"))
            {
                throw new Exception("El teléfono debe ser numérico");
            }
        }

        protected void ValidarDireccion()
        {
            if (string.IsNullOrWhiteSpace(Direccion))
            {
                throw new Exception("El campo dirección es obligatorio");
            }
            if (Direccion.Length <= 5)
            {
                throw new Exception("La dirección debe tener más de 5 caracteres");
            }
        }

        protected void ValidarPersonaContacto()
        {
            if (string.IsNullOrWhiteSpace(PersonaContacto))
            {
                throw new ClienteException("La persona de contacto es obligatoria");
            }
            if (PersonaContacto.Length <= 5)
            {
                throw new ClienteException("La persona de contacto debe tener más de 5 caracteres");
            }
        }
    }
}
