using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GoodsReceiptPO : System.Web.UI.Page
{
    protected string ConnStr = ConfigurationManager.ConnectionStrings["ERP_Db_Conn_Str"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DocStatus();
        }
    }

    protected void DocStatus()
    {
        SqlConnection Conn = new SqlConnection(ConnStr);

        try
        {
            Conn.Open();
            string SqlTxt = @"SELECT * FROM DocStatusEnum";
            SqlDataAdapter Adp = new SqlDataAdapter(SqlTxt, Conn);
            DataTable Dt = new DataTable();
            Adp.Fill(Dt);

            ddlDocStatus.DataSource = Dt;
            ddlDocStatus.DataTextField = "DocStatusName";
            ddlDocStatus.DataValueField = "DocStatusCode";
            ddlDocStatus.DataBind();
            ddlDocStatus.Items.Insert(0, new ListItem("---Select Document Status---",""));
        }
        catch (Exception ex) 
        {
            string ErrMsg = ex.Message.Replace("'","\\'");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error Response Message", $"alert('Error due to: {ErrMsg}');", true);
        }
        finally
        {
            Conn.Close();
        }
    }


}