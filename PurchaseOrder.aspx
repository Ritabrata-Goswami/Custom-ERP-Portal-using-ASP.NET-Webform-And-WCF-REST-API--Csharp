<%@ Page Title="ERP Portal - Purchase Order" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PurchaseOrder.aspx.cs" Inherits="PurchaseOrder" %>

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
        .PO-Hdr-Container,.PO-Dtl-Container{
            width:100%;
            padding:10px 7px;
            background-color:#d7d7d7;
            border-radius:5px;
            box-shadow:#a4a3a3 0px 5px;
            box-sizing: border-box;
        }
        .PO-Hdr-Txt,.PO-Dtl-Txt{
            font-size:15px;
            font-weight:bold;
            /*text-align:center;*/
        }

        .PO-Hrd-Flds,.PO-Dtl-Flds{
            margin-top:20px;
            display:flex;
            gap:10px;
            flex-wrap:wrap;
        }
        .PO-Fld{
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
            <div class="hdr-txt">Enter Purchase Order</div>
            <div class="PO-Hdr-Container">
                <div class="PO-Hdr-Txt">Enter PO Header</div>
                <div class="PO-Hrd-Flds">
                    <div class="PO-Fld">
                        <div class="levels">PO Number:</div>
                        <asp:TextBox runat="server" ID="txtPONum" placeholder="Enter PO Number..." CssClass="input-fld"></asp:TextBox>
                    </div>
                    <div class="PO-Fld">
                        <div class="levels">PO Date:</div>
                        <asp:TextBox runat="server" ID="txtPODate" placeholder="Enter PO Date..." TextMode="Date" CssClass="input-fld"></asp:TextBox>
                    </div>
                    <div class="PO-Fld">
                        <div class="levels">Expected Delivery Date:</div>
                        <asp:TextBox runat="server" ID="txtExpectedDlvDate" placeholder="Enter Delivery Date..." TextMode="Date" CssClass="input-fld"></asp:TextBox>
                    </div>
                    <div class="PO-Fld">
                        <div class="levels">Payment Terms:</div>
                        <asp:TextBox runat="server" ID="txtPymtTrms" placeholder="Enter Payment Terms (in days)..." CssClass="input-fld"></asp:TextBox>
                    </div>
                    <div class="PO-Fld">
                        <div class="levels">Total Amount:</div>
                        <asp:TextBox runat="server" ID="txtAmount" TextMode="Number" step="0.01" placeholder="Enter Total Amount..." CssClass="input-fld"></asp:TextBox>
                    </div>
                    <div class="PO-Fld">
                        <div class="levels">Currency:</div>
                        <asp:DropDownList ID="ddlCurrencyCode" runat="server" CssClass="input-fld"></asp:DropDownList>
                    </div>
                    <div class="PO-Fld">
                        <div class="levels">PO Remarks:</div>
                        <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="2" placeholder="Enter PO Remarks..." CssClass="textarea-fld"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="PO-Dtl-Container" style="margin-top:25px;" id="PO-Line-Container">
                <div class="PO-Dtl-Txt">Enter PO Details</div>
                <div class="PO-Dtl-Flds" style="border:0.5px solid #757272; border-radius:5px; padding:5px 5px;" id="Line-Fields-Container">
                    <div class="PO-Fld">
                        <div class="levels">Item Code:</div>
                        <div style="display:flex; flex-direction:column">
                            <asp:TextBox runat="server" ID="txtItemCode" placeholder="Enter Item Code..." CssClass="input-fld item-code"></asp:TextBox>
                            <button type="button" class="PopupBtn" onclick="OpenPopup(event, 'item',this)" style="width:150px;">Open Item Master</button>
                            <%--<asp:Button runat="server" Text="Open Item Master" CssClass="PopupBtn" OnClientClick="OpenPopup(event,'item')" Width="150px"/>--%>
                        </div>
                    </div>
                    <div class="PO-Fld">
                        <div class="levels">Vendor Code:</div>
                        <div style="display:flex; flex-direction:column">
                            <asp:TextBox runat="server" ID="txtVendorCode" placeholder="Enter Vendor Code..." CssClass="input-fld vnd-code"></asp:TextBox>
                            <button type="button" class="PopupBtn" onclick="OpenPopup(event, 'vendor',this)" style="width:150px;">Open Vendor Master</button>
                            <%--<asp:Button runat="server" Text="Open Vendor Master" CssClass="PopupBtn" OnClientClick="OpenPopup(event,'vendor')" Width="150px"/>--%>
                        </div>
                    </div>
                    <div class="PO-Fld">
                        <div class="levels">Warehouse Code:</div>
                        <div style="display:flex; flex-direction:column">
                            <asp:TextBox runat="server" ID="txtWhsCode" placeholder="Enter Warehouse Code..." CssClass="input-fld whs-code"></asp:TextBox>
                            <button type="button" class="PopupBtn" onclick="OpenPopup(event, 'warehouse',this)" style="width:150px;">Open Whs Master</button>
                            <%--<asp:Button runat="server" Text="Open Whs Master" CssClass="PopupBtn" OnClientClick="OpenPopup(event,'warehouse')" Width="150px"/>--%>
                        </div>
                    </div>
                    <div class="PO-Fld">
                        <div class="levels">Quantity:</div>
                        <asp:TextBox runat="server" ID="txtQuantity" TextMode="Number" step="0.01" placeholder="Enter Item Quantity..." CssClass="input-fld qty"></asp:TextBox>
                    </div>
                    <div class="PO-Fld">
                        <div class="levels">Price/Unit:</div>
                        <asp:TextBox runat="server" ID="txtPrice" TextMode="Number" step="0.01" placeholder="Enter Item Price..." CssClass="input-fld price"></asp:TextBox>
                    </div>
                    <div class="PO-Fld">
                        <div class="levels">Discount in %:</div>
                        <asp:TextBox runat="server" ID="txtDiscount" TextMode="Number" step="0.01" placeholder="Enter Discount %..." CssClass="input-fld discount"></asp:TextBox>
                    </div>
                    <div class="PO-Fld">
                        <div class="levels">Tax in %:</div>
                        <asp:TextBox runat="server" ID="txtTax" TextMode="Number" step="0.01" placeholder="Enter Tax % (0-100)..." CssClass="input-fld tax"></asp:TextBox>
                    </div>
                    <div class="PO-Fld">
                        <div class="levels">Remarks:</div>
                        <asp:TextBox runat="server" ID="txtRemarksDtl" TextMode="MultiLine" Rows="2" placeholder="Enter Line Remarks..." CssClass="textarea-fld remarks"></asp:TextBox>
                    </div>
                </div>
                <div id="Add-Line"></div>
                <div class="BtnContainer" id="BtnContainer">
                    <asp:Button runat="server" ID="AddRowBtn" Text="Add Row +" CssClass="AddRowBtn" />
                    <asp:Button runat="server" ID="SaveData" Text="Save PO Data" CssClass="SaveBtn"  OnClientClick="collectPOData(event)"/>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        let activeInput = null;

        function OpenPopup(e, s_arg, btn) {
            e.preventDefault();

            const parentDiv = btn.closest('.PO-Fld');
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
            <%--switch (popupTypeVal) {
                case "vendor":
                    document.getElementById("<%=txtVendorCode.ClientID%>").value = getVal;
                    break;
                case "warehouse":
                    document.getElementById("<%=txtWhsCode.ClientID%>").value = getVal;
                    break;
                case "item":
                    document.getElementById("<%=txtItemCode.ClientID%>").value = getVal;
                    break;
            }--%>

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
            const rows = container.querySelectorAll('.PO-Dtl-Flds');
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


        function collectPOData(e) {
            e.preventDefault();

            const UrlObj = new URLSearchParams(window.location.search);
            let EmpIdVal = UrlObj.get("empId");

            const header = {
                PONumber: document.getElementById("<%= txtPONum.ClientID %>").value,
                PODate: document.getElementById("<%= txtPODate.ClientID %>").value,
                ExpectedDlvDate: document.getElementById("<%= txtExpectedDlvDate.ClientID %>").value,
                PaymentTerms: document.getElementById("<%= txtPymtTrms.ClientID %>").value,
                EmpId: EmpIdVal,
                TotalAmount: document.getElementById("<%= txtAmount.ClientID %>").value,
                Currency: document.getElementById("<%= ddlCurrencyCode.ClientID %>").value,
                Remarks: document.getElementById("<%= txtRemarks.ClientID %>").value,
                DtlPO: []
            };

            const allLineContainers = document.querySelectorAll("#Line-Fields-Container, #Add-Line .PO-Dtl-Flds");

            allLineContainers.forEach(container => {
                const line = {
                    ItemCode: container.querySelector(".item-code")?.value || "",
                    VendorCode: container.querySelector(".vnd-code")?.value || "",
                    WarehouseCode: container.querySelector(".whs-code")?.value || "",
                    Quantity: container.querySelector(".qty")?.value || "",
                    Price: container.querySelector(".price")?.value || "",
                    Discount: container.querySelector(".discount")?.value || "",
                    Tax: container.querySelector(".tax")?.value || "",
                    RemarksDtl: container.querySelector(".remarks")?.value || ""
                };
                header.DtlPO.push(line);
            });

            //console.log(JSON.stringify({ A_Cls_Po:header }));

                                            //Sending JSON to WCF endpoint.
            fetch("http://localhost:49808/ERP_Web_Portal.svc/PostPO", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({ A_Cls_Po: header })
            }).then((res) => res.json()).then((res) => {
                //console.log(res);

                alert(res.PostPOResult.ResponseMsg);
            }).catch((err) => {
                console.log(err.message);
                alert(err.message);
            })
        }
    </script>
</asp:Content>

