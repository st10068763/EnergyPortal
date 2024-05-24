<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DashboardPage.aspx.cs" Inherits="ProgPoeAgriEnergyPortal.DashboardPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Agri-Energy Portal Dashboard</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <link rel="stylesheet" href="~/CSS/mySheet.css" />
    <style>
        body {
            display: flex;
        }
        .sidebar {
            height: 100vh;
            width: 250px;
            position: fixed;
            top: 0;
            left: 0;
            background-color: #f8f9fa;
            padding-top: 20px;
            transition: transform 0.3s ease;
        }
        .sidebar a {
            padding: 10px 15px;
            text-decoration: none;
            font-size: 18px;
            color: #343a40;
            display: block;
        }
        .sidebar a:hover {
            background-color: #ddd;
            color: #343a40;
        }
        .sidebar-hidden {
            transform: translateX(-100%);
        }
        .content {
            margin-left: 260px;
            padding: 20px;
            transition: margin-left 0.3s ease;
        }
        .content-full {
            margin-left: 10px;
        }
        .toggle-btn {
            position: fixed;
            top: 15px;
            left: 15px;
            z-index: 100;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Toggle Button -->
        <button type="button" class="btn btn-primary toggle-btn" id="toggleSidebar" aria-dropeffect="popup" aria-hidden="True">></button>

        <!-- Side Navigation Bar -->
        <div class="sidebar" id="sidebar">
            <a href="#">Dashboard</a>
            <a href="FarmHub.aspx">Farm Hub</a>
            <a href="GreenEnergyMarket.aspx">Green Energy Market</a>
            <a href="Education.aspx">Education</a>
            <a href="Funding.aspx">Funding</a>
            <a href="Collaboration.aspx">Collaboration</a>
        </div>

        <div class="content" id="content">
            <!-- Navigation Bar -->
            <nav class="navbar navbar-expand-lg navbar-light bg-light">
                <a class="navbar-brand" href="#">Agri-Energy Portal</a>
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav ml-auto">
                        <li class="nav-item">
                            <a class="nav-link" href="TransactionsPage.aspx">Transactions</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="CommunicationHub.aspx">Chat</a>
                        </li>
                        <!-------------------------------------->
                        <li class="nav-item">
                                <a class="nav-link" href="FarmingHub.aspx">Farming Hub</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="MarketPlace.aspx">Green Market</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="EducationHub.aspx">Education Hub</a>
                            </li>
                        <li class="nav-item">
                                <a class="nav-link" href="ProjectsAndFunds.aspx">Projects & Funds</a>
                            </li>
                        <li class="nav-item">
                            <a class="nav-link" href="LoginPage.aspx">Logout</a>
                        </li>
                    </ul>
                </div>
            </nav>
            </div>
            <div class="container mt-5">
                <div class="jumbotron">
                    <h1 class="display-4">Welcome to the Agri-Energy Dashboard</h1>
                    <p class="lead">Overview of all activities and metrics related to the Agri-Energy Portal.</p>
                </div>
                <!-- Display the total number of farmers, products, transactions and revenue -->
                <div class="row text-center">
                    <div class="col-md-3">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title">Total Farmers</h5>
                                <asp:TextBox ID="txtTotalFarmers" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title">Total Products</h5>
                                <asp:TextBox ID="txtTotalProducts" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="mt-5">
                    <h2>Performance Overview</h2>
                    <canvas id="performanceChart"></canvas>
                </div>

                <div class="mt-5">
                    <h2>Products Bought per Month</h2>
                    <canvas id="productsBoughtChart"></canvas>
                </div>

                <div class="mt-5">
                    <h2>Recent Activities</h2>
                    <ul class="list-group">
                        <li class="list-group-item">Farmer John added a new product: Corn</li>
                        <li class="list-group-item">Transaction of $200 completed by Jane Doe</li>
                        <li class="list-group-item">New farmer profile created for Farmer Amy</li>
                    </ul>
                </div>

                <div class="mt-5">
                    <h2>Available Products</h2>
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
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script>
        $(document).ready(function () {
            // Toggle sidebar
            $('#toggleSidebar').click(function () {
                $('#sidebar').toggleClass('sidebar-hidden');
                $('#content').toggleClass('content-full');
            });

            // Fetch performance data dynamically from the server
            $.ajax({
                type: "GET",
                url: "DashboardPage.aspx/GetProductsBoughtData",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var ctx = document.getElementById('productsBoughtChart').getContext('2d');
                    var productsBoughtChart = new Chart(ctx, {
                        type: 'bar',
                        data: {
                            labels: data.d.labels,
                            datasets: [{
                                label: 'Products Bought',
                                data: data.d.productsBought,
                                backgroundColor: 'rgba(75, 192, 192, 0.5)',
                                borderColor: 'rgba(75, 192, 192, 1)',
                                borderWidth: 1
                            }]
                        },
                        options: {
                            responsive: true,
                            maintainAspectRatio: true,
                            scales: {
                                y: {
                                    beginAtZero: true
                                }
                            }
                        }
                    });
                }
            });
        });
    </script>
</body>
</html>
