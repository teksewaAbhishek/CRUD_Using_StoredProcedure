<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterUsers.aspx.cs" Inherits="webform_UI.Views.RegisterUsers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>
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

        #registration-box {
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
            text-align: center;
            margin-top: 10px;
        }

        .button-container button {
            padding: 8px 15px;
            border: none;
            border-radius: 3px;
            cursor: pointer;
            background-color: #007bff;
            color: white;
        }

        #errorMessage {
            display: block;
            font-size: 14px;
            color: #ff0000;
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="registration-box">
            <h2>Registration</h2>
            <label for="txtUsername">Username:</label>
            <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>

            <label for="txtPassword">Password:</label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>

            <div class="button-container">
                <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" />
            </div>

            <asp:Literal ID="errorMessage" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>
