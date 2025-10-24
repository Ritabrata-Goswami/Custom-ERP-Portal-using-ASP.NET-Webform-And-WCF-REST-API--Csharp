using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewInventory : System.Web.UI.Page
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
		                        A.WhsCode,
		                        A.GRPONo,
		                        A.BatchNo,
		                        A.QuantityOnHand,
		                        A.UnitPrice,
		                        CONVERT(VARCHAR,A.LastUpdated,23) ""LastUpdateDate""
                        FROM	Inventory A
                        ORDER BY A.Id DESC";

            SqlDataAdapter Adp = new SqlDataAdapter(SqlTxt, Conn);
            Adp.Fill(Dt);

            InventoryData.DataSource = Dt;
            InventoryData.DataBind();
        }
        catch (Exception ex)
        {
            string Message = ex.Message.Replace("'", "\\'");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorResponseMessage", $"alert('Error due to: {Message}');", true);
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
            decimal OnHandQty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "QuantityOnHand").ToString());

            if (OnHandQty <= 0)
            {
                e.Row.BackColor = System.Drawing.Color.PaleVioletRed;
                e.Row.ForeColor = System.Drawing.Color.White;
            }
        }
    }


}