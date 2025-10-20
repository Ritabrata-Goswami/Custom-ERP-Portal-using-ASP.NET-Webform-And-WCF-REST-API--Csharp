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



    }
}
