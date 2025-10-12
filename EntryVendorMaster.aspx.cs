using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EntryVendorMaster : System.Web.UI.Page
{
    protected string SqlConnStr = ConfigurationManager.ConnectionStrings["ERP_Db_Conn_Str"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VendorTypesLoad();
            CurrencyLoad();
        }

    }


    protected void VendorTypesLoad()
    {
        string SqlTxt = "";
        SqlConnection Conn = null;
        DataTable dt = null;

        try
        {
            Conn = new SqlConnection(SqlConnStr);
            Conn.Open();
            SqlTxt = @"SELECT * FROM VendorTypes";

            SqlDataAdapter Adp = new SqlDataAdapter(SqlTxt, Conn);
            dt = new DataTable();
            Adp.Fill(dt);

                        //Populating Vendor Type Dropdown
            ddlVendorType.DataSource = dt;
            ddlVendorType.DataTextField = "VendorTypeName";
            ddlVendorType.DataValueField = "VendorTypeCode";
            ddlVendorType.DataBind();

            ddlVendorType.Items.Insert(0, new ListItem("---Select Vendor Type---", ""));   //Default Display.
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

    protected void CurrencyLoad()
    {
        string SqlTxt = "";
        
        SqlConnection Conn = null;
        DataTable dt = null;

        try
        {
            Conn = new SqlConnection(SqlConnStr);
            Conn.Open();
            SqlTxt = @"SELECT   A.CurrencyCode, 
                                UPPER(LEFT(A.CurrencyName,1)) + LOWER(SUBSTRING(A.CurrencyName, 2,LEN(A.CurrencyName)-1)) ""CurrencyName"" 
                        FROM    CurrencyTypes A";

            SqlDataAdapter Adp = new SqlDataAdapter(SqlTxt, Conn);
            dt = new DataTable();
            Adp.Fill(dt);

            ddlCurrency.DataSource = dt;
            ddlCurrency.DataTextField = "CurrencyName";
            ddlCurrency.DataValueField = "CurrencyCode";
            ddlCurrency.DataBind();
            ddlCurrency.Items.Insert(0, new ListItem("---Select Currency Type---", ""));
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


    protected void SaveMasterData(object sender, EventArgs e)
    {
        SqlConnection Conn = null;
        string SqlTxt = "";

        try
        {
            Conn = new SqlConnection(SqlConnStr);
            Conn.Open();

            string VendorCode = txtVendorCode.Text.Trim();
            string VendorFName = txtFName.Text.Trim();
            string VendorLName = txtLName.Text.Trim();
            string VendorType = ddlVendorType.SelectedValue;
            string ContactPersonName = txtContactPerson.Text.Trim();
            string ContactPhone = txtContactPhone.Text.Trim();
            string ContactEmail = txtContactEmail.Text.Trim();
            string VendorAddr = txtAddress.Text.Trim();
            string VendorBankAcct = txtBankAccount.Text.Trim();
            string VendorCreditLmt = txtCreditLimit.Text.Trim();
            string VendorCurrency = ddlCurrency.SelectedValue;
            string VendorActive = ddlActive.SelectedValue;

            SqlTxt = "BEGIN TRANSACTION;";
            SqlTxt += @"INSERT INTO VendorMaster (VendorCode,FName,LName,VendorType,ContactPerson,ContactPhone,
                        ContactEmail,VendorAddress,BankAccountDetail,CreditLimit,Currency,Active) VALUES (@VendorCode,@FName,
                        @LName,@VendorType,@ContactPerson,@ContactPhone,@ContactEmail,@VendorAddress,@BankAccountDetail,
                        @CreditLimit,@Currency,@Active);";
            SqlTxt += "COMMIT TRANSACTION;";

            SqlCommand cmd = new SqlCommand(SqlTxt, Conn);
            cmd.Parameters.AddWithValue("@VendorCode", VendorCode);
            cmd.Parameters.AddWithValue("@FName", VendorFName);
            cmd.Parameters.AddWithValue("@LName", VendorLName);
            cmd.Parameters.AddWithValue("@VendorType", VendorType);
            cmd.Parameters.AddWithValue("@ContactPerson", ContactPersonName);
            cmd.Parameters.AddWithValue("@ContactPhone", ContactPhone);
            cmd.Parameters.AddWithValue("@ContactEmail", ContactEmail);
            cmd.Parameters.AddWithValue("@VendorAddress", VendorAddr);
            cmd.Parameters.AddWithValue("@BankAccountDetail", VendorBankAcct);
            cmd.Parameters.AddWithValue("@CreditLimit", VendorCreditLmt);
            cmd.Parameters.AddWithValue("@Currency", VendorCurrency);
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