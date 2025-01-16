using Excepciones;
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

            const string caracteresPermitidos = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!+*";
            Random random = new Random();
            return new string(Enumerable.Repeat(caracteresPermitidos, longitud)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void ValidarContrasena(string password)
        {
            if (password.Length < 8)
            {
                throw new UsuarioException("La contraseña debe tener al menos 8 caracteres.");
            }

            bool tieneMayuscula = false;
            bool tieneMinuscula = false;
            bool tieneNumero = false;
            bool tieneSigno = false;

            foreach (char ch in password)
            {
                if (char.IsUpper(ch))
                {
                    tieneMayuscula = true;
                }
                else if (char.IsLower(ch))
                {
                    tieneMinuscula = true;
                }
                else if (char.IsDigit(ch))
                {
                    tieneNumero = true;
                }
                else if (!char.IsLetterOrDigit(ch))
                {
                    tieneSigno = true;
                }
            }

            if (!tieneMayuscula)
            {
                throw new UsuarioException("La contraseña debe contener al menos una letra mayúscula.");
            }

            if (!tieneMinuscula)
            {
                throw new UsuarioException("La contraseña debe contener al menos una letra minúscula.");
            }

            if (!tieneNumero)
            {
                throw new UsuarioException("La contraseña debe contener al menos un número.");
            }

            if (!tieneSigno)
            {
                throw new UsuarioException("La contraseña debe contener al menos un signo.");
            }
        }

    }
}
