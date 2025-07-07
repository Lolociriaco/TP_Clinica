using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Datos;
using Datos.Shared;

namespace Negocio.Shared
{
    public class AuthManager
    {

        // METODOS INTERMEDIOS CON EL DAO

        // VALIDAR SI EL USUARIO EXISTE
        public string ValidarUsuario(string user, string password)
        {
            AuthDao auth = new AuthDao();
            string role = auth.AuthUserAndRole(user, password);

            return role;
        }

        public bool UserExist(string user)
        {
            AuthDao auth = new AuthDao();
            bool existe = auth.validateUserExist(user);
            return existe;
        }

        public bool EsDniValido(string dni)
        {
            string patron = @"^\d{8}$";
            return Regex.IsMatch(dni, patron);
        }
    }
}
