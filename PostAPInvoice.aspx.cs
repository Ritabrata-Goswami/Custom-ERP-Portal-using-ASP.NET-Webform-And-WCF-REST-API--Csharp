using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PostAPInvoice : System.Web.UI.Page
{
    protected string StrConn = ConfigurationManager.ConnectionStrings["ERP_Db_Conn_Str"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CurrencyBinding();
            DocStatusBinding();
            TaxCodeBinding();
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

    protected void DocStatusBinding()
    {
        SqlConnection Conn = null;
        DataTable Dt = null;

        try
        {
            Conn = new SqlConnection(StrConn);
            Conn.Open();

            string SqlTxt = @"SELECT * FROM DocStatusEnum";
            Dt = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter(SqlTxt, Conn);
            Adp.Fill(Dt);

            ddlDocStatus.DataSource = Dt;
            ddlDocStatus.DataTextField = "DocStatusName";
            ddlDocStatus.DataValueField = "DocStatusCode";
            ddlDocStatus.DataBind();

            ddlDocStatus.Items.Insert(0, new ListItem("---Select Document Status---", ""));
        }
        catch (Exception ex)
        {
            string ErrMsg = ex.Message.Replace("'", "\\'");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "DocStatusBindingError", $"alert('Error due to: {ErrMsg}')", true);
        }
        finally
        {
            Conn.Close();
        }
    }

    protected void TaxCodeBinding()
    {
        SqlConnection Conn = null;
        DataTable Dt = null;

        try
        {
            Conn = new SqlConnection(StrConn);
            Conn.Open();

            string SqlTxt = @"SELECT * FROM TaxCodeMaster";
            Dt = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter(SqlTxt, Conn);
            Adp.Fill(Dt);

            ddlTaxCodeDtl.DataSource = Dt;
            ddlTaxCodeDtl.DataTextField = "TaxName";
            ddlTaxCodeDtl.DataValueField = "TaxCode";
            ddlTaxCodeDtl.DataBind();

            ddlTaxCodeDtl.Items.Insert(0, new ListItem("---Select Tax Code---", ""));
        }
        catch (Exception ex)
        {
            string ErrMsg = ex.Message.Replace("'", "\\'");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "TaxCodeBindingError", $"alert('Error due to: {ErrMsg}')", true);
        }
        finally
        {
            Conn.Close();
        }
    }



}