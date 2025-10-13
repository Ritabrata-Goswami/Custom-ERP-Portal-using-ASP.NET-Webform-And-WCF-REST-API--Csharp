<%@ Page Title="ERP Portal - Entry Warehouse Master" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EntryWarehouseMaster.aspx.cs" Inherits="EntryWarehouseMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
     <style>
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
         <h2>Warehouse Master Data Entry</h2>

         <div class="form-group">
             <label for="txtWarehouseCode">Warehouse Code</label>
             <asp:TextBox ID="txtWarehouseCode" runat="server"></asp:TextBox>
         </div>

         <div class="form-group">
             <label for="txtWarehouseName">Warehouse Name</label>
             <asp:TextBox ID="txtWarehouseName" runat="server"></asp:TextBox>
         </div>

         <div class="form-group">
            <label for="txtAddress">Warehouse Address</label>
            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Rows="2"></asp:TextBox>
         </div>

         <div class="form-group">
             <label for="txtContactPerson">Contact Person</label>
             <asp:TextBox ID="txtContactPerson" runat="server"></asp:TextBox>
         </div>

         <div class="form-group">
             <label for="txtContactPhone">Contact Phone</label>
             <asp:TextBox ID="txtContactPhone" runat="server"></asp:TextBox>
         </div>

         <div class="form-group">
             <label for="txtContactEmail">Contact Email</label>
             <asp:TextBox ID="txtContactEmail" runat="server" TextMode="Email"></asp:TextBox>
         </div>


         <div class="form-group">
             <label for="ddlWarehouseTypes">Warehouse Types</label>
             <asp:DropDownList ID="ddlWarehouseTypes" runat="server"></asp:DropDownList>
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

         <%--Field Validation--%>
     <script type="text/javascript">
         function validateForm() {
             var warehouseCode = document.getElementById('<%= txtWarehouseCode.ClientID %>').value.trim();
             var warehouseName = document.getElementById('<%= txtWarehouseName.ClientID %>').value.trim();
             var warehouseAddr = document.getElementById('<%= txtAddress.ClientID %>').value.trim();
             var contactPerson = document.getElementById('<%= txtContactPerson.ClientID %>').value.trim();
             var contactPhone = document.getElementById('<%= txtContactPhone.ClientID %>').value.trim();
             var contactEmail = document.getElementById('<%= txtContactEmail.ClientID %>').value.trim();
             var warehouseType = document.getElementById('<%= ddlWarehouseTypes.ClientID %>').value;
             var active = document.getElementById('<%= ddlActive.ClientID %>').value;

             if (!warehouseCode){
                 alert("Warehouse Code is required!");
                 return false;
             }
             if (!warehouseName){
                 alert("Warehouse Name is required!");
                 return false;
             }
             if (!warehouseAddr) {
                 alert("Warehouse Address is required!");
                 return false;
             }
             if (!contactPerson) {
                 alert("Contact Person is required!");
                 return false;
             }
             if (!contactPhone) {
                 alert("Contact Phone is required!");
                 return false;
             }
             if (!contactEmail) {
                 alert("Contact Email is required!");
                 return false;
             }
             if (!warehouseType) {
                 alert("Warehouse Types must be selected!");
                 return false;
             }
             if (!active) {
                 alert("Active Status must be selected!");
                 return false;
             }

             return true;
         }
     </script>
</asp:Content>

