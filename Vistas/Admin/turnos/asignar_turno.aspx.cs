using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace Vistas.Admin.turnos
{
    public partial class asignar_turno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEspecialidades();
                CargarDias();
            }

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
        private void CargarEspecialidades()
        {
            Validar validar = new Validar(); // o el nombre real de tu clase de negocio
            DataTable dtSexos = validar.ObtenerEspecialidades();

            ddlSpeciality.DataSource = dtSexos;
            ddlSpeciality.DataTextField = "NOMBRE_ESP";
            ddlSpeciality.DataValueField = "NOMBRE_ESP";
            ddlSpeciality.DataBind();

            ddlSpeciality.Items.Insert(0, new ListItem("", ""));
        }
        private void CargarDias()
        {
            Validar validar = new Validar(); // o el nombre real de tu clase de negocio
            DataTable dtSexos = validar.ObtenerDias();

            ddlDay.DataSource = dtSexos;
            ddlDay.DataTextField = "DIA_SEMANA";
            ddlDay.DataValueField = "DIA_SEMANA";
            ddlDay.DataBind();

            ddlDay.Items.Insert(0, new ListItem("", ""));
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {

        }
        protected void btnConfirmarAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTime.Text) || string.IsNullOrEmpty(txtDoctor.Text) || string.IsNullOrEmpty(txtPatient.Text))
            {
                lblMensaje.Text = "Please, complete all the fields.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (ddlSpeciality.SelectedValue == "0" || string.IsNullOrEmpty(ddlSpeciality.SelectedValue) || ddlDay.SelectedValue == "0" || string.IsNullOrEmpty(ddlDay.SelectedValue))
            {
                lblMensaje.Text = "Please, complete all the fields.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;

            }

            txtTime.Text = "";
            txtDoctor.Text = "";
            ddlSpeciality.SelectedIndex = 0;
            txtPatient.Text = "";
            ddlDay.SelectedIndex = 0;
            
        }
    }
}