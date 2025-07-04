
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
using Entidades;
using System.Text.RegularExpressions;


namespace Negocio
{
    public class Validar
    {

        // VALIDAR SI EL USUARIO EXISTE
        public string ValidarUsuario(string user, string password)
        {

            // Collate As lo hace case sensitive - Accent Sensitive


            string query = "SELECT ROLE_USER FROM USERS WHERE USERNAME COLLATE Latin1_General_CS_AS = @user " +
                "AND PASSWORD_USER COLLATE Latin1_General_CS_AS = @password";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@user", user),
                new SqlParameter("@password", password)
            };

            DB db = new DB();
            string tipoUsuario = db.ObtenerTipoUsuario(query, parametros);

            return tipoUsuario;
        }

        // VALIDAR EL CAMBIO DE USUARIO
        public bool ValidarCambioUsuario(string user)
        {
            string query = "SELECT * FROM USERS WHERE USERNAME = @user";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@user", user),
            };

            DB db = new DB();
            bool existe = db.validarUser(query, parametros);

            return existe;
        }

        // CONSULTA PARA OBTENER CAMPOS DE MEDICOS
        public DataTable ObtenerMedicos(string state, string weekDay, string speciality, string user)
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

            List<SqlParameter> parametros = new List<SqlParameter>();
            if(!string.IsNullOrEmpty(state))
            {
                query += " AND D.ID_STATE_DOC = @state";
                parametros.Add(new SqlParameter("@state", state));
            }

            if (!string.IsNullOrEmpty(weekDay))
            {
                query += " AND EXISTS (SELECT 1 FROM DOCTOR_SCHEDULES A WHERE A.ID_USER_DOCTOR = D.ID_USER AND A.WEEKDAY_SCH = @weekDay)";
                parametros.Add(new SqlParameter("@weekDay", weekDay));
            }

            if (!string.IsNullOrEmpty(speciality))
            {
                query += " AND D.ID_SPE_DOC = @speciality";
                parametros.Add(new SqlParameter("@speciality", speciality));
            }

            if (!string.IsNullOrEmpty(user))
            {
                query += " AND U.USERNAME = @user";
                parametros.Add(new SqlParameter("@user", user));
            }

            DB datos = new DB();
            return datos.ObtenerTurnos(query, parametros);
        }

        // CONSULTA PARA OBTENER CAMPOS DE PACIENTES
        public DataTable ObtenerPacientes()
        {
            string query = @"
                SELECT 
                    P.DNI_PAT, P.NAME_PAT, P.SURNAME_PAT, 
                    P.GENDER_PAT, P.NATIONALITY_PAT, P.ADDRESS_PAT, P.DATEBIRTH_PAT, 
                    C.NAME_CITY, P.ID_CITY_PAT, S.NAME_STATE, P.ID_STATE_PAT, 
                    P.PHONE_PAT, P.EMAIL_PAT
                FROM PATIENTS P
                INNER JOIN CITY C ON C.ID_CITY = P.ID_CITY_PAT
                INNER JOIN STATE S ON S.ID_STATE = P.ID_STATE_PAT
                WHERE P.ACTIVE_PAT = 1";


            DB datos = new DB();
            SqlDataAdapter adapter = datos.ObtenerAdaptador(query);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Pacientes");
            return ds.Tables["Pacientes"];
        }

        // CONSULTA PARA OBTENER CAMPOS DE TURNOS
        public DataTable ObtenerTurnos(string DNI_PAT, string DAY_APPO, string todayOrTomorrow, string state)
        {
            string query = "SELECT A.ID_APPO, A.DNI_PAT_APPO, A.DATE_APPO, A.TIME_APPO, A.STATE_APPO, A.OBSERVATION_APPO," +
                "P.NAME_PAT, P.SURNAME_PAT, P.GENDER_PAT " +
                "FROM APPOINTMENT A " +
                "INNER JOIN PATIENTS P ON P.DNI_PAT = A.DNI_PAT_APPO " +
                "WHERE P.ACTIVE_PAT = 1";

            List<SqlParameter> parametros = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(DNI_PAT))
            {
                query += " AND A.DNI_PAT_APPO = @dni";
                parametros.Add(new SqlParameter("@dni", DNI_PAT));
            }

            if (!string.IsNullOrEmpty(DAY_APPO))
            {
                query += " AND A.DATE_APPO = @day";
                parametros.Add(new SqlParameter("@day", DAY_APPO));
            }

            if (!string.IsNullOrEmpty(todayOrTomorrow))
            {
                if(todayOrTomorrow == "TODAY")
                {
                    query += " AND A.DATE_APPO = @todayDate";
                    parametros.Add(new SqlParameter("@todayDate", DateTime.Today));
                }

                else if(todayOrTomorrow == "TOMORROW")
                {
                    query += " AND A.DATE_APPO = @tomorrowDate";
                    parametros.Add(new SqlParameter("@tomorrowDate", DateTime.Today.AddDays(1)));
                }

            }

            if (!string.IsNullOrEmpty(state))
            {
                query += " AND A.STATE_APPO = @state";
                parametros.Add(new SqlParameter("@state", state));
            }

            DB datos = new DB();
            return datos.ObtenerTurnos(query, parametros);
        }


        public int ObtenerTotalTurnos()
        {
            string query = "SELECT COUNT(*) FROM APPOINTMENT";
            DB datos = new DB();
            object result = datos.EjecutarEscalar(query, new SqlParameter[0]); // Array vacío
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public DataTable MedicosConMasTurnosConPorcentaje()
        {
            int totalTurnos = ObtenerTotalTurnos();
            DataTable dtMedicos = MedicosConMasTurnos();

            if (dtMedicos.Columns.Contains("PORCENTAJE"))
                dtMedicos.Columns.Remove("PORCENTAJE");

            dtMedicos.Columns.Add("PORCENTAJE", typeof(string));

            foreach (DataRow row in dtMedicos.Rows)
            {
                int turnos = Convert.ToInt32(row["TOTALTURNOS"]);
                double porcentaje = totalTurnos > 0 ? (turnos * 100.0) / totalTurnos : 0;
                row["PORCENTAJE"] = porcentaje.ToString("N2") + "%";
            }

            return dtMedicos;
        }

        public DataTable EspecialidadConMasTurnosConPorcentaje()
        {
            int totalTurnos = ObtenerTotalTurnos();
            DataTable dtEspecialidades = EspecialidadConMasTurnos();

            if (dtEspecialidades.Columns.Contains("PORCENTAJE"))
                dtEspecialidades.Columns.Remove("PORCENTAJE");

            dtEspecialidades.Columns.Add("PORCENTAJE", typeof(string));

            foreach (DataRow row in dtEspecialidades.Rows)
            {
                int turnos = Convert.ToInt32(row["TotalTurnos"]);
                double porcentaje = totalTurnos > 0 ? (turnos * 100.0) / totalTurnos : 0;
                row["PORCENTAJE"] = porcentaje.ToString("N2") + "%";
            }

            return dtEspecialidades;
        }

        // CONSULTA PARA OBTENER SEXO DEL PACIENTE
        public DataTable ObtenerSexoPaciente()
        {
            string query = "SELECT DISTINCT GENDER_PAT FROM PATIENTS WHERE GENDER_PAT IS NOT NULL";
            DB datos = new DB();
            SqlDataAdapter adapter = datos.ObtenerAdaptador(query);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Sexos");
            return ds.Tables["Sexos"];
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

        // CONSULTA PARA OBTENER ESPECIALIDADES
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

        // CONSULTA PARA OBTENER CAMPOS DE PROVINCIAS
        public DataTable ObtenerProvincia()
        {
            string query = "SELECT ID_STATE, NAME_STATE FROM STATE";
            DB datos = new DB();
            SqlDataAdapter adapter = datos.ObtenerAdaptador(query);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Provincias");
            return ds.Tables["Provincias"];
        }

        // CONSULTA PARA OBTENER CAMPOS DE LOCALIDADES
        public DataTable ObtenerLocalidad()
        {
            string query = "SELECT ID_CITY, NAME_CITY FROM CITY";
            DB datos = new DB();
            SqlDataAdapter adapter = datos.ObtenerAdaptador(query);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Localidades");
            return ds.Tables["Localidades"];
        }

        // CONSULTA PARA OBTENER DIAS DE LA SEMANA
        public DataTable ObtenerDias()
        {
            string query = "SELECT DISTINCT WEEKDAY_SCH FROM DOCTOR_SCHEDULES WHERE WEEKDAY_SCH IS NOT NULL";
            DB datos = new DB();
            SqlDataAdapter adapter = datos.ObtenerAdaptador(query);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Dias");
            return ds.Tables["Dias"];
        }

        // CONSULTA PARA INSERTAR EL NUEVO PACIENTE
        public void AgregarPaciente(Paciente paciente)
        {
            string query = @"
                INSERT INTO PATIENTS (
                    DNI_PAT, NAME_PAT, SURNAME_PAT, 
                    GENDER_PAT, NATIONALITY_PAT, DATEBIRTH_PAT, ADDRESS_PAT, 
                    ID_CITY_PAT, ID_STATE_PAT, EMAIL_PAT, PHONE_PAT
                ) VALUES (
                    @dni, @nombre, @apellido, 
                    @sexo, @nacionalidad, @fecha, @direccion, 
                    @localidad, @provincia, @correo, @telefono
                )";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@dni", paciente._dni),
                new SqlParameter("@nombre", paciente._nombre),
                new SqlParameter("@apellido", paciente._apellido),
                new SqlParameter("@sexo", paciente._sexo),
                new SqlParameter("@nacionalidad", paciente._nacionalidad),
                new SqlParameter("@fecha", paciente._fechaNacimiento),
                new SqlParameter("@direccion", paciente._direccion),
                new SqlParameter("@localidad", paciente._localidad),
                new SqlParameter("@provincia", paciente._provincia),
                new SqlParameter("@correo", paciente._correoElectronico),
                new SqlParameter("@telefono", paciente._telefono),
            };

            DB datos = new DB();
            datos.EjecutarInsert(query, parametros);
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
            using (SqlConnection conn = datos.obtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddRange(parametros);
                conn.Open();
                object result = cmd.ExecuteScalar();
                return Convert.ToInt32(result); // devuelve el ID generado
            }
        }

        // CONSULTA PARA INSERTAR EL NUEVO MEDICO
        public void CargarTurno(Turnos turno)
        {
            string query = "INSERT INTO APPOINTMENT (ID_USER_DOCTOR, DNI_PAT_APPO, DATE_APPO, TIME_APPO) " +
                         "VALUES (@id_usuario_med, @dni_paciente, @fecha_turno, @hora_turno)";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@id_usuario_med", turno.IdUsuarioMedico),
                new SqlParameter("@dni_paciente", turno.DniPacTurno),
                new SqlParameter("@fecha_turno", turno.FechaTurno),
                new SqlParameter("@hora_turno", turno.HoraTurno)
            };

            DB datos = new DB();
            datos.EjecutarInsert(query, parametros);
        }

        public bool MedicoDisponible(string diaTurno, TimeSpan horaTurno, int id_medico)
        {
            string query = "SELECT * FROM DOCTORS WHERE ID_USER = @id_medico";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@id_medico", id_medico),
            };

            DB db = new DB();
            bool existe = db.validarUser(query, parametros);

            return existe;
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

        // CONSULTA PARA VERIFICAR SI EL DNI YA EXISTE
        public bool ExisteDni(int dni)
        {
            string query = "SELECT COUNT(*) FROM DOCTOR WHERE DNI_DOC = @dni";
            SqlParameter[] parametros = {
                new SqlParameter("@dni", dni)
            };

            DB db = new DB();
            int cantidad = Convert.ToInt32(db.EjecutarEscalar(query, parametros));
            return cantidad > 0;
        }

        public bool ExisteDniPaciente(int dni)
        {
            string query = "SELECT COUNT(*) FROM PATIENTS WHERE DNI_PAT = @dni";
            SqlParameter[] parametros = {
                new SqlParameter("@dni", dni)
            };

            DB db = new DB();
            int cantidad = Convert.ToInt32(db.EjecutarEscalar(query, parametros));
            return cantidad > 0;
        }

        public bool EsDniValido(string dni)
        {
            string patron = @"^\d{8}$";
            return Regex.IsMatch(dni, patron);
        }


        public bool ExisteUsuario(string user)
        {
            string query = "SELECT COUNT(*) FROM USERS WHERE USERNAME = @user";
            SqlParameter[] parametros = {
                new SqlParameter("@user", user)
            };

            DB db = new DB();
            int cantidad = Convert.ToInt32(db.EjecutarEscalar(query, parametros));
            return cantidad > 0;
        }

        // CONSULTA PARA VERIFICAR SI EL TELEFONO YA EXISTE
        public bool ExisteTelefono(string telefono)
        {
            string query = "SELECT COUNT(*) FROM DOCTOR WHERE PHONE_DOC = @telefono";
            SqlParameter[] parametros = {
                new SqlParameter("@telefono", telefono)
            };

            DB db = new DB();
            int cantidad = Convert.ToInt32(db.EjecutarEscalar(query, parametros));
            return cantidad > 0;
        }


        public DataTable MedicosConMasTurnos()
        {
            string query = @"
            SELECT TOP 10 
                D.ID_USER,
                D.DNI_DOC,
                D.NAME_DOC,
                D.SURNAME_DOC,
                D.ID_SPE_DOC,
                SP.NAME_SPE,

                COUNT(A.ID_APPO) AS TOTALTURNOS
            FROM DOCTOR D
            LEFT JOIN APPOINTMENT A ON D.ID_USER = A.ID_USER_DOCTOR
            LEFT JOIN STATE S ON D.ID_STATE_DOC = S.ID_STATE
            LEFT JOIN SPECIALITY SP ON D.ID_SPE_DOC = SP.ID_SPE
            GROUP BY 
                D.ID_USER,
                D.DNI_DOC,
                D.NAME_DOC,
                D.SURNAME_DOC,
                D.ID_SPE_DOC,
                SP.NAME_SPE

            ORDER BY TOTALTURNOS DESC";



            DB datos = new DB();
            SqlDataAdapter adapter = datos.ObtenerAdaptador(query);

            DataSet ds = new DataSet();
            adapter.Fill(ds, "DOCTOR");
            return ds.Tables["DOCTOR"];


        }

        public DataTable ObtenerLocalidadesFiltradas(int idProvincia)
        {
            string consulta = "SELECT ID_CITY, NAME_CITY FROM CITY WHERE ID_STATE_CITY = @idProv";
            SqlParameter[] parametros = new SqlParameter[]
            {
                 new SqlParameter("@idProv", idProvincia)
            };

            DB accesoDatos = new DB();
            return accesoDatos.ObtenerDataTable(consulta, parametros);
        }

        public DataTable EspecialidadConMasTurnos()
        {
            string query = @"
            SELECT TOP 5 
                SP.NAME_SPE AS Especialidad,  -- Usando alias
                COUNT(A.ID_APPO) AS TotalTurnos
            FROM SPECIALITY SP
            INNER JOIN DOCTOR D ON SP.ID_SPE = D.ID_SPE_DOC
            LEFT JOIN APPOINTMENT A ON D.ID_USER = A.ID_USER_DOCTOR
            GROUP BY SP.NAME_SPE
            ORDER BY TotalTurnos DESC";

            DB datos = new DB();
            return datos.ObtenerDataTable(query, null);
        }

        public DataTable ObtenerMedicosFiltrados(int idSpe)
        {
            string consulta = "SELECT ID_USER, NAME_DOC + ' ' + SURNAME_DOC AS FULL_NAME FROM DOCTOR WHERE ID_SPE_DOC = @idSpe";
            SqlParameter[] parametros = new SqlParameter[]
            {
                 new SqlParameter("@idSpe", idSpe)
            };

            DB accesoDatos = new DB();
            return accesoDatos.ObtenerDataTable(consulta, parametros);
        }

    }

}
