using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas.Admin.informes
{
    public partial class verInformes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarInformes();
                CargarInformeEspecialidades();
            }

            if (Session["role"] == null || Session["role"].ToString() != "ADMIN")
            {
                Response.Redirect("~/Login.aspx");
            }

            this.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            username.Text = Session["username"].ToString();
        }

        // CARGA DE INFORMES EN GRID
        private void CargarInformes()
        {
            Validar validar = new Validar();
            gvReporteMedicosMayoriaTurnos.DataSource = validar.MedicosConMasTurnos();
            gvReporteMedicosMayoriaTurnos.DataBind();

            // Opcional: Ajustar estilo si hay muchos datos
            gvReporteMedicosMayoriaTurnos.HeaderStyle.BackColor = System.Drawing.Color.LightGray;
            gvReporteMedicosMayoriaTurnos.HeaderStyle.Font.Bold = true;

        }

        // Nueva función exclusiva para especialidades
        private void CargarInformeEspecialidades()
        {
            Validar validar = new Validar();
            DataTable datosBrutos = validar.EspecialidadConMasTurnos();

            if (datosBrutos != null && datosBrutos.Rows.Count > 0)
            {
                // Asignamos directamente el DataTable devuelto
                gvEspecialidadTop.DataSource = datosBrutos;
                gvEspecialidadTop.DataBind();

                // Estilos opcionales
                gvEspecialidadTop.HeaderStyle.BackColor = System.Drawing.Color.LightGray;
                gvEspecialidadTop.HeaderStyle.Font.Bold = true;
            }
            else
            {
                // Manejo cuando no hay datos
                DataTable dtEmpty = new DataTable();
                dtEmpty.Columns.Add("Especialidad", typeof(string));
                dtEmpty.Columns.Add("TotalTurnos", typeof(int));
                dtEmpty.Rows.Add("No data available", 0);

                gvEspecialidadTop.DataSource = dtEmpty;
                gvEspecialidadTop.DataBind();
            }
        }

        protected void gvReporteMedicosMayoriaTurnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReporteMedicosMayoriaTurnos.PageIndex = e.NewPageIndex;
            CargarInformes();
        }

        protected void gvReporteMedicosMayoriaTurnos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Verifica que es una fila de datos
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                Label lblRank = (Label)e.Row.FindControl("lblRANK");

                if (lblRank != null)
                {
                    // Las filas comienzan desde índice 0, sumamos 1 para mostrar desde 1
                    lblRank.Text = (e.Row.RowIndex + 1).ToString();
                }
            }
        }

        // VOLVER A LOGIN
        protected void btnConfirmarLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }
    }
}