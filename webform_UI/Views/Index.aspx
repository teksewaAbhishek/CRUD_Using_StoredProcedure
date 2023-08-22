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
            <telerik:RadGrid ID="radGrid1" runat="server" AutoGenerateColumns="false" AllowSorting="true" OnNeedDataSource="radGrid1_NeedDataSource">
                <MasterTableView>
                    <Columns>
                        <telerik:GridBoundColumn DataField="Id" HeaderText="ID" UniqueName="Id" />
                        <telerik:GridBoundColumn DataField="Title" HeaderText="Title" UniqueName="Title" />
                        <telerik:GridBoundColumn DataField="Genre" HeaderText="Genre" UniqueName="Genre" />
                        <telerik:GridBoundColumn DataField="ReleaseDate" HeaderText="Release Date" UniqueName="ReleaseDate" DataFormatString="{0:yyyy-MM-dd}" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <asp:Literal ID="errorMessage" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>
