using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio
{
    public class Cliente
    {
        public int Id { get; set; }

        public String NombreEmpresa { get; set; }

        public String RUToCedula { get; set; }

        public String PersonaContacto { get; set; }

        public String TelefonoMovil { get; set; }

        public String Direccion { get; set; }

        public EstadoCliente Estado { get; set; }

        public List<CobroRecibido> CobrosDelCliente { get; set; }
    }
}
