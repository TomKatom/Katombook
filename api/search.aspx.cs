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
using Newtonsoft.Json;
public partial class api_search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request.Params["term"]))
        {
            string term = Request.QueryString["term"];
            string returnValue = "[";
            DataTable result = MyAdoHelper.ExecuteDataTable("SELECT * FROM `tblUsers` WHERE `fldFullName` LIKE '%" + term + "%' ORDER BY `fldIndex` ASC;");
            for (int i = 0; i < result.Rows.Count; i++)
            {
                Result temp = new Result();
                temp.id = result.Rows[i]["fldIndex"].ToString();
                temp.value = result.Rows[i]["fldFullName"].ToString();
                returnValue = returnValue + JsonConvert.SerializeObject(temp) + ",";
            }
            returnValue = returnValue.Substring(0, returnValue.Length - 1);
            returnValue += "]";

            Response.Write(returnValue);
        }
        else
        {
            if (!string.IsNullOrEmpty(Request.Params["profileName"]))
            {
                if (MyAdoHelper.IsExist("Select * FROM `tblUsers` WHERE `fldFullName` ='" + Request.Form["profileName"] + "';"))
                {
                    DataTable userFound = MyAdoHelper.ExecuteDataTable("Select * FROM `tblUsers` WHERE `fldFullName` ='" + Request.Form["profileName"] + "';");
                    string id = userFound.Rows[0]["fldIndex"].ToString();
                    Response.Redirect("/profile.aspx?id=" + id, false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    Response.Redirect("/Default.aspx?m=found", false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
            else
            {
                Response.Write("Invalid Parameters!");
            }
        }
 }
   
    public class Result
    {
        public string id;
        public string value;
    }
}