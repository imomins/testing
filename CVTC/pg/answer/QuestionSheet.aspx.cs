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
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Data.Odbc;

public partial class pg_answer_QuestionSheet : System.Web.UI.Page
{

    Collection<Question> AllQList = new Collection<Question>();

    string fieldName1 = null;

    string response =null;

    int answerPoint = 0;

    private int QuestionCounter = 1;

    AnswerDetail ansDetail = new AnswerDetail();
    Collection<AnswerDetail> ansDetailList = new Collection<AnswerDetail>();
    Answer ans = new Answer();
    Assessment ass = new Assessment();

    Collection<QuestionResponse> QResponselistPerOID = new Collection<QuestionResponse>();
    QuestionResponse qresponse = new QuestionResponse();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblAssessmentName.Text = "";
        Label1.Text = "";
        if (!Page.IsPostBack)
        {
            //int assOID = Convert.ToInt32(Request.QueryString["aid"].ToString());
            //int sectOID = Convert.ToInt32(Request.QueryString["sid"].ToString());
            int aid = (Session["aoid"] != null) ? Convert.ToInt32(Session["aoid"].ToString()) : 0;
            Assessment ass = new Assessment();
            ass = ass.GetAssessmentByOID(aid);
            if (ass!=null )
            {
                lblAssessmentName.Text = ass.AssessmentName;
            }
            StringBuilder sb = new StringBuilder();
            //Session["soid"] = sectOID;
            //Initialize(aid);
            Student student = (Student)(Session["currentStd"]);
            if (student != null)
            {
                lblStudentID.Text = student.StudentID;
                string strFullName = student.FullName;
                string[] strNames = null;
                if (strFullName.Contains(','))
                {
                    strNames = strFullName.Split(',');
                }
                else
                {
                    strNames = strFullName.Split(' ');
                }
                //lblFirstName.Text = strNames[0];
                lblFirstName.Text = student.FirstName;
                //lblLastName.Text = strNames[1];
                lblLastName.Text = student.LastName ;
                if (student.BirthDate  != null)
                {
                    lblBirthDate.Text = Convert.ToDateTime(student.BirthDate.ToString()).ToShortDateString();
                }
                lblProgram.Text = student.MajorProgramEnrollment ;

            }
            if (!IsExist( aid))
            {
              
                NonScoreTable(aid);
                ScoreTable(aid);
                
            }
            else
            {
                //ButtonSubmit.Visible = true  ;
                Response.Write("<b>You have already sit for this section</b>");
            }
            
        }

    }

 

    private bool IsExist(int AOID)
    {
        bool result = false;

        try
        {
            Student student = (Student)(Session["currentStd"]);
            if (student != null)
            {
                Answer ans = new Answer();
                result = ans.IsAnswerExist( AOID,student.StudentOID);
            }
            //result = true;
        }
        catch (Exception ex)
        { }
        return result;
    }
    
    private void InitializeQuestion(int soid)
    {
        try
        {
            Section s = new Section();
            s = s.GetSectionByOID(soid);
            Label lblSect;
            Label lineBreak = new Label();
            lineBreak.Text = "<br />";
            lblSect = new Label();

            //Section
            //lblSect.Text = s.SectionName;
           // PlaceHolderMain.Controls.Add(lblSect);

            PlaceHolderMain.Controls.Add(InsertLineBreaks(1));
            //Each Question
            foreach (Question q in s.QuestionList)
            {
                // PlaceHolderMain.Controls.Add(InsertLineBreaks(1));
                lblSect = new Label();
                lblSect.Text = q.OrderNo + ". " + q.QuestionText;
                PlaceHolderMain.Controls.Add(lblSect);

                if (q.RespAction == "Radio Button")
                {
                    //Panel p = new Panel();

                    RadioButtonList rblist = new RadioButtonList();
                    rblist.ID = q.QuestionOID.ToString();


                    //Each Question Response
                    foreach (QuestionResponse resp in q.QuestionRespList)
                    {
                        rblist.Items.Add(resp.Response);
                    }
                    //p.Controls.Add(rblist);
                    ///PlaceHolderMain.Controls.Add(InsertLineBreaks(1));
                    PlaceHolderMain.Controls.Add(rblist);
                }
                else if (q.RespAction == "Check Box")
                {
                    //Panel p = new Panel();
                    CheckBoxList chlist = new CheckBoxList();
                    chlist.ID = q.QuestionOID.ToString();

                    //Each Question Response
                    foreach (QuestionResponse resp in q.QuestionRespList)
                    {
                        chlist.Items.Add(resp.Response);
                    }
                    //p.Controls.Add(chlist);
                    //PlaceHolderMain.Controls.Add(InsertLineBreaks(1));
                    PlaceHolderMain.Controls.Add(chlist);
                }

                else if (q.RespAction == "Drop Down")
                {
                    //Panel p = new Panel();
                    DropDownList ddl = new DropDownList();
                    ddl.ID = q.QuestionOID.ToString();


                    //Each Question Response
                    foreach (QuestionResponse resp in q.QuestionRespList)
                    {
                        ddl.Items.Add(resp.Response);
                    }
                    //p.Controls.Add(ddl);
                    PlaceHolderMain.Controls.Add(ddl);
                }


            }
            PlaceHolderMain.Controls.Add(InsertLineBreaks(2));
        }
        catch (Exception ex)
        { }
    }

    private void Initialize(int AOID)
    {
        try
        {
            
            Assessment ass = new Assessment();
            ass = ass.GetAssessmentByOID(AOID);
            Label lblSect;
            Label lineBreak=new Label();
            lineBreak.Text = "<br />";
            //LabelAssessment.Text = "Exam for assessment " + ass.AssessmentName;

            foreach (Section s in ass.SectionList)
            {
                //lblSect = new Label();                
                //lblSect.Text = s.SectionName;
                //PlaceHolderMain.Controls.Add(lblSect);

                //PlaceHolderMain.Controls.Add(InsertLineBreaks(1));
                //Each Question
                foreach (Question q in s.QuestionList)
                {
                   // PlaceHolderMain.Controls.Add(InsertLineBreaks(1));
                    lblSect = new Label();
                    lblSect.Text = q.OrderNo+". "+q.QuestionText;
                    PlaceHolderMain.Controls.Add(lblSect);

                    if (q.RespAction == "Radio Button")
                    {
                        //Panel p = new Panel();
                        
                        RadioButtonList rblist = new RadioButtonList();
                        rblist.ID = q.QuestionOID.ToString();
                        

                        //Each Question Response
                        foreach (QuestionResponse resp in q.QuestionRespList)
                        {
                            rblist.Items.Add(resp.Response);
                        }
                        //p.Controls.Add(rblist);
                        ///PlaceHolderMain.Controls.Add(InsertLineBreaks(1));
                        PlaceHolderMain.Controls.Add(rblist);
                    }
                    else if (q.RespAction == "Check Box")
                    {
                        //Panel p = new Panel();
                        CheckBoxList chlist = new CheckBoxList();
                        chlist.ID = q.QuestionOID.ToString(); 

                        //Each Question Response
                        foreach (QuestionResponse resp in q.QuestionRespList)
                        {
                            chlist.Items.Add(resp.Response);
                        }
                        //p.Controls.Add(chlist);
                        //PlaceHolderMain.Controls.Add(InsertLineBreaks(1));
                        PlaceHolderMain.Controls.Add(chlist);
                    }

                    else if (q.RespAction == "Drop Down")
                    {
                        //Panel p = new Panel();
                        DropDownList ddl = new DropDownList();
                        ddl.ID = q.QuestionOID.ToString(); 
                        

                        //Each Question Response
                        foreach (QuestionResponse resp in q.QuestionRespList)
                        {
                            ddl.Items.Add(resp.Response);
                        }
                        //p.Controls.Add(ddl);
                        PlaceHolderMain.Controls.Add(ddl);
                    }

                    PlaceHolderMain.Controls.Add(InsertLineBreaks(1));
                }
                //PlaceHolderMain.Controls.Add(InsertLineBreaks(1));

            }
        }
        catch (Exception ex)
        { }
    }

    private static Label InsertLineBreaks(int breaks)
    {
        Label lblLineBreak = new Label();

        for (int i = 0; i<breaks; i++)
        {
            lblLineBreak.Text += "<br/>";
        }
        return lblLineBreak;
    }

    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //CheckValueNonScore();
            //CheckValueForScore();

            bool ChNonScore = CheckValueNonScore();
            if (!ChNonScore)
            {
                Alert.Show("Please Answer All  Non Score Questions");
               // Label1.Text = "<b>Check All Non Score</b>";
                return ;
            }

            bool ChScore = CheckValueForScore();
            if (!ChScore)
               {
                  Alert.Show("Please Answer All Score  Questions");
                  // Label1.Text = "<b>Check All Score Question</b>";
                   return ;
               }
          
            else 
            {
            int answerOID = GetValueNonScore();
            GetValueForScore(answerOID);
            
            Student student = (Student)(Session["currentStd"]);
            int aoid = Convert.ToInt32(Session["aoid"].ToString());
            
            try
            {
                if (student != null)
                {
                    RiskCalculation riskCalculation = new RiskCalculation().GetRiskCalculationByAOIDAndSOID(aoid, student.StudentOID);
                    if (riskCalculation != null)
                    {
                        Interventions intervention = new Interventions().GetInterventionByRiskOID(riskCalculation.RiskOID);
                        if (intervention != null)
                        {
                            intervention.StudentOID = student.StudentOID;
                            intervention.AddInterventions();
                        }
                    }
                }
            }
            catch (Exception ex) 
            { }

            try
            {
                PrintIntervention(aoid, student.StudentID,student .EmailAddress);
                UpdateTestingDate(lblStudentID.Text);
                //Response.Redirect("FinishExam.aspx?aoid=" + aoid + "");
                Response.Redirect("../../FinishExam.aspx");
            }
            catch (Exception ex)
            {
                lblValidation.Text = ex.ToString();
            }
           // Response.Redirect("FinishExam.aspx?aoid=" + aoid + "&bannerid='" + lblStudentID.Text + "'");
            
            }
        }
        catch (Exception )
        { 
        
        }


    }


    private void PrintIntervention(int aid, string studentID,string StuEmail)
    {
        try
        {
            Assessment ass = new Assessment();
            ass = ass.GetAssessmentByOID(aid);
            ResultEmail resultEmail = new ResultEmail();
            resultEmail = resultEmail.GetResultEmailByAOID(aid);

            if (resultEmail == null)
                return;

            Student student = new Student().GetStudentByStudentOID(studentID);

            StringBuilder sb = new StringBuilder();
            sb.Append("<table cellpadding='0px' cellspacing='0px' style='width:100%; margin:0px 30px 0px 30px'>");

            sb.Append("<tr>");
            sb.Append("<td align='center'  style='font-size:24px; font-weight:bold;'> " + ass.AssessmentName + " Feedback Report</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>CVTC applicant: <b>" + student.FullName + "</b> Program Interest: <b>" + student.MajorProgramEnrollment + "</b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td > <br />" + resultEmail.Header + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td ><br /><b>*Your individual Inventory of Student Success assessment results suggest . . .</b><br /></td>");
            sb.Append("</tr>");

            StudentRank studentRank = new StudentRank();
            Collection<StudentRank> studentRankList = studentRank.GetStudentRankBySOIDandAOID(student.StudentOID, ass.AssessmentOID);

            int i = 0;
            string txtSign = "", txtComments = "";
            sb.Append("<td>");
            sb.Append("<table cellpadding='0px' cellspacing='0px' style='width:80%;'>");
            foreach (StudentRank SR in studentRankList)
            {
                txtComments = SR.Comment;
                foreach (Section s in ass.SectionList)
                {
                    if (SR.SectionOID == s.SectionOID)
                    {
                        if (SR.Rank >= s.Medium)
                        {
                            txtSign = "+";
                        }
                        else if ((SR.Rank < s.Medium) && (SR.Rank >= s.Low))
                        {
                            txtSign = "~";
                        }
                        else if ((SR.Rank < s.Low))
                        {
                            txtSign = "-";
                        }
                        //else
                        //{
                        //    txtSign = "-";
                        //    //txtComments = "";
                        //}
                        break;
                    }
                }

                if (i % 2 == 0)
                {
                    sb.Append("<tr>");
                    sb.Append("<td align='right' valign='middle' style='width:2%'><b>" + txtSign + "</b></td>");
                    sb.Append("<td style='width:48%; border: thin solid #000000; padding:10px;'> " + txtComments + " </td>");
                    //sb.Append("<td style='width:48%; border: thin solid #000000; padding:10px;'> " + SR.Rank + " </td>");
                }
                else
                {
                    sb.Append("<td align='right'  valign='middle' style='width:2%'><b>" + txtSign + "</b></td>");
                    sb.Append("<td style='width:48%; border: thin solid #000000; padding:10px;'> " + txtComments + " </td>");
                    //sb.Append("<td style='width:48%; border: thin solid #000000; padding:10px;'> " + SR.Rank + " </td>");
                    sb.Append("</tr>");
                }
                i++;
                txtSign = "";
                txtComments = "";
            }

            if (studentRankList.Count % 2 == 0)
            {
                sb.Append("<tr>");
                sb.Append("<td colspan='4'><br />*( +above average ~average -below average )</td>");
                sb.Append("</tr>");
            }
            else
            {
                sb.Append("<td colspan='2'><br /> *( +above average ~average -below average )</td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            sb.Append("</td>");
            sb.Append("<tr>");
            sb.Append("<td > <br />" + resultEmail.ShowAboveResult + "</td>");
            sb.Append("</tr>");
            sb.Append("</table>");

            string StudentEmail=null;
            if (ConfigurationManager.AppSettings["productionMode"].ToString() == "OFF")
            {
                StudentEmail = System.Web.Configuration.WebConfigurationManager.AppSettings["asseeementEmail"].ToString();
            }
            else if (ConfigurationManager.AppSettings["productionMode"].ToString() == "ON")
            {
                StudentEmail = StuEmail;
                if (StudentEmail == null || StudentEmail == "")
                {
                    StudentEmail = System.Web.Configuration.WebConfigurationManager.AppSettings["asseeementEmail"].ToString();
                }
            }
           
            this.SendMail(StudentEmail, "Assesment feedback report ", sb.ToString());
            //this.SendMail("mom.ruet@gmail.com", "Assesment feedback report ", sb.ToString());
            
        }
        catch (Exception ex)
        { }
    }

    private bool SendMail_(string toAddress, string subject, string body)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("hitauthority@gmail.com");
            //mail.From = new MailAddress("studentperformance@cvtc.edu");
            mail.To.Add(toAddress);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Credentials = new System.Net.NetworkCredential("hitauthority", "hit@authority");
            smtpClient.EnableSsl = bool.Parse("True");
            smtpClient.Port = Int32.Parse("587");
            smtpClient.Send(mail);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private bool SendMail(string toAddress, string subject, string body)
    {
        #region Dummy
        //try
        //{
            

        //    MailMessage mail = new MailMessage();
        //    mail.From = new MailAddress("hitauthority@gmail.com");
        //    mail.To.Add(toAddress);
        //    mail.Subject = subject;
        //    mail.Body = body;
        //    mail.IsBodyHtml = true;
        //    SmtpClient smtpClient = new SmtpClient();
        //    smtpClient.Host = "smtp.gmail.com";
        //    smtpClient.Credentials = new System.Net.NetworkCredential
        //         ("hitauthority", "hit@authority");
        //    smtpClient.EnableSsl = bool.Parse("True");
        //    smtpClient.Port = Int32.Parse("587");
        //    smtpClient.Send(mail);
        //    return true;
        //}
        //catch
        //{
        //    return false;
        //}

        #endregion
        try
        {
            string fromEmail = ConfigurationManager.AppSettings["fromEmail"].ToString();
            string displayName = ConfigurationManager.AppSettings["displayName"].ToString();
            string smtpServer = ConfigurationManager.AppSettings["smtpServer"].ToString();
            using (var message = new MailMessage())
            {
                message.From = new MailAddress(fromEmail, displayName);
                message.To.Add(toAddress);
                message.Subject = subject;
                message.IsBodyHtml = true ;
                message.Body = body;
                var client = new SmtpClient(smtpServer);
                client.Send(message);
            }
            return true;
        }
        catch (Exception ex)
        {
            
            return false;
        }
    }


    //Save Value For NonScore Section
    private int GetValueNonScore()
    {

        try
        {
            int answerOID = 0;

            Student student = (Student)(Session["currentStd"]);
            int aoid = Convert.ToInt32(Session["aoid"].ToString());
           
            //Get Assessment By OID
            ass = ass.GetAssessmentByOID_QuestionSheet(aoid);
            //Assign Value to Answer
            ans = ans.GetAnswerBySOIDAndAOID(student.StudentOID, aoid);
            if (ans == null)
            {
                ans = new Answer();
                ans.CreatedDate = DateTime.Now;
                ans.AssessmentOID = ass.AssessmentOID;
                ans.CreatedBy = 1;
                ans.NumberOfPrinted = 0;
                ans.BannerID = student.StudentID;
                ans.StudentOID = student.StudentOID;//Get Currently Login Student OID
                ansDetailList.Clear();
                foreach (Section s in ass.SectionList)
                {
                    if (s.SectionName == "NoScore")
                    {
                        //to do for section
                        //Each Question

                        foreach (Question q in s.QuestionList)
                        {
                            QResponselistPerOID.Clear();
                            QResponselistPerOID = qresponse.GetQuestionRespByQOID(q.QuestionOID);

                            ansDetail = new AnswerDetail();
                            ansDetail.CreatedBy = 1;
                            ansDetail.SectionOID = s.SectionOID;
                            ansDetail.QuestionOID = q.QuestionOID;

                            response = null;
                            foreach (QuestionResponse qr in QResponselistPerOID)
                            {

                                fieldName1 = Convert.ToString("A" + q.QuestionOID + qr.QuestionResponseOID);
                                fieldName1 = Request.Form[fieldName1];
                                if (fieldName1 != null)
                                {
                                    response = qr.Response;
                                }
                                //else
                                //{
                                //    response = "No Answer";
                                //}

                            }
                          
                            ansDetail.Response = response;
                            ansDetailList.Add(ansDetail);
                        }
                    }
                }

                //Assign Answer Details List to 
                ans.AnswerDetailList = ansDetailList;

                //Save
                if (ans.AnswerOID > 0)
                {
                    ans.addAnswerDetails(ans.AnswerOID);
                    ansDetailList.Clear();
                }
                else
                {
                    answerOID = ans.AddAnswer();
                    ansDetailList.Clear();
                }

            }
            return answerOID;
        }
        catch
        {
            return 0;
        }
    }

    private void ChangebgColor(string id)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type='text/javascript'>");
        sb.Append(" function changebgColor('"+id+"')");
        sb.Append("document.getElementById('" + id + "').style.backgroundColor = 'blue';");
        sb.Append("</script>");
        divNonScoreTable.InnerHtml = sb.ToString();
    }

    //Check Value For NonScore
   private bool CheckValueNonScore()
    //private void  CheckValueNonScore()
    {
        

        try
        {
            bool ResultNonScore = true;

            Student student = (Student)(Session["currentStd"]);
            int aoid = Convert.ToInt32(Session["aoid"].ToString());
            Assessment asst = new Assessment();
            int ansOID = asst.GetAnswerOIDByAssessmentOID(aoid);
            //Get Assessment By OID
            ass = ass.GetAssessmentByOID(aoid);
            //Assign Value to Answer
            ans = ans.GetAnswerBySOIDAndAOID(student.StudentOID, aoid);
            if (ans == null)
            {
                ans = new Answer();
                ans.AssessmentOID = ass.AssessmentOID;
                //ans.CreatedBy = 1;
                //ans.NumberOfPrinted = 0;
                //ans.BannerID = student.StudentID;
                //ans.StudentOID = student.StudentOID;//Get Currently Login Student OID
                foreach (Section s in ass.SectionList)
                {
                    if (s.SectionName == "NoScore")
                    {
                        //to do for section
                        //Each Question

                        foreach (Question q in s.QuestionList)
                        {
                            QResponselistPerOID.Clear();
                            QResponselistPerOID = qresponse.GetQuestionRespByQOID(q.QuestionOID);

                            ansDetail = new AnswerDetail();
                            //ansDetail.CreatedBy = 1;
                            //ansDetail.SectionOID = s.SectionOID;
                            //ansDetail.QuestionOID = q.QuestionOID;

                            response = null;
                            foreach (QuestionResponse qr in QResponselistPerOID)
                            {

                                fieldName1 = Convert.ToString("A" + q.QuestionOID + qr.QuestionResponseOID);
                                fieldName1 = Request.Form[fieldName1];
                                if (fieldName1 != null)
                                {
                                    response = qr.Response;
                                }


                            }
                            if (response == null)
                            {
                             ResultNonScore = false;
                             return ResultNonScore;

                                //Alert.Show("Please Answer All NonScore  Questions");
                                //break ;
                            }

                            //ansDetail.Response = response;
                            //ansDetailList.Add(ansDetail);
                        }
                    }
                }


            }
            return ResultNonScore;
        }
        catch(Exception ax)
        {
            return false;
        }
    }

   //Save Value For Score Section
    private void   GetValueForScore( int answerOID)
    {
        bool res = false;
        try
        {
            string fieldName1 = null;
            string fieldName2 = null;
            string fieldName3 = null;
            string fieldName4 = null;
            string fieldName5 = null;
            string fieldName6 = null;

            string fieldName11 = null;
            string fieldName22 = null;
            string fieldName33 = null;
            string fieldName44 = null;
            string fieldName55 = null;
            string fieldName66 = null;

            Student student = (Student)(Session["currentStd"]);
            int aoid = Convert.ToInt32(Session["aoid"].ToString());
          

            //Get Assessment By OID
            ass = ass.GetAssessmentByOID(aoid);
            //Assign Value to Answer
            ans = ans.GetAnswerByStudentOIDAndAOID(student.StudentOID, aoid);
          // if (ans == null)
            if (ans != null)
            {
                ans = new Answer();
                ans.AssessmentOID = ass.AssessmentOID;
                ans.CreatedBy = 1;
                ans.NumberOfPrinted = 0;
                ans.BannerID = student.StudentID;
                ans.StudentOID = student.StudentOID;//Get Currently Login Student OID
                foreach (Section s in ass.SectionList)
                {
                    if (s.SectionName != "NoScore")
                    {
                        //to do for section
                        //Each Question

                        foreach (Question q in s.QuestionList)
                        {
                            ansDetail = new AnswerDetail();
                            ansDetail.CreatedBy = 1;
                            ansDetail.SectionOID = s.SectionOID;
                            ansDetail.QuestionOID = q.QuestionOID;

                          
                            fieldName11 = Convert.ToString("A1" + q.QuestionOID);
                            fieldName1 = Request.Form[fieldName11];
                            fieldName22 = Convert.ToString("A2" + q.QuestionOID);
                            fieldName2 = Request.Form[fieldName22];
                            fieldName33 = Convert.ToString("A3" + q.QuestionOID);
                            fieldName3 = Request.Form[fieldName33];
                            fieldName44 = Convert.ToString("A4" + q.QuestionOID);
                            fieldName4 = Request.Form[fieldName44];
                            fieldName55 = Convert.ToString("A5" + q.QuestionOID);
                            fieldName5 = Request.Form[fieldName55];
                            fieldName66 = Convert.ToString("A6" + q.QuestionOID);
                            fieldName6 = Request.Form[fieldName66];
                           
                            if (fieldName1 == "on")
                            {
                                response = "Not true at all";

                                answerPoint = q.Reverse==0? 1:6;
                                //res = true;
                            }

                            else if (fieldName2 == "on")
                            {
                                response = "Somewhat Untrue";
                                answerPoint = q.Reverse == 0 ? 2 : 5;
                                //res = true;
                            }
                            else if (fieldName3 == "on")
                            {
                                response = "Slightly Untrue ";
                                answerPoint = q.Reverse == 0 ? 3 : 4;
                                //res = true;
                            }
                            else if (fieldName4 == "on")
                            {
                                response = "Slightly True ";
                                answerPoint = q.Reverse == 0 ? 4 : 3;
                                //res = true;
                            }
                            else if (fieldName5 == "on")
                            {
                                response = "Somewhat True ";
                                answerPoint = q.Reverse == 0 ? 5 : 2;
                                //res = true;
                            }
                            else if (fieldName6 == "on")
                            {
                                response = "Completely True";
                                answerPoint = q.Reverse == 0 ? 6 : 1;
                                //res = true ;
                            }
                            //else
                            //{
                            //    response = "No Answer";
                            //    answerPoint = 0;
                            //    //res = true ;
                            //}
                            QuestionResponse  Qres=new QuestionResponse ();

                            //int answerPoint=Qres.GetQuestionFlagPointByQOIDAndResponse(q..QuestionOID ,response);
                            ansDetail.AnswerPoint = answerPoint;
                            ansDetail.Response = response;
                            ansDetailList.Add(ansDetail);
                        }
                    }
                }


                //Assign Answer Details List to 
                ans.AnswerDetailList = ansDetailList;

                //Save
                if (answerOID > 0)
                {
                    ans.addAnswerDetails(answerOID);
                }
                //else
                //{
                //    ans.AddAnswer();
                //}
            }
        }
        catch
        { }
        //return res;
    }

    //Check Value For Score
    private bool CheckValueForScore()
      {
        bool res = true;
        try
        {
            #region Variable
            string fieldName1 = null;
            string fieldName2 = null;
            string fieldName3 = null;
            string fieldName4 = null;
            string fieldName5 = null;
            string fieldName6 = null;

            string fieldName11 = null;
            string fieldName22 = null;
            string fieldName33 = null;
            string fieldName44 = null;
            string fieldName55 = null;
            string fieldName66 = null;
            #endregion
            Student student = (Student)(Session["currentStd"]);
            int aoid = Convert.ToInt32(Session["aoid"].ToString());
            ass = ass.GetAssessmentByOID(aoid);
            ans = ans.GetAnswerBySOIDAndAOID(student.StudentOID, aoid);
            if (ans == null)
            {
                ans = new Answer();
                foreach (Section s in ass.SectionList)
                {
                    if (s.SectionName != "NoScore")
                    {
                      
                        foreach (Question q in s.QuestionList)
                        {
                            #region Initialization
                            ansDetail = new AnswerDetail();
                            fieldName11 = Convert.ToString("A1" + q.QuestionOID);
                            fieldName1 = Request.Form[fieldName11];
                            fieldName22 = Convert.ToString("A2" + q.QuestionOID);
                            fieldName2 = Request.Form[fieldName22];
                            fieldName33 = Convert.ToString("A3" + q.QuestionOID);
                            fieldName3 = Request.Form[fieldName33];
                            fieldName44 = Convert.ToString("A4" + q.QuestionOID);
                            fieldName4 = Request.Form[fieldName44];
                            fieldName55 = Convert.ToString("A5" + q.QuestionOID);
                            fieldName5 = Request.Form[fieldName55];
                            fieldName66 = Convert.ToString("A6" + q.QuestionOID);
                            fieldName6 = Request.Form[fieldName66];
                            #endregion

                            #region Check
                            if (fieldName1 == "on")
                            {
                              res = true;
                            }

                            else if (fieldName2 == "on")
                            {
                               res = true;
                            }
                            else if (fieldName3 == "on")
                            {
                               res = true;
                            }
                            else if (fieldName4 == "on")
                            {
                               res = true;
                            }
                            else if (fieldName5 == "on")
                            {
                               res = true;
                            }
                            else if (fieldName6 == "on")
                            {
                               res = true;
                            }
                            else
                            {

                                res = false;
                                return res;

                            }

                            #endregion
                        }
                    }
                }
               
            }
        }
        catch
        { }
        return res;
    }


    private string GetCharByInterger(int i)
    {        
        return Char.ConvertFromUtf32(i+97);
    }

    private void SendLetterEmail()
    { 
        //Send Letter Email
    }

    // Randomize Scored Qustion

    private int InitWithRandomize(int LowerLimit, int UpperLimit)
    {
        int result = 0;
        try
        {
            if (LowerLimit != UpperLimit)
            {
            
            }
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    private void ScoreTable(int AOID)
    {
        StringBuilder sb = new StringBuilder();
        Assessment ass = new Assessment();
        # region Dummy JS
        //// Working For No Score
        //sb.Append("<script type='text/javascript'>");
        //sb.Append(" function clearAllRadios(radioList, count,position){ if(count == '') count = 7; if(position==0) position = 'Radio'; else if(position==1) position = 'RadioButonLeft'; else position = 'RadioButonRight'; radioList = radioList.toString(); ");
        //sb.Append(" var mySplitLength = radioList.length; ");
        //sb.Append(" var SplitResult = radioList.substring(0,mySplitLength-1); ");
        //sb.Append(" var lastDigit = radioList.substring(mySplitLength-1,mySplitLength); ");
        //sb.Append(" var firstDigit = radioList.substring(0,1); ");
        //sb.Append("i = parseInt(radioList); j= i ;");
        //sb.Append("var k = count - lastDigit;");
        //sb.Append("var i = parseInt(SplitResult) + '1';");
        //sb.Append(" alert('i='+i);alert('j+k='+(j+k));");
        //sb.Append(" for(i=i; i< j+k; i++){");
        //sb.Append("if( i != parseInt(radioList)) { ");
        //sb.Append(" var test111 = position + i;");
        //sb.Append(" alert(test111);");
        //sb.Append("document.getElementById(test111).checked = false;");
        //// End of IF
        //sb.Append("}");
        //// End of For
        //sb.Append("}");
        //// End of Function
        //sb.Append("}");

        //sb.Append("</script>"); 
        /// End of Working For No Score

       // //Working For Both
       //          sb.Append("<script type='text/javascript'>");
       //          sb.Append(" function clearAllRadios(radioList, count,position){ if(count == '') count = 7; if(position==0) position = 'Radio'; else if(position==1) position = 'RadioButonLeft'; else position = 'RadioButonRight'; radioList = radioList.toString(); ");
       //          sb.Append(" var mySplitLength = radioList.length; ");
       //          sb.Append(" var SplitResult = radioList.substring(0,mySplitLength-1); ");
       //          sb.Append(" var lastDigit = radioList.substring(mySplitLength-1,mySplitLength); ");
       //          sb.Append(" var firstDigit = radioList.substring(0,1); ");
       //          sb.Append("i = parseInt(radioList); j= i ;");
       //          sb.Append(" if(position =='Radio')  k = (count - lastDigit)-1; else k = count - lastDigit;");
                 
       //          sb.Append("var i = parseInt(SplitResult) + '1';");
       //          //sb.Append(" alert('i='+i);alert('j+k='+(j+k));");
       //          sb.Append(" for(i=i; i<= j+k; i++){");
       //          sb.Append("if( i != parseInt(radioList)) { ");
       //          sb.Append(" var test111 = position + i;");
       //          //sb.Append(" alert(test111);");
       //          sb.Append("document.getElementById(test111).checked = false;");
       // // End of IF
       //          sb.Append("}");
       // // End of For
       //          sb.Append("}");
       // // End of Function
       //          sb.Append("}");

       //          sb.Append("</script>");
        ////End of Working For Both
        #endregion


        #region Active JS
        //Working For Both
        sb.Append("<script type='text/javascript'>");
        sb.Append(" function clearAllRadios(radioList, count,position){ if(count == '') count = 7; if(position==0) position = 'Radio'; else if(position==1) position = 'RadioButonLeft'; else position = 'RadioButonRight'; radioList = radioList.toString(); ");
        sb.Append(" var mySplitLength = radioList.length; ");
        sb.Append(" var SplitResult = radioList.substring(0,mySplitLength-1); ");
        sb.Append(" var lastDigit = radioList.substring(mySplitLength-1,mySplitLength); ");
        sb.Append(" var firstDigit = radioList.substring(0,1); ");
        sb.Append("i = parseInt(radioList); j= i ;");
        sb.Append(" if(position =='Radio')  k = (count - lastDigit)-1; else k = count - lastDigit;");

        sb.Append("var i = parseInt(SplitResult) + '1';");
        //sb.Append(" alert('i='+i);alert('j+k='+(j+k));");
        sb.Append(" for(i=i; i<= j+k; i++){");
        sb.Append("if( i != parseInt(radioList)) { ");
        sb.Append(" var test111 = position + i;");
        //sb.Append(" alert(test111);");
        sb.Append("document.getElementById(test111).checked = false;");
        // End of IF
        sb.Append("}");
        // End of For
        sb.Append("}");
        // End of Function
        sb.Append("}");

        sb.Append("</script>");
        //End of Working For Both
        #endregion


        sb.Append(" <table width ='100%' class='answer_table'cellpadding='0' cellspacing='0' >");

              //First tr
              sb.Append("  <tr> <td width='80%' style='padding:5px;'>        For the remaining questions, choose one response for each statement that            indicates your level of agreement or disagreement with the statement. Measuring         attitudes is hard to do, so asking the same questions again in different ways is         necessary to reduce error. Please be patient and answer each item as naturally         as you can without trying to recall previous responses. Bear in mind that there         are no 'right' or 'wrong' answers, simply provide the answer that best fits you.         For questions on study habits and teachers, reference mainly your pre-college experiences.</td>");

              sb.Append("<td valign='bottom' width='3%' align='center' style='padding-left:5px;><div  class ='answer11' > <img src='images/Untitled1.png' alt='Not true at all'/> </div> </td>");
              sb.Append("<td valign='bottom' width='3%' align='center' style='padding-left:5px;> <div  class ='answer11' > <img src='images/Untitled2.png' alt='Somewhat Untrue'/> </div></td>");
              sb.Append("<td valign='bottom' width='3%' align='center' style='padding-left:5px;> <div  class ='answer11' > <img src='images/Untitled3.png' alt='Slightly Untrue'/> </div></td>");
              sb.Append("<td valign='bottom' width='3%' align='center' style='padding-left:5px;> <div  class ='answer11' > <img src='images/Untitled4.png' alt='Slightly True'/> </div></td>");
              sb.Append("<td valign='bottom' width='3%' align='center' style='padding-left:5px;> <div  class ='answer11' >  <img src='images/Untitled5.png' alt='Somewhat True'/> </div> </td>");
              sb.Append("<td valign='bottom' width='3%' align='center' style='padding-left:5px;> <div  class ='answer11' >  <img src='images/Untitled6.png' alt='Completely True'/> </div> </td>");
              sb.Append("</tr>");
              //End first tr
            if (AOID != 0)
            {
                ass = ass.GetAssessmentByOID(AOID);
                Section sec ;
                int count = 0; 
                for (int sn = 0; sn < ass.SectionList.Count; sn++)
                {
                   
                  
                    sec = ass.SectionList[sn];
                    
                if (sec.SectionName != "NoScore")
                {
                    foreach (Question q in sec.QuestionList)
                    {

                        count++;
                        //Second tr
                        sb.Append("<tr>");

                        //Question td
                          #region Question td
                        if (q.QuestionOID % 2 == 0)
                        {

                            sb.Append("<td width='80%' style='padding:10px;' align='right' class='qustion_title_bg'>");
                           
                        }
                        else
                        {
                            sb.Append("<td width='80%' style='padding:10px;' align='right' class='qustion_title_bg_alt'>");
                          
                        }

                        sb.Append("<span id='span" + q.QuestionOID + "'>" + q.QuestionText + "</span> <br />");
                        sb.Append("</td>");
                        #endregion
                        //End Question td


                        //Responses td
                        sb.Append("<td colspan='6' width='20%'>");
                       // sb.Append("<div >");
                        sb.Append("<table class='Groupnonscorequestion_" + QuestionCounter + "' cellpadding='0' cellspacing='0'width='100%' >");
                        sb.Append("<tr>");
                       
                        #region Responses

                        sb.Append("<td  align='center' width='3%' class ='answer1' >");
                        sb.Append("<label><input id='Radio" + q.QuestionOID + 1 + "' class='nonscorequestions_" + QuestionCounter + "'  title='Not true at all' name='A1" + q.QuestionOID + "' onclick=' clearAllRadios(" + q.QuestionOID + 1 + ", 7,0)' type='radio' enableviewstate='true'runat='server'/></label>");
                        sb.Append("</td>");


                        sb.Append("<td align='center' width='3%' class ='answer1'>");
                        sb.Append("<label><input id='Radio" + q.QuestionOID + 2 + "' class='nonscorequestions_" + QuestionCounter + "' title='Somewhat Untrue' name='A2" + q.QuestionOID + "' onclick=' clearAllRadios(" + q.QuestionOID + 2 + ", 7,0)' type='radio' enableviewstate='true'runat='server'/></label>");
                        sb.Append("</td>");


                        sb.Append("<td align='center' width='3%' class ='answer1'>");
                        sb.Append("<label><input id='Radio" + q.QuestionOID + 3 + "'  class='nonscorequestions_" + QuestionCounter + "' title='Slightly Untrue' name='A3" + q.QuestionOID + "' onclick=' clearAllRadios(" + q.QuestionOID + 3 + ", 7,0)' type='radio' enableviewstate='true'runat='server'/></label>");
                        sb.Append("</td>");


                        sb.Append("<td align='center' width='3%' class ='answer1' >");
                        sb.Append("<label><input id='Radio" + q.QuestionOID + 4 + "'  class='nonscorequestions_" + QuestionCounter + "' title='Slightly True' name='A4" + q.QuestionOID + "' type='radio' onclick=' clearAllRadios(" + q.QuestionOID + 4 + ", 7,0)' enableviewstate='true'runat='server'/></label>");
                        sb.Append("</td>");


                        sb.Append("<td align='center' width='3%' class ='answer1'>");
                        sb.Append("<label><input id='Radio" + q.QuestionOID + 5 + "' class='nonscorequestions_" + QuestionCounter + "' title='Somewhat True' name='A5" + q.QuestionOID + "' onclick=' clearAllRadios(" + q.QuestionOID + 5 + ", 7,0)' type='radio' enableviewstate='true'runat='server'/></label>");
                        sb.Append("</td>");


                        sb.Append("<td align='center' width='3%' class ='answer1'>");
                        sb.Append("<label><input id='Radio" + q.QuestionOID + 6 + "'  class='nonscorequestions_" + QuestionCounter + "' title='Completely True' name='A6" + q.QuestionOID + "' onclick=' clearAllRadios(" + q.QuestionOID + 6 + ", 7,0)' type='radio' enableviewstate='true'runat='server'/></label>");
                        sb.Append("</td>");

                        #endregion
                       
                        sb.Append("</tr>");
                        sb.Append("</table>");
                        //sb.Append("</div>");
                        sb.Append("</td>");
                        //End Responses td
                        sb.Append("</tr>");
                        
                        //End Second tr
                    //}
                        QuestionCounter++;
                }
                }
            }
            sb.Append(" </table>");
            divNonScoreTable.InnerHtml = sb.ToString();
        }
    }
        
    private void NonScoreTable(int AOID)
    {
        StringBuilder sb = new StringBuilder();
    
        Assessment ass = new Assessment();
        if (AOID != 0)
        {
            ass = ass.GetAssessmentByOID_QuestionSheet(AOID);
            Section sec;
            for (int sn = 0; sn < ass.SectionList.Count; sn++)
            {
                sec = ass.SectionList[sn];

                if (sec.SectionName == "NoScore")
                {
                   // AllQList.Clear();
                    Collection<Question> QList = new Collection<Question>();
                    QList = sec.QuestionList;
               
                for (int i = 0; i < QList.Count; i++)
                {

                    Question qu = QList[i];
                    {
                        AllQList.Add(qu);
                    }
                }

                sb.Append(" <table width ='100%' >");
                

                sb.Append("<tr>");
                sb.Append("<td width='100%' valign='top'>");
                sb.Append(" <table width ='100%' >");
                sb.Append("<tr>");


                int countLoop = 0;
                int modCounter = 0;
                foreach (Question q in AllQList)
                {
                    sb.Append(" <td class='Groupnonscorequestion_" + QuestionCounter + "'>");
                    sb.Append("<div  class='qustion_title_bg' id='div" + q.QuestionOID + "'>" + q.QuestionText + "</div> <br />");
                    //sb.Append("<div id ='" + q.QuestionOID + "'>" + q.QuestionRespList.Count + "</div> <br />");
                    sb.Append("<input type= 'hidden' id ='" + q.QuestionOID + "' value= '" + q.QuestionRespList.Count + "' />");
                    countLoop = 0;
                    modCounter++;
                    
                    foreach (QuestionResponse resp in q.QuestionRespList)
                    {
                        countLoop = countLoop + 1;
                        sb.Append("<label><input class='nonscorequestions_" + QuestionCounter +"' id='Radio" + q.QuestionOID + countLoop + "' value =" + resp.Response + " name='A" + q.QuestionOID + resp.QuestionResponseOID + "' onclick=' clearAllRadios(" + q.QuestionOID + countLoop + ", " + q.QuestionRespList.Count + ",0)' type='radio' runat='server' enableviewstate='true'/>" + resp.Response + "</label><br />");
                    }
                    sb.Append("</td>");
                    if (modCounter % 2 == 0)
                    {
                        sb.Append("</tr>");
                        sb.Append("<tr>");

                    }
                    QuestionCounter++;

                }
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append(" </table>");
                sb.Append(" </td>");
                sb.Append("</tr>");

                sb.Append(" </table>");
                  
            }
            }
            divSoreTable.InnerHtml = sb.ToString();

        }



    }

    private bool  UpdateTestingDate(string bannerID)
    {
        string  connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();
        bool res = false;
        DateTime latestTestingDate = DateTime.Now;
        try
        {
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                using (OdbcCommand command = new OdbcCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "{CALL Student_UpdateTestingDate(?,?)}";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@BannerID", bannerID);
                    command.Parameters.AddWithValue("@LatestTestingDate", latestTestingDate);
                    connection.Open();
                    int n = command.ExecuteNonQuery();
                    if (n == 1)
                    {
                        res = true;
                    }
                    else
                    {
                        res = false;
                    }

                }
            }


        }
        catch (Exception ax)
        {

        }
        return res;
    }
}
