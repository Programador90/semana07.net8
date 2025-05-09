using System.Collections.Generic;
using capa_de_datos;
using capa_de_entidades;

namespace capa_de_negocios
{
    public class ProductLogic
    {
        private ProductData data = new ProductData();

        public List<Product> ObtenerProductos()
        {
            return data.Listar();
        }

        public List<Product> BuscarPorNombre(string nombre)
        {
            return data.BuscarPorNombre(nombre);
        }
        public void InsertarProducto(Product p)
        {
            data.Insertar(p);
        }

    }
}
