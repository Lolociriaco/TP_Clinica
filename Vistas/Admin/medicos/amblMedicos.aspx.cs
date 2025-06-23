using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace Vistas
{
	public partial class amblMedicos : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMedicos();
            }

            if (Session["role"] == null || Session["role"].ToString() != "Admin")
            {
                Response.Redirect("~/Login.aspx");
            }
            

                username.Text = Session["username"].ToString();
        }

        // CARGA DE DDLS
        protected void gvMedicos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gvMedicos.EditIndex == e.Row.RowIndex)
            {
                e.Row.CssClass = "edit-row";

                Validar validar = new Validar();

                // LOCALIDAD 
                DropDownList ddlLocalidad = (DropDownList)e.Row.FindControl("ddlID_LOC_MED");
                if (ddlLocalidad != null)
                {
                    DataTable dtLocalidad = validar.ObtenerLocalidad();
                    ddlLocalidad.DataSource = dtLocalidad;
                    ddlLocalidad.DataTextField = "NOMBRE_LOC";
                    ddlLocalidad.DataValueField = "ID_LOC";
                    ddlLocalidad.DataBind();
                    ddlLocalidad.Items.Insert(0, new ListItem("< Seleccione >", ""));

                    object locObj = DataBinder.Eval(e.Row.DataItem, "ID_LOC_MED");
                    if (locObj != null)
                    {
                        string localidadActual = locObj.ToString();
                        if (ddlLocalidad.Items.FindByValue(localidadActual) != null)
                        {
                            if (ddlLocalidad.Items.FindByValue(localidadActual) != null)
                            {
                                ddlLocalidad.SelectedValue = localidadActual;
                            }
                        }
                    }
                }

                // PROVINCIA 
                DropDownList ddlProvincia = (DropDownList)e.Row.FindControl("ddlID_PROV_MED");
                if (ddlProvincia != null)
                {
                    DataTable dtProvincia = validar.ObtenerProvincia();
                    ddlProvincia.DataSource = dtProvincia;
                    ddlProvincia.DataTextField = "NOMBRE_PROV";
                    ddlProvincia.DataValueField = "ID_PROV";
                    ddlProvincia.DataBind();

                    // Agregar opción por defecto
                    ddlProvincia.Items.Insert(0, new ListItem("< Seleccione >", ""));

                    object provObj = DataBinder.Eval(e.Row.DataItem, "ID_PROV_MED");
                    if (provObj != null)
                    {
                        string provinciaActual = provObj.ToString();

                        ListItem item = ddlProvincia.Items.FindByValue(provinciaActual);
                        if (item != null)
                        {
                            if (ddlProvincia.Items.FindByValue(provinciaActual) != null)
                            {
                                ddlProvincia.SelectedValue = provinciaActual;
                            }
                        }
                    }
                }

                // ESPECIALIDAD
                DropDownList ddlEspecialidad = (DropDownList)e.Row.FindControl("ddlID_ESP");
                if (ddlEspecialidad != null)
                {
                    DataTable dtEspecialidad = validar.ObtenerEspecialidades();
                    ddlEspecialidad.DataSource = dtEspecialidad;
                    ddlEspecialidad.DataTextField = "NOMBRE_ESP";
                    ddlEspecialidad.DataValueField = "ID_ESP";
                    ddlEspecialidad.DataBind();

                    ddlEspecialidad.Items.Insert(0, new ListItem("< Seleccione >", ""));

                    object espObj = DataBinder.Eval(e.Row.DataItem, "ID_ESP_MED");
                    if (espObj != null)
                    {
                        string especialidadctual = espObj.ToString();

                        ListItem item = ddlEspecialidad.Items.FindByValue(especialidadctual);
                        if (item != null)
                        {
                            if (ddlEspecialidad.Items.FindByValue(especialidadctual) != null)
                            {
                                ddlEspecialidad.SelectedValue = especialidadctual;
                            }
                        }
                    }
                }

            }
        }

        // FUNCIONALIDAD DE UNSUBSCRIBE
        protected void gvMedicos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DarDeBaja")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvMedicos.Rows[index];
                int idUsuario = Convert.ToInt32(gvMedicos.DataKeys[index].Value);

                UserManager medico = new UserManager();
                medico.deleteDoctor(idUsuario);

                CargarMedicos();
            }
        }

        // EDITAR
        protected void gvMedicos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMedicos.EditIndex = e.NewEditIndex;
            CargarMedicos();
        }

        // CANCELAR
        protected void gvMedicos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMedicos.EditIndex = -1;
            CargarMedicos();
        }

        // ACTUALIZAR
        protected void gvMedicos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int idUsuario = Convert.ToInt32(gvMedicos.DataKeys[e.RowIndex].Value);

            GridViewRow row = gvMedicos.Rows[e.RowIndex];

            string nuevoNombre = ((TextBox)row.FindControl("txtNOMBRE_MED")).Text;
            string nuevoApellido = ((TextBox)row.FindControl("txtAPELLIDO_MED")).Text;
            string nuevoDni = ((TextBox)row.FindControl("txtDNI_MED")).Text;
            string nuevaDireccion = ((TextBox)row.FindControl("txtDIRECCION_MED")).Text;
            string nuevoCorreo = ((TextBox)row.FindControl("txtCORREO_MED")).Text;
            string nuevoTelefono = ((TextBox)row.FindControl("txtTELEFONO_MED")).Text;
            string nuevoDiasHorario = ((TextBox)row.FindControl("txtDIAS_HORARIO_MED")).Text;

            string fechaNacString = ((TextBox)row.FindControl("txtFECHANAC_MED")).Text;
            DateTime nuevaFechaNac;
            if (!DateTime.TryParse(fechaNacString, out nuevaFechaNac))
            {
                // Manejar error!!!
                //lblMensaje.Text = "La fecha de nacimiento ingresada no es válida.";
                return;
            }

            string nuevoSexo = ((DropDownList)row.FindControl("ddlSEXO_MED")).SelectedValue;

            DropDownList ddlEsp = (DropDownList)row.FindControl("ddlID_ESP");
            DropDownList ddlLoc = (DropDownList)row.FindControl("ddlID_LOC_MED");
            DropDownList ddlProv = (DropDownList)row.FindControl("ddlID_PROV_MED");

            if (ddlEsp == null || ddlEsp.SelectedValue == "0" || string.IsNullOrEmpty(ddlEsp.SelectedValue) ||
                ddlLoc == null || ddlLoc.SelectedValue == "0" || string.IsNullOrEmpty(ddlLoc.SelectedValue) ||
                ddlProv == null || ddlProv.SelectedValue == "0" || string.IsNullOrEmpty(ddlProv.SelectedValue))
            {
                lblMensaje.Text = "Select a valid city, locality and speciality.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            int nuevaEspecialidad = Convert.ToInt32(ddlEsp.SelectedValue);
            int nuevaLocalidad = Convert.ToInt32(ddlLoc.SelectedValue);
            int nuevaProvincia = Convert.ToInt32(ddlProv.SelectedValue);

            UserManager medico = new UserManager();
            medico.updateDoctor(idUsuario, nuevoNombre, nuevoApellido, nuevoDni,
                                nuevaDireccion, nuevoCorreo, nuevoTelefono, nuevaEspecialidad, nuevaFechaNac,
                                nuevoSexo, nuevaLocalidad, nuevaProvincia, nuevoDiasHorario);

            gvMedicos.EditIndex = -1;
            CargarMedicos();
        }

        // CARGA DE GRIDVIEW
        private void CargarMedicos()
        {
            Validar validar = new Validar();
            gvMedicos.DataSource = validar.ObtenerMedicos();
            gvMedicos.DataBind();
        }

        // VOLVER A LOGIN
        protected void btnConfirmarLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }

        // CAMBIO DE PAGINA DE GRIDVIEW
        protected void gvMedicos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMedicos.PageIndex = e.NewPageIndex;
            CargarMedicos();
        }
    }
}