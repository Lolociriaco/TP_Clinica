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
        private string cadenaConexion = @"Data Source=LOLO\SQLEXPRESS;Initial Catalog=BDCLINICA_TPINTEGRADOR;Integrated Security=True;TrustServerCertificate=True";

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
        public void InsertarPaciente (Paciente pac, string query)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand(query, conn);
                comando.Parameters.AddWithValue("@dni", pac._dni);
                comando.Parameters.AddWithValue("@nombre", pac._nombre);
                comando.Parameters.AddWithValue("@apellido", pac._apellido);
                comando.Parameters.AddWithValue("@sexo", pac._sexo);
                comando.Parameters.AddWithValue("@nacionalidad", pac._nacionalidad);
                comando.Parameters.AddWithValue("@fecha", pac._fechaNacimiento);
                comando.Parameters.AddWithValue("@direccion", pac._direccion);
                comando.Parameters.AddWithValue("@localidad", pac._localidad);
                comando.Parameters.AddWithValue("@provincia", pac._provincia);
                comando.Parameters.AddWithValue("@correo", pac._correoElectronico);
                comando.Parameters.AddWithValue("@telefono", pac._telefono);

                conn.Open();
                comando.ExecuteNonQuery();
            }
        }
        public void InsertarMedico(Medico med, string query)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand(query, conn);
                comando.Parameters.AddWithValue("@dni", med._dni);
                comando.Parameters.AddWithValue("@nombre", med._nombre);
                comando.Parameters.AddWithValue("@apellido", med._apellido);
                comando.Parameters.AddWithValue("@sexo", med._sexo);
                comando.Parameters.AddWithValue("@nacionalidad", med._nacionalidad);
                comando.Parameters.AddWithValue("@fecha", med._fechaNacimiento);
                comando.Parameters.AddWithValue("@direccion", med._direccion);
                comando.Parameters.AddWithValue("@localidad", med._localidad);
                comando.Parameters.AddWithValue("@provincia", med._provincia);
                comando.Parameters.AddWithValue("@correo", med._correoElectronico);
                comando.Parameters.AddWithValue("@especialidad", med._especialidad);
                comando.Parameters.AddWithValue("@diasYHorariosAtencion", med._diasYHorariosAtencion);
                comando.Parameters.AddWithValue("@legajo", med._legajo);
                comando.Parameters.AddWithValue("@telefono", med._telefono);

                conn.Open();
                comando.ExecuteNonQuery();
            }
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