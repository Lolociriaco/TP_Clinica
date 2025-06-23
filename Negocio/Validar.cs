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
            string query = "SELECT M.ID_USUARIO, M.NOMBRE_MED, M.APELLIDO_MED, M.DNI_MED, M.SEXO_MED, M.NACIONALIDAD_MED, M.DIRECCION_MED, M.FECHANAC_MED," +
                " LOC.NOMBRE_LOC, M.ID_LOC_MED, M.ID_PROV_MED, PROV.NOMBRE_PROV, ESP.NOMBRE_ESP, M.ID_ESP_MED, M.TELEFONO_MED, M.CORREO_MED, M.DIAS_HORARIO_MED FROM MEDICOS M" +
                " INNER JOIN LOCALIDADES LOC ON LOC.ID_LOC = M.ID_LOC_MED INNER JOIN PROVINCIAS PROV ON PROV.ID_PROV = M.ID_PROV_MED INNER JOIN " +
                " ESPECIALIDADES ESP ON ESP.ID_ESP = M.ID_ESP_MED WHERE M.ACTIVO_MED = 1";
            DB datos = new DB();
            SqlDataAdapter adapter = datos.ObtenerAdaptador(query);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Medicos");
            return ds.Tables["Medicos"];
        }

        public DataTable ObtenerPacientes()
        {
            string query = "SELECT P.DNI_PAC, P.NOMBRE_PAC, P.APELLIDO_PAC, P.SEXO_PAC, P.NACIONALIDAD_PAC, P.DIRECCION_PAC," +
                " P.FECHANAC_PAC, LOC.NOMBRE_LOC, P.ID_LOC_PAC, P.ID_PROV_PAC, PROV.NOMBRE_PROV, P.TELEFONO_PAC," +
                " P.CORREO_PAC FROM PACIENTES P INNER JOIN LOCALIDADES LOC ON LOC.ID_LOC = P.ID_LOC_PAC" +
                " INNER JOIN PROVINCIAS PROV ON PROV.ID_PROV = P.ID_PROV_PAC WHERE P.ACTIVO_PAC = 1";

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
        public DataTable ObtenerSexoPaciente()
        {
            string query = "SELECT DISTINCT SEXO_PAC FROM PACIENTES WHERE SEXO_PAC IS NOT NULL";
            DB datos = new DB();
            SqlDataAdapter adapter = datos.ObtenerAdaptador(query);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Sexos");
            return ds.Tables["Sexos"];
        }

        public DataTable ObtenerSexoMedico()
        {
            string query = "SELECT DISTINCT SEXO_MED FROM MEDICOS WHERE SEXO_MED IS NOT NULL";
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
            string query = "INSERT INTO MEDICOS (ID_USUARIO, DNI_MED, NOMBRE_MED, APELLIDO_MED, SEXO_MED, NACIONALIDAD_MED, FECHANAC_MED, DIRECCION_MED, ID_LOC_MED, ID_PROV_MED, CORREO_MED, TELEFONO_MED, ID_ESP_MED, DIAS_HORARIO_MED) " +
                         "VALUES (@id_usuario, @dni, @nombre, @apellido, @sexo, @nacionalidad, @fecha, @direccion, @localidad, @provincia, @correo, @telefono, @especialidad, @diasYHorariosAtencion)";

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
                new SqlParameter("@id_usuario", medico._id_usuario),
            };

            DB datos = new DB();
            datos.EjecutarInsert(query, parametros);
        }

        public void InsertarHorarioMedico(int idUsuario, string diaSemana, TimeSpan horaInicio, TimeSpan horaFin)
        {
            string query = "INSERT INTO HORARIOS_MEDICOS (ID_USUARIO_MEDICO, DIA_SEMANA, HORA_INICIO, HORA_FIN) " +
                           "VALUES (@idUsuario, @dia, @horaInicio, @horaFin)";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idUsuario", idUsuario),
                new SqlParameter("@dia", diaSemana),
                new SqlParameter("@horaInicio", horaInicio),
                new SqlParameter("@horaFin", horaFin)
            };

            DB db = new DB();
            db.EjecutarInsert(query, parametros);
        }
    }
}