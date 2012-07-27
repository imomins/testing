using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Collections.ObjectModel;

public partial class pg_message_temp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public static string ServerSideMethod(string param1)
    {
        User u=new User();
        Collection<User> list = u.GetAllUser();

        return "Message from server." + param1;
    }
}
