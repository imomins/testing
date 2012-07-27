using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.Security;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!Page.IsPostBack)

        {
            if (Session["CurrentUser"] == null)
            {
                Response.Redirect("~/login.aspx");
            }
            else
            {
                Initialization();
            }
        }
        else if (Session["CurrentUser"] == null)
        {
            Response.Redirect("~/login.aspx");
        }
    }

    private void Initialization()
    {
        try
        {
            if (Session["CurrentUser"] != null)
            {
                User u = (User)Session["CurrentUser"];
                LabelUserName.Text = u != null ? u.LastName + ", " + u.FirstName : "";
                LabelUserName.Text += "[" + DateTime.Now.ToString() + "]";
            }
            
        }
        catch (Exception ex)
        { }
    }

    [System.Web.Services.WebMethod]
    public static string GetXML()
    {
        //XmlTextReader reader = new XmlTextReader(
        //@"c:\tree.xml");

        //reader.WhitespaceHandling = WhitespaceHandling.None;
        //XmlDocument xmlDoc = new XmlDocument();
        ////Load the file into the XmlDocument

        //xmlDoc.Load(reader);
        ////Close off the connection to the file.

        //reader.Close();

        //return xmlDoc;

        string str="";

    
        return str;

    }
    protected void lbtnLogOut_Click(object sender, EventArgs e)
    {
        

        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoServerCaching();
        HttpContext.Current.Response.Cache.SetNoStore();
        Session.Abandon();

        Response.Redirect("~/login.aspx",true);


        
    }
}