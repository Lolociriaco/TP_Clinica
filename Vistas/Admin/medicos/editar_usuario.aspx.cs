using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Negocio.Shared;

namespace Vistas.Admin.medicos
{
	public partial class editar_usuario : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["role"] == null || Session["role"].ToString() != "ADMIN")
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
            if (!validarCamposVacios()) return;

            if (!validarCambioContraseña()) return;

            if (!validarUsuarioInexistente()) return;

            // CAMBIAR USUARIO
            UserManager usuario = new UserManager();
            bool cambiarPass = !string.IsNullOrWhiteSpace(txtPass.Text);
            bool cambiarUsuario = !string.IsNullOrWhiteSpace(txtNuevoUsuario.Text);

            bool cambioUsuario = usuario.modificarUsuario(
                txtUsuario.Text,
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
            txtUsuario.Text = "";
            txtNuevoUsuario.Text = "";
            txtPass.Text = "";
            txtRepPassword.Text = "";
        }

        // VALIDAR CAMPOS VACIOS
        private bool validarCamposVacios()
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                lblEstado.ForeColor = System.Drawing.Color.Red;
                lblEstado.Text = "Enter the current user.";
                return false;
            }
            return true;
        }

        // VALIDAR QUE EL USER EXISTA
        private bool validarUsuarioInexistente()
        {
            AuthManager auth = new AuthManager();
            bool userValido = auth.UserExist(txtUsuario.Text);

            if (!userValido)
            {
                lblEstado.ForeColor = System.Drawing.Color.Red;
                lblEstado.Text = "This user doesn´t exist.";
                return false;
            }
            return true;
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