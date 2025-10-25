using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using Cls_ERP_Web_Portal;


namespace ERP_Web_Portal_WCF
{
    public class ERP_Web_Portal : IERP_Web_Portal
    {
        protected string StrConn = ConfigurationManager.ConnectionStrings["ERP_Db_Conn_Str"].ConnectionString;
        public Cls_Response DoWork()
        {
            return new Cls_Response(){ 
                StatusCode = 200, 
                ResponseMsg = "Http connection is ok!", 
                Token="" 
            };
        }



        public Cls_Response PostPO(Cls_PurchaseOrderHdr A_Cls_Po)
        {
            SqlConnection Conn = new SqlConnection(StrConn);
            Cls_Response Res = new Cls_Response();
            SqlCommand Cmd = null;
            string SqlTxt = "";

            try
            {
                Conn.Open();

                if(A_Cls_Po != null)
                {
                    string PONumber = A_Cls_Po.PONumber.Trim();
                    DateTime PODate = Convert.ToDateTime(A_Cls_Po.PODate.Trim());
                    DateTime ExpectDlvDate = Convert.ToDateTime(A_Cls_Po.ExpectedDlvDate.Trim());
                    string PaymentTerms = A_Cls_Po.PaymentTerms.Trim();
                    string CreatedBy = A_Cls_Po.EmpId.Trim();
                    DateTime CreatedTime = DateTime.Now;
                    decimal TotalAmount = Convert.ToDecimal(A_Cls_Po.TotalAmount.Trim());
                    int CurrencyCode = Convert.ToInt32(A_Cls_Po.Currency.Trim());
                    string HdrRemarks = A_Cls_Po.Remarks.Trim();

                    SqlTxt = @"INSERT INTO PurchaseOrder_Hdr (PONum,PODate,Expected_Dlv_Date,Payment_Terms,Created_By,Created_Date,
                                Total_Amount,CurrencyCode,PO_Remarks)VALUES(@PONum,@PODate,@Expected_Dlv_Date,@Payment_Terms,@Created_By,
                                @Created_Date,@Total_Amount,@CurrencyCode,@PO_Remarks)";

                    Cmd = new SqlCommand(SqlTxt,Conn);
                    Cmd.Parameters.AddWithValue("@PONum", PONumber);
                    Cmd.Parameters.AddWithValue("@PODate", PODate);
                    Cmd.Parameters.AddWithValue("@Expected_Dlv_Date", ExpectDlvDate);
                    Cmd.Parameters.AddWithValue("@Payment_Terms", PaymentTerms);
                    Cmd.Parameters.AddWithValue("@Created_By", CreatedBy);
                    Cmd.Parameters.AddWithValue("@Created_Date", CreatedTime);
                    Cmd.Parameters.AddWithValue("@Total_Amount", TotalAmount);
                    Cmd.Parameters.AddWithValue("@CurrencyCode", CurrencyCode);
                    Cmd.Parameters.AddWithValue("@PO_Remarks", HdrRemarks);

                    int RowSaved = Cmd.ExecuteNonQuery();

                    if (RowSaved > 0)
                    {
                        if (A_Cls_Po.DtlPO != null)
                        {
                            foreach (Cls_PurchaseOrderDtl Cls_Po_Dtl in A_Cls_Po.DtlPO)
                            {
                                string ItemCode = Cls_Po_Dtl.ItemCode.Trim();
                                string VendorCode = Cls_Po_Dtl.VendorCode.Trim();
                                string WarehouseCode = Cls_Po_Dtl.WarehouseCode.Trim();
                                decimal Qty = Convert.ToDecimal(Cls_Po_Dtl.Quantity.Trim());
                                decimal Price = Convert.ToDecimal(Cls_Po_Dtl.Price.Trim());
                                decimal Discount = Convert.ToDecimal(Cls_Po_Dtl.Discount.Trim());
                                decimal Tax = Convert.ToDecimal(Cls_Po_Dtl.Tax.Trim());
                                string LineRemarks = Cls_Po_Dtl.RemarksDtl.Trim();

                                SqlTxt = @"BEGIN TRANSACTION;";
                                SqlTxt += @"INSERT INTO PurchaseOrder_Dtl (POId,ItemId,VendorCode,WhsCode,Quantity,Unit_Price,Discount_Per,Tax_Per,
                                            Line_Total,Remarks)VALUES(@POId,@ItemId,@VendorCode,@WhsCode,@Quantity,@Unit_Price,@Discount_Per,@Tax_Per,
                                            @Line_Total,@Remarks);";
                                SqlTxt += @"COMMIT TRANSACTION;";

                                decimal LineTotal = (Qty * Price * (1 - (Discount / 100)))*(1 +(Tax/100));

                                Cmd = new SqlCommand(SqlTxt, Conn);
                                Cmd.Parameters.AddWithValue("@POId", PONumber);
                                Cmd.Parameters.AddWithValue("@ItemId", ItemCode);
                                Cmd.Parameters.AddWithValue("@VendorCode", VendorCode);
                                Cmd.Parameters.AddWithValue("@WhsCode", WarehouseCode);
                                Cmd.Parameters.AddWithValue("@Quantity", Qty);
                                Cmd.Parameters.AddWithValue("@Unit_Price", Price);
                                Cmd.Parameters.AddWithValue("@Discount_Per", Discount);
                                Cmd.Parameters.AddWithValue("@Tax_Per", Tax);
                                Cmd.Parameters.AddWithValue("@Line_Total", LineTotal);
                                Cmd.Parameters.AddWithValue("@Remarks", LineRemarks);

                                int DtlRow = Cmd.ExecuteNonQuery();
                                if (DtlRow > 0)
                                {
                                    WebOperationContext.Current.OutgoingResponse.StatusDescription = HttpStatusCode.OK.ToString();
                                    WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.OK;
                                    Res.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
                                    Res.ResponseMsg = "PO Number " + PONumber + " Saved Successfully!";
                                }
                            }
                        }
                        else
                        {
                            throw new NullReferenceException("Detail part of PO is missing! DB Transaction failed!");
                        }
                    }
                    else
                    {
                        throw new Exception("Header table data not saved! Please try again.");
                    }
                }
                else
                {
                    throw new NullReferenceException("Input JSON object is missing, probably of key mismatch.");
                }
            }
            catch (NullReferenceException ex)
            {
                //HttpContext.Current.Response.StatusCode = Convert.ToInt32(HttpStatusCode.NotAcceptable);
                //HttpContext.Current.Response.StatusDescription = HttpStatusCode.NotAcceptable.ToString();
                WebOperationContext.Current.OutgoingResponse.StatusDescription = HttpStatusCode.NotAcceptable.ToString();
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.NotAcceptable;
                Res.ResponseMsg = ex.Message;
                Res.StatusCode = Convert.ToInt32(HttpStatusCode.NotAcceptable);
            }
            catch (Exception ex)
            {
                WebOperationContext.Current.OutgoingResponse.StatusDescription = HttpStatusCode.InternalServerError.ToString();
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;
                Res.ResponseMsg = ex.Message;
                Res.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);
            }
            finally
            {
                Conn.Close();
            }

            return Res;
        }

        public Cls_ShowPurchaseOrder GetPO(string PoNum)
        {
            SqlConnection Conn = new SqlConnection(StrConn);
            Cls_ShowPurchaseOrder Res = new Cls_ShowPurchaseOrder();
            Cls_PurchaseOrderHdr PO_Hdr = new Cls_PurchaseOrderHdr();
            Cls_PurchaseOrderDtl PO_Dtl = null;
            List <Cls_PurchaseOrderDtl> DtlList = new List<Cls_PurchaseOrderDtl>();
            SqlCommand Cmd = null;
            SqlDataReader Reader = null;
            string SqlTxt = "";

            try
            {
                Conn.Open();
                PoNum = PoNum.Trim();

                if (!string.IsNullOrEmpty(PoNum))
                {
                    SqlTxt = @"SELECT	A.Id,
		                                A.PONum,
		                                CONVERT(VARCHAR,A.PODate,23) ""PODate"",
		                                CONVERT(VARCHAR,A.Expected_Dlv_Date,23) ""Expected_Dlv_Date"",
		                                A.Payment_Terms,
		                                UPPER(SUBSTRING(A.PO_Status,1,1)) + LOWER(SUBSTRING(A.PO_Status,2,LEN(A.PO_Status)-1)) ""PO_Status"",
		                                A.Created_By,
		                                UPPER(SUBSTRING(B.[FName],1,1)) + LOWER(SUBSTRING(B.[FName],2,LEN(B.[FName])-1))
		                                 + ' ' + UPPER(SUBSTRING(B.[LName],1,1)) + LOWER(SUBSTRING(B.[LName],2,LEN(B.[LName])-1)) ""Name"",
		                                CONVERT(VARCHAR,A.Created_Date,23) ""Created_Date"",
		                                A.Approved_By,
		                                (CASE 
			                                WHEN A.Approved_Date IS NULL THEN NULL
			                                WHEN A.Approved_Date IS NOT NULL THEN CONVERT(VARCHAR,A.Approved_Date,23)
		                                END
		                                ) ""Approved_Date"",
		                                A.Total_Amount,
		                                UPPER(SUBSTRING(C.CurrencyName,1,1)) + LOWER(SUBSTRING(C.CurrencyName,2,LEN(C.CurrencyName)-1)) ""CurrencyName"",
		                                A.PO_Remarks
                                FROM	PurchaseOrder_Hdr A
		                                LEFT JOIN UserLogin B ON A.Created_By =B.EmpId AND B.ActiveUser='Y'
		                                LEFT JOIN CurrencyTypes C ON A.CurrencyCode=C.CurrencyCode
                                WHERE	A.PONum=@PONum";

                    Cmd = new SqlCommand(SqlTxt, Conn);
                    Cmd.Parameters.AddWithValue("@PONum",PoNum);
                    Reader = Cmd.ExecuteReader();

                    if(Reader.HasRows)
                    {
                        if (Reader.Read())
                        {
                            PO_Hdr.RowId = Reader["Id"].ToString();
                            PO_Hdr.PONumber = Reader["PONum"].ToString();
                            PO_Hdr.PODate = Reader["PODate"].ToString();
                            PO_Hdr.ExpectedDlvDate = Reader["Expected_Dlv_Date"].ToString();
                            PO_Hdr.PaymentTerms = Reader["Payment_Terms"].ToString();
                            PO_Hdr.POStatus = Reader["PO_Status"].ToString();
                            PO_Hdr.EmpId = Reader["Created_By"].ToString();
                            PO_Hdr.EmpName = Reader["Name"].ToString();
                            PO_Hdr.POCreatedDate = Reader["Created_Date"].ToString();
                            PO_Hdr.ApprovedBy = Reader["Approved_By"].ToString();
                            PO_Hdr.ApprovedDate = Reader["Approved_Date"].ToString();
                            PO_Hdr.TotalAmount = Reader["Total_Amount"].ToString();
                            PO_Hdr.Currency = Reader["CurrencyName"].ToString();
                            PO_Hdr.Remarks = Reader["PO_Remarks"].ToString();

                            Reader.Close();

                            SqlTxt = @"SELECT	A.Id,
		                                        A.POId,
		                                        A.ItemId,
		                                        A.VendorCode,
		                                        A.WhsCode,
		                                        A.Quantity,
		                                        A.Unit_Price,
		                                        A.Discount_Per,
		                                        A.Tax_Per,
		                                        A.Line_Total,
		                                        A.Remarks
                                        FROM	PurchaseOrder_Dtl A
                                        WHERE	A.POId=@PONum";
                            Cmd = new SqlCommand(SqlTxt,Conn);
                            Cmd.Parameters.AddWithValue("@PONum", PoNum);
                            Reader = Cmd.ExecuteReader();

                            if (Reader.HasRows)
                            {
                                while (Reader.Read())
                                {                                       //Openingup individual Obj ref variable.
                                    PO_Dtl = new Cls_PurchaseOrderDtl();

                                    PO_Dtl.ItemCode = Reader["ItemId"].ToString();
                                    PO_Dtl.VendorCode = Reader["VendorCode"].ToString();
                                    PO_Dtl.WarehouseCode = Reader["WhsCode"].ToString();
                                    PO_Dtl.Quantity = Reader["Quantity"].ToString();
                                    PO_Dtl.Price = Reader["Unit_Price"].ToString();
                                    PO_Dtl.Discount = Reader["Discount_Per"].ToString();
                                    PO_Dtl.Tax = Reader["Tax_Per"].ToString();
                                    PO_Dtl.RemarksDtl = Reader["Remarks"].ToString();
                                    PO_Dtl.LineTotal = Convert.ToDecimal(Reader["Line_Total"].ToString());

                                    DtlList.Add(PO_Dtl);
                                }    
                            }
                            else
                            {
                                PO_Dtl=null;
                                DtlList.Add(PO_Dtl);
                            }

                            Reader.Close();

                            WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.OK;
                            PO_Hdr.DtlPO = DtlList;
                            Res.PoHdr = PO_Hdr;
                            Res.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
                            Res.ResponseMsg = "Data Fetched Successfully!";
                        }
                    }
                    else
                    {
                        WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                        Res.StatusCode = Convert.ToInt32(HttpStatusCode.NotFound);
                        Res.ResponseMsg = "No Record Found On PO: " + PoNum;
                    }
                }
                else
                {
                    throw new Exception("PO number is not present in the query string! Please send a valid PO number.");
                }

            }
            catch (Exception ex) 
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;
                Res.ResponseMsg = ex.Message;
                Res.StatusCode= Convert.ToInt32(HttpStatusCode.InternalServerError);
            }
            finally
            {
                Conn.Close();
                Reader = null;
            }

            return Res;
        }

        public Cls_Response PostGRPO(Cls_GRPO_Hdr A_Cls_Grpo)
        {
            SqlConnection Conn = new SqlConnection(StrConn);
            Cls_Response Res = new Cls_Response();
            SqlCommand Cmd = null;
            string SqlTxt = "";

            try
            {
                Conn.Open();

                if (A_Cls_Grpo != null)
                {
                    string GRPONumber = A_Cls_Grpo.GRPO_No.Trim();
                    string PONum = A_Cls_Grpo.PO_No.Trim();
                    string VendorCode = A_Cls_Grpo.VendorCode.Trim();
                    string WhsCode = A_Cls_Grpo.WarehouseCode.Trim();
                    DateTime ReceivedDate = Convert.ToDateTime(A_Cls_Grpo.ReceivedDt.Trim());
                    string ReceivedBy = A_Cls_Grpo.ReceivedBy.Trim();
                    string DocStatus =A_Cls_Grpo.DocStatus.Trim();
                    string CreatedBy = A_Cls_Grpo.CreatedBy.Trim();
                    DateTime CreatedTime = DateTime.Now;
                    string HdrRemarks = A_Cls_Grpo.Remarks.Trim();

                    SqlTxt = @"INSERT INTO GRPO_Hdr(GRPO_No,PO_No,Vendor_Id,Whs_Id,Receipt_Date,Received_By,DocStatus,Created_By,Remarks)
                            VALUES(@GRPO_No,@PO_No,@Vendor_Id,@Whs_Id,@Receipt_Date,@Received_By,@DocStatus,@Created_By,@Remarks)";

                    Cmd = new SqlCommand(SqlTxt, Conn);
                    Cmd.Parameters.AddWithValue("@GRPO_No", GRPONumber);
                    Cmd.Parameters.AddWithValue("@PO_No", PONum);
                    Cmd.Parameters.AddWithValue("@Vendor_Id", VendorCode);
                    Cmd.Parameters.AddWithValue("@Whs_Id", WhsCode);
                    Cmd.Parameters.AddWithValue("@Receipt_Date", ReceivedDate);
                    Cmd.Parameters.AddWithValue("@Received_By", ReceivedBy);
                    Cmd.Parameters.AddWithValue("@DocStatus", DocStatus);
                    Cmd.Parameters.AddWithValue("@Created_By", CreatedBy);
                    Cmd.Parameters.AddWithValue("@Remarks", HdrRemarks);

                    int RowSaved = Cmd.ExecuteNonQuery();

                    if (RowSaved > 0)
                    {
                        if (A_Cls_Grpo.GrpoDtls != null)
                        {
                            int i = 0;

                            foreach (Cls_GRPO_Dtls Cls_Grpo_Dtl in A_Cls_Grpo.GrpoDtls)
                            {
                                string GRPO_Id = A_Cls_Grpo.GRPO_No.Trim();
                                string Line_Id = A_Cls_Grpo.GRPO_No.Trim() + "_" + Convert.ToString(i);
                                string Item_Id = Cls_Grpo_Dtl.ItemCode.Trim();
                                decimal Ordered_Qty = Convert.ToDecimal(Cls_Grpo_Dtl.OrderQuantity.Trim());
                                decimal Received_Qty = Convert.ToDecimal(Cls_Grpo_Dtl.ReceivedQty.Trim());
                                decimal Unit_Price = Convert.ToDecimal(Cls_Grpo_Dtl.UnitPrice.Trim());
                                int CurrencyCode = Convert.ToInt32(Cls_Grpo_Dtl.CurrencyCode.Trim());
                                string BatchNo =Cls_Grpo_Dtl.BatchNum.Trim();
                                DateTime ExpDate = Convert.ToDateTime(Cls_Grpo_Dtl.ExpireDt.Trim());
                                string LineRemarks = Cls_Grpo_Dtl.Remarks.Trim();

                                SqlTxt = @"BEGIN TRANSACTION;";
                                SqlTxt += @"INSERT INTO GRPO_Dtls (GRPO_Id,PO_Line_Id,Item_Id,Ordered_Qty,Received_Qty,Unit_Price,CurrencyCode,Batch_No,[Expiry_Date],Remarks)
                                            VALUES(@GRPO_Id,@PO_Line_Id,@Item_Id,@Ordered_Qty,@Received_Qty,@Unit_Price,@CurrencyCode,@Batch_No,@Expiry_Date,@Remarks);";
                                SqlTxt += @"COMMIT TRANSACTION;";

                                Cmd = new SqlCommand(SqlTxt, Conn);
                                Cmd.Parameters.AddWithValue("@GRPO_Id", GRPO_Id);
                                Cmd.Parameters.AddWithValue("@PO_Line_Id", Line_Id);
                                Cmd.Parameters.AddWithValue("@Item_Id", Item_Id);
                                Cmd.Parameters.AddWithValue("@Ordered_Qty", Ordered_Qty);
                                Cmd.Parameters.AddWithValue("@Received_Qty", Received_Qty);
                                Cmd.Parameters.AddWithValue("@Unit_Price", Unit_Price);
                                Cmd.Parameters.AddWithValue("@CurrencyCode", CurrencyCode);
                                Cmd.Parameters.AddWithValue("@Batch_No", BatchNo);
                                Cmd.Parameters.AddWithValue("@Expiry_Date", ExpDate);
                                Cmd.Parameters.AddWithValue("@Remarks", LineRemarks);

                                int DtlRow = Cmd.ExecuteNonQuery();
                                if (DtlRow > 0)
                                {
                                    WebOperationContext.Current.OutgoingResponse.StatusDescription = HttpStatusCode.OK.ToString();
                                    WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.OK;
                                    Res.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
                                    Res.ResponseMsg = "GRPO Number " + GRPO_Id + " Saved Successfully!";
                                }

                                i++;
                            }
                        }
                        else
                        {
                            throw new NullReferenceException("Detail part of PO is missing! DB Transaction failed!");
                        }
                    }
                    else
                    {
                        throw new Exception("Header table data not saved! Please try again.");
                    }
                }
                else
                {
                    throw new NullReferenceException("Input JSON object is missing, probably of key mismatch.");
                }
            }
            catch (NullReferenceException ex)
            {
                //HttpContext.Current.Response.StatusCode = Convert.ToInt32(HttpStatusCode.NotAcceptable);
                //HttpContext.Current.Response.StatusDescription = HttpStatusCode.NotAcceptable.ToString();
                WebOperationContext.Current.OutgoingResponse.StatusDescription = HttpStatusCode.NotAcceptable.ToString();
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.NotAcceptable;
                Res.ResponseMsg = ex.Message;
                Res.StatusCode = Convert.ToInt32(HttpStatusCode.NotAcceptable);
            }
            catch (Exception ex)
            {
                WebOperationContext.Current.OutgoingResponse.StatusDescription = HttpStatusCode.InternalServerError.ToString();
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;
                Res.ResponseMsg = ex.Message;
                Res.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);
            }
            finally
            {
                Conn.Close();
            }

            return Res;
        }

        public Cls_ShowGRPO GetGRPO(string GrpoNum)
        {
            SqlConnection Conn = new SqlConnection(StrConn);
            Cls_ShowGRPO Res = new Cls_ShowGRPO();
            Cls_GRPO_Hdr GRPO_Hdr = new Cls_GRPO_Hdr();
            Cls_GRPO_Dtls GRPO_Dtl = null;
            List<Cls_GRPO_Dtls> DtlList = new List<Cls_GRPO_Dtls>();
            SqlCommand Cmd = null;
            SqlDataReader Reader = null;
            string SqlTxt = "";

            try
            {
                Conn.Open();
                GrpoNum = GrpoNum.Trim();

                if (!string.IsNullOrEmpty(GrpoNum))
                {
                    SqlTxt = @"SELECT	A.Id,
		                                A.GRPO_No,
		                                A.PO_No,
		                                A.Vendor_Id,
		                                A.Whs_Id,
		                                B.WareHouseName,
		                                CONVERT(VARCHAR, A.Receipt_Date, 23) ""Receipt_Date"",
		                                A.Received_By,
		                                UPPER(SUBSTRING(ISNULL(C.FName,''),1,1)) + LOWER(SUBSTRING(ISNULL(C.FName,''),2,LEN(C.FName)-1)) 
                                + ' ' + UPPER(SUBSTRING(ISNULL(C.LName,''),1,1)) + LOWER(SUBSTRING(ISNULL(C.LName,''),2,LEN(C.LName)-1)) ""EmployeeName"",
                                        D.DocStatusName,
		                                CONVERT(VARCHAR, A.DocDate, 23) ""DocDate"",
		                                A.Created_By,
		                                A.Remarks
                                FROM	GRPO_Hdr A
		                                LEFT JOIN WarehouseMaster B ON A.Whs_Id =B.WareHouseCode AND B.Active='Y'
		                                LEFT JOIN UserLogin C ON A.Received_By=C.EmpId AND C.ActiveUser='Y'
		                                LEFT JOIN DocStatusEnum D ON A.DocStatus=D.DocStatusCode
                                WHERE   A.GRPO_No=@GrpoNo";

                    Cmd = new SqlCommand(SqlTxt, Conn);
                    Cmd.Parameters.AddWithValue("@GrpoNo", GrpoNum);
                    Reader = Cmd.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        if (Reader.Read())
                        {
                            GRPO_Hdr.GRPO_No = Reader["GRPO_No"].ToString();
                            GRPO_Hdr.PO_No = Reader["PO_No"].ToString();
                            GRPO_Hdr.VendorCode = Reader["Vendor_Id"].ToString();
                            GRPO_Hdr.WarehouseCode = Reader["Whs_Id"].ToString();
                            GRPO_Hdr.WarehouseName = Reader["WareHouseName"].ToString();
                            GRPO_Hdr.ReceivedDt = Reader["Receipt_Date"].ToString();
                            GRPO_Hdr.ReceivedBy = Reader["Received_By"].ToString();
                            GRPO_Hdr.EmpName = Reader["EmployeeName"].ToString();
                            GRPO_Hdr.DocStatus = Reader["DocStatusName"].ToString();
                            GRPO_Hdr.DocDate = Reader["DocDate"].ToString();
                            GRPO_Hdr.CreatedBy = Reader["Created_By"].ToString();
                            GRPO_Hdr.Remarks = Reader["Remarks"].ToString();

                            Reader.Close();

                            SqlTxt = @"SELECT	A.Id,
		                                        A.GRPO_Id,
		                                        A.PO_Line_Id,
		                                        A.Item_Id,
		                                        A.Ordered_Qty,
		                                        A.Received_Qty,
		                                        A.Unit_Price,
		                                        UPPER(SUBSTRING(ISNULL(B.CurrencyName,''),1,1)) + 
		                                        LOWER(SUBSTRING(ISNULL(B.CurrencyName,''),2,LEN(B.CurrencyName)-1)) ""CurrencyName"",
		                                        A.Total_Amount,
		                                        A.Batch_No,
		                                        A.Expiry_Date,
		                                        A.Remarks
                                        FROM	GRPO_Dtls A
		                                        LEFT JOIN CurrencyTypes B ON B.CurrencyCode=A.CurrencyCode
                                        WHERE	A.GRPO_Id=@GrpoNo";

                            Cmd = new SqlCommand(SqlTxt, Conn);
                            Cmd.Parameters.AddWithValue("@GrpoNo", GrpoNum);
                            Reader = Cmd.ExecuteReader();

                            if (Reader.HasRows)
                            {
                                while (Reader.Read())
                                {                                       //Openingup individual Obj ref variable.
                                    GRPO_Dtl = new Cls_GRPO_Dtls();

                                    GRPO_Dtl.Id = Convert.ToInt32(Reader["Id"].ToString());
                                    GRPO_Dtl.GRPO_Id = Reader["GRPO_Id"].ToString();
                                    GRPO_Dtl.LineId = Reader["PO_Line_Id"].ToString();
                                    GRPO_Dtl.ItemCode = Reader["Item_Id"].ToString();
                                    GRPO_Dtl.OrderQuantity = Reader["Ordered_Qty"].ToString();
                                    GRPO_Dtl.ReceivedQty = Reader["Received_Qty"].ToString();
                                    GRPO_Dtl.UnitPrice = Reader["Unit_Price"].ToString();
                                    GRPO_Dtl.CurrencyName = Reader["CurrencyName"].ToString();
                                    GRPO_Dtl.TotalAmount = Convert.ToDecimal(Reader["Total_Amount"].ToString());
                                    GRPO_Dtl.BatchNum = Reader["Batch_No"].ToString();
                                    GRPO_Dtl.ExpireDt = Reader["Expiry_Date"].ToString();
                                    GRPO_Dtl.Remarks = Reader["Remarks"].ToString();

                                    DtlList.Add(GRPO_Dtl);
                                }
                            }
                            else
                            {
                                GRPO_Dtl = null;
                                DtlList.Add(GRPO_Dtl);
                            }

                            Reader.Close();

                            WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.OK;
                            GRPO_Hdr.GrpoDtls = DtlList;
                            Res.GrpoHdr = GRPO_Hdr;
                            Res.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
                            Res.ResponseMsg = "Data Fetched Successfully!";
                        }
                    }
                    else
                    {
                        WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                        Res.StatusCode = Convert.ToInt32(HttpStatusCode.NotFound);
                        Res.ResponseMsg = "No Record Found On GRPO: " + GrpoNum;
                    }
                }
                else
                {
                    throw new Exception("PO number is not present in the query string! Please send a valid PO number.");
                }

            }
            catch (Exception ex)
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;
                Res.ResponseMsg = ex.Message;
                Res.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);
            }
            finally
            {
                Conn.Close();
                Reader = null;
            }

            return Res;
        }

        public Cls_Response PostApInv(Cls_AccountPurchaseInvHdr A_Cls_ApInv)
        {
            SqlConnection Conn = new SqlConnection(StrConn);
            Cls_Response Res = new Cls_Response();
            SqlCommand Cmd = null;
            string SqlTxt = "";

            try
            {
                Conn.Open();

                if (A_Cls_ApInv != null)
                {
                    string InvNo = A_Cls_ApInv.InvNo.Trim();
                    string Vendor_Id = A_Cls_ApInv.Vendor_Id.Trim();
                    string PONo = A_Cls_ApInv.PONo.Trim();
                    string GRPONo = A_Cls_ApInv.GRPONo.Trim();
                    DateTime DocDate = Convert.ToDateTime(A_Cls_ApInv.DocDate.Trim());
                    DateTime PostingDate = Convert.ToDateTime(A_Cls_ApInv.PostingDate.Trim());
                    DateTime DueDate =Convert.ToDateTime(A_Cls_ApInv.DueDate.Trim());
                    string Currency = A_Cls_ApInv.Currency.Trim();
                    decimal DocTotal = A_Cls_ApInv.DocTotal;
                    decimal TaxAmount = A_Cls_ApInv.TaxAmount;
                    decimal Discount = A_Cls_ApInv.Discount;
                    string Remarks = A_Cls_ApInv.Remarks.Trim();
                    string Status = A_Cls_ApInv.Status.Trim();
                    string CreatedBy = A_Cls_ApInv.CreatedBy.Trim();
                    
                    DateTime CreatedDate = DateTime.Now;


                    SqlTxt = @"INSERT INTO ApInvoice_Hdr (InvNo,Vendor_Id,PONo,GRPONo,DocDate,PostingDate,DueDate,Currency,DocTotal,TaxAmount,
                                Discount,Remarks,[Status],CreatedOn,CreatedBy)VALUES(@InvNo,@VendId,@PoNo,@GrpoNo,@DocDate,@PostDate,@DueDate,
                                @Currency,@DocTotal,@TaxAmt,@Discount,@Remarks,@Status,@CreatedOn,@CreatedBy)";

                    Cmd = new SqlCommand(SqlTxt, Conn);
                    Cmd.Parameters.AddWithValue("@InvNo", InvNo);
                    Cmd.Parameters.AddWithValue("@VendId", Vendor_Id);
                    Cmd.Parameters.AddWithValue("@PoNo", PONo);
                    Cmd.Parameters.AddWithValue("@GrpoNo", GRPONo);
                    Cmd.Parameters.AddWithValue("@DocDate", DocDate);
                    Cmd.Parameters.AddWithValue("@PostDate", PostingDate);
                    Cmd.Parameters.AddWithValue("@DueDate", DueDate);
                    Cmd.Parameters.AddWithValue("@Currency", Currency);
                    Cmd.Parameters.AddWithValue("@DocTotal", DocTotal);
                    Cmd.Parameters.AddWithValue("@TaxAmt", TaxAmount);
                    Cmd.Parameters.AddWithValue("@Discount", Discount);
                    Cmd.Parameters.AddWithValue("@Remarks", Remarks);
                    Cmd.Parameters.AddWithValue("@Status", Status);
                    Cmd.Parameters.AddWithValue("@CreatedOn", CreatedDate);
                    Cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

                    int RowSaved = Cmd.ExecuteNonQuery();

                    if (RowSaved > 0)
                    {
                        if (A_Cls_ApInv.ApInvDtls != null)
                        {
                            foreach (Cls_AccountPurchaseInvDtls Cls_Ap_Dtl in A_Cls_ApInv.ApInvDtls)
                            {
                                string ItemCode = Cls_Ap_Dtl.ItemCode.Trim();
                                decimal Quantity = Cls_Ap_Dtl.Quantity;
                                decimal UnitPrice = Cls_Ap_Dtl.Quantity;
                                string TaxCode = Cls_Ap_Dtl.TaxCode.Trim();
                                string RemarksDtl = Cls_Ap_Dtl.RemarksDtl.Trim();


                                SqlTxt = @"BEGIN TRANSACTION;";
                                SqlTxt += @"INSERT INTO ApInvoice_Dtl(InvNo,ItemCode,Quantity,UnitPrice,TaxCode,RemarksDtl)
                                            VALUES(@InvNo,@ItemCode,@Quantity,@UnitPrice,@TaxCode,@RemarksDtl);";
                                SqlTxt += @"COMMIT TRANSACTION;";

                                Cmd = new SqlCommand(SqlTxt, Conn);
                                Cmd.Parameters.AddWithValue("@InvNo", InvNo);
                                Cmd.Parameters.AddWithValue("@ItemCode", ItemCode);
                                Cmd.Parameters.AddWithValue("@Quantity", Quantity);
                                Cmd.Parameters.AddWithValue("@UnitPrice", UnitPrice);
                                Cmd.Parameters.AddWithValue("@TaxCode", TaxCode);
                                Cmd.Parameters.AddWithValue("@RemarksDtl", RemarksDtl);

                                int DtlRow = Cmd.ExecuteNonQuery();
                                if (DtlRow > 0)
                                {
                                    WebOperationContext.Current.OutgoingResponse.StatusDescription = HttpStatusCode.OK.ToString();
                                    WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.OK;
                                    Res.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
                                    Res.ResponseMsg = "AP Inv " + InvNo + " Saved Successfully!";
                                }
                            }
                        }
                        else
                        {
                            throw new NullReferenceException("Detail part of AP Invoice is missing! DB Transaction failed!");
                        }
                    }
                    else
                    {
                        throw new Exception("Header table data not saved! Please try again.");
                    }
                }
                else
                {
                    throw new NullReferenceException("Input JSON object is missing, probably of key mismatch.");
                }
            }
            catch (NullReferenceException ex)
            {
                //HttpContext.Current.Response.StatusCode = Convert.ToInt32(HttpStatusCode.NotAcceptable);
                //HttpContext.Current.Response.StatusDescription = HttpStatusCode.NotAcceptable.ToString();
                WebOperationContext.Current.OutgoingResponse.StatusDescription = HttpStatusCode.NotAcceptable.ToString();
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.NotAcceptable;
                Res.ResponseMsg = ex.Message;
                Res.StatusCode = Convert.ToInt32(HttpStatusCode.NotAcceptable);
            }
            catch (Exception ex)
            {
                WebOperationContext.Current.OutgoingResponse.StatusDescription = HttpStatusCode.InternalServerError.ToString();
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;
                Res.ResponseMsg = ex.Message;
                Res.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);
            }
            finally
            {
                Conn.Close();
            }

            return Res;
        }

        public Cls_ShowAccountPurchaseInv GetAPInvoice(string InvNum)
        {
            SqlConnection Conn = new SqlConnection(StrConn);
            Cls_ShowAccountPurchaseInv Res = new Cls_ShowAccountPurchaseInv();
            Cls_AccountPurchaseInvHdr AP_Hdr = new Cls_AccountPurchaseInvHdr();
            Cls_AccountPurchaseInvDtls AP_Dtl = null;
            List<Cls_AccountPurchaseInvDtls> DtlList = new List<Cls_AccountPurchaseInvDtls>();
            SqlCommand Cmd = null;
            SqlDataReader Reader = null;
            string SqlTxt = "";

            try
            {
                Conn.Open();
                InvNum = InvNum.Trim();

                if (!string.IsNullOrEmpty(InvNum))
                {
                    SqlTxt = @"SELECT	A.Id, 
		                                A.InvNo,
		                                A.Vendor_Id,
		                                A.PONo,
		                                A.GRPONo,
		                                A.DocDate,
		                                A.DueDate,
		                                A.PostingDate,
		                                UPPER(SUBSTRING(B.CurrencyName,1,1))+LOWER(SUBSTRING(B.CurrencyName,2,LEN(B.CurrencyName)-1)) ""Currency"",
		                                A.NetAmount,
		                                CONVERT(VARCHAR, A.CreatedOn,23) ""CreatedOn"",
		                                A.CreatedBy,
		                                UPPER(SUBSTRING(ISNULL(D.FName,''),1,1)) + LOWER(SUBSTRING(ISNULL(D.FName,''),2,LEN(D.FName)-1)) 
                                + ' ' + UPPER(SUBSTRING(ISNULL(D.LName,''),1,1)) + LOWER(SUBSTRING(ISNULL(D.LName,''),2,LEN(D.LName)-1)) ""CreatedByName"",
		                                C.DocStatusName,
                                        A.Remarks
                                FROM	ApInvoice_Hdr A
		                                LEFT JOIN CurrencyTypes B ON B.CurrencyCode=A.Currency
		                                LEFT JOIN DocStatusEnum C ON C.DocStatusCode=A.[Status]
		                                LEFT JOIN UserLogin D ON D.EmpId=A.CreatedBy AND D.ActiveUser='Y'
                                WHERE	A.InvNo=@InvNo";

                    Cmd = new SqlCommand(SqlTxt, Conn);
                    Cmd.Parameters.AddWithValue("@InvNo", InvNum);
                    Reader = Cmd.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        if (Reader.Read())
                        {
                            AP_Hdr.InvNo = Reader["InvNo"].ToString();
                            AP_Hdr.Vendor_Id = Reader["Vendor_Id"].ToString();
                            AP_Hdr.PONo = Reader["PONo"].ToString();
                            AP_Hdr.GRPONo = Reader["GRPONo"].ToString();
                            AP_Hdr.DocDate = Reader["DocDate"].ToString();
                            AP_Hdr.DueDate = Reader["DueDate"].ToString();
                            AP_Hdr.PostingDate = Reader["PostingDate"].ToString();
                            AP_Hdr.Currency = Reader["Currency"].ToString();
                            AP_Hdr.NetAmount = Convert.ToDecimal(Reader["NetAmount"].ToString());
                            AP_Hdr.CreatedOn = Reader["CreatedOn"].ToString();
                            AP_Hdr.CreatedBy = Reader["CreatedBy"].ToString();
                            AP_Hdr.CreatedByName = Reader["CreatedByName"].ToString();
                            AP_Hdr.Status = Reader["DocStatusName"].ToString();
                            AP_Hdr.Remarks = Reader["Remarks"].ToString();

                            Reader.Close();

                            SqlTxt = @"SELECT	A.Id, 
		                                        A.InvNo, 
		                                        A.ItemCode, 
		                                        A.Quantity,
		                                        A.UnitPrice, 
		                                        A.LineTotal,
		                                        B.TaxName ""TaxCode"",
		                                        A.RemarksDtl
                                        FROM	ApInvoice_Dtl A
		                                        LEFT JOIN TaxCodeMaster B ON A.TaxCode=B.TaxCode AND B.IsActive='Y'
                                        WHERE	A.InvNo=@InvNo";

                            Cmd = new SqlCommand(SqlTxt, Conn);
                            Cmd.Parameters.AddWithValue("@InvNo", InvNum);
                            Reader = Cmd.ExecuteReader();

                            if (Reader.HasRows)
                            {
                                while (Reader.Read())
                                {                                       //Openingup individual Obj ref variable.
                                    AP_Dtl = new Cls_AccountPurchaseInvDtls();

                                    AP_Dtl.Id = Convert.ToInt32(Reader["Id"].ToString());
                                    AP_Dtl.InvNo = Reader["InvNo"].ToString();
                                    AP_Dtl.ItemCode = Reader["ItemCode"].ToString();
                                    AP_Dtl.Quantity = Convert.ToDecimal(Reader["Quantity"].ToString());
                                    AP_Dtl.UnitPrice = Convert.ToDecimal(Reader["UnitPrice"].ToString());
                                    AP_Dtl.LineTotal = Convert.ToDecimal(Reader["LineTotal"].ToString());
                                    AP_Dtl.TaxCode = Reader["TaxCode"].ToString();
                                    AP_Dtl.RemarksDtl = Reader["RemarksDtl"].ToString();

                                    DtlList.Add(AP_Dtl);
                                }
                            }
                            else
                            {
                                AP_Dtl = null;
                                DtlList.Add(AP_Dtl);
                            }

                            Reader.Close();

                            WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.OK;
                            AP_Hdr.ApInvDtls = DtlList;
                            Res.ApHdr = AP_Hdr;
                            Res.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
                            Res.ResponseMsg = "Data Fetched Successfully!";
                        }
                    }
                    else
                    {
                        WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                        Res.StatusCode = Convert.ToInt32(HttpStatusCode.NotFound);
                        Res.ResponseMsg = "No Record Found On A/P Invoice: " + InvNum;
                    }
                }
                else
                {
                    throw new Exception("A/P Invoice number is not present in the query string! Please send a valid invoice number.");
                }

            }
            catch (Exception ex)
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;
                Res.ResponseMsg = ex.Message;
                Res.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);
            }
            finally
            {
                Conn.Close();
                Reader = null;
            }

            return Res;
        }

        public Cls_Response UpdateAPInv(Cls_UpdateAPInv sendObj)
        {
            Cls_UpdateAPInv UpdateAp = new Cls_UpdateAPInv();
            Cls_Response Res = new Cls_Response();

            return Res;
        }
    }
}
