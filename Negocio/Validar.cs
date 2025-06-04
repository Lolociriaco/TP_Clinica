using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio
{
    public class Validar
    {
        public bool ValidarDNI(string dni)
        {
            // Verificar que el DNI tenga 8 dígitos y una letra
            if (dni.Length != 8) return false;

            // Verificar que todos los digitos sean numeros

            foreach (char c in dni)
            {
                if (!char.IsDigit(c)) return false;
            }

            return true;
        }

        public bool ValidarUsuario(string dni, string password)
        {
            if (!ValidarDNI(dni)) return false;

            // Verificar que la contraseña tenga al menos 6 caracteres
            if (password.Length < 6) return false;


            string query = "SELECT * FROM Usuarios WHERE Dni = @dni AND Password = @password";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@dni", dni),
                new SqlParameter("@pass", password)
            };

            DB db = new DB();
            bool existe = db.validarUser(query, parametros);

            return true;
        }
    }
}