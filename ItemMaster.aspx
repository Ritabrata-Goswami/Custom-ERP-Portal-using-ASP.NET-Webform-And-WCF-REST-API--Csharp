<%@ Page Title="ERP Portal - Item Master" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ItemMaster.aspx.cs" Inherits="ItemMaster" %>

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
            font-size:15px;
            margin-bottom:5px;
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
            <div class="GridViewHdrTxt">Item Master Data</div>
            <div class="GridPort">
                 <asp:GridView runat="server" ID="ItemMasterData" CssClass="GridView" ShowHeaderWhenEmpty="true" CellPadding="10" AutoGenerateColumns="false" OnRowDataBound="ItemInvalidMasterData_DataBound">
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
                         <asp:TemplateField HeaderText="Item Name">
                             <ItemTemplate>
                                 <div class="GridRowData"><%# Eval("ItemName") %></div>
                             </ItemTemplate>
                         </asp:TemplateField>

                         <asp:TemplateField HeaderText="Item Type">
                            <ItemTemplate>
                                <div class="GridRowData"><%# Eval("ItemTypeName") %></div>
                            </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Units of Measure">
                            <ItemTemplate>
                                <div class="GridRowData"><%# Eval("UoMName") %></div>
                            </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Price/Unit">
                            <ItemTemplate>
                                <div class="GridRowData"><%# Eval("Price") %></div>
                            </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Current Stock">
                            <ItemTemplate>
                                <div class="GridRowData"><%# Eval("CurrentStock") %></div>
                            </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Reorder Level">
                            <ItemTemplate>
                                <div class="GridRowData"><%# Eval("ReorderLevel") %></div>
                            </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Vendor Code">
                            <ItemTemplate>
                                <div class="GridRowData"><%# Eval("VendorCode") %></div>
                            </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Vendor Name">
                            <ItemTemplate>
                                <div class="GridRowData"><%# Eval("VendorName") %></div>
                            </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Warehouse Code">
                            <ItemTemplate>
                                <div class="GridRowData"><%# Eval("WhsCode") %></div>
                            </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Warehouse Name">
                             <ItemTemplate>
                                 <div class="GridRowData"><%# Eval("WareHouseName") %></div>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Warehouse Type">
                             <ItemTemplate>
                                 <div class="GridRowData"><%# Eval("WareHouseType") %></div>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Dimension">
                             <ItemTemplate>
                                 <div class="GridRowData"><%# Eval("Dimensions") %></div>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Weight">
                             <ItemTemplate>
                                 <div class="GridRowData"><%# Eval("Weight") %></div>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Expire Date">
                            <ItemTemplate>
                                <div class="GridRowData"><%# Eval("ExpDate") %></div>
                            </ItemTemplate>
                         </asp:TemplateField>

                         <asp:TemplateField HeaderText="Active?">
                             <ItemTemplate>
                                 <div class="GridRowData"><%# Eval("Active") %></div>
                             </ItemTemplate>
                         </asp:TemplateField>

                         <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <div class="">
                                    <asp:HyperLink 
                                        ID="EditBtn" 
                                        runat="server" 
                                        CssClass="EditBtn" 
                                        Text="Edit Vendor" 
                                        NavigateUrl='<%#Eval("ItemCode", "EditMaster.aspx?Code={0}&Type=Item") %> ' 
                                        Target="_blank">
                                    </asp:HyperLink>
                                </div>
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

