using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;

namespace Vistas.Admin.turnos
{
    public partial class asignar_turno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEspecialidades();
                CargarDoctores();
            }

            if (Session["role"] == null || Session["role"].ToString() != "ADMINISTRADOR")
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
            ddlSpeciality.DataTextField = "NOMBRE_ESP";
            ddlSpeciality.DataValueField = "NOMBRE_ESP";
            ddlSpeciality.DataBind();

            ddlSpeciality.Items.Insert(0, new ListItem("< SELECT >", ""));
        }

        private void CargarDoctores()
        {
            Validar validar = new Validar();
            DataTable dtDoctor = validar.getNombreYApellidoDoctores();

            ddlDoctor.DataSource = dtDoctor;
            ddlDoctor.DataTextField = "NOMBRE_COMPLETO";
            ddlDoctor.DataValueField = "ID_USUARIO";
            ddlDoctor.DataBind();

            ddlDoctor.Items.Insert(0, new ListItem("< SELECT >", ""));
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {

        }

        // ASIGNAR TURNO
        protected void btnConfirmarAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTime.Text) || string.IsNullOrEmpty(txtDNIPatient.Text))
            {
                lblMensaje.Text = "Please, complete all the fields.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (ddlSpeciality.SelectedValue == "0" || string.IsNullOrEmpty(ddlSpeciality.SelectedValue) || ddlDoctor.SelectedValue == "0" || string.IsNullOrEmpty(ddlDoctor.SelectedValue))
            {
                lblMensaje.Text = "Please, complete all the fields.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;

            }

            DateTime fechaTurno = DateTime.Parse(txtDate.Text);

            if (fechaTurno.Date <= DateTime.Today.AddDays(1))
            {
                lblMensaje.Text = "You can't make an appointment for a past date.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Validacion de medico disponible el dia seleccionado

            TimeSpan horaTurno = TimeSpan.Parse(txtTime.Text);
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

            txtTime.Text = "";
            txtDate.Text = "";
            ddlSpeciality.SelectedIndex = 0;
            txtDNIPatient.Text = diaTurno; // para debugerarsfra
            ddlDoctor.SelectedIndex = 0;
            
        }
    }
}