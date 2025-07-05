using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Datos.Admin;
using Entidades;

namespace Negocio
{
    public class AdminDoctorManager
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

        public DoctorSchedule ObtenerHorasTrabajadas(int id_user, string day)
        {
            string query = "SELECT TIME_START, TIME_END FROM DOCTOR_SCHEDULES WHERE ID_USER_DOCTOR = @id_user AND WEEKDAY_SCH = @day";
            SqlParameter[] parametros = {
                new SqlParameter("@id_user",id_user),
                new SqlParameter("@day", day)
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
        public bool updateDoctor(Medico medico, string diaSeleccionado, string horaInicio, string horaFin)
        {
            DB db = new DB();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@nombre", medico.Nombre),
                new SqlParameter("@apellido", medico.Apellido),
                new SqlParameter("@dni", medico.DNI),
                new SqlParameter("@direccion", medico.Direccion),
                new SqlParameter("@correo", medico.CorreoElectronico),
                new SqlParameter("@telefono", medico.Telefono),
                new SqlParameter("@nacionalidad", medico.Nacionalidad),
                new SqlParameter("@diaSeleccionado", diaSeleccionado),
                new SqlParameter("@horarioInicio", horaInicio),
                new SqlParameter("@horarioFin", horaFin),
                new SqlParameter("@idEsp", medico.Especialidad),
                new SqlParameter("@fechaNac", medico.FechaNacimiento),
                new SqlParameter("@sexo", medico.Sexo),
                new SqlParameter("@idLoc", medico.Localidad),
                new SqlParameter("@idProv", medico.Provincia),
                new SqlParameter("@id", medico.IdUsuario)
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
                WHERE ID_USER = @id;

                IF EXISTS (
                    SELECT 1 FROM DOCTOR_SCHEDULES 
                    WHERE ID_USER_DOCTOR = @id AND WEEKDAY_SCH = @diaSeleccionado
                )
                BEGIN
                    UPDATE DOCTOR_SCHEDULES
                    SET TIME_START = @horarioInicio, TIME_END = @horarioFin
                    WHERE ID_USER_DOCTOR = @id AND WEEKDAY_SCH = @diaSeleccionado
                END
                ELSE
                BEGIN
                    INSERT INTO DOCTOR_SCHEDULES (ID_USER_DOCTOR, WEEKDAY_SCH, TIME_START, TIME_END)
                    VALUES (@id, @diaSeleccionado, @horarioInicio, @horarioFin)
                END
            
            ";

            return db.updateUser(query, parametros);
        }

        // ACTUALIZACION DEL PACIENTE
        public bool updatePatient(Paciente paciente)
        {
            DB db = new DB();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@nombre", paciente.Nombre),
                new SqlParameter("@apellido", paciente.Apellido),
                new SqlParameter("@dni", paciente.DNI),
                new SqlParameter("@direccion", paciente.Direccion),
                new SqlParameter("@correo", paciente.CorreoElectronico),
                new SqlParameter("@telefono", paciente.Telefono),
                new SqlParameter("@nacionalidad", paciente.Nacionalidad),
                new SqlParameter("@fechaNac", paciente.FechaNacimiento),
                new SqlParameter("@sexo", paciente.Sexo),
                new SqlParameter("@idLoc", paciente.Localidad),
                new SqlParameter("@idProv", paciente.Provincia)
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





        //-------------------------------------------------------------------------------------------------- 


        public DataTable ObtenerMedicos()
        {
            AdminDoctorDao doctorDao = new AdminDoctorDao();
            return doctorDao.ObtenerMedicos();
        }

        public DataTable ObtenerMedicosFiltrados(string state, string weekDay, string speciality, string user)
        {
            AdminDoctorDao doctorDao = new AdminDoctorDao();
            return doctorDao.ObtenerMedicosFiltrados(state, weekDay, speciality, user);
        }

        public DataTable ObtenerSexoMedico()
        {
            AdminDoctorDao admin = new AdminDoctorDao();
            return admin.ObtenerSexoMedico();
        }
    }
}
