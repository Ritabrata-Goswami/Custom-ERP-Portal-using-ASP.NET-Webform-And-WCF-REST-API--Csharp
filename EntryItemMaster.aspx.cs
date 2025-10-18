using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EntryItemMaster : System.Web.UI.Page
{
    protected string StrConn = ConfigurationManager.ConnectionStrings["ERP_Db_Conn_Str"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ItemTypeBinding();
            UoMBinding();
            CurrencyBinding();
            UnitMeasurement();
        }
    }

    protected void ItemTypeBinding()
    {
        SqlConnection Conn = null;
        DataTable Dt = null;

        try
        {
            Conn = new SqlConnection(StrConn);
            Conn.Open();

            string SqlTxt = @"SELECT * FROM ItemTypes";
            Dt = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter(SqlTxt,Conn);
            Adp.Fill(Dt);

            ddlItemType.DataSource = Dt;
            ddlItemType.DataTextField = "ItemTypeName";
            ddlItemType.DataValueField = "ItemTypeCode";
            ddlItemType.DataBind();

            ddlItemType.Items.Insert(0, new ListItem("---Select Item Type---",""));
        }
        catch(Exception ex)
        {
            string ErrMsg = ex.Message.Replace("'", "\\'");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ItemTypeBindingError", $"alert('Error due to: {ErrMsg}')", true);
        }
        finally
        {
            Conn.Close();
        }
    }

    protected void UoMBinding()
    {
        SqlConnection Conn = null;
        DataTable Dt = null;

        try
        {
            Conn = new SqlConnection(StrConn);
            Conn.Open();

            string SqlTxt = @"SELECT * FROM UnitsOfMeasurement";
            Dt = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter(SqlTxt, Conn);
            Adp.Fill(Dt);

            ddlUom.DataSource = Dt;
            ddlUom.DataTextField = "UoMName";
            ddlUom.DataValueField = "UoMCode";
            ddlUom.DataBind();

            ddlUom.Items.Insert(0, new ListItem("---Select UoM---", ""));
        }
        catch (Exception ex)
        {
            string ErrMsg = ex.Message.Replace("'", "\\'");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "UoMBindingError", $"alert('Error due to: {ErrMsg}')", true);
        }
        finally
        {
            Conn.Close();
        }
    }

    protected void CurrencyBinding()
    {
        SqlConnection Conn = null;
        DataTable Dt = null;

        try
        {
            Conn = new SqlConnection(StrConn);
            Conn.Open();

            string SqlTxt = @"SELECT * FROM CurrencyTypes";
            Dt = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter(SqlTxt, Conn);
            Adp.Fill(Dt);

            ddlCurrency.DataSource = Dt;
            ddlCurrency.DataTextField = "CurrencyName";
            ddlCurrency.DataValueField = "CurrencyCode";
            ddlCurrency.DataBind();

            ddlCurrency.Items.Insert(0, new ListItem("---Select Currency---", ""));
        }
        catch (Exception ex)
        {
            string ErrMsg = ex.Message.Replace("'", "\\'");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CurrencyBindingError", $"alert('Error due to: {ErrMsg}')", true);
        }
        finally
        {
            Conn.Close();
        }
    }

    protected void UnitMeasurement()
    {
        SqlConnection Conn = null;
        DataTable Dt = null;

        try
        {
            Conn = new SqlConnection(StrConn);
            Conn.Open();

            string SqlTxt = @"SELECT * FROM WeightUnits";
            Dt = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter(SqlTxt, Conn);
            Adp.Fill(Dt);

            ddlWegUnit.DataSource = Dt;
            ddlWegUnit.DataTextField = "UnitName";
            ddlWegUnit.DataValueField = "UnitName";
            ddlWegUnit.DataBind();

            ddlWegUnit.Items.Insert(0, new ListItem("---Select Weight Unit---", ""));
        }
        catch (Exception ex)
        {
            string ErrMsg = ex.Message.Replace("'", "\\'");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CurrencyBindingError", $"alert('Error due to: {ErrMsg}')", true);
        }
        finally
        {
            Conn.Close();
        }
    }


    protected void SaveMasterData(object sender, EventArgs e)
    {
        SqlConnection Conn = null;
        string SqlTxt = "";

        try
        {
            Conn = new SqlConnection(StrConn);
            Conn.Open();

            string ItemCode = txtItemCode.Text.Trim();
            string ItemName = txtItemName.Text.Trim();
            int ItemType = Convert.ToInt32(ddlItemType.Text.Trim());
            int UoM = Convert.ToInt32(ddlUom.Text.Trim());
            decimal Price = Convert.ToDecimal(txtPrice.Text.Trim());
            int Currency = Convert.ToInt32(ddlCurrency.Text.Trim());
            double Stock = Convert.ToDouble(txtStock.Text.Trim());
            double ReorderLvl = Convert.ToDouble(txtReordLvl.Text.Trim());
            string VendorCode = txtVndCode.Text.Trim();
            string WarehouseCode = txtWhsCode.Text.Trim();
            string Dim = txtDimns.Text.Trim();
            double Weight = Convert.ToDouble(txtWeight.Text.Trim());
            string WeightMeasurement = ddlWegUnit.Text.Trim();
            DateTime? ExpDate = null;
            if (String.IsNullOrEmpty(txtExpDate.Text))
            {
                ExpDate = Convert.ToDateTime("1900-01-01");
            }
            else
            {
                ExpDate = Convert.ToDateTime(txtExpDate.Text.Trim());
            }

            string Active = ddlActive.Text.Trim();

            SqlTxt = @"BEGIN TRANSACTION;";
            SqlTxt += @"INSERT INTO ItemMaster(ItemCode,ItemName,ItemType,UoM,Price,CurrencyCode,CurrentStock,ReorderLevel,
					        VendorCode,WhsCode,Dimensions,[Weight],WeightUnit,ExpiryDate,Active) VALUES(
					        @ItemCode,@ItemName,@ItemType,@UoM,@Price,@CurrencyCode,@CurrentStock,@ReorderLevel,
					        @VendorCode,@WhsCode,@Dimensions,@Weight,@WeightUnit,@ExpiryDate,@Active)";
            SqlTxt += "COMMIT TRANSACTION;";

            SqlCommand Cmd = new SqlCommand(SqlTxt,Conn);
            Cmd.Parameters.AddWithValue("@ItemCode", ItemCode);
            Cmd.Parameters.AddWithValue("@ItemName", ItemName);
            Cmd.Parameters.AddWithValue("@ItemType", ItemType);
            Cmd.Parameters.AddWithValue("@UoM", UoM);
            Cmd.Parameters.AddWithValue("@Price", Price);
            Cmd.Parameters.AddWithValue("@CurrencyCode", Currency);
            Cmd.Parameters.AddWithValue("@CurrentStock", Stock);
            Cmd.Parameters.AddWithValue("@ReorderLevel", ReorderLvl);
            Cmd.Parameters.AddWithValue("@VendorCode", VendorCode);
            Cmd.Parameters.AddWithValue("@WhsCode", WarehouseCode);
            Cmd.Parameters.AddWithValue("@Dimensions", Dim);
            Cmd.Parameters.AddWithValue("@Weight", Weight);
            Cmd.Parameters.AddWithValue("@WeightUnit", WeightMeasurement);
            Cmd.Parameters.AddWithValue("@ExpiryDate", ExpDate);
            Cmd.Parameters.AddWithValue("@Active", Active);

            int RowSaved = Cmd.ExecuteNonQuery();
            if(RowSaved > 0)
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