<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="webform_UI.Views.Login" Async="true" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
        }

        #login-box {
            background-color: #fff;
            border-radius: 5px;
            box-shadow: 0px 2px 5px rgba(0, 0, 0, 0.1);
            padding: 20px;
            width: 300px;
        }

        label {
            font-weight: bold;
        }

        input[type="text"],
        input[type="password"] {
            width: 100%;
            padding: 8px;
            margin: 5px 0;
            border: 1px solid #ccc;
            border-radius: 3px;
        }

        .button-container {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-top: 10px;
        }

        .button-container button {
            padding: 8px 15px;
            border: none;
            border-radius: 3px;
            cursor: pointer;
        }

        #btnLogin {
            background-color: #007bff;
            color: white;
        }

        #btnRegister {
            background-color: #28a745;
            color: white;
        }

        #lblSessionValue {
            font-size: 14px;
            color: #333;
            margin-top: 10px;
        }

        #tokenLabel {
            font-size: 12px;
            color: #888;
            margin-top: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="login-box">
            <h2>Login</h2>
            <label for="txtUsername">Username:</label>
            <input type="text" id="txtUsername" runat="server" />

            <label for="txtPassword">Password:</label>
            <input type="password" id="txtPassword" runat="server" />

            <div class="button-container">
                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="Button_Click" CssClass="login-button" />
                <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" CssClass="register-button" />
            </div>

            <div>
                <asp:Label ID="lblSessionValue" runat="server" Text=""></asp:Label>
                <asp:Label ID="tokenLabel" runat="server"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
