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


public partial class pg_assessment_PrintResultLetter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Visible = true;
        if (!Page.IsPostBack)
        {
            string aid = Request.QueryString["aid"].ToString();
            string riskName = null;
            if (!string.IsNullOrEmpty(Request.QueryString["risk"]))
            {
                 riskName = Request.QueryString["risk"] as string;
            }
            if (!string.IsNullOrEmpty(Request.QueryString["mypid"]))
            {
                string rlst = Request.QueryString["mypid"] as string;
                //PrintIntervention(Convert.ToInt32(aid), Convert.ToInt32(rlst));
                PrintIntervention(Convert.ToInt32(aid), Convert.ToInt32(rlst), riskName);
                Label1.Visible = false;
                PrintWindow();
            }

        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        Label1.Visible = true;
        Label1.Text = "Please wait Until Printing Start......";
    }

    private void PrintWindow()
    {
        Response.Write("<script language='javascript'>");
        Response.Write("window.print()");
        Response.Write("</script>");
    }

    public void PrintIntervention(int aid, int rlst,string riskName)
    {
        try
        {
            Assessment ass = new Assessment();
            ass = ass.GetAssessmentByOID(aid);

            ResultLetterDetail letterDetail = null;
            ResultLetter resultLetter = new ResultLetter();
            resultLetter = resultLetter.GetResultLetterByAOID(aid);

            Collection<Answer> answers = new Answer().GetAnswerByAOIDAndRLST_ForPrintResultLetter(aid, rlst);


            //Collection<Student> studentList = new Student().GetAllStudentByResultLetterSentTimes(rlst);
            Collection<Student> studentList = new Collection<Student>();// new Student().GetAllStudentByResultLetterSentTimes(rlst);
            foreach (Answer answer in answers)
            {
                //Student student = new Student();
                Student student = new Student().GetStudentByStudentOID(answer.BannerID.ToString());
                RiskCalculation riskCalculation = new RiskCalculation();
                if (student != null)
                {
                    riskCalculation = riskCalculation.GetRiskCalculationByAOIDAndSOIDAndRiskName(aid, student.StudentOID,riskName);
                    
                }

                // if (student.StudentOID == 0) break ;
                bool isContain = false;
                foreach (Student std in studentList)
                {
                    if (student.StudentOID == std.StudentOID)
                    {
                        isContain = true;
                    }
                }
                
                  if (!isContain && riskCalculation != null && student != null)
                  //if (!isContain && riskCalculation != null)
                 
                    {
                        answer.UpdateAnswer_ForNumberofPrinted(aid, rlst, Convert.ToInt32(student.StudentOID));
                        answer.UpdateScoreDetailsTable_ForNumberofPrinted(aid, rlst, Convert.ToInt32(student.StudentOID));
                        studentList.Add(student);
                       
                    }
                
            }


           // bool updateStatus = new Student().UpdateAllStudentByResultLetterSentTimes(rlst);

            List<ScoreDetailTable> scoreDetailTableList = new ScoreDetailTable().GetScoreDetailTableByAOID(ass.AssessmentOID);
            //List<ScoreDetailTable> scoreDetailTableList = new List<ScoreDetailTable>();
            StringBuilder sb = new StringBuilder();
            string dateStr = System.DateTime.Today.ToLongDateString();
            foreach (Student student in studentList)
            {
                //sb.Append("<table cellpadding='0' cellspacing='0'  style='page-break-after:always; width:90%; margin:0px 0px 0px 0px;' >");
                #region Header
                sb.Append("<table cellpadding='0' cellspacing='0'   style='width:100%; margin:10px 10px 5px 10px;font-size:12px; page-break-after:always; height:100%'>");
                
                sb.Append("<tr>");
                sb.Append("<td> " + dateStr + "</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td> <br/>" + student.FullName + "</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td> " + student.AddressOne + "</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td> ");

                sb.Append("<table cellpadding='0' cellspacing='0' style='width:100%;'>");
                sb.Append("<tr>");
                sb.Append("<td align='left' style='font-size:12px'>");

                if (!string.IsNullOrEmpty(student.City))
                {
                    sb.Append(student.City);
                }
                if (!string.IsNullOrEmpty(student.State))
                {
                    sb.Append(", " + student.State);
                }
                if (!string.IsNullOrEmpty(student.ZIPCode))
                {
                    sb.Append(", " + student.ZIPCode);
                }

                sb.Append("</td> ");
                sb.Append("<td align='right'> Student ID: " + student.StudentID + "</td>");
                sb.Append("<tr>");
                sb.Append("</table>");

                sb.Append("</td>");
                sb.Append("</tr>");
                #endregion


                #region StudentName
                string studentFirstName = student.FullName;
                if (!string.IsNullOrEmpty(studentFirstName))
                {
                    try
                    {
                        if (studentFirstName.Contains(','))
                        {
                            studentFirstName = studentFirstName.Split(',')[1];
                        }
                    }
                    catch
                    { }
                }
                #endregion

                sb.Append("<tr>");
                sb.Append("<td><br/>Dear " + studentFirstName + ":</td>");
                sb.Append("</tr>");
               // sb.Append("</br><div id='Header' style='height:150px;position: relative;vertical-align:top;border:1px solid red'>");
                if (resultLetter != null)
                {
                    
                    sb.Append("<tr>");
                    
                    sb.Append("<td style='font-size:12px;border:1px''>" + resultLetter.Header + "</td>");
                    sb.Append("</tr>");
                   
                }
               // sb.Append("</div>");
                sb.Append("<tr>");
                sb.Append("<td><br/>");
                sb.Append("<table cellpadding='0' cellspacing='0' style='width:98%;'>");
                sb.Append("<tr>");
                sb.Append("<td style='background:#d7d7d7; width:29%; border: thin solid #999; padding:8px;font-family:Calibri;font-size:10px;'><b>CATEGORY</b></td>");
                sb.Append("<td style='background:#d7d7d7; width:69%; border: thin solid #999; padding:8px;font-family:Calibri;font-size:10px;'><b>DEFINITION</b></td>");
                sb.Append("</tr>");

                //Collection<StudentRank> studentRankList_ = new StudentRank().GetStudentRankBySOIDandAOID(student.StudentOID, ass.AssessmentOID);

                #region SectionList
                foreach (Section s in ass.SectionList)
                {
                    if (s.SectionName == "NoScore") continue;
                    string definition = "", starValue = "";
                    if (resultLetter != null)
                    {
                        if (resultLetter.LetterDetail != null)
                        {
                            var tmp = from detail in resultLetter.LetterDetail
                                      where detail.SectionOID == s.SectionOID
                                      select detail;
                            letterDetail = tmp != null ? tmp.First() : null;
                        }
                    }
                #endregion
                    sb.Append("<tr>");
                    #region unused
                    //foreach (StudentRank SR in studentRankList)
                    //{
                    //    if (SR.SectionOID == s.SectionOID)
                    //    {
                    //        if (SR.Rank < s.Flag)
                    //        {
                    //            starValue = " *";
                    //            break;
                    //        }
                    //        else
                    //        { starValue = ""; }
                    //    }
                    //}
                    //scoreDetailTableList
                    #endregion

                    #region Star
                    foreach (ScoreDetailTable SR in scoreDetailTableList)
                    {
                        if ((SR.StudentOID == student.StudentOID) && (SR.SectionOID == s.SectionOID))
                        {
                            //if ((SR.Score*s.TotalQuestion) < s.Flag)
                            if ((SR.Rank) < s.Flag)
                            {
                                starValue = " *";
                                break;
                            }
                            else
                            { 
                                starValue = "";
                                break;
                            }
                        }
                    }
                    #endregion


                    sb.Append("<td style='width:29%; border: thin solid #999; padding:8px;font-family:Calibri;font-size:10px;'><b>" + s.SectionName + starValue + "</b></td>");
                    definition = (letterDetail != null) ? letterDetail.SectionDefinition : "";

                    sb.Append("<td style='width:69%; border: thin solid #999; padding:8px; text-align:left; font-family:Calibri;font-size:10px;'>" + definition + "</td>");
                    
                   
                }
               
                sb.Append("</table>");
                //sb.Append("<div id='Footer' style='height:150px;position: relative;vertical-align:top;border:1px solid red'>");
                if (resultLetter != null)
                {
                    sb.Append("<tr>");
                    sb.Append("<td style='valign:top;font-size:12px;border:1px''><br/>" + resultLetter.ShowAboveResult + "</td>");
                    sb.Append("</tr>");
                }

                //sb.Append("</div>");
                

                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</table>");
               
            }
            ////print_div1.InnerHtml = sb.ToString();
            Response.Write(sb.ToString());
            return;
        }
        catch (Exception ex)
        {
             
        }
    }

    public void UpdateIntervention(int aid, int rlst)
    {
        try
        {
            Assessment ass = new Assessment();
            ass = ass.GetAssessmentByOID(aid);

            ResultLetterDetail letterDetail = null;
            ResultLetter resultLetter = new ResultLetter();
            resultLetter = resultLetter.GetResultLetterByAOID(aid);

            Collection<Student> studentList = new Student().GetAllStudentByResultLetterSentTimes(rlst);
            
            
            
            StringBuilder sb = new StringBuilder();
            string dateStr = System.DateTime.Today.ToString();
            foreach (Student student in studentList)
            {
                sb.Append("<table cellpadding='0px' cellspacing='0px' style='width:100%; page-break-after: always; margin:0px 20px 0px 20px'>");

                sb.Append("<tr>");
                sb.Append("<td> " + dateStr + "</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td> " + student.FullName + "</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td> " + student.AddressOne + "</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td> " + student.AddressTwo + "</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td> " + student.AddressThree + "</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td align='right'> Student ID: " + student.StudentID + "</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td> Dear " + student.FirstName + ":</td>");
                sb.Append("</tr>");

                if (resultLetter != null)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>" + resultLetter.Header + "</td>");
                    sb.Append("</tr>");
                }

                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append("<table cellpadding='0px' cellspacing='0px' style='width:100%;'>");
                sb.Append("<tr>");
                sb.Append("<td style='background-color:Gray; width:30%; border: thin solid #000000;'>CATEGORY</td>");
                sb.Append("<td style='background-color:Gray; width:70%; border: thin solid #000000;'>DEFINITION</td>");
                sb.Append("</tr>");

                //Collection<StudentRank> studentRankList_ = new StudentRank().GetStudentRankBySOIDandAOID(student.StudentOID, ass.AssessmentOID);

                foreach (Section s in ass.SectionList)
                {
                    string definition = "", starValue = "";
                    if (resultLetter != null)
                    {
                        if (resultLetter.LetterDetail != null)
                        {
                            var tmp = from detail in resultLetter.LetterDetail
                                      where detail.SectionOID == s.SectionOID
                                      select detail;
                            letterDetail = tmp != null ? tmp.First() : null;
                        }
                    }

                    sb.Append("<tr>");

                    //foreach (StudentRank SR in studentRankList)
                    //{
                    //    if (SR.SectionOID == s.SectionOID)
                    //    {
                    //        if (SR.Rank < s.Flag)
                    //        {
                    //            starValue = " *";
                    //            break;
                    //        }
                    //        else
                    //        { starValue = ""; }
                    //    }
                    //}

                    sb.Append("<td style='width:30%; border: thin solid #000000'>" + s.SectionName + starValue + "</td>");
                    definition = (letterDetail != null) ? letterDetail.SectionDefinition : "";

                    sb.Append("<td style='width:70%; border: thin solid #000000'>" + definition + "</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</table>");

                if (resultLetter != null)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>" + resultLetter.ShowAboveResult + "</td>");
                    sb.Append("</tr>");
                }

                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
            }
            //print_div1.InnerHtml = sb.ToString();
            Response.Write(sb.ToString());
            return;
        }
        catch (Exception ex)
        {

        }
    }
}
