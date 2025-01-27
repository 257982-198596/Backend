using Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Dominio
{
    public class Documento
    {
        public int Id { get; set; }

        public String Nombre { get; set; }

        public void ValidaTipoDocumento(String NumDocumento)
        {
            if(this.Nombre == "Cédula" && NumDocumento.Length != 8)
            {
                throw new DocumentoException("El número de cedula debe tener 8 caracteres");
            }
            if (this.Nombre == "RUT" && NumDocumento.Length != 12)
            {
                throw new DocumentoException("El número de RUT debe tener 12 caracteres");
            }
        }
    }
}
