using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PopupMaster : System.Web.UI.Page
{
    protected string popupType = "";
    protected string StrConn = ConfigurationManager.ConnectionStrings["ERP_Db_Conn_Str"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SqlConnection Conn = null;
            string SqlTxt = "";
            string ErrMsg = "";
            DataTable Dt = null;

            popupType = Request.QueryString["popup_type"];

            if (string.IsNullOrEmpty(popupType))
            {
                Session.Clear();
                Response.Redirect("/Default.aspx");
                return;
            }
            else
            {
                popupType = popupType.Trim().ToLower();
            }

            try
            {
                Conn = new SqlConnection(StrConn);
                Conn.Open();

                switch (popupType)
                {
                    case "vendor":
                        SqlTxt = @"SELECT	A.Id,
		                                    A.VendorCode, 
		                                    ISNULL(A.FName, '') + ' ' + ISNULL(A.LName,'') ""VendorName"",
		                                    B.VendorTypeName
                                    FROM	VendorMaster A
		                                    INNER JOIN VendorTypes B ON A.VendorType=B.VendorTypeCode
                                    WHERE	A.Active='Y'";
                        break;
                    case "warehouse":
                        SqlTxt = @"SELECT	A.Id,
		                                    A.WareHouseCode,
		                                    A.WareHouseName,
		                                    A.ContactPerson,
		                                    B.WareHouseTypeName
                                    FROM	WarehouseMaster A
		                                    INNER JOIN WareHouseTypes B ON A.WareHouseType=B.WareHouseTypeCode
                                    WHERE	A.Active='Y'";
                        break;
                    case "item":
                        SqlTxt = @"SELECT	A.Id, 
		                                    A.ItemCode, 
		                                    A.ItemName,
		                                    B.ItemTypeName
                                    FROM	ItemMaster A
		                                    INNER JOIN ItemTypes B ON A.ItemType = B.ItemTypeCode
                                    WHERE	A.Active='Y'";
                        break;
                    case "po":
                        SqlTxt = @"SELECT	A.Id,
		                                    A.PONum,
		                                    A.PO_Status ""PO Status"",
		                                    CONVERT(VARCHAR, A.Created_Date, 23) ""Created Date""
                                    FROM	PurchaseOrder_Hdr A
                                    WHERE	A.PO_Status LIKE '%approved%'";
                        break;
                    case "employee":
                        SqlTxt = @"SELECT	A.Id,
		                                    A.EmpId,
		                                    UPPER(SUBSTRING(ISNULL(A.FName,''),1,1)) + LOWER(SUBSTRING(ISNULL(A.FName,''),2,LEN(A.FName)-1)) 
                                            + ' ' + UPPER(SUBSTRING(ISNULL(A.LName,''),1,1)) + LOWER(SUBSTRING(ISNULL(A.LName,''),2,LEN(A.LName)-1)) ""EmployeeName"",
		                                    CONVERT(VARCHAR, A.JoinDate, 23) ""Join Date""
                                    FROM	UserLogin A
                                    WHERE	A.ActiveUser='Y'";
                        break;
                    case "currency":
                        SqlTxt = @"SELECT	A.Id,
		                                    A.CurrencyCode,
		                                    UPPER(SUBSTRING(ISNULL(A.CurrencyName,''),1,1)) + 
		                                    LOWER(SUBSTRING(ISNULL(A.CurrencyName,''),2,LEN(A.CurrencyName)-1)) ""CurrencyName""
                                    FROM	CurrencyTypes A";
                        break;
                    case "grpo":
                        SqlTxt = @"SELECT	A.Id, 
		                                    A.GRPO_No ""GRPONo"", 
		                                    A.PO_No ""PO No"", 
		                                    B.DocStatusName ""Status"" 
                                    FROM	GRPO_Hdr A
		                                    INNER JOIN DocStatusEnum B ON B.DocStatusCode=A.DocStatus";
                        break;
                    case "batch":
                        SqlTxt = @"SELECT	A.Id, 
		                                    A.Batch_No,
		                                    A.GRPO_Id,
		                                    A.PO_Line_Id ""Line Id""
                                    FROM	GRPO_Dtls A";
                        break;
                }

                //foreach(DataColumn Col in Dt.Columns)
                //{
                //    BoundField BndFld = new BoundField();
                //    BndFld.HeaderText = Col.ColumnName;
                //    BndFld.DataField = Col.ColumnName;

                //    DisplayData.Columns.Add(BndFld);
                //}

                SqlDataAdapter Adp = new SqlDataAdapter(SqlTxt, Conn);
                Dt =new DataTable();
                Adp.Fill(Dt);

                
                DisplayData.Columns.Clear();
                DisplayData.ShowHeaderWhenEmpty = true;
                DisplayData.RowDataBound += DisplayData_RowDataBound;

                DisplayData.DataSource = Dt;
                DisplayData.DataBind();
            }
            catch(NullReferenceException ex)
            {
                ErrMsg = ex.Message.Replace("'", "\\'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorMessage1", $"alert('Error due to:- {ErrMsg}');", true);
            }
            catch(Exception ex)
            {
                ErrMsg = ex.Message.Replace("'","\\'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorMessage2",$"alert('Error due to:- {ErrMsg}');", true);
            }
            finally
            {
                Conn.Close();
            }
        }

    }


    protected void DisplayData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string columnName = "";

            switch (popupType)
            {
                case "vendor":
                    columnName = "VendorCode";
                    break;
                case "warehouse":
                    columnName = "WareHouseCode";
                    break;
                case "item":
                    columnName = "ItemCode";
                    break;
                case "po":
                    columnName = "PONum";
                    break;
                case "employee":
                    columnName = "EmpId";
                    break;
                case "currency":
                    columnName = "CurrencyCode";
                    break;
                case "grpo":
                    columnName = "GRPONo";
                    break;
                case "batch":
                    columnName = "Batch_No";
                    break;
            }

            int colIndex = ((DataTable)DisplayData.DataSource).Columns.IndexOf(columnName);
            if (colIndex >= 0)
            {
                string valueToSend = e.Row.Cells[colIndex].Text;

                e.Row.Attributes["onclick"] = $"return DataToParent('{valueToSend}');";
                //e.Row.Style["cursor"] = "pointer";
            }
        }
    }
}