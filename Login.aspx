<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<html>
    <head>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
        <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
        <!-- Latest compiled and minified CSS -->
        <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
        <link rel="stylesheet" href="CSS/login.css">
        <script src="https://cdn.rawgit.com/PascaleBeier/bootstrap-validate/v2.2.0/dist/bootstrap-validate.js" ></script>
            <title>KatomBook - World's Leading Social Network</title>
            <meta charset="utf-8">
                <meta name="viewport" content="width=device-width, initial-scale=1">
                </head>
                <body>
                    <div class="login-page">
                        <div class="form">
                            <!-- Register Form -->
                            <div id="register" class="register">
                                <form action="/api/Default.aspx" method="post" name="registerForm" class="register-form" onsubmit="return isValid();">
                                    <div>
                                        <input type="text" placeholder="Username" id="username_register" name="username_register" class="form-control" required/>
                                    </div>
                                    <div>
                                        <input type="text" placeholder="Full Name" id="name_register" name="name_register" class="form-control" required/>
                                    </div>
                                    <div>
                                        <input type="text" placeholder="email address" id="email_register" name="email_register" class="form-control" required/>
                                    </div>
                                    <div>
                                        <input type="password" placeholder="password" id="password_register" name="password_register" class="form-control" required/>
                                    </div>
                                    <div>
                                        <input type="password" placeholder="re-type password" id="password_register_rt" name="password_register_rt" class="form-control" required/>
                                    </div>
                                    <input type="text" name="action" value="register" hidden/>
                                    <!--<center><div class="g-recaptcha" data-sitekey="6LfQu3sUAAAAAIyUvvWnGxcH8R_BXHYzb4Bz-06k"></div></center>-->
                                    <button type="submit" onclick="return isValid();">create</button>
                                    <p class="message">Already registered? 
                                        <a href="#">Sign In</a>
                                    </p>
                                </form>
                            </div>
                                <!-- Login Form -->
                                <div id="logn" class="login">
                                    <form action="/api/Default.aspx" id="login_form" method="post" class="login-form">
                                        <input type="text" placeholder="username" id="username" name="username" required/>
                                        <input type="password" placeholder="password" id="password" name="password"required/>
                                        <input type="text" name="action" value="login" hidden/>
                                        <button type="submit">login</button>
                                        <p class="message1">Not registered? 
                                            <a href="#">Create an account</a>
                                        </p>
                                    </form>
                                </div>
                            </div>
                        </div>
                        <script>
                            var url_string = window.location.href
                            var url = new URL(url_string);
                            if (url.searchParams.get("m") == "found") {
                                alert("Wrong Username Or Password.");
                            }
            $('.message1 a').click(function(){
               $('.login-form').hide();
               $('.register-form').animate({ height: "toggle", opacity: "toggle" }, "slow");
            });//when a create account is clicked on the login form
            
            $('.message a').click(function(){
                //sign in
                $('.register-form').hide();
                $('.login-form').animate({height: "toggle", opacity: "toggle"}, "slow");
            });//when sign in is clicked on the register formn

            bootstrapValidate(["#username_register", "#name_register"], "min:7:Please Enter At Least 7 Characters!");

            bootstrapValidate("#email_register", "email:Please Enter A Valid Email Address");
            bootstrapValidate("#password_register", "min:8:Password Must Be More Than 8 Characters Long!");
            bootstrapValidate("#password_register_rt", "matches:#password_register:Passwords Must Match!");
            function validateEmail(email) {
                var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                return re.test(String(email).toLowerCase());
            }
            function containsNumbers(str){
                for(var i = 0; i < str.length; i++){
                    if(!isNaN(str[i])){
                        return true;
                    }
                }
                return false;
            }
            function isValid() {
                var username = document.getElementById("username_register");
                var name = document.getElementById("name_register");
                var email = document.getElementById("email_register");
                var password = document.getElementById("password_register");
                var password_rt = document.getElementById("password_register_rt");
                var valid = true;
                if (username.length > 7 || username.length > 32) {
                    valid = false;
                }
                if (name.length < 8) {
                    valid = false;
                }
                if(containsNumbers(username.value)){
                    valid = false;
                }
                if (!validateEmail(email.value)) {
                    valid = false;
                }
                if (password.length < 8 || !containsNumbers(password.value)) {
                    valid = false;
                }
                if (password.value != password_rt.value) {
                    valid = false;
                }
                return valid;
            }
        </script>
    </body>
</html>