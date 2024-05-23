<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EducationHub.aspx.cs" Inherits="ProgPoeAgriEnergyPortal.EducationHub" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Education Hub</title>
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
                        <a class="nav-link" href="MarketPlace.aspx">MarketPlace</a>
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

        <!-- Education Hub Content -->
        <div class="container mt-5">
            <div class="jumbotron text-center">
                <h1 class="display-4">Education Hub</h1>
                <p class="lead">Explore and register for courses, webinars, and workshops.</p>
            </div>

            <!-- Educational Events Listings -->
            <div class="row" id="eventList">
                <!-- Course Item -->
                <%-- Repeat this block for each course, webinar, or workshop --%>
                <div class="col-md-4">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">EventName</h5>
                            <p class="card-text">EventDescription</p>
                            <p class="card-text"><small class="text-muted">Type: EventType</small></p>
                            <p class="card-text"><small class="text-muted">Date: EventDate</small></p>
                            <a href="EventDetails.aspx?eventId=EventID" class="btn btn-primary">View Details</a>
                            <a href="RegisterEvent.aspx?eventId=EventID" class="btn btn-success">Register</a>
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
