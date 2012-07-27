using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.Odbc;
using System.Web.Script.Serialization;
using System.Collections.ObjectModel;

public partial class pg_assessment_ResultEmail : System.Web.UI.Page
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
            string aid = Request.QueryString["aid"].ToString();
            //Response.Write(aid);
            Session["aid"] = aid;
            Initialize(Convert.ToInt32( aid));

            LabelMessage.Text = "";
            //PrintIntervention(Convert.ToInt32(aid), "@036463");
        }
    }

    //private void PrintIntervention(int aid, string studentID)
    //{
    //    try
    //    {
    //        Assessment ass = new Assessment();
    //        ass = ass.GetAssessmentByOID(aid);
    //        ResultEmail resultEmail = new ResultEmail();
    //        resultEmail = resultEmail.GetResultEmailByAOID(aid);

    //        Student student = new Student().GetStudentByStudentOID(studentID);

    //        StringBuilder sb = new StringBuilder();
    //        sb.Append("<table cellpadding='0px' cellspacing='0px' style='width:100%; margin:0px 30px 0px 30px'>");

    //        sb.Append("<tr>");
    //        sb.Append("<td align='center'  style='font-size:24px; font-weight:bold;'> " + ass.AssessmentName + " Feedback Report</td>");
    //        sb.Append("</tr>");
    //        sb.Append("<tr>");
    //        sb.Append("<td>CVTC applicant: <b>" + student.FullName + "</b> Program Interest: <b>" + student.MajorProgramEnrollment + "</b></td>");
    //        sb.Append("</tr>");
    //        sb.Append("<tr>");
    //        sb.Append("<td >" + resultEmail.Header + "</td>");
    //        sb.Append("</tr>");
    //        sb.Append("<tr>");
    //        sb.Append("<td ><b>*Your individual Inventory of Student Success assessment results suggest . . .</b></br></td>");
    //        sb.Append("</tr>");

    //        StudentRank studentRank = new StudentRank();
    //        Collection<StudentRank> studentRankList = studentRank.GetStudentRankBySOIDandAOID(student.StudentOID, ass.AssessmentOID);

    //        int i = 0;
    //        string txtSign="", txtComments="";
    //        sb.Append("<td>");
    //        sb.Append("<table cellpadding='0px' cellspacing='0px' style='width:80%;'>");
    //        foreach (StudentRank SR in studentRankList)
    //        {
    //            txtComments = SR.Comment;
    //            foreach (Section s in ass.SectionList)
    //            {
    //                if (SR.SectionOID == s.SectionOID)
    //                {
    //                    if (SR.Rank >= s.High)
    //                    {
    //                        txtSign = "+";
    //                    }
    //                    else if ((SR.Rank < s.High) && (SR.Rank >= s.Medium))
    //                    {
    //                        txtSign = "~";
    //                    }
    //                    else if ((SR.Rank < s.Medium) && (SR.Rank >= s.Low))
    //                    {
    //                        txtSign = "-";
    //                    }
    //                    else
    //                    {
    //                        txtSign = "";
    //                        txtComments = "";
    //                    }
    //                    break;
    //                }
    //            }

    //            if (i % 2 == 0)
    //            {
    //                sb.Append("<tr>");
    //                sb.Append("<td align='right' valign='middle' style='width:2%'><b>" + txtSign + "</b></td>");
    //                sb.Append("<td style='width:48%; border: thin solid #000000'> " + txtComments + " </td>");
    //            }
    //            else
    //            {
    //                sb.Append("<td align='right'  valign='middle' style='width:2%'><b>" + txtSign + "</b></td>");
    //                sb.Append("<td style='width:48%; border: thin solid #000000'> " + txtComments + " </td>");
    //                sb.Append("</tr>");
    //            }
    //            i++;
    //            txtSign = ""; 
    //            txtComments = "";
    //        }
            
    //        if (studentRankList.Count % 2 == 0)
    //        {
    //            sb.Append("<tr>");
    //            sb.Append("<td colspan='4'></br>*( +above average ~average -below average )</br></td>");
    //            sb.Append("</tr>");
    //        }
    //        else
    //        {
    //            sb.Append("<td></br>*( +above average ~average -below average )</br></td>");
    //            sb.Append("</tr>");
    //        }
    //        sb.Append("</table>");
    //        sb.Append("</td>");
    //        sb.Append("<tr>");
    //        sb.Append("<td > </br>" + resultEmail.ShowAboveResult + "</td>");
    //        sb.Append("</tr>");

    //        sb.Append("</table>");
    //        print_div1.InnerHtml = sb.ToString();
    //    }
    //    catch (Exception ex)
    //    { }
    //}

    private void Initialize(int aid)
    {
        try
        {
            string html = "";
            Assessment ass = new Assessment();
            ass = ass.GetAssessmentByOID(aid);
            ResultEmail resultEmail = new ResultEmail();
            resultEmail = resultEmail.GetResultEmailByAOID(aid);
            string txtLow, txtMedium, txtHigh;

            if (resultEmail != null)
            {
                //TextBoxHeader.Text = resultEmail.Header;
                //TextBoxShowAboveResult.Text = resultEmail.ShowAboveResult;

                TextBoxHeaderHtml = resultEmail.Header;
                TextBoxShowAboveResultHtml = resultEmail.ShowAboveResult;
            }            

            ResultEmailDetail rEmailDetail=null;
            html = "<table>";
            foreach (Section s in ass.SectionList)
            {
                //PlaceHolderResult.Controls.Add(InsertLineBreaks(1));
                if (s.SectionName != "NoScore")
                {
                    if (resultEmail != null)
                    {
                        if (resultEmail.ResultDetail != null)
                        {
                            var tmp = from detail in resultEmail.ResultDetail
                                      where detail.SectionOID == s.SectionOID
                                      select detail;


                            if (tmp.Count<ResultEmailDetail>() > 0)
                            {
                                rEmailDetail = tmp != null ? tmp.First() : null;
                            }
                            else
                            {
                                rEmailDetail = null;
                            }


                        }
                    }
                    txtLow = (rEmailDetail != null) ? rEmailDetail.LowResult : "";
                    txtMedium = (rEmailDetail != null) ? rEmailDetail.MediumResult : "";
                    txtHigh = (rEmailDetail != null) ? rEmailDetail.HighResult : "";

                    html += "<tr>";
                    html += "<td colspan='3' style='font-size:6;font-weight:bold;'>" + s.SectionName + "<td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td>" + "Low Ranking <br/>" + "<input  id='txtLow" + s.SectionOID.ToString() + "' name='txtLow" + s.SectionOID.ToString() + "' type='text' style='height:20px;width:250px;' value='" + txtLow + "'/>" + "<td>";
                    html += "<td>" + "Medium Ranking <br/>" + "<input id='txtMedium" + s.SectionOID.ToString() + "'  name='txtMedium" + s.SectionOID.ToString() + "' type='text' style='height:20px;width:250px;' value='" + txtMedium + " '/>" + "<td>";
                    html += "<td>" + "High Ranking <br/>" + "<input id='txtHigh" + s.SectionOID.ToString() + "' name='txtHigh" + s.SectionOID.ToString() + "' type='text' style='height:20px;width:250px;' value='" + txtHigh + " '/>" + "<td>";
                    html += "</tr>";

                    #region Old Code
                    ////Positive Result
                    //txtBox = new TextBox();
                    //lbl = new Label();
                    //txtBox.ID = "txtPositive" + s.SectionOID.ToString();
                    //txtBox.Text = (rEmailDetail != null) ? rEmailDetail.PositiveResult : "";
                    //lbl.ID = "lblPositive" + s.SectionOID.ToString();
                    //lbl.Text = "Positive Result for " + s.SectionName;
                    //PlaceHolderResult.Controls.Add(lbl);
                    //PlaceHolderResult.Controls.Add(InsertSpace(2));
                    //PlaceHolderResult.Controls.Add(txtBox);
                    //PlaceHolderResult.Controls.Add(InsertSpace(10));

                    ////Negative Result
                    //txtBox = new TextBox();
                    //lbl = new Label();
                    //txtBox.ID = "txtNegative" + s.SectionOID.ToString();
                    //txtBox.Text = (rEmailDetail != null) ? rEmailDetail.NegativeResult : "";
                    //lbl.ID = "lblNegative" + s.SectionOID.ToString();
                    //lbl.Text = "Negative Result for "+s.SectionName;
                    //PlaceHolderResult.Controls.Add(lbl);
                    //PlaceHolderResult.Controls.Add(InsertSpace(2));
                    //PlaceHolderResult.Controls.Add(txtBox);

                    //PlaceHolderResult.Controls.Add(InsertLineBreaks(1)); 
                    #endregion

                }
            }
            html += " </table>";
            ResultHolder.InnerHtml = html;
            //PlaceHolderResult.Controls.Add(html);
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
            User user=(User)Session["CurrentUser"];
            if (user == null) return;
            System.Collections.ObjectModel.Collection<ResultEmailDetail> _list = new System.Collections.ObjectModel.Collection<ResultEmailDetail>();
            ResultEmailDetail emailDetail=null;
            aid = (Session["aid"] != null) ? (Convert.ToInt32(Session["aid"])) : 0;
            Assessment ass = new Assessment();
            ass = ass.GetAssessmentByOID(aid);

            ResultEmail resultEmail = new ResultEmail();
            //resultEmail.Header = TextBoxHeader.Text;
            //resultEmail.ShowAboveResult = TextBoxShowAboveResult.Text;
            resultEmail.Header = TextBoxHeaderHtml;
            resultEmail.ShowAboveResult = TextBoxShowAboveResultHtml;

            resultEmail.AssessmentOID = aid;
            resultEmail.CreatedBy = user.UserOID;
            resultEmail.LastModifiedBy = user.UserOID;

            //Result Detail
            ResultEmail rEmail = resultEmail.GetResultEmailByAOID(aid);
            //TextBox txt;
            string val = "";
            foreach (Section s in ass.SectionList)
            {
                emailDetail = new ResultEmailDetail();
                ////Positive Result
                //val = Request.Form["txtPositive" + s.SectionOID.ToString()];                
                //emailDetail.PositiveResult = val;

                ////Negative Result                
                //val = Request.Form["txtNegative" + s.SectionOID.ToString()];
                //emailDetail.NegativeResult = val;

                //Low Result                
                val = Request.Form["txtLow" + s.SectionOID.ToString()];
                emailDetail.LowResult = val;

                //Medium Result                
                val = Request.Form["txtMedium" + s.SectionOID.ToString()];
                emailDetail.MediumResult = val;

                //High Result                
                val = Request.Form["txtHigh" + s.SectionOID.ToString()];
                emailDetail.HighResult = val;

                emailDetail.SectionOID = s.SectionOID;
                emailDetail.LastModifiedBy = user.UserOID;
                emailDetail.CreatedBy = user.UserOID;
                
                
                _list.Add(emailDetail);

            }
            resultEmail.ResultDetail = _list;

            //check whether It is exist or not
            
            if (rEmail == null)
            {
                if (resultEmail.AddResultEmail())
                {
                    LabelMessage.Text = "Saved Successfully.";
                }
                else
                {
                    LabelMessage.Text = "Not Saved.";
                }
            }
            else
            {
                resultEmail.AssessmentResultOID = rEmail.AssessmentResultOID;
                for (int i = 0; i < rEmail.ResultDetail.Count; i++)
                {
                    resultEmail.ResultDetail[i].ResultSectionOID = rEmail.ResultDetail[i].ResultSectionOID;

                }

                if (resultEmail.UpdateResultEmail())
                {
                    LabelMessage.Text = "Update Successfully.";
                }
                else
                {
                    LabelMessage.Text = "Update Failed.";
                }
            }


            TextBoxShowAboveResultHtml = "";
            TextBoxHeaderHtml = "";
            this.Initialize(aid);
        }
        catch (Exception ex)
        {
            
           this.Initialize(aid);
        }
    }



    protected void ButtonRefresh_Click(object sender, EventArgs e)
    {
        int aid = Convert.ToInt32(Session["aid"]);
        Initialize(Convert.ToInt32(aid));
        LabelMessage.Text = "";
    }
}
