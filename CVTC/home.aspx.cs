using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;
using System.Data;

public partial class home : System.Web.UI.Page
{

    DataTable dtStudents;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            GenerateCalendar();
            GenerateTaskOnCalendar();
          
            Student stu = new Student();
            //string  strOID = stu.GetFirstStudentID();
            int firstRowID = 1;
            string strOID = stu.GetStudentByRowID(firstRowID);
            ViewState["RowID"] = firstRowID;
            int StdOID = stu.GetStudentOIDByStudentBannerID(strOID);

            //dtStudents = 

            ViewState["StuID"] = StdOID;
            
            string fassName = "";
            Sections sec = new Sections();
            Populate(Convert.ToString(strOID));
            populateDropdownList();
            fassName = sec.GetFirstAssessmentName();
            //PopulateSection(ddlAssesment.Text, Convert.ToInt32(StdOID));
            PopulateSection(ddlAssesment.Text, strOID);
            PopulateSectionNoScore(ddlAssesment.Text, strOID);
            Initialization();
            populateChart();
        }
        
        
    }

    private void populateChart()
    {
        Chart chrt = new Chart();
        chrt = chrt.GetAllLabels();
        if (chrt!=null)
        {
        Chart1.Series["HPrealgebra"].Label = chrt .Label1;
        Chart1.Series["LPrealgebra"].Label = chrt.Label2;
        string strPre = "0";
        if (chrt.Label3 != "" && chrt.Label3 != null)
        {
            strPre = chrt.Label3.Substring(0, 5);
        }
        Chart1.Series["APrealgebra"].Label = strPre;

        Chart1.Series["HAlgebra"].Label = chrt.Label4;
        Chart1.Series["LAlgebra"].Label = chrt.Label5;
       // string strPre1 = chrt.Label6.Substring(0, 5);
        string strPre1 = "0";
        if (chrt.Label6 != "" && chrt.Label6 != null)
        {
            strPre1 = chrt.Label6.Substring(0, 5);
        }
        Chart1.Series["AAlgebra"].Label = strPre1;

        Chart1.Series["HWritting"].Label = chrt.Label7;
        Chart1.Series["LWritting"].Label = chrt.Label8;
        //string strPre2 = chrt.Label9.Substring(0, 5);
        string strPre2 = "0";
        if (chrt.Label9 != "" && chrt.Label9 != null)
        {
            strPre2 = chrt.Label9.Substring(0, 5);
        }
        Chart1.Series["AWritting"].Label = strPre2;

        Chart1.Series["HReading"].Label = chrt.Label10;
        Chart1.Series["LReading"].Label = chrt.Label11;
        //string strPre3 = chrt.Label12.Substring(0, 5);
        string strPre3 = "0";
        if (chrt.Label12 != "" && chrt.Label12 != null)
        {
            strPre3 = chrt.Label12.Substring(0, 5);
        }
        Chart1.Series["AReading"].Label = strPre3;

        Chart1.Series["HEnglish"].Label = chrt.Label13;
        Chart1.Series["LEnglish"].Label = chrt.Label14;
        //string strPre4 = chrt.Label15.Substring(0, 5);
        string strPre4 = "0";
        if (chrt.Label15 != "" && chrt.Label15 != null)
        {
            strPre4 = chrt.Label15.Substring(0, 5);
        }
        Chart1.Series["AEnglish"].Label = strPre4;

        Chart1.Series["HMath"].Label = chrt.Label16;
        Chart1.Series["LMath"].Label = chrt.Label17;
        //string strPre5 = chrt.Label18.Substring(0, 5);
        string strPre5 = "0";
        if (chrt.Label18 != "" && chrt.Label18 != null)
        {
            strPre5 = chrt.Label18.Substring(0, 5);
        }
        Chart1.Series["AMath"].Label = strPre5;

        Chart1.Series["HReadingAssessment"].Label = chrt.Label19;
        Chart1.Series["LReadingAssessment"].Label = chrt.Label20;
        //string strPre6 = chrt.Label21.Substring(0, 5);
        string strPre6 = "0";
        if (chrt.Label21 != "" && chrt.Label21 != null)
        {
            strPre6 = chrt.Label21.Substring(0, 5);
        }
        Chart1.Series["AReadingAssessment"].Label = strPre6;

        Chart1.Series["HScienceAssessment"].Label = chrt.Label22;
        Chart1.Series["LScienceAssessment"].Label = chrt.Label23;
        //string strPre7 = chrt.Label24.Substring(0, 5);
        string strPre7 = "0";
        if (chrt.Label24 != "" && chrt.Label24 != null)
        {
            strPre7 = chrt.Label24.Substring(0, 5);
        }
        Chart1.Series["AScienceAssessment"].Label = strPre7;
        }
    }

    private void populateDropdownList()
    {
        
        Collection<Answer> answers = new Collection<Answer>();
        Answer ans = new Answer();
        answers = ans.GetAOIDByStudentID(lbtnID.Text);
        Assessment As = new Assessment();
        //if (answers == null)
        //{
            ddlAssesment.Items.Clear();
        //}
        foreach (Answer a in answers)
        {
            
            Collection<Assessment> assList = new Collection<Assessment>();
            assList = As.GetAssessmentByAssessmentOID(a.AssessmentOID);
            foreach (Assessment asmnt in assList)
            {
                ddlAssesment.Items.Add(asmnt.AssessmentName);
            }
            
            //ddlAssesment.DataSource = As.GetAssessmentByAssessmentOID(a.AssessmentOID);
           
        }
        ddlAssesment.DataBind();
        //int AssessmentOID = 0;
        
        //ans = ans.GetAnswerByStudentID(lbtnID.Text);
        //if (ans != null)
        //{
        //    AssessmentOID = ans.AssessmentOID;
        //}

        //Assessment As = new Assessment();

        //ddlAssesment.DataSource = As.GetAssessmentByAssessmentOID(AssessmentOID);
        //ddlAssesment.DataBind();
    }

    private void Populate(string strID)
    {

        Student stu = new Student();
        stu=stu.GetStudentByStudentOID(strID);
        if (stu != null)
        {
            lbtnStudentName.Text = stu.FullName;
            lbtnID.Text = stu.StudentID;
            //lblDate.Text = Convert.ToString(stu.LatestTestingDate);
            Session["StudentID"] = lbtnID.Text.ToString();
        }
        
       
    }

    private void PopulateSection(string strname, string  StudentID)
    {
        int AssessmentOID=0;
        Sections section = new Sections();
        AssessmentOID = section.GetAssessmentOIDByAssessmentName(strname);
        Answer ans = new Answer();
        ans = ans.GetAnswerByAssessmentandStudentID(AssessmentOID, StudentID);
        if (ans != null)
        {
            lblDate.Text = Convert.ToString(ans.CreatedDate);

        }
        else
        {
            lblDate.Text = "";
        }
        GridViewSection.DataSource = section.GetSectionsWithScoreByAOID(AssessmentOID, lbtnID.Text.ToString());
        GridViewSection.DataBind();

    }

    protected void GridViewSection_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
          
        }
        catch (Exception ex)
        { }
    }

    private void PopulateSectionNoScore(string strname, string  StudentID)
    {
        int AssessmentOID=0;
        Sections section = new Sections();
        AssessmentOID = section.GetAssessmentOIDByAssessmentName(strname);
        GridViewScore.DataSource = section.GetSectionsWithNoScore(AssessmentOID, StudentID);
        GridViewScore.DataBind();

    }
     
    private void Initialization()
    {
        try
        {
            if (Session["CurrentUser"] != null)
            {
                User u = (User)Session["CurrentUser"];
                LabelUserName.Text = u != null ? u.LastName + ", " + u.FirstName : "";
                LabelUserName.Text += "[" + DateTime.Now.ToString() + "]";
                HiddenFieldCurrentUser.Value = u.UserOID.ToString();
            }
        }
        catch (Exception ex)
        { }
    }

    public void GenerateCalendar()
    {

        try
        {
            ///System.DateTime.Now.
            DateTime curDate = DateTime.Now;
            int dayIndex = 0;
            int min;
            string lbl="";
            Label cntl;

            string day=curDate.DayOfWeek.ToString();

            dayIndex = (day == "Monday") ? 1 : dayIndex;
            dayIndex = (day == "Tuesday") ? 2 : dayIndex;
            dayIndex = (day == "Wednesday") ? 3 : dayIndex;
            dayIndex = (day == "Thursday") ? 4 : dayIndex;
            dayIndex = (day == "Friday") ? 5 : dayIndex;
            dayIndex = (day == "Saturday") ? 6 : dayIndex;
            dayIndex = (day == "Sunday") ? 7 : dayIndex;
            

            if (curDate.Day <= 14)
            {
                min = 1;                                
            }
            else if (curDate.Day <= 28)
            {
                min = 15;
            }
            else
            {
                min = 29;
            }

            lbl = "Label" + dayIndex.ToString();
            cntl = (Label)Page.FindControl(lbl);
            cntl.Text = curDate.Day.ToString();            
            //cntl.BackColor = System.Drawing.Color.Red;

            if (dayIndex == 1) lbl1.Attributes.Add("class", "selected");
            else if (dayIndex == 2) lbl2.Attributes.Add("class", "selected");
            else if (dayIndex == 3) lbl3.Attributes.Add("class", "selected");
            else if (dayIndex == 4) lbl4.Attributes.Add("class", "selected");
            else if (dayIndex == 5) lbl5.Attributes.Add("class", "selected");
            else if (dayIndex == 6) lbl6.Attributes.Add("class", "selected");
            else if (dayIndex == 7) lbl7.Attributes.Add("class", "selected");
            else if (dayIndex == 8) lbl8.Attributes.Add("class", "selected");
            else if (dayIndex == 9) lbl9.Attributes.Add("class", "selected");
            else if (dayIndex == 10) lbl10.Attributes.Add("class", "selected");
            else if (dayIndex == 11) lbl11.Attributes.Add("class", "selected");
            else if (dayIndex == 12) lbl12.Attributes.Add("class", "selected");
            else if (dayIndex == 13) lbl13.Attributes.Add("class", "selected");
            else if (dayIndex == 14) lbl14.Attributes.Add("class", "selected");
            


            min = curDate.Day;

            for (int i = dayIndex-1; i >0; i--)
            {
                min--;
                min = (min) % (System.DateTime.DaysInMonth(curDate.Year, curDate.Month) + 1);
                if (min == 0) min = 1;
                lbl = "Label" + i.ToString();
                cntl = (Label)Page.FindControl(lbl);
                cntl.Text = min.ToString();
                
                
            }
            min = curDate.Day;
            for (int i = dayIndex + 1; i <=14; i++)
            {
                min++;
                min = (min) % (System.DateTime.DaysInMonth(curDate.Year, curDate.Month) + 1);
                if (min == 0) min = 1;
                lbl = "Label" + i.ToString();
                cntl = (Label)Page.FindControl(lbl);
                cntl.Text = min.ToString();

            }

            //HyperLink lb = new HyperLink();
            //lb.ID = "lb2";
            //lb.Text = "testing";
            //lb.NavigateUrl = @"pg/task/DisplayTask.aspx?taskOID=5&keepThis=true&TB_iframe=true&height=460&width=600";
            //lb.CssClass = "thickbox";
            //lb.BorderStyle = BorderStyle.Solid;
            //PlaceHolder2.Controls.Add(lb);
            //PlaceHolder2.Controls.Add(InsertLineBreaks(1));
            //lb = new HyperLink();
            //lb.ID = "lb2";
            //lb.Text = "Inter";
            //lb.BorderStyle = BorderStyle.Solid;
            //lb.NavigateUrl = @"pg/task/DisplayTask.aspx?taskOID=5&keepThis=true&TB_iframe=true&height=460&width=600";
            //lb.CssClass = "thickbox";
            //PlaceHolder2.Controls.Add(lb);
             
        }
        catch (Exception ex)
        { }
    }

    private void GenerateTaskOnCalendar()
    {
        try
        {
            Collection<Task> _taskList = null;
            Task task = new Task();
            string lbl = "";
            DateTime curDate = DateTime.Now;
            DateTime start = DateTime.Now.Date;
            DateTime end = DateTime.Now.Date.AddDays(1).AddMilliseconds(-1);
            PlaceHolder cntl;
            HyperLink hplk;
            int dayIndex = 0,min,userOID=0;
            string day = curDate.DayOfWeek.ToString();
            User currentUser; 

            if (Session["CurrentUser"] != null)
            {
                currentUser = (User)Session["CurrentUser"];
                userOID = currentUser.UserOID;
            }


            dayIndex = (day == "Monday") ? 1 : dayIndex;
            dayIndex = (day == "Tuesday") ? 2 : dayIndex;
            dayIndex = (day == "Wednesday") ? 3 : dayIndex;
            dayIndex = (day == "Thursday") ? 4 : dayIndex;
            dayIndex = (day == "Friday") ? 5 : dayIndex;
            dayIndex = (day == "Saturday") ? 6 : dayIndex;
            dayIndex = (day == "Sunday") ? 7 : dayIndex;

            //For Current Date
            lbl = "PlaceHolder" + dayIndex.ToString();
            cntl = (PlaceHolder)Page.FindControl(lbl);
            _taskList = null;
            _taskList = task.GetTaskByStartAndEndDateUOID(start, end, userOID);
            //For Each task on this date
            foreach (Task t in _taskList)
            {
                hplk = new HyperLink();
                hplk.ID = "hplk" + t.TaskOID.ToString() + "_" + dayIndex.ToString();
                hplk.Text = t.Subject;
                hplk.NavigateUrl = @"pg/task/DisplayTask.aspx?taskOID=" + t.TaskOID.ToString() + "&keepThis=true&TB_iframe=true&height=460&width=600";
                hplk.CssClass = "thickbox";                
                cntl.Controls.Add(hplk);
                cntl.Controls.Add(InsertLineBreaks(1));
                
            }


            min = curDate.Day;
            for (int i = dayIndex - 1; i > 0; i--)
            {
                //Add Task
                min--;
                min = (min) % (System.DateTime.DaysInMonth(curDate.Year, curDate.Month) + 1);
                if (min == 0) min = 1;
                lbl = "PlaceHolder" + i.ToString();
                cntl = (PlaceHolder)Page.FindControl(lbl);

                start = start.AddDays(-1);
                end = end.AddDays(-1);
                _taskList = null;
                _taskList = task.GetTaskByStartAndEndDateUOID(start, end, userOID);
                
                //For Each task on this date
                foreach (Task t in _taskList)
                {
                    hplk = new HyperLink();
                    hplk.ID = "hplk" + t.TaskOID.ToString()+"_"+i.ToString(); 
                    hplk.Text = t.Subject;
                    hplk.NavigateUrl = @"pg/task/DisplayTask.aspx?taskOID=" + t.TaskOID.ToString() + "&keepThis=true&TB_iframe=true&height=460&width=600";
                    hplk.CssClass = "thickbox";                    
                    cntl.Controls.Add(hplk);
                    cntl.Controls.Add(InsertLineBreaks(1));
                    
                }
            }

            min = curDate.Day;
            start = DateTime.Now.Date;
            end = DateTime.Now.Date.AddDays(1).AddMilliseconds(-1);
            for (int i = dayIndex + 1; i <= 14; i++)
            { 
                //Add task
                min++;
                min = (min) % (System.DateTime.DaysInMonth(curDate.Year, curDate.Month) + 1);
                if (min == 0) min = 1;
                lbl = "PlaceHolder" + i.ToString();
                cntl = (PlaceHolder)Page.FindControl(lbl);

                _taskList = null;
                start = start.AddDays(1);
                end = end.AddDays(1);
                _taskList = task.GetTaskByStartAndEndDateUOID(start, end, userOID);
               
                //For Each task on this date
                foreach (Task t in _taskList)
                {
                    hplk = new HyperLink();
                    hplk.ID = "hplk" + t.TaskOID.ToString() + "_" + i.ToString();
                    hplk.Text = t.Subject;
                    hplk.NavigateUrl = @"pg/task/DisplayTask.aspx?taskOID=" + t.TaskOID.ToString() + "&keepThis=true&TB_iframe=true&height=460&width=600";
                    hplk.CssClass = "thickbox";                    
                    cntl.Controls.Add(hplk);
                    cntl.Controls.Add(InsertLineBreaks(1));
                }
            }
        }
        catch (Exception ex){ 
        
        }
        
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //Response.Redirect("hello.aspx");
    }

    private static Label InsertLineBreaks(int breaks)
    {
        Label lblLineBreak = new Label();

        for (int i = 0; i < breaks; i++)
        {
            lblLineBreak.Text += "<br/>";
        }
        return lblLineBreak;
    }

    protected void ButtonRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            GenerateCalendar();
            GenerateTaskOnCalendar();
            //lbl5.Attributes.Add("class", "selected");
            Initialization();
            populateDropdownList();
            
        }
        catch (Exception ex)
        { }
    }

    protected void GridViewScore_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlAssesment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Student stu = new Student();

            //int stdOID = stu.GetStudentOIDByStudentBannerID(lbtnID.Text.ToString());
            int stuOID = stu.GetStudentOIDByStudentBannerID(lbtnID.Text.ToString());
          
            string strSectionName = null;
            strSectionName = ddlAssesment.SelectedItem.ToString();

            PopulateSection(strSectionName, lbtnID.Text.ToString());
            PopulateSectionNoScore(ddlAssesment.Text.ToString(), lbtnID.Text.ToString());
                       
        }
        catch (Exception ex)
        { }
    }
    
    protected void btnNext_Click(object sender, EventArgs e)
    {
        int stdOID = 0;
        lbtnPrev.Enabled = true;

        Student stu = new Student();
        
        //stdOID = Next();
        int rowid=Convert .ToInt32 ( ViewState ["RowID"]);
        rowid++;
        ViewState["RowID"] = rowid;
        string strID = stu.GetStudentByRowID(rowid);
        stdOID = stu.GetStudentOIDByRowID(rowid);
        //if (ViewState["StuID"] == stdOID.ToString ())
        //{
        //    rowid++;
        //}
        //ViewState["StuID"] = stdOID.ToString();
        //string strID = stu.GetStudentIDByStudentBannerOID(stdOID);

        if (strID == "" || strID == null)
        {
            lbtnNext.Enabled = false;
        }
        else
        {
            Populate(strID);
            populateDropdownList();
            PopulateSectionNoScore(ddlAssesment.Text.ToString(), strID);
            PopulateSection(ddlAssesment.Text.ToString(), strID);
        }
    }

    #region Unused code for Next and Previous
    private int Next()
    {
        int i = 1;
        string strID=null ;
        Student stu = new Student();
        //if (lblDate.Text != "")
        //{
        //    i = stu.GetNextStudentOID(Convert.ToDateTime(lblDate.Text.ToString()));
        //}
        i = stu.GetStudentOIDByStudentBannerID(lbtnID.Text.ToString());
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
    
    private int Prev()
    {
        int i = 0;
        Student stu = new Student();
        //if (lblDate.Text != "")
        //{
        //    i = stu.GetPrevStudentOID(Convert.ToDateTime(lblDate.Text.ToString()));
        //}
        i = stu.GetStudentOIDByStudentBannerID(lbtnID.Text.ToString());
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
    #endregion


    protected void lbtnPrev_Click(object sender, EventArgs e)
    {

        int OID = 0;
        Student stu = new Student();
        int rowid = Convert.ToInt32(ViewState["RowID"]);
        rowid--;
        ViewState["RowID"] = rowid;
        string strID = stu.GetStudentByRowID(rowid);
        OID = stu.GetStudentOIDByRowID(rowid);
        //if (ViewState["StuID"] == OID.ToString())
        //{
        //    rowid--;
        //}
        //ViewState["StuID"] = OID.ToString();
       // Session["StudentID"] = lbtnID.Text.ToString();
        lbtnNext.Enabled = true;
        
        //OID = Prev();
        //string strID = stu.GetStudentIDByStudentBannerOID(OID);
        if (strID == "" || strID == null)
        {
            lbtnPrev.Enabled = false;

        }
        else
        {
            Populate(strID);
            populateDropdownList();
            PopulateSectionNoScore(ddlAssesment.Text.ToString(), strID);
            PopulateSection(ddlAssesment.Text.ToString(), strID);
        }
    }


    
}
