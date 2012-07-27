using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Data.Odbc;
using System.Data;

public partial class pg_student_Merge : System.Web.UI.Page
{
    string connectionString = "";
    string id = null;
    string tempID = null;
    string  birth="";
    string term = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString(); 
        lblStatus.Text = "";
        if (!IsPostBack)
        {

            if (Request.QueryString["id"] != null)
            {
                id = Request.QueryString["id"];
                populate(id);
            }
            if (Request.QueryString["tempID"] != null)
            {
                tempID = Request.QueryString["tempID"];
               
            }
           
        }
    }

    private void populate(string bannerID)
    {
        Student stu = new Student();
        Collection<Student> studentList = new Collection<Student>();
        studentList = stu.GetStudentsForMerge(bannerID);
        gridMerge.DataSource = studentList;
        gridMerge.DataBind();
        if (studentList.Count ==0)
        {
            lblStatus.Text = "No Related Data Exist";
             lblStatus.ForeColor = Color.Red;
        }
    }

    protected void gridMerge_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridMerge.PageIndex = e.NewPageIndex;
        populate(Request.QueryString["id"]);
    }

    protected void btnMerge_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((Button)sender).Parent.Parent as GridViewRow;
        HiddenField HiddenFieldStudentID = (HiddenField)row.Cells[0].FindControl("HiddenFieldStudentID");
        string StudentOID = HiddenFieldStudentID.Value;
        Student std = new Student();
        int id = std.GetStudentOIDByBannerID(Request.QueryString["tempID"]);
        //lblStatus.Text = StudentOID;

        std = std.GetStudentByStudentOID(Convert.ToInt32(StudentOID));
        if (std != null)
        {
            DeleteStudent(std.StudentOID);

            if (std.UpdateTempStudent(std, id) == true)
            {
                std.UpdateTempStudentinAnswer(Request.QueryString["id"], Request.QueryString["tempID"]);
                populate(Request.QueryString["id"]);
                lblStatus.ForeColor = Color.Red;
                lblStatus.Font.Size = 12;
                lblStatus.Font.Bold = true;
                lblStatus.Text = "Successfully Merged With Orginal Student";
            }
            else
            {
                lblStatus.ForeColor = Color.Red;
                lblStatus.Font.Size = 12;
                lblStatus.Font.Bold = true;
                lblStatus.Text = "Merging Fail...";
            }
        }



    }

    private void DeleteStudent(int id)
    {


        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Student_Delete(?)}";
                command.CommandType = CommandType.StoredProcedure;

                OdbcParameter paramID = new OdbcParameter("@StudentOID", OdbcType.VarChar, 200);
                paramID.Value = id;
                command.Parameters.Add(paramID);

                connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ae)
                { }

            }
        }

    }
}
