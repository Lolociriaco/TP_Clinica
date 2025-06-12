using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas
{
	public partial class abmlPaciente : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEmptyGrid();
            }
        }

        private void BindEmptyGrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("DNI");
            dt.Columns.Add("Phone Number");

            // No se agregan filas => tabla vacía
            gvPacientes.DataSource = dt;
            gvPacientes.DataBind();
        }

        protected void btnConfirmarLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx"); // Cambialo por la ruta a tu login
        }
    }
}