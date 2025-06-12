using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas.Admin.medicos
{
    public partial class cargarMedicos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["role"] == null || Session["role"].ToString() != "Admin")
            {
                //Response.Redirect("~/Login.aspx");
            }

            //username.Text = Session["username"].ToString();
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
            txtLocality.Text = "";
            txtCity.Text = "";
            ddlSexo.Text = "";
            txtNation.Text = "";
            txtAddress.Text = "";
            txtMail.Text = "";
            txtPhone.Text = "";
            txtLegajo.Text = "";
            txtDay.Text = "";
            txtTimes.Text = "";
            txtUser.Text = "";
            txtPassword.Text = "";
            ddlSpeciality.Text = "";
        }

        protected void btnConfirmarAgregar_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {

        }
    }
}