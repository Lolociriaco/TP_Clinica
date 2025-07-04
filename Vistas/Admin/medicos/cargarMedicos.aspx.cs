﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
                CargarProvincia();
                CargarDias();
                
            }

            if (Session["role"] == null || Session["role"].ToString() != "ADMIN")
            {
                Response.Redirect("~/Login.aspx");
            }

            this.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            username.Text = Session["username"].ToString();
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {

        }

        // AGREGAR MEDICO
        protected void btnConfirmarAgregar_Click(object sender, EventArgs e)
        {
            // VALIDACIONES
            if (!validarCamposVacios()) return;

            if (!validarContraseñas()) return;

            if (!validarFechaNacimiento()) return;

            if (!validarDNI()) return;

            if (!validarUserYTelefono()) return;

            if (!ValidarYObtenerDias(out List<string> diasAtencion, out List<TimeSpan> horasInicio, out List<TimeSpan> horasFin))
                return;

            // SI SE VALIDO TODO, SE AGREGA EL MEDICO
            Validar validar = new Validar();
            Usuario user = new Usuario
            {
                NombreUsuario = txtUser.Text,
                Contrasena = txtPassword.Text,
                TipoUsuario = "DOCTOR"
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
                Usuario = txtUser.Text,
                FechaNacimiento = DateTime.Parse(txtBirth.Text),
                Contrasena = txtPassword.Text,
                RepeContrasena = txtRepeatPass.Text
            };

            medico._id_usuario = idUsuario;                    
            validar.AgregarMedico(medico);

            for (int i = 0; i < diasAtencion.Count; i++)
            {
                validar.InsertarHorarioMedico(idUsuario, diasAtencion[i], horasInicio[i], horasFin[i]);
            }

            // LIMPIAR CONTROLES
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
            txtUser.Text = "";
            txtPassword.Text = "";
            ddlSpeciality.SelectedIndex = 0;

            // DEJAR CHECK BOX LIST

            diasAtencion.Clear();
            horasInicio.Clear();
            horasFin.Clear();
        }

        // VALIDAR LOS CAMPOS VACIOS
        private bool validarCamposVacios()
        {
            if (string.IsNullOrEmpty(txtDNI.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtSurname.Text) || string.IsNullOrEmpty(txtNation.Text) || string.IsNullOrEmpty(txtAddress.Text)
               || string.IsNullOrEmpty(txtMail.Text) || string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtRepeatPass.Text)
               || string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                lblMensaje.Text = "Please, complete all the fields.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return false;
            }

            if (ddlSpeciality.SelectedValue == "0" || string.IsNullOrEmpty(ddlSpeciality.SelectedValue) || ddlSexo.SelectedValue == "0"
               || string.IsNullOrEmpty(ddlSexo.SelectedValue) || ddlCity.SelectedValue == "0" || string.IsNullOrEmpty(ddlCity.SelectedValue)
               || ddlLocality.SelectedValue == "0" || string.IsNullOrEmpty(ddlLocality.SelectedValue))
            {
                lblMensaje.Text = "Please, complete all the fields.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return false;

            }

            return true;
        }

        private bool validarFechaNacimiento()
        {
            DateTime fechaNacimiento;

            if (!DateTime.TryParse(txtBirth.Text, out fechaNacimiento))
            {
                validateBirthday.ErrorMessage = "Enter a valid birth date.";
                validateBirthday.IsValid = false;
                return false;
            }

            int edad = DateTime.Today.Year - fechaNacimiento.Year;
            if (fechaNacimiento > DateTime.Today.AddYears(-edad)) edad--;

            if (edad < 18)
            {
                validateBirthday.ErrorMessage = "Must be at least 18 years old.";
                validateBirthday.IsValid = false;
                return false;
            }

            return true;
        }

        // VALIDAR QUE LAS CONTRASEÑAS SEAN IGUALES
        private bool validarContraseñas() 
        {
            if (txtPassword.Text.Trim() != txtRepeatPass.Text.Trim())
            {
                lblMensaje.Text = "Passwords don't match.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                txtRepeatPass.Text = "";
                return false;
            }
            return true;
        }

        // VALIDAR QUE EL USUARIO Y EL TELEFONO NO EXISTAN
        private bool validarUserYTelefono()
        {
            AdminDoctorManager adminDoctor = new AdminDoctorManager();
            Validar validar = new Validar();

            if (adminDoctor.ExisteTelefonoDoctor(txtPhone.Text))
            {
                validatePhone.ErrorMessage = "That phone number is already registered.";
                validatePhone.IsValid = false;
                return false;
            }

            if (validar.ExisteUsuario(txtUser.Text))
            {
                validateUser.ErrorMessage = "That username is already registered.";
                validateUser.IsValid = false;
                return false;
            }

            return true;
        }

        // VALIDAR FORMATO Y EXISTENCIA DEL DNI
        private bool validarDNI()
        {
            AdminDoctorManager adminDoctor = new AdminDoctorManager();

            Validar validar = new Validar();

            string dni = txtDNI.Text.Trim();

            if (!validar.EsDniValido(dni))
            {
                validateDni.ErrorMessage = "Invalid DNI (format: 12345678)";
                validateDni.IsValid = false;
                return false;
            }

            if (!adminDoctor.ExisteDniDoctor(int.Parse(dni)))
            {
                validateDni.ErrorMessage = "That DNI doesn't exist.";
                validateDni.IsValid = false;
                return false;
            }

            return true;
        }

        // VALIDAR QUE LOS DIAS Y HORARIOS ESTEN INGRESADOS
        private bool ValidarYObtenerDias(out List<string> dias, out List<TimeSpan> horasInicio, out List<TimeSpan> horasFin)
        {
            dias = new List<string>();
            horasInicio = new List<TimeSpan>();
            horasFin = new List<TimeSpan>();

            bool alMenosUno = false;

            foreach (RepeaterItem item in rptDias.Items)
            {
                Label lblDia = (Label)item.FindControl("lblDia");
                CheckBox chkAtiende = (CheckBox)item.FindControl("chkDia");
                TextBox txtHoraInicio = (TextBox)item.FindControl("txtHoraInicio");
                TextBox txtHoraFin = (TextBox)item.FindControl("txtHoraFin");

                if (chkAtiende.Checked)
                {
                    if (TimeSpan.TryParse(txtHoraInicio.Text.Trim(), out TimeSpan ini) &&
                        TimeSpan.TryParse(txtHoraFin.Text.Trim(), out TimeSpan fin))
                    {
                        dias.Add(lblDia.Text);
                        horasInicio.Add(ini);
                        horasFin.Add(fin);
                        alMenosUno = true;
                    }
                    else
                    {
                        lblMensaje.Text = "Please enter valid hours for the selected days.";
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                        return false;
                    }
                }
            }

            if (!alMenosUno)
            {
                lblMensaje.Text = "Please select at least one day with valid hours.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return false;
            }

            return true;
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
            AdminDoctorManager admin = new AdminDoctorManager();
            DataTable dtSexos = admin.ObtenerSexoMedico();

            ddlSexo.DataSource = dtSexos;
            ddlSexo.DataTextField = "GENDER_DOC";
            ddlSexo.DataValueField = "GENDER_DOC";
            ddlSexo.DataBind();

            ddlSexo.Items.Insert(0, new ListItem("< SELECT >", ""));
        }

        // CARGAR DDL ESPECIALIDADES
        private void CargarEspecialidades()
        {
            AdminDoctorManager adminDoctor = new AdminDoctorManager();
            DataTable dtEspecialidad = adminDoctor.ObtenerEspecialidades();

            ddlSpeciality.DataSource = dtEspecialidad;
            ddlSpeciality.DataTextField = "NAME_SPE";
            ddlSpeciality.DataValueField = "ID_SPE";
            ddlSpeciality.DataBind();

            ddlSpeciality.Items.Insert(0, new ListItem("< SELECT >", ""));
        }

        // CARGAR DDL PROVINCIA
        private void CargarProvincia()
        {
            Validar validar = new Validar();
            DataTable dtCity = validar.ObtenerProvincia();

            ddlCity.DataSource = dtCity;
            ddlCity.DataTextField = "NAME_STATE";
            ddlCity.DataValueField = "ID_STATE";
            ddlCity.DataBind();

            ddlCity.Items.Insert(0, new ListItem("< SELECT >", ""));
        }

        // CARGAR LOCALIDADES SEGUN LA PROVINCIA ELEGIDA
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
                ddlLocality.Items.Insert(0, new ListItem("< SELECT >", ""));
            }
            else
            {
                ddlLocality.Items.Clear();
                ddlLocality.Items.Insert(0, new ListItem("< Select a state first >", ""));
            }
        }

        // CARGAR DIAS
        private void CargarDias()
        {
            List<string> dias = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            rptDias.DataSource = dias;
            rptDias.DataBind();
        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarLocalidadesPorProvincia();
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {

        }
    }
}