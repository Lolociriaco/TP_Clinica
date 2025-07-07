using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos.Admin
{
    public class AdminAppoDao
    {
        // CONSULTA PARA OBTENER LOS MEDICOS DISPONIBLES
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

        // CONSULTA PARA OBTENER LAS HORAS TRABAJADAS X MEDICO
        public DoctorSchedule ObtenerHorasTrabajadas(int id_user, string day)
        {
            string query = "SELECT TIME_START, TIME_END FROM DOCTOR_SCHEDULES WHERE ID_USER_DOCTOR = @id_user AND WEEKDAY_SCH = @day";

            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id_user", id_user);
                cmd.Parameters.AddWithValue("@day", day);
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

        // CONSULTA PARA OBTENER LOS TURNOS ASIGNADOS X CADA MEDICO
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

        // CONSULTA PARA INSERTAR TURNOS
        public void CargarTurno(Turnos turno)
        {
            string query = "INSERT INTO APPOINTMENT (ID_USER_DOCTOR, DNI_PAT_APPO, DATE_APPO, TIME_APPO) " +
                         "VALUES (@id_usuario_med, @dni_paciente, @fecha_turno, @hora_turno)";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@id_usuario_med", turno.IdUsuarioMedico),
                new SqlParameter("@dni_paciente", turno.DniPacTurno),
                new SqlParameter("@fecha_turno", turno.FechaTurno),
                new SqlParameter("@hora_turno", turno.HoraTurno)
            };

            DB datos = new DB();
            datos.EjecutarInsert(query, parametros);
        }
    }
}
