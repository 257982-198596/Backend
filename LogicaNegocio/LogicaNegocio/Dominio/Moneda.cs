﻿using Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Dominio
{
    public class Moneda
    {

        public int Id { get; set; }

        public String Nombre { get; set; }

        public decimal CovertirADolares(decimal monto, decimal cotizacion)
        {
            if(this.Nombre == "Pesos")
            {
                monto = monto / cotizacion;
            }
            return monto;
        }

        
    }
}
