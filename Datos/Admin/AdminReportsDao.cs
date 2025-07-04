using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Datos.Admin
{
    public class AdminReportsDao
    {
        public int ObtenerTotalTurnos()
        {
            string query = "SELECT COUNT(*) FROM APPOINTMENT";
            DB datos = new DB();
            object result = datos.EjecutarEscalar(query, new SqlParameter[0]); // Array vacío
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public DataTable MedicosConMasTurnos()
        {
            string query = @"
            SELECT TOP 10 
                D.ID_USER,
                D.DNI_DOC,
                D.NAME_DOC,
                D.SURNAME_DOC,
                D.ID_SPE_DOC,
                SP.NAME_SPE,

                COUNT(A.ID_APPO) AS TOTALTURNOS
            FROM DOCTOR D
            LEFT JOIN APPOINTMENT A ON D.ID_USER = A.ID_USER_DOCTOR
            LEFT JOIN STATE S ON D.ID_STATE_DOC = S.ID_STATE
            LEFT JOIN SPECIALITY SP ON D.ID_SPE_DOC = SP.ID_SPE
            GROUP BY 
                D.ID_USER,
                D.DNI_DOC,
                D.NAME_DOC,
                D.SURNAME_DOC,
                D.ID_SPE_DOC,
                SP.NAME_SPE

            ORDER BY TOTALTURNOS DESC";



            DB datos = new DB();
            SqlDataAdapter adapter = datos.ObtenerAdaptador(query);

            DataSet ds = new DataSet();
            adapter.Fill(ds, "DOCTOR");
            return ds.Tables["DOCTOR"];


        }

        public DataTable EspecialidadConMasTurnos()
        {
            string query = @"
            SELECT TOP 5 
                SP.NAME_SPE AS Especialidad,  -- Usando alias
                COUNT(A.ID_APPO) AS TotalTurnos
            FROM SPECIALITY SP
            INNER JOIN DOCTOR D ON SP.ID_SPE = D.ID_SPE_DOC
            LEFT JOIN APPOINTMENT A ON D.ID_USER = A.ID_USER_DOCTOR
            GROUP BY SP.NAME_SPE
            ORDER BY TotalTurnos DESC";

            DB datos = new DB();
            return datos.ObtenerDataTable(query, null);
        }

        public DataTable ObtenerEstadisticasMensualesGrid()
        {
            string query = @"
            SELECT TOP 1
                DATENAME(MONTH, DATE_APPO) AS NombreMes,
                COUNT(*) AS CantidadTurnos,
                (SELECT COUNT(*) FROM APPOINTMENT) AS TotalTurnos
            FROM APPOINTMENT
            GROUP BY DATENAME(MONTH, DATE_APPO), MONTH(DATE_APPO)
            ORDER BY CantidadTurnos DESC"; 

            DB datos = new DB();
            return datos.ObtenerDataTable(query, null);
            
        }
    }
}
