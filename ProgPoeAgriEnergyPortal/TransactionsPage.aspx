<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransactionsPage.aspx.cs" Inherits="ProgPoeAgriEnergyPortal.TransactionsPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Transaction - Agri-Energy Connect Portal</title>

    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <link rel="stylesheet" href="~/CSS/mySheet.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Navigation Bar -->
        <nav class="navbar navbar-expand-lg navbar-light bg-light">
            <a class="navbar-brand" href="DashboardPage.aspx">Agri-Energy Portal</a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav">
                    <li class="nav-item">
                        <a class="nav-link" href="DashboardPage.aspx">Dashboard</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="LoginPage.aspx">Logout</a>
                    </li>
                </ul>
            </div>
        </nav>

        <div class="container mt-5">
            <h1>Transaction Details</h1>
            <div class="card">
                <div class="card-body">
                    <h3 class="card-title" aria-orientation="horizontal">Product Information</h3>
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <label for="ProductName">Product Name</label>
                            <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label for="Category">Category</label>
                            <asp:TextBox ID="txtCategory" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <label for="ProductionDate">Production Date</label>
                            <asp:TextBox ID="txtProductionDate" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label for="FarmerName">Farmer Name</label>
                            <asp:TextBox ID="txtFarmerName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>

                    <h3 class="card-title mt-4">Buyer Information</h3>
                    <div class="form-group">
                        <label for="BuyerName">Buyer Name</label>
                        <asp:TextBox ID="txtBuyerName" runat="server" CssClass="form-control" placeholder="Enter your name"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="BuyerEmail">Email address</label>
                        <asp:TextBox ID="txtBuyerEmail" runat="server" CssClass="form-control" placeholder="Enter your email"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="BuyerAddress">Shipping Address</label>
                        <asp:TextBox ID="txtBuyerAddress" runat="server" CssClass="form-control" placeholder="Enter your address"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="CardNumber">Card Number</label>
                        <asp:TextBox ID="txtcardNumber" runat="server" CssClass="form-control" placeholder="Enter your visa card number"></asp:TextBox>
                    </div>

                     <div class="form-group">
                         <label for="CVV">Card CVV</label>
                         <asp:TextBox ID="txtCVV" runat="server" CssClass="form-control" TextMode="Password" placeholder="XXX"></asp:TextBox>
                     </div>
                    <asp:Button ID="btnConfirmPurchase" runat="server" CssClass="btn btn-primary btn-block" Text="Confirm Purchase" OnClick="btnConfirmPurchase_Click" />
                </div>
            </div>

            <!-- Displays the past transactions -->
            <div class="mt-5">
                <h2>Past Transactions</h2>
                <asp:Repeater ID="PastTransactionsRepeater" runat="server">
                    <HeaderTemplate>
                        <table class="table table-striped">
                            <thead>
                                <tr>
                                    <th>Transaction ID</th>
                                    <th>Product Name</th>
                                    <th>Category</th>
                                    <th>Production Date</th>
                                    <th>Buyer Name</th>
                                    <th>Transaction Date</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("TransactionID") %></td>
                            <td><%# Eval("ProductName") %></td>
                            <td><%# Eval("Category") %></td>
                            <td><%# Eval("ProductionDate", "{0:yyyy-MM-dd}") %></td>
                            <td><%# Eval("BuyerName") %></td>
                            <td><%# Eval("TransactionDate", "{0:yyyy-MM-dd HH:mm:ss}") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                            </tbody>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
    <!-- Bootstrap JS and dependencies -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.4/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
