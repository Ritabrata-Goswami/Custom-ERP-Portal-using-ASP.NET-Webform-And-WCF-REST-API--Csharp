using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["EmpName"] != null || Session["EmpId"] != null || Session["RowId"] != null)
            {
                Response.Redirect("/VendorMaster.aspx?empId=" + Session["EmpId"] + "&rowId=" + Session["RowId"]);
            }
        }
    }

    protected void Login(object sender, EventArgs e)
    {
        string ConnStr = ConfigurationManager.ConnectionStrings["ERP_Db_Conn_Str"].ConnectionString;
        SqlConnection Conn = null;
        string SqlTxt = "";
        string ActiveUser = "N";

        try
        {
            string EmpId = Enter_EmpId.Text.Trim().ToLower();
            string Password = Enter_Password.Text.Trim().ToLower();
            Conn = new SqlConnection(ConnStr);
            Conn.Open();

            if (string.IsNullOrEmpty(EmpId) || string.IsNullOrEmpty(Password))
            {
                txtMessage.Text = "Both User Id and Password are needed to login!";
            }
            else
            {
                SqlTxt = "SELECT * FROM UserLogin WHERE EmpId=@empId AND [Password]=@pass";
                SqlCommand cmd = new SqlCommand(SqlTxt, Conn);

                cmd.Parameters.AddWithValue("@empId", EmpId);
                cmd.Parameters.AddWithValue("@pass", Password);

                SqlDataReader DataReader = cmd.ExecuteReader();
                if (DataReader.HasRows)
                {
                    DataReader.Read();

                    ActiveUser = DataReader["ActiveUser"].ToString();
                    if (ActiveUser == "Y")
                    {
                        string fullName = DataReader["FName"].ToString().Trim() + DataReader["LName"].ToString().Trim();
                        string empId = DataReader["EmpId"].ToString().Trim();
                        string rowId = DataReader["Id"].ToString().Trim();

                        Session["EmpName"] = fullName;
                        Session["EmpId"] = empId;
                        Session["RowId"] = rowId;

                        Response.Redirect("/VendorMaster.aspx?empId=" + Session["EmpId"] + "&rowId=" + Session["RowId"]);

                    }
                    else
                    {
                        txtMessage.Text = "Not active user, login failed!";
                    }
                }
                else
                {
                    txtMessage.Text = "User credentials mismatch, login failed!";
                }
            }
        }
        catch (Exception ex)
        {
            txtMessage.Text = ex.Message;
        }
        finally
        {
            Conn.Close();
            ActiveUser = "N";
        }


    }
}