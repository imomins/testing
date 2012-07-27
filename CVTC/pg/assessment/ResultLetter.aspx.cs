using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;
using System.Text;
using System.Data.Odbc;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Drawing;


public partial class pg_assessment_ResultLetter : System.Web.UI.Page
{

    public string TextBoxHeaderHtml
    {
        get { return HttpUtility.HtmlDecode(TextBoxHeader.Text); }
        set { TextBoxHeader.Text = value; }
    }

    public string TextBoxShowAboveResultHtml
    {
        get { return HttpUtility.HtmlDecode(TextBoxShowAboveResult.Text); }
        set { TextBoxShowAboveResult.Text = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            LabelMessage.Text = "";
            LabelMessage.Text = "Please make sure that Header text Length Must Be Less than 1000 Characters and Footer text Length must be Less than 1500 Characters";
            LabelMessage.Width = 400;
            hplPrint.Visible = false;
        }
 
    }

   
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string aid = Request.QueryString["aid"].ToString();
            HiddenFieldAID.Value = aid;
            Session["aid"] = aid;
            Initialize(Convert.ToInt32(aid));
            GetRisks(Convert.ToInt32(aid));
        }
    }

    private void GetRisks(int aoid)
    {
        RiskCalculation rsk = new RiskCalculation();
        Collection<RiskCalculation> RisksList = new Collection<RiskCalculation>();
        RisksList = rsk.GetRiskCalulationByAOID(aoid);
        if (RisksList.Count != 0)
        {

            foreach (RiskCalculation r in RisksList)
            {
                ddlRisk.Items.Add(r.RiskName);
            }
        }

    }

    private void Initialize(int aid)
    {
       
        try
        {
            Assessment ass = new Assessment();
            ass = ass.GetAssessmentByOID(aid);
            TextBox txtBox;
            
            Label lbl ;//= new Label();

            ResultLetterDetail letterDetail = null;
            ResultLetter resultLetter = new ResultLetter();
            resultLetter = resultLetter.GetResultLetterByAOID(aid);

            if (resultLetter != null)
            {
                //TextBoxHeader.Text = resultLetter.Header;
                //TextBoxShowAboveResult.Text = resultLetter.ShowAboveResult;
                TextBoxHeaderHtml = resultLetter.Header;
                TextBoxShowAboveResultHtml = resultLetter.ShowAboveResult;
 
            }

            foreach (Section s in ass.SectionList)
            {
                if (s.SectionName == "NoScore") continue;
                if (resultLetter != null)
                {
                    try
                    {
                        if (resultLetter.LetterDetail != null)
                        {
                            var tmp = from detail in resultLetter.LetterDetail
                                      where detail.SectionOID == s.SectionOID
                                      select detail;
                            letterDetail = tmp != null ? tmp.First() : null;
                        }
                    }
                    catch (Exception ex)
                    {
                        letterDetail = null;
                    }
                }

                PlaceHolderSectionDefinition.Controls.Add(InsertLineBreaks(1));
                txtBox = new TextBox();
                txtBox.Height = 20;
                lbl = new Label();
                txtBox.ID = "txt" + s.SectionOID.ToString();
                txtBox.Width = 630;
                //txtBox.Height = 60;
                //txtBox.TextMode = TextBoxMode.MultiLine;
                txtBox.Text = (letterDetail != null) ? letterDetail.SectionDefinition : "";
                lbl.ID = "lbl" + s.SectionOID.ToString();
                lbl.Text = s.SectionName+" : Definition";
                PlaceHolderSectionDefinition.Controls.Add(lbl);
           //     PlaceHolderSectionDefinition.Controls.Add(InsertSpace(3));
                PlaceHolderSectionDefinition.Controls.Add(InsertLineBreaks(1));
                PlaceHolderSectionDefinition.Controls.Add(txtBox);
                PlaceHolderSectionDefinition.Controls.Add(InsertLineBreaks(1));
                
            }
        }
        catch (Exception ex)
        { }
    }

    


    private static Label InsertSpace(int noSpace)
    {
        Label lblLineBreak = new Label();

        for (int i = 0; i < noSpace; i++)
        {
            lblLineBreak.Text += "&nbsp;";
        }
        return lblLineBreak;
    }

    private static Label InsertLineBreaks(int breaks)
    {
        Label lblLineBreak = new Label();

        for (int i = 0; i < breaks; i++)
        {
            lblLineBreak.Text += "<br/>";
        }
        return lblLineBreak;
    }

    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        int aid = 0;
        try
        {
            User user = (User)Session["CurrentUser"];
            if (user == null) return;
            aid = (Session["aid"] != null) ? (Convert.ToInt32(Session["aid"])) : 0;
            Assessment ass = new Assessment();
            ass = ass.GetAssessmentByOID(aid);

            Collection<ResultLetterDetail> _list = new Collection<ResultLetterDetail>();
            ResultLetterDetail letterDetail = null;
            ResultLetter resultLetter = new ResultLetter();

            //resultLetter = resultLetter.GetResultLetterByOID();
            
            resultLetter.AssessmentOID = aid;
            resultLetter.CreatedBy = user.UserOID;
            resultLetter.LastModifiedBy = user.UserOID;
            
            //resultLetter.Header = TextBoxHeader.Text;
            //resultLetter.ShowAboveResult = TextBoxShowAboveResult.Text;

            resultLetter.Header = TextBoxHeaderHtml;
            resultLetter.ShowAboveResult = TextBoxShowAboveResultHtml;

            //resultLetter.Header = TextBoxShowAboveResult.Text;
            int HeaderTextLength = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["ResultLetterHeaderTextLenth"].ToString());
            int FooterTextLength = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["ResultLetterFooterTextLenth"].ToString());
            if (TextBoxHeaderHtml.Length < HeaderTextLength && TextBoxShowAboveResultHtml.Length < FooterTextLength)
            {

                string val = "";
                foreach (Section s in ass.SectionList)
                {
                    if (s.SectionName == "NoScore") continue;
                    letterDetail = new ResultLetterDetail();

                    val = Request.Form["txt" + s.SectionOID.ToString()];


                    //identifier = "txt" + s.SectionOID.ToString();
                    //txt = (TextBox)PlaceHolderSectionDefinition.FindControl(identifier);
                    //if (txt == null) continue;
                    letterDetail.SectionDefinition = val;
                    letterDetail.SectionOID = s.SectionOID;
                    letterDetail.LastModifiedBy = user.UserOID;
                    letterDetail.CreatedBy = user.UserOID;

                    _list.Add(letterDetail);
                }

                resultLetter.LetterDetail = _list;

                //check whether it is existing or not

                ResultLetter rletter = resultLetter.GetResultLetterByAOID(aid);
                if (rletter == null)
                {
                    if (resultLetter.AddResultLetter())
                    {
                        LabelMessage.Text = "Saved Successfully.";
                    }
                    else
                    {
                        LabelMessage.Text = "Saved Failed.";
                    }
                }
                else
                {
                    resultLetter.AssessmentResultLetter = rletter.AssessmentResultLetter;
                    for (int i = 0; i < resultLetter.LetterDetail.Count; i++)
                    {
                        try
                        {
                            resultLetter.LetterDetail[i].ResultLetterSectionCommentOID = rletter.LetterDetail[i].ResultLetterSectionCommentOID;
                        }
                        catch
                        {
                            resultLetter.LetterDetail[i].ResultLetterSectionCommentOID = -1;
                            resultLetter.LetterDetail[i].AssessmentLetterOID = rletter.AssessmentResultLetter;
                        }
                    }
                    if (resultLetter.UpdateResultLetter())
                    {
                        LabelMessage.Text = "Update Successfully.";
                        //LabelMessage .Text ="Header:"+TextBoxHeaderHtml.Length .ToString ()+"And Footer :"+TextBoxShowAboveResultHtml.Length .ToString ();
                    }
                    else
                    {
                        LabelMessage.Text = "Update Failed.";
                    }
                }


                TextBoxHeaderHtml = "";
                TextBoxShowAboveResultHtml = "";
               
                //Initialize(aid);

            }
            else
            {
                LabelMessage.Text = "Sorry! can't Save. Header Length Must Be Less than 1000 Characters and Footer Length must be Less than 1500 Characters";
                LabelMessage.ForeColor = Color.Red;
                LabelMessage.Width = 350;
            }
            Initialize(aid);

        }
        catch (Exception ex)
        {
            Initialize(aid);
        }
    }
    protected void ButtonRefresh_Click(object sender, EventArgs e)
    {        
        int   aid=Convert.ToInt32(Session["aid"]);
        Initialize(aid);
        LabelMessage.Text = "";
        LabelMessage.Text = "Please make sure that Header text Length Must Be Less than 1000 Characters and Footer text Length must be Less than 1500 Characters";
        LabelMessage.Width = 400;

    }

    protected void process_mydiv1_Click(object sender, EventArgs e)
    {
     string url = null;
     int time=Convert .ToInt32 ( number_of_times.Value);
     string riskName = ddlRisk.Text;
     int aid = Convert.ToInt32(Session["aid"]);
     url = "PrintResultLetter.aspx?aid=" + aid + "&mypid=" + time + "&risk=" + riskName+"";
     //hplPrint.Visible = false ;
     //hplPrint.NavigateUrl = url;

  
     Initialize(aid);
     LabelMessage.Text = "";
     LabelMessage.Text = "Please make sure that Header text Length Must Be Less than 1000 Characters and Footer text Length must be Less than 1500 Characters";
     LabelMessage.Width = 400;

     //Response.Redirect(url);
     Response.Write("<script>window.open('" + url + "')</script>");

    }
}
