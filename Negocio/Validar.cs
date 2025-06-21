using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Datos;
using System.Globalization;
using Entidades;


namespace Negocio
{
    public class Validar
    {

        public bool ValidarUsuario(string user, string password)
        {

            // Verificar que la contraseña tenga al menos 6 caracteres
            if (password.Length < 6) return false;


            string query = "SELECT * FROM USUARIOS WHERE USUARIO = @user AND CONTRASENA = @password";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@user", user),
                new SqlParameter("@pass", password)
            };

            DB db = new DB();
            bool existe = db.validarUser(query, parametros);

            return existe;
        }

        public bool ValidarCambioUsuario(string user)
        {
            string query = "SELECT * FROM USUARIOS WHERE USUARIO = @user";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@user", user),
            };

            DB db = new DB();
            bool existe = db.validarUser(query, parametros);

            return existe;
        }

        public DataTable ObtenerMedicos()
        {
            string query = "SELECT \r\n                        M.ID_USUARIO,\r\n                        M.NOMBRE_MED,\r\n                        M.APELLIDO_MED,\r\n                        M.DNI_MED,\r\n                        M.SEXO_MED,\r\n                        M.NACIONALIDAD_MED,\r\n                        M.DIRECCION_MED,\r\n                        M.FECHANAC_MED,\r\n                        LOC.NOMBRE_LOC,\r\n                        M.ID_LOC_MED,\r\n                        M.ID_PROV_MED,\r\n                        PROV.NOMBRE_PROV,\r\n                        ESP.NOMBRE_ESP,\r\n                        M.TELEFONO_MED,\r\n                        M.CORREO_MED,\r\n                        M.DIAS_HORARIO_MED \r\n                    FROM MEDICOS M\r\n                        INNER JOIN LOCALIDADES LOC ON LOC.ID_LOC = M.ID_LOC_MED\r\n                        INNER JOIN PROVINCIAS PROV ON PROV.ID_PROV = M.ID_PROV_MED\r\n                        INNER JOIN ESPECIALIDADES ESP ON ESP.ID_ESP = M.ID_ESP_MED\r\n                    WHERE M.ACTIVO_MED = 1";
            DB datos = new DB();
            SqlDataAdapter adapter = datos.ObtenerAdaptador(query);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Medicos");
            return ds.Tables["Medicos"];
        }

        public DataTable ObtenerPacientes()
        {
            string query = "SELECT * FROM PACIENTES";
            DB datos = new DB();
            SqlDataAdapter adapter = datos.ObtenerAdaptador(query);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Pacientes");
            return ds.Tables["Pacientes"];
        }
        public DataTable ObtenerTurnos()
        {
            string query = "SELECT * FROM TURNOS";
            DB datos = new DB();
            SqlDataAdapter adapter = datos.ObtenerAdaptador(query);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Turnos");
            return ds.Tables["Turnos"];
        }
        public DataTable ObtenerSexos()
        {
            string query = "SELECT DISTINCT SEXO_PAC FROM PACIENTES WHERE SEXO_PAC IS NOT NULL";
            DB datos = new DB();
            SqlDataAdapter adapter = datos.ObtenerAdaptador(query);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Sexos");
            return ds.Tables["Sexos"];
        }
        public DataTable ObtenerEspecialidades()
        {
            string query = "SELECT NOMBRE_ESP, ID_ESP FROM ESPECIALIDADES";
            DB datos = new DB();
            SqlDataAdapter adapter = datos.ObtenerAdaptador(query);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Especialidades");
            return ds.Tables["Especialidades"];
        }
        public DataTable ObtenerProvincia()
        {
            string query = "SELECT ID_PROV, NOMBRE_PROV FROM PROVINCIAS";
            DB datos = new DB();
            SqlDataAdapter adapter = datos.ObtenerAdaptador(query);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Provincias");
            return ds.Tables["Provincias"];
        }

        public DataTable ObtenerLocalidad()
        {
            string query = "SELECT ID_LOC, NOMBRE_LOC FROM LOCALIDADES";
            DB datos = new DB();
            SqlDataAdapter adapter = datos.ObtenerAdaptador(query);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Localidades");
            return ds.Tables["Localidades"];
        }
        public DataTable ObtenerDias()
        {
            string query = "SELECT DISTINCT DIA_SEMANA FROM HORARIOS_MEDICOS WHERE DIA_SEMANA IS NOT NULL";
            DB datos = new DB();
            SqlDataAdapter adapter = datos.ObtenerAdaptador(query);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Dias");
            return ds.Tables["Dias"];
        }
        public void AgregarPaciente(Paciente paciente)
        {
            string query = "INSERT INTO PACIENTES (DNI_PAC, NOMBRE_PAC, APELLIDO_PAC, SEXO_PAC, NACIONALIDAD_PAC, FECHANAC_PAC, DIRECCION_PAC, ID_LOC_PAC, ID_PROV_PAC, CORREO_PAC, TELEFONO_PAC) " +
                           "VALUES (@dni, @nombre, @apellido, @sexo, @nacionalidad, @fecha, @direccion, @localidad, @provincia, @correo, @telefono)";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@dni", paciente._dni),
                new SqlParameter("@nombre", paciente._nombre),
                new SqlParameter("@apellido", paciente._apellido),
                new SqlParameter("@sexo", paciente._sexo),
                new SqlParameter("@nacionalidad", paciente._nacionalidad),
                new SqlParameter("@fecha", paciente._fechaNacimiento),
                new SqlParameter("@direccion", paciente._direccion),
                new SqlParameter("@localidad", paciente._localidad),
                new SqlParameter("@provincia", paciente._provincia),
                new SqlParameter("@correo", paciente._correoElectronico),
                new SqlParameter("@telefono", paciente._telefono),
            };

            DB datos = new DB();
            datos.EjecutarInsert(query, parametros);
        }


        public int AgregarUsuario(Usuario user)
        {
            string query = "INSERT INTO USUARIOS (USUARIO, CONTRASENA, TIPO_USUARIO) " +
                           "VALUES (@usuario, @contrasena, @tipoUsuario); SELECT SCOPE_IDENTITY();";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@usuario", user.NombreUsuario),
                new SqlParameter("@contrasena", user.Contrasena),
                new SqlParameter("@tipoUsuario", user.TipoUsuario)
            };

            DB datos = new DB();
            using (SqlConnection conn = datos.obtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddRange(parametros);
                conn.Open();
                object result = cmd.ExecuteScalar();
                return Convert.ToInt32(result); // devuelve el ID generado
            }
        }

        public void AgregarMedico(Medico medico)
        {
            string query = "INSERT INTO MEDICOS (DNI_MED, NOMBRE_MED, APELLIDO_MED, SEXO_MED, NACIONALIDAD_MED, FECHANAC_MED, DIRECCION_MED, ID_LOC_MED, ID_PROV_MED, CORREO_MED, TELEFONO_MED, ID_ESP_MED, DIAS_HORARIO_MED, ID_USUARIO) " +
                         "VALUES (@dni, @nombre, @apellido, @sexo, @nacionalidad, @fecha, @direccion, @localidad, @provincia, @correo, @telefono, @especialidad, @diasYHorariosAtencion, @legajo)";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@dni", medico._dni),
                new SqlParameter("@nombre", medico._nombre),
                new SqlParameter("@apellido", medico._apellido),
                new SqlParameter("@sexo", medico._sexo),
                new SqlParameter("@nacionalidad", medico._nacionalidad),
                new SqlParameter("@fecha", medico._fechaNacimiento),
                new SqlParameter("@direccion", medico._direccion),
                new SqlParameter("@localidad", medico._localidad),
                new SqlParameter("@provincia", medico._provincia),
                new SqlParameter("@correo", medico._correoElectronico),
                new SqlParameter("@telefono", medico._telefono),
                new SqlParameter("@especialidad", medico._especialidad),
                new SqlParameter("@diasYHorariosAtencion", medico._diasYHorariosAtencion),
                new SqlParameter("@legajo", medico._legajo),
            };

            DB datos = new DB();
            datos.EjecutarInsert(query, parametros);
        }

        /*public void eliminarProducto(Medicos medico)
        {
            string consulta = "DELETE FROM MEDICOS WHERE DNI_MEDICO = " + medico.DNI_MEDICO;
            DB datos = new DB();
            datos.ejecutarConsulta(consulta);
        }

        public void ActualizarProducto(Medicos medico)
        {
            string consulta = $"UPDATE MEDICOS SET Username = '{medico.Username}', DNI_MEDICO = {medico.DNI_MEDICO.ToString(CultureInfo.InvariantCulture)}";
            DB datos = new DB();
            datos.ejecutarConsulta(consulta);
        }*/
    }
}