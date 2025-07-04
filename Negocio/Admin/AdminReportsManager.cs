using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Datos.Admin;
using Datos.Doctor;

namespace Negocio.Admin
{
    public class AdminReportsManager
    {
        public DataTable EspecialidadConMasTurnosConPorcentaje()
        {
            AdminReportsDao dao = new AdminReportsDao();
            DataTable dt = dao.EspecialidadConMasTurnos();
            int total = dao.ObtenerTotalTurnos();

            dt.Columns.Add("PORCENTAJE", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                int turnos = Convert.ToInt32(row["TotalTurnos"]);
                double porcentaje = total > 0 ? (turnos * 100.0) / total : 0;
                row["PORCENTAJE"] = porcentaje.ToString("N2") + "%";
            }

            return dt;
        }

        public DataTable MedicosConMasTurnosConPorcentaje()
        {
            AdminReportsDao dao = new AdminReportsDao();
            DataTable dt = dao.MedicosConMasTurnos();
            int total = dao.ObtenerTotalTurnos();

            dt.Columns.Add("PORCENTAJE", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                int turnos = Convert.ToInt32(row["TOTALTURNOS"]);
                double porcentaje = total > 0 ? (turnos * 100.0) / total : 0;
                row["PORCENTAJE"] = porcentaje.ToString("N2") + "%";
            }

            return dt;
        }

        public DataTable ObtenerTextoEstadisticas()
        {
            AdminReportsDao dao = new AdminReportsDao();
            DataTable dt = dao.ObtenerEstadisticasMensualesGrid();

            DataTable dtResultado = new DataTable();
            dtResultado.Columns.Add("Estadistica", typeof(string));

            if (dt.Rows.Count == 0)
            {
                dtResultado.Rows.Add("No records have been found.");
            }
            else
            {
                string nombreMes = dt.Rows[0]["NombreMes"].ToString();
                int cantidad = Convert.ToInt32(dt.Rows[0]["CantidadTurnos"]);
                int total = Convert.ToInt32(dt.Rows[0]["TotalTurnos"]);

                double porcentaje = total > 0 ? Math.Round(cantidad * 100.0 / total, 2) : 0;

                string mensaje =  $"The month with most shifts was {nombreMes} with {cantidad} shifts ({porcentaje}% from the total)";
                dtResultado.Rows.Add(mensaje);
            }

            return dtResultado;
        }
    }
}
