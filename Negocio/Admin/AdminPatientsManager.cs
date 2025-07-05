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
        public DataTable ObtenerPacientes()
        {
            AdminPatientsDao daoPatients = new AdminPatientsDao();
            return daoPatients.ObtenerPacientes();
        }

        public DataTable ObtenerPacientesFiltrados(string state, string name, string dni, string sexo)
        {
            AdminPatientsDao patientDao = new AdminPatientsDao();
            return patientDao.ObtenerPacientesFiltrados(state, name, dni, sexo);
        }

        public DataTable ObtenerSexoPaciente()
        {
            AdminPatientsDao admin = new AdminPatientsDao();
            return admin.ObtenerSexoPaciente();
        }
    }
}
