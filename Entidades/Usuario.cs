using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Usuario
    {
        // GETTERS Y SETTERS
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public string TipoUsuario { get; set; }

        public bool EsAdministrador => TipoUsuario == "ADMINISTRADOR";
        public bool EsMedico => TipoUsuario == "MEDICO";
    }

}
