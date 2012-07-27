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

public partial class ExamHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            string aid = Request.QueryString["aid"].ToString();
            //this.Page.ClientScript.RegisterHiddenField("HiddenAOID", aid);
            HiddenAOID.Value = aid;

            //Assessment ass = new Assessment();
            //ass = ass.GetAssessmentByOID(Convert.ToInt32(aid));
            //if (ass != null)
            //{
            //    LabelAsssessment.Text = ass.AssessmentName;
            //}
        }
        catch (Exception ex)
        { }
    
    }
}
