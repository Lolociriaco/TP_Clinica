using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;
using System.Globalization;

namespace Vistas.Admin.turnos
{
    public partial class asignar_turno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEspecialidades();
                //CargarDoctores();
                //CargarMedicosPorEspecialidad();
            }

            if (Session["role"] == null || Session["role"].ToString() != "ADMIN")
            {
                Response.Redirect("~/Login.aspx");
            }

            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            username.Text = Session["username"].ToString();
        }

        // VOLVER A LOGIN
        protected void btnConfirmarLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }

        // CARGAR ESPECIALIDADES EN DDL
        private void CargarEspecialidades()
        {
            Validar validar = new Validar(); 
            DataTable dtSexos = validar.ObtenerEspecialidades();

            ddlSpeciality.DataSource = dtSexos;
            ddlSpeciality.DataTextField = "NAME_SPE";
            ddlSpeciality.DataValueField = "ID_SPE";
            ddlSpeciality.DataBind();

            ddlSpeciality.Items.Insert(0, new ListItem("< SELECT >", ""));

        }

        // FILTRAR MEDICOS POR ESPECIALIDAD
        private void CargarMedicosPorEspecialidad()
        {
            int idSpe;
            if (int.TryParse(ddlSpeciality.SelectedValue, out idSpe))
            {
                Validar validar = new Validar();
                DataTable dt = validar.ObtenerMedicosFiltrados(idSpe);

                ddlDoctor.DataSource = dt;
                ddlDoctor.DataTextField = "FULL_NAME";
                ddlDoctor.DataValueField = "ID_USER";
                ddlDoctor.DataBind();
                ddlDoctor.Items.Insert(0, new ListItem("< SELECT >", ""));
            }
            else
            {
                ddlDoctor.Items.Clear();
                ddlDoctor.Items.Insert(0, new ListItem("< Select a speciality first >", ""));
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {

        }

        // ASIGNAR TURNO
        protected void btnConfirmarAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDNIPatient.Text))
            {
                lblMensaje.Text = "Please, complete all the fields.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (ddlSpeciality.SelectedValue == "0" || string.IsNullOrEmpty(ddlSpeciality.SelectedValue) || ddlTime.SelectedValue == "0" || string.IsNullOrEmpty(ddlTime.SelectedValue) ||
                ddlDoctor.SelectedValue == "0" || string.IsNullOrEmpty(ddlDoctor.SelectedValue))
            {
                lblMensaje.Text = "Please, complete all the fields.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;

            }

            DateTime fechaTurno = DateTime.Parse(txtDate.Text);

            if (fechaTurno.Date <= DateTime.Today)
            {
                lblMensaje.Text = "You can't make an appointment for a past date.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Validacion de medico disponible el dia seleccionado

            TimeSpan horaTurno = TimeSpan.Parse(ddlTime.SelectedValue);
            int id_medico = int.Parse(ddlDoctor.SelectedValue);

            //obtener dia para validar que el medico labure ese dia
            DayOfWeek dayWeek = fechaTurno.DayOfWeek;
            string diaTurno = fechaTurno.ToString("dddd"); // "Monday"

            Validar validar = new Validar();

            string dni = txtDNIPatient.Text.Trim();

            if (!validar.EsDniValido(dni))
            {
                validateDni.ErrorMessage = "Invalid DNI (format: 12345678)";
                validateDni.IsValid = false;
                return;
            }

            if (!Page.IsValid) return;

            if (!validar.ExisteDni(int.Parse(txtDNIPatient.Text)))
            {
                validateDni.ErrorMessage = "That DNI doesn't exist.";
                validateDni.IsValid = false;
                return;
            }

            //validar.MedicoDisponible(diaTurno, horaTurno, id_medico);

            Turnos turno = new Turnos
            {
                IdUsuarioMedico = id_medico,
                DniPacTurno = int.Parse(txtDNIPatient.Text),
                FechaTurno = fechaTurno,
                HoraTurno = horaTurno,
                EstadoTurno = "PENDING",
                ObservacionTurno = ""
            };

            validar.CargarTurno(turno);

            lblMensaje.Text = "Appointment registered successfully.";
            lblMensaje.ForeColor = System.Drawing.Color.Green;

            ddlTime.SelectedIndex = 0;
            txtDate.Text = "";
            ddlSpeciality.SelectedIndex = 0;
            txtDNIPatient.Text = ""; // para debugerarsfra
            ddlDoctor.SelectedIndex = 0;
            
        }

        // FILTRAR HORARIOS
        public void filtrarHorarios()
        {
            if (ddlDoctor.SelectedIndex == 0) return;

            if (string.IsNullOrWhiteSpace(txtDate.Text)) return;

            if (!DateTime.TryParse(txtDate.Text, out DateTime fechaTurno)) return;

            AdminDoctorManager manager = new AdminDoctorManager();

            DayOfWeek dayWeek = fechaTurno.DayOfWeek;
            string day = fechaTurno.ToString("dddd", new CultureInfo("en-US")).ToUpper();

            int id_medico = int.Parse(ddlDoctor.SelectedValue);

            bool isAvailable = manager.medicoDisponible(day, id_medico);

            if (!isAvailable)
            {
                ddlTime.Items.Clear();
                ddlTime.Items.Insert(0, new ListItem("< Doctor selected doesn't work this day >", ""));
                return;
            }

            DoctorSchedule doctorSchedule = manager.ObtenerHorasTrabajadas(id_medico);

            if (doctorSchedule == null)
            {
                return;
            }

            List<TimeSpan> turnosAsignados = manager.ObtenerTurnosAsignados(id_medico, fechaTurno);
            TimeSpan duracionTurno = TimeSpan.FromMinutes(30);

            List<TimeSpan> horariosDisponibles = new List<TimeSpan>();
            TimeSpan horaActual = doctorSchedule._TimeStart;

            while (horaActual + duracionTurno <= doctorSchedule._TimeEnd)
            {
                if (!turnosAsignados.Contains(horaActual))
                {
                    horariosDisponibles.Add(horaActual);
                }

                horaActual = horaActual.Add(duracionTurno);
            }

            ddlTime.Items.Clear();
            ddlTime.Items.Insert(0, new ListItem("< SELECT >", ""));


            foreach (TimeSpan hora in horariosDisponibles)
            {
                ddlTime.Items.Add(new ListItem(hora.ToString(@"hh\:mm"), hora.ToString()));
            }
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            filtrarHorarios();
        }

        protected void ddlSpeciality_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarMedicosPorEspecialidad();
        }

        protected void ddlDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrarHorarios();
        }
    }
}