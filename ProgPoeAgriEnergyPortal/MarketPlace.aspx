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

            <!-- Add Product Button -->
            <div class="text-center mb-4">
                <a href="AddProduct.aspx" class="btn btn-primary">Add New Product</a>
            </div>

            <!-- Product Listings -->
            <div class="row" id="productList">
                <!-- Product Item -->
                <%-- Repeat this block for each product --%>
                <div class="col-md-4">
                    <div class="card">
                        <img src="ProductImageURL" class="card-img-top product-img" alt="Product Image"/>
                        <div class="card-body">
                            <h5 class="card-title">ProductName</h5>
                            <p class="card-text">ProductDescription</p>
                            <p class="card-text"><small class="text-muted">Category: ProductCategory</small></p>
                            <p class="card-text"><strong>Price: RProductPrice</strong></p>
                            <a href="ProductDetails.aspx?productId=ProductID" class="btn btn-primary">View Details</a>
                            <a href="BuyProduct.aspx?productId=ProductID" class="btn btn-success">Buy</a>
                        </div>
                    </div>
                </div>
                <%-- End Repeat --%>
            </div>
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
