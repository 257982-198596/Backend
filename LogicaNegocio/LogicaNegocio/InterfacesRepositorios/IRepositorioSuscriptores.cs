﻿using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioSuscriptores : IRepositorio<Suscriptor>
    {
        Suscriptor FindByIdUsuario(int idUsuario);
    }
}
