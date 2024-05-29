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
                <div class="card">
                    <div class="card-body">
                        <div>
                             <!-- Message for error-->
                            <asp:Label ID="lblErrorMessage" runat="server" CssClass="text-danger" />
                        </div>
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
                            <asp:DropDownList ID="ddlProjectType" runat="server" CssClass="form-select">
                                <asp:ListItem>Green Energy</asp:ListItem>
                                <asp:ListItem>Sustainable Agriculture</asp:ListItem>
                                <asp:ListItem>Water Conservation</asp:ListItem>
                                <asp:ListItem>Soil Health</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="StartDate">Project Start Date</label>
                            <asp:TextBox ID = "txtStartDate" runat = "server" CssClass = "form-control" TextMode = "Date" />
                        </div>
                        <div class="form-group">
                            <label for="EndDate">Project End Date</label>
                            <asp:TextBox ID = "txtEndDate" runat = "server" CssClass = "form-control" TextMode = "Date" />
                        </div>  
                        <div class="form-group">
                            <label for="ProjectLeaderName">Project Leader Name</label>
                            <asp:TextBox ID="txtProjectLeaderName" runat="server" CssClass="form-control" placeholder="Enter the name of the project leader." />
                            <div>
                                 <!--- Message for success-->
                                 <asp:Label ID="lblSuccessMessage" runat="server" CssClass="text-success" />
                            </div>
                        </div>
                        <asp:Button ID="btnAddProject" runat="server" Text="Add Project" CssClass="btn btn-primary" OnClick="btnAddProject_Click" />
                    </div>
                </div>
            </div>

            <!-- Project Repeater -->
               <div class="mt-5">
                    <h2>Available Projects</h2>
                    <asp:Repeater ID="ProductsRepeater" runat="server">
                        <ItemTemplate>
                            <div class="card mb-3">
                                <div class="row no-gutters">                                    
                                    <div class="col-md-8">
                                        <div class="card-body">
                                            <h5 class="card-title"><%# Eval("ProjectName") %></h5>
                                            <p class="card-text"><%# Eval("Description") %></p>
                                            <p class="card-text"><small class="text-muted">Category: <%# Eval("ProjectType") %></small></p>
                                            <p class="card-text">Start Date: <%# Eval("StartDate") %></p>
                                            <p class="card-text">End Date: <%# Eval("EndDate") %></p>
                                            <p class="card-text">Project Leader: <%# Eval("ProjectLeaderName") %></p>
                                            <asp:Button ID="btnJoin" runat="server" Text="Join Project" CommandArgument='<%# Eval("ProjectID") %>' OnClick="btnJoinProject_Click" CssClass="btn btn-primary" />
                                            <asp:Button ID="btnFund" runat="server" Text="Fund Project" CommandArgument='<%# Eval("Project ID") %>' OnClick="btnFundProject_Click" CssClass="btn btn-primary" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>                   
            </div>
            <!-- Grants and Funding Repeater -->
                <div class="mb-5">
                    <h2>Grants and Funding Opportunities</h2>
                    <asp:Repeater ID="GrantsRepeater" runat="server">
                        <ItemTemplate>
                            <div class="card">
                                <div class="card-body">
                                    <h5 class="card-title"><%# Eval("GrantName") %></h5>
                                    <p class="card-text"><%# Eval("GrantDescription") %></p> 
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
</body>
</html>
