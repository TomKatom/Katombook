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


public partial class Default2 : System.Web.UI.Page
{

    public string id = "";
    public string fullName, bio, desc, email, website, lastSeen, address, profilePic;
    public bool own, logged;
    public int[] friends;
    public DataTable[] friendsTable;
    protected void Page_Load(object sender, EventArgs e)
    {
        logged = true;
        if (Session.Count < 2)
        {
            logged = false;
            Response.Redirect("/Login.aspx", false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
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
            DataTable userInfo = MyAdoHelper.ExecuteDataTable("Select * FROM `tblUsers` WHERE `fldIndex` = " + id + ";");
            fullName = userInfo.Rows[0]["fldFullName"].ToString();
            bio = userInfo.Rows[0]["fldBio"].ToString();
            desc = userInfo.Rows[0]["fldDescription"].ToString();
            email = userInfo.Rows[0]["fldEmail"].ToString();
            website = userInfo.Rows[0]["fldWebsite"].ToString();
            lastSeen = userInfo.Rows[0]["fldLastSeen"].ToString();
            address = userInfo.Rows[0]["fldAddress"].ToString();
            profilePic = userInfo.Rows[0]["fldProfilePic"].ToString();
            own = Session["index"].ToString() == id;
            friends = GetFriends(id);
            friendsTable = new DataTable[friends.Length];
            for (int i = 0; i < friendsTable.Length; i++)
            {
                friendsTable[i] = MyAdoHelper.ExecuteDataTable("SELECT * FROM `tblUsers` WHERE `fldIndex` = " + friends[i] + ";");
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
