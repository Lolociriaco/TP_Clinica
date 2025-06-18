using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Datos
{
    public class DB
    {
        private string cadenaConexion = @"Data Source=CIRIACO\SQLEXPRESS;Initial Catalog=BDCLINICA_TPINTEGRADOR;Integrated Security=True;Encrypt=False";

        public SqlDataAdapter ObtenerAdaptador(string consultaSQL)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlDataAdapter adaptador = new SqlDataAdapter(consultaSQL, conexion);
            return adaptador;
        }

        private SqlConnection obtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }

        public void ejecutarConsulta(string consulta)
        {
            SqlConnection conexion = obtenerConexion();
            SqlCommand cmd = new SqlCommand(consulta, conexion);
            conexion.Open();
            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        public bool validarUser(string query, SqlParameter[] parametro)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {

                cmd.Parameters.Add(parametro);

                // Devuelve true si el user existe
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.HasRows;
                }
            }
        }
    }
}