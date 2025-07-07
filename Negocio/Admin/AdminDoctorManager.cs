using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Datos.Admin;
using Entidades;

namespace Negocio
{
    public class AdminDoctorManager
    {

        // METODOS INTERMEDIOS CON EL DAO

        public DataTable ObtenerMedicosFiltradosEspecialidad(int idSpe)
        {
            AdminDoctorDao doctorDao = new AdminDoctorDao();
            return doctorDao.ObtenerMedicosFiltradosEspecialidad(idSpe);
        }

        public bool deleteDoctor(int id)
        {
            AdminDoctorDao doctorDao = new AdminDoctorDao();
            return doctorDao.deleteDoctor(id);
        }

        public DataTable ObtenerMedicos()
        {
            AdminDoctorDao doctorDao = new AdminDoctorDao();
            return doctorDao.ObtenerMedicos();
        }

        public DataTable ObtenerMedicosFiltrados(string state, string weekDay, string speciality, string user)
        {
            AdminDoctorDao doctorDao = new AdminDoctorDao();
            return doctorDao.ObtenerMedicosFiltrados(state, weekDay, speciality, user);
        }

        public DataTable ObtenerSexoMedico()
        {
            AdminDoctorDao admin = new AdminDoctorDao();
            return admin.ObtenerSexoMedico();
        }

        public bool updateDoctor(Medico medico, string diaSeleccionado, string horaInicio, string horaFin)
        {
            AdminDoctorDao adminDoctorDao = new AdminDoctorDao();
            return adminDoctorDao.updateDoctor(medico, diaSeleccionado, horaInicio, horaFin);
        }

        public DataTable ObtenerEspecialidades()
        {
            AdminDoctorDao adminDoctorDao = new AdminDoctorDao();
            return adminDoctorDao.ObtenerEspecialidades();
        }

        public DataTable getNombreYApellidoDoctores()
        {
            AdminDoctorDao adminDoctorDao = new AdminDoctorDao();
            return adminDoctorDao.getNombreYApellidoDoctores();
        }

        public DataTable ObtenerDias()
        {
            AdminDoctorDao adminDoctorDao = new AdminDoctorDao();
            return adminDoctorDao.ObtenerDias();
        }

        public bool AgregarMedico(Medico medico)
        {
            AdminDoctorDao adminDoctorDao = new AdminDoctorDao();
            return adminDoctorDao.AgregarMedico(medico);
        }

        public bool InsertarHorarioMedico(int idUsuario, string diaSemana, TimeSpan horaInicio, TimeSpan horaFin)
        {
            AdminDoctorDao adminDoctorDao = new AdminDoctorDao();
            return adminDoctorDao.InsertarHorarioMedico(idUsuario, diaSemana, horaInicio, horaFin);
        }

        public bool ExisteDniDoctor(int dni)
        {
            AdminDoctorDao adminDoctorDao = new AdminDoctorDao();
            return adminDoctorDao.ExisteDniDoctor(dni);
        }

        public bool ExisteTelefonoDoctor(string telefono)
        {
            AdminDoctorDao adminDoctorDao = new AdminDoctorDao();
            return adminDoctorDao.ExisteTelefonoDoctor(telefono);
        }

        public int AgregarUsuario(Usuario user)
        {
            AdminDoctorDao adminDoctorDao = new AdminDoctorDao();
            return adminDoctorDao.AgregarUsuario(user);
        }
    }
}
