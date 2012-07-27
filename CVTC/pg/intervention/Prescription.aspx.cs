using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections.ObjectModel;
using System.Net.Mail;
using System.Configuration;
using System.Data.Odbc;
using System.Data;

public partial class pg_intervention_Prescription : System.Web.UI.Page
{
    private string studentid = null;
    string connectionString = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();
        string POID = "";
        string SOID = "";
        string riskOID="";
        if (!Page.IsPostBack)
        {
            //studentid = Session["StudentID"].ToString ();
            try
            {
                
                POID = Request.QueryString["poid"];
                riskOID = Request.QueryString["riskOID"];
                //string POID = "4";
                ViewState["POID"] = POID;
                if (POID == null || POID == "")
                {
                    HyperLink1.Visible = false;
                }
                else
                {
                    HyperLink1.Visible = true;
                }
                
                SOID = Request.QueryString["studentID"];
                ViewState["soid"] = SOID;
                if (POID == null || POID == "")
                {
                    PopulateDomain();
                    PopulateAssessmentForNewDomain();

                }
                else
                {
                    if (!CheckUser(Convert.ToInt32(POID)))
                    {
                        InsertUserIntoPrescription(Convert.ToInt32(POID));
                    }
                    updateReadUnread(Convert.ToInt32(POID));
                }
                Initialize(Convert.ToInt32(POID));
               // PrintIntervention(Convert.ToInt32(POID));
                
                //Student st = new Student();
                //string strBanerID = st.GetStudentIDByStudentBannerOID(Convert.ToInt32(SOID));
                //if (SOID != null)
                //{
                //    PopulateAssessment(SOID);

                //}
                //else
                //{
                    
                    //PopulateAssessment(SOID);
               // }
                //Interventions inter = new Interventions();
                //inter = inter.GetInterventionByOID(Convert .ToInt16 ( POID));
                //DropDownListDomain.Text=inter .DomainName;
                //DropDownListIntervention.Text = inter.InterventionName;

                LabelStatus.Text = "";
                

                
                            
            }


            catch (Exception ex)
            { }
           
            
        }
        string link = "PrintIntervention.aspx?poid=" + POID + "&soid=" + SOID + "&riskOID=" + riskOID + "";
        HyperLink1.NavigateUrl = link;
    }

    private bool CheckUser(int poid)
    {
        bool returnType = false;
        int UserOID=0;
        User user = new User();
        user = Session["CurrentUser"] as User;
        if (user != null)
        {
            UserOID = user.UserOID;
        }
        try
        {
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                using (OdbcCommand command = new OdbcCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "{CALL PrescriptionStatus_Check(?,?)}";
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.AddWithValue("@UserOID", UserOID);
                    command.Parameters.AddWithValue("@PrescriptionOID", poid);
                    connection.Open();

                    using (OdbcDataReader dataReader = command.ExecuteReader())
                    {


                        if (dataReader.Read())
                        {
                            returnType = true;   
                        }
                    }

                }
            }


        }
        catch (Exception ax)
        {

        }        
        return returnType;
    }

    private void InsertUserIntoPrescription(int poid)
    {
        User user = new User();
        user = Session["CurrentUser"] as User;
        if (user != null)
        {
                try
                {
                    using (OdbcConnection connection = new OdbcConnection(connectionString))
                    {
                        using (OdbcCommand command = new OdbcCommand())
                        {
                            command.Connection = connection;
                            command.CommandText = "{CALL PrescriptionStatus_insert(?,?)}";
                            command.CommandType = CommandType.StoredProcedure;



                            command.Parameters.AddWithValue("@UserOID", user.UserOID);
                            command.Parameters.AddWithValue("@PrescriptionOID", poid);

                            connection.Open();
                            command.ExecuteNonQuery();
                            
                        }
                    }


                }
                catch (Exception ax)
                {

                }

            }
    
    }


    private bool updateReadUnread(int poid)
    {
        
        bool res = false;
        DateTime latestTestingDate = DateTime.Now;
        try
        {
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                using (OdbcCommand command = new OdbcCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "{CALL Prescription_Update_Status(?)}";
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.AddWithValue("@poid", poid);
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

    private void Initialize( int POID)
    {
        try
        {

            Interventions inter = new Interventions();
            inter=inter.GetInterventionByOID(POID);

            if (inter != null)
            {
                TextBoxContactNotes.Text = inter.Comment;
                
                if (inter.Urgent == 1) CheckBoxListLeft.Items[0].Selected = true;
                if (inter.Internal == 1) CheckBoxListLeft.Items[1].Selected = true;
                if (inter.Prescribed == 1) CheckBoxListLeft.Items[2].Selected = true;
                if (inter.Participating == 1) CheckBoxListLeft.Items[3].Selected = true;
                if (inter.Completed == 1) CheckBoxListLeft.Items[4].Selected = true;
                if (inter.Email == 1) CheckBoxListRight.Items[0].Selected = true;
                if (inter.Telephone == 1) CheckBoxListRight.Items[1].Selected = true;
                if (inter.InPerson == 1) CheckBoxListRight.Items[2].Selected = true;
                if (inter.HandOff == 1) CheckBoxListRight.Items[3].Selected = true;
                if (inter.Testing == 1)  CheckBoxTesting.Checked = true;
                //DropDownListDomain.Text = inter.DomainName;
                 //DropDownListDomain.DataValueField = inter.DomainName;
                 DropDownListDomain.Items.Add(inter.DomainName);
                 Assessment assment = new Assessment();
                 assment = assment.GetAssessmentByOID(inter.AssessmentOID);
                 if (assment != null)
                 {
                     ddlAssessment.Items.Add(assment.AssessmentName);
                 }
                //DropDownListDomain.selectedte = inter.DomainName;
                //DropDownListIntervention.Text = inter.InterventionName;
                 //DropDownListIntervention.DataValueField = inter.InterventionName;
                 DropDownListIntervention.Items.Add(inter.InterventionName);
                //DropDownListIntervention.SelectedItem = inter.InterventionName;
                //DropDownListAdvocate.SelectedItem = inter.UserName;
                DropDownListAdvocate.Text = inter.UserName;
                TextBoxLatestAction.Value = inter.LatestActionDate;
                TextBoxLatestContact.Value = inter.LatestContact;
                ViewState["studentID"] = inter.StudentOID;
            }
        }
        catch(Exception ex)
        {
        
        }
    }

    private void PrintIntervention(int POID)
    {
        try
        {
            Interventions intervention = new Interventions();
            intervention = intervention.GetInterventionByOID(POID);

            if (intervention != null)
            {
                
                Student stdnt = new Student();
                stdnt = stdnt.GetStudentByOID(intervention.StudentOID);
                if (stdnt != null)
                {
                    string html ="";

                    //Create Html
                    html += "<table width='100%' ><tr><td><img alt='Logo' src='../../images/chippewavtc_logo.jpg' /></td><td style='text-align:center;'><span style='font-size:medium;font-weight:bold;'> Steps to Success: CVTC's commitment to helping YOU succeed!</span><br/><span style='text-align:center;'> Inventory of Student Success: Personalized Success Plan </span></td></tr></table>";
                    html += "<table width='100%'><tr><td style='width:45%;'>Success Steps For:<br /><b>First Name :</b>" + stdnt.FirstName + "  <b>Last Name :</b> " + stdnt.LastName + "</td><td> <div style='background-color: #C0C0C0;float:left;width:20%;'>Program&nbsp;&nbsp;</div><div style='padding-left:10px;border:solid 1px black;width:75%; float:left;'>" + stdnt.MajorProgramEnrollment + "</div></td></tr></table>";
                    // html += "<table width='100%'><tr><td>Success Steps For:<br />First: Abdul Last: Karim</td><td>                                <div style=' background-color: #C0C0C0;float:left;width:20%; '>Program&nbsp;&nbsp;</div><div style='padding-left:10px;border:solid 1px black;width:78%; float:left;'>Information Technology </div></td></tr></table>";
                    html += "<table width='100%'><tr><td style='border:solid 1px black;'>Inventory of Student Success results provide comparsions of your attitudes and confidence with other<br />    entering CVTC students in key focus areas that could affect your academic performance and program<br /> persistance:     </td></tr></table>";



                   //Collection<Interventions> interventions = intervention.GetInterventionByStudentOID(intervention.StudentOID);
                   Collection<Interventions> interventions = intervention.GetInterventionByAssOID(intervention.StudentOID, ddlAssessment.Text);

                    foreach (Interventions inter in interventions)
                    {
                        //Start
                        html += "<table style='width: 100%;'>";
                        //Get Sectionwise Student Rank 
                        StudentRank studentRank = new StudentRank();
                       // Collection<StudentRank> _list = studentRank.GetStudentRankByOID(stdnt.StudentOID);
                        int AssessmentOID = 0;
                        Assessment ass = new Assessment();
                        AssessmentOID = ass.GetAssessmentOIDByAssessmentName(ddlAssessment.Text);
                        Collection<StudentRank> _list = studentRank.GetStudentRankBySOIDandAOID(intervention.StudentOID, AssessmentOID);
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
                        }
                        html += "</table>";

                        //End                        
 
                    }

                    html += "<table style='width: 100%; border:solid 1px black;'>    <tr  >    <td>COMPASS test scores measure your academic readiness for college coursework in four broad areas:</td>    </tr>        </table>";

                    html += " <table style='width: 60%; '>    <tr><td style='background-color:#727272;width:200px;'>COMPASS Pre-Algebra</td><td style='width:100px; border:solid 1px black;'>" + stdnt.PrealgebraTestScore + "</td><td style='background-color:#727272;width:200px;'>COMPASS Writing</td><td style='width:100px; border:solid 1px black;'>" + stdnt.CompassWrittingTestScore + "</td></tr>   <tr><td style='background-color:#727272;width:200px;'>COMPASS Algebra</td><td style='width:100px; border:solid 1px black;'>" + stdnt.CompassalgebraTestScore + "</td><td style='background-color:#727272;width:200px;'>COMPASS Reading</td><td style='width:100px; border:solid 1px black;'>" + stdnt.CompassReadingTestScore + "</td></tr> </table>";
                    html += "<br/>";

                    int counter = 0;
                    Collection<Interventions> interventionsList = intervention.GetInterventionByStudentOID(intervention.StudentOID);
                    foreach (Interventions inter in interventionsList)
                    {
                        counter++;
                        html += "<table style='width: 100%; '>";
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

                        html += "</td></tr>";
                        html += "</table>";
                        html += "<br /><br />";


                        //End


                    }//foreach

                    html += "<script type =\"text/javascript\">";
                    html += "printDiv();";
                    html += "</script>";
                    //Response.Write(html.ToString());
                    print_div1.InnerHtml = html;
                                        
                  
                }//std
            }



        }
        catch (Exception ex)
        { }
    }

    private void PopulateAssessment(string  BannerID)
    {
        ddlAssessment.Items.Clear();
        Assessment ass = new Assessment();
        Collection<Assessment> assList = new Collection<Assessment>();
        assList = ass.GetAssessmentBySOID(BannerID);
        foreach (Assessment assment in assList)
        {
            ddlAssessment.Items.Add(assment .AssessmentName );

        }
    }


    private void PopulateAssessmentForNewDomain()
    {
        ddlAssessment.Items.Clear();
        Assessment ass = new Assessment();
        Collection<Assessment> assList = new Collection<Assessment>();
        assList = ass.GetAllAssessmnet();
        foreach (Assessment assment in assList)
        {
            ddlAssessment.Items.Add(assment.AssessmentName);

        }
    }

    private void PopulateDomain_(string BannerID)
    {
        ddlAssessment.Items.Clear();
        Assessment ass = new Assessment();
        Collection<Assessment> assList = new Collection<Assessment>();
        assList = ass.GetAssessmentBySOID(BannerID);
        //assList = ass.GetAllAssessmnet();
        foreach (Assessment assment in assList)
        {
            ddlAssessment.Items.Add(assment.AssessmentName);

        }
    }

    private void PopulateDomain()
    {
        DropDownListDomain.Items.Clear();
        Domain dom = new Domain();
        Collection<Domain> domainList = new Collection<Domain>();
        domainList = dom.GetAllDomain();
        foreach (Domain dom1 in domainList)
        {
            DropDownListDomain.Items.Add(dom1.DomainName);

        }
    }

    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if ((string.IsNullOrEmpty(TextBoxLatestAction.Value)) || (string.IsNullOrEmpty(TextBoxLatestContact.Value)))
            {
                LabelStatus.Text = "Please Enter Action and Contact Date";
                return;
            }
          
            Interventions interventions = new Interventions();
          

            //Changed by Babul 20111103
          //int  strInterventionOID = interventions.GetInterventionOIDByInterventionName(DropDownListIntervention.SelectedItem.ToString());
            int strInterventionOID = 0;
            if (DropDownListIntervention.SelectedIndex == -1)
            {
                strInterventionOID = 1;
            }
            else
            {
                strInterventionOID = interventions.GetInterventionOIDByInterventionName(DropDownListIntervention.SelectedItem.ToString());
            }
            //
          
           interventions.InterventionOID = strInterventionOID;
           Domain domain=new Domain ();
           int DomainIOD = domain.GetDomainOIDByDomainName(DropDownListDomain.SelectedItem.ToString());
           interventions.DomainOID = DomainIOD;
           User user = new User();
           int UserIOD = user.GetUserOIDByUserName(DropDownListAdvocate.SelectedItem.ToString());
           interventions.UserOID = UserIOD;

           if (ViewState["POID"] == null)
           {
               studentid = Session["StudentID"].ToString();
               Student student = new Student();
               int StudentOID = student.GetStudentOIDByStudentBannerID(studentid);
               interventions.StudentOID = StudentOID;
               ViewState["studentID"]=studentid;
           }
           else
           {
               interventions.StudentOID = Convert.ToInt32(ViewState["studentID"]);
           }
           //interventions.LatestActionDate = TextBoxLatestAction.Text.ToString();
           //interventions.LatestContact = TextBoxLatestContact.Text.ToString();
           interventions.LatestActionDate = TextBoxLatestAction.Value.ToString();
           interventions.LatestContact = TextBoxLatestContact.Value.ToString();
           interventions.CreatedBy = (Session["CurrentUser"] != null) ? ((User)Session["CurrentUser"]).UserOID : 0;
           if (CheckBoxListLeft.Items[0].Selected)
           {
               interventions.Urgent = 1;

           }
           else
           {
               interventions.Urgent = 0;
           }
           if (CheckBoxListLeft.Items[1].Selected)
           {
               interventions.Internal = 1;
           }
           else
           {
               interventions.Internal = 0;   
           }

           if (CheckBoxListLeft.Items[2].Selected)
           {
               interventions.Prescribed = 1;
           }
           else
           {
               interventions.Prescribed = 0;
           }

           if (CheckBoxListLeft.Items[3].Selected)
           {
               interventions.Participating = 1; 
           }
           else
           {
               interventions.Participating = 0;
           }
           if (CheckBoxListLeft.Items[4].Selected)
           {
               interventions.Completed = 1;
           }
           else
           {
               interventions.Completed = 0;
           }


           if (CheckBoxListRight.Items[0].Selected)
           {
               interventions.Email = 1;
           }
           else
           {
               interventions.Email = 0;
           }
           if (CheckBoxListRight.Items[1].Selected)
           {
               interventions.Telephone = 1;
           }
           else
           {
               interventions.Telephone = 0;
           }
           if (CheckBoxListRight.Items[2].Selected)
           {
               interventions.InPerson = 1;
           }
           else
           {
               interventions.InPerson = 0;
           }
           if (CheckBoxListRight.Items[3].Selected)
           {
               interventions.HandOff = 1;
           }
           else
           {
               interventions.HandOff = 0;
           }
           if (CheckBoxTesting.Checked == true)
           {
               interventions.Testing = 1;
           }
           else
           {
               interventions.Testing = 0;
           }
           interventions.Comment = TextBoxContactNotes.Text.Trim();
           interventions.RiskOID = 0;
           Assessment assment=new Assessment ();
           interventions.AssessmentOID = assment.GetAssessmentOIDByAssessmentName(ddlAssessment.SelectedItem.Text);
           if (ViewState["POID"] == null)
           {
               if (interventions.AddInterventions() > 0)
               {
                   LabelStatus.Text = "Saved Successfully.";
               }
               else
               {
                   LabelStatus.Text = "Save Failed.";
               }
               ViewState["POID"] = interventions.PrescriptionOID;
               
           }
           else
           {
               interventions.PrescriptionOID=Convert.ToInt32(ViewState["POID"].ToString());
               if (interventions.UpdateInterventions())
               {
                   LabelStatus.Text = "Update Successfully.";
               }
               else
               {
                   LabelStatus.Text = "Update Failed.";
               }
           }
         
            //Sending Email For Hand Off
           try
           {
               if (interventions.HandOff == 1)
               {
                   string toEmail = null;
                   if (ConfigurationManager.AppSettings["productionMode"].ToString() == "OFF")
                   {
                       toEmail = ConfigurationManager.AppSettings["toEmail"].ToString();
                   }
                   else
                   {

                       //Get Student Email Address By ID
                       studentid = Session["StudentID"].ToString();
                       Student stu = new Student();
                       stu = stu.GetStudentByStudentOID(studentid);
                       if (stu != null)
                       {
                           if (stu.EmailAddress != null)
                           {
                               toEmail = stu.EmailAddress;
                           }

                       }

                       //toEmail = ConfigurationManager.AppSettings["toEmail"].ToString();
                   }

                   #region Handoff Body
                   string  html = null ;
                   html += "<table style='width: 100%; '>";
                   html += "<tr><td colspan='4'> <h3>Handoff Details of " + DropDownListIntervention.SelectedItem.ToString() + " Intervention</h3></td></tr>";
                   html += "<tr><td >&nbsp;</td><td style='background-color:#727272;width:100px;'> Assessment</td><td>" + ddlAssessment.Text + "</td><td></td></tr>";
                   html += "<tr><td >&nbsp;</td><td style='background-color:#727272;width:100px;'> Domain</td><td>" + DropDownListDomain.SelectedItem.ToString() + "</td><td></td></tr>";
                   html += "<tr><td >&nbsp;</td><td style='background-color:#727272;width:100px;'> Intervention</td><td>" + DropDownListIntervention.SelectedItem.ToString() + "</td><td></td></tr>";
                   html += "<tr><td >&nbsp;</td><td > &nbsp;</td><td style='background-color:#727272; width:150px;'>User Name  </td><td>" + DropDownListAdvocate.SelectedItem.ToString() + "</td></tr>";
                   html += "<tr><td >&nbsp;</td><td > &nbsp;</td><td style='background-color:#727272; width:100px;'>Action Date </td><td>" + TextBoxLatestAction.Value.ToString();
                   html += "&nbsp;&nbsp;Prescribed&nbsp;";
                   if (interventions.Prescribed == 1)
                   {
                       html += "<img alt='yes' src='../../images/tic-pic.png' />";
                   }
                   else
                   {
                       html += "<img alt='no' src='../../images/nonetic-pic.png' />";
                   }

                   html += "&nbsp;Participating&nbsp;";
                   if (interventions.Participating == 1)
                   {
                       html += "<img alt='yes' src='../../images/tic-pic.png' />";
                   }
                   else
                   {
                       html += "<img alt='no' src='../../images/nonetic-pic.png' />";
                   }

                   html += "&nbsp;Internal&nbsp;";
                   if (interventions.Internal == 1)
                   {
                       html += "<img alt='yes' src='../../images/tic-pic.png' />";
                   }
                   else
                   {
                       html += "<img alt='no' src='../../images/nonetic-pic.png' />";
                   }

                   html += "&nbsp;Urgent&nbsp;";
                   if (interventions.Urgent == 1)
                   {
                       html += "<img alt='yes' src='../../images/tic-pic.png' />";
                   }
                   else
                   {
                       html += "<img alt='no' src='../../images/nonetic-pic.png' />";
                   }

                   html += "&nbsp;Completed&nbsp;";
                   if (interventions.Completed == 1)
                   {
                       html += "<img alt='yes' src='../../images/tic-pic.png' />";
                   }
                   else
                   {
                       html += "<img alt='no' src='../../images/nonetic-pic.png' />";
                   }



                   html += "</td></tr>";
                   html += "<tr><td >&nbsp;</td><td > &nbsp;</td><td style='background-color:#727272; width:100px;'>Contact Date </td><td>" + TextBoxLatestContact.Value.ToString();
                   html += "&nbsp;&nbsp;Email&nbsp;";
                   if (interventions.Email == 1)
                   {
                       html += "<img alt='yes' src='../../images/tic-pic.png' />";
                   }
                   else
                   {
                       html += "<img alt='no' src='../../images/nonetic-pic.png' />";
                   }
                   html += "&nbsp;&nbsp;Telephone&nbsp;";
                   if (interventions.Telephone == 1)
                   {
                       html += "<img alt='yes' src='../../images/tic-pic.png' />";
                   }
                   else
                   {
                       html += "<img alt='no' src='../../images/nonetic-pic.png' />";
                   }
                   html += "&nbsp;&nbsp;In Person&nbsp;";
                   if (interventions.InPerson == 1)
                   {
                       html += "<img alt='yes' src='../../images/tic-pic.png' />";
                   }
                   else
                   {
                       html += "<img alt='no' src='../../images/nonetic-pic.png' />";
                   }
                   html += "&nbsp;&nbsp;Hand-Off&nbsp;";
                   if (interventions.HandOff == 1)
                   {
                       html += "<img alt='yes' src='../../images/tic-pic.png' />";
                   }
                   else
                   {
                       html += "<img alt='no' src='../../images/nonetic-pic.png' />";
                   }
                   html += "&nbsp;&nbsp;Testing&nbsp;";
                   if (interventions.Testing == 1)
                   {
                       html += "<img alt='yes' src='../../images/tic-pic.png' />";
                   }
                   else
                   {
                       html += "<img alt='no' src='../../images/nonetic-pic.png' />";
                   }

                   html += "</td></tr>";
                   html += "<tr><td >&nbsp;</td><td > &nbsp;</td><td style='background-color:#727272; width:100px;'>Comments: </td><td>" + TextBoxContactNotes.Text;
                   html += "</table>";
                   #endregion

                   if (SendMail(toEmail, "You have got a Handoff", html))
                   {
                       LabelStatus.Text = "Eamil  Sent  .. Successfully";
                   }
                   else
                   {
                       LabelStatus.Text = "Eamil Not Sent  .. Please contact with admin ";
                   }
               }
           }
           catch (Exception ae)
           {
               LabelStatus.Text = "Eamil Not Sent  .. Please contact with admin ";
           }
            //End Sending Mail For Hand Off
        }
            

        catch (Exception ex)
        { 
        
        }
    }

    #region Testing Email
    private bool SendMail_(string toAddress, string subject, string body)
    {
        try
        {
            MailMessage mail = new MailMessage();
            //mail.From = new MailAddress("hitauthority@gmail.com");
            mail.From = new MailAddress("hitauthority@gmail.com");
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
    #endregion


    private bool SendMail(string toAddress, string subject, string body)
    {
        string fromEmail = ConfigurationManager.AppSettings["fromEmail"].ToString();
        string smtpServer = ConfigurationManager.AppSettings["smtpServer"].ToString();
        string displayName = ConfigurationManager.AppSettings["displayName"].ToString();

        try
        {
            using (var message = new MailMessage())
            {
                message.From = new MailAddress(fromEmail, displayName);
                message.To.Add(toAddress);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                var client = new SmtpClient(smtpServer);
                client.Send(message);
            }
            return true;
        }
        catch (Exception ex)
        {
            LabelStatus.Text = "Eamil Not Sent  .. Please contact with admin ";
            return false;
        }

    }

    protected void DropDownListDomain_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListIntervention.Items.Clear();
        Interventions iv;
        if (DropDownListDomain.Text != null)
        {
            iv = new Interventions();
            Collection<Interventions> inList = new Collection<Interventions>();
            inList = iv.GetInterventionByDomainName(DropDownListDomain.Text);
            //DropDownListIntervention.Items.Clear();
            foreach (Interventions inter in inList)
            {
                DropDownListIntervention.Items.Add(inter.InterventionName);
            }
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        string strID = null;
        strID = ViewState["POID"].ToString();
        PrintIntervention(Convert.ToInt32(strID));
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        
        string strID = null;
        strID =Convert.ToString ( ViewState["POID"]);
        string SOID = ViewState["soid"].ToString (); 

        //Response.Redirect("PrintIntervention.aspx?poid=" + strID + "&soid=" + SOID + "?keepThis=true&TB_iframe=true&height=470&width=570" class="thickbox"");
        //Response.Redirect("<a class=\"thickbox\" href=\"PrintIntervention.aspx?poid=" + strID + "&soid=" + SOID + "&keepThis=true&TB_iframe=true&height=500&width=850\">Edit</a>");
        
       // <a class=\"thickbox\" href=\"PrintIntervention.aspx?poid=" + strID + "&soid=" + SOID + "&keepThis=true&TB_iframe=true&height=500&width=850\">Edit</a>
        //if (strID != "0" && strID !="")
        //{
        //    PrintIntervention(Convert.ToInt32(strID));
        //}
        //else
        //{
        //    PrintIntervention(0);
        //}
    }


}
