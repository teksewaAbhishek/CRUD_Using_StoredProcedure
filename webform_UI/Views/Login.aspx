<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="webform_UI.Views.Login" Async="true" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label for="txtUsername">Username:</label>
            <input type="text" id="txtUsername" runat="server" />
            <br />
            <label for="txtPassword">Password:</label>
            <input type="password" id="txtPassword" runat="server" />
            <br />
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="Button_Click" />
            <asp:Label ID="lblSessionValue" runat="server" Text=""></asp:Label>
            <asp:Label ID="tokenLabel" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>

