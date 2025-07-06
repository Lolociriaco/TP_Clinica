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
        public SqlDataAdapter ObtenerAdaptador(string consultaSQL)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            SqlDataAdapter adaptador = new SqlDataAdapter(consultaSQL, conexion);
            return adaptador;
        }

        public bool updateUser(string query, SqlParameter[] parametros)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
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


        public void EjecutarInsert(string query, SqlParameter[] parametros)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            {
                using (SqlCommand comando = new SqlCommand(query, conn))
                {
                    comando.Parameters.AddRange(parametros);
                    conn.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        public object EjecutarEscalar(string query, SqlParameter[] parametros)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            {
                using (SqlCommand comando = new SqlCommand(query, conn))
                {
                    comando.Parameters.AddRange(parametros);
                    conn.Open();
                    return comando.ExecuteScalar(); // Devuelve un único valor
                }
            }
        }

        public DataTable ObtenerDataTable(string consulta, SqlParameter[] parametros)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.Cadena))
            {
                SqlCommand cmd = new SqlCommand(consulta, connection);
                if (parametros != null)
                {
                    cmd.Parameters.AddRange(parametros);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable ObtenerListDT(string consulta, List<SqlParameter> parametros)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.Cadena))
            {
                SqlCommand cmd = new SqlCommand(consulta, connection);
                if (parametros != null)
                {
                    cmd.Parameters.AddRange(parametros.ToArray());
                }

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}