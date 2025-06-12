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
            if (Session["role"] == null || Session["role"].ToString() != "Admin")
            {
                //Response.Redirect("~/Login.aspx");
            }

            //username.Text = Session["username"].ToString();
        }

        protected void btnConfirmarLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }
    }
}