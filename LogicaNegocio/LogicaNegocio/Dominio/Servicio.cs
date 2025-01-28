using Excepciones;
using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.Dominio
{
    public class Servicio : IValidar
    {
        public int Id { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }

        public Categoria CategoriaDelServicio { get; set; }
        public int CategoriaId { get; set; }

        public int SuscriptorId { get; set; }

        public void Validar()
        {
            ValidarNombre();
            ValidarDescripcion();
            ValidarCategoria();
        }

        private void ValidarNombre()
        {
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                throw new ServicioException("El nombre es obligatorio");
            }
            if (Nombre.Length < 3)
            {
                throw new ServicioException("El nombre debe tener al menos 3 caracteres");
            }
            if (Nombre.Length > 100)
            {
                throw new ServicioException("El nombre no debe exceder los 100 caracteres");
            }
        }

        private void ValidarDescripcion()
        {
            if (string.IsNullOrWhiteSpace(Descripcion))
            {
                throw new ServicioException("La descripción es obligatoria");
            }
            if (Descripcion.Length < 10)
            {
                throw new ServicioException("La descripción debe tener al menos 10 caracteres");
            }
            if (Descripcion.Length > 500)
            {
                throw new ServicioException("La descripción no debe exceder los 500 caracteres");
            }
        }

        private void ValidarCategoria()
        {
            this.CategoriaDelServicio.Validar();
            
        }
    }
}
