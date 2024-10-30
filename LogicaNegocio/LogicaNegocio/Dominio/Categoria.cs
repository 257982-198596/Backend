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

        public void Validar()
        {
            throw new NotImplementedException();
        }
    }
}
