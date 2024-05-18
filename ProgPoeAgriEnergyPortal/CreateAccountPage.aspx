<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateAccountPage.aspx.cs" Inherits="ProgPoeAgriEnergyPortal.CreateAccountPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Create Account - Agri-Energy Connect Portal</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f8f9fa;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }
        .create-account-container {
            background: #fff;
            padding: 2rem;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        .create-account-container h2 {
            margin-bottom: 1rem;
            text-align: center;
        }
    </style>
</head>
<body>
    <!-----------------------------------------------Form code---------------------------------------------------------->
    <form id="form1" runat="server">
        <div class="create-account-container">
            <h2>Create Account</h2>
            <div class="form-group">
                <label for="name">Full Name</label>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Enter full name"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="email">Email address</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter email"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="phone">Phone Number</label>
                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" placeholder="Enter phone number"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="password">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Password"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="role">Role</label>
                <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Farmer" Value="Farmer"></asp:ListItem>
                    <asp:ListItem Text="Employee" Value="Employee"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <asp:Button ID="btnCreateAccount" runat="server" CssClass="btn btn-primary btn-block" Text="Create Account" OnClick="btnCreateAccount_Click" />
        </div>
    </form>
    <!-----------------------------------------------Form code---------------------------------------------------------->
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
</body>
</html>
