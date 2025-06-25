using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace Vistas
{
	public partial class abmlPaciente : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPacientes();
            }

            if (Session["role"] == null || Session["role"].ToString() != "ADMINISTRADOR")
            {
                Response.Redirect("~/Login.aspx");
            }

            username.Text = Session["username"].ToString();
        }

        // CARGA DE DDLS EN GRIDVIEW
        protected void gvPacientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gvPacientes.EditIndex == e.Row.RowIndex)
            {
                Validar validar = new Validar();

                // PROVINCIA
                DropDownList ddlProvincia = (DropDownList)e.Row.FindControl("ddlID_PROV_PAC");
                DropDownList ddlLocalidad = (DropDownList)e.Row.FindControl("ddlID_LOC_PAC");

                if (ddlProvincia != null)
                {
                    DataTable dtProvincia = validar.ObtenerProvincia();
                    ddlProvincia.DataSource = dtProvincia;
                    ddlProvincia.DataTextField = "NOMBRE_PROV";
                    ddlProvincia.DataValueField = "ID_PROV";
                    ddlProvincia.AutoPostBack = true;
                    ddlProvincia.DataBind();
                    ddlProvincia.Items.Insert(0, new ListItem("< SELECT >", ""));

                    int idProvinciaActual;

                    // Verificamos si venimos de un cambio (por ViewState)
                    if (ViewState["ProvinciaSeleccionadaGV"] != null)
                    {
                        idProvinciaActual = (int)ViewState["ProvinciaSeleccionadaGV"];
                    }
                    else
                    {
                        object provObj = DataBinder.Eval(e.Row.DataItem, "ID_PROV_PAC");
                        idProvinciaActual = provObj != null ? Convert.ToInt32(provObj) : 0;
                    }

                    if (ddlProvincia.Items.FindByValue(idProvinciaActual.ToString()) != null)
                        ddlProvincia.SelectedValue = idProvinciaActual.ToString();

                    if (ddlLocalidad != null)
                    {
                        DataTable dtLocalidades = validar.ObtenerLocalidadesFiltradas(idProvinciaActual);
                        ddlLocalidad.DataSource = dtLocalidades;
                        ddlLocalidad.DataTextField = "NOMBRE_LOC";
                        ddlLocalidad.DataValueField = "ID_LOC";
                        ddlLocalidad.DataBind();
                        ddlLocalidad.Items.Insert(0, new ListItem("< SELECT >", ""));

                        // Seleccionar localidad actual
                        object locObj = DataBinder.Eval(e.Row.DataItem, "ID_LOC_PAC");
                        string idLocActual = locObj != null ? locObj.ToString() : "";

                        if (ddlLocalidad.Items.FindByValue(idLocActual) != null)
                            ddlLocalidad.SelectedValue = idLocActual;
                    }
                }
            }
        }

        // FUNCIONALIDAD DE UNSUBSCRIBE
        protected void gvPacientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DarDeBaja")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvPacientes.Rows[index];
                int idUsuario = Convert.ToInt32(gvPacientes.DataKeys[index].Value);

                UserManager paciente = new UserManager();
                paciente.deletePatient(idUsuario);

                CargarPacientes();
            }
        }

        // EDITAR
        protected void gvPacientes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPacientes.EditIndex = e.NewEditIndex;
            CargarPacientes();
        }

        // CANCELAR
        protected void gvPacientes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
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

            string fechaNacString = ((TextBox)row.FindControl("txtFECHANAC_PAC")).Text;

            DateTime nuevaFechaNac;
            if (!DateTime.TryParse(fechaNacString, out nuevaFechaNac))
            {
                // Manejar error!!!
                //lblMensaje.Text = "La fecha de nacimiento ingresada no es válida.";
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

            UserManager medico = new UserManager();
            medico.updatePatient(nuevoNombre, nuevoApellido, dniPaciente, 
                                nuevaDireccion, nuevoCorreo, nuevoTelefono, nuevaNacionalidad, nuevaFechaNac,
                                nuevoSexo, nuevaLocalidad, nuevaProvincia);

            gvPacientes.EditIndex = -1;
            CargarPacientes();
        }

        // CARGA DE PACIENTES EN GRID
        private void CargarPacientes()
        {
            Validar validar = new Validar();
            gvPacientes.DataSource = validar.ObtenerPacientes();
            gvPacientes.DataBind();
        }
        protected void ddlID_PROV_PAC_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlProvincia = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlProvincia.NamingContainer;

            int index = row.RowIndex;
            int nuevaProvincia;

            if (int.TryParse(ddlProvincia.SelectedValue, out nuevaProvincia))
            {
                ViewState["ProvinciaSeleccionadaGV"] = nuevaProvincia;

                gvPacientes.EditIndex = index;
                CargarPacientes(); // Vuelve a cargar la grilla con la provincia seleccionada
            }
            else
            {
                lblMensaje.Text = "You must select a valid city.";
            }
        }

        // VOLVER A LOGIN
        protected void btnConfirmarLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }

        // CAMBIO DE PAGINA EN GRID
        protected void gvPacientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPacientes.PageIndex = e.NewPageIndex;
            CargarPacientes();
        }
    }
}