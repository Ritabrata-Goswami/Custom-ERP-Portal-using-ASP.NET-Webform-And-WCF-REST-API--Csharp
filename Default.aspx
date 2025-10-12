<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ERP Portal - Log In</title>
    <style type="text/css">
        body{
            font-family:Arial;
            background-color:#e6e6e6;
        }
        .parent-container{
            display: flex;
            flex-direction: column;
            padding:12px 13px;
            align-items:center;
            height:100vh;
        }
        .hdr-txt01{
            font-weight:bold;
            font-size:25px;
            margin-bottom:10px;
        }
        .hdr-txt02{
            font-size:17px;
            color:#ff0101;
        }

        .txtMessageContainer{
            margin-top:10px;
            margin-bottom:10px; 
            text-align:center;
        }

                /*Login container Css start*/
        .login-container{
            margin-top:15vh;
            width:40%;
            /*display:flex;
            flex-direction:column;
            align-items:center;
            border:1px solid #000000; */
            padding:15px 15px;
        }
        .login-fields-container{
            display:flex;
            flex-direction:row;
            align-items: center; 
            margin-bottom:10px;
        }
        .field-txt{
            font-size:17px;
            flex:25%;
        }
        .login-fields{
            padding:5px 10px;
            flex:75%;
            border:0.5px solid #948a8a;
            border-radius:3px;
            font-size:17px;
        }
        .btn{
            padding:10px 15px;
            border:none;
            border-radius:3px;
            background-color:#269bf3;
            color:aliceblue;
            font-size:18px;
            font-weight:bold;
            cursor:pointer;
            width:100%;
        }
    </style>
</head>
<body>
    <div class="parent-container">
        <div class="hdr-txt01">Enterprise Resource Planning Portal</div>
        <div class="hdr-txt02">* fields are mandatory below</div>
        <div class="txtMessageContainer">
            <asp:Label 
                runat="server" 
                ID="txtMessage" 
                Style="font-size:16px; color:#e81313; font-weight:bold;"></asp:Label>
        </div>

        <form id="form1" runat="server" class="login-container">
            <div class="login-fields-container">
                <div class="field-txt">
                    <span style="color:#de0f0f">*</span> Employee Id: 
                </div>
                <asp:TextBox 
                    ID="Enter_EmpId" 
                    runat="server" 
                    CssClass="login-fields" 
                    placeholder="Enter Employee Id..."></asp:TextBox>
            </div>
            <div class="login-fields-container">
                <div class="field-txt">
                    <span style="color:#de0f0f">*</span> Password: 
                </div>
                <asp:TextBox 
                    TextMode="Password" 
                    ID="Enter_Password" 
                    runat="server" 
                    CssClass="login-fields" 
                    placeholder="Enter Password..."></asp:TextBox>
            </div>
            <div style="margin-top:20px">
                <asp:Button runat="server" CssClass="btn" ID="LoginBtn" Text="Login" OnClick="Login"/>
            </div>
        </form>
    </div>
</body>
</html>
