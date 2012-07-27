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

public partial class pg_answer_assessmentList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Initialize();

    }

    private void Initialize()
    {
        try
        {
            Assessment ass = new Assessment();
            GridViewAssessment.DataSource=ass.GetAllAssessmnet();
            GridViewAssessment.DataBind();
        }
        catch (Exception ex)
        {             
        }
    }
}
