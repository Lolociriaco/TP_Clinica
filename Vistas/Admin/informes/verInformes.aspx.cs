using Negocio;
using System;
using System.Collections.Generic;
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
            }

            if (Session["role"] == null || Session["role"].ToString() != "ADMINISTRADOR")
            {
                Response.Redirect("~/Login.aspx");
            }

            username.Text = Session["username"].ToString();
        }

        // CARGA DE INFORMES EN GRID
        private void CargarInformes()
        {
            Validar validar = new Validar();
            gvReporteMedicosMayoriaTurnos.DataSource = validar.MedicosConMasTurnos();
            gvReporteMedicosMayoriaTurnos.DataBind();
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