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


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string savePO(Cls_PurchaseOrderHdr jsonObj)
    {
                //Convert incoming object to JSON string
        string json = jsonObj.ToString();

                //Deserialize to C# class
        Cls_PurchaseOrderHdr PO = JsonConvert.DeserializeObject<Cls_PurchaseOrderHdr>(json);

                //Now you can access header and line details
        string PONumber = PO.PONumber.Trim();
        DateTime PODate = Convert.ToDateTime(PO.PODate);
        DateTime ExpectDlvDate = Convert.ToDateTime(PO.ExpectedDlvDate);
        string PaymentTerms = PO.PaymentTerms.Trim();
        string CreatedBy = PO.EmpId.Trim();
        DateTime CreatedTime = DateTime.Now;
        decimal TotalAmount = Convert.ToDecimal(PO.TotalAmount);
        int CurrencyCode = Convert.ToInt32(PO.Currency);
        string Remarks = PO.Remarks.Trim();

        foreach (var line in PO.DtlPO)
        {
            
        }

        return "Success";
    }

}