using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class api_logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] != null)
        {
            Session.Clear();
            Response.Redirect("/Login.aspx");
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        else
        {
            Response.Redirect("/Login.aspx");
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}