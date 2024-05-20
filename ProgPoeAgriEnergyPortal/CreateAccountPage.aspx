<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateAccountPage.aspx.cs" Inherits="ProgPoeAgriEnergyPortal.CreateAccountPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Create Account - Agri-Energy Connect Portal</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <link rel="stylesheet" href="~/CSS/mySheet.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <div class="row justify-content-center">
                <div class="col-md-6">
                    <div class="card">
                        <div class="card-header text-center">
                            <h2>Create Account</h2>
                        </div>
                        <div class="card-body">
                            <div class="form-group">
                                <label for="txtName">Full Name</label>
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Enter full name"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtEmail">Email address</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter email"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtPhone">Phone Number</label>
                                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" placeholder="Enter phone number"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtPassword">Password</label>
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Password"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="ddlRole">Role</label>
                                <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Farmer" Value="Farmer"></asp:ListItem>
                                    <asp:ListItem Text="Employee" Value="Employee"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <asp:Button ID="btnCreateAccount" runat="server" CssClass="btn btn-primary btn-block" Text="Create Account" OnClick="btnCreateAccount_Click" style="left: 0px; top: 0px" />
                        </div>
                        <!------------------------- link to login page -------------------------->
                        <div class="card-footer text-center">
                            <a href="LoginPage.aspx">Already have an account? Login</a>
                          </div>

                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.4/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
