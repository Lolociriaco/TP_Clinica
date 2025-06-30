using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace Vistas
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text;
            string password = txtPassword.Text;

            txtUser.Text = "";
            txtPassword.Text = "";

            Validar validar = new Validar();

            string role = validar.ValidarUsuario(username, password);

            Session["username"] = username;
            Session["role"] = role;

            if(role == "ADMIN")
            {
                Response.Redirect("~/Admin/pacientes/abmlPaciente.aspx");
                return;
            }
            else if( role == "DOCTOR")
            {
                Response.Redirect("~/Medicos/turnos.aspx");
                return;
            }
            
            lblError.Text = "Wrong user or password";
        }
    }
}