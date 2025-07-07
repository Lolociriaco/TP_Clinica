using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Negocio.Shared;

namespace Vistas.Medicos
{
	public partial class editar_usuario : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["role"] == null || Session["role"].ToString() != "DOCTOR")
            {
                Response.Redirect("~/Login.aspx");
            }


            if (!IsPostBack)
            {
                username.Text = Session["username"].ToString();
                lblEstado.Text = "";
            }
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
            // VALIDACIONES
            if (!validarCambioContraseña()) return;

            // CAMBIAR USUARIO
            UserManager usuario = new UserManager();
            bool cambiarPass = !string.IsNullOrWhiteSpace(txtPass.Text);
            bool cambiarUsuario = !string.IsNullOrWhiteSpace(txtNuevoUsuario.Text);
            string user = Session["username"].ToString();


            bool cambioUsuario = usuario.modificarUsuario(
                user,
                cambiarPass ? txtPass.Text : null,
                cambiarUsuario ? txtNuevoUsuario.Text : null
            );

            lblEstado.ForeColor = System.Drawing.Color.Green;
            lblEstado.Text = "User modified successfully.";

            if (cambioUsuario)
            {
                Session["username"] = txtNuevoUsuario.Text;
                username.Text = txtNuevoUsuario.Text; // Actualizar el campo de usuario en la página
            }

            // VACIAR CONTROLES
            txtNuevoUsuario.Text = "";
            txtPass.Text = "";
            txtRepPassword.Text = "";
        }


        // VALIDAR EL CAMBIO DE LA CONTRASEÑA
        private bool validarCambioContraseña()
        {
            bool cambiarPass = !string.IsNullOrWhiteSpace(txtPass.Text);
            bool cambiarUsuario = !string.IsNullOrWhiteSpace(txtNuevoUsuario.Text);

            if (!cambiarPass && !cambiarUsuario)
            {
                lblEstado.ForeColor = System.Drawing.Color.Red;
                lblEstado.Text = "You must add a new password or user.";
                return false;
            }

            if (cambiarPass)
            {
                if (string.IsNullOrWhiteSpace(txtRepPassword.Text))
                {
                    lblEstado.ForeColor = System.Drawing.Color.Red;
                    lblEstado.Text = "Confirm your password.";
                    return false;
                }

                if (txtPass.Text != txtRepPassword.Text)
                {
                    lblEstado.ForeColor = System.Drawing.Color.Red;
                    lblEstado.Text = "Passwords dont match.";
                    return false;
                }
            }
            return true;
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {

        }
    }
}