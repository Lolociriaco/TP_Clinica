using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Negocio.Doctor;

namespace Vistas.Medicos
{
    public enum AppointmentStatus
    {
        PENDING,
        PRESENT,
        ABSERNT,
        RESCHEDULED,
        CANCELED
    }

    public partial class turnos : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                CargarTurnos();
            }

            if (Session["role"] == null || Session["role"].ToString() != "DOCTOR")
            {
                Response.Redirect("~/Login.aspx");
            }

            username.Text = Session["username"].ToString();
        }
        private void CargarTurnos()
        {
            string DNI_PAT = txtDNI.Text.Trim();
            string DAY_APPO = txtDay.Text.Trim();

            Validar validar = new Validar();
            gvTurnos.DataSource = validar.ObtenerTurnos(DNI_PAT, DAY_APPO);
            gvTurnos.DataBind();
        }

        protected void btnConfirmarLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx"); 
        }

        // LOGICA EDICION Y BORRADO LOGICO GRID
        protected void gvTurnos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gvTurnos.EditIndex == e.Row.RowIndex)
            {
                e.Row.CssClass = "edit-row";

                Validar validar = new Validar();

                // PROVINCIA
                DropDownList ddlState = (DropDownList)e.Row.FindControl("ddlSTATE_APPO");

                if (ddlState == null) return;
                
                DataTable dtProvincia = validar.ObtenerProvincia();
                var statuses = Enum.GetNames(typeof(AppointmentStatus))
                 .Select(s => new { Value = s, Text = s });

                ddlState.DataSource = statuses;
                ddlState.DataTextField = "Text";
                ddlState.DataValueField = "Value";
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("<Select status>", ""));

            }
        }

        // EDITAR
        protected void gvTurnos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            lblMensaje.Text = "";
            gvTurnos.EditIndex = e.NewEditIndex;
            CargarTurnos();
        }

        // CANCELAR
        protected void gvTurnos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            lblMensaje.Text = "";
            gvTurnos.EditIndex = -1;
            CargarTurnos();
        }

        // ACTUALIZAR
        protected void gvTurnos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int idAppo = Convert.ToInt32(gvTurnos.DataKeys[e.RowIndex].Value);

            GridViewRow row = gvTurnos.Rows[e.RowIndex];

            string newObservation = ((TextBox)row.FindControl("txtOBSERVATION_APPO")).Text;
            DropDownList ddlState = ((DropDownList)row.FindControl("ddlSTATE_APPO"));

            if (ddlState == null || ddlState.SelectedValue == "0" || string.IsNullOrEmpty(ddlState.SelectedValue))
            {
                lblMensaje.Text = "Select a valid state.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string newState = ddlState.SelectedValue.ToString();

            DoctorAppointmentsManager appoManager = new DoctorAppointmentsManager();
            appoManager.updateAppointment(newState, newObservation, idAppo);

            lblMensaje.Text = "The patient was modified succesfully.";
            lblMensaje.ForeColor = System.Drawing.Color.Green;

            gvTurnos.EditIndex = -1;
            CargarTurnos();
        }

        protected void gvTurnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTurnos.PageIndex = e.NewPageIndex;
            CargarTurnos();
        }

        protected void txtDay_TextChanged(object sender, EventArgs e)
        {
            CargarTurnos();
        }

        protected void txtDNI_TextChanged(object sender, EventArgs e)
        {
            CargarTurnos();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtDNI.Text = "";
            txtDay.Text = "";
            CargarTurnos();
        }
    }
}