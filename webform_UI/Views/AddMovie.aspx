<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddMovie.aspx.cs" Inherits="webform_UI.Views.AddMovie" Async="true"  %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Movie</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label for="txtTitle">Title:</label>
            <asp:TextBox ID="txtTitle" runat="server" />

            <label for="txtGenre">Genre:</label>
            <asp:TextBox ID="txtGenre" runat="server" />

            <label for="txtReleaseDate">Release Date:</label>
            <asp:TextBox ID="txtReleaseDate" runat="server" placeholder="MM-DD-YYYY" />



            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
             <asp:Literal ID="errorMessage" runat="server"></asp:Literal>
                
        </div>
    </form>
</body>
</html>
