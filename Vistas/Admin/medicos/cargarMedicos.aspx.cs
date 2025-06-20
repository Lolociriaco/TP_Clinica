using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace Vistas.Admin.medicos
{
    public partial class cargarMedicos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarSexo();
                CargarEspecialidades();
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

        private void CargarEspecialidades()
        {
            Validar validar = new Validar(); // o el nombre real de tu clase de negocio
            DataTable dtEspecialidad = validar.ObtenerEspecialidades();

            ddlSpeciality.DataSource = dtEspecialidad;
            ddlSpeciality.DataTextField = "NOMBRE_ESP";
            ddlSpeciality.DataValueField = "ID_ESP";
            ddlSpeciality.DataBind();

            ddlSpeciality.Items.Insert(0, new ListItem("", ""));
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
            ddlLocality.DataTextField = "NOMBRE_LOC";
            ddlLocality.DataValueField = "ID_LOC";
            ddlLocality.DataBind();

            ddlLocality.Items.Insert(0, new ListItem("", ""));
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {

        }

        protected void btnConfirmarAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDNI.Text) || string.IsNullOrEmpty(txtFullName.Text) || string.IsNullOrEmpty(txtNation.Text) || string.IsNullOrEmpty(txtAddress.Text) 
               || string.IsNullOrEmpty(txtMail.Text) || string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtRepeatPass.Text) || string.IsNullOrEmpty(txtDay.Text) 
               || string.IsNullOrEmpty(txtTimes.Text) || string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                lblMensaje.Text = "Please, complete all the fields.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (ddlSpeciality.SelectedValue == "0" || string.IsNullOrEmpty(ddlSpeciality.SelectedValue) || ddlSexo.SelectedValue == "0" 
               || string.IsNullOrEmpty(ddlSexo.SelectedValue) || ddlCity.SelectedValue == "0" || string.IsNullOrEmpty(ddlCity.SelectedValue) 
               || ddlLocality.SelectedValue == "0" || string.IsNullOrEmpty(ddlLocality.SelectedValue))
            {
                lblMensaje.Text = "Please, complete all the fields.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;

            }

            //lblMensaje.Text = $"Valores: pass1={txtPassword.Text}, pass2={txtRepeatPass.Text}";
            if (txtPassword.Text.Trim() != txtRepeatPass.Text.Trim())
            {
                lblMensaje.Text = "Passwords don't match.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                txtRepeatPass.Text = "";
                return;
            }
            

            Validar validar = new Validar();

            string nombreCompleto = txtFullName.Text.Trim();
            string[] partes = nombreCompleto.Split(' ');

            string nombre = partes[0];
            string apellido = partes.Length > 1 ? string.Join(" ", partes.Skip(1)) : "";

            Usuario user = new Usuario
            {
                NombreUsuario = txtUser.Text,
                Contrasena = txtPassword.Text,
                TipoUsuario = "MEDICO"
            };

            Medico medico = new Medico
            {
                Nombre = nombre,
                Apellido = apellido,
                DNI = int.Parse(txtDNI.Text),
                Localidad = ddlLocality.SelectedValue,
                Provincia = ddlCity.SelectedValue,
                Nacionalidad = txtNation.Text,
                CorreoElectronico = txtMail.Text,
                Direccion = txtAddress.Text,
                Telefono = txtPhone.Text,
                Sexo = ddlSexo.SelectedValue,
                Especialidad = ddlSpeciality.SelectedValue,
                DiasYHorariosAtencion = txtTimes.Text,
                Usuario = txtUser.Text,
                FechaNacimiento = DateTime.Parse(txtBirth.Text),
                Contrasena = txtPassword.Text,
                RepeContrasena = txtRepeatPass.Text
            };

            int idUsuario = validar.AgregarUsuario(user); // ← obtenés el ID
            medico.Legajo = idUsuario;                    // ← se lo asignás al médico
            validar.AgregarMedico(medico);

            lblMensaje.Text = "¡Doctor added succesfully!";
            lblMensaje.ForeColor = System.Drawing.Color.Green;

            txtDNI.Text = "";
            txtFullName.Text = "";
            ddlLocality.SelectedIndex = 0;
            ddlCity.SelectedIndex = 0;
            ddlSexo.SelectedIndex = 0;
            txtNation.Text = "";
            txtAddress.Text = "";
            txtMail.Text = "";
            txtBirth.Text = "";
            txtPhone.Text = "";
            txtRepeatPass.Text = "";
            txtDay.Text = "";
            txtTimes.Text = "";
            txtUser.Text = "";
            txtPassword.Text = "";
            ddlSpeciality.SelectedIndex = 0;
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {

        }
    }
}