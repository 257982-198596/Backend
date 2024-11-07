using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.Dominio
{
    public class Servicio : IValidar
    {
        public int Id { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }

        public Categoria CategoriaDelServicio { get; set; }
        public int CategoriaId { get; set; }

        public void Validar()
        {
            //throw new NotImplementedException();
        }
    }
}
