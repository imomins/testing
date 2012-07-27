using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Services;

using System.Web.UI.WebControls;
using System.Xml.Linq;


public partial class pg_message_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

   [WebMethod]
  public static string ServerSideMethod()
  {
      return "Message from server.";
  }
}