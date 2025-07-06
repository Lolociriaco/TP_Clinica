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
    }
}
