<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MarketPlace.aspx.cs" Inherits="ProgPoeAgriEnergyPortal.MarketPlace" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Green Energy Marketplace</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <link rel="stylesheet" href="~/CSS/mySheet.css" />
    <style>
        .jumbotron {
            background-color: #f8f9fa;
            padding: 80px 0;
        }
        .card {
            margin-bottom: 20px;
        }
        .product-img {
            max-height: 200px;
            object-fit: cover;
        }
        .btn-primary {
            background-color: #007bff;
            border-color: #007bff;
        }
        .btn-primary:hover {
            background-color: #0056b3;
            border-color: #0056b3;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Navbar -->
        <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
            <a class="navbar-brand" href="#">Agri-Energy Portal</a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav ml-auto">
                    <li class="nav-item">
                        <a class="nav-link" href="DashboardPage.aspx">Dashboard</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="FarmingHub.aspx">Farming Hub</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="TransactionsPage.aspx">Transactions</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="CommunicationHub.aspx">Communication Hub</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="LoginPage.aspx">Logout</a>
                    </li>
                </ul>
            </div>
        </nav>

        <!-- Green Energy Marketplace Content -->
        <div class="container mt-5">
            <div class="jumbotron text-center">
                <h1 class="display-4">Green Energy Marketplace</h1>
                <p class="lead">A marketplace for green energy solutions tailored to agricultural needs.</p>
            </div>

            <!-- Product Listings -->
            <div class="row" id="productList">
                <%--------------------_____________Repeater_____________----------------------%>
                    <asp:Repeater ID="ProductsRepeater" runat="server">
                            <ItemTemplate>
                                <div class="card mb-3">
                                    <div class="row no-gutters">
                                        <div class="col-md-4">
                                            <img src='<%# Eval("Product_Image") %>' class="card-img" alt="Product Image">
                                        </div>
                                        <div class="col-md-8">
                                            <div class="card-body">
                                                <h5 class="card-title"><%# Eval("ProductName") %></h5>
                                                <p class="card-text"><%# Eval("Description") %></p>
                                                <p class="card-text"><small class="text-muted">Category: <%# Eval("Category") %></small></p>
                                                <p class="card-text">Price: R<%# Eval("Product_Price") %></p>
                                                <p class="card-text">Stock available: <%# Eval("Quantity") %></p>
                                                <p class="card-text">Production Date: <%# Eval("ProductDate") %></p>
                                                <p class="card-text">Farmer Name: <%# Eval("FarmerName") %></p>
                                                <asp:Button ID="btnBuy" runat="server" Text="Buy" CommandArgument='<%# Eval("Product_ID") %>' OnClick="btnBuy_Click" CssClass="btn btn-primary" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                    </asp:Repeater>  
                <%--=======================End Repeat=================--%>
            </div>

            <!-------------------------------Repeater for the green products----------------------------------> 
            <div class="mt-5"> 
                <h2>Green Products</h2>                 
                <asp:Repeater ID="GreenRepeater" runat="server">
                    <ItemTemplate>
                        <div class="card mb-3">
                            <div class="row no-gutters">
                                <div class="col-md-4">
                                    <img src='<%# Eval("Product_Image") %>' class="card-img" alt="Product Image">
                                </div>
                                <div class="col-md-8">
                                    <div class="card-body">
                                        <h5 class="card-title"><%# Eval("Product_Name") %></h5>
                                        <p class="card-text">Stock available: <%# Eval("Quantity") %></p>
                                        <p class="card-text"><small class="text-muted">Category: <%# Eval("Category") %></small></p>
                                        <p class="card-text">Price: R<%# Eval("Product_Price") %></p>
                                        <p class="card-text"><%# Eval("Description") %></p>
                                        <p class="card-text">Farmer Name: <%# Eval("FarmerName") %></p>
                                        <asp:Button ID="btnBuyGreen" runat="server" Text="Buy" CommandArgument='<%# Eval("GreenMarket_ID") %>' OnClick="btnBuyGreen_Click" CssClass="btn btn-primary" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <!-- end of green products repeater -->
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
