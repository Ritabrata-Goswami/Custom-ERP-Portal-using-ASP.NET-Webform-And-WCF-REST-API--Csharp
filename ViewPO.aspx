<%@ Page Title="ERP Portal - View Purchase Order" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ViewPO.aspx.cs" Inherits="ViewPO" %>

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
        #search-PO{
            padding:7px 10px;
            cursor:pointer;
            border:none;
            border-radius:3px;
            background-color:#07cd6a;
            color:antiquewhite;
        }
        #search-PO:hover{
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
                <div class="HdrTxt01">View Purchase Order</div>
                <div class="HdrTxt02">Enter PO Number to view purchase order</div>
                <div class="search-container">
                    <input type="text" placeholder="Enter PO number..." name="enter_po" class="input-box" id="enterPONum"/>
                    <button id="search-PO">Find PO</button>
                </div>
            </div>
            <div class="data-part">
                <div id="PO-Hdr-Res"></div>
                <div id="PO-Dtl-Res"></div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        window.document.getElementById("search-PO").addEventListener("click", (event) => {
            event.preventDefault();

            let getPONum = document.getElementById("enterPONum").value;
            getPONum = getPONum.trim();

            if (getPONum === "") {
                alert("Please enter a valid PO number!");
                return false;
            }

            fetch(`http://localhost:49808/ERP_Web_Portal.svc/GetPO?PoNum=${getPONum}`)
                .then((res) => res.json())
                .then((res) => {
                    //console.log(res);

                    const hdrContainer = document.getElementById("PO-Hdr-Res");
                    const dtlContainer = document.getElementById("PO-Dtl-Res");

                                //Clear previous results
                    hdrContainer.innerHTML = "";
                    dtlContainer.innerHTML = "";

                    if (res.StatusCode === 200 && res.PoHdr) {
                        const h = res.PoHdr;
                        hdrContainer.innerHTML = `<h2>Purchase Order Header</h2>
                            <div style="margin-bottom:3px;"><strong>PO Number: </strong> ${h.PONumber}</div>
                            <div style="margin-bottom:3px;"><strong>PO Date: </strong> ${h.PODate}</div>
                            <div style="margin-bottom:3px;"><strong>Employee: </strong> ${h.EmpName} (${h.EmpId})</div>
                            <div style="margin-bottom:3px;"><strong>Status: </strong> ${h.POStatus}</div>
                            <div style="margin-bottom:3px;"><strong>Payment Terms: </strong> ${h.PaymentTerms}</div>
                            <div style="margin-bottom:3px;"><strong>Currency: </strong> ${h.Currency}</div>
                            <div style="margin-bottom:3px;"><strong>Approved By: </strong> ${h.ApprovedBy}</div>
                            <div style="margin-bottom:3px;"><strong>Approved Date: </strong> ${h.ApprovedDate}</div>
                            <div style="margin-bottom:3px;"><strong>Expected Delivery: </strong> ${h.ExpectedDlvDate}</div>
                            <div style="margin-bottom:3px;"><strong>Total Amount: </strong> ${h.TotalAmount}</div>
                            <div style="margin-bottom:3px;"><strong>Remarks: </strong> ${h.Remarks}</div><hr/>`;


                        let tableHTML = `<h2>Purchase Order Details</h2>
                                            <table border="1" cellspacing="0" cellpadding="6">
                                                <thead>
                                                    <tr>
                                                        <th>Item Code</th>
                                                        <th>Quantity</th>
                                                        <th>Price</th>
                                                        <th>Discount %</th>
                                                        <th>Tax %</th>
                                                        <th>Line Total</th>
                                                        <th>Vendor Code</th>
                                                        <th>Warehouse Code</th>
                                                        <th>Remarks</th>
                                                    </tr>
                                                </thead>
                                                <tbody>`;

                        h.DtlPO.forEach((item) => {
                            tableHTML += `<tr><td>${item.ItemCode}</td>
                                    <td>${item.Quantity}</td>
                                    <td>${item.Price}</td>
                                    <td>${item.Discount}</td>
                                    <td>${item.Tax}</td>
                                    <td>${item.LineTotal}</td>
                                    <td>${item.VendorCode}</td>
                                    <td>${item.WarehouseCode}</td>
                                    <td>${item.RemarksDtl}</td>
                                </tr>`;});

                        tableHTML +=`</tbody></table>`;
                        dtlContainer.innerHTML = tableHTML;

                    }else{
                        hdrContainer.innerHTML = `<p style="color:red; font-weight:bold;font-size:15px;">
                                                        ${res.ResponseMsg || "Something went wrong!"}</p>`;
                    }
                }).catch((err) => {
                    console.log(err);
                    const hdrContainer = document.getElementById("PO-Hdr-Res");
                    hdrContainer.innerHTML = `<div style="margin-top:20px;font-size:15px;text-align:'center';">
                                                                    <strong>Exception due to:- </strong>${err.message}</div>`;
                });
        })
    </script>
</asp:Content>

