using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WarehouseMaster : System.Web.UI.Page
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
		                        A.WareHouseCode,
		                        A.WareHouseName,
		                        A.WareHouseAddress,
		                        A.ContactPerson,
		                        ISNULL(A.Phone,'-') ""Phone"",
		                        ISNULL(A.Email,'-') ""Email"",
		                        B.WareHouseTypeName,
		                        (CASE 
			                        WHEN A.Active='Y' THEN 'Yes'
			                        WHEN A.Active='N' THEN 'No'
			                        ELSE '-' 
		                        END) ""Active"" 
                        FROM	WarehouseMaster A
		                        INNER JOIN WareHouseTypes B ON B.WareHouseTypeCode =A.WareHouseType
                        ORDER BY A.Id DESC";

            SqlDataAdapter Adp = new SqlDataAdapter(SqlTxt, Conn);
            Adp.Fill(Dt);

            WarehouseMasterData.DataSource = Dt;
            WarehouseMasterData.DataBind();
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


    protected void WhsInvalidMasterData_DataBound(object sender, GridViewRowEventArgs e)
    {
                                        //Check if this is a data row (ignore header/footer)
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
                                        //Get the "Active" value from the current row's data
            string activeStatus = DataBinder.Eval(e.Row.DataItem, "Active").ToString();

            if (activeStatus == "No")
            {
                e.Row.BackColor = System.Drawing.Color.PaleVioletRed;
                e.Row.ForeColor = System.Drawing.Color.White; 
            }
        }
    }

}