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
    public class DatosCustomer
    {
        private readonly string connectionString = "Data Source=LAB411-005\\SQLEXPRESS;Initial Catalog=Neptuno;User ID=userJhon;Password=Tecsup00;TrustServerCertificate=True;";

        public List<EntidadCustomer> ObtenerCustomers()
        {
            var lista = new List<EntidadCustomer>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("USP_ListarCustomers", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new EntidadCustomer
                    {
                        CustomerId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Address = reader.GetString(2),
                        Phone = reader.GetString(3)
                    });
                }
            }

            return lista;
        }
    }
}
