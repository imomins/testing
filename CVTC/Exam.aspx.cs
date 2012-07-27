using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Exam : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int aid = (Request.QueryString["aid"] == null) ? 0 : Convert.ToInt32(Request.QueryString["aid"].ToString());
            string reg = (Request.QueryString["reg"] == null) ? "" : Convert.ToString(Request.QueryString["reg"].ToString());
            //int aid = 13;
            Initialize(aid, reg);
        }
        catch (Exception ex)
        { }
    }

    private void Initialize(int aid,string reg)
    {
        try
        {
            //aid
            Assessment ass = new Assessment();
            ass=ass.GetAssessmentByOID(aid);
            if (ass.AssessmentName != null&&reg =="")
            {
                FormsAuthentication.SetAuthCookie("Student", false);
                Response.Redirect("~/pg/answer/home.aspx?aid=" + aid,true);
                
            }
            else if (ass.AssessmentName != null && reg == "yes"&&Request.QueryString["id"] != null)
            {
                FormsAuthentication.SetAuthCookie("Student", false);
                string url="~/pg/answer/home.aspx?aid=" + aid+"&reg=yes&id="+Request.QueryString["id"];
                Response.Redirect(url, true);
            }
            else
            {
                Response.Write("Your link is not correct");
            }
            
        }
        catch (Exception ex)
        { }

    }
}
