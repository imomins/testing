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

using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data.Odbc;

public partial class pg_assessment_TermCode : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int assessmentOID = 0;
            if (!string.IsNullOrEmpty(Request.QueryString["AssessmentOID"]))
            {
                assessmentOID = Convert.ToInt32(Request.QueryString["AssessmentOID"]);
                ViewState["AssessmentOID"] = assessmentOID;
            }
            this.Initialize(assessmentOID);
        }
    }

    private void Initialize(int assessmentOID)
    {
        TermCode termCode = new TermCode();
        Collection<TermCode> termCodeList = termCode.GetTermCodeByAssessmentOID(assessmentOID);

        GridViewLocations.DataSource = termCodeList;
        GridViewLocations.DataBind();
    }
    protected void ImageButtonEditLocation_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            HiddenField hiddenFieldLocationOID = (HiddenField)row.Cells[0].FindControl("HiddenFieldLocationOID");
            int chapterOID = Convert.ToInt32(hiddenFieldLocationOID.Value);


            TermCode termCode = new TermCode();
            termCode = termCode.GetTermCodeByTermCodeOID(chapterOID);
            if (termCode != null)
            {
                ViewState["TermCode"] = termCode;
                TextBoxTermCode.Text = termCode.TermCodeName;
            }
            else
            {
                ViewState["TermCode"] = null;
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void ImageButtonDeleteLocation_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
            HiddenField hiddenFieldLocationOID = (HiddenField)row.Cells[0].FindControl("HiddenFieldLocationOID");
            int locationOID = Convert.ToInt32(hiddenFieldLocationOID.Value);
            TermCode termCode = new TermCode();
            bool status = termCode.DeleteTermCode(locationOID);
            if (status)
            {
                this.Initialize(termCode.AssessmentOID);
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        TermCode termCode = new TermCode();

        if (ViewState["TermCode"] == null)
        {
            termCode.AssessmentOID = Convert.ToInt32(ViewState["AssessmentOID"]);
            termCode.TermCodeName = TextBoxTermCode.Text.Trim();
            bool status = termCode.AddTermCode(termCode);
            if (status)
            {
                TextBoxTermCode.Text = "";
                this.Initialize(termCode.AssessmentOID);
            }

        }
        else
        {
            termCode = ViewState["TermCode"] as TermCode;
            termCode.TermCodeName = TextBoxTermCode.Text.Trim();
            termCode.UpdateTermCode(termCode);

            this.Initialize(termCode.AssessmentOID);
        }
    }
}
