using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditVendor : System.Web.UI.Page
{
    protected string MasterType="";
    protected string Code = "";
    protected string StrConn = ConfigurationManager.ConnectionStrings["ERP_Db_Conn_Str"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MasterType = Request.QueryString["Type"];
            Code = Request.QueryString["Code"];

            if (string.IsNullOrEmpty(MasterType) || String.IsNullOrEmpty(Code))
            {
                Session.Clear();
                Response.Redirect("/Default.aspx");
                return;
            }
            else
            {
                MasterType = MasterType.Trim().ToLower();
                Page.Title = "ERP Portal - Edit " + MasterType + " Master";
            }

            string SqlTxt = "";
            string Active = "";
            SqlConnection Conn = new SqlConnection(StrConn);
            Conn.Open();

            switch (MasterType)
            {
                case "item":
                    SqlTxt = "SELECT Active FROM ItemMaster WHERE ItemCode=@Code";
                    break;
                case "vendor":
                    SqlTxt = "SELECT Active FROM VendorMaster WHERE VendorCode=@Code";
                    break;
                case "warehouse":
                    SqlTxt = "SELECT Active FROM WarehouseMaster WHERE WareHouseCode=@Code";
                    break;
            }
            
            SqlCommand Cmd = new SqlCommand(SqlTxt, Conn);
            Cmd.Parameters.AddWithValue("@Code", Code.Trim());

            SqlDataReader Reader = Cmd.ExecuteReader();
            if (Reader.HasRows)
            {
                if (Reader.Read())
                {
                    Active = Reader["Active"].ToString();
                }

                ddlActive.SelectedValue = (Active == "Y") ? "Y" : "N";
            }

            Conn.Close();
        }
    }


    protected void EditData(object sender, EventArgs e)
    {
        SqlConnection Conn = null;
        MasterType = Request.QueryString["Type"];
        Code = Request.QueryString["Code"];

        try
        {
            Conn =new SqlConnection(StrConn);
            Conn.Open();

            string ActiveVal = ddlActive.Text.Trim();
            string SqlTxt = "";

            if (String.IsNullOrEmpty(MasterType) || String.IsNullOrEmpty(Code))
            {
                Session.Clear();
                Response.Redirect("/Default.aspx");
                return;
            }
            else
            {
                MasterType = MasterType.Trim().ToLower();
            }

            switch (MasterType)
            {
                case "item":
                    SqlTxt = @"UPDATE ItemMaster SET Active='" + ActiveVal + "' WHERE ItemCode=@Code";
                    break;
                case "vendor":
                    SqlTxt = @"UPDATE VendorMaster SET Active='" + ActiveVal + "' WHERE VendorCode=@Code";
                    break;
                case "warehouse":
                    SqlTxt = @"UPDATE WarehouseMaster SET Active='" + ActiveVal + "' WHERE WareHouseCode=@Code";
                    break;
            }
            SqlCommand Cmd = new SqlCommand(SqlTxt, Conn);
            Cmd.Parameters.AddWithValue("@Code", Code.Trim());
            int RowUpdated= Cmd.ExecuteNonQuery();
            if (RowUpdated>0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ResSuccessMessage", $"alert('Data Updated Successfully!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ResFailedMessage", $"alert('Error: Data did not updated., try again');", true);
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