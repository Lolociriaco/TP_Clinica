using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;
using Negocio.Admin;
using Negocio.Shared;

namespace Vistas
{
	public partial class abmlPaciente : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["role"] == null || Session["role"].ToString() != "ADMIN")
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                CargarPacientes();
                cargarStates(ddlState, "Any state");
                cargarSexoGeneral(ddlSexo, "Any gender");
                username.Text = Session["username"].ToString();
            }

            this.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        // CARGA DE DDLS EN GRIDVIEW
        protected void gvPacientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gvPacientes.EditIndex == e.Row.RowIndex)
            {
                cargarProvinciasDDL(e);
            }
        }

        // CARGAR ESPECIALIDADES EN EL FILTRO
        private void cargarSexoGeneral(DropDownList ddl, string message)
        {
            AdminPatientsManager admin = new AdminPatientsManager();
            DataTable dtSexo = admin.ObtenerSexoPaciente();
            ddl.DataSource = dtSexo;
            ddl.DataTextField = "GENDER_PAT";
            ddl.DataValueField = "GENDER_PAT";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem(message, ""));
        }

        // CARGAR PROVINCIAS EN EL FILTRO
        private void cargarStates(DropDownList ddl, string message)
        {
            GetUbicationManager manager = new GetUbicationManager();

            DataTable dtProvincia = manager.ObtenerProvincia();
            ddl.DataSource = dtProvincia;
            ddl.DataTextField = "NAME_STATE";
            ddl.DataValueField = "ID_STATE";
            ddl.AutoPostBack = true;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem(message, ""));
        }

        // CARGAR DDL DE PROVINCIAS
        private void cargarProvinciasDDL(GridViewRowEventArgs e)
        {

            GetUbicationManager manager = new GetUbicationManager();

            // PROVINCIA
            DropDownList ddlProvincia = (DropDownList)e.Row.FindControl("ddlID_PROV_PAC");
                DropDownList ddlLocalidad = (DropDownList)e.Row.FindControl("ddlID_LOC_PAC");

                if (ddlProvincia != null)
                {

                    cargarStates(ddlProvincia, "< SELECT >");

                    int idProvinciaActual;

                    // Verificamos si venimos de un cambio (por ViewState)
                    if (ViewState["ProvinciaSeleccionadaGV"] != null)
                    {
                        idProvinciaActual = (int)ViewState["ProvinciaSeleccionadaGV"];
                    }
                    else
                    {
                        object provObj = DataBinder.Eval(e.Row.DataItem, "ID_STATE_PAT");
                        idProvinciaActual = provObj != null ? Convert.ToInt32(provObj) : 0;
                    }

                    if (ddlProvincia.Items.FindByValue(idProvinciaActual.ToString()) != null)
                        ddlProvincia.SelectedValue = idProvinciaActual.ToString();

                    if (ddlLocalidad != null)
                    {
                        DataTable dtLocalidades = manager.ObtenerLocalidadesFiltradas(idProvinciaActual);
                        ddlLocalidad.DataSource = dtLocalidades;
                        ddlLocalidad.DataTextField = "NAME_CITY";
                        ddlLocalidad.DataValueField = "ID_CITY";
                        ddlLocalidad.DataBind();
                        ddlLocalidad.Items.Insert(0, new ListItem("< SELECT >", ""));

                        object locObj = DataBinder.Eval(e.Row.DataItem, "ID_CITY_PAT");
                        string idLocActual = locObj != null ? locObj.ToString() : "";

                        if (ddlLocalidad.Items.FindByValue(idLocActual) != null)
                            ddlLocalidad.SelectedValue = idLocActual;
                    }
                }
        }

        // BAJA LOGICA
        private void bajaLogica(GridViewCommandEventArgs e)
        {
            if (e.CommandName != "DarDeBaja")
            {
                return;
            }

            GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int index = row.RowIndex;
            int idUsuario = Convert.ToInt32(gvPacientes.DataKeys[index].Value);


            AdminPatientsManager paciente = new AdminPatientsManager();
            paciente.deletePatient(idUsuario);

            lblMensaje.Text = "The patient was deleted succesfully.";
            lblMensaje.ForeColor = System.Drawing.Color.Red;

            CargarPacientes();

        }

        // FUNCIONALIDAD DE UNSUBSCRIBE
        protected void gvPacientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            bajaLogica(e);
        }

        // EDITAR
        protected void gvPacientes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            lblMensaje.Text = "";
            gvPacientes.EditIndex = e.NewEditIndex;
            CargarPacientes();
        }

        // CANCELAR
        protected void gvPacientes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            lblMensaje.Text = "";
            gvPacientes.EditIndex = -1;
            CargarPacientes();
        }

        // ACTUALIZAR
        protected void gvPacientes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            string dniPaciente = gvPacientes.DataKeys[e.RowIndex].Value.ToString();

            GridViewRow row = gvPacientes.Rows[e.RowIndex];

            string nuevoNombre = ((TextBox)row.FindControl("txtNOMBRE_PAC")).Text;
            string nuevoApellido = ((TextBox)row.FindControl("txtAPELLIDO_PAC")).Text;
            string nuevaDireccion = ((TextBox)row.FindControl("txtDIRECCION_PAC")).Text;
            string nuevoCorreo = ((TextBox)row.FindControl("txtCORREO_PAC")).Text;
            string nuevoTelefono = ((TextBox)row.FindControl("txtTELEFONO_PAC")).Text;
            string nuevaNacionalidad = ((TextBox)row.FindControl("txtNACIONALIDAD_PAC")).Text;

            TextBox txtFechaNac = (TextBox)row.FindControl("txtFECHANAC_PAC");

            if (txtFechaNac == null)
            {
                lblMensaje.Text = "Birth date input not found.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                e.Cancel = true;
                return;
            }

            DateTime nuevaFechaNac;
            if (!DateTime.TryParse(txtFechaNac.Text, out nuevaFechaNac))
            {
                lblMensaje.Text = "Enter a valid birth date.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                e.Cancel = true;
                return;
            }

            int edad = DateTime.Today.Year - nuevaFechaNac.Year;
            if (nuevaFechaNac > DateTime.Today.AddYears(-edad)) edad--;

            if (edad < 18)
            {
                lblMensaje.Text = "The doctor must be at least 18 years old.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                e.Cancel = true;
                return;
            }

            string nuevoSexo = ((DropDownList)row.FindControl("ddlSEXO_PAC")).SelectedValue;

            DropDownList ddlLoc = (DropDownList)row.FindControl("ddlID_LOC_PAC");
            DropDownList ddlProv = (DropDownList)row.FindControl("ddlID_PROV_PAC");

            if (ddlLoc == null || ddlLoc.SelectedValue == "0" || string.IsNullOrEmpty(ddlLoc.SelectedValue) ||
                ddlProv == null || ddlProv.SelectedValue == "0" || string.IsNullOrEmpty(ddlProv.SelectedValue))
            {
                lblMensaje.Text = "Select a valid city and/or locality.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            int nuevaLocalidad = Convert.ToInt32(ddlLoc.SelectedValue);

            int nuevaProvincia = Convert.ToInt32(ddlProv.SelectedValue);

            AdminPatientsManager adminPaciente = new AdminPatientsManager();

            Paciente paciente = new Paciente
            (
                int.Parse(dniPaciente),
                nuevoNombre,
                nuevoApellido,
                nuevoSexo,
                nuevaNacionalidad,
                nuevaFechaNac,
                nuevaDireccion,
                nuevaLocalidad.ToString(),
                nuevaProvincia.ToString(),
                nuevoCorreo,
                nuevoTelefono
            );

            adminPaciente.updatePatient(paciente);

            lblMensaje.Text = "The patient was modified succesfully.";
            lblMensaje.ForeColor = System.Drawing.Color.Green;

            gvPacientes.EditIndex = -1;
            CargarPacientes();
        }

        // CARGA DE PACIENTES EN GRID
        private void CargarPacientes()
        {
            string state = ddlState.SelectedValue;
            string sexo = ddlSexo.SelectedValue;
            string name = txtName.Text.Trim();
            string dni = txtDNI.Text.Trim();

            AdminPatientsManager adminPatients = new AdminPatientsManager();
            gvPacientes.DataSource = adminPatients.ObtenerPacientesFiltrados(state, name, dni, sexo);
            gvPacientes.DataBind();
        }

        // ACTUALIZACION DE DDL AL SELECCIONAR UNA PROVINCIA NUEVA
        protected void ddlID_PROV_PAC_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            DropDownList ddlProvincia = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlProvincia.NamingContainer;

            int index = row.RowIndex;
            int nuevaProvincia;

            if (int.TryParse(ddlProvincia.SelectedValue, out nuevaProvincia))
            {
                ViewState["ProvinciaSeleccionadaGV"] = nuevaProvincia;

                gvPacientes.EditIndex = index;
                CargarPacientes(); 
            }
            else
            {
                lblMensaje.Text = "You must select a valid city.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        // VOLVER A LOGIN
        protected void btnConfirmarLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }

        // ACTUALIZACION DE GRID SEGUN FILTRO DE PROVINCIA
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            CargarPacientes();
        }

        // ACTUALIZACION DE GRID SEGUN FILTRO DE SEXO
        protected void ddlSexo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            CargarPacientes();
        }

        // ACTUALIZACION DE GRID SEGUN FILTRO DE DNI
        protected void txtDNI_TextChanged(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            CargarPacientes();
        }

        // ACTUALIZACION DE GRID SEGUN FILTRO DE NOMBRE
        protected void txtName_TextChanged(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            CargarPacientes();
        }

        // LIMPIEZA DE FILTRO
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtDNI.Text = "";
            txtName.Text = "";
            ddlSexo.SelectedIndex = 0;
            ddlState.SelectedIndex = 0;
            CargarPacientes();
        }

        // CAMBIO DE PAGINA EN GRID
        protected void gvPacientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPacientes.PageIndex = e.NewPageIndex;
            CargarPacientes();
        }
    }
}