using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class NegocioProduct
    {
        public List<EntidadProduct> ObtenerProductos()
        {
            DatosProduct datos = new DatosProduct();
            return datos.ObtenerProducts();
        }

        public List<EntidadProduct> ObtenerProductosPorNombre(string nombre)
        {
            DatosProduct datos = new DatosProduct();
            List<EntidadProduct> productos = datos.ObtenerProducts();

            var productosFiltrados = productos
                .Where(p => p.Name.ToLower().Contains(nombre.ToLower()))
                .ToList();

            return productosFiltrados;
        }

        public void RegistrarProducto(EntidadProduct producto)
        {
            DatosProduct datos = new DatosProduct();
            datos.RegistrarProducto(producto);
        }

    }
}
