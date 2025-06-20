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
                CargarLocalidad();
                CargarProvincia();
            }

            if (Session["role"] == null || Session["role"].ToString() != "Admin")
            {
                Response.Redirect("~/Login.aspx");
            }

            username.Text = Session["username"].ToString();
        }
        protected void btnConfirmarLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx"); 
        }

        private void CargarSexo()
        {
            Validar validar = new Validar(); // o el nombre real de tu clase de negocio
            DataTable dtSexos = validar.ObtenerSexos();

            ddlSexo.DataSource = dtSexos;
            ddlSexo.DataTextField = "SEXO_PAC";
            ddlSexo.DataValueField = "SEXO_PAC";
            ddlSexo.DataBind();

            ddlSexo.Items.Insert(0, new ListItem("", ""));
        }

        private void CargarProvincia()
        {
            Validar validar = new Validar(); // o el nombre real de tu clase de negocio
            DataTable dtCity = validar.ObtenerProvincia();

            ddlCity.DataSource = dtCity;
            ddlCity.DataTextField = "NOMBRE_PROV";
            ddlCity.DataValueField = "ID_PROV";
            ddlCity.DataBind();

            ddlCity.Items.Insert(0, new ListItem("", ""));
        }

        private void CargarLocalidad()
        {
            Validar validar = new Validar(); // o el nombre real de tu clase de negocio
            DataTable dtLocality = validar.ObtenerLocalidad();

            ddlLocality.DataSource = dtLocality;
            ddlLocality.DataTextField  = "NOMBRE_LOC";
            ddlLocality.DataValueField = "ID_LOC";
            ddlLocality.DataBind();

            ddlLocality.Items.Insert(0, new ListItem("", ""));
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {


        }

        protected void btnConfirmarAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDNI.Text) || string.IsNullOrEmpty(txtFullName.Text) || string.IsNullOrEmpty(txtNation.Text) 
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

            Validar validar = new Validar();

            string nombreCompleto = txtFullName.Text.Trim();
            string[] partes = nombreCompleto.Split(' ');

            string nombre = partes[0];
            string apellido = partes.Length > 1 ? string.Join(" ", partes.Skip(1)) : "";

            Paciente paciente = new Paciente
            {
                Nombre = nombre,
                Apellido = apellido,
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
            txtFullName.Text = "";
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