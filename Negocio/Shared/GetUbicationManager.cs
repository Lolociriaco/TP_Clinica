using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Datos.Shared;

namespace Negocio.Shared
{
    public class GetUbicationManager
    {
        public DataTable ObtenerProvincia()
        {
            GetUbicationDao getUbication = new GetUbicationDao();
            return getUbication.Provincias();
        }

        public DataTable ObtenerLocalidad()
        {
            GetUbicationDao getUbication = new GetUbicationDao();
            return getUbication.Localidades();
        }
        public DataTable ObtenerLocalidadesFiltradas(int idProvincia)
        {
            GetUbicationDao getUbication = new GetUbicationDao();
            return getUbication.LocalidadesFiltradas(idProvincia);
        }
    }
}
