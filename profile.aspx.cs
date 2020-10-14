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

public partial class profile : System.Web.UI.Page
{
    public string id = "", lastSeenMessage = "";
    public string fullName, bio, desc, email, website, address, profilePic;
    public bool own, logged, exists;
    protected void Page_Load(object sender, EventArgs e)
    {
        int lastSeen = 0;
        logged = true;

        if (Session.Count < 2)
        {
            logged = false;
            Response.Redirect("/Login.aspx", false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();

        }

        Response.Write("<br>");
        if (logged)
        {
            if (string.IsNullOrEmpty(Request.Params["id"]))
            {
                id = Session["index"].ToString();
            }
            else
            {
                id = Request.QueryString["id"].ToString();
            }
            exists = MyAdoHelper.IsExist("SELECT * FROM `tblUsers` WHERE `fldIndex` = " + id + ";");
            if (exists)
            {
                DataTable userInfo = MyAdoHelper.ExecuteDataTable("Select * FROM `tblUsers` WHERE `fldIndex` = " + id + ";");
                fullName = userInfo.Rows[0]["fldFullName"].ToString();
                bio = userInfo.Rows[0]["fldBio"].ToString();
                desc = userInfo.Rows[0]["fldDescription"].ToString();
                email = userInfo.Rows[0]["fldEmail"].ToString();
                website = userInfo.Rows[0]["fldWebsite"].ToString();
                lastSeen = int.Parse(userInfo.Rows[0]["fldLastSeen"].ToString());
                address = userInfo.Rows[0]["fldAddress"].ToString();
                profilePic = userInfo.Rows[0]["fldProfilePic"].ToString();
                own = Session["index"].ToString() == id;
                if (lastSeen == 0 || lastSeen < 0)
                {
                    lastSeenMessage = "Never";
                }
                if(lastSeen > 0)
                {
                    lastSeen = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds - lastSeen;
                    if (lastSeen < 120)
                    {
                        lastSeenMessage = "Currently Online";
                    }
                    else if (lastSeen > 300 && lastSeen < 600)
                    {
                        lastSeenMessage = "5 Minutes Ago.";
                    }
                    else if (lastSeen > 600 && lastSeen < 1200)
                    {
                        lastSeenMessage = "20 Minutes Ago.";
                    }
                    else if (lastSeen > 1800 && lastSeen < 3600)
                    {
                        lastSeenMessage = "30 Minutes Ago.";
                    }
                    else
                    {
                        lastSeenMessage = string.Format("{0} Hours Ago.", (int)(lastSeen * 0.000277777778));
                    }
                }

            }
            else
            {
                Response.Redirect("/Default.aspx?m=found", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }

        }

    }
    public int[] GetFriends(string id)
    {
        DataTable friendsTable = MyAdoHelper.ExecuteDataTable("SELECT * FROM `tblFriends` WHERE `user1_id` = " + int.Parse(id) + ";");
        int[] friends = new int[friendsTable.Rows.Count];
        for (int i = 0; i < friends.Length; i++)
            friends[i] = 0;
        for (int i = 0; i < friendsTable.Rows.Count; i++)
        {
            friends[i] = int.Parse(friendsTable.Rows[i]["user2_id"].ToString());
        }
        return friends;
    }
}
