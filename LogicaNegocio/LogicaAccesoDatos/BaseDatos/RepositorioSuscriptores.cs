﻿using Excepciones;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.BaseDatos
{
    public class RepositorioSuscriptores : IRepositorioSuscriptores
    {

        public CobrosContext Contexto { get; set; }

        public RepositorioSuscriptores(CobrosContext context)
        {
            Contexto = context;
        }


        public void Add(Suscriptor obj)
        {


            try
            {
                obj.Validar();
                Rol elRol = Contexto.Roles.FirstOrDefault(e => e.Nombre == "Suscriptor");
                Pais elPais = Contexto.Paises.Find(obj.PaisId);

                if (elRol != null)
                {
                    if (elPais != null)
                    {
                        obj.Pais = elPais;
                        obj.UsuarioLogin.RolDeUsuario = elRol;
                        Contexto.Add(obj);
                        Contexto.SaveChanges();
                    }
                    else
                    {
                        throw new SuscriptorException("Debe seleccionar un pais para el suscriptor");
                    }


                }
                else
                {
                    throw new SuscriptorException("Error al asignar rol");
                }
            }
            catch (SuscriptorException ex)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public IEnumerable<Suscriptor> FindAll()
        {
            throw new NotImplementedException();
        }

        public Suscriptor FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Suscriptor obj)
        {
            throw new NotImplementedException();
        }
    }
}
