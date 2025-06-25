using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace Vistas.Admin.pacientes
{
    public partial class cargarPaciente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarSexo();
                CargarLocalidadesPorProvincia();
                CargarProvincia();
            }

            if (Session["role"] == null || Session["role"].ToString() != "ADMIN")
            {
                Response.Redirect("~/Login.aspx");
            }

            this.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            username.Text = Session["username"].ToString();
        }

        // VOLVER A LOGIN
        protected void btnConfirmarLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx"); 
        }

        // CARGA DE DDL SEXO
        private void CargarSexo()
        {
            Validar validar = new Validar(); 
            DataTable dtSexos = validar.ObtenerSexoPaciente();

            ddlSexo.DataSource = dtSexos;
            ddlSexo.DataTextField = "GENDER_PAT";
            ddlSexo.DataValueField = "GENDER_PAT";
            ddlSexo.DataBind();

            ddlSexo.Items.Insert(0, new ListItem("", ""));
        }

        // CARGA DE DDL PROVINCIA
        private void CargarProvincia()
        {
            Validar validar = new Validar(); 
            DataTable dtCity = validar.ObtenerProvincia();

            ddlCity.DataSource = dtCity;
            ddlCity.DataTextField = "NAME_STATE";
            ddlCity.DataValueField = "ID_STATE";
            ddlCity.DataBind();

            ddlCity.Items.Insert(0, new ListItem("", ""));
        }


        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarLocalidadesPorProvincia();
        }

        private void CargarLocalidadesPorProvincia()
        {
            int idProvincia;
            if (int.TryParse(ddlCity.SelectedValue, out idProvincia))
            {
                Validar validar = new Validar();
                DataTable dt = validar.ObtenerLocalidadesFiltradas(idProvincia);

                ddlLocality.DataSource = dt;
                ddlLocality.DataTextField = "NAME_CITY";
                ddlLocality.DataValueField = "ID_CITY";
                ddlLocality.DataBind();
                ddlLocality.Items.Insert(0, new ListItem("< Select >", ""));
            }
            else
            {
                ddlLocality.Items.Clear();
                ddlLocality.Items.Insert(0, new ListItem("< Select a city first >", ""));
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {


        }

        // AGREGAR PACIENTE
        protected void btnConfirmarAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDNI.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtSurname.Text) || string.IsNullOrEmpty(txtNation.Text) 
               || string.IsNullOrEmpty(txtAddress.Text) || string.IsNullOrEmpty(txtMail.Text) || string.IsNullOrEmpty(txtPhone.Text) 
               || string.IsNullOrEmpty(txtBirth.Text))
            {
                lblMensaje.Text = "Please, complete all the fields.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (ddlSexo.SelectedValue == "0" || string.IsNullOrEmpty(ddlSexo.SelectedValue) || ddlCity.SelectedValue == "0" || string.IsNullOrEmpty(ddlCity.SelectedValue) || ddlLocality.SelectedValue == "0" || string.IsNullOrEmpty(ddlLocality.SelectedValue))
            {
                lblMensaje.Text = "Please, complete all the fields.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            DateTime fechaNacimiento;

            if (!DateTime.TryParse(txtBirth.Text, out fechaNacimiento))
            {
                validateBirthday.ErrorMessage = "Enter a valid birth date.";
                validateBirthday.IsValid = false;
                return;
            }

            int edad = DateTime.Today.Year - fechaNacimiento.Year;
            if (fechaNacimiento > DateTime.Today.AddYears(-edad)) edad--;

            if (edad < 18)
            {
                validateBirthday.ErrorMessage = "Must be at least 18 years old.";
                validateBirthday.IsValid = false;
                return;
            }

            Validar validar = new Validar();

            string dni = txtDNI.Text.Trim();

            if (!validar.EsDniValido(dni))
            {
                validateDni.ErrorMessage = "Invalid DNI (format: 12345678)";
                validateDni.IsValid = false;
                return;
            }

            if (!Page.IsValid) return; 

            if (validar.ExisteDni(int.Parse(txtDNI.Text)))
            {
                validateDni.ErrorMessage = "That DNI is already registered.";
                validateDni.IsValid = false; 
                return;
            }

            if (!Page.IsValid) return; 

            if (validar.ExisteTelefono(txtPhone.Text))
            {
                validatePhone.ErrorMessage = "That phone number is already registered.";
                validatePhone.IsValid = false; 
                return;
            }

            Paciente paciente = new Paciente
            {
                Nombre = txtName.Text,
                Apellido = txtSurname.Text,
                DNI = int.Parse(txtDNI.Text),
                Localidad = ddlLocality.SelectedValue,
                Provincia = ddlCity.SelectedValue,
                FechaNacimiento = DateTime.Parse(txtBirth.Text),
                Nacionalidad = txtNation.Text,
                CorreoElectronico = txtMail.Text,
                Direccion = txtAddress.Text,
                Telefono = txtPhone.Text,
                Sexo = ddlSexo.SelectedValue,
            };

            validar.AgregarPaciente(paciente);

            lblMensaje.Text = "¡Patient added succesfully!";
            lblMensaje.ForeColor = System.Drawing.Color.Green;

            txtDNI.Text = "";
            txtName.Text = "";
            txtSurname.Text = "";
            ddlLocality.SelectedIndex = 0;
            ddlCity.SelectedIndex = 0;
            ddlSexo.SelectedIndex = 0;
            txtNation.Text = "";
            txtAddress.Text = "";
            txtMail.Text = "";
            txtPhone.Text = "";
            txtBirth.Text = "";
        }
    }
}