using Excepciones;
using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Dominio
{
    public class Categoria : IValidar
    {

        public int Id { get; set; }

        public String Nombre { get; set; }

        public int SuscriptorId { get; set; }
        public void Validar()
        {
            if (this.Id == 0)
            {
                throw new CategoriaException("Debe seleccionar una categoría");
                ValidarNombre();
            }
        }

        public void ValidarNombre()
        {
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                throw new ServicioException("El nombre es obligatorio");
            }
            if (Nombre.Length < 3)
            {
                throw new ServicioException("El nombre debe tener al menos 3 caracteres");
            }
            if (Nombre.Length > 100)
            {
                throw new ServicioException("El nombre no debe exceder los 100 caracteres");
            }
        }
    }
}
