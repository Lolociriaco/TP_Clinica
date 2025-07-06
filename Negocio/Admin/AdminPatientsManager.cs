using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Datos.Admin;
using Entidades;
using System.Net;

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

        public bool updatePatient(Paciente paciente)
        {
            AdminPatientsDao adminPatientsDao = new AdminPatientsDao();
            return adminPatientsDao.updatePatient(paciente);
        }

        public bool deletePatient(int dni)
        {
            AdminPatientsDao adminPatientsDao = new AdminPatientsDao();
            return adminPatientsDao.deletePatient(dni);
        }

        public bool ExisteDniPaciente(int dni)
        {
            AdminPatientsDao adminPatientsDao = new AdminPatientsDao();
            return adminPatientsDao.ExisteDniPaciente(dni);
        }

        public bool ExisteTelefonoPaciente(string telefono)
        {
            AdminPatientsDao adminPatientsDao = new AdminPatientsDao();
            return adminPatientsDao.ExisteTelefonoPaciente(telefono);
        }

        public void AgregarPaciente(Paciente paciente)
        {
            AdminPatientsDao adminPatientsDao = new AdminPatientsDao();
            adminPatientsDao.AgregarPaciente(paciente);
        }
    }
}  
