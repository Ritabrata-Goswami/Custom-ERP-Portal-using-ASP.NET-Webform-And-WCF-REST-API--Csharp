using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ServiceModel.Web;
using System.Web.Services;
using Cls_ERP_Web_Portal;
using Newtonsoft.Json;
using System.Web.Script.Services;

public partial class PurchaseOrder : System.Web.UI.Page
{
    protected string StrConn = ConfigurationManager.ConnectionStrings["ERP_Db_Conn_Str"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CurrencyBinding();
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

            string SqlTxt = @"SELECT	A.Id, 
		                                A.CurrencyCode, 
		                                UPPER(SUBSTRING(A.CurrencyName,1,1)) + LOWER(SUBSTRING(A.CurrencyName,2,(LEN(A.CurrencyName)-1))) ""CurrencyName"" 
                                FROM	CurrencyTypes A";
            Dt = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter(SqlTxt, Conn);
            Adp.Fill(Dt);

            ddlCurrencyCode.DataSource = Dt;
            ddlCurrencyCode.DataTextField = "CurrencyName";
            ddlCurrencyCode.DataValueField = "CurrencyCode";
            ddlCurrencyCode.DataBind();

            ddlCurrencyCode.Items.Insert(0, new ListItem("---Select Currency---", ""));
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


}