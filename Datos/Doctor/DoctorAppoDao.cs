using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Doctor
{
    public class DoctorAppoDao
    {
        public DataTable ObtenerTurnosFiltrados(string DNI_PAT, string DAY_APPO, string todayOrTomorrow, string state)
        {
            string query = "SELECT A.ID_APPO, A.DNI_PAT_APPO, A.DATE_APPO, A.TIME_APPO, A.STATE_APPO, A.OBSERVATION_APPO," +
                "P.NAME_PAT, P.SURNAME_PAT, P.GENDER_PAT " +
                "FROM APPOINTMENT A " +
                "INNER JOIN PATIENTS P ON P.DNI_PAT = A.DNI_PAT_APPO " +
                "WHERE P.ACTIVE_PAT = 1";

            List<SqlParameter> parametros = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(DNI_PAT))
            {
                query += " AND A.DNI_PAT_APPO LIKE @dni";
                parametros.Add(new SqlParameter("@dni", DNI_PAT + "%"));
            }

            if (!string.IsNullOrEmpty(DAY_APPO))
            {
                query += " AND A.DATE_APPO = @day";
                parametros.Add(new SqlParameter("@day", DAY_APPO));
            }

            if (!string.IsNullOrEmpty(todayOrTomorrow))
            {
                if (todayOrTomorrow == "TODAY")
                {
                    query += " AND A.DATE_APPO = @todayDate";
                    parametros.Add(new SqlParameter("@todayDate", DateTime.Today));
                }

                else if (todayOrTomorrow == "TOMORROW")
                {
                    query += " AND A.DATE_APPO = @tomorrowDate";
                    parametros.Add(new SqlParameter("@tomorrowDate", DateTime.Today.AddDays(1)));
                }
            }

            if (!string.IsNullOrEmpty(state))
            {
                query += " AND A.STATE_APPO = @state";
                parametros.Add(new SqlParameter("@state", state));
            }

            DB dB = new DB();
            return dB.ObtenerListDT(query, parametros);
        }
    }
}
