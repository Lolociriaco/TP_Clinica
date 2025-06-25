using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio
{
    public class UserManager
    {

        // BAJA LOGICA DEL MEDICO
        public bool deleteDoctor(int id)
        {
            DB db = new DB();
            string query = "UPDATE DOCTOR SET ACTIVE_DOC = 0 WHERE ID_USER = @id";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };

            return db.updateUser(query, parametros);
        }

        // BAJA LOGICA DEL PACIENTE
        public bool deletePatient(int dni)
        {
            DB db = new DB();
            string query = "UPDATE PATIENTS SET ACTIVE_PAT = 0 WHERE DNI_PAT = @dni";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@dni", dni)
            };

            return db.updateUser(query, parametros);
        }

        // MODIFICACION DEL USUARIO
        public bool modificarUsuario(string user, string newPassword = null, string newUser = null)
        {
            List<string> sets = new List<string>();
            List<SqlParameter> parametros = new List<SqlParameter>();

            if (newUser != null)
            {
                sets.Add("USERNAME = @nuevoUsuario");
                parametros.Add(new SqlParameter("@nuevoUsuario", newUser));
            }

            if (newPassword != null)
            {
                sets.Add("PASSWORD_USER = @nuevaPass");
                parametros.Add(new SqlParameter("@nuevaPass", newPassword));
            }

            // WHERE con el usuario original
            parametros.Add(new SqlParameter("@usuarioOriginal", user));

            string query = "UPDATE USERS SET " + string.Join(", ", sets) + " WHERE USERNAME = @usuarioOriginal";

            DB db = new DB();

            return db.updateUser(query, parametros.ToArray());
        }

        // ACTUALIZACION DEL MEDICO
        public bool updateDoctor(int idUsuario, string nombre, string apellido, string dni,
                                 string direccion, string correo, string telefono, string nacionalidad, int idEsp, DateTime fechaNac,
                                 string sexo, int idLoc, int idProv, string diasHorario)
        {
            DB db = new DB();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@nombre", nombre),
                new SqlParameter("@apellido", apellido),
                new SqlParameter("@dni", dni),
                new SqlParameter("@direccion", direccion),
                new SqlParameter("@correo", correo),
                new SqlParameter("@telefono", telefono),
                new SqlParameter("@nacionalidad", nacionalidad),
                new SqlParameter("@idEsp", idEsp),
                new SqlParameter("@fechaNac", fechaNac),
                new SqlParameter("@sexo", sexo),
                new SqlParameter("@idLoc", idLoc),
                new SqlParameter("@idProv", idProv),
                new SqlParameter("@diasHorario", diasHorario),
                new SqlParameter("@id", idUsuario)
            };

            string query = @"
                UPDATE DOCTOR SET
                    NAME_DOC = @nombre,
                    SURNAME_DOC = @apellido,
                    DNI_DOC = @dni,
                    ADDRESS_DOC = @direccion,
                    EMAIL_DOC = @correo,
                    PHONE_DOC = @telefono,
                    NATIONALITY_DOC = @nacionalidad,
                    ID_SPE_DOC = @idEsp,
                    DATEBIRTH_DOC = @fechaNac,
                    GENDER_DOC = @sexo,
                    ID_CITY_DOC = @idCiudad,
                    ID_STATE_DOC = @idProvincia
                WHERE ID_USER = @id";


            return db.updateUser(query, parametros);
        }

        // ACTUALIZACION DEL PACIENTE
        public bool updatePatient(string nombre, string apellido, string dni,
                                string direccion, string correo, string telefono, string nacionalidad, DateTime fechaNac,
                                string sexo, int idLoc, int idProv)
        {
            DB db = new DB();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@nombre", nombre),
                new SqlParameter("@apellido", apellido),
                new SqlParameter("@dni", dni),
                new SqlParameter("@direccion", direccion),
                new SqlParameter("@correo", correo),
                new SqlParameter("@telefono", telefono),
                new SqlParameter("@nacionalidad", nacionalidad),
                new SqlParameter("@fechaNac", fechaNac),
                new SqlParameter("@sexo", sexo),
                new SqlParameter("@idLoc", idLoc),
                new SqlParameter("@idProv", idProv)
            };
            string query = @"
                UPDATE PATIENTS SET
                    NAME_PAT = @nombre,
                    SURNAME_PAT = @apellido,
                    ADDRESS_PAT = @direccion,
                    EMAIL_PAT = @correo,
                    PHONE_PAT = @telefono,
                    NATIONALITY_PAT = @nacionalidad,
                    DATEBIRTH_PAT = @fechaNac,
                    GENDER_PAT = @sexo,
                    ID_CITY_PAT = @idCiudad,
                    ID_STATE_PAT = @idProvincia
                WHERE DNI_PAT = @dni";


            return db.updateUser(query, parametros);
        }

    }
}
