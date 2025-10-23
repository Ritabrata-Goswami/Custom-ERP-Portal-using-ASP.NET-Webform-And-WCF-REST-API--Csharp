using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PostInventory : System.Web.UI.Page
{
    protected string StrConn = ConfigurationManager.ConnectionStrings["ERP_Db_Conn_Str"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void SaveInventoryData(object sender, EventArgs e)
    {
        SqlConnection Conn = new SqlConnection(StrConn);

        try
        {
            Conn.Open();

            string ItemCode = txtItemCode.Text.Trim();
            string WhsCode= txtWhsCode.Text.Trim();
            string GRPONo = txtGrpoNum.Text.Trim();
            string BatchNo = txtBatchNo.Text.Trim();
            string UnitPrice = txtPrice.Text.Trim();
            string Stock = txtStock.Text.Trim();
            DateTime UpdateTime =DateTime.Now;

            string SqlTxt = @"INSERT INTO Inventory(ItemCode,WhsCode,GRPONo,BatchNo,QuantityOnHand,UnitPrice,LastUpdated)
                                VALUES(@ItemCode,@WhsCode,@GRPONo,@BatchNo,@QuantityOnHand,@UnitPrice,@LastUpdated)";

            SqlCommand Cmd=new SqlCommand(SqlTxt, Conn);
            Cmd.Parameters.AddWithValue("@ItemCode", ItemCode);
            Cmd.Parameters.AddWithValue("@WhsCode", WhsCode);
            Cmd.Parameters.AddWithValue("@GRPONo", GRPONo);
            Cmd.Parameters.AddWithValue("@BatchNo", BatchNo);
            Cmd.Parameters.AddWithValue("@QuantityOnHand", UnitPrice);
            Cmd.Parameters.AddWithValue("@UnitPrice", Stock);
            Cmd.Parameters.AddWithValue("@LastUpdated", UpdateTime);

            int RowSaved =Cmd.ExecuteNonQuery();
            if (RowSaved > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ResSuccessMessage", $"alert('Data Saved Successfully!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ResFailedMessage", $"alert('Error: Data did not saved, try again');", true);
            }

        }
        catch (Exception ex) 
        {
            string Message = ex.Message.Replace("'", "\\'");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrMessage", $"alert('Error due to: {Message}');", true);
        }
        finally
        {
            Conn.Close();
        }
    }

}