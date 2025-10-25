<%@ Page Title="ERP Portal - A/P Invoice" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PostAPInvoice.aspx.cs" Inherits="PostAPInvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
        <style type="text/css">
        .hdr-container{
            padding:8px 5px;

        }
        .hdr-txt{
            font-size:20px;
            font-weight:bold;
            margin-bottom:10px;
        }
        .AP-Hdr-Container,.AP-Dtl-Container{
            width:100%;
            padding:10px 7px;
            background-color:#d7d7d7;
            border-radius:5px;
            box-shadow:#a4a3a3 0px 5px;
            box-sizing: border-box;
        }
        .AP-Hdr-Txt,.AP-Dtl-Txt{
            font-size:15px;
            font-weight:bold;
            /*text-align:center;*/
        }

        .AP-Hrd-Flds,.AP-Dtl-Flds{
            margin-top:20px;
            display:flex;
            gap:10px;
            flex-wrap:wrap;
        }
        .AP-Fld{
            padding: 1px 2px;
            flex-direction:row;
        }
        .levels{
            font-size:13px;
            font-weight:bold;
            margin-bottom:2px;
        }
        .input-fld{
            padding:5px 3px;
            font-size:13px;
            border:0.5px solid #b0adad;
            border-radius:3px;
            width:200px;
        }
        .textarea-fld{
            width:350px;
            height:30px;
            padding:5px 3px;
            font-size:13px;
            border:0.5px solid #b0adad;
            border-radius:3px;
        }

        .PopupBtn{
            /*width:30px;*/
            /*height:20px;*/
            margin-top:5px;
            padding:7px 10px;
            border:none;
            cursor:pointer;
            font-size:12px;
            background-color:coral;
            color:#2c1f1f;
            border-radius:20px;
        }
        .PopupBtn:hover{
            background-color:#e00a0a;
            transition:0.5s;
        }

        .BtnContainer{
            margin-top:15px; 
            display:flex; 
            flex-direction:row;
            gap:8px;
        }
        .AddRowBtn{
            border:none;
            border-radius:3px;
            padding:7px 10px;
            cursor:pointer;
            background-color:#047af1;
            color:antiquewhite;
            font-size:13px;
        }
        .AddRowBtn:hover{
            transition:0.5s;
            background-color:#064bd8;
        }
        .SaveBtn{
            border:none;
            border-radius:3px;
            padding:7px 10px;
            cursor:pointer;
            background-color:#07b85f;
            color:antiquewhite;
            font-size:13px;
        }
        .SaveBtn:hover{
            transition:0.5s;
            background-color:#009875;
        }
        .delete-row-btn{
            border:none;
            border-radius:3px;
            height:30px;
            padding:7px 10px;
            cursor:pointer;
            background-color:#ea1010;
            color:antiquewhite;
            font-size:15px;
        }
    </style>
    <div>
        <div class="hdr-container">
            <div class="hdr-txt">Enter Account Purchase Invoice</div>
            <div class="AP-Hdr-Container">
                <div class="AP-Hdr-Txt">Enter A/P Invoice Header</div>
                <div class="AP-Hrd-Flds">
                    <div class="AP-Fld">
                        <div class="levels">A/P Invoice Number:</div>
                        <asp:TextBox runat="server" ID="txtAPNum" placeholder="Enter A/P Inv Number..." CssClass="input-fld"></asp:TextBox>
                    </div>
                    <div class="AP-Fld">
                        <div class="levels">Vendor Code:</div>
                        <div style="display:flex; flex-direction:column">
                            <asp:TextBox runat="server" ID="txtVendCode" placeholder="Enter Vendor Code..." CssClass="input-fld vnd-code"></asp:TextBox>
                            <button type="button" class="PopupBtn" onclick="OpenPopup_Hdr(event, 'vendor')" style="width:150px;">Open Vendor Master</button>
                        </div>
                    </div>
                    <div class="AP-Fld">
                        <div class="levels">PO Number:</div>
                        <div style="display:flex; flex-direction:column">
                            <asp:TextBox runat="server" ID="txtPONum" placeholder="Enter PO Number..." CssClass="input-fld"></asp:TextBox>
                            <button type="button" class="PopupBtn" onclick="OpenPopup_Hdr(event, 'po')" style="width:150px;">Open Purchase Order</button>
                        </div>
                    </div>
                    <div class="AP_Fld">
                        <div class="levels">GRPO Number:</div>
                        <div style="display:flex; flex-direction:column">
                            <asp:TextBox runat="server" ID="txtGrpoCode" placeholder="Enter Grpo Number..." CssClass="input-fld whs-code"></asp:TextBox>
                            <button type="button" class="PopupBtn" onclick="OpenPopup_Hdr(event, 'grpo')" style="width:150px;">Open GRPO</button>
                        </div>
                    </div>
                    <div class="AP-Fld">
                        <div class="levels">Document Date:</div>
                        <asp:TextBox runat="server" ID="txtDocDate" placeholder="Enter Document Date..." TextMode="Date" CssClass="input-fld"></asp:TextBox>
                    </div>
                    <div class="AP-Fld">
                        <div class="levels">Posting Date:</div>
                        <asp:TextBox runat="server" ID="txtPostingDate" placeholder="Enter Posting Date..." TextMode="Date" CssClass="input-fld"></asp:TextBox>
                    </div>
                    <div class="AP-Fld">
                        <div class="levels">Due Date:</div>
                        <asp:TextBox runat="server" ID="txtDueDate" placeholder="Enter Due Date..." TextMode="Date" CssClass="input-fld"></asp:TextBox>
                    </div>
                    <div class="AP-Fld">
                        <div class="levels">Currency:</div>
                        <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="input-fld"></asp:DropDownList>
                    </div>
                    <div class="AP-Fld">
                        <div class="levels">Document Total:</div>
                        <asp:TextBox runat="server" ID="txtDocTotal" placeholder="Enter Total Amount..." TextMode="Number" step="0.01" CssClass="input-fld vnd-code"></asp:TextBox>
                    </div>
                    <div class="AP-Fld">
                        <div class="levels">Tax Amount:</div>
                        <asp:TextBox runat="server" ID="txtTaxAmt" placeholder="Enter Total Tax Amount..." TextMode="Number" step="0.01" CssClass="input-fld vnd-code"></asp:TextBox>
                    </div>
                    <div class="AP-Fld">
                        <div class="levels">Discount Amount:</div>
                        <asp:TextBox runat="server" ID="txtDiscount" placeholder="Enter Total Discount Amount..." TextMode="Number" step="0.01" CssClass="input-fld vnd-code"></asp:TextBox>
                    </div>
                    <div class="AP-Fld">
                        <div class="levels">Document Status:</div>
                        <asp:DropDownList ID="ddlDocStatus" runat="server" CssClass="input-fld"></asp:DropDownList>
                    </div>
                    <div class="AP-Fld">
                        <div class="levels">A/P Inv Remarks:</div>
                        <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2" placeholder="Enter A/P Invoice Remarks..." CssClass="textarea-fld"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="AP-Dtl-Container" style="margin-top:25px;" id="AP-Line-Container">
                <div class="AP-Dtl-Txt">Enter GRPO Details</div>
                <div class="AP-Dtl-Flds" style="border:0.5px solid #757272; border-radius:5px; padding:5px 5px;" id="Line-Fields-Container">
                    <div class="AP-Fld">
                        <div class="levels">Item Code:</div>
                        <div style="display:flex; flex-direction:column">
                            <asp:TextBox runat="server" ID="txtItemCode" placeholder="Enter Item Code..." CssClass="input-fld item-code"></asp:TextBox>
                            <button type="button" class="PopupBtn" onclick="OpenPopup_Dtl(event, 'item',this)" style="width:150px;">Open Item Master</button>
                        </div>
                    </div>
                    <div class="AP-Fld">
                        <div class="levels">Received Quantity:</div>
                        <asp:TextBox runat="server" ID="txtReivdQty" TextMode="Number" step="0.01" placeholder="Enter Item Received Quantity..." CssClass="input-fld rcvd-qty"></asp:TextBox>
                    </div>
                    <div class="AP-Fld">
                        <div class="levels">Price/Unit:</div>
                        <asp:TextBox runat="server" ID="txtPrice" TextMode="Number" step="0.01" placeholder="Enter Item Price..." CssClass="input-fld price-unit"></asp:TextBox>
                    </div>
                    <div class="AP-Fld">
                        <div class="levels">TaxCode:</div>
                        <asp:DropDownList ID="ddlTaxCodeDtl" runat="server" CssClass="input-fld txt-code"></asp:DropDownList>
                    </div>

                    <div class="AP-Fld">
                        <div class="levels">Remarks:</div>
                        <asp:TextBox runat="server" ID="txtRemarksDtl" TextMode="MultiLine" Rows="2" placeholder="Enter Line Remarks..." CssClass="textarea-fld remarks"></asp:TextBox>
                    </div>
                </div>
                <div id="Add-Line"></div>
                <div class="BtnContainer" id="BtnContainer">
                    <asp:Button runat="server" ID="AddRowBtn" Text="Add Row +" CssClass="AddRowBtn" />
                    <asp:Button runat="server" ID="SaveData" Text="Save A/P Inv Data" CssClass="SaveBtn"  OnClientClick="collectAPInvoiceData(event)"/>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        let activeInput = null;

        function OpenPopup_Hdr(e, s_arg) {
            e.preventDefault();

            let PopupWidth = screen.width - 200;
            let PopupHeight = screen.height - 100;

            let ScreenWidth = screen.width;
            let ScreenHeight = screen.height;
            let VerticalSpace = (ScreenHeight - PopupHeight) / 2;
            let HorizontalSpace = (ScreenWidth - PopupWidth) / 2;

            let PopupDefn = "width=" + PopupWidth + ",height=" + PopupHeight + ",top=" + VerticalSpace + ",left=" + HorizontalSpace + ",scrollbars=yes";

            window.open("/PopupMaster.aspx?popup_type=" + s_arg, s_arg, PopupDefn);
        }

        function OpenPopup_Dtl(e, s_arg, btn) {
            e.preventDefault();

            const parentDiv = btn.closest('.AP-Fld');
            activeInput = parentDiv.querySelector('input');

            let PopupWidth = screen.width - 200;
            let PopupHeight = screen.height - 100;

            let ScreenWidth = screen.width;
            let ScreenHeight = screen.height;
            let VerticalSpace = (ScreenHeight - PopupHeight) / 2;
            let HorizontalSpace = (ScreenWidth - PopupWidth) / 2;

            let PopupDefn = "width=" + PopupWidth + ",height=" + PopupHeight + ",top=" + VerticalSpace + ",left=" + HorizontalSpace + ",scrollbars=yes";

            window.open("/PopupMaster.aspx?popup_type=" + s_arg, s_arg, PopupDefn);
        }

        function SetPopupValue(getVal, popupTypeVal) {
            switch (popupTypeVal) {
                case "vendor":
                    document.getElementById("<%=txtVendCode.ClientID%>").value = getVal;
                    break;
                case "grpo":
                    document.getElementById("<%=txtGrpoCode.ClientID%>").value = getVal;
                    break;
                <%--case "item":
                    document.getElementById("<%=txtItemCode.ClientID%>").value = getVal;
                    break;--%>
                case "po":
                    document.getElementById("<%=txtPONum.ClientID%>").value = getVal;
                    break;
            }

            if (activeInput) {
                activeInput.value = getVal;
                activeInput = null;     //clear after setting
            } else {
                console.warn("No active input field to update!");
            }
        }


        function addLineRow() {
            const container = document.getElementById('Add-Line');
            const template = document.getElementById('Line-Fields-Container');
            const BtnContainer = document.getElementById('BtnContainer');

                            //Clone and reset values
            const newRow = template.cloneNode(true);
            newRow.removeAttribute('id'); // remove duplicate id
            const inputs = newRow.querySelectorAll('input, textarea');
            inputs.forEach(i => i.value = '');


                                        //Create Delete button
            const delBtn = document.createElement('button');
            delBtn.type = 'button';
            delBtn.innerText = 'Delete This Row';
            delBtn.className = 'delete-row-btn';
            delBtn.style.marginTop = '22px';

                                        //Attach click event to delete the row
            delBtn.addEventListener('click', function () {
                if (container.children.length >= 1) {
                    newRow.remove();
                    updateDeleteButtons();
                }
            });

            newRow.appendChild(delBtn);

            container.appendChild(newRow);
            updateDeleteButtons();
        }

        function updateDeleteButtons() {
            const container = document.getElementById('Add-Line');
            const rows = container.querySelectorAll('.AP-Dtl-Flds');
            rows.forEach(row => {
                const btn = row.querySelector('.delete-row-btn');
                if (btn) {
                    btn.style.display = rows.length >= 1 ? 'inline-block' : 'none';
                }
            });
        }

        document.getElementById('<%= AddRowBtn.ClientID %>').addEventListener('click', function (e) {
            e.preventDefault(); // prevent postback
            addLineRow();
        });


        function collectAPInvoiceData(e) {
            e.preventDefault();

            const UrlObj = new URLSearchParams(window.location.search);
            let EmpIdVal = UrlObj.get("empId");

            const header = {
                InvNo: document.getElementById("<%= txtAPNum.ClientID %>").value,
                Vendor_Id: document.getElementById("<%= txtVendCode.ClientID %>").value,
                PONo:document.getElementById("<%= txtPONum.ClientID%>").value,
                GRPONo:document.getElementById("<%= txtGrpoCode.ClientID%>").value,
                DocDate: document.getElementById("<%= txtDocDate.ClientID %>").value,
                PostingDate:document.getElementById("<%= txtPostingDate.ClientID %>").value,
                DueDate:document.getElementById("<%= txtDueDate.ClientID %>").value,
                Currency: document.getElementById("<%= ddlCurrency.ClientID %>").value,
                DocTotal: document.getElementById("<%= txtDocTotal.ClientID %>").value,
                TaxAmount: document.getElementById("<%= txtTaxAmt.ClientID %>").value,
                Discount: document.getElementById("<%= txtDiscount.ClientID %>").value,
                Status: document.getElementById("<%= ddlDocStatus.ClientID %>").value,
                CreatedBy: EmpIdVal,
                Remarks: document.getElementById("<%= txtRemarks.ClientID %>").value,
                ApInvDtls: []
            };

            const allLineContainers = document.querySelectorAll("#Line-Fields-Container, #Add-Line .AP-Dtl-Flds");

            allLineContainers.forEach(container => {
                const line = {
                    ItemCode: container.querySelector(".item-code")?.value || "",
                    Quantity: container.querySelector(".rcvd-qty")?.value || "",
                    UnitPrice: container.querySelector(".price-unit")?.value || "",
                    TaxCode: container.querySelector(".txt-code")?.value || "",
                    RemarksDtl: container.querySelector(".remarks")?.value || ""
                };
                header.ApInvDtls.push(line);
            });

            //console.log(header);

                                            //Sending JSON to WCF endpoint.
            fetch("http://localhost:49808/ERP_Web_Portal.svc/PostAPInv", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({ A_Cls_ApInv: header })
            }).then((res) => res.json()).then((res) => {
                //console.log(res);

                alert(res.PostApInvResult.ResponseMsg);
            }).catch((err) => {
                console.log(err.message);
                alert(err.message);
            });

        }
    </script>
</asp:Content>

