using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.Dominio
{
    public class Servicio : IValidar
    {
        public int ID { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }

        public void Validar()
        {
            throw new NotImplementedException();
        }
    }
}
