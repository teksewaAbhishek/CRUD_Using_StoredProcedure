<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Checking.aspx.cs" Inherits="webform_UI.Views.Checking"  Async="true" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>API Consumption</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/telerik-kendo-theme@latest/dist/all.css" />
    <script src="https://cdn.jsdelivr.net/npm/jquery@latest/dist/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@progress/kendo-ui@latest/js/kendo.all.min.js"></script>

    <style type="text/css">
        div.RadGrid {
            margin-left: auto !important;
            margin-right: auto !important;
        }

        .row-options-box {
        display: none;
        position: absolute;
        background-color: snow; 
        color: white; 
        border: 1px solid #ddd;
        padding: 5px;
        box-shadow: 2px 2px 5px rgba(0, 0, 0, 0.2);
    }

    .options-item {
        margin-bottom: 5px;
    }
    </style>




</head>
<body>
    <form id="form1" runat="server">

        <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
         <asp:Button ID="btnAddMovie" runat="server" Text="Add Movie" OnClick="btnAddMovie_Click" />
        
      
        <br />
        
     
         <telerik:RadGrid ID="radGrid1" runat="server" AutoGenerateColumns="false" AllowSorting="true" AllowPaging="true" OnNeedDataSource="radGrid1_NeedDataSource"  CssClass="auto-style1" DataKeyNames="Id">

            <MasterTableView >
                <Columns>
                      <telerik:GridBoundColumn DataField="Id" HeaderText="ID" UniqueName="Id" />
  <telerik:GridBoundColumn DataField="Title" HeaderText="Title" UniqueName="Title" />
  <telerik:GridBoundColumn DataField="Genre" HeaderText="Genre" UniqueName="Genre" />
  <telerik:GridBoundColumn DataField="ReleaseDate" HeaderText="Release Date" UniqueName="ReleaseDate" DataFormatString="{0:yyyy-MM-dd}" />
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>

        
            <asp:Literal ID="errorMessage" runat="server"></asp:Literal>
         



<div id="optionsBox" class="row-options-box" style="display: none;">
    <div class="options-item">
        <a href="#" class="edit-button">Edit</a>
    </div>
    <div class="options-item">
        <a href="#" class="delete-button">Delete</a>
    </div>
</div>

<script>
    
    $(document).ready(function () {
       
        var currentOptionsBox = null;

        $("#radGrid1").on("contextmenu", "td", function (event) {
            event.preventDefault();

            var cell = $(this);

            if (currentOptionsBox) {
                currentOptionsBox.remove(); // Remove the previous context menu
            }

            var optionsBox = $("#optionsBox").clone();

            optionsBox.find(".edit-button").on("click", function (event) {
                event.preventDefault();
                var id = cell.closest("tr").find("td:eq(0)").text();
                //window.location.href = "EditMovie.aspx?movieId=" + id + "&token" + Session["AccessToken"];
                window.location.href = "EditMovie.aspx?movieId=" + id + "&token=" + '<%= Session["AccessToken"] %>';

            });

            optionsBox.find(".delete-button").on("click", function (event) {
                event.preventDefault();
                var id = cell.closest("tr").find("td:eq(0)").text();
                window.location.href = "DeleteMovie.aspx?movieId=" + id + "&token=" + '<%= Session["AccessToken"] %>';
            });

            optionsBox.css({
                top: event.clientY,
                left: event.clientX
            });

            $("body").append(optionsBox);
            optionsBox.show();

            currentOptionsBox = optionsBox;

            // Hide the context menu when clicking anywhere else on the page
            $(document).on("click", function () {
                if (currentOptionsBox) {
                    currentOptionsBox.remove();
                    currentOptionsBox = null;
                }
            });
        });
    });
</script>








    </form>
</body>
</html>