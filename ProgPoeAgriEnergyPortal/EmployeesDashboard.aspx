<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeesDashboard.aspx.cs" Inherits="ProgPoeAgriEnergyPortal.EmployeesDashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employees Dashboard</title>
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <link rel="stylesheet" href="~/CSS/mySheet.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Navigation Bar -->
        <nav class="navbar navbar-expand-lg navbar-light bg-light">
            <a class="navbar-brand" href="DashboardPage.aspx">Agri-Energy Portal</a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav">
                    <li class="nav-item">
                        <a class="nav-link" href="DashboardPage.aspx">Dashboard</a>
                    </li>
                    <li class="nav-item">
                       <a class="nav-link" href="CommunicationHub.aspx">Chat</a>
                   </li> 
                    <li class="nav-item">
                        <a class="nav-link" href="TransactionsPage.aspx">Transactions</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="LoginPage.aspx">Logout</a>
                    </li>
                </ul>
            </div>
        </nav>

        <div class="container mt-5">
            <h1>Welcome to the Employees Dashboard</h1>
            
            <!------------------------------------- Add farmer form ---------------------------------------------->
            <div class="section mb-4">
                <h2>Add New Farmer</h2>
                <div class="form-group">
                    <label for="txtFarmerName">Farmer Name</label>
                    <asp:TextBox ID="txtFarmerName" runat="server" CssClass="form-control" placeholder="Farmer Name"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtFarmerContact">Contact</label>
                    <asp:TextBox ID="txtFarmerContact" runat="server" CssClass="form-control" placeholder="Contact"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtFarmerLocation">Location</label>
                    <asp:TextBox ID="txtFarmerLocation" runat="server" CssClass="form-control" placeholder="Location"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtFarmerEmail">Email</label>
                    <asp:TextBox ID="txtFarmerEmail" runat="server" CssClass="form-control" placeholder="Enter farmer email"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtpassword">Password</label>
                    <asp:TextBox ID="txtFarmerPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter farmer password"></asp:TextBox>
                </div>
                <asp:Button ID="btnAddFarmer" runat="server" Text="Add Farmer" CssClass="btn btn-primary" OnClick="btnAddFarmer_Click" />
            </div>
            <!--Product table binds the data from the database to the employee table -->
            <div class="section mb-4">
                <h2>Search Products</h2>
                <div class="form-group">
                    <label for="txtSearchProduct">Search Product</label>
                    <asp:TextBox ID="txtSearchProduct" runat="server" CssClass="form-control" placeholder="Search Product..."></asp:TextBox>
                </div>
                <asp:Button ID="btnSearchProduct" runat="server" Text="Search" CssClass="btn btn-primary mb-3" OnClick="btnSearchProduct_Click" />
                <asp:GridView ID="GridViewProducts" runat="server" AutoGenerateColumns="false" CssClass="table table-striped">
                    <Columns>
                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                        <asp:BoundField DataField="Quantity" HeaderText="Stock Available" />
                        <asp:BoundField DataField="Category" HeaderText="Category" />
                        <asp:BoundField DataField="Product_Price" HeaderText="Price" />
                        <asp:BoundField DataField="Product_Image" HeaderText="Product Image" />
                        <asp:BoundField DataField="Description" HeaderText="Product Description" />
                    </Columns>
                </asp:GridView>
            </div>

            <div class="section">
                <h2>Search Farmers</h2>
                <div class="form-group">
                    <label for="txtSearchFarmer">Search Farmer</label>
                    <asp:TextBox ID="txtSearchFarmer" runat="server" CssClass="form-control" placeholder="Search Farmer..."></asp:TextBox>
                </div>
                <asp:Button ID="btnSearchFarmer" runat="server" Text="Search" CssClass="btn btn-primary mb-3" OnClick="btnSearchFarmer_Click" />
                <asp:GridView ID="GridViewFarmers" runat="server" AutoGenerateColumns="false" CssClass="table table-striped">
                    <Columns>
                        <asp:BoundField DataField="FarmerName" HeaderText="Farmer Name" />
                        <asp:BoundField DataField="CellphoneNumber" HeaderText="Contact" />
                        <asp:BoundField DataField="Location" HeaderText="Location" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
    <!-- Bootstrap JS and dependencies -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.4/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
