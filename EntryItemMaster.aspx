<%@ Page Title="ERP Portal - Entry Item Master" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EntryItemMaster.aspx.cs" Inherits="EntryItemMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <style type="text/css">
        .container{
            width: 90%;
            max-width: 800px;
            margin: 30px auto;
            background: #fff;
            padding: 25px;
            border-radius: 8px;
            box-shadow: 0 0 8px rgba(0, 0, 0, 0.1);
        }
        h2{
            text-align: center;
            color: #079a42;
            margin-bottom: 20px;
        }
        .form-group{
            display: flex;
            flex-direction: column;
            margin-bottom: 15px;
        }
        .form-group label{
            font-weight: 600;
            margin-bottom: 5px;
            color: #333;
        }
        .form-group input[type="text"],
        .form-group input[type="email"],
        .form-group input[type="number"],
        .form-group textarea,
        .form-group select{
            padding: 8px 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
            font-size: 14px;
            outline: none;
            width: 100%;
            box-sizing: border-box;
        }
        .form-group textarea{
            resize: vertical;
        }

        .PopupBtn{
            /*width:30px;*/
            /*height:20px;*/
            margin-top:5px;
            padding:7px 10px;
            border:none;
            cursor:pointer;
            font-size:12px;
            background-color:coral;
            color:#2c1f1f;
            border-radius:20px;
        }
        .PopupBtn:hover{
            background-color:#e00a0a;
            transition:0.5s;
        }

                /* ===== Save Button ===== */
        .save-btn{
            width: 100%;
            background-color: #4875e9;
            color: white;
            border: none;
            padding: 12px;
            font-size: 16px;
            font-weight: bold;
            border-radius: 5px;
            cursor: pointer;
            margin-top: 15px;
            transition: background-color 0.2s ease-in-out;
        }
        .save-btn:hover{
            background-color:#0762de;
        }

    </style>
    <div class="container">
        <h2>Item Master Data Entry</h2>

        <div class="form-group">
            <label for="txtItemCode">Item Code</label>
            <asp:TextBox ID="txtItemCode" runat="server" placeholder="Enter Item Code"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtItemName">Item Name</label>
            <asp:TextBox ID="txtItemName" runat="server" placeholder="Enter Item Name"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="ddlItemType">Item Type</label>
            <asp:DropDownList ID="ddlItemType" runat="server"></asp:DropDownList>
        </div>

        <div class="form-group">
            <label for="ddlUom">Unit of Measurement</label>
            <asp:DropDownList ID="ddlUom" runat="server"></asp:DropDownList>
        </div>

        <div class="form-group">
            <label for="txtPrice">Item Price/Unit</label>
            <asp:TextBox ID="txtPrice" runat="server" TextMode="Number" placeholder="Enter Unit Price/Unit" step="0.01"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="ddlCurrency">Currency</label>
            <asp:DropDownList ID="ddlCurrency" runat="server"></asp:DropDownList>
        </div>

        <div class="form-group">
            <label for="txtStock">Current Stock</label>
            <asp:TextBox ID="txtStock" runat="server" TextMode="Number" placeholder="Enter Current Stock"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtReordLvl">Reorder Level</label>
            <asp:TextBox ID="txtReordLvl" runat="server" TextMode="Number" placeholder="Enter Reorder Level"></asp:TextBox>
        </div>
        
        <div class="form-group">
            <label for="txtVndCode">Vendor Code</label>
            <div>
                <asp:TextBox ID="txtVndCode" runat="server" placeholder="Enter Vendor Code"></asp:TextBox>
                <asp:Button runat="server" Text="Open Vendor Popup" CssClass="PopupBtn" OnClientClick="OpenPopup(event,'vendor')"/>
            </div>
        </div>

        <div class="form-group">
            <label for="txtWhsCode">Warehouse Code</label>
            <div>
                <asp:TextBox ID="txtWhsCode" runat="server" placeholder="Enter Warehouse Code" ></asp:TextBox>
                <asp:Button runat="server" Text="Open Warehouse Popup" CssClass="PopupBtn" OnClientClick="OpenPopup(event,'warehouse')" />
            </div>
        </div>

        <div class="form-group">
            <label for="txtDimns">Dimension</label>
            <asp:TextBox ID="txtDimns" runat="server" placeholder="Enter Dimension ... (2.5 X 2.5 X 2.5)"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtWeight">Item Weight/Unit</label>
            <asp:TextBox ID="txtWeight" runat="server" TextMode="Number" placeholder="Enter Weight/Unit" step="0.01"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="ddlWegUnit">Weight Measurement</label>
            <asp:DropDownList ID="ddlWegUnit" runat="server"></asp:DropDownList>
        </div>

        <div class="form-group">
            <label for="txtExpDate">Expire Date</label>
            <asp:TextBox ID="txtExpDate" runat="server" TextMode="Date" placeholder="Enter Expire Date"></asp:TextBox>
        </div>


        <div class="form-group">
            <label for="ddlActive">Active</label>
            <asp:DropDownList ID="ddlActive" runat="server">
                <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                <asp:ListItem Text="No" Value="N"></asp:ListItem>
            </asp:DropDownList>
        </div>

        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="save-btn" OnClientClick="return validateForm()" OnClick="SaveMasterData" />
    </div>

    <script type="text/javascript">
        function validateForm() {
            var ItemCode = document.getElementById('<%= txtItemCode.ClientID %>').value.trim();
            var ItemName = document.getElementById('<%= txtItemName.ClientID %>').value.trim();
            var ItemType = document.getElementById('<%= ddlItemType.ClientID %>').value.trim();
            var Uom = document.getElementById('<%= ddlUom.ClientID %>').value;
            var ItemPrice = document.getElementById('<%= txtPrice.ClientID %>').value.trim();
            var Currency = document.getElementById('<%= ddlCurrency.ClientID %>').value;
            var Stock = document.getElementById('<%= txtStock.ClientID %>').value.trim();
            var ReorderLvl = document.getElementById('<%= txtReordLvl.ClientID %>').value.trim();
            var VendorCode = document.getElementById('<%= txtVndCode.ClientID %>').value.trim();
            var WarehouseCode = document.getElementById('<%= txtWhsCode.ClientID %>').value.trim();
            
            var Dimension = document.getElementById('<%= txtDimns.ClientID %>').value.trim();
            var ItemWeight = document.getElementById('<%= txtWeight.ClientID %>').value.trim();
            var ItemWeightMeasurement = document.getElementById('<%= ddlWegUnit.ClientID %>').value.trim();
            var ExpireDate = document.getElementById('<%= txtExpDate.ClientID %>').value.trim();
            var Active = document.getElementById('<%= ddlActive.ClientID %>').value;

            if (!ItemCode || !ItemName || !ItemType || !Uom || !ItemPrice || !Currency || !Stock || !ReorderLvl
                || !VendorCode || !WarehouseCode || !ItemWeight || !ItemWeightMeasurement || !Active) {
                alert("All fields are mandatory!");

                return false;
                }
        }

        function OpenPopup(e, s_arg) {
            e.preventDefault();

            let PopupWidth = screen.width - 200;
            let PopupHeight = screen.height -100;

            let ScreenWidth = screen.width;
            let ScreenHeight = screen.height;
            let VerticalSpace = (ScreenHeight - PopupHeight) / 2;
            let HorizontalSpace = (ScreenWidth - PopupWidth) / 2;

            let PopupDefn = "width=" + PopupWidth + ",height=" + PopupHeight + ",top=" + VerticalSpace + ",left=" + HorizontalSpace + ",scrollbars=yes";

            window.open("/PopupMaster.aspx?popup_type=" + s_arg, s_arg, PopupDefn);
        }

        function SetPopupValue(getVal, popupTypeVal) {
            //console.log(getVal);
            //console.log(popupTypeVal);

            switch (popupTypeVal) {
                case "vendor":
                    document.getElementById("<%=txtVndCode.ClientID%>").value = getVal;
                    break;
                case "warehouse":
                    document.getElementById("<%=txtWhsCode.ClientID%>").value = getVal;
                    break;
            }
        }
    </script>
</asp:Content>

