using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class DoctorSchedule
    {
        public TimeSpan _TimeStart { get; set; }
        public TimeSpan _TimeEnd { get; set; }
        public int _id_user_doctor { get; set; }
        public int _id_schedule { get; set; }
        public string _weekday_schedule { get; set; }

        public DoctorSchedule() { }

    }

}
