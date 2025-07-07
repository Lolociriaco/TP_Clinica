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

namespace Negocio.Admin
{
    public class AdminAppoManager
    {

        // METODOS INTERMEDIOS CON EL DAO

        public bool medicoDisponible(string day, int id_user)
        {
            AdminAppoDao appoDao = new AdminAppoDao();
            return appoDao.medicoDisponible(day, id_user);
        }

        public DoctorSchedule ObtenerHorasTrabajadas(int id_user, string day)
        {
            AdminAppoDao appoDao = new AdminAppoDao();
            return appoDao.ObtenerHorasTrabajadas(id_user, day);
        }

        public List<TimeSpan> ObtenerTurnosAsignados(int id_doctor, DateTime date)
        {
            AdminAppoDao appoDao = new AdminAppoDao();
            return appoDao.ObtenerTurnosAsignados(id_doctor, date); 
        }

        public void CargarTurno(Turnos turno)
        {
            AdminAppoDao appoDao = new AdminAppoDao();
            appoDao.CargarTurno(turno);
        }
    }
}
