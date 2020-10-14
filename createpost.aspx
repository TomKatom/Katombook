<%@ Page Language="C#" AutoEventWireup="true" CodeFile="createpost.aspx.cs" Inherits="createpost" %>

<html>
    <head>
        <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
        <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
        <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
        <script src="https://cdn.rawgit.com/PascaleBeier/bootstrap-validate/v2.2.0/dist/bootstrap-validate.js" ></script>
        <title>KatomBook - Create Post</title>
    </head>
    <body>
        <% if (logged)
            { %>
        <br />
        <br />
        <br />  
        <div class="container">
            <form method="post" action="/api/Default.aspx">
                 <div class="form-group">
                     <label for="titleInput">Title</label>
                     <input type="text" class="form-control" id="titleInput" placeholder="Enter Title" name="title">
                 </div>
                 <div class="form-group">
                     <label for="contentTextArea">Content</label>
                     <textarea class="form-control" id="contentTextArea" placeholder="Your Content Here" name="content"></textarea>
                 </div>
                 <input type="text" value="createPost" name="action" hidden/>
                 <button type="submit" class="btn btn-primary">Create Post</button>
                 <a href="Default.aspx" class="btn btn-info">Go Back</a>
            </form>
        </div>
    </body>
    <% } %>
</html>