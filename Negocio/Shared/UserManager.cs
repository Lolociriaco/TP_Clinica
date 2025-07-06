using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.Admin;
using Datos.Shared;

namespace Negocio.Shared
{
    public class UserManager
    {
        public bool modificarUsuario(string user, string newPassword = null, string newUser = null)
        {
            UserDao userDao = new UserDao();
            return userDao.modificarUsuario(user, newPassword, newUser);
        }
    }
}
