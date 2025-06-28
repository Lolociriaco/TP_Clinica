using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class UserManager
    {

        // BAJA LOGICA DEL MEDICO
        public bool deleteDoctor(int id)
        {
            DB db = new DB();
            string query = "UPDATE DOCTOR SET ACTIVE_DOC = 0 WHERE ID_USER = @id";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };

            return db.updateUser(query, parametros);
        }

        // BAJA LOGICA DEL PACIENTE
        public bool deletePatient(int dni)
        {
            DB db = new DB();
            string query = "UPDATE PATIENTS SET ACTIVE_PAT = 0 WHERE DNI_PAT = @dni";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@dni", dni)
            };

            return db.updateUser(query, parametros);
        }

        // MODIFICACION DEL USUARIO
        public bool modificarUsuario(string user, string newPassword = null, string newUser = null)
        {
            List<string> sets = new List<string>();
            List<SqlParameter> parametros = new List<SqlParameter>();

            if (newUser != null)
            {
                sets.Add("USERNAME = @nuevoUsuario");
                parametros.Add(new SqlParameter("@nuevoUsuario", newUser));
            }

            if (newPassword != null)
            {
                sets.Add("PASSWORD_USER = @nuevaPass");
                parametros.Add(new SqlParameter("@nuevaPass", newPassword));
            }

            // WHERE con el usuario original
            parametros.Add(new SqlParameter("@usuarioOriginal", user));

            string query = "UPDATE USERS SET " + string.Join(", ", sets) + " WHERE USERNAME = @usuarioOriginal";

            DB db = new DB();

            return db.updateUser(query, parametros.ToArray());
        }

        public bool medicoDisponible(string day, int id_user)
        {
            string query = "SELECT COUNT(*) FROM DOCTOR_SCHEDULES WHERE WEEKDAY_SCH = @day AND ID_USER_DOCTOR = @id_user";
            SqlParameter[] parametros = {
                new SqlParameter("@day", day),
                new SqlParameter("@id_user", id_user)
            };

            DB db = new DB();
            int cantidad = Convert.ToInt32(db.EjecutarEscalar(query, parametros));
            return cantidad > 0;
        }

        public WorkingHours ObtenerHorasTrabajadas(int id_user)
        {
            string query = "SELECT TIME_START, TIME_END FROM DOCTOR_SCHEDULES WHERE ID_USER_DOCTOR = @id_user";
            SqlParameter[] parametros = {
                new SqlParameter("@id_user",id_user)
            };

            DB db = new DB();

            return db.ExecWorkingHours(query, parametros);
        }

        public DataTable horariosDisponibles(DateTime date)
        {
            string consulta = "SELECT , NAME_CITY FROM CITY WHERE ID_STATE_CITY = @date";
            SqlParameter[] parametros = new SqlParameter[]
            {
                 new SqlParameter("@date", date.Date)
            };

            DB accesoDatos = new DB();
            return accesoDatos.ObtenerDataTable(consulta, parametros);
        }

        public List<TimeSpan> ObtenerTurnosAsignados(int id_doctor, DateTime date)
        {
            string query = "SELECT TIME_APPO FROM APPOINTMENT WHERE ID_USER_DOCTOR = @id_doctor AND DATE_APPO = @date";
            SqlParameter[] parametros = {
            new SqlParameter("@id_doctor", id_doctor),
            new SqlParameter("@date", date.Date)
    };

            DB db = new DB();
            DataTable dt = db.ObtenerDataTable(query, parametros); // asumimos que lo devuelve como DataTable

            List<TimeSpan> turnosOcupados = new List<TimeSpan>();
            foreach (DataRow row in dt.Rows)
            {
                turnosOcupados.Add((TimeSpan)row["TIME_APPO"]);
            }

            return turnosOcupados;
        }


        // ACTUALIZACION DEL MEDICO
        public bool updateDoctor(int idUsuario, string nombre, string apellido, string dni,
                                 string direccion, string correo, string telefono, string nacionalidad, int idEsp, DateTime fechaNac,
                                 string sexo, int idLoc, int idProv)
        {
            DB db = new DB();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@nombre", nombre),
                new SqlParameter("@apellido", apellido),
                new SqlParameter("@dni", dni),
                new SqlParameter("@direccion", direccion),
                new SqlParameter("@correo", correo),
                new SqlParameter("@telefono", telefono),
                new SqlParameter("@nacionalidad", nacionalidad),
                new SqlParameter("@idEsp", idEsp),
                new SqlParameter("@fechaNac", fechaNac),
                new SqlParameter("@sexo", sexo),
                new SqlParameter("@idLoc", idLoc),
                new SqlParameter("@idProv", idProv),
                new SqlParameter("@id", idUsuario)
            };

            string query = @"
                UPDATE DOCTOR SET
                    NAME_DOC = @nombre,
                    SURNAME_DOC = @apellido,
                    DNI_DOC = @dni,
                    ADDRESS_DOC = @direccion,
                    EMAIL_DOC = @correo,
                    PHONE_DOC = @telefono,
                    NATIONALITY_DOC = @nacionalidad,
                    ID_SPE_DOC = @idEsp,
                    DATEBIRTH_DOC = @fechaNac,
                    GENDER_DOC = @sexo,
                    ID_CITY_DOC = @idLoc,
                    ID_STATE_DOC = @idProv
                WHERE ID_USER = @id";


            return db.updateUser(query, parametros);
        }

        // ACTUALIZACION DEL PACIENTE
        public bool updatePatient(string nombre, string apellido, string dni,
                                string direccion, string correo, string telefono, string nacionalidad, DateTime fechaNac,
                                string sexo, int idLoc, int idProv)
        {
            DB db = new DB();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@nombre", nombre),
                new SqlParameter("@apellido", apellido),
                new SqlParameter("@dni", dni),
                new SqlParameter("@direccion", direccion),
                new SqlParameter("@correo", correo),
                new SqlParameter("@telefono", telefono),
                new SqlParameter("@nacionalidad", nacionalidad),
                new SqlParameter("@fechaNac", fechaNac),
                new SqlParameter("@sexo", sexo),
                new SqlParameter("@idLoc", idLoc),
                new SqlParameter("@idProv", idProv)
            };
            string query = @"
                UPDATE PATIENTS SET
                    NAME_PAT = @nombre,
                    SURNAME_PAT = @apellido,
                    ADDRESS_PAT = @direccion,
                    EMAIL_PAT = @correo,
                    PHONE_PAT = @telefono,
                    NATIONALITY_PAT = @nacionalidad,
                    DATEBIRTH_PAT = @fechaNac,
                    GENDER_PAT = @sexo,
                    ID_CITY_PAT = @idLoc,
                    ID_STATE_PAT = @idProv
                WHERE DNI_PAT = @dni";


            return db.updateUser(query, parametros);
        }

    }
}
