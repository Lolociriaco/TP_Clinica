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

            if (Session["role"] == null || Session["role"].ToString() != "ADMINISTRADOR")
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

        // CARGAR DDL SEXOS
        private void CargarSexo()
        {
            Validar validar = new Validar(); 
            DataTable dtSexos = validar.ObtenerSexoMedico();

            ddlSexo.DataSource = dtSexos;
            ddlSexo.DataTextField = "SEXO_MED";
            ddlSexo.DataValueField = "SEXO_MED";
            ddlSexo.DataBind();

            ddlSexo.Items.Insert(0, new ListItem("", ""));
        }

        // CARGAR DDL ESPECIALIDADES
        private void CargarEspecialidades()
        {
            Validar validar = new Validar(); 
            DataTable dtEspecialidad = validar.ObtenerEspecialidades();

            ddlSpeciality.DataSource = dtEspecialidad;
            ddlSpeciality.DataTextField = "NOMBRE_ESP";
            ddlSpeciality.DataValueField = "ID_ESP";
            ddlSpeciality.DataBind();

            ddlSpeciality.Items.Insert(0, new ListItem("", ""));
        }

        // CARGAR DDL PROVINCIA
        private void CargarProvincia()
        {
            Validar validar = new Validar(); 
            DataTable dtCity = validar.ObtenerProvincia();

            ddlCity.DataSource = dtCity;
            ddlCity.DataTextField = "NOMBRE_PROV";
            ddlCity.DataValueField = "ID_PROV";
            ddlCity.DataBind();

            ddlCity.Items.Insert(0, new ListItem("", ""));
        }

        // CARGAR DDL LOCALIDADES
        private void CargarLocalidad()
        {
            Validar validar = new Validar(); 
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

        // AGREGAR MEDICO
        protected void btnConfirmarAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDNI.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtSurname.Text) || string.IsNullOrEmpty(txtNation.Text) || string.IsNullOrEmpty(txtAddress.Text)
               || string.IsNullOrEmpty(txtMail.Text) || string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtRepeatPass.Text) || string.IsNullOrEmpty(chkDias.Text)
               || string.IsNullOrEmpty(rblHorarios.Text) || string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPassword.Text))
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

            if (txtPassword.Text.Trim() != txtRepeatPass.Text.Trim())
            {
                lblMensaje.Text = "Passwords don't match.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                txtRepeatPass.Text = "";
                return;
            }


            DateTime fechaNacimiento;

            if (!DateTime.TryParse(txtBirth.Text, out fechaNacimiento))
            {
                // Mostrar error si no es una fecha válida
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

            if (validar.ExisteTelefono(txtPhone.Text))
            {
                validatePhone.ErrorMessage = "That phone number is already registered.";
                validatePhone.IsValid = false; 
                return;
            }
            
            if (validar.ExisteUsuario(txtUser.Text))
            {
                validateUser.ErrorMessage = "That username is already registered.";
                validateUser.IsValid = false; 
                return;
            }

            //  OBTENER DIAS DEL CHECKBOXLIST

            List<string> diasSeleccionados = new List<string>();

            foreach(ListItem item in chkDias.Items)
            {
                if (item.Selected)
                {
                    diasSeleccionados.Add(item.Value);
                }
            }

            string diasYHorarios = rblHorarios.SelectedValue + " " + string.Join("-", diasSeleccionados);

            Usuario user = new Usuario
            {
                NombreUsuario = txtUser.Text,
                Contrasena = txtPassword.Text,
                TipoUsuario = "MEDICO"
            };

            int idUsuario = validar.AgregarUsuario(user); 

            Medico medico = new Medico
            {
                Nombre = txtName.Text,
                Apellido = txtSurname.Text,
                DNI = int.Parse(txtDNI.Text),
                Localidad = ddlLocality.SelectedValue,
                Provincia = ddlCity.SelectedValue,
                Nacionalidad = txtNation.Text,
                CorreoElectronico = txtMail.Text,
                Direccion = txtAddress.Text,
                Telefono = txtPhone.Text,
                Sexo = ddlSexo.SelectedValue,
                Especialidad = ddlSpeciality.SelectedValue,
                DiasYHorariosAtencion = diasYHorarios,
                Usuario = txtUser.Text,
                FechaNacimiento = DateTime.Parse(txtBirth.Text),
                Contrasena = txtPassword.Text,
                RepeContrasena = txtRepeatPass.Text
            };

            medico._id_usuario = idUsuario;                    
            validar.AgregarMedico(medico);

            string[] horas = rblHorarios.SelectedValue.Split('-');
            TimeSpan horaInicio = TimeSpan.Parse(horas[0]);
            TimeSpan horaFin = TimeSpan.Parse(horas[1]);

            foreach (ListItem item in chkDias.Items)
            {
                if (item.Selected)
                {
                    string dia = item.Value;
                    validar.InsertarHorarioMedico(idUsuario, dia, horaInicio, horaFin);
                }
            }

            lblMensaje.Text = "¡Doctor added succesfully!";
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
            txtBirth.Text = "";
            txtPhone.Text = "";
            txtRepeatPass.Text = "";
            rblHorarios.Text = "";
            chkDias.Text = "";
            txtUser.Text = "";
            txtPassword.Text = "";
            ddlSpeciality.SelectedIndex = 0;

            // DEJAR CHECK BOX LIST

            foreach(ListItem item in chkDias.Items)
            {
                item.Selected = false;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {

        }
    }
}