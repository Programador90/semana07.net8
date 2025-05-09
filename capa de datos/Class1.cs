using System.Collections.Generic;

using capa_de_entidades;
using Microsoft.Data.SqlClient;

namespace capa_de_datos
{
    public class ProductData
    {
        private string connectionString = "Data Source=DESKTOP-4HL5V5T\\SQLEXPRESS2017;Initial Catalog=FacturaDB;Integrated Security=True;TrustServerCertificate=True;";

        public List<Product> Listar()
        {
            var lista = new List<Product>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM products WHERE active = 1", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

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
            }
            return lista;
        }

        public List<Product> BuscarPorNombre(string nombre)
        {
            var lista = new List<Product>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spBuscarProductoPorNombre", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", nombre);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

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
            }
            return lista;
        }
        public void Insertar(Product p)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spInsertarProducto", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@name", p.Name);
                cmd.Parameters.AddWithValue("@price", p.Price);
                cmd.Parameters.AddWithValue("@stock", p.Stock);
                cmd.Parameters.AddWithValue("@active", p.Active);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
