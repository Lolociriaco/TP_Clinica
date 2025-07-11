﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Negocio.Shared;

namespace Vistas
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // BOTON DE LOGIN CLICKEADO
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPassword.Text.Trim();

            txtUser.Text = "";
            txtPassword.Text = "";

            AuthManager auth = new AuthManager();

            string role = auth.ValidarUsuario(username, password);

            Session["username"] = username;
            Session["role"] = role;

            // SI ES ADMIN, REDIRIGE A ADMIN PACIENTES
            if(role == "ADMIN")
            {
                Response.Redirect("~/Admin/pacientes/abmlPaciente.aspx");
                return;
            }
            // SI ES DOCTOR, REDIRIGE A MEDICOS 
            else if( role == "DOCTOR")
            {
                Response.Redirect("~/Medicos/turnos.aspx");
                return;
            }
            
            // SI NO ES NINGUNO, ERROR
            lblError.Text = "Wrong user or password";
        }
    }
}