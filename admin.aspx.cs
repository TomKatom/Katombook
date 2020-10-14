using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.ServiceModel.Security;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.KatomBook;

public partial class admin : System.Web.UI.Page
{
    public bool logged;
    public DataTable users;
    protected void Page_Load(object sender, EventArgs e)
    {
        logged = true;
        if (Session.Count < 2)
        {
            logged = false;
            Response.Redirect("/Login.aspx", false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        else if(Session["isAdmin"].ToString() != "True")
        {
            logged = false;
            Response.Redirect("/Login.aspx", false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        if(logged)
        {
            users = MyAdoHelper.ExecuteDataTable("SELECT * FROM `tblUsers` WHERE `fldIsAdmin` = 0 ;");
        }
    }
}