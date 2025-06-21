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
            if (Session["role"] == null || Session["role"].ToString() != "Admin")
            {
                Response.Redirect("~/Login.aspx");
            }

            username.Text = Session["username"].ToString();

            if (!IsPostBack)
            {
            }
                lblEstado.Text = "";
        }
        protected void btnConfirmarLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            // Validar que el usuario original esté ingresado
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                lblEstado.Text = "Ingrese su usuario actual.";
                return;
            }

            // Validar que al menos uno de los campos a cambiar esté completo
            bool cambiarPass = !string.IsNullOrWhiteSpace(txtPass.Text);
            bool cambiarUsuario = !string.IsNullOrWhiteSpace(txtNuevoUsuario.Text);

            if (!cambiarPass && !cambiarUsuario)
            {
                lblEstado.Text = "Debe ingresar una nueva contraseña o nuevo usuario.";
                return;
            }

            // Si quiere cambiar contraseña, validar que repita la misma
            if (cambiarPass)
            {
                if (string.IsNullOrWhiteSpace(txtRepPassword.Text))
                {
                    lblEstado.Text = "Confirme su nueva contraseña.";
                    return;
                }

                if (txtPass.Text != txtRepPassword.Text)
                {
                    lblEstado.Text = "Las contraseñas no coinciden.";
                    return;
                }
            }

            // Validar que el usuario actual exista
            Validar validar = new Validar();
            bool userValido = validar.ValidarCambioUsuario(txtUsuario.Text);
            if (!userValido)
            {
                lblEstado.Text = "El usuario actual no existe.";
                return;
            }

            // Realizar el cambio
            UserManager usuario = new UserManager();
            bool cambioUsuario = usuario.modificarUsuario(
                txtUsuario.Text,
                cambiarPass ? txtPass.Text : null,
                cambiarUsuario ? txtNuevoUsuario.Text : null
            );

            if (!cambioUsuario)
            {
                lblEstado.Text = "Ocurrió un error al actualizar los datos.";
                return;
            }

            // Éxito
            lblEstado.Text = "Usuario modificado correctamente.";

            // Limpiar campos
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