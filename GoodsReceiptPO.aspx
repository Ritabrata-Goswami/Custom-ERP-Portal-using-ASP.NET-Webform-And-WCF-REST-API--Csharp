<%@ Page Title="ERP Portal - Goods Receipt Purchase Order" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="GoodsReceiptPO.aspx.cs" Inherits="GoodsReceiptPO" %>

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
        .GRPO-Hdr-Container,.GRPO-Dtl-Container{
            width:100%;
            padding:10px 7px;
            background-color:#d7d7d7;
            border-radius:5px;
            box-shadow:#a4a3a3 0px 5px;
            box-sizing: border-box;
        }
        .GRPO-Hdr-Txt,.GRPO-Dtl-Txt{
            font-size:15px;
            font-weight:bold;
            /*text-align:center;*/
        }

        .GRPO-Hrd-Flds,.GRPO-Dtl-Flds{
            margin-top:20px;
            display:flex;
            gap:10px;
            flex-wrap:wrap;
        }
        .GRPO-Fld{
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
            <div class="hdr-txt">Enter Goods Receipt Purchase Order</div>
            <div class="GRPO-Hdr-Container">
                <div class="GRPO-Hdr-Txt">Enter GRPO Header</div>
                <div class="GRPO-Hrd-Flds">
                    <div class="GRPO-Fld">
                        <div class="levels">GRPO Number:</div>
                        <asp:TextBox runat="server" ID="txtGRPONum" placeholder="Enter GRPO Number..." CssClass="input-fld"></asp:TextBox>
                    </div>
                    <div class="GRPO-Fld">
                        <div class="levels">PO Number:</div>
                        <div style="display:flex; flex-direction:column">
                            <asp:TextBox runat="server" ID="txtPONum" placeholder="Enter PO Number..." CssClass="input-fld"></asp:TextBox>
                            <button type="button" class="PopupBtn" onclick="OpenPopup_Hdr(event, 'po')" style="width:150px;">Open Purchase Order</button>
                        </div>
                    </div>
                    <div class="GRPO-Fld">
                        <div class="levels">Vendor Code:</div>
                        <div style="display:flex; flex-direction:column">
                            <asp:TextBox runat="server" ID="txtVendCode" placeholder="Enter Vendor Code..." CssClass="input-fld vnd-code"></asp:TextBox>
                            <button type="button" class="PopupBtn" onclick="OpenPopup_Hdr(event, 'vendor')" style="width:150px;">Open Vendor Master</button>
                        </div>
                    </div>
                    <div class="GRPO_Fld">
                        <div class="levels">Warehouse Code:</div>
                        <div style="display:flex; flex-direction:column">
                            <asp:TextBox runat="server" ID="txtWhsCode" placeholder="Enter Warehouse Code..." CssClass="input-fld whs-code"></asp:TextBox>
                            <button type="button" class="PopupBtn" onclick="OpenPopup_Hdr(event, 'warehouse')" style="width:150px;">Open Whs Master</button>
                        </div>
                    </div>
                    <div class="GRPO-Fld">
                        <div class="levels">Delivery Date:</div>
                        <asp:TextBox runat="server" ID="txtExpectedDlvDate" placeholder="Enter Delivery Date..." TextMode="Date" CssClass="input-fld"></asp:TextBox>
                    </div>
                    <div class="GRPO-Fld">
                        <div class="levels">GRPO Receipt By:</div>
                        <div style="display:flex; flex-direction:column">
                            <asp:TextBox runat="server" ID="txtReiptBy" placeholder="Enter Recipient Code..." CssClass="input-fld vnd-code"></asp:TextBox>
                            <button type="button" class="PopupBtn" onclick="OpenPopup_Hdr(event, 'employee')" style="width:150px;">Open Employee</button>
                        </div>
                    </div>
                    <div class="GRPO-Fld">
                        <div class="levels">Document Status:</div>
                        <asp:DropDownList ID="ddlDocStatus" runat="server" CssClass="input-fld"></asp:DropDownList>
                    </div>
                    <div class="GRPO-Fld">
                        <div class="levels">GRPO Remarks:</div>
                        <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2" placeholder="Enter GRPO Remarks..." CssClass="textarea-fld"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="GRPO-Dtl-Container" style="margin-top:25px;" id="PO-Line-Container">
                <div class="GRPO-Dtl-Txt">Enter GRPO Details</div>
                <div class="GRPO-Dtl-Flds" style="border:0.5px solid #757272; border-radius:5px; padding:5px 5px;" id="Line-Fields-Container">
                    <div class="GRPO-Fld">
                        <div class="levels">Item Code:</div>
                        <div style="display:flex; flex-direction:column">
                            <asp:TextBox runat="server" ID="txtItemCode" placeholder="Enter Item Code..." CssClass="input-fld item-code"></asp:TextBox>
                            <button type="button" class="PopupBtn" onclick="OpenPopup_Dtl(event, 'item',this)" style="width:150px;">Open Item Master</button>
                        </div>
                    </div>
                    <div class="GRPO-Fld">
                        <div class="levels">Order Quantity:</div>
                        <asp:TextBox runat="server" ID="txtOrdQuantity" TextMode="Number" step="0.01" placeholder="Enter Item Order Quantity..." CssClass="input-fld ord-qty"></asp:TextBox>
                    </div>
                    <div class="GRPO-Fld">
                        <div class="levels">Received Quantity:</div>
                        <asp:TextBox runat="server" ID="txtReivdQty" TextMode="Number" step="0.01" placeholder="Enter Item Received Quantity..." CssClass="input-fld rcvd-qty"></asp:TextBox>
                    </div>
                    <div class="GRPO-Fld">
                        <div class="levels">Price/Unit:</div>
                        <asp:TextBox runat="server" ID="txtPrice" TextMode="Number" step="0.01" placeholder="Enter Item Price..." CssClass="input-fld price-unit"></asp:TextBox>
                    </div>
                    <div class="GRPO-Fld">
                        <div class="levels">Currency Code:</div>
                        <div style="display:flex; flex-direction:column">
                            <asp:TextBox runat="server" ID="txtCurrencyCode" placeholder="Enter Currency Code..." CssClass="input-fld currency-code"></asp:TextBox>
                            <button type="button" class="PopupBtn" onclick="OpenPopup_Dtl(event, 'currency',this)" style="width:150px;">Open Item Master</button>
                        </div>
                    </div>
                    <div class="GRPO-Fld">
                        <div class="levels">Batch Number:</div>
                        <asp:TextBox runat="server" ID="txtBatch" placeholder="Enter Batch Number..." CssClass="input-fld batchNum"></asp:TextBox>
                    </div>
                    <div class="GRPO-Fld">
                        <div class="levels">Expire Date:</div>
                        <asp:TextBox runat="server" ID="txtTax" TextMode="Date" placeholder="Enter expire date..." CssClass="input-fld exp-date"></asp:TextBox>
                    </div>
                    <div class="GRPO-Fld">
                        <div class="levels">Remarks:</div>
                        <asp:TextBox runat="server" ID="txtRemarksDtl" TextMode="MultiLine" Rows="2" placeholder="Enter Line Remarks..." CssClass="textarea-fld remarks"></asp:TextBox>
                    </div>
                </div>
                <div id="Add-Line"></div>
                <div class="BtnContainer" id="BtnContainer">
                    <asp:Button runat="server" ID="AddRowBtn" Text="Add Row +" CssClass="AddRowBtn" />
                    <asp:Button runat="server" ID="SaveData" Text="Save GRPO Data" CssClass="SaveBtn"  OnClientClick="collectGRPOData(event)"/>
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

            const parentDiv = btn.closest('.GRPO-Fld');
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
                case "warehouse":
                    document.getElementById("<%=txtWhsCode.ClientID%>").value = getVal;
                    break;
                case "employee":
                    document.getElementById("<%=txtReiptBy.ClientID%>").value = getVal;
                    break;
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
            const rows = container.querySelectorAll('.GRPO-Dtl-Flds');
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


        function collectGRPOData(e) {
            e.preventDefault();

            const UrlObj = new URLSearchParams(window.location.search);
            let EmpIdVal = UrlObj.get("empId");

            const header = {
                GRPO_No: document.getElementById("<%= txtGRPONum.ClientID %>").value,
                PO_No: document.getElementById("<%= txtPONum.ClientID %>").value,
                VendorCode:document.getElementById("<%= txtVendCode.ClientID%>").value,
                WarehouseCode:document.getElementById("<%= txtWhsCode.ClientID%>").value,
                ReceivedDt: document.getElementById("<%= txtExpectedDlvDate.ClientID %>").value,
                ReceivedBy:document.getElementById("<%= txtReiptBy.ClientID %>").value,
                DocStatus:document.getElementById("<%= ddlDocStatus.ClientID %>").value,
                CreatedBy: EmpIdVal,
                Remarks: document.getElementById("<%= txtRemarks.ClientID %>").value,
                GrpoDtls: []
            };

            const allLineContainers = document.querySelectorAll("#Line-Fields-Container, #Add-Line .GRPO-Dtl-Flds");

            allLineContainers.forEach(container => {
                const line = {
                    ItemCode: container.querySelector(".item-code")?.value || "",
                    OrderQuantity: container.querySelector(".ord-qty")?.value || "",
                    ReceivedQty: container.querySelector(".rcvd-qty")?.value || "",
                    UnitPrice: container.querySelector(".price-unit")?.value || "",
                    CurrencyCode: container.querySelector(".currency-code")?.value || "",
                    BatchNum: container.querySelector(".batchNum")?.value || "",
                    ExpireDt: container.querySelector(".exp-date")?.value||"",
                    Remarks: container.querySelector(".remarks")?.value || ""
                };
                header.GrpoDtls.push(line);
            });

            console.log(header);

                                            //Sending JSON to WCF endpoint.
            fetch("http://localhost:49808/ERP_Web_Portal.svc/PostGRPO", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({ A_Cls_Grpo: header })
            }).then((res) => res.json()).then((res) => {
                //console.log(res);

                alert(res.PostGRPOResult.ResponseMsg);
            }).catch((err) => {
                console.log(err.message);
                alert(err.message);
            });

        }
    </script>
</asp:Content>

