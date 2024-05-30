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
            
            <!-- Add farmer form -->
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
            
            <!-- Add Grant form -->
            <div class="section m-4">
                <h2>Add New Grant</h2>
                <!-- Error Message -->
                <div>
                    <asp:Label ID="GrantErrorMessage" runat="server" CssClass="text-danger" />
                </div>
                <!-- Success Message -->
                <div>
                    <asp:Label ID="GrantSuccessMessage" runat="server" CssClass="text-success" />
                </div>
                <div class="form-group">
                    <label for="txtGrantName">Grant Name</label>
                    <asp:TextBox ID="txtGrantName" runat="server" CssClass="form-control" placeholder="Grant Name"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtGrantDescription">Grant description</label>
                    <asp:TextBox ID="txtGrantDescription" runat="server" CssClass="form-control" placeholder="Grant Description"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="grantGroup">Grant group</label>
                    <asp:DropDownList ID="ddlGrantGroup" runat="server" CssClass="form-control">
                        <asp:ListItem Value="1">Farmers</asp:ListItem>
                        <asp:ListItem Value="2">Employees</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <asp:Button ID="btnAddGrant" runat="server" Text="Add Grant" CssClass="btn-outline-primary" OnClick="btnAddGrant_Click" />
            </div>

            <!-- Product filter section -->
            <div class="section mb-4">
                <h2>Search Products</h2>
                <div class="form-group">
                    <label for="txtSearchProduct">Search Product</label>
                    <asp:TextBox ID="txtSearchProduct" runat="server" CssClass="form-control" placeholder="Search Product..."></asp:TextBox>
                </div>
                <asp:Button ID="btnSearchProduct" runat="server" Text="Search" CssClass="btn btn-primary mb-3" OnClick="btnSearchProduct_Click" />
                <!--Drop down list values-->
                <div class="form-group">
                    <label for="ddlSortOptions">Sort Search By</label>
                    <asp:DropDownList ID="ddlSortOptions" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSortOptions_SelectedIndexChanged" CssClass="form-control">
                        <asp:ListItem Value="ProductName ASC">Product Name (A-Z)</asp:ListItem>
                        <asp:ListItem Value="ProductName DESC">Product Name (Z-A)</asp:ListItem>
                        <asp:ListItem Value="Product_Price ASC">Price (Lowest to Highest)</asp:ListItem>
                        <asp:ListItem Value="Product_Price DESC">Price (Highest to Lowest)</asp:ListItem>
                        <asp:ListItem Value="ProductDate ASC">Creation Date (Oldest to Newest)</asp:ListItem>
                        <asp:ListItem Value="ProductDate DESC">Creation Date (Newest to Oldest)</asp:ListItem>
                        <asp:ListItem Value="FarmerName ASC">Farmer Name (A-Z)</asp:ListItem>
                        <asp:ListItem Value="FarmerName DESC">Farmer Name (Z-A)</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <asp:Repeater ID="RepeaterProducts" runat="server">
                    <HeaderTemplate>
                        <table class="table table-striped">
                            <thead>
                                <tr>
                                    <th>Product Name</th>
                                    <th>Quantity</th>
                                    <th>Category</th>
                                    <th>Price</th>
                                    <th>Image</th>
                                    <th>Description</th>
                                    <th>Creation Date</th>
                                    <th>Farmer Name</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("ProductName") %></td>
                            <td><%# Eval("Quantity") %></td>
                            <td><%# Eval("Category") %></td>
                            <td><%# Eval("Product_Price") %></td>
                            <td><%# Eval("Product_Image") %></td>
                            <td><%# Eval("Description") %></td>
                            <td><%# Eval("ProductDate", "{0:yyyy-MM-dd}") %></td>
                            <td><%# Eval("FarmerName") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                            </tbody>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
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
