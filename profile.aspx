<%@ Page Language="C#" AutoEventWireup="true" CodeFile="profile.aspx.cs" Inherits="profile" %>
<!DOCTYPE html>
<html>
		<head>

			<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
			<link rel="stylesheet" href="CSS/profile.css">
			<link rel="stylesheet" href="CSS/profilejquery.css">
			<meta charset="utf-8">
			<meta name="viewport" content="width=device-width, initial-scale=1">
			<title> <%= fullName %> | KatomBook </title>
            <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
	        <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
	        <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
		    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>


		</head>
<% if (logged && exists)
   { %>
				<body>
                   
					<!-- Font -->
					<link href="//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" rel="stylesheet">
					<div class="container">
					    <div class="col-md-9">
							<div class="row">
								<div class="col-md-4 col-sm-5">
									<div class="thumbnail">
										<!-- Profile Pic  -->
										<img src="ProfilePics/<%= profilePic %>" alt="Profile Picture">
										<% if (own)
										   { %>
						                    <form id="formUploadImg" action="/api/upload.aspx" method="post" class="upload-btn-wrapper" enctype="multipart/form-data">
												<button class="btn"><i class="fa fa-upload" type="submit"></i></button>
                                                <input type="text" name="id" value="<%= id %>" hidden />
	 											<input type="file" name="UploadedFile" id="fileToUpload" type="submit" onchange="form.submit();" />
	 										</form>
										 <% } %>
									</div>
									<br>
									<!-- Buttons -->
									<div class="list-group">  
										<a href="/Default.aspx" class="list-group-item">
											<i class="fa fa-asterisk"></i> Back To Home Page
											<i class="fa fa-chevron-right list-group-chevron"></i>
										</a> 
										<a href="#" class="list-group-item">
											<i class="fa fa-envelope"></i> ��Messages
											<i class="fa fa-chevron-right list-group-chevron"></i>
										</a> 
										<a href="friendlist.aspx?id=<%= id %>" class="list-group-item">
											<i class="fa fa-group"></i> ��Friends
											<i class="fa fa-chevron-right list-group-chevron"></i>
											<span class="badge">
	                                            <%= GetFriends(id).Length.ToString() %>
											</span>
										</a> 
										<a href="#" class="list-group-item">
											<i class="fa fa-cog"></i> ��Settings
											<i class="fa fa-chevron-right list-group-chevron"></i>
										</a> 
									</div> 
								</div> 

								<!-- MISC Profile Info -->
								<div class="col-md-8 col-sm-7">
									<h2><%= fullName %></h2>
									<h4><%= bio %></h4>
									<h5>Last Seen: <%= lastSeenMessage %></h5>
								    <% if (!own)
								       { %>
                                    
									    <hr>
									    
										    <!-- Message And Add Friend -->
										    <p>
                                                <a href="/api/friend.aspx?mode=add&id=<%= Request.QueryString["id"] %>" class="btn btn-success">Add Friend</a>
										        <a href="#" class="btn btn-info">Send Message</a>
                                            </p>
								    <% } %>
									<hr>
									<!-- Profile Contacts -->
									<ul class="icons-list">
										<li><i class="icon-li fa fa-envelope"></i> <%= email %></li>
										<% if (website.Length > 0)
										   { %>
											<li><i class="icon-li fa fa-globe"></i><a href="<%= website %>" target="_blank"> <%= website %> </a></li>
										<% } %>
										<% if (address.Length > 0)
										   { %>
											<li><i class="icon-li fa fa-map-marker"></i> <%= address %></li>
										<% } %>
									</ul>
									<br>
									<!-- Profile Bio -->
									<p><%= desc.Replace("\n", "<br>") %></p>
									<hr>
							</div>
							<% if (own)
							   { %>
								<button class="edit" id="edit" type="button"> Edit Profile </button>
							<% } %>
						</div>
					</div>
				</div>                                                            
			</div>
		<!-- Popup Form -->
		<div id="dialog-form" title="Edit Profile Information">
		  <form action="/api/Default.aspx" method="post" id="formEditProfile">
		    <fieldset>
		      <label for="bio">Bio</label>
		      <input type="text" name="bio" id="bio" value="<%= bio %>" class="text ui-widget-content ui-corner-all" size="60">
		      <label for="website">Website</label>
		      <input type="text" name="website" id="website" value="<%= website %>" class="text ui-widget-content ui-corner-all" size="60">
		      <label for="address">Address</label>
		      <input type="text" name="address" id="address" value="<%= address %>" class="text ui-widget-content ui-corner-all" size="60">
		      <label for="description">Description</label>
              <input type="text" name="action" value="updateProfile" hidden/>
               <input type="text" value="<%= id %>" name="id" hidden />
		      <textarea type="description" name="description" rows="5" cols="60" id="description" class="text ui-widget-content ui-corner-all"><%= desc %></textarea>

		      <!-- Allow form submission with keyboard without duplicating the dialog button -->
		      <input type="submit" tabindex="-1" style="position:absolute; top:-1000px">
		    </fieldset>
		  </form>

		  </div>
</body>
	<script>
	    var url_string = window.location.href
	    var url = new URL(url_string);
	    if (url.searchParams.get("m") == "self") {
	        alert("Error, Cannot Add Yourself!");
	    }
	    if (url.searchParams.get("m") == "already") {
	        alert("You Are Already Friends!");
	    }
	    if (url.searchParams.get("m") == "many") {
	        alert("You Already Sent A Friend Request!");
	    }
	    if (url.searchParams.get("m") == "friends") {
	        alert("You Are Now Friends!");
	    }
	    if (url.searchParams.get("m") == "sent") {
	        alert("Friend Request Sent!");
	    }

		//Edit profile Jquery
	    $(function () {
	        setInterval(function () {
	            seennow();
	        }, 3000);

	        function seennow() {
	            $.get("/api/Default.aspx?set=now");
	        }
		  var dialog, form,
			//Defining the inputted info
		    bio = $( "#bio" ),
		    website = $( "#website" ),
		    address = $( "#address" ),
		    description = $( "#description" ),
		    allFields = $( [] ).add( bio ).add( website ).add( address ).add( description),
		    tips = $( ".validateTips" );
		    var url_string = window.location.href
		    var url = new URL(url_string);
		    function updateTips( t ) {
		      tips
		        .text( t )
		        .addClass( "ui-state-highlight" );
		      setTimeout(function() {
		        tips.removeClass( "ui-state-highlight", 1500 );
		      }, 500 );
		    }

			errorDialog = $( "#error" ).dialog({
			  modal: true,
			  autoOpen: false,
			  buttons: {
			    Ok: function() {
			      $( this ).dialog( "close" );
			    }
			  }
			});

				
		    dialog = $( "#dialog-form" ).dialog({
			    autoOpen: false,
			    height: "auto",
			    width: "auto",
			    modal: true,
			    buttons: {
			      	"Finish": function(){
			      	    document.getElementById("formEditProfile").submit();
			      	},
			    	Cancel: function() {
			        	dialog.dialog( "close" );
			      	}
			    },
			    close: function() {
			      form[ 0 ].reset();
			      allFields.removeClass( "ui-state-error" );
			    }
		  	});
		
			form = dialog.find( "form" ).on( "submit", function( event ) {
				event.preventDefault();
				editProfile();
			});
		  
			if (url.searchParams.get("m") == "self") {
			    self.dialog("open");
			}
		  $( "#edit" ).button().on( "click", function() {
		    	dialog.dialog( "open" );
         });
		} );
	</script>
    <% } %>
</html>