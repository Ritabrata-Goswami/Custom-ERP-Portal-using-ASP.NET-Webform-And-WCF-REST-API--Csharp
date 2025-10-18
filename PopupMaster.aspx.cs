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