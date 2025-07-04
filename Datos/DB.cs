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

        public SqlConnection obtenerConexion()
        {
            return new SqlConnection(Conexion.Cadena);
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
        
        public bool updateAppointment(string state, string observation, int id)
        {
            string query = @"
                UPDATE APPOINTMENT SET
                    STATE_APPO = @STATE,
                    OBSERVATION_APPO = @OBSERVATION
                WHERE ID_APPO = @id";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@STATE", state),
                new SqlParameter("@OBSERVATION", observation),
                new SqlParameter("@ID", id)
            };

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

        public bool validarUser(string query, SqlParameter[] parametro)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
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

        public DoctorSchedule ExecWorkingHours(string query, SqlParameter[] parametros)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddRange(parametros);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new DoctorSchedule
                        {
                            _TimeStart = reader.GetTimeSpan(0),
                            _TimeEnd = reader.GetTimeSpan(1)
                        };
                    }
                }
            }

            return null;
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