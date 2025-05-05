using CapaEntidad;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DatosProduct
    {
        private readonly string connectionString = "Data Source=LAPTOP-DELL;Initial Catalog=Lab07DB;User ID=userJhon;Password=Tecsup00;TrustServerCertificate=True;";
        //private readonly string connectionString = "Data Source=LAB411-005\\SQLEXPRESS;Initial Catalog=Lab07DB;Integrated Security=True;TrustServerCertificate=True";
        //private readonly string connectionString = "Data Source=LAB411-003\\SQLEXPRESS;Initial Catalog=Lab07DB;User ID=userJhon;Password=Tecsup00;TrustServerCertificate=True;";

        public List<EntidadProduct> ObtenerProducts()
        {
            var lista = new List<EntidadProduct>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("USP_ListarProducts", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new EntidadProduct
                    {
                        ProductId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Price = reader.GetDecimal(2),
                        Stock = reader.GetInt32(3)
                    });
                }
            }

            return lista;
        }

        public List<EntidadProduct> ObtenerProductsPaginado(int page, int pageSize)
        {
            var lista = new List<EntidadProduct>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("USP_ListarProductosPaginado", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Page", page);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new EntidadProduct
                    {
                        ProductId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Price = reader.GetDecimal(2),
                        Stock = reader.GetInt32(3)
                    });
                }
            }

            return lista;
        }

        public List<EntidadProduct> BuscarProductosPaginado(string nombre, int page, int pageSize)
        {
            var lista = new List<EntidadProduct>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("USP_ListarProductosPaginadoFiltrado", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Page", page);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new EntidadProduct
                    {
                        ProductId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Price = reader.GetDecimal(2),
                        Stock = reader.GetInt32(3)
                    });
                }
            }

            return lista;
        }

        public void RegistrarProducto(EntidadProduct producto)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("USP_InsertarProducto", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", producto.Name);
                cmd.Parameters.AddWithValue("@price", producto.Price);
                cmd.Parameters.AddWithValue("@stock", producto.Stock);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
