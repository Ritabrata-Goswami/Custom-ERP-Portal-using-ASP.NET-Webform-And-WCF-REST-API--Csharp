using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VendorMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["EmpName"] == null || Session["EmpId"] == null || Session["RowId"] == null)
            {
                Response.Redirect("/Default.aspx");
                return;
            }

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
		                        A.VendorCode, 
		                        A.FName + ' ' + A.LName ""VendorName"",
		                        B.VendorTypeName ""Type"", 
		                        A.ContactPerson, 
		                        ISNULL(A.ContactPhone, '-') ""ContactPhone"", 
		                        ISNULL(A.ContactEmail,'-') ""ContactEmail"",	
		                        ISNULL(A.VendorAddress,'-') ""VendorAddress"",
		                        A.BankAccountDetail,
		                        CAST(A.CreditLimit AS VARCHAR) + ' ' + UPPER(LEFT(C.CurrencyName,1)) + LOWER(SUBSTRING(C.CurrencyName,2, LEN(C.CurrencyName)-1)) ""CreditLimit"",
		                        (CASE 
			                        WHEN A.Active='Y' THEN 'Yes'
			                        WHEN A.Active='N' THEN 'No'
			                        ELSE '-' 
		                        END) ""Active"" 
                        FROM	VendorMaster A
		                        INNER JOIN VendorTypes B ON B.VendorTypeCode = A.VendorType
		                        INNER JOIN CurrencyTypes C ON C.CurrencyCode = A.Currency
                        ORDER BY A.Id DESC";

            SqlDataAdapter Adp = new SqlDataAdapter(SqlTxt, Conn);
            Adp.Fill(Dt);

            VendorMasterData.DataSource = Dt;
            VendorMasterData.DataBind();
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
}