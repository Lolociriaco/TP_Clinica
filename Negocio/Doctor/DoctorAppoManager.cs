using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio.Doctor
{
    public class DoctorAppoManager
    {
        public bool updateAppointment(string state, string observation, int id)
        {
            DB db = new DB();
            return db.updateAppointment(state, observation, id);
        }

    }
}
