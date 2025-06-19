using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace Vistas.Medicos
{
	public partial class turnos : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                CargarTurnos();
            }

            if (Session["role"] == null || Session["role"].ToString() != "Doctor")
            {
                Response.Redirect("~/Login.aspx");
            }

            username.Text = Session["username"].ToString();
        }
        private void CargarTurnos()
        {
            Validar validar = new Validar();
            gvTurnos.DataSource = validar.ObtenerTurnos();
            gvTurnos.DataBind();
        }

        protected void btnConfirmarLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx"); 
        }

    }
}