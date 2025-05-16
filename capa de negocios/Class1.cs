using System.Collections.Generic;
using capa_de_datos;
using capa_de_entidades;

namespace capa_de_negocios
{
    public class ProductLogic
    {
        private readonly ProductData data = new ProductData();

        public List<Product> ObtenerProductos()
            => data.Listar();

        public List<Product> BuscarPorNombre(string nombre)
            => data.BuscarPorNombre(nombre);

        public void InsertarProducto(Product p)
            => data.Insertar(p);

        public void ActualizarProducto(Product p)
            => data.Actualizar(p);

        public void EliminarProducto(int productId)
            => data.EliminarLogico(productId);
    }
}
