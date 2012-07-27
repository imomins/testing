using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;

public partial class pg_assessment_viewResult : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int aid = Convert.ToInt32(Request.QueryString["aid"].ToString());
            //Response.Write(aid);
            //Initialize(aid);
            //Initialized(1);
            Initialized(aid);
            
        }
    }

    private void Initialize(int aid)
    {
        try
        {
            StringBuilder html = new StringBuilder();
            StudentAnswer stdAns = new StudentAnswer();
            Assessment ass = new Assessment();
            
            Collection<StudentAnswer> stdAnsList=stdAns.GetStudentAnswerByAOID(aid);
            ass=ass.GetAssessmentByOID(aid);
            html.Append("<table border='0' cellspacing='0' cellpadding='0'><tr>    <td bgcolor='#000000'>");
            html.Append("<table bgcolor='#000000' cellspacing='1' cellpadding='0'  >");
            html.Append("<tr>");
            html.Append("<td bgcolor='#FFFFFF'>&nbsp;</td><td bgcolor='#FFFFFF'>&nbsp;</td>");
            foreach (Section s in ass.SectionList)
            {
                html.Append("<td bgcolor='#FFFFFF'>");
                html.Append(s.SectionName);
                html.Append("</td>");
            }
            html.Append("</tr>");

            //Second Row
            html.Append("<tr>");
            html.Append("<td bgcolor='#FFFFFF'>Student Name</td><td bgcolor='#FFFFFF'>Banner ID</td>");
            //
            foreach (Section s in ass.SectionList)
            {
                html.Append("<td bgcolor='#FFFFFF'>");
                html.Append("<table bgcolor='#000000' cellspacing='1' cellpadding='4'>");
                html.Append("<tr>");
                int i = 1;
                foreach (Question q in s.QuestionList)
                {
                    html.Append("<td bgcolor='#FFFFFF'>");
                    html.Append(i++.ToString());
                    html.Append("</td>");
                }
                html.Append("</tr>");
                html.Append("</table>");
                html.Append("</td>");
            }
            //
            html.Append("</tr>");

            //Third Row and onward
            var StdList = from stans in stdAnsList
                          group  stans.StudentName by
                          new {stans.StudentName,stans.BannerID} into g
                          select g;

            //for (int j = 0; j < StdList.Count; j++)
            int j = 0;
            foreach (var stName in StdList)
            {
                html.Append("<tr>");
                html.Append("<td bgcolor='#FFFFFF' >" + stName.Key.StudentName + "</td><td bgcolor='#FFFFFF'>" + stName.Key.BannerID + "</td>");
                //
                foreach (Section s in ass.SectionList)
                {
                    html.Append("<td bgcolor='#FFFFFF'>");
                    html.Append("<table bgcolor='#000000' cellspacing='1' cellpadding='4'>");
                    html.Append("<tr>");

                    foreach (Question q in s.QuestionList)
                    {
                        if (stdAnsList.Count == 0)
                        {
                            html.Append("<td bgcolor='#FFFFFF'>&nbsp;</td>");
                            continue;
                        }
                        stdAns = stdAnsList[j++];

                        if (stdAns.questionOID == q.QuestionOID && stdAns.SectionOID == s.SectionOID)
                        {
                            if (stdAns.IsRight == 1)
                            {
                                html.Append("<td bgcolor='#FFFFFF'>");
                            }
                            else
                            {
                                html.Append("<td bgcolor='#FF0000'>");
                            }
                            //html.Append(q.OrderNo);
                            html.Append(stdAns.Response);
                            html.Append("</td>");
                        }
                        else
                        {
                            j--;
                            html.Append("<td bgcolor='#FFFFFF'>&nbsp;</td>");
                        }

                    }
                    html.Append("</tr>");
                    html.Append("</table>");
                    html.Append("</td>");
                }
                //
                html.Append("</tr>");

            }
                //END third
                html.Append("</table>");
                html.Append(" </td></tr></table>");
            Response.Write(html);
            //html.Remove(0, html.Length);
            
            

        }
        catch (Exception ex)
        { }
    }

    private void Initialized(int aid)
    {
        try
        {
            string colsp = "";
            int i = 0;
            StringBuilder html = new StringBuilder();
            StudentAnswer stdAns = new StudentAnswer();
            Assessment ass = new Assessment();

            Collection<StudentAnswer> stdAnsList = stdAns.GetStudentAnswerByAOID(aid);
            ass = ass.GetAssessmentByOID(aid);

            html.Append("<table border='0' cellspacing='0' cellpadding='0'><tr>    <td bgcolor='#000000'>");
            html.Append("<table bgcolor='#000000' cellspacing='1' cellpadding='4'  >");
            
            //First Row
            html.Append("<tr>");
            html.Append("<td bgcolor='#FFFFFF' colspan='2' >&nbsp;</td>");
            foreach (Section s in ass.SectionList)
            {
                i = s.QuestionList.Count == 0 ? 1 : s.QuestionList.Count;
                colsp = "colspan='" + i+"'";
                html.Append("<td bgcolor='#CDCABB' " + colsp + " >");
                html.Append(s.SectionName);
                html.Append("</td>");
            }
            html.Append("</tr>");

            //Second Row
            html.Append("<tr>");
            html.Append("<td bgcolor='#CDCABB'>Student Name</td><td bgcolor='#CDCABB'>Banner ID</td>");
            //
            foreach (Section s in ass.SectionList)
            {                    
                i = 1;
                foreach (Question q in s.QuestionList)
                {
                    html.Append("<td bgcolor='#CDCABB'>");
                    html.Append(i++.ToString());
                    html.Append("</td>");
                }             
            }            
            html.Append("</tr>");
            
            //Third Row and onword
            var StdList = from stans in stdAnsList
                          group stans.StudentName by
                          new { stans.StudentName, stans.BannerID } into g
                          select g;

            //for (int j = 0; j < StdList.Count; j++)
            int j = 0;
            foreach (var stName in StdList)
            {
                html.Append("<tr>");
                html.Append("<td bgcolor='#FFFFFF' >" + stName.Key.StudentName + "</td><td bgcolor='#FFFFFF'>" + stName.Key.BannerID + "</td>");
                //
                foreach (Section s in ass.SectionList)
                {
                    //html.Append("<td bgcolor='#FFFFFF'>");
                    //html.Append("<table bgcolor='#000000' cellspacing='1' cellpadding='4'>");
                    //html.Append("<tr>");

                    foreach (Question q in s.QuestionList)
                    {
                        if (stdAnsList.Count == 0)
                        {
                            html.Append("<td bgcolor='#FFFFFF'>&nbsp;</td>");
                            continue;
                        }
                        //stdAns = stdAnsList[j++];
                        StudentAnswer stdAns1 = null;
                        
                         var t = from ans in stdAnsList
                                 where ans.questionOID == q.QuestionOID && ans.SectionOID == s.SectionOID && ans.BannerID == stName.Key.BannerID
                                select ans;
                         foreach (StudentAnswer m in t)
                         {
                             stdAns1 = m;
                             break;
                             
                         }


                         if (stdAns1 == null)
                         {
                             html.Append("<td bgcolor='#FFFFFF'>&nbsp;</td>");
                             continue;
                         }
                        
                          // StudentAnswer stdAns1 =stdAnsList.
                        if (stdAns1.questionOID == q.QuestionOID && stdAns1.SectionOID == s.SectionOID)
                        {
                            if (stdAns1.IsRight == 1)
                            {
                                html.Append("<td bgcolor='#FFFFFF'>");
                            }
                            else
                            {
                                html.Append("<td bgcolor='#FF0000'>");
                            }
                            //html.Append(q.OrderNo);
                            html.Append(stdAns1.Response);
                            html.Append("</td>");
                        }
                        else
                        {
                            j--;
                            html.Append("<td bgcolor='#FFFFFF'>&nbsp;</td>");
                        }

                    }
                    //html.Append("</tr>");
                    //html.Append("</table>");
                    //html.Append("</td>");
                }
                //
                html.Append("</tr>");

            }
            html.Append("</table>");
            html.Append(" </td></tr></table>");
            Response.Write(html);

        }
        catch (Exception ex)
        { }
    }
}
