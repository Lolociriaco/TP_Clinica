using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Shared
{
    public class GetUbicationDao
    {
        public DataTable Provincias()
        {
            string query = "SELECT ID_STATE, NAME_STATE FROM STATE";
            DB datos = new DB();
            SqlDataAdapter adapter = datos.ObtenerAdaptador(query);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Provincias");
            return ds.Tables["Provincias"];
        }

        public DataTable Localidades()
        {
            string query = "SELECT ID_CITY, NAME_CITY FROM CITY";
            DB datos = new DB();
            SqlDataAdapter adapter = datos.ObtenerAdaptador(query);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Localidades");
            return ds.Tables["Localidades"];
        }
        public DataTable LocalidadesFiltradas(int idProvincia)
        {
            string consulta = "SELECT ID_CITY, NAME_CITY FROM CITY WHERE ID_STATE_CITY = @idProv";
            SqlParameter[] parametros = new SqlParameter[]
            {
                 new SqlParameter("@idProv", idProvincia)
            };

            DB accesoDatos = new DB();
            return accesoDatos.ObtenerDataTable(consulta, parametros);
        }
    }
}
