using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Shared
{
    public class AuthDao
    {
        // CONSULTA PARA VALIDAR EL USUARIO Y CONTRASEÑA
        public string AuthUserAndRole(string user, string password)
        {
            // Collate As lo hace case sensitive - Accent Sensitive

            string query = "SELECT ROLE_USER FROM USERS WHERE USERNAME COLLATE Latin1_General_CS_AS = @user " +
                "AND PASSWORD_USER COLLATE Latin1_General_CS_AS = @password";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@user", user),
                new SqlParameter("@password", password)
            };

            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@user", user);
                cmd.Parameters.AddWithValue("@password", password);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader["ROLE_USER"].ToString();
                    }
                    else
                    {
                        return null; // No existe o no coincide
                    }
                }
            }
        }

        // CONSULTA PARA VALIDAR SI EL USUARIO YA EXISTE
        public bool validateUserExist(string user)
        {
            string query = "SELECT * FROM USERS WHERE USERNAME = @user";

            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@user", user);

                conn.Open();
                // Devuelve true si el user existe
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.HasRows;
                }
            }
        }
    }
}
