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

public partial class api_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
        {
            Response.Redirect("/Login.aspx", false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        try
        {
            switch (Request.Form["action"])
            {
                case "login":
                    UserLogin(Request.Form["username"].ToString(), Request.Form["password"].ToString());
                    break;
                case "register":
                    Response.Write(UserRegister(Request.Form["username_register"], Request.Form["name_register"], Request.Form["password_register"], Request.Form["email_register"], Request.Form["password_register_rt"]));
                    break;
                case "updateProfile":
                    UpdateProfile(Request.Form["bio"], Request.Form["website"], Request.Form["address"], Request.Form["description"],  Request.Form["id"]);
                    break;
                case "createPost":
                    createPost(Request.Form["title"], Request.Form["content"]);
                    break;
                case "removeUser":
                    if(Session["isAdmin"].ToString() == "True")
                    {
                        MyAdoHelper.DoQuery("DELETE FROM `tblUsers` WHERE `fldIndex` = " + Request.Form["userIndex"].ToString() + ";");
                        MyAdoHelper.DoQuery("DELETE FROM `tblFriends` WHERE `user1_id` = " + Request.Form["userIndex"].ToString() + ";");
                        MyAdoHelper.DoQuery("DELETE FROM `tblFriends` WHERE `user2_id` = " + Request.Form["userIndex"].ToString() + ";");
                        Response.Redirect("/admin.aspx?m=s", false);
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                    else
                    {
                        Response.Write("No Permission!");
                    }
                    
                    break;
            }
            if (!string.IsNullOrEmpty(Request.Params["set"]))
            {
                if(Request.QueryString["set"].ToString() == "now")
                {
                    MyAdoHelper.DoQuery("UPDATE `tblUsers` SET `fldLastSeen` = " + (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds + " WHERE `fldIndex` = " + Session["index"].ToString() + ";");
                }
            }

        }
        catch (FormatException ex)
        {
            Response.Write("<h1 style='text-align:center'>Invalid Arguments!</h1> " + ex);
            Response.End();
        }
        catch (Exception ex)
        {
            Response.StatusCode = 500;
            Response.Write("<h1 style='text-align:center'>Internal Server Error!</h1> Error: <b>" + ex + "</b>");
            Response.End();
        }
    }

    public static bool IsValidEmail(string email)
    {
        try
        {
            MailAddress addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
    private void UserLogin(string username, string password)
    {
        if(MyAdoHelper.IsExist(string.Format("SELECT * FROM `tblUsers` WHERE fldUsername = '{0}' AND fldPassword = '{1}';", username, password)))
        {
            DataTable userTable = MyAdoHelper.ExecuteDataTable(string.Format("SELECT * FROM `tblUsers` WHERE fldUsername = '{0}' AND fldPassword = '{1}';", username, password));
            Session["username"] = username;
            Session["fullName"] = userTable.Rows[0]["fldFullName"].ToString();
            Session["isAdmin"] = userTable.Rows[0]["fldIsAdmin"].ToString() == "True";
            Session["index"] = userTable.Rows[0]["fldIndex"].ToString();
            Application["counter"] = int.Parse(Application["counter"].ToString()) + 1;
            Response.Redirect("/Default.aspx", false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        
        else
        {
            Response.Redirect("/Login.aspx?m=found", false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        // return string.Format("[username: '{0}', FullName: '{1}', IsAdmin: '{2}']", username, userTable.Rows[0]["fldFullName"].ToString(), userTable.Rows[0]["fldIsAdmin"].ToString());
    }

    private string UserRegister(string username, string fullName, string password, string email, string re_password)
    {
        if (MyAdoHelper.IsExist("SELECT * FROM `tblUsers` WHERE `fldUsername` = '" + username + "'"))
            return "Username Exists";
        if (MyAdoHelper.IsExist("SELECT * FROM `tblUsers` WHERE `fldEmail` = '" + email + "'"))
            return "Email Already Exists";

        if (password != re_password)
            return "Passwords Do Not Match";
        if (password.Length < 8)
            return "Invalid Password Length";
        if (!IsValidEmail(email))
            return "Invalid Email";

        MyAdoHelper.ExecuteDataTable(string.Format("INSERT INTO `tblUsers` (`fldUsername`, `fldEmail`, `fldPassword`, `fldFullName`, `fldBio`, `fldDescription`, `fldProfilePic`) VALUES ('{0}', '{1}', '{2}', '{3}', 'KatomBook User', 'Hey there!\nI am using KatomBook.', 'defprofile.png');", username, email, password, fullName));
        return "Created";
    }

    private void UpdateProfile(string bio, string website, string address, string description, string id)
    {
        MyAdoHelper.DoQuery("UPDATE `tblUsers` SET `fldBio` = '" + bio + "', `fldWebsite` = '" + website + "', `fldAddress` = '" + address + "', `fldDescription` = '" + description + "'WHERE `fldIndex` = " + int.Parse(id) + ";");
        Response.Redirect("/profile.aspx", false);
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }
    private void createPost(string title, string content)
    {
        int authorIndex = int.Parse(Session["index"].ToString());
        if(title.Length < 1)
        {
            Response.Write("No Empty Titles!");
            Response.End();
        }
        if(content.Length < 1)
        {
            Response.Write("No Empty Content!");
            Response.End();
        }
        string date = DateTime.Now.ToString("dd/MM/yyyy");
        MyAdoHelper.DoQuery(string.Format("INSERT INTO `tblPosts` (`fldAuthorIndex`, `fldTitle`, `fldDate`, `fldContent`, `fldAuthorName`) VALUES ({0}, '{1}', '{2}', '{3}', '{4}'); ", authorIndex, title, date, content, Session["fullName"].ToString()));
        Response.Redirect("/Default.aspx", false);
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }
}