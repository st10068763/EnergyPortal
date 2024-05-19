<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="ProgPoeAgriEnergyPortal.LoginPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Login - Agri-Energy Connect Portal</title>

   <%-- <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" />--%>

    <link rel="stylesheet" href="~/CSS/mySheet.css"/>

    <style>
        body {
            background-color:gray;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }
        .login-container {
            background: #fff;
            padding: 2rem;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        .login-container h2 {
            margin-bottom: 1rem;
            text-align: center;
        }
        .loginBT {
            -ms-align-content: center;
            -webkit-align-content: center;
            align-content: center;
        }
    </style>
</head>
<body>
    <!-------------------------------------------------Login form------------------------------------------------------------>
    <form id="form1" runat="server">
        <div class="login-container">
            <h2>Login</h2>
            <div class="form-group">
                <label for="email">Email address</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter email"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="password">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter your password"></asp:TextBox>
            </div>
            <!--Open div-->
            <div> </div>
            <div class="loginBT">
                <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary btn-block" Text="Login" OnClick="btnLogin_Click" style="left: 0px; top: 0px; width: 83px" />
            </div>

            <div class="form-group">
                <a href="CreateAccountPage.aspx">Create an account</a>            
        </div>
    </form>
    <!-------------------------------------------------Login form------------------------------------------------------------>

    <!-------------------------------------------------Scripts------------------------------------------------------------>
  
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>

</body>
</html>
