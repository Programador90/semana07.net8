// Archivo: capa_de_datos/ProductData.cs

using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using capa_de_entidades;

namespace capa_de_datos
{
    public class ProductData
    {
        private readonly string connectionString =
            @"Data Source=DESKTOP-4HL5V5T\SQLEXPRESS2017;
              Initial Catalog=FacturaDB;
              Integrated Security=True;
              TrustServerCertificate=True;";

        public List<Product> Listar()
        {
            var lista = new List<Product>();
            using var con = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(
                "SELECT product_id, name, price, stock, active " +
                "FROM products WHERE active = 1", con);
            con.Open();
            using var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lista.Add(new Product
                {
                    ProductId = (int)dr["product_id"],
                    Name = dr["name"].ToString(),
                    Price = (decimal)dr["price"],
                    Stock = (int)dr["stock"],
                    Active = (bool)dr["active"]
                });
            }
            return lista;
        }

        public List<Product> BuscarPorNombre(string nombre)
        {
            var lista = new List<Product>();
            using var con = new SqlConnection(connectionString);
            using var cmd = new SqlCommand("spBuscarProductoPorNombre", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@nombre", nombre);
            con.Open();
            using var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lista.Add(new Product
                {
                    ProductId = (int)dr["product_id"],
                    Name = dr["name"].ToString(),
                    Price = (decimal)dr["price"],
                    Stock = (int)dr["stock"],
                    Active = (bool)dr["active"]
                });
            }
            return lista;
        }

        public void Insertar(Product p)
        {
            using var con = new SqlConnection(connectionString);
            using var cmd = new SqlCommand("spInsertarProducto", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@name", p.Name);
            cmd.Parameters.AddWithValue("@price", p.Price);
            cmd.Parameters.AddWithValue("@stock", p.Stock);
            cmd.Parameters.AddWithValue("@active", p.Active);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public void Actualizar(Product p)
        {
            using var con = new SqlConnection(connectionString);
            using var cmd = new SqlCommand("spActualizarProducto", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@product_id", p.ProductId);
            cmd.Parameters.AddWithValue("@name", p.Name);
            cmd.Parameters.AddWithValue("@price", p.Price);
            cmd.Parameters.AddWithValue("@stock", p.Stock);
            cmd.Parameters.AddWithValue("@active", p.Active);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public void EliminarLogico(int productId)
        {
            using var con = new SqlConnection(connectionString);
            using var cmd = new SqlCommand("spEliminarProductoLogico", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@product_id", productId);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
