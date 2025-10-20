using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : MasterPage
{
    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;

    protected void Page_Init(object sender, EventArgs e)
    {
        // The code below helps to protect against XSRF attacks
        var requestCookie = Request.Cookies[AntiXsrfTokenKey];
        Guid requestCookieGuidValue;
        if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
        {
            // Use the Anti-XSRF token from the cookie
            _antiXsrfTokenValue = requestCookie.Value;
            Page.ViewStateUserKey = _antiXsrfTokenValue;
        }
        else
        {
            // Generate a new Anti-XSRF token and save to the cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
            Page.ViewStateUserKey = _antiXsrfTokenValue;

            var responseCookie = new HttpCookie(AntiXsrfTokenKey)
            {
                HttpOnly = true,
                Value = _antiXsrfTokenValue
            };
            if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
            {
                responseCookie.Secure = true;
            }
            Response.Cookies.Set(responseCookie);
        }

        Page.PreLoad += master_Page_PreLoad;
    }

    protected void master_Page_PreLoad(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Set Anti-XSRF token
            ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
            ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
        }
        else
        {
            // Validate the Anti-XSRF token
            if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
            {
                throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["EmpName"] == null || Session["EmpId"] == null || Session["RowId"] == null)
            {
                Response.Redirect("/Default.aspx");
                return;
            }

            string EmpId = Request.QueryString["empId"];
            string RowId = Request.QueryString["rowId"];

            if (!string.IsNullOrEmpty(EmpId) || !string.IsNullOrEmpty(RowId))
            {
                txtEmpId.Text = EmpId.Trim();
            }

            DashboardLink.NavigateUrl = "/VendorMaster.aspx?empId=" + Session["EmpId"] + "&rowId=" + Session["RowId"];
            ItemMasterLink.NavigateUrl = "/ItemMaster.aspx?empId=" + Session["EmpId"] + "&rowId=" + Session["RowId"];
            WarehouseMasterLink.NavigateUrl= "/WarehouseMaster.aspx?empId=" + Session["EmpId"] + "&rowId=" + Session["RowId"];
            EntryVendor.NavigateUrl = "/EntryVendorMaster.aspx?empId=" + Session["EmpId"] + "&rowId=" + Session["RowId"];
            EntryItem.NavigateUrl = "/EntryItemMaster.aspx?empId=" + Session["EmpId"] + "&rowId=" + Session["RowId"];
            EntryWarehouse.NavigateUrl = "/EntryWarehouseMaster.aspx?empId=" + Session["EmpId"] + "&rowId=" + Session["RowId"];
            EntryPO.NavigateUrl = "/PurchaseOrder.aspx?empId=" + Session["EmpId"] + "&rowId=" + Session["RowId"];
            SearchPO.NavigateUrl= "/ViewPO.aspx?empId=" + Session["EmpId"] + "&rowId=" + Session["RowId"];
        }

    }

    protected void Session_Logout(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("/Default.aspx");
    }

    protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        Context.GetOwinContext().Authentication.SignOut();
    }
}