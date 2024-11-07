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
    public class RepositorioServicios : IRepositorioServicios
    {
        public CobrosContext Contexto { get; set; }

        public RepositorioServicios(CobrosContext context)
        {
            Contexto = context;
        }

        public void Add(Servicio obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Servicio> FindAll()
        {
            return Contexto.Servicios.ToList();
        }

        public Servicio FindById(int id)
        {
            return Contexto.Servicios.Where(ser => ser.Id == id).SingleOrDefault();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Servicio obj)
        {
            throw new NotImplementedException();
        }
    }
}
