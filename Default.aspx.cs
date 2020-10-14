using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using com.KatomBook;

public partial class _Default : System.Web.UI.Page 
{
    public DataTable[] posts;
    public int[] friends;
    public bool logged;
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
            friends = GetFriends(Session["index"].ToString());
            posts = new DataTable[friends.Length];
            for (int i = 0; i < posts.Length; i++)
            {
                posts[i] = MyAdoHelper.ExecuteDataTable("SELECT * FROM `tblPosts` WHERE `fldAuthorIndex` = " + friends[i] + ";");
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
