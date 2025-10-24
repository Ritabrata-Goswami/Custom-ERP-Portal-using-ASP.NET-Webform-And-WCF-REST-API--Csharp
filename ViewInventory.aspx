<%@ Page Title="ERP Portal - View Inventory" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ViewInventory.aspx.cs" Inherits="ViewInventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <style type="text/css">
        .EmpIdDisplay{
            font-weight:bold;
        }
        .display-container{
            margin-top:10px;
            height: 100%;
            display: flex;
            flex-direction: column;
        }
        .GridPort{
            flex: 1;
            width: 100%;       
            overflow-x: auto;   
            overflow-y: auto;
            border: 1px solid #ccc; 
            padding: 5px;       
            background-color: #f8f8f8;
            box-sizing: border-box;
            max-height:76vh;
        }
        .GridView{
            min-width: 1200px;
            border-collapse: collapse;
        }
        .GridView th, .GridView td {
            padding: 7px 10px;
            border: 1px solid #ccc;
            white-space: nowrap;   
        }
        .GridViewHdrTxt{
            font-weight:bold;
            font-size:18px;
            margin-bottom:8px;
        }
        .GridRowData{
            font-size:14px;
            font-family:'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
        }
        .EditBtn{
            text-decoration:none;
            padding:7px 10px;
            background-color:#078b4c;
            color:aliceblue;
            font-weight:bold;
            font-size:13px;
            cursor:pointer;
            border-radius:3px;
        }
        .EditBtn:hover{
            transition:0.3s;
            background-color:#0fb3b1
        }

        .GridViewEmptyData{
            text-align: center; 
            padding: 10px; 
            color:#e61212; 
            background-color:#e1d7d7;
            font-weight:bold
        }
    </style>

    <div class="display-container">
        <div>
            <div class="GridViewHdrTxt">Display Inventory Data</div>
            <div class="GridPort">
                 <asp:GridView runat="server" ID="InventoryData" CssClass="GridView" ShowHeaderWhenEmpty="true" CellPadding="10" AutoGenerateColumns="false" OnRowDataBound="ItemInvalidMasterData_DataBound">
                     <Columns>
                         <asp:TemplateField HeaderText="Id" >
                             <ItemTemplate>
                                 <div class="GridRowData"><%# Eval("Id") %></div>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Item Code">
                             <ItemTemplate>
                                 <div class="GridRowData"><%# Eval("ItemCode") %></div>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Warehouse Code">
                            <ItemTemplate>
                                <div class="GridRowData"><%# Eval("WhsCode") %></div>
                            </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="GRPO No">
                             <ItemTemplate>
                                 <div class="GridRowData"><%# Eval("GRPONo") %></div>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Batch No">
                             <ItemTemplate>
                                 <div class="GridRowData"><%# Eval("BatchNo") %></div>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Quantity On Hand">
                             <ItemTemplate>
                                 <div class="GridRowData"><%# Eval("QuantityOnHand") %></div>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Price/Unit">
                             <ItemTemplate>
                                 <div class="GridRowData"><%# Eval("UnitPrice") %></div>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Last Date Inventory Updated">
                            <ItemTemplate>
                                <div class="GridRowData"><%# Eval("LastUpdateDate") %></div>
                            </ItemTemplate>
                         </asp:TemplateField>
                     </Columns>
                     <HeaderStyle BackColor="#1595d5" ForeColor="White"/>
                     <RowStyle BackColor="#cccccc" />
                     <SelectedRowStyle  BackColor="LightCyan" ForeColor="DarkBlue" Font-Bold="true"/>
                     <EmptyDataTemplate>
                         <div class="GridViewEmptyData">
                             <strong>No records found matching your criteria.</strong>
                         </div>
                     </EmptyDataTemplate>
                 </asp:GridView>
            </div>
   
        </div>
    </div>
</asp:Content>

