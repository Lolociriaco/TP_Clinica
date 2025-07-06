using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
        ABSENT,
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
                CargarEstadosEnDropDown(ddlState, "< Select a status >");
            }

            if (Session["role"] == null || Session["role"].ToString() != "DOCTOR")
            {
                Response.Redirect("~/Login.aspx");
            }

            username.Text = Session["username"].ToString();
        }

        // CARGAR TURNOS
        private void CargarTurnos()
        {
            string DNI_PAT = txtDNI.Text.Trim();
            string DAY_APPO = txtDay.Text.Trim();

            string todayOrTomorrow = chckToday.Checked ? "TODAY" :
                           chckTomorrow.Checked ? "TOMORROW" : "";

            string state = ddlState.SelectedValue;

            DoctorAppoManager doctorManager = new DoctorAppoManager();
            gvTurnos.DataSource = doctorManager.ObtenerTurnos(DNI_PAT, DAY_APPO, todayOrTomorrow, state);
            gvTurnos.DataBind();
        }

        protected void btnConfirmarLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx"); 
        }

        // LOGICA EDICION GRID
        protected void gvTurnos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gvTurnos.EditIndex == e.Row.RowIndex)
            {
                e.Row.CssClass = "edit-row";

                // CARGAR DDL DE STATUS EN LA GRID
                DropDownList ddlState = (DropDownList)e.Row.FindControl("ddlSTATE_APPO");
                if (ddlState != null)
                {
                    CargarEstadosEnDropDown(ddlState, "< Select a status >");
                }

            }
        }

        // CARGAR ESTADOS EN LAS DDL
        private void CargarEstadosEnDropDown(DropDownList ddl, string primerItem)
        {
            var estados = Enum.GetNames(typeof(AppointmentStatus))
                .Select(s => new { Value = s, Text = s });

            ddl.DataSource = estados;
            ddl.DataTextField = "Text";
            ddl.DataValueField = "Value";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem(primerItem, ""));
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

            if (!validarCamposVacios(e)) return;

            string newState = ddlState.SelectedValue.ToString();

            DoctorAppoManager appoManager = new DoctorAppoManager();
            appoManager.updateAppointment(newState, newObservation, idAppo);

            lblMensaje.Text = "The patient was modified succesfully.";
            lblMensaje.ForeColor = System.Drawing.Color.Green;

            gvTurnos.EditIndex = -1;
            CargarTurnos();
        }

        private bool validarCamposVacios (GridViewUpdateEventArgs e)
        {
            if (ddlState == null || ddlState.SelectedValue == "0" || string.IsNullOrEmpty(ddlState.SelectedValue))
            {
                lblMensaje.Text = "Select a valid state.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return false;
            }
            return true;
        }

        // CAMBIO DE PAGINA EN LA GRID
        protected void gvTurnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTurnos.PageIndex = e.NewPageIndex;
            CargarTurnos();
        }

        // CAMBIO DE TEXTO EN TEXTBOX DE DIA
        protected void txtDay_TextChanged(object sender, EventArgs e)
        {
            if(!validarFecha()) return;

            chckToday.Checked = false;
            chckTomorrow.Checked = false;
            CargarTurnos();
        }

        // VALIDAR FECHA
        private bool validarFecha()
        {
            if (!DateTime.TryParse(txtDay.Text, out DateTime fecha))
            {
                lblMensaje.Text = "Enter a valid full date.";
                lblMensaje.ForeColor = Color.Red;
                return false;
            }
            return true;
        }

        // CAMBIO DE TEXTO EN TEXTBOX DE DNI
        protected void txtDNI_TextChanged(object sender, EventArgs e)
        {
            CargarTurnos();
        }

        // BORRAR FILTROS
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtDNI.Text = "";
            txtDay.Text = "";
            chckToday.Checked = false;
            chckTomorrow.Checked = false;
            ddlState.SelectedIndex = 0;
            CargarTurnos();
        }

        // CAMBIO DE CHECKBOX HOY
        protected void chckToday_CheckedChanged(object sender, EventArgs e)
        {
            if (chckToday.Checked)
            {
                chckTomorrow.Checked = false;
                txtDay.Text = "";
            }
            CargarTurnos();
        }

        // CAMBIO DE CHECKBOX MAÑANA
        protected void chckTomorrow_CheckedChanged(object sender, EventArgs e)
        {
            if(chckTomorrow.Checked)
            {
                chckToday.Checked = false;
                txtDay.Text = "";
            }
            CargarTurnos();
        }

        // CAMBIO DE INDICE EN LA DDL DE ESTADO
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            CargarTurnos();
        }
    }
}