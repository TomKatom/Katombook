<%@ Page Language="C#" AutoEventWireup="true" CodeFile="friendlist.aspx.cs" Inherits="Default2" %>
<!DOCTYPE html>
<html>
	<head>
		<meta charset="UTF-8">
		<title><%= fullName %> - Friendlist</title>
		<link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css'>
		<link href="//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" rel="stylesheet">
        <link rel="stylesheet" href="CSS/friendlist.css">
	</head>
	<body>
        <% if (logged)
            { %>

        <div class="container">
          <div class="top">
              <% if (own)
                  { %>
            <h2>Friend's List</h2>
              <%} %>
              <% else
                  { %>
              <h2><%= fullName %>'s Friend's List</h2>
              <%} %>
          </div>
            <% for (int i = 0; i < friendsTable.Length; i++)
                {  %>
          <div class="row">
              <% if (i == 0)
                  { %>
            <div class="shadow">
            <%} %>
              <div class="col-sm-12">
                <div class="col-sm-2">
                  <img src="/ProfilePics/<%= friendsTable[i].Rows[0]["fldProfilePic"].ToString() %>" class="img-circle" width="60px">
                </div>
                <div class="col-sm-8">
                  <h4><a href="/profile.aspx?id=<%= friends[i].ToString() %>"><%= friendsTable[i].Rows[0]["fldFullName"].ToString() %></a></h4>
                </div>
                  <% if (friends[i].ToString() != Session["index"].ToString())
                      { %>
                <div class="col-sm-2">
                  <a href="/api/friend.aspx?mode=add&id=<%= friends[i].ToString() %>">Send Request</a>
                </div>
                  <% } %>
              <% if (i == 0)
                  { %>
              </div>
                <%} %>
              <div class="clearfix"></div>
              <hr />
            <% } %>
          <a href="/profile.aspx?id=<%= id.ToString() %>" class="btn btn-link">Go Back To Profile</a>
	</body>
    <% } %>
</html>