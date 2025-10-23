<%@ Page Title="ERP Portal - Post Inventory" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PostInventory.aspx.cs" Inherits="PostInventory" %>

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
        <h2>Item Inventory Entry</h2>

        <div class="form-group">
            <label for="txtItemCode">Item Code</label>
            <div>
                <asp:TextBox ID="txtItemCode" runat="server" placeholder="Enter Item Code"></asp:TextBox>
                <asp:Button runat="server" Text="Open Item Popup" CssClass="PopupBtn" OnClientClick="OpenPopup(event,'item')" />
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
            <label for="txtGrpoNum">GRPO Num</label>
            <div>
                <asp:TextBox ID="txtGrpoNum" runat="server" placeholder="Enter GRPO Number" ></asp:TextBox>
                <asp:Button runat="server" Text="Open Grpo Popup" CssClass="PopupBtn" OnClientClick="OpenPopup(event,'grpo')" />
            </div>
        </div>
        <div class="form-group">
            <label for="txtBatchNo">Batch No</label>
            <div>
                <asp:TextBox ID="txtBatchNo" runat="server" placeholder="Enter Batch Number" ></asp:TextBox>
                <asp:Button runat="server" Text="Open Batch Popup" CssClass="PopupBtn" OnClientClick="OpenPopup(event,'batch')" />
            </div>
        </div>
        <div class="form-group">
            <label for="txtPrice">Item Price/Unit</label>
            <asp:TextBox ID="txtPrice" runat="server" TextMode="Number" placeholder="Enter Unit Price/Unit" step="0.01"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtStock">On Hand Quantity</label>
            <asp:TextBox ID="txtStock" runat="server" TextMode="Number" placeholder="Enter On Hand Quantity"></asp:TextBox>
        </div>

        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="save-btn" OnClientClick="return validateForm()" OnClick="SaveInventoryData" />
    </div>

    <script type="text/javascript">
        function validateForm() {
            var ItemCode = document.getElementById('<%= txtItemCode.ClientID %>').value.trim();
            var WarehouseCode = document.getElementById('<%= txtWhsCode.ClientID %>').value.trim();
            var GRPONo = document.getElementById('<%= txtGrpoNum.ClientID %>').value.trim();
            var BatchNo = document.getElementById('<%= txtBatchNo.ClientID %>').value;
            var ItemPrice = document.getElementById('<%= txtPrice.ClientID %>').value.trim();
            var Stock = document.getElementById('<%= txtStock.ClientID %>').value.trim();

            if (!ItemCode || !WarehouseCode || !GRPONo || !BatchNo || !ItemPrice || !Stock) {
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
                case "item":
                    document.getElementById("<%=txtItemCode.ClientID%>").value = getVal;
                    break;
                case "warehouse":
                    document.getElementById("<%=txtWhsCode.ClientID%>").value = getVal;
                    break;
                case "grpo":
                    document.getElementById("<%=txtGrpoNum.ClientID%>").value = getVal;
                    break;
                case "batch":
                    document.getElementById("<%=txtBatchNo.ClientID%>").value = getVal;
                    break;
            }
        }
    </script>
</asp:Content>

