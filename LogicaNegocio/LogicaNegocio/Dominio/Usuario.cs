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


        public Rol RolDeUsuario { get; set; }

        public int RolId { get; set; }

        public string GenerarContrasenaTemporal(int longitud = 8)
        {

            const string caracteresPermitidos = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            Random random = new Random();
            return new string(Enumerable.Repeat(caracteresPermitidos, longitud)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
