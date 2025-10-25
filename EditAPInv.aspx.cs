using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class EditAPInv : System.Web.UI.Page
{
    protected string StrConn = ConfigurationManager.ConnectionStrings["ERP_Db_Conn_Str"].ConnectionString;
    protected string APInvNo = "";
    protected string APInvRowId = "";
    protected string DocStatusVal = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if(String.IsNullOrEmpty(Request.QueryString["InvNo"]) || string.IsNullOrEmpty(Request.QueryString["Id"]) || Session["EmpId"] == null)
            {
                Response.Redirect("/Default.aspx");
                return;
            }
            else
            {
                APInvNo = Request.QueryString["InvNo"].Trim();
                APInvRowId = Request.QueryString["Id"].Trim();
            }

            DocStatusBinding();
            InvStatusValBinding();
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

            ddlStatus.DataSource = Dt;
            ddlStatus.DataTextField = "DocStatusName";
            ddlStatus.DataValueField = "DocStatusCode";
            ddlStatus.DataBind();

            ddlStatus.Items.Insert(0, new ListItem("---Select Document Status---", ""));
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

    public void InvStatusValBinding()
    {
        SqlConnection Conn = null;
        DataTable Dt = null;
        string RowId = Request.QueryString["Id"].Trim();
        string InvNo = Request.QueryString["InvNo"].Trim();

        try
        {
            Conn = new SqlConnection(StrConn);
            Conn.Open();

            string SqlTxt = @"SELECT * FROM ApInvoice_Hdr WHERE Id='" + RowId + "'AND InvNo='" + InvNo + "'";
            SqlCommand Cmd = new SqlCommand(SqlTxt, Conn);
            SqlDataReader Reader =Cmd.ExecuteReader();

            if (Reader.Read())
            {
                DocStatusVal = Reader["Status"].ToString();
            }
            ddlStatus.SelectedValue = (DocStatusVal == "C") ? "C" : "O";
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


}