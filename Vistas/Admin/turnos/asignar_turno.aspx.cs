using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio.Admin;
using Negocio;
using Entidades;
using System.Globalization;
using Negocio.Shared;

namespace Vistas.Admin.turnos
{
    public partial class asignar_turno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEspecialidades();
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
            AdminDoctorManager adminManager = new AdminDoctorManager(); 
            DataTable dtSexos = adminManager.ObtenerEspecialidades();

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
                AdminDoctorManager doctorManager = new AdminDoctorManager();
                DataTable dt = doctorManager.ObtenerMedicosFiltradosEspecialidad(idSpe);

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
            lblMensaje.Text = "";

            // VALIDACIONES
            if (!validarCamposVacios()) return;

            if (!validarDNI()) return;

            if (!validarDisponibilidadTurno(out DateTime fechaTurno, out TimeSpan horaTurno)) return;

            int id_medico = int.Parse(ddlDoctor.SelectedValue);

            validarDNI();

            // CARGAR TURNO
            Turnos turno = new Turnos
            {
                IdUsuarioMedico = id_medico,
                DniPacTurno = int.Parse(txtDNIPatient.Text),
                FechaTurno = fechaTurno,
                HoraTurno = horaTurno,
                EstadoTurno = "PENDING",
                ObservacionTurno = ""
            };

            AdminAppoManager manager = new AdminAppoManager();
            manager.CargarTurno(turno);

            lblMensaje.Text = "Appointment registered successfully.";
            lblMensaje.ForeColor = System.Drawing.Color.Green;

            // VACIAR CONTROLES
            ddlTime.SelectedIndex = 0;
            txtDate.Text = "";
            ddlSpeciality.SelectedIndex = 0;
            txtDNIPatient.Text = ""; 
            ddlDoctor.SelectedIndex = 0;
            
        }

        // VALIDAR QUE NO HAYAN CAMPOS VACIOS
        private bool validarCamposVacios()
        {
            if (string.IsNullOrEmpty(txtDNIPatient.Text) ||
                ddlSpeciality.SelectedValue == "0" || string.IsNullOrEmpty(ddlSpeciality.SelectedValue) ||
                ddlDoctor.SelectedValue == "0" || string.IsNullOrEmpty(ddlDoctor.SelectedValue) ||
                ddlTime.SelectedValue == "0" || string.IsNullOrEmpty(ddlTime.SelectedValue))
            {
                lblMensaje.Text = "Please, complete all the fields.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return false;
            }

            return true;
        }

        // VALIDAR QUE EL DNI EXISTE Y QUE ES VALIDO SU FORMATO
        private bool validarDNI()
        {
            AdminPatientsManager adminPatients = new AdminPatientsManager();
            AuthManager validar = new AuthManager();

            string dni = txtDNIPatient.Text.Trim();

            if (!validar.EsDniValido(dni))
            {
                validateDni.ErrorMessage = "Invalid DNI (format: 12345678)";
                validateDni.IsValid = false;
                return false;
            }

            if (!adminPatients.ExisteDniPaciente(int.Parse(dni)))
            {
                validateDni.ErrorMessage = "That DNI doesn't exist.";
                validateDni.IsValid = false;
                return false;
            }

            return true;
        }

        // VALIDAR QUE EL MEDICO ESTE DISPONIBLE ESE DIA, Y QUE LA FECHA SEA VALIDA
        private bool validarDisponibilidadTurno(out DateTime fechaTurno, out TimeSpan horaTurno)
        {
            fechaTurno = DateTime.MinValue;
            horaTurno = TimeSpan.Zero;

            if (!DateTime.TryParse(txtDate.Text, out fechaTurno))
            {
                lblMensaje.Text = "Invalid date.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return false;
            }

            if (fechaTurno.Date <= DateTime.Today)
            {
                lblMensaje.Text = "You can't make an appointment for a past date.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return false;
            }

            if (!TimeSpan.TryParse(ddlTime.SelectedValue, out horaTurno))
            {
                lblMensaje.Text = "Invalid time selected.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return false;
            }

            return true;
        }

        // FILTRAR HORARIOS
        public void filtrarHorarios()
        {
            if (ddlDoctor.SelectedIndex == 0 || string.IsNullOrEmpty(ddlDoctor.SelectedValue)) return;

            if (string.IsNullOrWhiteSpace(txtDate.Text)) return;

            if (!DateTime.TryParse(txtDate.Text, out DateTime fechaTurno)) return;

            AdminAppoManager manager = new AdminAppoManager();

            string day = fechaTurno.ToString("dddd", new CultureInfo("en-US")).ToUpper();

            int id_medico = int.Parse(ddlDoctor.SelectedValue);

            bool isAvailable = manager.medicoDisponible(day, id_medico);

            if (!isAvailable)
            {
                ddlTime.Items.Clear();
                ddlTime.Items.Insert(0, new ListItem("< Doctor selected doesn't work this day >", ""));
                return;
            }

            DoctorSchedule doctorSchedule = manager.ObtenerHorasTrabajadas(id_medico, day);

            if (doctorSchedule == null)
            {
                return;
            }

            List<TimeSpan> turnosAsignados = manager.ObtenerTurnosAsignados(id_medico, fechaTurno);
            TimeSpan duracionTurno = TimeSpan.FromMinutes(30);

            List<TimeSpan> horariosDisponibles = new List<TimeSpan>();
            TimeSpan horaActual = doctorSchedule._TimeStart;

            if (doctorSchedule._TimeEnd < doctorSchedule._TimeStart)
            {
                doctorSchedule._TimeEnd = doctorSchedule._TimeEnd.Add(new TimeSpan(1, 0, 0, 0));
            }

            while (horaActual < doctorSchedule._TimeEnd)
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

        // ACTUALIZACION DE HORARIOS SEGUN LA FECHA
        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            filtrarHorarios();
        }

        // ACTUALIZACION DE DDL DE MEDICOS SEGUN LA ESPECIALIDAD
        protected void ddlSpeciality_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            CargarMedicosPorEspecialidad();
        }

        // ACTUALIZACION DE HORARIOS SEGUN EL MEDICO
        protected void ddlDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            filtrarHorarios();
        }
    }
}