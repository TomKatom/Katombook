<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin.aspx.cs" Inherits="admin" %>

<html>
    <head>
        <title>KatomBook - Admin Page</title>
        <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
        <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
        <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
        <link rel="stylesheet" href="CSS/admin.css">
    </head>
    <body>
        <% if (logged)
                 { %>
        <center><h1>KatomBook - Control Panel</h1></center>
        <br />
        <center>
            <h4>Total Logins: <%= Application["counter"].ToString() %></h4>
            <table style="width:75%">
                <tr>
                    <th>Index</th>
                    <th>Email</th>
                    <th>Password</th>
                    <th>Name</th>
                    <th>Bio</th>
                    <th>Website</th>
                    <th>Address</th>
                    <th>Description</th>
                </tr>
                <% for (int i = 0; i < users.Rows.Count; i++)
                 { %>
                        <tr>
                            <td><%= users.Rows[i]["fldIndex"].ToString() %></td>
                            <td><%= users.Rows[i]["fldEmail"].ToString() %></td>
                            <td><%= users.Rows[i]["fldPassword"].ToString() %></td>
                            <td><%= users.Rows[i]["fldFullName"].ToString() %></td>
                            <td><%= users.Rows[i]["fldBio"].ToString() %></td>
                            <td><%= users.Rows[i]["fldWebsite"].ToString() %></td>
                            <td><%= users.Rows[i]["fldAddress"].ToString() %></td>
                            <td><%= users.Rows[i]["fldDescription"].ToString() %></td>
                        </tr>
                 <% } %>
            </table>
            <br />
        </center>
        <div class="container">
            <form class="form-inline" onsubmit="return checkInput();" method="post" action="/api/Default.aspx">
              <label class="sr-only" for="indexInput">Id To Delete:</label>
              <input type="text" class="form-control mb-2 mr-sm-2" id="indexInput" placeholder="Enter User ID To Delete" name="userIndex" required>
              <input type="text" value="removeUser" name="action" hidden />
              <button type="submit" class="btn btn-primary mb-2">Delete</button>
            </form>
        </div>
        <% } %>
        <center><a href="Default.aspx" class="btn btn-info">Go Back</a></center>
    </body>
    <script>
        var url_string = window.location.href
        var url = new URL(url_string);
        if (url.searchParams.get("m") == "s") {
            alert("Successfully Deleted The User.");
        }
        function checkInput() {
            idTextbox = document.getElementById("indexInput");
            if (isNaN(idTextbox.value)) {
                alert("Invalid Index Number!");
                return false;
            }
            else {
                return true;
            }
        }
    </script>
</html>