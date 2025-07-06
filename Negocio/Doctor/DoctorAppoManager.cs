using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Datos.Admin;
using Datos.Doctor;
using Entidades;

namespace Negocio.Doctor
{
    public class DoctorAppoManager
    {
        public bool updateAppointment(string state, string observation, int id)
        {
            DoctorAppoDao appoDao = new DoctorAppoDao();
            return appoDao.updateAppointment(state, observation, id);
        }

        public DataTable ObtenerTurnos(string DNI_PAT, string DAY_APPO, string todayOrTomorrow, string state, int doctor_id)
        {
            DoctorAppoDao appoDao = new DoctorAppoDao();
            return appoDao.ObtenerTurnosFiltrados(DNI_PAT, DAY_APPO, todayOrTomorrow, state, doctor_id);
        }

        public int GetDoctorID(string username)
        {
            DoctorAppoDao appoDao = new DoctorAppoDao();
            return appoDao.GetDoctorID(username);
        }

    }
}
