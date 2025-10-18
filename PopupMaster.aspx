<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopupMaster.aspx.cs" Inherits="PopupMaster" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ERP Portal - Popup <%= popupType %></title>
    <style type="text/css">
        .MainContainer {
            padding:5px 7px;
            font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
        .HdrTxt1{
            font-size:20px;
            font-weight:bold;
            margin-top:5px;
            margin-bottom:5px;
        }
        .HdrTxt2{
            font-size:16px;
            margin-bottom:5px;
        }
        .TblContainer{
            margin-top:20px;
            padding-left:10px;
            padding-right:10px;
        }
        .GridView{
            min-width: 1200px;
            border-collapse: collapse;
        }
        .GridView th{
            background-color:#0688f5;
            color:antiquewhite;
        }
        .GridView th, .GridView td {
            padding: 7px 10px;
            border: 1px solid #ccc;
            white-space: nowrap; 
            text-align:center;
        }
        .GridView tr:hover{
            cursor:pointer;
            background-color:#e6e6e6;
        }
        .GridViewEmptyData{
            text-align: center; 
            padding: 10px; 
            color:#e61212; 
            background-color:#e1d7d7;
            font-weight:bold
        }


    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="MainContainer">
            <div class="HdrTxt1">Welcome to <%= popupType %> popup page.</div>
            <div class="HdrTxt2">Select by clicking row</div>
            <div class="TblContainer">
                <asp:GridView runat="server" ID="DisplayData" CssClass="GridView" CellPadding="10">
                    <EmptyDataTemplate>
                        <div class="GridViewEmptyData">
                            <strong>No records found matching your criteria.</strong>
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </form>

    <script type="text/javascript">
        function DataToParent(Val) {
            const urlParams = new URLSearchParams(window.location.search);
            let popupType = urlParams.get("popup_type");

            if (window.opener) {
                window.opener.SetPopupValue(Val, popupType);
                window.close();
            }
        }
    </script>
</body>
</html>
