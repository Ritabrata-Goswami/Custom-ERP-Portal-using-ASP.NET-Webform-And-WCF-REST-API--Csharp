<%@ Page Title="ERP Portal - View A/P Invoice" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ViewAPInvoice.aspx.cs" Inherits="ViewAPInvoice" %>

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
            #search-AP-Inv{
                padding:7px 10px;
                cursor:pointer;
                border:none;
                border-radius:3px;
                background-color:#07cd6a;
                color:antiquewhite;
            }
            #search-AP-Inv:hover{
                transition:0.5s;
                background-color:#099931;
            }
            .data-part{margin-bottom:20px;}
            th{
                background-color:#2b8aea;
                color:aliceblue;
            }

            .EditBtn{
                border:none;
                background-color:#16ad61;
                color:whitesmoke;
                padding:8px 8px;
                border-radius:3px;
                font-weight:bold;
                font-size:14px;
                cursor:pointer;
            }
            .EditBtn:hover{
                transition:0.6s;
                background-color:#15e669
            }
        </style>
        <div>
            <div class="ParentContainer">
                <div class="header-part">
                    <div class="HdrTxt01">View Account Purchase Invoices</div>
                    <div class="HdrTxt02">Enter A/P Invoice Number to view Account Purchase Invoices</div>
                    <div class="search-container">
                        <input type="text" placeholder="Enter invoice number..." name="enter_grpo" class="input-box" id="enterAPInvoice" />
                        <button id="search-AP-Inv">Find A/P Invoice</button>
                    </div>
                </div>
                <div class="data-part">
                    <div id="AP-Hdr-Res"></div>
                    <div id="AP-Dtl-Res"></div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            window.document.getElementById("search-AP-Inv").addEventListener("click", (event) => {
                event.preventDefault();

                let getAPInvNum = document.getElementById("enterAPInvoice").value;
                getAPInvNum = getAPInvNum.trim();

                if (getAPInvNum === "") {
                    alert("Please enter a valid AP invoice number!");
                    return false;
                }

                fetch(`http://localhost:49808/ERP_Web_Portal.svc/GetAPInv?InvNum=${getAPInvNum}`)
                    .then((res) => res.json())
                    .then((res) => {
                        //console.log(res);

                        const hdrContainer = document.getElementById("AP-Hdr-Res");
                        const dtlContainer = document.getElementById("AP-Dtl-Res");

                                    //Clear previous results
                        hdrContainer.innerHTML = "";
                        dtlContainer.innerHTML = "";

                        if (res.StatusCode === 200 && res.ApHdr) {
                            const h = res.ApHdr;
                            hdrContainer.innerHTML = `<h2>Account Purchase Invoices Header</h2>
                                <div style="margin-bottom:3px;"><strong>A/P Invoice Number: </strong> ${h.InvNo}</div>
                                <div style="margin-bottom:3px;"><strong>PO Number: </strong> ${h.PONo}</div>
                                <div style="margin-bottom:3px;"><strong>GRPO Number: </strong> ${h.GRPONo}</div>
                                <div style="margin-bottom:3px;"><strong>Document Date(DocDate): </strong> ${h.DocDate}</div>
                                <div style="margin-bottom:3px;"><strong>Posting Date: </strong> ${h.PostingDate}</div>
                                <div style="margin-bottom:3px;"><strong>Doc Due Date: </strong> ${h.DueDate}</div>
                                <div style="margin-bottom:3px;"><strong>Net Amount Paid: </strong> ${h.NetAmount}</div>
                                <div style="margin-bottom:3px;"><strong>Document Status: </strong> ${h.Status}</div>
                                <div style="margin-bottom:3px;"><strong>Currency: </strong> ${h.Currency}</div>

                                <div style="margin-bottom:3px;"><strong>Created By (ID): </strong> ${h.CreatedBy}</div>
                                <div style="margin-bottom:3px;"><strong>Created By (Name): </strong> ${h.CreatedByName}</div>
                                <div style="margin-bottom:3px;"><strong>Created Time: </strong> ${h.CreatedOn}</div>
                                <div style="margin-bottom:3px;"><strong>Modified By: </strong> ${h.ModifiedBy}</div>
                                <div style="margin-bottom:3px;"><strong>Modified On: </strong> ${h.ModifiedOn}</div>
                                <div style="margin-bottom:3px;"><strong>Vendor Code: </strong> ${h.Vendor_Id}</div>

                                <div style="margin-bottom:3px;"><strong>Remarks: </strong> ${h.Remarks}</div><hr/>`;


                            let tableHTML = `<h2>Account Purchase Invoices Details</h2>
                                                <table border="1" cellspacing="0" cellpadding="6">
                                                    <thead>
                                                        <tr>
                                                            <th>Id</th>
                                                            <th>A/P Invoice No</th>
                                                            <th>Item Code</th>
                                                            <th>Line Total</th>
                                                            <th>Quantity</th>
                                                            <th>Tax Code</th>
                                                            <th>Unit Price</th>
                                                            <th>Remarks</th>
                                                            <th>Open Edit</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>`;

                            h.ApInvDtls.forEach((item) => {
                                tableHTML += `<tr><td>${item.Id}</td>
                                        <td>${item.InvNo}</td>
                                        <td>${item.ItemCode}</td>
                                        <td>${item.LineTotal}</td>
                                        <td>${item.Quantity}</td>
                                        <td>${item.TaxCode}</td>
                                        <td>${item.UnitPrice}</td>
                                        <td>${item.RemarksDtl}</td>
                                        <td><a href="/EditAPInv.aspx?Id=${item.Id}&InvNo=${item.InvNo}" id="Line-${item.Id}" target="_blank"><button type="button" class="EditBtn">Edit</button></a></td>
                                    </tr>`;});

                            tableHTML +=`</tbody></table>`;
                            dtlContainer.innerHTML = tableHTML;

                        }else{
                            hdrContainer.innerHTML = `<p style="color:red; font-weight:bold;font-size:15px;">
                                                            ${res.ResponseMsg || "Something went wrong!"}</p>`;
                        }
                    }).catch((err) => {
                        console.log(err);
                        const hdrContainer = document.getElementById("AP-Hdr-Res");
                        hdrContainer.innerHTML = `<div style="margin-top:20px;font-size:15px;text-align:'center';">
                                                                        <strong>Exception due to:- </strong>${err.message}</div>`;
                    });
            })
        </script>
</asp:Content>

