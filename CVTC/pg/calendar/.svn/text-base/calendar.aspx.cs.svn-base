using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Collections.ObjectModel;

public partial class pg_calendar_calendar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Initialize();
        }
    }

    private void Initialize()
    {
        try
        {
            if (Session["CurrentUser"] != null)
            {
                User u = (User)Session["CurrentUser"];
                HiddenFieldCurrentUser.Value = u.UserOID.ToString();
            }
        }
        catch (Exception ex)
        { }
    }

    [WebMethod(true)]
    public static string EventList()
    {

        return "true";
    }
}
