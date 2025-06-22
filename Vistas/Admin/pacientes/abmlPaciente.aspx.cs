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

            if (Session["role"] == null || Session["role"].ToString() != "Admin")
            {
                Response.Redirect("~/Login.aspx");
            }

            username.Text = Session["username"].ToString();
        }
        protected void gvPacientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gvPacientes.EditIndex == e.Row.RowIndex)
            {
                Validar validar = new Validar(); 

                // LOCALIDAD 
                DropDownList ddlLocalidad = (DropDownList)e.Row.FindControl("ddlID_LOC_PAC");
                if (ddlLocalidad != null)
                {
                    DataTable dtLocalidad = validar.ObtenerLocalidad();
                    ddlLocalidad.DataSource = dtLocalidad;
                    ddlLocalidad.DataTextField = "NOMBRE_LOC";
                    ddlLocalidad.DataValueField = "ID_LOC";
                    ddlLocalidad.DataBind();
                    ddlLocalidad.Items.Insert(0, new ListItem("< Seleccione >", ""));

                    object locObj = DataBinder.Eval(e.Row.DataItem, "ID_LOC_PAC");
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
                DropDownList ddlProvincia = (DropDownList)e.Row.FindControl("ddlID_PROV_PAC");
                if (ddlProvincia != null)
                {
                    DataTable dtProvincia = validar.ObtenerProvincia();
                    ddlProvincia.DataSource = dtProvincia;
                    ddlProvincia.DataTextField = "NOMBRE_PROV";
                    ddlProvincia.DataValueField = "ID_PROV";
                    ddlProvincia.DataBind();

                    ddlProvincia.Items.Insert(0, new ListItem("< Seleccione >", ""));

                    object provObj = DataBinder.Eval(e.Row.DataItem, "ID_PROV_PAC");
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
            }
        }

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

            string fechaNacString = ((TextBox)row.FindControl("txtFECHANAC_PAC")).Text;
            DateTime nuevaFechaNac;
            if (!DateTime.TryParse(fechaNacString, out nuevaFechaNac))
            {
                // Manejar error!!!
                //lblMensaje.Text = "La fecha de nacimiento ingresada no es válida.";
                return;
            }

            string nuevoSexo = ((DropDownList)row.FindControl("ddlSEXO_PAC")).SelectedValue;

            int nuevaLocalidad = Convert.ToInt32(((DropDownList)row.FindControl("ddlID_LOC_PAC")).SelectedValue);

            int nuevaProvincia = Convert.ToInt32(((DropDownList)row.FindControl("ddlID_PROV_PAC")).SelectedValue);



            UserManager medico = new UserManager();
            medico.updatePatient(nuevoNombre, nuevoApellido, dniPaciente, 
                                nuevaDireccion, nuevoCorreo, nuevoTelefono, nuevaFechaNac,
                                nuevoSexo, nuevaLocalidad, nuevaProvincia);

            gvPacientes.EditIndex = -1;
            CargarPacientes();
        }

        private void CargarPacientes()
        {
            Validar validar = new Validar();
            gvPacientes.DataSource = validar.ObtenerPacientes();
            gvPacientes.DataBind();
        }

        protected void btnConfirmarLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }

        protected void gvPacientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
    }
}