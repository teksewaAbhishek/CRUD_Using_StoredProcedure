<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="webform_UI.Views.Index" Async="true"  %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>API Consumption Example</title>
    
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
        <div>
             <asp:Button ID="btnAddMovie" runat="server" Text="Add Movie" OnClick="btnAddMovie_Click" />
           <telerik:RadGrid ID="radGrid1" runat="server" AutoGenerateColumns="false" AllowSorting="true" AllowPaging="true" OnNeedDataSource="radGrid1_NeedDataSource" DataKeyNames="Id">
    <MasterTableView>
        <Columns>
            <telerik:GridBoundColumn DataField="Id" HeaderText="ID" UniqueName="Id" />
            <telerik:GridBoundColumn DataField="Title" HeaderText="Title" UniqueName="Title" />
            <telerik:GridBoundColumn DataField="Genre" HeaderText="Genre" UniqueName="Genre" />
            <telerik:GridBoundColumn DataField="ReleaseDate" HeaderText="Release Date" UniqueName="ReleaseDate" DataFormatString="{0:yyyy-MM-dd}" />
           <telerik:GridTemplateColumn HeaderText="Actions">
    <ItemTemplate>
        <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CommandArgument='<%# Eval("Id") %>' />
        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CommandArgument='<%# Eval("Id") %>' />
    </ItemTemplate>
</telerik:GridTemplateColumn>

        </Columns>
    </MasterTableView>
</telerik:RadGrid>

         
            <asp:Literal ID="errorMessage" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>
