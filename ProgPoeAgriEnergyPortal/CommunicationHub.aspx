<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommunicationHub.aspx.cs" Inherits="ProgPoeAgriEnergyPortal.CommunicationHub" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Communication Hub</title>
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
        <div class="chat-container">
            <div class="chat-box" id="chatBox" runat="server">
                <!-- Messages will be displayed here -->
            </div>
            <div class="message-input">
                <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="2" placeholder="Type your message..."></asp:TextBox>
                <asp:Button ID="btnSend" runat="server" Text="Send" OnClick="btnSend_Click" />
            </div>
        </div>
    </form>
</body>
</html>
