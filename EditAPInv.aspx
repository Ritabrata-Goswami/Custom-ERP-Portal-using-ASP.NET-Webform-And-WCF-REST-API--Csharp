<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditAPInv.aspx.cs" Inherits="EditAPInv" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ERP Portal - Edit A/P Invoice</title>
    <style type="text/css">
        body{
            font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
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
        .EditInfoContainer{
            display:flex;
            flex-direction:column; 
            gap:5px; 
            justify-content:center;
            align-items:center;
        }
        .Info-txt{
            font-size:18px;
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
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <div class="container">
                <h2>Edit A/P Invoice</h2>
                <div class="EditInfoContainer">
                    <div class="Info-txt"><strong>Invoice No: </strong><%=APInvNo %></div>
                    <div class="Info-txt"><strong>Row Id: </strong><%=APInvRowId %></div>
                </div>
                
                <div class="form-group">
                    <label for="ddlActive">Invoice Status</label>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropdown">

                    </asp:DropDownList>
                </div>
                 <asp:Button ID="btnSave" runat="server" Text="Update Invoice" CssClass="save-btn" OnClientClick="return UpdateAPInvoice(event)" />
            </div>

        </div>
    </form>

    <script type="text/javascript">
        function UpdateAPInvoice(e) {
            e.preventDefault();

            const urlObj = new URLSearchParams(window.location.search);
            let empId = urlObj.get("empId");

            let DocStatusVal = document.getElementById("<%=ddlStatus.ClientID%>").value;
            if (DocStatusVal === "") {
                return false;
            }

            let sendObj = {
                RowId:<%= APInvRowId%>,
                InvNo: '<%= APInvNo%>',
                UpdateBy: empId,
                DocStatus: DocStatusVal
            };

            //console.log({ InvUpdate: sendObj });

            fetch(`http://localhost:49808/ERP_Web_Portal.svc/UpdateAPInv`, {
                method: "PATCH",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({ InvUpdate: sendObj })
            })
                .then((res) => res.json())
                .then((res) => alert(res.UpdateAPInvResult.ResponseMsg))
                .catch((err) => {
                    console.log(err.message);
                    alert("Error due to:- " + err.message);
                });
        }
    </script>
</body>
</html>
