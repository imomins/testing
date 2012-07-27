using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections.ObjectModel;

public partial class pg_intervention_PrintIntervention : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int AssessmentOID = 0;
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["poid"] != null && Request.QueryString["poid"] != "")
            {
                string SOID = "";
                SOID = Request.QueryString["soid"];
                string riskoid = Request.QueryString["riskOID"];
                Interventions  inter=new Interventions ();
                AssessmentOID = inter.GetAssessmentOIDByPrescriptionOID(Request.QueryString["poid"]);
                if (SOID != null && SOID != null)
                {
                    //PopulateAssessment(SOID, AssessmentOID);
                    PopulateAssessment(AssessmentOID);
                }

                PrintIntervention(Convert.ToInt32(Request.QueryString["poid"]), Convert.ToInt32(riskoid), SOID, AssessmentOID);
                PrintWindow();
            }
        }

    }


    private void PrintWindow()
    {
        Response.Write("<script language='javascript'>");
        Response.Write("window.print()");
        Response.Write("</script>");
    }

   // private void PopulateAssessment(string BannerID,int AOID)
    private void PopulateAssessment(int AOID)
    {
        ddlAssessment.Items.Clear();
        Assessment ass = new Assessment();
        ass.GetAssessmentByAssessmentOID(AOID);
        if (ass != null)
        {
            ddlAssessment.Items.Add(ass.AssessmentName);
        }
        //Collection<Assessment> assList = new Collection<Assessment>();
        //assList = ass.GetAssessmentBySOID(BannerID);
        //foreach (Assessment assment in assList)
        //{
        //    ddlAssessment.Items.Add(assment.AssessmentName);

        //}
    }



    private void PrintIntervention(int POID, int RiskOID, string BannerID,int AOID)
    {
        try
        {
            Interventions intervention = new Interventions();
            intervention = intervention.GetInterventionByOID(POID);

            if (intervention != null)
            {
                 Student stdnt = new Student();
                
                int StdOID = stdnt.GetStudentOIDByStudentBannerID(Request.QueryString["soid"]);
                stdnt = stdnt.GetStudentByOID(StdOID);
               
                if (stdnt != null)
                {
                    string html = "";
                    Assessment ass = new Assessment();

                    //Create Html
                    html += "<table width='100%' ><tr><td><img alt='Logo' src='../../images/chippewavtc_logo.jpg' /></td><td style='text-align:center;'><span style='font-size:medium;font-weight:bold;'> Steps to Success: CVTC's commitment to helping YOU succeed!</span><br/><span style='text-align:center;'> Inventory of Student Success: Personalized Success Plan </span></td></tr></table>";
                    html += "<table width='100%'><tr><td style='width:45%;'>Success Steps For:<br /><b>First Name :</b>" + stdnt.FirstName + "  <b>Last Name :</b> " + stdnt.LastName + "</td><td> <div style='background-color: #C0C0C0;float:left;width:20%;'>Program&nbsp;&nbsp;</div><div style='padding-left:10px;border:solid 1px black;width:75%; float:left;'>" + stdnt.MajorProgramEnrollment + "</div></td></tr></table>";
                    //ass.GetAssessmentByAssessmentOID(AOID);
                    //if (ass != null)
                    //{
                    //html += "<table width='100%'><tr><td style='width:45%;'><b>Assessment Name :  </b><b>" + ass.AssessmentName + "</b></td><td> </td></tr></table>";
                    //}
                    
                    html += "<table width='100%'><tr><td style='border:solid 1px black;'>Inventory of Student Success results provide comparsions of your attitudes and confidence with other<br />    entering CVTC students in key focus areas that could affect your academic performance and program<br /> persistance:     </td></tr></table>";



                    //Collection<Interventions> interventions = intervention.GetInterventionByStudentOID(intervention.StudentOID);

                    Collection<Interventions> interventions = new Collection<Interventions>();
                    interventions = intervention.GetInterventionByAssOID(StdOID, RiskOID,AOID);

                     foreach (Interventions inter in interventions)
                     {
                         //Start
                         html += "<table style='width: 100%;'>";
                         //Get Sectionwise Student Rank 
                         StudentRank studentRank = new StudentRank();
                         // Collection<StudentRank> _list = studentRank.GetStudentRankByOID(stdnt.StudentOID);
                         int AssessmentOID = 0;
                         //Assessment ass = new Assessment();

                        // AssessmentOID = ass.GetAssessmentOIDByAssessmentName(ddlAssessment.SelectedItem.Text);

                        // Collection<Assessment> assList = new Collection<Assessment>();
                         //assList = ass.GetAssessmentBySOID(BannerID);
                         ass.GetAssessmentByAssessmentOID(AOID);
                         if (ass != null)
                         {
                             //foreach (Assessment assessment in assList)
                             //{
                             Collection<StudentRank> _list = studentRank.GetStudentRankBySOIDandAOID(StdOID, ass.AssessmentOID);
                             //if
                             //for (int i = 0; i < 5; i++)
                             foreach (StudentRank stRank in _list)
                             {

                                 html += "<tr>";
                                 html += "<td style='border:solid 1px black;background-color:#727272;width:200px;'>" + stRank.SectionName + "</td>";
                                 html += "<td style='border:solid 1px black;width:50px;text-align:center;'>" + stRank.Rank.ToString() + "</td>";
                                 html += "<td style='border:solid 1px black;'>" + stRank.Comment + "</td>";
                                 html += "</tr>";
                                 //break;
                             } break;
                             //}
                         }
                             html += "</table>";

                             //End                        

                         
                     }
                    html += "<table style='width: 100%; border:solid 1px black;'>    <tr  >    <td>COMPASS test scores measure your academic readiness for college coursework in four broad areas:</td>    </tr>        </table>";

                    html += " <table style='width: 60%; '>    <tr><td style='background-color:#727272;width:200px;'>COMPASS Pre-Algebra</td><td style='width:100px; border:solid 1px black;'>" + stdnt.PrealgebraTestScore + "</td><td style='background-color:#727272;width:200px;'>COMPASS Writing</td><td style='width:100px; border:solid 1px black;'>" + stdnt.CompassWrittingTestScore + "</td></tr>   <tr><td style='background-color:#727272;width:200px;'>COMPASS Algebra</td><td style='width:100px; border:solid 1px black;'>" + stdnt.CompassalgebraTestScore + "</td><td style='background-color:#727272;width:200px;'>COMPASS Reading</td><td style='width:100px; border:solid 1px black;'>" + stdnt.CompassReadingTestScore + "</td></tr> </table>";
                    html += "<br/>";

                    int counter = 0;
                    //Collection<Interventions> interventionsList = intervention.GetInterventionByStudentOID(StdOID,RiskOID);

                    foreach (Interventions inter in interventions)
                    {
                        counter++;
                        #region Printing Checkboxes

                        html += "<table style='width: 100%; '>";
                        html += "<tr><td style='width:60px;font-size:medium;font-weight:bold;'></td><td style='background-color:#727272;width:100px;'> Assessment Name  : </td><td>" + inter .AssessmentName   + "</td><td></td></tr>";
                        html += "<tr><td style='width:60px;font-size:medium;font-weight:bold;'>Step " + counter.ToString() + "</td><td style='background-color:#727272;width:100px;'> Plan Target</td><td>" + inter.DomainName + "</td><td></td></tr>";
                        html += "<tr><td >&nbsp;</td><td style='background-color:#727272;width:100px;'> Intervention</td><td>" + inter.InterventionName + "</td><td></td></tr>";
                        html += "<tr><td >&nbsp;</td><td > &nbsp;</td><td style='background-color:#727272; width:150px;'>CVTC Advocate </td><td>" + inter.UserName + "</td></tr>";
                        html += "<tr><td >&nbsp;</td><td > &nbsp;</td><td style='background-color:#727272; width:100px;'>Action Date </td><td>" + inter.LatestActionDate;
                       
                        html += "&nbsp;&nbsp;Prescribed&nbsp;";
                        if (inter.Prescribed == 1)
                        {
                            html += "<img alt='' src='../../images/tic-pic.png' />";
                        }
                        else
                        {
                            html += "<img alt='' src='../../images/nonetic-pic.png' />";
                        }

                        html += "&nbsp;Participating&nbsp;";
                        if (inter.Participating == 1)
                        {
                            html += "<img alt='' src='../../images/tic-pic.png' />";
                        }
                        else
                        {
                            html += "<img alt='' src='../../images/nonetic-pic.png' />";
                        }

                        html += "&nbsp;Internal&nbsp;";
                        if (inter.Internal == 1)
                        {
                            html += "<img alt='' src='../../images/tic-pic.png' />";
                        }
                        else
                        {
                            html += "<img alt='' src='../../images/nonetic-pic.png' />";
                        }

                        html += "&nbsp;Urgent&nbsp;";
                        if (inter.Urgent == 1)
                        {
                            html += "<img alt='' src='../../images/tic-pic.png' />";
                        }
                        else
                        {
                            html += "<img alt='' src='../../images/nonetic-pic.png' />";
                        }

                        html += "&nbsp;Completed&nbsp;";
                        if (inter.Completed == 1)
                        {
                            html += "<img alt='' src='../../images/tic-pic.png' />";
                        }
                        else
                        {
                            html += "<img alt='' src='../../images/nonetic-pic.png' />";
                        }



                        html += "</td></tr>";
                        html += "<tr><td >&nbsp;</td><td > &nbsp;</td><td style='background-color:#727272; width:100px;'>Contact Date </td><td>" + inter.LatestContact;
                        html += "&nbsp;&nbsp;Email&nbsp;";
                        if (inter.Email == 1)
                        {
                            html += "<img alt='' src='../../images/tic-pic.png' />";
                        }
                        else
                        {
                            html += "<img alt='' src='../../images/nonetic-pic.png' />";
                        }
                        html += "&nbsp;&nbsp;Telephone&nbsp;";
                        if (inter.Telephone == 1)
                        {
                            html += "<img alt='' src='../../images/tic-pic.png' />";
                        }
                        else
                        {
                            html += "<img alt='' src='../../images/nonetic-pic.png' />";
                        }
                        html += "&nbsp;&nbsp;In Person&nbsp;";
                        if (inter.InPerson == 1)
                        {
                            html += "<img alt='' src='../../images/tic-pic.png' />";
                        }
                        else
                        {
                            html += "<img alt='' src='../../images/nonetic-pic.png' />";
                        }
                        html += "&nbsp;&nbsp;Hand-Off&nbsp;";
                        if (inter.HandOff == 1)
                        {
                            html += "<img alt='' src='../../images/tic-pic.png' />";
                        }
                        else
                        {
                            html += "<img alt='' src='../../images/nonetic-pic.png' />";
                        }
                        html += "&nbsp;&nbsp;Testing&nbsp;";
                        if (inter.Testing == 1)
                        {
                            html += "<img alt='' src='../../images/tic-pic.png' />";
                        }
                        else
                        {
                            html += "<img alt='' src='../../images/nonetic-pic.png' />";
                        }
                        #endregion
                        html += "</td></tr>";
                        html += "<tr><td >&nbsp;</td><td > &nbsp;</td><td style='background-color:#727272; width:150px;'>Comments :  </td><td>" + inter.Comment  + "</td></tr>";
                        html += "</table>";
                        html += "<br /><br />";


                        //End


                    }//foreach

                    //html += "<script type =\"text/javascript\">";
                    //html += "printDiv();";
                    //html += "</script>";
                    Response.Write(html.ToString());
                    //print_div1.InnerHtml = html;


                }//std
            }



        }
        catch (Exception ex)
        { }
    }


}
