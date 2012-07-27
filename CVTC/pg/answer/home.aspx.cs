 using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;
using System.Data;
using System.Collections.ObjectModel;

public partial class pg_answer_home : System.Web.UI.Page
{
    string connectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();
            int aid = (Request.QueryString["aid"] == null) ? 0 : Convert.ToInt32(Request.QueryString["aid"].ToString());
            string reg = (Request.QueryString["reg"] == null) ? "" : Convert.ToString(Request.QueryString["reg"].ToString());
            Session["aoid"] = aid;
            Session["reg"] = reg;
            if (reg == null && reg == "")
            {
                populateProgram();
            }
            else
            {
                if (Request.QueryString["id"] != null)
                {
                    Populate(Request.QueryString["id"]);
                    ButtonSubmit_Click(null, null);
                }
            }
        }
    }

    private void Populate(string bannerid)
    {
        Student stu = new Student();
        stu = stu.GetStudentByStudentOID(bannerid);
        if (stu != null)
        {
            TextBoxBannerID.Text = stu.StudentID;
            if (stu.FirstName != null)
            {
                TextBoxFirstName.Text = stu.FirstName;
            }
            if (stu.LastName  != null)
            {
                TextBoxLastName.Text = stu.LastName;
            }
        }
        else
        {
            LabelMsg.Text = "Please Fill Up Regitration Page again";
        }
    }

    private void populateProgram()
    {
        Student stu = new Student();
        Collection<Student> ProgramList = new Collection<Student>();
        
        ProgramList = stu.GetProgramInterst();
        foreach (Student st in ProgramList)
        {
            TextBoxProgram.Items.Add(st.MajorProgramEnrollment);
        }
       // TextBoxProgram.DataSource = ProgramList;
        TextBoxProgram.DataBind();
    }
    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            //if (TextBoxBannerID.Text.Length < 1 && Date1.CalendarDateString=="")
            if (TextBoxBannerID.Text.Length < 1 && Date1.CalendarDateString == "")
            {
                LabelMsg.Text = "Please Enter BannerID or Date of Birth";
                return;
            }
            if (TextBoxFirstName.Text.Length < 1)
            {
                LabelMsg.Text = "Please Enter First Name";
                TextBoxFirstName.Focus();
                return;
               
            }
            if (TextBoxLastName.Text.Length < 1)
            {
                LabelMsg.Text = "Please Enter Last Name";
                TextBoxLastName.Focus();
                return;

            }
            else
            {
                string aid = (Session["aoid"] != null) ? Session["aoid"].ToString() : "0";

                string firstName = "", lastName = "", bannerID = "", studentName = "", creditQuestion = "", term = "", program = "", middleName = "";
                string dob = "";

                firstName = TextBoxFirstName.Text.Trim();
                lastName = TextBoxLastName.Text.Trim();
                if (TextBoxMiddleName.Value != "")
                {
                    middleName = TextBoxMiddleName.Value.Trim();
                }
                bannerID = TextBoxBannerID.Text.Trim();

                studentName = lastName + ", " + firstName + " " + middleName;
                //creditQuestion = TextBoxPrior.Text;
                term = TextBoxTerm.Text;
                //program = TextBoxProgram.SelectedItem.Text;
                if (Date1.CalendarDateString != "")
                {
                    dob = DateTime.Parse(Date1.CalendarDateString).ToString();
                }
                Student std = new Student();
                // std = std.GetValidStudentsForAssessment(bannerID, studentName,term,program);
                std = std.GetValidStudentsForAssessment(bannerID, firstName, middleName, lastName, term, dob);
                Answer ans = new Answer();
                ans = ans.CheckBannerID_Answer(bannerID, Convert.ToInt32(aid));
                if (ans != null)
                {
                    LabelMsg.Text = "This Student already take this Assessment....";
                }

                else if (std != null)
                {

                    Session["currentStd"] = std;
                    //Response.Redirect("ExamHome.aspx?aid=" + aid, false);
                    Response.Redirect("QuestionSheet.aspx?aoid=" + aid, false);
                }
                else
                {
                    LabelMsg.Text = "Student does not exist with your provided information";
                }


            }


        }
        catch (Exception ex)
        {
            LabelMsg.Text = "Student does not exist with your provided information";
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string asseeementURL = System.Web.Configuration.WebConfigurationManager.AppSettings["asseeementURL"].ToString();
        string url = asseeementURL + "CVTC/Registration.aspx?aid=" + Session["aoid"];
        Response.Redirect(url);
    }
}
