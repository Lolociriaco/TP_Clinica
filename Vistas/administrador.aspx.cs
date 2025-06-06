using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas
{
	public partial class administrador : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void btnCambiarPaciente_Click(object sender, EventArgs e)
        {
            Response.Redirect("abmlPaciente.aspx");
        }

        protected void btnModificarMedico_Click(object sender, EventArgs e)
        {
            Response.Redirect("amblMedicos.aspx");
        }
    }
}