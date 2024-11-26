using Excepciones;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.BaseDatos
{
    public class RepositorioCobros : IRepositorioCobros
    {


        public CobrosContext Contexto { get; set; }

        public RepositorioCobros(CobrosContext context)
        {
            Contexto = context;
        }
        public void Add(CobroRecibido obj)
        {
            try
            {
                obj.Validar();

                Moneda laMoneda = Contexto.Monedas.Find(obj.MonedaDelCobroId);
                MedioDePago elMedio = Contexto.MediosDePago.Find(obj.MedioPagoId);

                if (laMoneda != null )
                {
                    if (elMedio != null) {
                        obj.MonedaDelCobro= laMoneda;
                        obj.MedioPago = elMedio;

                        Contexto.Add(obj);
                        Contexto.SaveChanges();

                    }
                    else
                    {
                        throw new CobroRecibidoException("Debe seleccionar un Medio de Pago ");
                    }


                }
                else
                {
                    throw new CobroRecibidoException("Debe seleccionar una Moneda");
                }
            }
            catch (CobroRecibidoException ex)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public IEnumerable<CobroRecibido> FindAll()
        {

            try
            {
                List<CobroRecibido> losCobrosRecibidos = Contexto.CobrosRecibidos
                    .Include(co => co.MedioPago)
                    .Include(co => co.MonedaDelCobro)
                    .ToList();

                if (losCobrosRecibidos != null)
                {
                    return losCobrosRecibidos;
                }
                else
                {
                    throw new CobroRecibidoException("No hay Cobros Recibidos en el sistema");
                }

            }
            catch (CobroRecibidoException ex)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public CobroRecibido FindById(int id)
        {
            return Contexto.CobrosRecibidos.Where(co => co.Id == id).SingleOrDefault();
        }

        public void Remove(int id)
        {
            CobroRecibido elCobroAEliminar = Contexto.CobrosRecibidos.Find(id);
            try
            {
                if (elCobroAEliminar != null)
                {
                    //TODO:validar registros en otras tablas
                    Contexto.Remove(elCobroAEliminar);
                    Contexto.SaveChanges();
                }
                else
                {
                    throw new CobroRecibidoException("No se pudo dar la baja, el Cobro a Eliminar no existe en el sistema");
                }

            }
            catch (CobroRecibidoException ex)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void Update(CobroRecibido obj)
        {
            try
            {
                Moneda laMoneda = Contexto.Monedas.Find(obj.MonedaDelCobroId);
                MedioDePago elMedio = Contexto.MediosDePago.Find(obj.MedioPagoId);
                

                if (laMoneda != null)
                {
                    if (elMedio !=null) {

                        //Valida Cobro recibido
                        obj.MedioPago = elMedio;
                        obj.MonedaDelCobro = laMoneda;

                        obj.Validar();
                        Contexto.CobrosRecibidos.Update(obj);
                        Contexto.SaveChanges();
                    }
                    else
                    {
                        throw new CobroRecibidoException("El medio seleccionado no existe en el sistema");
                    }

                }
                else
                {
                    throw new CobroRecibidoException("La Moneda seleccionada no existe en el sistema");
                }

            }
            catch (ServicioException ce)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
