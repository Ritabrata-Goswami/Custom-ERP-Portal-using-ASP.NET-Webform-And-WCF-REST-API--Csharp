<%@ Page Title="ERP Portal - Vendor Master" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="VendorMaster.aspx.cs" Inherits="VendorMaster" %>

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
            <div class="GridViewHdrTxt">Vendor Master Data</div>
            <div class="GridPort">
                 <asp:GridView runat="server" ID="VendorMasterData" CssClass="GridView" ShowHeaderWhenEmpty="true" CellPadding="10" AutoGenerateColumns="false">
                     <Columns>
                         <asp:TemplateField HeaderText="Id" >
                             <ItemTemplate>
                                 <div class="GridRowData"><%# Eval("Id") %></div>
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
                         <asp:TemplateField HeaderText="Type">
                             <ItemTemplate>
                                 <div class="GridRowData"><%# Eval("Type") %></div>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Contact Person">
                             <ItemTemplate>
                                 <div class="GridRowData"><%# Eval("ContactPerson") %></div>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Contact Phone">
                             <ItemTemplate>
                                 <div class="GridRowData"><%# Eval("ContactPhone") %></div>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Contact Email">
                             <ItemTemplate>
                                 <div class="GridRowData"><%# Eval("ContactEmail") %></div>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Vendor Address">
                             <ItemTemplate>
                                 <div class="GridRowData"><%# Eval("VendorAddress") %></div>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Account Detail">
                             <ItemTemplate>
                                 <div class="GridRowData"><%# Eval("BankAccountDetail") %></div>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Credit Limit">
                             <ItemTemplate>
                                 <div class="GridRowData"><%# Eval("CreditLimit") %></div>
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
                                        NavigateUrl='<%#Eval("VendorCode", "EditVendor.aspx?VendorCode={0}") %> ' 
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

