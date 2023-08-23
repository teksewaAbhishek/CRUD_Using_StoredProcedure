<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditMovie.aspx.cs" Inherits="webform_UI.Views.EditMovie" Async="true" %>




<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Movie</title>
</head>
<body>
    <form id="form1" runat="server">
      
        <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtGenre" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtReleaseDate" runat="server"></asp:TextBox>
        
        <asp:Button ID="btnSave" runat="server" Text="Save Changes" OnClick="btnSave_Click" />
        <asp:Literal ID="errorMessage" runat="server"></asp:Literal>
    </form>
</body>
</html>

