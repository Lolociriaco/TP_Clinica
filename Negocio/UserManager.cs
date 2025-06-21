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

        public bool updateDoctor(int idUsuario, string nombre, string apellido, string dni,
                                 string direccion, string correo, string telefono, DateTime fechaNac,
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
                        FECHANAC_MED = @fechaNac,
                        SEXO_MED = @sexo,
                        ID_LOC_MED = @idLoc,
                        ID_PROV_MED = @idProv,
                        DIAS_HORARIO_MED = @diasHorario
                    WHERE ID_USUARIO = @id";

            return db.updateUser(query, parametros);
        }

    }
}
