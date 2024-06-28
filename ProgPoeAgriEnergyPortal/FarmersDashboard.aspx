<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FarmersDashboard.aspx.cs" Inherits="ProgPoeAgriEnergyPortal.FarmersDashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Farmers Dashboard</title>
    <!-- Add modern CSS framework link here, like Bootstrap or Tailwind CSS -->
    <link rel="stylesheet" href="~/CSS/mySheet.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Navbar -->
        <nav class="navbar navbar-expand-lg navbar-light bg-light">
            <a class="navbar-brand" href="DashboardPage.aspx">Agri-Energy Portal</a>
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
                    <li class="nav-item">
                        <a class="nav-link" href="LoginPage.aspx">Logout</a>
                    </li>     
                </ul>
            </div>
        </nav>

        <div class="container mt-5">
            <h1>Welcome to the Farmers Dashboard</h1>
            
            <!-- Form for adding new product -->
            <div class="card mt-4">
                <div class="card-header">
                    Add New Product
                </div>
                <div class="card-body">
                    <div class="form-group">
                        <label for="productName">Product Name:</label>
                        <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" placeholder="Enter product name"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="category">Category:</label>
                        <asp:DropDownList ID="CategoryDL" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Vegetables" />
                            <asp:ListItem Text="Live stock" />
                            <asp:ListItem Text="Fertelizers" />
                            <asp:ListItem Text="Pesticides" />
                            <asp:ListItem Text="Seeds" />
                            <asp:ListItem Text="Fruits" />
                            <asp:ListItem Text="Flowers" />
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="productDescription">Product Description:</label>
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" placeholder="Enter description for your product."></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="Product_Price">Product Price per unit:</label>                         
                        <asp:TextBox ID="txtProductPrice" runat="server" CssClass="form-control" placeholder="Enter the product price per unit here"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="ProductQuantity">Product Quantity:</label>                         
                        <asp:TextBox ID="txtProductQuantity" runat="server" CssClass="form-control" placeholder="Enter the product quantity here"></asp:TextBox>
                    </div>                    
                    <div class="form-group">
                        <label for="productDate">Product Date:</label>
                        <input type="date" id="productionDate" class="form-control" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="FarmerName">Farmer Name:</label>
                        <asp:TextBox ID="txtFarmerName" runat="server" CssClass="form-control" placeholder="Enter Farmer name here"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="productImage">Product image:</label>
                        <asp:TextBox ID="txtProductImage" runat="server" CssClass="form-control" placeholder="Enter your image URL here"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnAddProduct" runat="server" CssClass="btn btn-primary btn-block" Text="Add Product" OnClick="btnAddProduct_Click" />
                </div>
            </div>

             <!-- Form to add green products -->
             <div class="card mt-4">
                 <div class="card-header">
                     Add New Green Product
                 </div>
                 <div class="card-body">
                     <div class="form-group">
                         <label for="productName">Product Name:</label>
                         <asp:TextBox ID="txtGreenProductName" runat="server" CssClass="form-control" placeholder="Enter the green product name"></asp:TextBox>
                     </div>
                     <div class="form-group">
                         <label for="category">Category:</label>
                         <asp:DropDownList ID="GreenProductDL" runat="server" CssClass="form-control">
                             <asp:ListItem Text="Solar power system" />
                             <asp:ListItem Text="Wind turbines" />
                             <asp:ListItem Text="Biogas energy" />                
                         </asp:DropDownList>
                     </div>
                     <div class="form-group">
                         <label for="productDescription">Product Description:</label>
                         <asp:TextBox ID="GreenProductDescription" runat="server" CssClass="form-control" placeholder="Enter description for your green product."></asp:TextBox>
                     </div>
                     <div class="form-group">
                         <label for="Product_Price">Product Price:</label>                         
                         <asp:TextBox ID="GreenProduct_Price" runat="server" CssClass="form-control" placeholder="Enter the product price per unit here"></asp:TextBox>
                     </div>
                     <div class="form-group">
                         <label for="ProductQuantity">Product Quantity:</label>                         
                         <asp:TextBox ID="GreenProductQuantity" runat="server" CssClass="form-control" placeholder="Enter the product quantity here"></asp:TextBox>
                     </div>                    
                     <div class="form-group">
                         <label for="FarmerName">Farmer Name:</label>
                         <asp:TextBox ID="GreenFarmerName" runat="server" CssClass="form-control" placeholder="Enter Farmer name here"></asp:TextBox>
                     </div>
                     <div class="form-group">
                         <label for="productImage">Product image:</label>
                         <asp:TextBox ID="GreenproductImage" runat="server" CssClass="form-control" placeholder="Enter your image URL here"></asp:TextBox>
                     </div>         
                     <asp:Button ID="btnAddGreenProduct" runat="server" CssClass="btn btn-primary btn-block" Text="Add Product" OnClick="btnAddGreenProduct_Click" />
                 </div>
             </div> 

            <!-- Repeater for all the products entered by a particular farmer -->
            <div class="mt-5">
                <h2>My Products</h2>
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
                                        <p class="card-text">Farmer ID: <%# Eval("FarmerID") %></p>                                    
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>                   
            </div>

            <!-- Repeater for the green products -->
            <div class="mt-5"> 
                <h2>Green Products</h2>                 
                <asp:Repeater ID="GreenRepeater" runat="server">
                    <ItemTemplate>
                        <div class="card mb-3">
                            <div class="row no-gutters">
                                <div class="col-md-4">
                                    <img src='<%# Eval("Product_Image") %>' class="card-img" alt="Product Image">
                                </div>
                                <div class="col-md-8">
                                    <div class="card-body">
                                        <h5 class="card-title"><%# Eval("Product_Name") %></h5>
                                        <p class="card-text">Stock available: <%# Eval("Quantity") %></p>
                                        <p class="card-text"><small class="text-muted">Category: <%# Eval("Category") %></small></p>
                                        <p class="card-text">Price: R<%# Eval("Product_Price") %></p>
                                        <p class="card-text"><%# Eval("Description") %></p>
                                        <p class="card-text">Farmer Name: <%# Eval("FarmerName") %></p>                                                
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>                     
        </div>
    </form>

    <!-- Scripts -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</body>
</html>
