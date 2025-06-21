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

        protected void gvMedicos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gvMedicos.EditIndex == e.Row.RowIndex)
            {
                DropDownList ddlLocalidad = (DropDownList)e.Row.FindControl("ddlID_LOC_MED");
                if (ddlLocalidad != null)
                {
                    Validar validar = new Validar();
                    DataTable dtLocalidad = validar.ObtenerLocalidad();

                    ddlLocalidad.DataSource = dtLocalidad;
                    ddlLocalidad.DataTextField = "NOMBRE_LOC"; // nombre a mostrar
                    ddlLocalidad.DataValueField = "ID_LOC";    // valor (clave primaria)
                    ddlLocalidad.DataBind();

                    // Seleccionar el valor actual de localidad
                    string localidadActual = DataBinder.Eval(e.Row.DataItem, "ID_LOC_MED").ToString();

                    // Verificá que el valor existe antes de asignarlo
                    if (ddlLocalidad.Items.FindByValue(localidadActual) != null)
                    {
                        ddlLocalidad.SelectedValue = localidadActual;
                    }
                }
            }
        }


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

        // EDITAR: activa el modo edición
        protected void gvMedicos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMedicos.EditIndex = e.NewEditIndex;
            CargarMedicos();
        }

        // CANCELAR: vuelve al modo normal
        protected void gvMedicos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMedicos.EditIndex = -1;
            CargarMedicos();
        }

        // ACTUALIZAR: guarda los cambios
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
            DateTime nuevaFechaNac = DateTime.Parse(fechaNacString);

            string nuevoSexo = ((DropDownList)row.FindControl("ddlSEXO_MED")).SelectedValue;

            int nuevaLocalidad = Convert.ToInt32(((DropDownList)row.FindControl("ddlID_LOC_MED")).SelectedValue);

            int nuevaProvincia = Convert.ToInt32(((DropDownList)row.FindControl("ddlID_PROV_MED")).SelectedValue);

            UserManager medico = new UserManager();
            medico.updateDoctor(idUsuario, nuevoNombre, nuevoApellido, nuevoDni,
                                nuevaDireccion, nuevoCorreo, nuevoTelefono, nuevaFechaNac,
                                nuevoSexo, nuevaLocalidad, nuevaProvincia, nuevoDiasHorario);

            gvMedicos.EditIndex = -1;
            CargarMedicos();
        }


        private void CargarMedicos()
        {
            Validar validar = new Validar();
            gvMedicos.DataSource = validar.ObtenerMedicos();
            gvMedicos.DataBind();
        }

        protected void btnConfirmarLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }
    }
}