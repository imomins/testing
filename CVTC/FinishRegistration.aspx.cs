using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pg_answer_FinishRegistration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = "";
        Label1.Visible = true;
        Label2.Visible = true;
        Label3.Visible = true;
        Label4.Visible = true;
        string  BannerID= (Request.QueryString["bannerid"] == null) ? "0" : Convert.ToString(Request.QueryString["bannerid"]);
        
        Session["BannerID"] = BannerID;
        Student std = new Student();
        std = std.GetStudentByStudentBannerID(BannerID);
        if (std != null)
        {
            lblBannerID.Text = std.StudentID;
            lblFirstName.Text = std.FirstName;
            lblLastName.Text = std.LastName;
            lblMiddleName.Text = std.MiddleName;
            lblTerm.Text = std.ProgramEnrollment;
            lblProgram.Text = std.MajorProgramEnrollment;
        }
        else if (std == null || BannerID=="0")
        {
            Label1.Visible = false ;
            Label2.Visible = false ;
            Label3.Visible = false ;
            Label4.Visible = false ;
            lblError.Text = "Error Occured During Registration Process. Please Try again...";
        }
    }
}
