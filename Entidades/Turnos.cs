using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Turnos
    {
        public int IdTurno { get; set; }
        public int IdUsuarioMedico { get; set; }
        public int DniPacTurno { get; set; }
        public DateTime FechaTurno { get; set; }
        public TimeSpan HoraTurno { get; set; }
        public string EstadoTurno { get; set; }
        public string ObservacionTurno { get; set; }
    }
}

