<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>
<html>
	<head>
		<title>KatomBook - World's Leading Social Network</title>
		<link rel="stylesheet" href="CSS/index.css">
		<meta charset="utf-8">
		<meta name="viewport" content="width=device-width, initial-scale=1">

        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
	    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
	    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>
		<script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
        <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
	    <!-- Latest compiled and minified CSS -->
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
	    <!-- Latest compiled JavaScript -->

	    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>
	</head>
    <% if (logged)
        { %>
		<body>
		<nav class="navbar navbar-expand-lg navbar-light bg-light">
		    <a class="navbar-brand" href="#">KatomBook</a>
		    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
		        <span class="navbar-toggler-icon"></span>
		    </button>
		    <div class="collapse navbar-collapse" id="navbarSupportedContent">
		        <ul class="navbar-nav mr-auto">
		            <li class="nav-item active">
		                <a class="nav-link" href="/Default.aspx">Home <span class="sr-only">(current)</span></a>
		            </li>
		            <li class="nav-item dropdown">
		                <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
		                    <%= Session["fullName"] %>
		                </a>
		                <div class="dropdown-menu" aria-labelledby="navbarDropdown">
		                    <a class="dropdown-item" href="/friendlist.aspx">Friend List</a>
		                    <div class="dropdown-divider"></div>
		                    <a class="dropdown-item" href="/api/logout.aspx">Logout</a>
		                </div>
		            </li>
		            <li class="nav-item">
		                <a class="nav-link" href="/profile.aspx">Profile</a>
		            </li>
                    <% if (Session["isAdmin"].ToString() == "True")
                        { %>
                    <li class="nav-item">
		                <a class="nav-link" href="/admin.aspx">Admin Page</a>
		            </li>
                    <%} %>
		        </ul>
                <ul class =" navbar-nav ml-auto">
                    <li class="nav-item">
                        <a class="nav-link" href="/createpost.aspx">Create A Post</a>
                    </li>
                </ul>
		        <form class="form-inline my-2 my-lg-0" action="/api/search.aspx" method="post">
		            <input class="form-control mr-sm-2 search" type="search" placeholder="Search" aria-label="Search" name="profileName">
		            <button class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
		        </form>
		    </div>
		</nav>
            <main role="main" class="container">
                <div class="row">
                    <div class="col-md-8">
                        <% for (int i = 0; i < posts.Length; i++)
                            { %>
                                <% if (posts[i].Rows.Count > 0)
                                    { %>
                                    <% for (int j = 0; j < posts[i].Rows.Count; j++)
                                        { %>
                                            <br />
                                          <article class="media content-section">
                                            <img class="rounded-circle article-img" src="ProfilePics/<%= com.KatomBook.MyAdoHelper.ExecuteDataTable("SELECT * FROM `tblUsers` WHERE `fldIndex` = " + friends[i] + ";").Rows[0]["fldProfilePic"].ToString() %>">
                                            <div class="media-body">
                                              <div class="article-metadata">
                                                <a class="mr-2" href="/profile.aspx?id=<%= posts[i].Rows[j]["fldAuthorIndex"].ToString() %>"><%= posts[i].Rows[j]["fldAuthorName"].ToString() %></a>
                                                <small class="text-muted"><%= posts[i].Rows[j]["fldDate"].ToString() %></small>
                                              </div>
                                              <h2 class="article-title"><%= posts[i].Rows[j]["fldTitle"].ToString() %></h2>
                                              <p class="article-content"><%= posts[i].Rows[j]["fldContent"].ToString() %></p>
                                            </div>
                                          </article>
                                    <% } %>
                                <% } %>
                        <%} %>
                    </div>
                </div>
            </main>
		</body>
		<script>
		    function httpGet(theUrl)
		    {
		        var xmlHttp = new XMLHttpRequest();
		        xmlHttp.open( "GET", theUrl, false ); // false for synchronous request
		        xmlHttp.send( null );
		        return xmlHttp.responseText;
		    }
		    function seenow(){
		        httpGet("/api/Default.aspx?set=now");
		    }
		    setInterval(function(){
		        seenow();
		    }, 6000)
		    $(function () {
; 
		        $(".search").autocomplete({
		            source: "api/search.aspx"
		        });
		    });
		</script>
    <% } %>
</html>