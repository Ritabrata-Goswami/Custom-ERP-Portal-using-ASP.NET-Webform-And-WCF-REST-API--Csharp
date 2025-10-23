<%@ Page Title="ERP Portal - View GRPO" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ViewGRPO.aspx.cs" Inherits="ViewGRPO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
        <style type="text/css">
        .ParentContainer{
            padding:5px 8px;
        }
        .header-part{
            margin-bottom:20px;
        }
        .HdrTxt01{
            font-size:18px;
            font-weight:bold;
            margin-bottom:5px;
        }
        .HdrTxt02{
            font-size:15px;
            margin-bottom:10px;
        }
        .search-container{
            display:flex; 
            flex-direction:row; 
            gap:5px
        }
        .input-box{
            padding:3px 3px;
            font-size:14px;
            font-weight:bold;
            border:0.5px solid #b7b6b6;
            border-radius:3px;
            width:230px;
        }
        #search-GRPO{
            padding:7px 10px;
            cursor:pointer;
            border:none;
            border-radius:3px;
            background-color:#07cd6a;
            color:antiquewhite;
        }
        #search-GRPO:hover{
            transition:0.5s;
            background-color:#099931;
        }
        .data-part{margin-bottom:20px;}
        th{
            background-color:#2b8aea;
            color:aliceblue;
        }
    </style>
    <div>
        <div class="ParentContainer">
            <div class="header-part">
                <div class="HdrTxt01">View Goods Receipt Purchase Order</div>
                <div class="HdrTxt02">Enter GRPO Number to view Goods Receipt Purchase Order</div>
                <div class="search-container">
                    <input type="text" placeholder="Enter GRPO number..." name="enter_grpo" class="input-box" id="enterGRPONum"/>
                    <button id="search-GRPO">Find GRPO</button>
                </div>
            </div>
            <div class="data-part">
                <div id="GRPO-Hdr-Res"></div>
                <div id="GRPO-Dtl-Res"></div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        window.document.getElementById("search-GRPO").addEventListener("click", (event) => {
            event.preventDefault();

            let getGRPONum = document.getElementById("enterGRPONum").value;
            getGRPONum = getGRPONum.trim();

            if (getGRPONum === "") {
                alert("Please enter a valid GRPO number!");
                return false;
            }

            fetch(`http://localhost:49808/ERP_Web_Portal.svc/GetGRPO?GrpoNum=${getGRPONum}`)
                .then((res) => res.json())
                .then((res) => {
                    //console.log(res);

                    const hdrContainer = document.getElementById("GRPO-Hdr-Res");
                    const dtlContainer = document.getElementById("GRPO-Dtl-Res");

                                //Clear previous results
                    hdrContainer.innerHTML = "";
                    dtlContainer.innerHTML = "";

                    if (res.StatusCode === 200 && res.GrpoHdr) {
                        const h = res.GrpoHdr;
                        hdrContainer.innerHTML = `<h2>Goods Receipt Purchase Order Header</h2>
                            <div style="margin-bottom:3px;"><strong>GRPO Number: </strong> ${h.GRPO_No}</div>
                            <div style="margin-bottom:3px;"><strong>PO Number: </strong> ${h.PO_No}</div>
                            <div style="margin-bottom:3px;"><strong>Vendor Code: </strong> ${h.VendorCode}</div>
                            <div style="margin-bottom:3px;"><strong>Warehouse Code: </strong> ${h.WarehouseCode}</div>
                            <div style="margin-bottom:3px;"><strong>Warehouse Name: </strong> ${h.WarehouseName}</div>
                            <div style="margin-bottom:3px;"><strong>Received Date: </strong> ${h.ReceivedDt}</div>
                            <div style="margin-bottom:3px;"><strong>Received By: </strong> ${h.ReceivedBy}</div>
                            <div style="margin-bottom:3px;"><strong>Employee Name: </strong> ${h.EmpName}</div>
                            <div style="margin-bottom:3px;"><strong>Document Status: </strong> ${h.DocStatus}</div>
                            <div style="margin-bottom:3px;"><strong>Document Date: </strong> ${h.DocDate}</div>
                            <div style="margin-bottom:3px;"><strong>Created By: </strong> ${h.CreatedBy}</div>
                            <div style="margin-bottom:3px;"><strong>Remarks: </strong> ${h.Remarks}</div><hr/>`;


                        let tableHTML = `<h2>Goods Receipt Purchase Order Details</h2>
                                            <table border="1" cellspacing="0" cellpadding="6">
                                                <thead>
                                                    <tr>
                                                        <th>Id</th>
                                                        <th>GRPO No</th>
                                                        <th>Line Id</th>
                                                        <th>Item Code</th>
                                                        <th>Order Quantity</th>
                                                        <th>Received Quantity</th>
                                                        <th>Unit Price</th>
                                                        <th>Currency Name</th>
                                                        <th>Total Amount</th>
                                                        <th>Batch Num</th>
                                                        <th>Expiry Date</th>
                                                        <th>Remarks</th>
                                                    </tr>
                                                </thead>
                                                <tbody>`;

                        h.GrpoDtls.forEach((item) => {
                            tableHTML += `<tr><td>${item.Id}</td>
                                    <td>${item.GRPO_Id}</td>
                                    <td>${item.LineId}</td>
                                    <td>${item.ItemCode}</td>
                                    <td>${item.OrderQuantity}</td>
                                    <td>${item.ReceivedQty}</td>
                                    <td>${item.UnitPrice}</td>
                                    <td>${item.CurrencyName}</td>
                                    <td>${item.TotalAmount}</td>
                                    <td>${item.BatchNum}</td>
                                    <td>${item.ExpireDt}</td>
                                    <td>${item.Remarks}</td>
                                </tr>`;});

                        tableHTML +=`</tbody></table>`;
                        dtlContainer.innerHTML = tableHTML;

                    }else{
                        hdrContainer.innerHTML = `<p style="color:red; font-weight:bold;font-size:15px;">
                                                        ${res.ResponseMsg || "Something went wrong!"}</p>`;
                    }
                }).catch((err) => {
                    console.log(err);
                    const hdrContainer = document.getElementById("GRPO-Hdr-Res");
                    hdrContainer.innerHTML = `<div style="margin-top:20px;font-size:15px;text-align:'center';">
                                                                    <strong>Exception due to:- </strong>${err.message}</div>`;
                });
        })
    </script>
</asp:Content>

