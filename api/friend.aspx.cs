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

public partial class api_addfriend : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count < 2)
        {
            Response.Redirect("/Login.aspx", false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
             
        switch (Request.QueryString["mode"])
        {
            case "add":
                AddFriend(Request.QueryString["id"]);
                break;
        }
    }
    protected void AddFriend(string id)
    {
        if(id == Session["index"].ToString())
        {
             Response.Redirect("/profile.aspx?id=" + id + "&m=self", false);
             HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        else if (MyAdoHelper.IsExist(string.Format("SELECT * FROM `tblFriends` WHERE `user1_id` = {0} AND `user2_id` = {1} AND `mode` = 'friends';", int.Parse(Session["index"].ToString()), int.Parse(id))))
        {
            Response.Redirect("/profile.aspx?id=" + id + "&m=already", false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        else if (MyAdoHelper.IsExist(string.Format("SELECT * FROM `tblFriends` WHERE `user1_id` = {0} AND `user2_id` = {1} AND `mode` = 'sent';", int.Parse(Session["index"].ToString()), int.Parse(id))))
        {
            Response.Redirect("/profile.aspx?id=" + id + "&m=many", false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        else if (MyAdoHelper.IsExist("SELECT * FROM `tblFriends` WHERE `user1_id` = " + int.Parse(Session["index"].ToString()) + " AND `user2_id` = " + int.Parse(id) + " AND `mode` = 'pending'"))
        {
            MyAdoHelper.DoQuery("UPDATE `tblFriends` SET `mode` = 'friends' WHERE `user1_id` = " + int.Parse(Session["index"].ToString()) + "AND `user2_id` = " + int.Parse(id) + ";");
            MyAdoHelper.DoQuery("UPDATE `tblFriends` SET `mode` = 'friends' WHERE `user2_id` = " + int.Parse(Session["index"].ToString()) + "AND `user1_id` = " + int.Parse(id) + ";");
            Response.Redirect("/profile.aspx?id=" + id + "&m=friends", false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        else
        {
            MyAdoHelper.DoQuery(string.Format("INSERT INTO `tblFriends` (`user1_id`, `user2_id`, `mode`) VALUES ({0}, {1}, 'sent');", int.Parse(Session["index"].ToString()), int.Parse(id)));
            MyAdoHelper.DoQuery(string.Format("INSERT INTO `tblFriends` (`user1_id`, `user2_id`, `mode`) VALUES ({0}, {1}, 'pending');", int.Parse(id), int.Parse(Session["index"].ToString())));
            Response.Redirect("/profile.aspx?id=" + id + "&m=sent", false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        Response.Write("ok");
    }
    public int[] GetFriends(string id)
    {
        DataTable friendsTable = MyAdoHelper.ExecuteDataTable("SELECT * FROM `tblFriends` WHERE `user1_id` = " + int.Parse(id) + ";");
        int[] friends = new int[friendsTable.Rows.Count];
        for (int i = 0; i < friends.Length; i++)
            friends[i] = 0;
        for(int i = 0; i < friendsTable.Rows.Count; i++)
        {
            friends[i] = int.Parse(friendsTable.Rows[i]["user2_id"].ToString());
        }
        return friends;
    }
}
