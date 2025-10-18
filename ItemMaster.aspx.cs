using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ItemMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridDataBinding();
        }
    }

    
    protected void GridDataBinding()
    {
        string ConnStr = ConfigurationManager.ConnectionStrings["ERP_Db_Conn_Str"].ConnectionString;
        string SqlTxt = "";
        SqlConnection Conn = null;
        DataTable Dt = new DataTable();

        try
        {
            Conn = new SqlConnection(ConnStr);
            Conn.Open();
            SqlTxt = @"SELECT	A.Id, 
		                        A.ItemCode,
		                        A.ItemName,
		                        F.ItemTypeName,
		                        B.UoMName,
		                        CAST(A.Price AS VARCHAR) + ' ' + UPPER(LEFT(C.CurrencyName,1)) + LOWER(SUBSTRING(C.CurrencyName,2,LEN(C.CurrencyName)-1)) ""Price"", 
		                        A.CurrentStock,
		                        A.ReorderLevel,
		                        A.VendorCode,
		                        (D.FName + ' ' + D.LName) ""VendorName"",
		                        A.WhsCode,
		                        E.WareHouseName,
		                        E.WareHouseType,
		                        ISNULL(A.Dimensions,'-') ""Dimensions"",
		                        ISNULL(CAST(A.[Weight] AS VARCHAR) + ' ' + A.WeightUnit,'-') ""Weight"",
		                        CONVERT(VARCHAR, A.ExpiryDate, 111) ""ExpDate"",
		                        (CASE 
			                        WHEN A.Active='Y' THEN 'Yes'
			                        WHEN A.Active='N' THEN 'No'
			                        ELSE '-' 
		                        END) ""Active"" 
                        FROM	ItemMaster A
		                        INNER JOIN UnitsOfMeasurement B ON B.UoMCode=A.UoM
		                        INNER JOIN CurrencyTypes C ON C.CurrencyCode = A.CurrencyCode
		                        INNER JOIN VendorMaster D ON D.VendorCode = A.VendorCode
		                        INNER JOIN WarehouseMaster E ON E.WareHouseCode = A.WhsCode
		                        INNER JOIN ItemTypes F ON F.ItemTypeCode = A.ItemType
                        ORDER BY A.Id DESC";

            SqlDataAdapter Adp = new SqlDataAdapter(SqlTxt, Conn);
            Adp.Fill(Dt);

            ItemMasterData.DataSource = Dt;
            ItemMasterData.DataBind();
        }
        catch (Exception ex)
        {
            string Message = ex.Message.Replace("'", "\\'");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Response Message", $"alert('Error due to: {Message}');", true);
        }
        finally
        {
            Conn.Close();
        }

    }

    protected void ItemInvalidMasterData_DataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string activeStatus = DataBinder.Eval(e.Row.DataItem, "Active").ToString();

            if (activeStatus == "No")
            {
                e.Row.BackColor = System.Drawing.Color.PaleVioletRed;
                e.Row.ForeColor = System.Drawing.Color.White;
            }
        }
    }

}