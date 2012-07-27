using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;


public partial class pg_student_tttt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

       // XMLParse();
    }

    public void XMLParse()
    {
        string path = @"D:\Asp.net\CVTC\pg\student\cars_tree.xml";
        XmlTextReader reader = new XmlTextReader(path);
        //XmlTextReader reader = new XmlTextReader( Server.MapPath(path));

        XmlDocument xmlDoc = new XmlDocument(); //* create an xml document object.
        xmlDoc.Load(reader);
         
         Response.Write(xmlDoc.InnerText);
        
    }
}
