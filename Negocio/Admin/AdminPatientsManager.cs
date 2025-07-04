using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Datos.Admin;

namespace Negocio.Admin
{
    public class AdminPatientsManager
    {
        public DataTable ObtenerPacientes(string state, string name, string dni, string sexo)
        {
            AdminPatientsDao daoPatients = new AdminPatientsDao();
            return daoPatients.ObtenerPacientes(state, name, dni, sexo);
        }
    }
}
