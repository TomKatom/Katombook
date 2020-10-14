using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class createpost : System.Web.UI.Page
{
    public bool logged;
    protected void Page_Load(object sender, EventArgs e)
    {
        logged = true;
        if(Session.Count < 2)
        {
            logged = false;
            Response.Redirect("/Login.aspx", false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}