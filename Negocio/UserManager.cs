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
        public bool deleteDoctor(int id)
        {
            DB db = new DB();
            string query = "UPDATE MEDICOS SET ACTIVO_MED = 0 WHERE ID_USUARIO = @id";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };

            return db.updateUser(query, parametros);
        }

        public bool deletePatient(int dni)
        {
            DB db = new DB();
            string query = "UPDATE PACIENTES SET ACTIVO_PAC = 0 WHERE DNI_PAC = @dni";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@dni", dni)
            };

            return db.updateUser(query, parametros);
        }

        public bool modificarUsuario(string user, string newPassword = null, string newUser = null)
        {
            List<string> sets = new List<string>();
            List<SqlParameter> parametros = new List<SqlParameter>();

            if (newUser != null)
            {
                sets.Add("USUARIO = @nuevoUsuario");
                parametros.Add(new SqlParameter("@nuevoUsuario", newUser));
            }

            if (newPassword != null)
            {
                sets.Add("CONTRASENA = @nuevaPass");
                parametros.Add(new SqlParameter("@nuevaPass", newPassword));
            }

            // WHERE con el usuario original
            parametros.Add(new SqlParameter("@usuarioOriginal", user));

            string query = "UPDATE Usuarios SET " + string.Join(", ", sets) + " WHERE Usuario = @usuarioOriginal";

            DB db = new DB();

            return db.updateUser(query, parametros.ToArray());
        }


        public bool updateDoctor(int idUsuario, string nombre, string apellido, string dni,
                                 string direccion, string correo, string telefono, int idEsp, DateTime fechaNac,
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
                new SqlParameter("@idEsp", idEsp),
                new SqlParameter("@fechaNac", fechaNac),
                new SqlParameter("@sexo", sexo),
                new SqlParameter("@idLoc", idLoc),
                new SqlParameter("@idProv", idProv),
                new SqlParameter("@diasHorario", diasHorario),
                new SqlParameter("@id", idUsuario)
            };

            string query = @"UPDATE Medicos SET
                        NOMBRE_MED = @nombre,
                        APELLIDO_MED = @apellido,
                        DNI_MED = @dni,
                        DIRECCION_MED = @direccion,
                        CORREO_MED = @correo,
                        TELEFONO_MED = @telefono,
                        ID_ESP_MED = @idEsp,
                        FECHANAC_MED = @fechaNac,
                        SEXO_MED = @sexo,
                        ID_LOC_MED = @idLoc,
                        ID_PROV_MED = @idProv,
                        DIAS_HORARIO_MED = @diasHorario
                    WHERE ID_USUARIO = @id";

            return db.updateUser(query, parametros);
        }
        public bool updatePatient(string nombre, string apellido, string dni,
                                string direccion, string correo, string telefono, DateTime fechaNac,
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
                new SqlParameter("@fechaNac", fechaNac),
                new SqlParameter("@sexo", sexo),
                new SqlParameter("@idLoc", idLoc),
                new SqlParameter("@idProv", idProv)
            };

            string query = @"UPDATE Pacientes SET
                        NOMBRE_PAC = @nombre,
                        APELLIDO_PAC = @apellido,
                        DIRECCION_PAC = @direccion,
                        CORREO_PAC = @correo,
                        TELEFONO_PAC = @telefono,
                        FECHANAC_PAC = @fechaNac,
                        SEXO_PAC = @sexo,
                        ID_LOC_PAC = @idLoc,
                        ID_PROV_PAC = @idProv
                    WHERE DNI_PAC = @dni";

            return db.updateUser(query, parametros);
        }

    }
}
