using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Dominio
{
    public abstract class EntidadComercial
    {
        [MinLength(5, ErrorMessage = "El nombre tiene que tener por lo menos 5 caracteres")]
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
    }
}
