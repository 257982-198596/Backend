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
            }
        }
    }
}
