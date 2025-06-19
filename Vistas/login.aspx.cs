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

            validar.ValidarUsuario(username, password);

            // validar usuario puede devolver un string con el tipo de rol
            // o un null/string vacio si el usuario no existe

            //ingreso el rol por password para probar
            string role = password;

            Session["username"] = username;
            Session["role"] = role;

            if(role == "Admin")
            {
                Response.Redirect("~/Admin/pacientes/abmlPaciente.aspx");
            }
            else
            {
                Response.Redirect("~/Medicos/turnos.aspx");
            }
        }
    }
}