<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectsAndFunds.aspx.cs" Inherits="ProgPoeAgriEnergyPortal.ProjectsAndFunds" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Projects and Funding Opportunities</title>
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
        .auto-style1 {
            display: block;
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: var(--bs-body-color);
            background-clip: padding-box;
            border-radius: var(--bs-border-radius);
            transition: none;
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
            box-shadow: var(--bs-box-shadow-inset);
            border: 1px solid #ced4da;
            background-color: #f0f5fa;
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
                        <a class="nav-link" href="EducationHub.aspx">Education Hub</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="LoginPage.aspx">Logout</a>
                    </li>
                </ul>
            </div>
        </nav>

        <!-- Projects and Funding Content -->
        <div class="container mt-5">
            <div class="jumbotron text-center">
                <h1 class="display-4">Projects and Funding Opportunities</h1>
                <p class="lead">Collaborate on projects and find funding opportunities in green agriculture.</p>
            </div>

            <!-- Add Project Section -->
            <div class="mb-5">
                <h2>Add New Project</h2>
                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" />
                <div class="card">
                    <div class="card-body">
                        <div class="form-group">
                            <label for="ProjectName">Project Name</label>
                            <asp:TextBox ID="txtProjectName" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <label for="ProjectDescription">Project Description</label>
                            <asp:TextBox ID="txtProjectDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                        </div>
                        <div class="form-group">
                            <label for="ProjectType">Project Type</label>
                            <asp:DropDownList ID="ddlProjectType" runat="server" CssClass="form-control">
                                <asp:ListItem>Green Energy</asp:ListItem>
                                <asp:ListItem>Sustainable Agriculture</asp:ListItem>
                                <asp:ListItem>Water Conservation</asp:ListItem>
                                <asp:ListItem>Soil Health</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <asp:Button ID="btnAddProject" runat="server" Text="Add Project" CssClass="btn btn-primary" OnClick="btnAddProject_Click" />
                    </div>
                </div>
            </div>

            <!-- Project Listings Section -->
            <div class="mb-5">
                <h2>Current Projects</h2>
                <div class="row" id="projectList">
                    <!-- Project Item -->
                    <%-- Repeat this block for each project --%>
                    <div class="col-md-4">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title">ProjectName</h5>
                                <p class="card-text">ProjectDescription</p>
                                <p class="card-text"><small class="text-muted">Type: ProjectType</small></p>
                                <a href="ProjectDetails.aspx?projectId=ProjectID" class="btn btn-primary">View Details</a>
                                <a href="JoinProject.aspx?projectId=ProjectID" class="btn btn-success">Join Project</a>
                            </div>
                        </div>
                    </div>
                    <%-- End Repeat --%>
                </div>
            </div>

            <!-- Grants and Funding Section -->
            <div class="mb-5">
                <h2>Grants and Funding Opportunities</h2>
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Grant Name</h5>
                        <p class="card-text">Grant Description</p>
                        <a href="GrantDetails.aspx?grantId=GrantID" class="btn btn-info">View Details</a>
                        <a href="FundProject.aspx?projectId=ProjectID" class="btn btn-warning">Fund Project</a>
                    </div>
                </div>
            </div>
        </div>
    </form>
     <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
     <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/dist/js/bootstrap.bundle.min.js"></script>
     <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
</body>
</html>
