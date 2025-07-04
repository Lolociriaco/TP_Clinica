using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Admin
{
    public class AdminPatientsDao
    {
        public DataTable ObtenerPacientes(string state, string name, string dni, string sexo)
        {
            string query = @"
                SELECT 
                    P.DNI_PAT, P.NAME_PAT, P.SURNAME_PAT, 
                    P.GENDER_PAT, P.NATIONALITY_PAT, P.ADDRESS_PAT, P.DATEBIRTH_PAT, 
                    C.NAME_CITY, P.ID_CITY_PAT, S.NAME_STATE, P.ID_STATE_PAT, 
                    P.PHONE_PAT, P.EMAIL_PAT
                FROM PATIENTS P
                INNER JOIN CITY C ON C.ID_CITY = P.ID_CITY_PAT
                INNER JOIN STATE S ON S.ID_STATE = P.ID_STATE_PAT
                WHERE P.ACTIVE_PAT = 1";


            List<SqlParameter> parametros = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(state))
            {
                query += " AND P.ID_STATE_PAT = @state";
                parametros.Add(new SqlParameter("@state", state));
            }

            if (!string.IsNullOrEmpty(sexo))
            {
                query += " AND P.GENDER_PAT = @sexo";
                parametros.Add(new SqlParameter("@sexo", sexo));
            }

            if (!string.IsNullOrEmpty(name))
            {
                query += " AND P.NAME_PAT LIKE @name";
                parametros.Add(new SqlParameter("@name", "%" + name + "%"));
            }

            if (!string.IsNullOrEmpty(dni))
            {
                query += " AND P.DNI_PAT LIKE @dni";
                parametros.Add(new SqlParameter("@dni", dni + "%"));
            }

            DB dB = new DB();
            return dB.ObtenerListDT(query, parametros);
        }

        // CONSULTA PARA OBTENER SEXO DEL PACIENTE
        public DataTable ObtenerSexoPaciente()
        {
            string query = "SELECT DISTINCT GENDER_PAT FROM PATIENTS WHERE GENDER_PAT IS NOT NULL";
            DB datos = new DB();
            SqlDataAdapter adapter = datos.ObtenerAdaptador(query);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Sexos");
            return ds.Tables["Sexos"];
        }
    }
}
