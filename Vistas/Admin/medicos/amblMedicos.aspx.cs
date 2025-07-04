using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;
using Vistas.Medicos;

namespace Vistas
{
    public enum WeekDays
    {
        MONDAY,
        TUESDAY,
        WEDNESDAY,
        THURSDAY,
        FRIDAY,
        SATURDAY,
        SUNDAY
    }

	public partial class amblMedicos : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMedicos();
                username.Text = Session["username"].ToString();
                cargarWeekDays();
                cargarStates(ddlState, "Any state");
                cargarSpecialitiesGeneral(ddlSpeciality, "Any speciality");
            }

            if (Session["role"] == null || Session["role"].ToString() != "ADMIN")
            {
                Response.Redirect("~/Login.aspx");
            }

            this.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        // CARGA DE DDLS
        protected void gvMedicos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gvMedicos.EditIndex == e.Row.RowIndex)
            {
                e.Row.CssClass = "edit-row";

                cargarProvinciasDDL(e);
                cargarEspecialidadesDDL(e);
                cargarDiasHorariosDDL(e);
            }
        }

        // CARGAR DIAS EN EL FILTRO
        private void cargarWeekDays()
        {
            var estados = Enum.GetNames(typeof(WeekDays))
                .Select(s => new { Value = s, Text = s });

            ddlWeekDay.DataSource = estados;
            ddlWeekDay.DataTextField = "Text";
            ddlWeekDay.DataValueField = "Value";
            ddlWeekDay.DataBind();

            ddlWeekDay.Items.Insert(0, new ListItem("Any day", ""));
        }

        // CARGAR PROVINCIAS EN EL FILTRO
        private void cargarStates(DropDownList ddl, string message)
        {
            Validar validar = new Validar();    

            DataTable dtProvincia = validar.ObtenerProvincia();
            ddl.DataSource = dtProvincia;
            ddl.DataTextField = "NAME_STATE";
            ddl.DataValueField = "ID_STATE";
            ddl.AutoPostBack = true;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem(message, ""));
        }

        // CARGA DE DDL PROVINCIAS Y LOCALIDADES
        private void cargarProvinciasDDL(GridViewRowEventArgs e)
        {
            Validar validar = new Validar();

            // PROVINCIA
            DropDownList ddlProvincia = (DropDownList)e.Row.FindControl("ddlID_PROV_MED");
            DropDownList ddlLocalidad = (DropDownList)e.Row.FindControl("ddlID_LOC_MED");

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
                    object provObj = DataBinder.Eval(e.Row.DataItem, "ID_STATE_DOC");
                    idProvinciaActual = provObj != null ? Convert.ToInt32(provObj) : 0;
                }

                if (ddlProvincia.Items.FindByValue(idProvinciaActual.ToString()) != null)
                    ddlProvincia.SelectedValue = idProvinciaActual.ToString();

                if (ddlLocalidad != null)
                {
                    DataTable dtLocalidades = validar.ObtenerLocalidadesFiltradas(idProvinciaActual);
                    ddlLocalidad.DataSource = dtLocalidades;
                    ddlLocalidad.DataTextField = "NAME_CITY";
                    ddlLocalidad.DataValueField = "ID_CITY";
                    ddlLocalidad.DataBind();
                    ddlLocalidad.Items.Insert(0, new ListItem("< SELECT >", ""));

                    // Seleccionar localidad actual
                    object locObj = DataBinder.Eval(e.Row.DataItem, "ID_CITY_DOC");
                    string idLocActual = locObj != null ? locObj.ToString() : "";

                    if (ddlLocalidad.Items.FindByValue(idLocActual) != null)
                        ddlLocalidad.SelectedValue = idLocActual;
                }
            }
        }

        // CARGA DE DDL ESPECIALIDADES 
        private void cargarEspecialidadesDDL(GridViewRowEventArgs e)
        {
            Validar validar = new Validar();

            // ESPECIALIDAD
            DropDownList ddlEspecialidad = (DropDownList)e.Row.FindControl("ddlID_ESP");
            if (ddlEspecialidad != null)
            {
                 cargarSpecialitiesGeneral(ddlEspecialidad, "< SELECT >");

                 object espObj = DataBinder.Eval(e.Row.DataItem, "ID_SPE_DOC");
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

        // CARGAR ESPECIALIDADES EN EL FILTRO
        private void cargarSpecialitiesGeneral(DropDownList ddl, string message)
        {
            Validar validar = new Validar();
            DataTable dtEspecialidades = validar.ObtenerEspecialidades();
            ddl.DataSource = dtEspecialidades;
            ddl.DataTextField = "NAME_SPE";
            ddl.DataValueField = "ID_SPE";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem(message, ""));
        }

        // CARGA DE DIAS Y HORARIOS EN SUS DDL
        private void cargarDiasHorariosDDL(GridViewRowEventArgs e)
        {

            DropDownList ddlDias = (DropDownList)e.Row.FindControl("ddlDIAS");
            TextBox txtStart = (TextBox)e.Row.FindControl("txtTIME_START");
            TextBox txtEnd = (TextBox)e.Row.FindControl("txtTIME_END");

            if (ddlDias != null)
            {
                Validar validar = new Validar();
                DataTable dtDias = validar.ObtenerDias();

                ddlDias.DataSource = dtDias;
                ddlDias.DataTextField = "WEEKDAY_SCH";
                ddlDias.DataValueField = "WEEKDAY_SCH";
                ddlDias.DataBind();

                string diasTrabajo = DataBinder.Eval(e.Row.DataItem, "DIAS_TRABAJO")?.ToString();

                if (!string.IsNullOrEmpty(diasTrabajo))
                {
                    string[] diasArray = diasTrabajo.Split(',');


                    string primerDia = diasArray[0].Trim();

                    if (ddlDias.Items.FindByValue(primerDia) != null)
                        ddlDias.SelectedValue = primerDia;
                }
            }

            string entradas = DataBinder.Eval(e.Row.DataItem, "HORA_ENTRADA")?.ToString();
            string salidas = DataBinder.Eval(e.Row.DataItem, "HORA_SALIDA")?.ToString();

            if (!string.IsNullOrEmpty(entradas))
                ViewState["EntradasHorarios"] = entradas.Split(',');
            if (!string.IsNullOrEmpty(salidas))
                ViewState["SalidasHorarios"] = salidas.Split(',');

            if (txtStart != null && txtEnd != null)
            {
                string[] entradasArray = (string[])ViewState["EntradasHorarios"];
                string[] salidasArray = (string[])ViewState["SalidasHorarios"];

                if (entradasArray.Length > 0)
                    txtStart.Text = entradasArray[0].Trim();
                if (salidasArray.Length > 0)
                    txtEnd.Text = salidasArray[0].Trim();
            }
        }

        // EVENTO DE UNSUBSCRIBE
        protected void gvMedicos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            bajaLogica(e);
        }

        // BAJA LOGICA
        private void bajaLogica (GridViewCommandEventArgs e)
        {
            if (e.CommandName != "DarDeBaja")
            {
                return;
            }

            GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int index = row.RowIndex;
            int idUsuario = Convert.ToInt32(gvMedicos.DataKeys[index].Value);


            AdminDoctorManager medico = new AdminDoctorManager();
            medico.deleteDoctor(idUsuario);

            lblMensaje.Text = "The doctor was deleted succesfully.";
            lblMensaje.ForeColor = System.Drawing.Color.Red;

            CargarMedicos();

        }

        // EDITAR
        protected void gvMedicos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            lblMensaje.Text = "";
            gvMedicos.EditIndex = e.NewEditIndex;
            CargarMedicos();
        }

        // CANCELAR
        protected void gvMedicos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            lblMensaje.Text = "";
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
            string nuevaNacionalidad = ((TextBox)row.FindControl("txtNACIONALIDAD_MED")).Text;
            string nuevoHorarioInicio = ((TextBox)row.FindControl("txtTIME_START")).Text;
            string nuevoHorarioFin = ((TextBox)row.FindControl("txtTIME_END")).Text;

            TextBox txtFechaNac = (TextBox)row.FindControl("txtFECHANAC_MED");

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

            string nuevoSexo = ((DropDownList)row.FindControl("ddlSEXO_MED")).SelectedValue;

            DropDownList ddlEsp = (DropDownList)row.FindControl("ddlID_ESP");
            DropDownList ddlLoc = (DropDownList)row.FindControl("ddlID_LOC_MED");
            DropDownList ddlProv = (DropDownList)row.FindControl("ddlID_PROV_MED");
            DropDownList ddlDias = (DropDownList)row.FindControl("ddlDIAS");

            int nuevaEspecialidad = Convert.ToInt32(ddlEsp.SelectedValue);
            int nuevaLocalidad = Convert.ToInt32(ddlLoc.SelectedValue);
            int nuevaProvincia = Convert.ToInt32(ddlProv.SelectedValue);
            string nuevosDias = (ddlDias.SelectedValue);
            
            if (ddlEsp == null || ddlEsp.SelectedValue == "0" || string.IsNullOrEmpty(ddlEsp.SelectedValue) ||
                ddlLoc == null || ddlLoc.SelectedValue == "0" || string.IsNullOrEmpty(ddlLoc.SelectedValue) ||
                ddlProv == null || ddlProv.SelectedValue == "0" || string.IsNullOrEmpty(ddlProv.SelectedValue) ||
                ddlDias == null || ddlDias.SelectedValue == "0" || string.IsNullOrEmpty(ddlDias.SelectedValue))
            {
                lblMensaje.Text = "Select a valid city, day, locality and/or speciality.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            AdminDoctorManager medico = new AdminDoctorManager();
            medico.updateDoctor(idUsuario, nuevoNombre, nuevoApellido, nuevoDni,
                                nuevaDireccion, nuevoCorreo, nuevoTelefono, nuevaNacionalidad, nuevosDias, nuevoHorarioInicio, nuevoHorarioFin, nuevaEspecialidad, nuevaFechaNac,
                                nuevoSexo, nuevaLocalidad, nuevaProvincia);

            lblMensaje.Text = "The doctor was modified succesfully.";
            lblMensaje.ForeColor = System.Drawing.Color.Green;

            gvMedicos.EditIndex = -1;
            CargarMedicos();
        }

        // CARGA DE GRIDVIEW
        private void CargarMedicos()
        {
            string state = ddlState.SelectedValue;
            string weekDay = ddlWeekDay.SelectedValue;
            string speciality = ddlSpeciality.SelectedValue;

            string user = txtUser.Text.Trim();

            Validar validar = new Validar();
            gvMedicos.DataSource = validar.ObtenerMedicos(state, weekDay, speciality, user);
            gvMedicos.DataBind();
        }

        // MODIFICACION DE LOCALIDADES SEGUN LA PROV ELEGIDA
        protected void ddlID_PROV_MED_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            DropDownList ddlProvincia = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlProvincia.NamingContainer;

            int index = row.RowIndex;
            int nuevaProvincia;

            if (int.TryParse(ddlProvincia.SelectedValue, out nuevaProvincia))
            {
                ViewState["ProvinciaSeleccionadaGV"] = nuevaProvincia;

                gvMedicos.EditIndex = index;
                CargarMedicos(); // Vuelve a cargar la grilla con la provincia seleccionada
            }
            else
            {
                lblMensaje.Text = "You must select a valid city.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        // MODIFICACION DE HORARIOS SEGUN EL DIA ELEGIDO
        protected void ddlDIAS_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            DropDownList ddlDias = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlDias.NamingContainer;

            TextBox txtStart = (TextBox)row.FindControl("txtTIME_START");
            TextBox txtEnd = (TextBox)row.FindControl("txtTIME_END");

            string[] entradasArray = (string[])ViewState["EntradasHorarios"];
            string[] salidasArray = (string[])ViewState["SalidasHorarios"];

            int diaSeleccionado = ddlDias.SelectedIndex;

            if (diaSeleccionado >= 0 && diaSeleccionado < entradasArray.Length)
                txtStart.Text = entradasArray[diaSeleccionado].Trim();

            if (diaSeleccionado >= 0 && diaSeleccionado < salidasArray.Length)
                txtEnd.Text = salidasArray[diaSeleccionado].Trim();
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

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            CargarMedicos();

        }

        protected void ddlSpeciality_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            CargarMedicos();
        }

        protected void ddlWeekDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            CargarMedicos();
        }

        protected void txtUser_TextChanged(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            CargarMedicos();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtUser.Text = "";
            ddlWeekDay.SelectedIndex = 0;
            ddlSpeciality.SelectedIndex = 0;
            ddlState.SelectedIndex = 0;
            CargarMedicos();
        }
    }
}