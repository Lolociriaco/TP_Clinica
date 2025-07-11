﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;
using Negocio.Admin;
using Negocio.Shared;

namespace Vistas.Admin.pacientes
{
    public partial class cargarPaciente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["role"] == null || Session["role"].ToString() != "ADMIN")
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                CargarSexo();
                CargarLocalidadesPorProvincia();
                CargarProvincia();
                username.Text = Session["username"].ToString();
            }

            this.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {

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
            AdminPatientsManager admin = new AdminPatientsManager(); 
            DataTable dtSexos = admin.ObtenerSexoPaciente();

            ddlSexo.DataSource = dtSexos;
            ddlSexo.DataTextField = "GENDER_PAT";
            ddlSexo.DataValueField = "GENDER_PAT";
            ddlSexo.DataBind();

            ddlSexo.Items.Insert(0, new ListItem("< SELECT >", ""));
        }

        // CARGA DE DDL PROVINCIA
        private void CargarProvincia()
        {
            GetUbicationManager manager = new GetUbicationManager();
            DataTable dtCity = manager.ObtenerProvincia();

            ddlCity.DataSource = dtCity;
            ddlCity.DataTextField = "NAME_STATE";
            ddlCity.DataValueField = "ID_STATE";
            ddlCity.DataBind();

            ddlCity.Items.Insert(0, new ListItem("< SELECT >", ""));
        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarLocalidadesPorProvincia();
        }

        // CARGAR LOCALIDADES SEGUN LA PROVINCIA ELEGIDA
        private void CargarLocalidadesPorProvincia()
        {
            int idProvincia;
            if (int.TryParse(ddlCity.SelectedValue, out idProvincia))
            {
                GetUbicationManager manager = new GetUbicationManager();
                DataTable dt = manager.ObtenerLocalidadesFiltradas(idProvincia);

                ddlLocality.DataSource = dt;
                ddlLocality.DataTextField = "NAME_CITY";
                ddlLocality.DataValueField = "ID_CITY";
                ddlLocality.DataBind();
                ddlLocality.Items.Insert(0, new ListItem("< Select >", ""));
            }
            else
            {
                ddlLocality.Items.Clear();
                ddlLocality.Items.Insert(0, new ListItem("< Select a state first >", ""));
            }
        }

        // AGREGAR PACIENTE
        protected void btnConfirmarAgregar_Click(object sender, EventArgs e)
        {
            // VALIDACIONES
            if (!validarCamposVacios()) return;

            if (!validarFechaNacimiento()) return;

            if (!validarDNI()) return;

            if (!validarTelefono()) return;

            // CARGAR PACIENTE
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

            AdminPatientsManager manager = new AdminPatientsManager();
            manager.AgregarPaciente(paciente);

            lblMensaje.Text = "¡Patient added succesfully!";
            lblMensaje.ForeColor = System.Drawing.Color.Green;

            // VACIAR CONTROLES
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

        // VALIDAR FORMATO DE DNI VALIDO Y SI YA EXISTE
        private bool validarDNI()
        {
            AdminPatientsManager adminPatients = new AdminPatientsManager();
            AuthManager validar = new AuthManager();   

            string dni = txtDNI.Text.Trim();

            if (!validar.EsDniValido(dni))
            {
                validateDni.ErrorMessage = "Invalid DNI (format: 12345678)";
                validateDni.IsValid = false;
                return false;
            }

            if (!Page.IsValid) return false;

            if (adminPatients.ExisteDniPaciente(int.Parse(txtDNI.Text)))
            {
                validateDni.ErrorMessage = "That DNI is already registered.";
                validateDni.IsValid = false;
                return false;
            }
            return true;
        }

        // VALIDAR SI EL TELEFONO YA EXISTE
        private bool validarTelefono()
        {
            AdminPatientsManager adminPatients = new AdminPatientsManager();

            if (!Page.IsValid) return false;

            if (adminPatients.ExisteTelefonoPaciente(txtPhone.Text))
            {
                validatePhone.ErrorMessage = "That phone number is already registered.";
                validatePhone.IsValid = false;
                return false;
            }
            return true;   
        }

        // VALIDAR QUE LA FECHA SEA VALIDA
        private bool validarFechaNacimiento()
        {
            DateTime fechaNacimiento;

            if (!DateTime.TryParse(txtBirth.Text, out fechaNacimiento))
            {
                validateBirthday.ErrorMessage = "Enter a valid birth date.";
                validateBirthday.IsValid = false;
                return false;
            }

            if (fechaNacimiento > DateTime.Today)
            {
                validateBirthday.ErrorMessage = "Birth date cannot be in the future.";
                validateBirthday.IsValid = false;
                return false;
            }

            return true;
        }

        // VALIDAR QUE NO HAYAN CAMPOS VACIOS
        private bool validarCamposVacios()
        {
            if (string.IsNullOrEmpty(txtDNI.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtSurname.Text) || string.IsNullOrEmpty(txtNation.Text)
               || string.IsNullOrEmpty(txtAddress.Text) || string.IsNullOrEmpty(txtMail.Text) || string.IsNullOrEmpty(txtPhone.Text)
               || string.IsNullOrEmpty(txtBirth.Text))
            {
                lblMensaje.Text = "Please, complete all the fields.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return false;
            }

            if (ddlSexo.SelectedValue == "0" || string.IsNullOrEmpty(ddlSexo.SelectedValue) || ddlCity.SelectedValue == "0" || string.IsNullOrEmpty(ddlCity.SelectedValue) || ddlLocality.SelectedValue == "0" || string.IsNullOrEmpty(ddlLocality.SelectedValue))
            {
                lblMensaje.Text = "Please, complete all the fields.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return false;
            }
            return true;
        }
    }
}