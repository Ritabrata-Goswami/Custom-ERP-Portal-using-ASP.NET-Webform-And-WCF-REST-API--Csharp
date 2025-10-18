<%@ Page Title="ERP Portal - Edit Master" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EditMaster.aspx.cs" Inherits="EditVendor" %>

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

        .Code{
            text-align:center; 
            font-size:18px; 
            margin-bottom:5px; 
            font-weight:bold;
        }
        .instruction{
            text-align:center; 
            font-size:15px; 
            margin-bottom:10px;
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
        <h2>Edit <%=MasterType %> Master Data</h2>
        <div class="Code">Code: <%=Code %></div>
        <div class="instruction">You can only <strong>active</strong> or <strong>de-active</strong> the master data.</div>
        <div class="form-group">
            <label for="ddlActive">Active</label>
            <asp:DropDownList ID="ddlActive" runat="server">
                <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                <asp:ListItem Text="No" Value="N"></asp:ListItem>
            </asp:DropDownList>
        </div>
         <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="save-btn" OnClick="EditData" />
    </div>
</asp:Content>

