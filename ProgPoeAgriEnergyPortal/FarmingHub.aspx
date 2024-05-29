<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FarmingHub.aspx.cs" Inherits="ProgPoeAgriEnergyPortal.FarmingHub" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Farming Hub</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <link rel="stylesheet" href="~/CSS/mySheet.css" />
    <style>
        /* Add your custom CSS styles here */
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
                    <li class="nav-item"><a class="nav-link" href="DashboardPage.aspx">Dashboard</a></li>
                    <li class="nav-item"><a class="nav-link" href="GreenEnergyMarket.aspx">Green Energy Market</a></li>
                    <li class="nav-item"><a class="nav-link" href="TransactionsPage.aspx">Transactions</a></li>
                    <li class="nav-item"><a class="nav-link" href="CommunicationHub.aspx">Communication Hub</a></li>
                    <li class="nav-item"><a class="nav-link" href="LoginPage.aspx">Logout</a></li>
                </ul>
            </div>
        </nav>

        <!-- Farming Hub Content -->
        <div class="container mt-5">
            <div class="jumbotron text-center">
                <h1 class="display-4">Welcome to the Farming Hub</h1>
                <p class="lead">A resource center for sharing best practices in sustainable farming.</p>
            </div>

            <!-- Create New Post Section -->
            <div class="row mt-4">
                <div class="col-md-8 mx-auto">
                    <div class="card">
                        <div class="card-header">Create New Post</div>
                        <div class="card-body">
                            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="text-danger"></asp:Label>
                            <asp:TextBox ID="txtPostTitle" runat="server" CssClass="form-control" Placeholder="Post Title"></asp:TextBox><br />
                            <asp:TextBox ID="txtPostContent" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" Placeholder="Post Content"></asp:TextBox><br />
                            <asp:Button ID="btnCreatePost" runat="server" Text="Create Post" CssClass="btn btn-primary" OnClick="btnCreatePost_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <!-- Latest Posts Section -->
            <div class="row mt-4">
                <div class="col-md-8 mx-auto">
                    <h2 class="text-center">Latest Posts</h2>
                    <asp:Repeater ID="PostsRepeater" runat="server">
                        <ItemTemplate>
                            <div class="forum-post">
                                <h3 class="post-title"><%# Eval("Title") %></h3>
                                <p class="post-content"><%# Eval("Content") %></p>
                                <p class="post-date">Created on: <%# Eval("DateCreated", "{0:MMMM dd, yyyy}") %></p>
                                <p class="post-reply"><%# Eval("ReplyCount") %> replies</p>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <a href="#" class="btn btn-primary btn-block mt-3">View More Posts</a>
                </div>
            </div>
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
