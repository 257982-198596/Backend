using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Dominio
{
    public class Usuario
    {

        public int Id { get; set; }

        public String Email { get; set; }

        public String Password { get; set; }

        public Pais PaisUsuario { get; set; }

        public Rol RolDeUsuario { get; set; }
    }
}
