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
            <a class="navbar-brand" href="DashboardPage.aspx">Agri-Energy Portal</a>
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

            <!-- Event Creation Form for Farmers -->
            <div class="row mb-4">
                <div class="col-md-6 offset-md-3">
                    <div class="card">
                        <div class="card-header">Create New Event</div>
                        <div class="card-body">
                            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="text-danger"></asp:Label>
                            <asp:Label ID="CorrectMessage" runat="server" Text="" CssClass="alert-success"></asp:Label>
                            <asp:TextBox ID="txtEventName" runat="server" CssClass="form-control" Placeholder="Event Name"></asp:TextBox><br />
                            <asp:TextBox ID="txtEventDescription" runat="server" CssClass="form-control" Placeholder="Event Description"></asp:TextBox><br />
                            <asp:DropDownList ID="ddlEventType" runat="server" CssClass="dropdown">
                                <asp:ListItem Text="Select Event Type" Value="" />
                                <asp:ListItem Text="Online Course" Value="Online Course" />
                                <asp:ListItem Text="Webinar" Value="Webinar" />
                                <asp:ListItem Text="Workshop" Value="Workshop" />
                            </asp:DropDownList><br />
                            <asp:TextBox ID="txtEventDate" runat="server" type="date" CssClass="form-control" Placeholder="Event Date (YYYY-MM-DD)"></asp:TextBox><br />
                            <asp:TextBox ID="txtEventImage" runat="server" CssClass="form-control" Placeholder="Event Image URL"></asp:TextBox><br />
                            <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" Placeholder="Enter the price for the event"></asp:TextBox><br />
                            <asp:TextBox ID="txtLectureName" runat="server" CssClass="form-control" Placeholder="Enter the lecture name here"></asp:TextBox><br />
                            <asp:Button ID="btnCreateEvent" runat="server" Text="Create Event" CssClass="btn btn-primary" OnClick="btnCreateEvent_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <!-- Educational Events Listings -->
            <div class="row" id="eventList">
                <asp:Repeater ID="rptEvents" runat="server">
                    <ItemTemplate>
                        <div class="col-md-4">
                            <div class="card">
                                
                                <img src='<%# Eval("Event_Image") %>' class="card-img-top" alt="Event Image" />
                                <div class="card-body">
                                    <h5 class="card-title"><%# Eval("Event_Name") %></h5>
                                    <p class="card-text"><%# Eval("Description") %></p>
                                    <p class="card-text"><small class="text-muted">Type: <%# Eval("Category") %></small></p>
                                    <p class="card-text"><small class="text-muted">Date: <%# Eval("EventDate") %></small></p>
                                    <p class="card-text"><small class="text-muted">Price: <%# Eval("Product_Price") %></small></p>
                                    <p class="card-text"><small class="text-muted">Lecturer: <%# Eval("FarmerName") %></small></p>
                                    <asp:Button ID="EnrollBtn" runat="server" Text="Enroll" CommandArgument='<%# Eval("Event_ID") %>' CssClass="btn btn-primary" OnClick="EnrollBtn_Click" />
                                    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="text-danger"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
