<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeesDashboard.aspx.cs" Inherits="ProgPoeAgriEnergyPortal.EmployeesDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employees Dashboard</title>
    <!-- Add modern CSS framework link here, like Bootstrap or Tailwind CSS -->
    <link rel="stylesheet" href="~/CSS/mySheet.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <h1>Welcome to the Employees Dashboard</h1>
            
            <!------------------------------------- Add farmer form ---------------------------------------------->
            <div class="section">
                <h2>Add New Farmer</h2>
                <asp:TextBox ID="txtFarmerName" runat="server" CssClass="form-control" placeholder="Farmer Name"></asp:TextBox>
                <asp:TextBox ID="txtFarmerContact" runat="server" CssClass="form-control" placeholder="Contact"></asp:TextBox>
                <asp:TextBox ID="txtFarmerLocation" runat="server" CssClass="form-control" placeholder="Location"></asp:TextBox>
                <asp:TextBox ID="txtFarmerEmail" runat="server" CssClass="form-control" placeholder="Enter farmer email"></asp:TextBox>
                 <asp:TextBox ID="txtpassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter farmer password"></asp:TextBox>
                <asp:Button ID="btnAddFarmer" runat="server" Text="Add Farmer" CssClass="btn btn-primary" OnClick="btnAddFarmer_Click" style="left: 0px; top: 0px" />
            </div>

            <div class="section">
                <h2>Search Products</h2>
                <asp:TextBox ID="txtSearchProduct" runat="server" CssClass="form-control" placeholder="Search Product..."></asp:TextBox>
                <asp:Button ID="btnSearchProduct" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearchProduct_Click" />
                <asp:GridView ID="GridViewProducts" runat="server" AutoGenerateColumns="false" CssClass="table table-striped">
                    <Columns>
                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                        <asp:BoundField DataField="Category" HeaderText="Category" />
                        <asp:BoundField DataField="ProductionDate" HeaderText="Production Date" />
                        <asp:BoundField DataField="FarmerName" HeaderText="Farmer" />
                    </Columns>
                </asp:GridView>
            </div>

            <div class="section">
                <h2>Search Farmers</h2>
                <asp:TextBox ID="txtSearchFarmer" runat="server" CssClass="form-control" placeholder="Search Farmer..."></asp:TextBox>
                <asp:Button ID="btnSearchFarmer" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearchFarmer_Click" />
                <asp:GridView ID="GridViewFarmers" runat="server" AutoGenerateColumns="false" CssClass="table table-striped">
                    <Columns>
                        <asp:BoundField DataField="FarmerName" HeaderText="Farmer Name" />
                        <asp:BoundField DataField="Contact" HeaderText="Contact" />
                        <asp:BoundField DataField="Location" HeaderText="Location" />
                    </Columns>
                </asp:GridView>
                </div>
            </div>
        </div>
    </form>
     <!---------------------------------Scripts----------------------------->
  <script
      src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous">
  </script>
</body>
</html>


