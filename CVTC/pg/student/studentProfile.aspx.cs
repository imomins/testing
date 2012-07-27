using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data.Odbc;

public partial class pg_student_studentProfile : System.Web.UI.Page
{

    string connectionString = "";
    public string getID;
    int rowID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();
        if (!Page.IsPostBack)
        {
            string strID = null;
            if (Request.QueryString["id"] != null)
            {
                strID = Request.QueryString["id"].ToString();
                ViewState["STDOID"] = strID;
                Session["STUOID"] = strID;
            }
            populateDropdownList();
            Student stu = new Student();
            string strOID = stu.GetStudentIDByStudentBannerOID(Convert.ToInt32(strID));
            Populate(strOID);
            populateDropdownList();
            PopulateSection(ddlAssesment.Text, strOID);
            PopulateSectionNoScore(ddlAssesment.Text, strOID);
            PopulateCourseByStudentID();
            int AOID = 0;
            if (ddlAssesment.Text != null && ddlAssesment.Text != "")
            {
                AOID = new Sections().GetAssessmentOIDByAssessmentName(ddlAssesment.Text);
            }
            if (!string.IsNullOrEmpty(strID))
            {
                this.BindRiskCalculation(AOID, Convert.ToInt32(strID));
            }
            int RiskOID = 0;
            if (LabelRisk.Text != "")
            {
                RiskOID = new RiskCalculation().GetRiskOIDByAOIDAndName(AOID, LabelRisk.Text);
            }
            ViewState["ROID"] = RiskOID;
            HiddenRiskOID.Value = RiskOID.ToString();
            PopulateGridIntervention(RiskOID);
            rowID = stu.GetStudentRowIDByBannerID(lblID.Text.Trim());
            ViewState["RowID"] = rowID;



        }

    }

    protected void ButtonRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            Student stu = new Student();
            int strID = stu.GetStudentOIDByStudentBannerID(lblID.Text.ToString());
            //string strID = null;
            //strID = ViewState["STDOID"].ToString();
            populateDropdownList();
            string strOID = stu.GetStudentIDByStudentBannerOID(Convert.ToInt32(strID));
            Populate(strOID);
            PopulateSection(ddlAssesment.Text.ToString(), lblID.Text);
            PopulateSectionNoScore(ddlAssesment.Text.ToString(), lblID.Text);
            PopulateCourseByStudentID();
            PopulateGridIntervention(Convert.ToInt32(ViewState["ROID"]));
        }
        catch (Exception ex)
        { }
    }

    private void populateDropdownList()
    {
        Collection<Answer> answers = new Collection<Answer>();
        Answer ans = new Answer();
        answers = ans.GetAOIDByStudentID(lblID.Text);
        //if (answers == null)
        //{
        ddlAssesment.Items.Clear();
        //}
        Assessment As = new Assessment();
        foreach (Answer a in answers)
        {
            Collection<Assessment> assList = new Collection<Assessment>();
            assList = As.GetAssessmentByAssessmentOID(a.AssessmentOID);
            foreach (Assessment asmnt in assList)
            {
                ddlAssesment.Items.Add(asmnt.AssessmentName);
                ans = ans.CheckBannerID_Answer(lblID.Text, asmnt.AssessmentOID);
                if (ans.CreatedDate != null)
                {
                    lblDate.Text = ans.CreatedDate.ToShortDateString();
                }
            }

            //ddlAssesment.DataSource = As.GetAssessmentByAssessmentOID(a.AssessmentOID);

        }
        ddlAssesment.DataBind();


    }


    private void Populate(string strID)
    {


        Student stu = new Student();
        stu = stu.GetStudentByStudentOID(strID);
        lblStudentName.Text = stu.FullName;
        lblID.Text = stu.StudentID;

        //if (stu.LatestTestingDate != null)
        //{
        //    lblDate.Text = stu.LatestTestingDate.ToString();
        //}
        if (stu.BirthDate != null)
        {
            lblBirth.Text = Convert.ToDateTime(stu.BirthDate.ToString()).ToShortDateString();
        }
        Session["StudentID"] = lblID.Text.ToString();
        getID = lblID.Text;
        lblTelephone.Text = stu.HomeTelephoneNumber;
        if (stu.CumulativeGPA.ToString().Length == 3)
        {
            lblCumulativeGPA.Text = stu.CumulativeGPA.ToString() + "0";
        }
        else if (stu.CumulativeGPA.ToString().Length == 1)
        {
            lblCumulativeGPA.Text = stu.CumulativeGPA.ToString() + ".00";
        }
        else
        {
            lblCumulativeGPA.Text = stu.CumulativeGPA.ToString();
        }
        lblCVTCProgram.Text = stu.MajorProgramEnrollment;
        lblCreditAttempted.Text = stu.CreditAttempted.ToString();
        lblCreditEarned.Text = stu.CreditEarned.ToString();

        lblHSname.Text = stu.HighSchoolName;
        lblgraddate.Text = stu.HighSchoolGraduationDate.ToShortDateString();
        lblacte.Text = stu.EnglishAssessmentScore;
        lblactm.Text = stu.MathAssessmentScore;
        lblactr.Text = stu.ReadingAssessmentScore;
        lblacts.Text = stu.ScienceAssessmentScore;
        lblalgebra.Text = stu.CompassalgebraTestScore;
        lblprealgebra.Text = stu.PrealgebraTestScore;
        lblreading.Text = stu.CompassReadingTestScore;
        lblwritting.Text = stu.CompassWrittingTestScore;
        lblPreProgram.Text = stu.PreprogramIndicator;

        LabelMC.Text = "Multicultural";//(stu.MC == "0") ? "" : stu.MC;
        if (stu.MC != "0" && stu.MC != null)
        {
            LabelMC.Visible = true;
            LabelMC.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            LabelMC.Visible = false;
        }
        //else
        //{
        //    LabelMC.ForeColor = System.Drawing.Color.Black;
        //}
        LabelNTO.Text = "NTO";//(stu.NTO == "0") ? "" : stu.NTO;
        if (stu.NTO != "0" && stu.NTO != null)
        {
            LabelNTO.Visible = true;
            LabelNTO.ForeColor = System.Drawing.Color.Red;
        }

        else
        {
            LabelNTO.Visible = false;
        }
        //else
        //{
        //    LabelNTO.ForeColor = System.Drawing.Color.Black;
        //}
        LabelAllert.Text = "Alert";//(stu.ALLERT == "0") ? "" : stu.ALLERT;
        if (stu.ALLERT != "0" && stu.ALLERT != null)
        {
            LabelAllert.Visible = true;
            LabelAllert.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            LabelAllert.Visible = false;
        }
        //else
        //{
        //    LabelAllert.ForeColor = System.Drawing.Color.Black;
        //}
        LabelPell.Text = "PELL";//(stu.PELL == "0") ? "" : stu.PELL;
        if (stu.PELL != "0" && stu.PELL != null)
        {
            LabelPell.Visible = true;
            LabelPell.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            LabelPell.Visible = false;
        }
        //else
        //{
        //    LabelPell.ForeColor = System.Drawing.Color.Black;
        //}
        LabelRVP.Text = "River Falls";//(stu.RVP == "0") ? "" : stu.RVP;
        if (stu.RVP != "0" && stu.RVP != null)
        {
            LabelRVP.Visible = true;
            LabelRVP.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            LabelRVP.Visible = false;
        }
        //else
        //{
        //    LabelRVP.ForeColor = System.Drawing.Color.Black;
        //}

    }

    private void PopulateCourseByStudentID()
    {
        Course course = new Course();
        string id = lblID.Text.Trim();
        CourseGrid.DataSource = course.GetCourseByStudentOID(id);
        CourseGrid.DataBind();
    }


    private void PopulateSectionNoScore(string strname, string StudentOID)
    {
        int AssessmentOID;
        Sections section = new Sections();
        AssessmentOID = section.GetAssessmentOIDByAssessmentName(strname);
        //GridViewSection.DataSource = section.GetSectionsByAssessmentOID(AssessmentOID);
        GridViewScore.DataSource = section.GetSectionsWithNoScore(AssessmentOID, StudentOID);
        GridViewScore.DataBind();

    }

    private void PopulateSection(string strname, string StudentID)
    {
        int AssessmentOID;
        Sections section = new Sections();
        AssessmentOID = section.GetAssessmentOIDByAssessmentName(strname);
        //GridViewSection.DataSource = section.GetSectionsByAssessmentOID(AssessmentOID);
        GridViewSection.DataSource = section.GetSectionsWithScoreByAOID(AssessmentOID, StudentID);
        GridViewSection.DataBind();

    }


    private void PopulateGridIntervention(int RISKOID)
    {
        int UserOID = 0;
        User user = new User();
        user = Session["CurrentUser"] as User;
        if (user != null)
        {
            UserOID = user.UserOID;
        }
        Interventions inv = null;// new Interventions();
        Student stu = new Student();
        Collection<Interventions> IntListStd = new Collection<Interventions>();
        Collection<Interventions> IntListRisk = new Collection<Interventions>();
        int i = stu.GetStudentOIDByStudentBannerID(lblID.Text.ToString());
        if (i > 0)
        {
            inv = new Interventions();
            IntListStd = inv.GetInterventionByStudentOID(i, "1", "10", UserOID);
            //GridViewIntervention.DataSource = inv.GetInterventionByStudentOID(i, "1", "10");

        }
        if (RISKOID > 0)
        {

            inv = new Interventions();
            inv = inv.GetInterventionByRiskOID(RISKOID, lblID.Text.ToString(), "1", "10", UserOID);
            if (inv != null)
            {
                IntListStd.Add(inv);
            }


        }

        GridViewIntervention.DataSource = IntListStd;
        GridViewIntervention.DataBind();
    }

    protected void FormView1_PageIndexChanging(object sender, FormViewPageEventArgs e)
    {
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {



    }

    protected void FormView1_DataBound(object sender, EventArgs e)
    {

    }

    protected void ddlAssesment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int stdOID = Convert.ToInt32(ViewState["STDOID"]);
            int stuOID = Convert.ToInt32(Session["STUOID"]);
            //string  stdOID = (string)ViewState["STDOID"];

            string strSectionName = null;
            strSectionName = ddlAssesment.SelectedItem.ToString();
            //PopulateSection(strSectionName, stdOID);
            //PopulateSectionNoScore(ddlAssesment.Text.ToString(), stdOID);

            PopulateSection(strSectionName, lblID.Text);
            PopulateSectionNoScore(ddlAssesment.Text.ToString(), lblID.Text);

            //This Code for Risk Calculation
            int AOID = new Sections().GetAssessmentOIDByAssessmentName(strSectionName);
            this.BindRiskCalculation(AOID, stdOID);
            Answer ans = new Answer();
            ans = ans.CheckBannerID_Answer(lblID.Text, AOID);
            if (ans.CreatedDate != null)
            {
                lblDate.Text = ans.CreatedDate.ToShortDateString();
            }


        }
        catch (Exception ex)
        { }

    }

    private void BindRiskCalculation(int AOID, int SOID)
    {
        RiskCalculation riskCalculation = new RiskCalculation().GetRiskCalculationByAOIDAndSOID(AOID, SOID);
        if (riskCalculation == null)
        {
            LabelRisk.Text = "";
        }
        else
        {
            LabelRisk.Text = riskCalculation.RiskName.ToString();
        }
    }


    protected void CourseGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        CourseGrid.PageIndex = e.NewPageIndex;
        PopulateCourseByStudentID();
    }

    protected void GridViewIntervention_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }

    protected void GridViewIntervention_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewIntervention.PageIndex = e.NewPageIndex;
        //PopulateGridIntervention();
        PopulateGridIntervention(Convert.ToInt32(ViewState["ROID"]));
    }

    protected void lbtnNext_Click(object sender, EventArgs e)
    {
        lbtnPrev.Enabled = true;
        Student stu = new Student();
        //int stdOID = Next();
        int stdOID = 0;
        rowID = Convert.ToInt32(ViewState["RowID"]);
        rowID++;
        ViewState["RowID"] = rowID;
        string strID = stu.GetStudentByRowID_Profile(rowID);
        stdOID = stu.GetStudentOIDByRowID_Profile(rowID);
        //string strID = stu.GetStudentIDByStudentBannerOID(stdOID);

        if (strID == "" || strID == null)
        {
            lbtnNext.Enabled = false;
        }
        else
        {
            Populate(strID);
            populateDropdownList();
            PopulateCourseByStudentID();

            PopulateSectionNoScore(ddlAssesment.Text.ToString(), strID);
            PopulateSection(ddlAssesment.Text.ToString(), strID);

            int AOID = new Sections().GetAssessmentOIDByAssessmentName(ddlAssesment.Text.ToString());
            Student stu1 = new Student();
            int studentOID = stu1.GetStudentOIDByStudentBannerID(strID);
            if (!string.IsNullOrEmpty(studentOID.ToString()))
            {
                this.BindRiskCalculation(AOID, Convert.ToInt32(studentOID));
            }
            if (LabelRisk.Text != "")
            {
                PopulateGridIntervention(Convert.ToInt32(ViewState["ROID"]));
            }
            else
            {
                PopulateGridIntervention(0);
            }
        }
    }

    private int Next()
    {
        int i = 0;
        string strID = "1";
        Student stu = new Student();
        i = stu.GetStudentOIDByStudentBannerID(lblID.Text.ToString());

        i++;
        strID = stu.GetStudentIDByStudentBannerOID(i);
        if (strID == "" || strID == null)
        {
            i++;

            for (int j = i; j < 25000000; j++)
            {
                strID = stu.GetStudentIDByStudentBannerOID(j);
                if (strID != "" && strID != null)
                {
                    i = j;
                    return i;
                }
            }

        }

        return i;
    }

    //protected void ddlAssesment_SelectedIndexChanged1(object sender, EventArgs e)
    //{

    //}
    //protected void ddlAssesment_SelectedIndexChanged2(object sender, EventArgs e)
    //{

    //}

    protected void lbtnPrev_Click(object sender, EventArgs e)
    {
        Session["StudentID"] = lblID.Text.ToString();
        lbtnNext.Enabled = true;
        Student stu = new Student();

        //int OID = Prev();
        int OID = 0;
        rowID = Convert.ToInt32(ViewState["RowID"]);
        rowID--;
        ViewState["RowID"] = rowID;
        string strID = stu.GetStudentByRowID_Profile(rowID);
        OID = stu.GetStudentOIDByRowID_Profile(rowID);
        //string strID = stu.GetStudentIDByStudentBannerOID(OID);
        if (strID == "" || strID == null)
        {
            lbtnPrev.Enabled = false;

        }
        else
        {
            Populate(strID);
            populateDropdownList();
            PopulateCourseByStudentID();
            //PopulateGridIntervention();

            PopulateSectionNoScore(ddlAssesment.Text.ToString(), strID);
            PopulateSection(ddlAssesment.Text.ToString(), strID);
            int AOID = new Sections().GetAssessmentOIDByAssessmentName(ddlAssesment.Text.ToString());
            Student stu1 = new Student();
            int studentOID = stu1.GetStudentOIDByStudentBannerID(strID);
            if (!string.IsNullOrEmpty(studentOID.ToString()))
            {
                this.BindRiskCalculation(AOID, Convert.ToInt32(studentOID));
            }
            if (LabelRisk.Text != "")
            {
                PopulateGridIntervention(Convert.ToInt32(ViewState["ROID"]));
            }
            else
            {
                PopulateGridIntervention(0);
            }
        }
    }



    private int Prev()
    {
        int i = 0;
        Student stu = new Student();
        i = stu.GetStudentOIDByStudentBannerID(lblID.Text.ToString());

        if (i > 1)
        {
            i = i - 1;
            string strID = stu.GetStudentIDByStudentBannerOID(i);
            if (strID == "" || strID == null)
            {
                i--;
                for (int j = i; j > 0; j--)
                {
                    strID = stu.GetStudentIDByStudentBannerOID(j);
                    if (strID != "" && strID != null)
                    {
                        i = j;
                        return i;
                    }
                }
            }
        }
        else
        {
            i = 1;
        }
        return i;
    }


    protected void btnPrescribed_Click(object sender, EventArgs e)
    {

    }
    protected void CourseGrid_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridViewSection_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            int stdOID = (int)ViewState["STDOID"];
            GridViewSection.PageIndex = e.NewPageIndex;
            PopulateSection(ddlAssesment.Text.ToString(), lblID.Text);
            PopulateSectionNoScore(ddlAssesment.Text.ToString(), lblID.Text);
        }
        catch (Exception ex)
        { }
    }

    private void PrintInterventionAll(int SOID)
    {
        try
        {
            string html = "";
            Interventions inter = new Interventions();
            Collection<Interventions> _list = new Collection<Interventions>();
            _list = inter.GetInterventionByStudentOID(SOID);

            //if (inter != null)
            if (_list.Count != 0)
            {
                foreach (Interventions iv in _list)
                {
                    Student stdnt = new Student();
                    // stdnt = stdnt.GetStudentByOID(inter.StudentOID);
                    stdnt = stdnt.GetStudentByOID(SOID);
                    if (stdnt != null)
                    {


                        //Create Html
                        html += "<table width='100%' ><tr><td><img alt='Logo' src='../../images/chippewavtc_logo.jpg' /></td><td style='text-align:center;'><span style='font-size:medium;font-weight:bold;'> Steps to Success: CVTC's commitment to helping YOU succeed!</span><br/><span style='text-align:center;'> Inventory of Student Success: Personalized Success Plan </span></td></tr></table>";
                        html += "<table width='100%'><tr><td style='width:45%;'>Success Steps For:<br />First:" + stdnt.FirstName + "  Last: " + stdnt.LastName + "</td><td> <div style='background-color: #C0C0C0;float:left;width:20%;'>Program&nbsp;&nbsp;</div><div style='padding-left:10px;border:solid 1px black;width:75%; float:left;'>" + stdnt.MajorProgramEnrollment + "</div></td></tr></table>";
                        // html += "<table width='100%'><tr><td>Success Steps For:<br />First: Abdul Last: Karim</td><td>                                <div style=' background-color: #C0C0C0;float:left;width:20%; '>Program&nbsp;&nbsp;</div><div style='padding-left:10px;border:solid 1px black;width:78%; float:left;'>Information Technology </div></td></tr></table>";
                        html += "<table width='100%'><tr><td style='border:solid 1px black;'>Inventory of Student Success results provide comparsions of your attitudes and confidence with other<br />    entering CVTC students in key focus areas that could affect your academic performance and program<br /> persistance:     </td></tr></table>";
                        html += "<table style='width: 100%;'>";

                        //Get Sectionwise Student Rank 
                        StudentRank studentRank = new StudentRank();
                        //Collection<StudentRank> srLIST = studentRank.GetStudentRankBySOIDandAOID(SOID,22);
                        Collection<StudentRank> srLIST = studentRank.GetStudentRankByOID(SOID);
                        //if
                        //for (int i = 0; i < 5; i++)
                        foreach (StudentRank stRank in srLIST)
                        {

                            html += "<tr>";
                            html += "<td style='border:solid 1px black;background-color:#727272;width:200px;'>" + stRank.SectionName + "</td>";
                            html += "<td style='border:solid 1px black;width:50px;text-align:center;'>" + stRank.Rank.ToString() + "</td>";
                            html += "<td style='border:solid 1px black;'>" + stRank.Comment + "</td>";
                            html += "</tr>";
                        }
                        html += "</table>";

                        html += "<table style='width: 100%; border:solid 1px black;'>    <tr  >    <td>COMPASS test scores measure your academic readiness for college coursework in four broad areas:</td>    </tr>        </table>";

                        html += " <table style='width: 60%; '>    <tr><td style='background-color:#727272;width:200px;'>COMPASS Pre-Algebra</td><td style='width:100px; border:solid 1px black;'>" + stdnt.PrealgebraTestScore + "</td><td style='background-color:#727272;width:200px;'>COMPASS Writing</td><td style='width:100px; border:solid 1px black;'>" + stdnt.CompassWrittingTestScore + "</td></tr>   <tr><td style='background-color:#727272;width:200px;'>COMPASS Algebra</td><td style='width:100px; border:solid 1px black;'>" + stdnt.CompassalgebraTestScore + "</td><td style='background-color:#727272;width:200px;'>COMPASS Reading</td><td style='width:100px; border:solid 1px black;'>" + stdnt.CompassReadingTestScore + "</td></tr> </table>";
                        html += "<br/>";
                        //Last Part Intervention
                        html += "<table style='width: 100%; '>";
                        html += "<tr><td style='width:60px;font-size:medium;font-weight:bold;'>Step 1</td><td style='background-color:#727272;width:100px;'> Plan Target</td><td>" + inter.DomainName + "</td><td></td></tr>";
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


                    }



                }
                html += "<script type =\"text/javascript\">";
                html += "printDiv();";
                html += "</script>";
                print_div1.InnerHtml = html;
            }
        }
        catch (Exception ex)
        { }
    }
    protected void btnPrintAll_Click(object sender, EventArgs e)
    {
        string strID = null;
        strID = ViewState["STDOID"].ToString();
        PrintInterventionAll(Convert.ToInt32(strID));
    }
    protected void GridViewScore_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void GridViewIntervention_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        for (int i = 0; i < GridViewIntervention.Rows.Count; i++)
        {
            HiddenField HiddenFieldUnread = (HiddenField)GridViewIntervention.Rows[i].Cells[0].FindControl("HiddenFieldUnread");
            HiddenField HiddenFieldFlag = (HiddenField)GridViewIntervention.Rows[i].Cells[0].FindControl("HiddenFieldFlag");
            HiddenField HiddenFieldUser = (HiddenField)GridViewIntervention.Rows[i].Cells[0].FindControl("HiddenFieldUser");
            HiddenField HiddenFieldPOID = (HiddenField)GridViewIntervention.Rows[i].Cells[0].FindControl("HiddenFieldPOID");

            User user = new User();
            user = Session["CurrentUser"] as User;
            if (user != null && !CheckUser(Convert.ToInt32(HiddenFieldPOID.Value)))
            {

                if (HiddenFieldFlag.Value == "1")
                {
                    GridViewIntervention.Rows[i].BackColor = System.Drawing.Color.Tomato;
                }
                else
                {
                    GridViewIntervention.Rows[i].BackColor = System.Drawing.Color.White;

                }
            }
            else
            {
                GridViewIntervention.Rows[i].BackColor = System.Drawing.Color.White;
            }

        }


    }


    private bool CheckUser(int poid)
    {
        bool returnType = false;
        int UserOID = 0;
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

    protected void HandOffURLs_Load(object sender, EventArgs e)
    {


    }

}
