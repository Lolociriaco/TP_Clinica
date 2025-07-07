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
    public class AdminPatientsDao
    {
        public DataTable ObtenerPacientesFiltrados(string state, string name, string dni, string sexo)
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


            List<SqlParameter> parametros = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(state))
            {
                query += " AND P.ID_STATE_PAT = @state";
                parametros.Add(new SqlParameter("@state", state));
            }

            if (!string.IsNullOrEmpty(sexo))
            {
                query += " AND P.GENDER_PAT = @sexo";
                parametros.Add(new SqlParameter("@sexo", sexo));
            }

            if (!string.IsNullOrEmpty(name))
            {
                query += " AND P.NAME_PAT LIKE @name";
                parametros.Add(new SqlParameter("@name", "%" + name + "%"));
            }

            if (!string.IsNullOrEmpty(dni))
            {
                query += " AND P.DNI_PAT LIKE @dni";
                parametros.Add(new SqlParameter("@dni", dni + "%"));
            }

            DB dB = new DB();
            return dB.ObtenerListDT(query, parametros);
        }

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

            using (SqlConnection connection = new SqlConnection(Conexion.Cadena))
            {
                SqlCommand cmd = new SqlCommand(query, connection);


                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

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

        // ACTUALIZACION DEL PACIENTE
        public bool updatePatient(Paciente paciente)
        {
            DB db = new DB();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@nombre", paciente.Nombre),
                new SqlParameter("@apellido", paciente.Apellido),
                new SqlParameter("@dni", paciente.DNI),
                new SqlParameter("@direccion", paciente.Direccion),
                new SqlParameter("@correo", paciente.CorreoElectronico),
                new SqlParameter("@telefono", paciente.Telefono),
                new SqlParameter("@nacionalidad", paciente.Nacionalidad),
                new SqlParameter("@fechaNac", paciente.FechaNacimiento),
                new SqlParameter("@sexo", paciente.Sexo),
                new SqlParameter("@idLoc", paciente.Localidad),
                new SqlParameter("@idProv", paciente.Provincia)
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
                    ID_CITY_PAT = @idLoc,
                    ID_STATE_PAT = @idProv
                WHERE DNI_PAT = @dni";


            return db.updateUser(query, parametros);
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

        // CONSULTA PARA VERIFICAR SI EL TELEFONO DE PACIENTE YA EXISTE
        public bool ExisteTelefonoPaciente(string telefono)
        {
            string query = "SELECT COUNT(*) FROM PATIENTS WHERE PHONE_PAT = @telefono";
            SqlParameter[] parametros = {
                new SqlParameter("@telefono", telefono)
            };

            DB db = new DB();
            int cantidad = Convert.ToInt32(db.EjecutarEscalar(query, parametros));
            return cantidad > 0;
        }

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
    }
}
