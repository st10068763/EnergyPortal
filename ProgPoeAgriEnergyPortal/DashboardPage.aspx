<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DashboardPage.aspx.cs" Inherits="ProgPoeAgriEnergyPortal.DashboardPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Agri-Energy Portal Dashboard</title>
    <!-- Add modern CSS framework link here, like Bootstrap or Tailwind CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <link rel="stylesheet" href="~/CSS/mySheet.css" />
</head>
<body>
    <form id="form1" runat="server">
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
                         <a class="nav-link" href="LoginPage.aspx">Logout</a>
                     </li>     
                </ul>
            </div>
        </nav>
        
        <!-- Dashboard Header -->
        <div class="container mt-5">
            <div class="jumbotron">
                <h1 class="display-4">Welcome to the Agri-Energy Dashboard</h1>
                <p class="lead">Overview of all activities and metrics related to the Agri-Energy Portal.</p>
            </div>
            
            <!-- Summary Cards -->
            <div class="row text-center">
                <div class="col-md-3">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Total Farmers</h5>
                            <p class="card-text">150</p>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Total Products</h5>
                            <p class="card-text">500</p>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Total Transactions</h5>
                            <p class="card-text">1200</p>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Total Revenue</h5>
                            <p class="card-text">$250,000</p>
                        </div>
                    </div>
                </div>
            </div>
            
            <!-- Charts and Graphs -->
            <div class="mt-5">
                <h2>Performance Overview</h2>
                <canvas id="performanceChart"></canvas>
            </div>
            
            <!-- Recent Activities -->
            <div class="mt-5">
                <h2>Recent Activities</h2>
                <ul class="list-group">
                    <li class="list-group-item">Farmer John added a new product: Corn</li>
                    <li class="list-group-item">Transaction of $200 completed by Jane Doe</li>
                    <li class="list-group-item">New farmer profile created for Farmer Amy</li>
                </ul>
            </div>
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script>
        var ctx = document.getElementById('performanceChart').getContext('2d');
        var performanceChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
                datasets: [{
                    label: 'Transactions',
                    data: [12, 15, 18, 20, 22, 24, 26],
                    backgroundColor: 'rgba(0, 123, 255, 0.5)',
                    borderColor: 'rgba(0, 123, 255, 1)',
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
    </script>
</body>
</html>
