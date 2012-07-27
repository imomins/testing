using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.IO;

public partial class pg_user_test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string ServerSideMethod(string str)
    {
        string value2 = File.ReadAllText(@"C:\1.txt");
        //Textreader tr = new StreamReader(@"c:\1.txt");
        return value2;
    }
}
