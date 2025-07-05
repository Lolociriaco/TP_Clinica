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
    public class AdminDoctorDao
    {
        public DataTable ObtenerMedicos()
        {
            string query = @"
                SELECT D.ID_USER, U.USERNAME, D.NAME_DOC, D.SURNAME_DOC, D.DNI_DOC, D.GENDER_DOC,
                    D.NATIONALITY_DOC, D.ADDRESS_DOC, DATEBIRTH_DOC,
                    C.NAME_CITY, D.ID_CITY_DOC,
                    D.ID_STATE_DOC, S.NAME_STATE, SP.NAME_SPE, D.ID_SPE_DOC, 

                        (
                            SELECT STRING_AGG(A.WEEKDAY_SCH, ', ') 
                            FROM DOCTOR_SCHEDULES A 
                            WHERE A.ID_USER_DOCTOR = D.ID_USER
                        ) AS DIAS_TRABAJO,

                        -- Horas de inicio concatenadas
                        (
                            SELECT STRING_AGG(CONVERT(VARCHAR(5), A.TIME_START, 108), ', ') 
                            FROM DOCTOR_SCHEDULES A 
                            WHERE A.ID_USER_DOCTOR = D.ID_USER
                        ) AS HORA_ENTRADA,

                        -- Horas de fin concatenadas
                        (
                            SELECT STRING_AGG(CONVERT(VARCHAR(5), A.TIME_END, 108), ', ') 
                            FROM DOCTOR_SCHEDULES A 
                            WHERE A.ID_USER_DOCTOR = D.ID_USER
                        ) AS HORA_SALIDA,

                    D.PHONE_DOC, D.EMAIL_DOC

                FROM DOCTOR D
                INNER JOIN CITY C ON C.ID_CITY = D.ID_CITY_DOC
                INNER JOIN STATE S ON S.ID_STATE = D.ID_STATE_DOC
                INNER JOIN SPECIALITY SP ON SP.ID_SPE = D.ID_SPE_DOC
                INNER JOIN USERS U ON U.ID_USER = D.ID_USER
                WHERE D.ACTIVE_DOC = 1";

            using (SqlConnection connection = new SqlConnection(Conexion.Cadena))
            {
                SqlCommand cmd = new SqlCommand(query, connection);
                

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable ObtenerMedicosFiltrados(string state, string weekDay, string speciality, string user)
        {
            string queryBase = @"
        SELECT D.ID_USER, U.USERNAME, D.NAME_DOC, D.SURNAME_DOC, D.DNI_DOC, D.GENDER_DOC,
            D.NATIONALITY_DOC, D.ADDRESS_DOC, D.DATEBIRTH_DOC,
            C.NAME_CITY, D.ID_CITY_DOC,
            D.ID_STATE_DOC, S.NAME_STATE, SP.NAME_SPE, D.ID_SPE_DOC,

            (
                SELECT STRING_AGG(A.WEEKDAY_SCH, ', ') 
                FROM DOCTOR_SCHEDULES A 
                WHERE A.ID_USER_DOCTOR = D.ID_USER
            ) AS DIAS_TRABAJO,

            (
                SELECT STRING_AGG(CONVERT(VARCHAR(5), A.TIME_START, 108), ', ') 
                FROM DOCTOR_SCHEDULES A 
                WHERE A.ID_USER_DOCTOR = D.ID_USER
            ) AS HORA_ENTRADA,

            (
                SELECT STRING_AGG(CONVERT(VARCHAR(5), A.TIME_END, 108), ', ') 
                FROM DOCTOR_SCHEDULES A 
                WHERE A.ID_USER_DOCTOR = D.ID_USER
            ) AS HORA_SALIDA,

            D.PHONE_DOC, D.EMAIL_DOC

        FROM DOCTOR D
        INNER JOIN CITY C ON C.ID_CITY = D.ID_CITY_DOC
        INNER JOIN STATE S ON S.ID_STATE = D.ID_STATE_DOC
        INNER JOIN SPECIALITY SP ON SP.ID_SPE = D.ID_SPE_DOC
        INNER JOIN USERS U ON U.ID_USER = D.ID_USER
        WHERE D.ACTIVE_DOC = 1";

            List<string> condiciones = new List<string>();
            List<SqlParameter> parametros = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(state))
            {
                condiciones.Add("D.ID_STATE_DOC = @state");
                parametros.Add(new SqlParameter("@state", state));
            }

            if (!string.IsNullOrEmpty(weekDay))
            {
                condiciones.Add(@"D.ID_USER IN (
            SELECT ID_USER_DOCTOR FROM DOCTOR_SCHEDULES WHERE WEEKDAY_SCH = @weekDay
        )");
                parametros.Add(new SqlParameter("@weekDay", weekDay));
            }

            if (!string.IsNullOrEmpty(speciality))
            {
                condiciones.Add("D.ID_SPE_DOC = @speciality");
                parametros.Add(new SqlParameter("@speciality", speciality));
            }

            if (!string.IsNullOrEmpty(user))
            {
                condiciones.Add("U.USERNAME LIKE @user");
                parametros.Add(new SqlParameter("@user", "%" + user + "%"));
            }

            if (condiciones.Count > 0)
            {
                queryBase += " AND " + string.Join(" AND ", condiciones);
            }

            using (SqlConnection connection = new SqlConnection(Conexion.Cadena))
            {
                SqlCommand cmd = new SqlCommand(queryBase, connection);
                cmd.Parameters.AddRange(parametros.ToArray());

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        // CONSULTA PARA OBTENER SEXO DEL MEDICO
        public DataTable ObtenerSexoMedico()
        {
            string query = "SELECT DISTINCT GENDER_DOC FROM DOCTOR WHERE GENDER_DOC IS NOT NULL";
            DB datos = new DB();
            SqlDataAdapter adapter = datos.ObtenerAdaptador(query);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Sexos");
            return ds.Tables["Sexos"];
        }
    }
}
