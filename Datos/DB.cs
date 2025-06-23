using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data.Common;
using Entidades;

namespace Datos
{
    public class DB
    {
        private string cadenaConexion = @"Data Source=DESKTOP-GUU4RQA\SQLEXPRESS;Initial Catalog=BDCLINICA_TPINTEGRADOR;Integrated Security=True;TrustServerCertificate=True";


        public SqlDataAdapter ObtenerAdaptador(string consultaSQL)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            SqlDataAdapter adaptador = new SqlDataAdapter(consultaSQL, conexion);
            return adaptador;
        }

        public SqlConnection obtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }

        public bool updateUser(string query, SqlParameter[] parametros)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand comando = new SqlCommand(query, conn))
                {
                    comando.Parameters.AddRange(parametros);
                    conn.Open();
                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0; // Retorna true si se actualizó al menos un registro
                }
            }
        }

        public void ejecutarConsulta(string consulta)
        {
            SqlConnection conexion = obtenerConexion();
            SqlCommand cmd = new SqlCommand(consulta, conexion);
            conexion.Open();
            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        public void EjecutarInsert(string query, SqlParameter[] parametros)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand comando = new SqlCommand(query, conn))
                {
                    comando.Parameters.AddRange(parametros);
                    conn.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        public bool validarUser(string query, SqlParameter[] parametro)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {

                cmd.Parameters.AddRange(parametro);
                conn.Open();
                // Devuelve true si el user existe
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.HasRows;
                }
            }
        }

        public object EjecutarEscalar(string query, SqlParameter[] parametros)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand comando = new SqlCommand(query, conn))
                {
                    comando.Parameters.AddRange(parametros);
                    conn.Open();
                    return comando.ExecuteScalar(); // Devuelve un único valor
                }
            }
        }
    }
}