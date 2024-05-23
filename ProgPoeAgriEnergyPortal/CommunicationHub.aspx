<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommunicationHub.aspx.cs" Inherits="ProgPoeAgriEnergyPortal.CommunicationHub" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Communication Hub</title>
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <link rel="stylesheet" href="~/CSS/mySheet.css" />
    <style>
        .chat-container {
            display: flex;
            flex-direction: column;
            height: 80vh;
            width: 50%;
            margin: 0 auto;
            border: 1px solid #ccc;
            padding: 10px;
        }
        .chat-box {
            flex: 1;
            overflow-y: scroll;
            border: 1px solid #ccc;
            margin-bottom: 10px;
            padding: 10px;
        }
        .message-input {
            display: flex;
        }
        .message-input textarea {
            flex: 1;
            padding: 5px;
            border: 1px solid #ccc;
            border-radius: 3px;
        }
        .message-input button {
            padding: 5px 10px;
            margin-left: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Navigation Bar -->
        <nav class="navbar navbar-expand-lg navbar-light bg-light">
            <a class="navbar-brand" href="#">Agri-Energy Portal</a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav ml-auto">
                    <li class="nav-item">
                        <a class="nav-link" href="TransactionsPage.aspx">Transactions</a>
                    </li>    
                    <li class="nav-item">
                        <a class="nav-link" href="LoginPage.aspx">Logout</a>
                    </li>     
                </ul>
            </div>
        </nav>
        <div class="chat-container">
            <div class="chat-box" id="chatBox" runat="server">
                <!-- Messages will be displayed here -->
            </div>

            <div>
                 <asp:Label ID="Label1" runat="server" Text="Select the receiver"></asp:Label>
                 <asp:DropDownList ID="ddlReceiver" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>

            <div class="message-input">
                
            <div>

            </div>

                <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" placeholder="Type your message..."></asp:TextBox>
                <asp:Button ID="btnSend" runat="server" CssClass="btn btn-primary" Text="Send Message" OnClick="btnSend_Click" style="left: 0px; top: 0px; width: 180px" />
            </div>
        </div>
    </form>
    <!-- Bootstrap JS -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
</body>
</html>
