<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FarmingHub.aspx.cs" Inherits="ProgPoeAgriEnergyPortal.FarmingHub" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Farming Hub</title>
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
        .forum-post {
            background-color: #f8f9fa;
            border: 1px solid #dee2e6;
            padding: 15px;
            border-radius: 5px;
        }
        .forum-post .post-title {
            font-weight: bold;
            color: #007bff;
        }
        .forum-post .post-content {
            margin-top: 10px;
        }
        .post-reply {
            margin-left: 30px;
            margin-top: 10px;
            color: #6c757d;
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
                        <a class="nav-link" href="GreenEnergyMarket.aspx">Green Energy Market</a>
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

        <!-- Farming Hub Content -->
        <div class="container mt-5">
            <div class="jumbotron text-center">
                <h1 class="display-4">Welcome to the Farming Hub</h1>
                <p class="lead">A resource center for sharing best practices in sustainable farming.</p>
            </div>

            <!-- Forum Section -->
            <div class="row">
                <div class="col-md-8 mx-auto">
                    <div class="card">
                        <div class="card-body">
                            <h2 class="card-title">Interactive Forums</h2>
                            <p class="card-text">Join the discussion on sustainable farming techniques, water conservation, and soil health maintenance.</p>
                            <a href="#" class="btn btn-primary">Explore Forums</a>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Latest Posts Section -->
            <div class="row mt-4">
                <div class="col-md-8 mx-auto">
                    <h2 class="text-center">Latest Posts</h2>
                    <!-- Sample Forum Post -->
                    <div class="forum-post">
                        <h3 class="post-title">Organic Farming Techniques</h3>
                        <p class="post-content">Learn about the benefits of organic farming and share your experiences.</p>
                        <p class="post-reply">12 replies</p>
                    </div>
                    <!-- Sample Forum Post -->
                    <div class="forum-post">
                        <h3 class="post-title">Water Conservation Methods</h3>
                        <p class="post-content">Discuss innovative ways to conserve water in agriculture.</p>
                        <p class="post-reply">8 replies</p>
                    </div>
                    <!-- Sample Forum Post -->
                    <div class="forum-post">
                        <h3 class="post-title">Soil Health Maintenance</h3>
                        <p class="post-content">Share tips on maintaining soil health for sustainable farming.</p>
                        <p class="post-reply">5 replies</p>
                    </div>
                    <a href="#" class="btn btn-primary btn-block mt-3">View More Posts</a>
                </div>
            </div>
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
