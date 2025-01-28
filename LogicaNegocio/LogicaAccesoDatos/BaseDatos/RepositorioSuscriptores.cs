using Excepciones;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
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
                        if(obj.UsuarioLogin != null)
                        {
                            obj.Pais = elPais;
                            obj.UsuarioLogin.RolDeUsuario = elRol;

                            // Hash the password
                            obj.UsuarioLogin.ValidarContrasena(obj.UsuarioLogin.Password);
                            obj.UsuarioLogin.Password = BCrypt.Net.BCrypt.HashPassword(obj.UsuarioLogin.Password);

                            Contexto.Add(obj);
                            Contexto.SaveChanges();
                        }
                        else
                        {
                            throw new SuscriptorException("El suscriptor debe tener un usuario asociado");
                        }
                        
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
            try
            {
                Suscriptor suscriptor = Contexto.Suscriptores
                .Include(s => s.UsuarioLogin)
                .FirstOrDefault(s => s.Id == id);
                if (suscriptor == null)
                {
                    throw new SuscriptorException("Suscriptor no encontrado");
                }
                return suscriptor;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Suscriptor obj)
        {
            try
            {
                Suscriptor elSuscriptor = FindById(obj.Id);

                if (elSuscriptor != null)
                {
                    Contexto.Entry(elSuscriptor).State = EntityState.Detached;
                    Contexto.Entry(obj).State = EntityState.Modified;

                    obj.Validar();
                    Rol elRol = Contexto.Roles.FirstOrDefault(e => e.Nombre == "Suscriptor");
                    Pais elPais = Contexto.Paises.Find(obj.PaisId);

                    if (elRol != null)
                    {
                        if (elPais != null)
                        {
                            obj.Pais = elPais;
                            obj.UsuarioLogin.RolDeUsuario = elRol;

                            // Hash de contrasena si fue cambiada
                            if (!BCrypt.Net.BCrypt.Verify(obj.UsuarioLogin.Password, elSuscriptor.UsuarioLogin.Password))
                            {
                                obj.UsuarioLogin.ValidarContrasena(obj.UsuarioLogin.Password);
                                obj.UsuarioLogin.Password = BCrypt.Net.BCrypt.HashPassword(obj.UsuarioLogin.Password);
                            }

                            Contexto.Suscriptores.Update(obj);
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
                else
                {
                    throw new SuscriptorException("Suscriptor no encontrado");
                }
            }
            catch (SuscriptorException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Suscriptor FindByIdUsuario(int idUsuario)
        {
            try
            {
                Suscriptor suscriptor = Contexto.Suscriptores
                    .Include(s => s.ClientesDelSuscriptor)
                    .Include(s => s.ServiciosDelSuscriptor)
                    .FirstOrDefault(s => s.UsuarioLogin.Id == idUsuario);

                if (suscriptor == null)
                {
                    throw new SuscriptorException("No se encontró un suscriptor con el ID de usuario especificado");
                }

                return suscriptor;
            }
            catch (Exception e)
            {
                throw new SuscriptorException("Error al obtener el suscriptor por ID de usuario", e);
            }
        }
    }
}
