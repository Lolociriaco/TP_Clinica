using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos.Admin
{
    public class AdminDoctorDao
    {
        public DataTable ObtenerMedicos()
        {
            string query = @"
                SELECT D.ID_USER, U.USERNAME, D.NAME_DOC, D.SURNAME_DOC, D.DNI_DOC, D.GENDER_DOC,
                    D.NATIONALITY_DOC, D.ADDRESS_DOC, DATEBIRTH_DOC,
                    C.NAME_CITY, D.ID_CITY_DOC,
                    D.ID_STATE_DOC, S.NAME_STATE, SP.NAME_SPE, D.ID_SPE_DOC, 

                        (
                            SELECT STRING_AGG(A.WEEKDAY_SCH, ', ') 
                            FROM DOCTOR_SCHEDULES A 
                            WHERE A.ID_USER_DOCTOR = D.ID_USER
                        ) AS DIAS_TRABAJO,

                        -- Horas de inicio concatenadas
                        (
                            SELECT STRING_AGG(CONVERT(VARCHAR(5), A.TIME_START, 108), ', ') 
                            FROM DOCTOR_SCHEDULES A 
                            WHERE A.ID_USER_DOCTOR = D.ID_USER
                        ) AS HORA_ENTRADA,

                        -- Horas de fin concatenadas
                        (
                            SELECT STRING_AGG(CONVERT(VARCHAR(5), A.TIME_END, 108), ', ') 
                            FROM DOCTOR_SCHEDULES A 
                            WHERE A.ID_USER_DOCTOR = D.ID_USER
                        ) AS HORA_SALIDA,

                    D.PHONE_DOC, D.EMAIL_DOC

                FROM DOCTOR D
                INNER JOIN CITY C ON C.ID_CITY = D.ID_CITY_DOC
                INNER JOIN STATE S ON S.ID_STATE = D.ID_STATE_DOC
                INNER JOIN SPECIALITY SP ON SP.ID_SPE = D.ID_SPE_DOC
                INNER JOIN USERS U ON U.ID_USER = D.ID_USER
                WHERE D.ACTIVE_DOC = 1";

            using (SqlConnection connection = new SqlConnection(Conexion.Cadena))
            {
                SqlCommand cmd = new SqlCommand(query, connection);
                

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable ObtenerMedicosFiltrados(string state, string weekDay, string speciality, string user)
        {
            string queryBase = @"
                SELECT D.ID_USER, U.USERNAME, D.NAME_DOC, D.SURNAME_DOC, D.DNI_DOC, D.GENDER_DOC,
                    D.NATIONALITY_DOC, D.ADDRESS_DOC, D.DATEBIRTH_DOC,
                    C.NAME_CITY, D.ID_CITY_DOC,
                    D.ID_STATE_DOC, S.NAME_STATE, SP.NAME_SPE, D.ID_SPE_DOC,

                    (
                        SELECT STRING_AGG(A.WEEKDAY_SCH, ', ') 
                        FROM DOCTOR_SCHEDULES A 
                        WHERE A.ID_USER_DOCTOR = D.ID_USER
                    ) AS DIAS_TRABAJO,

                    (
                        SELECT STRING_AGG(CONVERT(VARCHAR(5), A.TIME_START, 108), ', ') 
                        FROM DOCTOR_SCHEDULES A 
                        WHERE A.ID_USER_DOCTOR = D.ID_USER
                    ) AS HORA_ENTRADA,

                    (
                        SELECT STRING_AGG(CONVERT(VARCHAR(5), A.TIME_END, 108), ', ') 
                        FROM DOCTOR_SCHEDULES A 
                        WHERE A.ID_USER_DOCTOR = D.ID_USER
                    ) AS HORA_SALIDA,

                    D.PHONE_DOC, D.EMAIL_DOC

                FROM DOCTOR D
                INNER JOIN CITY C ON C.ID_CITY = D.ID_CITY_DOC
                INNER JOIN STATE S ON S.ID_STATE = D.ID_STATE_DOC
                INNER JOIN SPECIALITY SP ON SP.ID_SPE = D.ID_SPE_DOC
                INNER JOIN USERS U ON U.ID_USER = D.ID_USER
                WHERE D.ACTIVE_DOC = 1";

            List<string> condiciones = new List<string>();
            List<SqlParameter> parametros = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(state))
            {
                condiciones.Add("D.ID_STATE_DOC = @state");
                parametros.Add(new SqlParameter("@state", state));
            }

            if (!string.IsNullOrEmpty(weekDay))
            {
                condiciones.Add(@"D.ID_USER IN (
            SELECT ID_USER_DOCTOR FROM DOCTOR_SCHEDULES WHERE WEEKDAY_SCH = @weekDay
        )");
                parametros.Add(new SqlParameter("@weekDay", weekDay));
            }

            if (!string.IsNullOrEmpty(speciality))
            {
                condiciones.Add("D.ID_SPE_DOC = @speciality");
                parametros.Add(new SqlParameter("@speciality", speciality));
            }

            if (!string.IsNullOrEmpty(user))
            {
                condiciones.Add("U.USERNAME LIKE @user");
                parametros.Add(new SqlParameter("@user", "%" + user + "%"));
            }

            if (condiciones.Count > 0)
            {
                queryBase += " AND " + string.Join(" AND ", condiciones);
            }

            using (SqlConnection connection = new SqlConnection(Conexion.Cadena))
            {
                SqlCommand cmd = new SqlCommand(queryBase, connection);
                cmd.Parameters.AddRange(parametros.ToArray());

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable ObtenerMedicosFiltradosEspecialidad(int idSpe)
        {
            string consulta = "SELECT ID_USER, NAME_DOC + ' ' + SURNAME_DOC AS FULL_NAME FROM DOCTOR WHERE ID_SPE_DOC = @idSpe";
            SqlParameter[] parametros = new SqlParameter[]
            {
                 new SqlParameter("@idSpe", idSpe)
            };

            DB accesoDatos = new DB();
            return accesoDatos.ObtenerDataTable(consulta, parametros);
        }

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

        // CONSULTA PARA OBTENER SEXO DEL MEDICO
        public DataTable ObtenerSexoMedico()
        {
            string query = "SELECT DISTINCT GENDER_DOC FROM DOCTOR WHERE GENDER_DOC IS NOT NULL";
            DB datos = new DB();
            SqlDataAdapter adapter = datos.ObtenerAdaptador(query);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Sexos");
            return ds.Tables["Sexos"];
        }

        // ACTUALIZACION DEL MEDICO
        public bool updateDoctor(Medico medico, string diaSeleccionado, string horaInicio, string horaFin)
        {
            DB db = new DB();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@nombre", medico.Nombre),
                new SqlParameter("@apellido", medico.Apellido),
                new SqlParameter("@dni", medico.DNI),
                new SqlParameter("@direccion", medico.Direccion),
                new SqlParameter("@correo", medico.CorreoElectronico),
                new SqlParameter("@telefono", medico.Telefono),
                new SqlParameter("@nacionalidad", medico.Nacionalidad),
                new SqlParameter("@diaSeleccionado", diaSeleccionado),
                new SqlParameter("@horarioInicio", horaInicio),
                new SqlParameter("@horarioFin", horaFin),
                new SqlParameter("@idEsp", medico.Especialidad),
                new SqlParameter("@fechaNac", medico.FechaNacimiento),
                new SqlParameter("@sexo", medico.Sexo),
                new SqlParameter("@idLoc", medico.Localidad),
                new SqlParameter("@idProv", medico.Provincia),
                new SqlParameter("@id", medico.IdUsuario)
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
                    ID_CITY_DOC = @idLoc,
                    ID_STATE_DOC = @idProv
                WHERE ID_USER = @id;

                IF EXISTS (
                    SELECT 1 FROM DOCTOR_SCHEDULES 
                    WHERE ID_USER_DOCTOR = @id AND WEEKDAY_SCH = @diaSeleccionado
                )
                BEGIN
                    UPDATE DOCTOR_SCHEDULES
                    SET TIME_START = @horarioInicio, TIME_END = @horarioFin
                    WHERE ID_USER_DOCTOR = @id AND WEEKDAY_SCH = @diaSeleccionado
                END
                ELSE
                BEGIN
                    INSERT INTO DOCTOR_SCHEDULES (ID_USER_DOCTOR, WEEKDAY_SCH, TIME_START, TIME_END)
                    VALUES (@id, @diaSeleccionado, @horarioInicio, @horarioFin)
                END
            
            ";

            return db.updateUser(query, parametros);
        }

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

        public DataTable ObtenerEspecialidades()
        {
            string query = "SELECT NAME_SPE, ID_SPE FROM SPECIALITY";
            DB datos = new DB();
            SqlDataAdapter adapter = datos.ObtenerAdaptador(query);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Especialidades");
            return ds.Tables["Especialidades"];
        }

        public DataTable getNombreYApellidoDoctores()
        {
            string query = "SELECT ID_USER, NAME_DOC + ' ' + SURNAME_DOC AS NOMBRE_COMPLETO FROM DOCTOR";
            DB datos = new DB();
            SqlDataAdapter adapter = datos.ObtenerAdaptador(query);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Medicos");
            return ds.Tables["Medicos"];
        }

        public DataTable ObtenerDias()
        {
            string query = "SELECT DISTINCT WEEKDAY_SCH FROM DOCTOR_SCHEDULES WHERE WEEKDAY_SCH IS NOT NULL";
            DB datos = new DB();
            SqlDataAdapter adapter = datos.ObtenerAdaptador(query);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Dias");
            return ds.Tables["Dias"];
        }

        public void AgregarMedico(Medico medico)
        {
            string query = @"
                INSERT INTO DOCTOR (
                    ID_USER, DNI_DOC, NAME_DOC, SURNAME_DOC, 
                    GENDER_DOC, NATIONALITY_DOC, DATEBIRTH_DOC, ADDRESS_DOC, 
                    ID_CITY_DOC, ID_STATE_DOC, EMAIL_DOC, PHONE_DOC, 
                    ID_SPE_DOC
                ) VALUES (
                    @id_usuario, @dni, @nombre, @apellido, 
                    @sexo, @nacionalidad, @fecha, @direccion, 
                    @localidad, @provincia, @correo, @telefono, 
                    @especialidad
                )";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@dni", medico._dni),
                new SqlParameter("@nombre", medico._nombre),
                new SqlParameter("@apellido", medico._apellido),
                new SqlParameter("@sexo", medico._sexo),
                new SqlParameter("@nacionalidad", medico._nacionalidad),
                new SqlParameter("@fecha", medico._fechaNacimiento),
                new SqlParameter("@direccion", medico._direccion),
                new SqlParameter("@localidad", medico._localidad),
                new SqlParameter("@provincia", medico._provincia),
                new SqlParameter("@correo", medico._correoElectronico),
                new SqlParameter("@telefono", medico._telefono),
                new SqlParameter("@especialidad", medico._especialidad),
                new SqlParameter("@id_usuario", medico._id_usuario),
            };

            DB datos = new DB();
            datos.EjecutarInsert(query, parametros);
        }

        // CONSULTA PARA INSERTAR EL HORARIO DEL MEDICO
        public void InsertarHorarioMedico(int idUsuario, string diaSemana, TimeSpan horaInicio, TimeSpan horaFin)
        {
            string query = "INSERT INTO DOCTOR_SCHEDULES (ID_USER_DOCTOR, WEEKDAY_SCH, TIME_START, TIME_END) " +
                           "VALUES (@idUsuario, @dia, @horaInicio, @horaFin)";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idUsuario", idUsuario),
                new SqlParameter("@dia", diaSemana),
                new SqlParameter("@horaInicio", horaInicio),
                new SqlParameter("@horaFin", horaFin)
            };

            DB db = new DB();
            db.EjecutarInsert(query, parametros);
        }

        public bool ExisteDniDoctor(int dni)
        {
            string query = "SELECT COUNT(*) FROM DOCTOR WHERE DNI_DOC = @dni";
            SqlParameter[] parametros = {
                new SqlParameter("@dni", dni)
            };

            DB db = new DB();
            int cantidad = Convert.ToInt32(db.EjecutarEscalar(query, parametros));
            return cantidad > 0;
        }


        // CONSULTA PARA VERIFICAR SI EL TELEFONO DE DOCTOR YA EXISTE
        public bool ExisteTelefonoDoctor(string telefono)
        {
            string query = "SELECT COUNT(*) FROM DOCTOR WHERE PHONE_DOC = @telefono";
            SqlParameter[] parametros = {
                new SqlParameter("@telefono", telefono)
            };

            DB db = new DB();
            int cantidad = Convert.ToInt32(db.EjecutarEscalar(query, parametros));
            return cantidad > 0;
        }

        // CONSULTA PARA INSERTAR EL NUEVO USUARIO
        public int AgregarUsuario(Usuario user)
        {
            string query = "INSERT INTO USERS (USERNAME, PASSWORD_USER, ROLE_USER) " +
                           "VALUES (@usuario, @contrasena, @tipoUsuario); SELECT SCOPE_IDENTITY();";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@usuario", user.NombreUsuario),
                new SqlParameter("@contrasena", user.Contrasena),
                new SqlParameter("@tipoUsuario", user.TipoUsuario)
            };

            DB datos = new DB();
            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddRange(parametros);
                conn.Open();
                object result = cmd.ExecuteScalar();
                return Convert.ToInt32(result); // devuelve el ID generado
            }
        }
    }
}
