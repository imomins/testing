using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;
using System.Data;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Net;
using System.Text.RegularExpressions;

public partial class Registration : System.Web.UI.Page
{
    #region Variable
    private string strLastName = "";
    private string strFirstName = "";
    private string strBannerID = "";
    private string strTermCode = "";
    
    private string strStatus = "Pending";
    #endregion

    string connectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int aid = (Request.QueryString["aid"] == null) ? 0 : Convert.ToInt32(Request.QueryString["aid"].ToString());
            Session["aid"] = aid;
            connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();
            populateProgram();
            populateTerm();
        }
        string strKey = DateTime.Now.ToString("HHmmss");
        strBannerID = "@TM" + strKey;
    }


   
    private void populateProgram()
    {
        Student stu = new Student();
        Collection<Student> ProgramList = new Collection<Student>();
        ProgramList = stu.GetProgramInterst();
       
        txtProgramInterest.DataSource = ProgramList;
        txtProgramInterest.DataBind();
    }


    private void populateTerm()
    {
        Student stu = new Student();
        Collection<Student> TermList = new Collection<Student>();
        TermList = stu.GetTerms();

        TextTerm.DataSource = TermList;
        TextTerm.DataBind();
    }

    private bool isRealDomain(string inputEmail)
    {
    bool isReal = false;
    try
    {
        string[] host = (inputEmail.Split('@'));
        string hostname = host[1];

        IPHostEntry IPhst = Dns.GetHostEntry(hostname);
        IPEndPoint endPt = new IPEndPoint(IPhst.AddressList[0], 25);
        Socket s = new Socket(endPt.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);
        s.Connect(endPt);
        s.Close();
        isReal = true;
    }
    catch (Exception ae)
    {
    }

    return isReal;
    }

    public static bool IsValidEmail1(string email)
    {

        Regex rx = new Regex(
        @"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
        return rx.IsMatch(email);
    }

    
    public bool IsValidEmail(string email)
    {
        //regular expression pattern for valid email
        //addresses, allows for the following domains:
        //com,edu,info,gov,int,mil,net,org,biz,name,museum,coop,aero,pro,tv
        string pattern = @"^[-a-zA-Z0-9][-.a-zA-Z0-9]*@[-.a-zA-Z0-9]+(\.[-.a-zA-Z0-9]+)*\.
    (com|edu|info|gov|int|mil|net|org|biz|name|museum|coop|aero|pro|tv|[a-zA-Z]{2})$";
        //Regular expression object
        Regex check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
        //boolean variable to return to calling method
        bool valid = false;

        //make sure an email address was provided
        if (string.IsNullOrEmpty(email))
        {
            valid = false;
        }
        else
        {
            //use IsMatch to validate the address
            valid = check.IsMatch(email);
        }
        //return the value to the calling method
        return valid;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Student stu = new Student();
            stu.StudentID = strBannerID;

            #region Validation

            stu.FirstName = TextFirstName.Text;
            if (TextFirstName.Text.Length < 1)
            {
                lblStatus.Text = "First Name Required";
                TextFirstName.Focus();
                return;
            }

            stu.LastName = TextLastName.Text;
            if (TextLastName.Text.Length < 1)
            {
                lblStatus.Text = "Last Name Required";
                TextLastName.Focus();
                return;
            }
            

            if (TextTerm.Text.Length < 1)
            {
                lblStatus.Text = "Term Code Required";
                TextTerm.Focus();
                return;
            }

            if (txtProgramInterest.Text.Length < 1)
            {
                lblStatus.Text = "Program Interest Required";
                txtProgramInterest.Focus();
                return;
            }

            stu.EmailAddress  = TextBoxEmail.Text;
            if (TextBoxEmail.Text.Length < 1)
            {
                lblStatus.Text = "Email Address Required";
                TextBoxEmail.Focus();
                return;
            }

            if (Date1.CalendarDateString == "")
            {
                lblStatus.Text = "Birth Date Required";
                Date1.Focus();
                return;
            }
            if (TextBoxPriorCredit.Text .Length < 1)
            {
                lblStatus.Text = "Prior Credit Question Required";
                TextBoxPriorCredit.Focus();
                return;
            }

            if (!IsValidEmail(TextBoxEmail.Text.Trim()))
            {
                lblStatus.Text = "Invalid Email Address";
                TextBoxEmail.Focus();
                 return ;
            }
            #endregion

            stu.FullName = TextLastName.Text+", "+TextFirstName .Text+" "+TextBoxMiddleName.Value ;
            stu.ProgramEnrollment = TextTerm.Text;
            stu.MajorProgramEnrollment = txtProgramInterest.SelectedItem.Text;
            
            stu.Status = strStatus;
            stu.MiddleName = TextBoxMiddleName.Value ;
            //if (TextBirthDate.Value != null && TextBirthDate.Value != "")
            //{
                //stu.BirthDate = Convert .ToDateTime ( TextBirthDate.Value);
                stu.BirthDate = DateTime.Parse(Date1.CalendarDateString);
            //}
            stu.PriorCreditQuestion = TextBoxPriorCredit.Text;
            bool retValue = false;
           
            if (stu != null)
            {
                retValue = stu.AddNewStudent(stu);
                if (retValue == false)
                {
                    lblStatus.Text = "Not Saved";
                }
                else
                {
                    
                    string asseeementURL = System.Web.Configuration.WebConfigurationManager.AppSettings["asseeementURL"].ToString();
                    string url = asseeementURL + "CVTC/Exam.aspx?aid=" + Session["aid"] + "&reg=yes&id=" + strBannerID + "";
                    //Response.Redirect("FinishRegistration.aspx?bannerid=" + stu.StudentID+"&aid="+Session["aid"], false);
                    Response.Redirect(url , false);
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
        //TextTerm.Text = "";
        TextBoxPriorCredit.Text = "";
        TextBoxMiddleName.Value  = "";
        TextBirthDate.Value = "";
        TextBoxEmail.Text = "";
        TextFirstName.Focus();

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        clearData();
    }
}
