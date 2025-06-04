using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Datos
{
    internal class DB
    {
        private string DbConnection = @"Data Source=CIRIACO\SQLEXPRESS;Initial Catalog=BDSucursales;Integrated Security=True;Encrypt=False";


        public bool validarUser(string query, SqlParameter[] parametro)
        {
            /*
            using (SqlConnection conn = new SqlConnection(DbConnection))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {

                cmd.Parameters.Add(parametro);

                // Devuelve true si el user existe
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.HasRows;
                }
            }
            */
            return true;
        }
    }
}
