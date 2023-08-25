<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeleteMovie.aspx.cs" Inherits="webform_UI.Views.DeleteMovie"  Async="true" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Delete Movie</title>
</head>
<body>
    <form id="form1" runat="server">
      
        <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtGenre" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtReleaseDate" runat="server"></asp:TextBox>
        
        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CssClass="auto-click-button" />

        <asp:Literal ID="errorMessage" runat="server"></asp:Literal>
    </form>
    <script type="text/javascript">
        window.onload = function () {
            var autoClickButton = document.querySelector('.auto-click-button');
            autoClickButton.click();
        }
    </script>

</body>
</html>