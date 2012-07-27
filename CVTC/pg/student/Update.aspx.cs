using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;
using System.Data;
using System.Collections.ObjectModel;
using System.Text;

public partial class pg_student_Update : System.Web.UI.Page
{
    #region Variable
    private string strLastName = "";
    private string strFirstName = "";
    private string strBannerID = "";
    private string strTermCode = "";
    private string strFullPart = "";
    private double  dblGPA = 0.00;
    private double dblCreditAttempted = 0.00;
    private double dblCreditEarned = 0.00;

    private string strPreAlgebra = "0";
    private string strAlgebra = "0";
    private string strWritting = "0";
    private string strReading = "0";

    private string strEng = "0";
    private string strMath = "0";
    private string strReadingAss = "0";
    private string strScienceAss = "0";

    private string strHighSchoolName = "";
    private string strGradDate = "";
    private string strPhone = "";
    private string strAdd1 = "";
    private string strAdd2 = "";
    private string strAdd3 = "";

    private string strCity = "";
    private string strState = "";
    private string strZIP = "";
    private string strEmail = "";

    private string strStatus = "Pending";
    #endregion

    string connectionString;
    string BannerID = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"]!=null)
            {
                string strID = Request.QueryString["id"].ToString();
                ViewState["STDOID"] = strID;
                Student stu = new Student();
                string strOID = stu.GetStudentIDByStudentBannerOID(Convert.ToInt32(strID));
                ViewState["BannerID"] = strOID;
                Populate(strOID);
            }

            connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();
            populateProgram();
            populateStatus();
           
        }
       
    }

    private void populateStatus()
    {
        DropDownListStatus.Items.Add("Pending");
        DropDownListStatus.Items.Add("Approved");
        
    }
   
    private void populateProgram()
    {
        Student stu = new Student();
        Collection<Student> ProgramList = new Collection<Student>();
        ProgramList = stu.GetProgramInterst();
        txtProgramInterest.DataSource = ProgramList;
        txtProgramInterest.DataBind();
    }

    private void Populate(string BannerID)
    {
        Student stu = new Student();
        stu = stu.GetStudentByStudentOID(BannerID);
        if (stu != null)
        {
            TextBoxBannerID.Text = stu.StudentID;
            TextLastName.Text = stu.LastName;
            TextFirstName.Text = stu.FirstName;
            if (stu.MiddleName!=null )
            {
            TextMiddleName.Value  = stu.MiddleName.Trim ();
            }
            if(stu.ProgramEnrollment!=null )
            {
            TextTerm.Text = stu.ProgramEnrollment;
            }
            if (stu.BirthDate != null )
            {
                if (stu.BirthDate.ToString().Length > 11)
                {
                    //Date1.CalendarDateString =Convert .ToString ( stu.BirthDate).Substring (0,8);
                    TextBirthDate.Value = stu.BirthDate.ToString().Substring(0, stu.BirthDate.ToString().Length - 12);
                }
            }
            TextBoxPriorCredit.Text = stu.PriorCreditQuestion;
            if (stu.MajorProgramEnrollment != null)
            {
                txtProgramInterest.Text = stu.MajorProgramEnrollment;
            }
            DropDownListStatus.Text = stu.Status;


        }
        createSession();
        
    }


    private void createSession()
    {
        try
        {
            Student stu1 = new Student();
            stu1 = stu1.GetValidStudentsForMerging("", TextFirstName.Text.Trim (), TextMiddleName.Value.Trim (), TextLastName.Text.Trim (), TextTerm.Text.Trim (), TextBirthDate.Value);
            if (stu1 != null)
            {
                BannerID = stu1.StudentID;

            }
            else
            {
                BannerID = "0";
            }
        }
        catch
        {
            BannerID = "0";
        }
        Session["bannerid"] = BannerID;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Student stu = new Student();
            stu.StudentOID =Convert .ToInt32 ( ViewState["STDOID"]);
            stu.StudentID = TextBoxBannerID.Text;

            stu.LastName = TextLastName.Text;
            if (TextLastName.Text.Length < 1)
            {
                lblStatus.Text = "Last Name Required";
                TextLastName.Focus();
                return;
            }
            stu.FirstName = TextFirstName.Text;
            if (TextFirstName.Text.Length < 1)
            {
                lblStatus.Text = "First Name Required";
                TextFirstName.Focus();
                return;
            }
            stu.MiddleName = TextMiddleName.Value ; 
            stu.FullName = TextLastName.Text+", "+TextFirstName .Text+" "+TextMiddleName .Value ;
            stu.ProgramEnrollment = TextTerm.Text;
            stu.MajorProgramEnrollment = txtProgramInterest.Text;
            if (TextBirthDate.Value.Length <1)
            {
                lblStatus.Text = "Birth Date Required";
                TextBirthDate.Focus();
                return;
            }
            if (TextBirthDate.Value!=null)
            {
                stu.BirthDate = Convert.ToDateTime(TextBirthDate.Value);
            }
            stu.PriorCreditQuestion = TextBoxPriorCredit.Text;
            string status = null;
            if (DropDownListStatus.Text == "Approved")
            {
                stu.Status = null;
                status = null;
            }
            else
            {
                status = "Pending";
            }
            bool retValue = false;
          

            if (stu != null)
            {
                retValue = stu.UpdateOldStudent(stu, status);
                if (retValue == false)
                {
                    lblStatus.Text = "Not Saved";
                }
                else
                {
                    Populate(stu.StudentID);
                    lblStatus.Text = "Saved!";
                   
                }
            }
            else
            {
                lblStatus.Text = "Not Saved ......!";
               
            }
        }
        catch (Exception ax)
        {
            lblStatus.Text = "Error Occured : " + ax.ToString();
        }
    }


    private void clearData()
    {
        TextLastName.Text = "";
        TextFirstName.Text = "";
        TextTerm.Text = "";
        //TextFullPart.Text = "";
       
        TextLastName.Focus();

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        clearData();
    }

    protected void btnMerge_Click(object sender, EventArgs e)
    {
        //if (DropDownListStatus.Text == "Pending")
        //{
        //    lblStatus.Text = "You have to Update Staus to Approved";
        //    return;
        //}
        //else
        //{
            //if (ViewState["BannerID"] != null)
            //{
            //    Populate(ViewState["BannerID"].ToString());
            //}
            createSession();
            lblStatus.Text = "";
            string id = TextBoxBannerID.Text;
            string name = TextLastName.Text + ", " + TextFirstName.Text + " " + TextMiddleName.Value;
            string term = TextTerm.Text;
            string birth = null;
            if (TextBirthDate.Value != "1/1/0001")
            {
                birth = TextBirthDate.Value;
            }
            string url = "Merge.aspx?id=" + Session["bannerid"] + "&tempID=" + TextBoxBannerID.Text.Trim() + "";
            //string url = "Merge.aspx?name="+name+"&term="+term +"&birth="+birth+"&id="+id+"";
            Response.Write("<script>window.open('" + url + "','Merging Student', 'width=800,height=300,scrollbars=yes');</script>");
        //}
    }
}
