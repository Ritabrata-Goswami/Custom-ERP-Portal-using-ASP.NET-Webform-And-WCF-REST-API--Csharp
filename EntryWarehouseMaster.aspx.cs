using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EntryWarehouseMaster : System.Web.UI.Page
{
    string ConnStr = ConfigurationManager.ConnectionStrings["ERP_Db_Conn_Str"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            WarehouseTypeBinding();
        }
    }

    protected void WarehouseTypeBinding()
    {
        SqlConnection Conn = null;
        DataTable Dt = null;
        string SqlTxt = "";

        try
        {
            Conn = new SqlConnection(ConnStr);
            Conn.Open();
            SqlTxt = "SELECT * FROM WareHouseTypes";
            SqlDataAdapter Adp = new SqlDataAdapter(SqlTxt, Conn);
            Dt = new DataTable();
            Adp.Fill(Dt);

            ddlWarehouseTypes.DataSource = Dt;
            ddlWarehouseTypes.DataTextField = "WareHouseTypeName";
            ddlWarehouseTypes.DataValueField = "WareHouseTypeCode";
            ddlWarehouseTypes.DataBind();

            ddlWarehouseTypes.Items.Insert(0,new ListItem("---Select Warehouse Type---",""));
        }
        catch (Exception ex) 
        {
            string ErrMsg = ex.Message.Replace("'","\\'");
            ScriptManager.RegisterStartupScript(this,this.GetType(),"WarehouseTypeError",$"alert('Error due to: {ErrMsg}')",true);
        }
        finally
        {
            Conn?.Close();
        }
    }

    protected void SaveMasterData(object sender, EventArgs e)
    {
        SqlConnection Conn = null;
        string SqlTxt = "";

        try
        {
            Conn = new SqlConnection(ConnStr);
            Conn.Open();

            string WarehouseCode = txtWarehouseCode.Text.Trim();
            string WarehouseName = txtWarehouseName.Text.Trim();
            string ContactPersonName = txtContactPerson.Text.Trim();
            string ContactPhone = txtContactPhone.Text.Trim();
            string ContactEmail = txtContactEmail.Text.Trim();
            string WarehouseAddr = txtAddress.Text.Trim();
            string WarehouseTypes = ddlWarehouseTypes.SelectedValue;
            string VendorActive = ddlActive.SelectedValue;

            SqlTxt = "BEGIN TRANSACTION;";
            SqlTxt += @"INSERT INTO WarehouseMaster (WareHouseCode,WareHouseName,WareHouseAddress,ContactPerson,Phone,Email,
                        WareHouseType,Active)VALUES(@WareHouseCode,@WareHouseName,@WareHouseAddress,@ContactPerson,@Phone,@Email,
                        @WareHouseType,@Active);";
            SqlTxt += "COMMIT TRANSACTION;";

            SqlCommand cmd = new SqlCommand(SqlTxt, Conn);
            cmd.Parameters.AddWithValue("@WareHouseCode", WarehouseCode);
            cmd.Parameters.AddWithValue("@WareHouseName", WarehouseName);
            cmd.Parameters.AddWithValue("@WareHouseAddress", WarehouseAddr);
            cmd.Parameters.AddWithValue("@ContactPerson", ContactPersonName);
            cmd.Parameters.AddWithValue("@Phone", ContactPhone);
            cmd.Parameters.AddWithValue("@Email", ContactEmail);
            cmd.Parameters.AddWithValue("@WareHouseType", WarehouseTypes);
            cmd.Parameters.AddWithValue("@Active", VendorActive);
            int rowSaved = cmd.ExecuteNonQuery();

            if (rowSaved > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Response Message", $"alert('Data Saved Successfully!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Response Message", $"alert('Error: Data did not saved, try again');", true);
            }

        }
        catch (Exception ex)
        {
            string Message = ex.Message.Replace("'", "\\'");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Response Message", $"alert('Error due to: {Message}');", true);
        }
        finally
        {
            Conn?.Close();
        }

    }
}