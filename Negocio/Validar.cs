using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Datos;
using System.Globalization;


namespace Negocio
{
    public class Validar
    {

        public bool ValidarUsuario(string user, string password)
        {

            // Verificar que la contraseña tenga al menos 6 caracteres
            if (password.Length < 6) return false;


            string query = "SELECT * FROM Usuarios WHERE User = @user AND Password = @password";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@user", user),
                new SqlParameter("@pass", password)
            };

            DB db = new DB();
            bool existe = db.validarUser(query, parametros);

            return true;
        }

        public DataTable ObtenerMedicos()
        {
            string query = "SELECT * FROM MEDICOS";
            DB datos = new DB();
            SqlDataAdapter adapter = datos.ObtenerAdaptador(query);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Medicos");
            return ds.Tables["Medicos"];
        }

        public DataTable ObtenerPacientes()
        {
            string query = "SELECT * FROM PACIENTES";
            DB datos = new DB();
            SqlDataAdapter adapter = datos.ObtenerAdaptador(query);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Pacientes");
            return ds.Tables["Pacientes"];
        }

        /*public void eliminarProducto(Medicos medico)
        {
            string consulta = "DELETE FROM MEDICOS WHERE DNI_MEDICO = " + medico.DNI_MEDICO;
            DB datos = new DB();
            datos.ejecutarConsulta(consulta);
        }

        public void ActualizarProducto(Medicos medico)
        {
            string consulta = $"UPDATE MEDICOS SET Username = '{medico.Username}', DNI_MEDICO = {medico.DNI_MEDICO.ToString(CultureInfo.InvariantCulture)}";
            DB datos = new DB();
            datos.ejecutarConsulta(consulta);
        }*/
    }
}