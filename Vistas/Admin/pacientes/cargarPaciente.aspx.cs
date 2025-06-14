﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas.Admin.pacientes
{
    public partial class cargarPaciente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

        protected void btnConfirm_Click(object sender, EventArgs e)
        {

            txtDNI.Text = "";
            txtFullName.Text = "";
            ddlLocality.Text = "";
            ddlCity.Text = "";
            ddlSexo.Text = "";
            ddlNation.Text = "";
            txtAddress.Text = "";
            txtMail.Text = "";
            txtPhone.Text = "";
            txtBirth.Text = "";

        }

        protected void btnConfirmarAgregar_Click(object sender, EventArgs e)
        {

        }
    }
}