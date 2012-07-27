using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class t : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Write();
    }

    private void Write()
    {
        Response.ContentType = "application/xml";
        Response.Write("<?xml version=\"1.0\"?>");
        Response.Write("<rows><page>1</page><total>1</total><records>1</records><row><cell>1</cell><cell>Home</cell><cell>home.aspx</cell><cell>0</cell><cell>1</cell><cell>2</cell><cell>true</cell>s<cell>true</cell></row></rows>");        


        Response.End();
    }
}
