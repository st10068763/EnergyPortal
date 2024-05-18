<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FarmersDashboard.aspx.cs" Inherits="ProgPoeAgriEnergyPortal.FarmersDashboard" %>

<!DOCTYPE html>



<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Farmers Dashboard</title>




    <!-- Add modern CSS framework link here, like Bootstrap or Tailwind CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" integrity="sha384-JcKb8q3iqJ61gNV9KGb8thSsNjpSL0n8PARn9HuZOnIxN0hoP+VmmDGMN5t9UJ0Z" crossorigin="anonymous">
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <h1>Welcome to the Farmers Dashboard</h1>
            
            <!-- Add form for adding new product -->
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
                         <label for="ProductQuantity">Product Quantity:</label>                         
                         <asp:TextBox ID="txtProductQuantity" runat="server" CssClass="form-control" placeholder="Enter the product quantity here"></asp:TextBox>
                         </div>
                    <div class="form-group">
                        <label for="category">Category:</label>                       
                    </div>
                    <div class="form-group">
                         <asp:DropDownList runat="server">
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
                        <label for="productionDate">Production Date:</label>
                        <input type="date" id="productionDate" class="form-control" runat="server" />
                    </div>
                    <asp:Button ID="btnAddProduct" runat="server" CssClass="btn btn-primary btn-block" Text="Add Product" OnClick="btnAddProduct_Click" />
                </div>
            </div>
           
            <!-- Display existing products in a grid view -->
            <div class="card mt-5">
                <div class="card-header">
                    Products List
                </div>
                <div class="card-body">
                    <asp:GridView ID="GridViewProducts" runat="server" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                            <asp:BoundField DataField="Category" HeaderText="Category" />
                            <asp:BoundField DataField="ProductionDate" HeaderText="Production Date" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

<script>   
</script>
