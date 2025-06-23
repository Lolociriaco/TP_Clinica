using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace Vistas.Admin.medicos
{
	public partial class editar_usuario : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["role"] == null || Session["role"].ToString() != "ADMINISTRADOR")
            {
                Response.Redirect("~/Login.aspx");
            }

            username.Text = Session["username"].ToString();

            if (!IsPostBack)
            {
            }
                lblEstado.Text = "";
        }

        // VOLVER A LOGIN
        protected void btnConfirmarLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }

        // AGREGAR USUARIO NUEVO
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                lblEstado.ForeColor = System.Drawing.Color.Red;
                lblEstado.Text = "Enter the current user.";
                return;
            }

            bool cambiarPass = !string.IsNullOrWhiteSpace(txtPass.Text);
            bool cambiarUsuario = !string.IsNullOrWhiteSpace(txtNuevoUsuario.Text);

            if (!cambiarPass && !cambiarUsuario)
            {
                lblEstado.ForeColor = System.Drawing.Color.Red;
                lblEstado.Text = "You must add a new password or user.";
                return;
            }

            if (cambiarPass)
            {
                if (string.IsNullOrWhiteSpace(txtRepPassword.Text))
                {
                    lblEstado.ForeColor = System.Drawing.Color.Red;
                    lblEstado.Text = "Confirm your password.";
                    return;
                }

                if (txtPass.Text != txtRepPassword.Text)
                {
                    lblEstado.ForeColor = System.Drawing.Color.Red;
                    lblEstado.Text = "Passwords dont match.";
                    return;
                }
            }

            Validar validar = new Validar();
            bool userValido = validar.ValidarCambioUsuario(txtUsuario.Text);
            if (!userValido)
            {
                lblEstado.ForeColor = System.Drawing.Color.Red;
                lblEstado.Text = "This user doesn´t exist.";
                return;
            }

            UserManager usuario = new UserManager();
            bool cambioUsuario = usuario.modificarUsuario(
                txtUsuario.Text,
                cambiarPass ? txtPass.Text : null,
                cambiarUsuario ? txtNuevoUsuario.Text : null
            );

            if (!cambioUsuario)
            {
                lblEstado.ForeColor = System.Drawing.Color.Red;
                lblEstado.Text = "An error ocurred at updating the data.";
                return;
            }

            lblEstado.ForeColor = System.Drawing.Color.Green;
            lblEstado.Text = "User modified successfully.";

            txtUsuario.Text = "";
            txtNuevoUsuario.Text = "";
            txtPass.Text = "";
            txtRepPassword.Text = "";
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {

        }
    }
}