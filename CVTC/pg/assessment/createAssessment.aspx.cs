using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;

public partial class pg_assessment_createAssessment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            LabelMessage.Text = "";
            lblStatus.Text = "";
            PopulateGrid();
        }
    }

    private void PopulateGrid()
    {
        Assessment As = new Assessment();
        gridAssessment.DataSource = As.GetAllAssessmnet();
        gridAssessment.DataBind();
    
    }

    protected void ButtonCreate_Click(object sender, EventArgs e)
    {
        try
        {
            if (TextBoxAssessment.Text.Length > 0)
            {
                string assName = TextBoxAssessment.Text;
                int menuId = new CVTCMenu().GetMaxMenuID();
                menuId += 1;
                TextBoxAssessment.Text = "";
                Collection<CVTCMenu> menuList = new Collection<CVTCMenu>();
                CVTCMenu menu = null;
                Section section = new Section();

                Assessment ass = new Assessment();
                ass.AssessmentName = assName;
                ass.CreatedBy = 1;
                ass.CreatedDate = DateTime.Now;
                ass.LastModifiedBy = 1;
                ass.LastModifiedDate = DateTime.Now;
                ass.RefMenuID = menuId;
                ass.TotalFlag = 0;
                ass.TotalFlagPoint = 0;
                ass.TotalQuestion = 0;
                ass.TotalSection = 0;
                ass.AddAssessment();

                section.AssessmentOID = ass.AssessmentOID;
                section.SectionName = "NoScore";
                section.TotalQuestion = 0;
                section.TotalFlag = 0;
                section.Low = -1;
                section.Medium = -1;
                section.Flag = -1;
                section.High = -1;
                section.LastModifiedBy = 1;
                section.CreatedBy = 1;
                section.CreatedDate = DateTime.Now;
                section.LastModifiedDate = DateTime.Now;
                section.PassingTotal = -1;
                section.FlagPointTotal = -1;
                section.QuestionList = null;
                section.AddSection();

                #region Assign Value to Menu Items
                for (int i = menuId; i <= (menuId + 10); i++)
                {
                    menu = new CVTCMenu();
                    menu.MenuID = i;
                    if (menu.MenuID == menuId)
                    {
                        menu.NameMenu = assName;
                        menu.URL = " ";
                        menu.MenuLevel = 1;
                        menu.Parent = 24;
                        menu.IsExpanded = "false";
                        menu.IsLeave = "false";
                    }
                    if (menu.MenuID == (menuId + 1))
                    {

                        menu.NameMenu = "View Results";
                        menu.URL = "pg/assessment/Result.aspx?aid=" + ass.AssessmentOID.ToString();
                        menu.MenuLevel = 2;
                        menu.Parent = menuId;
                        menu.IsExpanded = "true";
                        menu.IsLeave = "true";
                    }
                    if (menu.MenuID == (menuId + 2))
                    {
                        menu.NameMenu = "Settings";
                        menu.URL = "pg/assessment/setting.aspx?aid=" + ass.AssessmentOID.ToString();
                        menu.MenuLevel = 2;
                        menu.Parent = menuId;
                        menu.IsExpanded = "true";
                        menu.IsLeave = "true";
                    }
                    if (menu.MenuID == (menuId + 3))
                    {
                        menu.NameMenu = "Reminder Email";
                        menu.URL = " ";
                        menu.MenuLevel = 2;
                        menu.Parent = menuId;
                        menu.IsExpanded = "false";
                        menu.IsLeave = "false";
                    }
                    if (menu.MenuID == (menuId + 4))
                    {
                        menu.NameMenu = "Send Email";
                        menu.URL = "pg/assessment/ReminderEmail.aspx?aid=" + ass.AssessmentOID.ToString();
                        menu.MenuLevel = 3;
                        menu.Parent = (menuId + 3);
                        menu.IsExpanded = "true";
                        menu.IsLeave = "true";
                    }
                    if (menu.MenuID == (menuId + 5))
                    {
                        menu.NameMenu = "Edit Term Codes";
                        menu.URL = "pg/assessment/editTermCodes.aspx?aid=" + ass.AssessmentOID.ToString();
                        menu.MenuLevel = 3;
                        menu.Parent = (menuId + 3);
                        menu.IsExpanded = "true";
                        menu.IsLeave = "true";
                    }
                    if (menu.MenuID == (menuId + 6))
                    {
                        menu.NameMenu = "Results Email";
                        menu.URL = "pg/assessment/ResultEmail.aspx?aid=" + ass.AssessmentOID.ToString();
                        menu.MenuLevel = 2;
                        menu.Parent = menuId;
                        menu.IsExpanded = "true";
                        menu.IsLeave = "true";
                    }
                    if (menu.MenuID == (menuId + 7))
                    {
                        menu.NameMenu = "Results Letter";
                        menu.URL = "pg/assessment/ResultLetter.aspx?aid=" + ass.AssessmentOID.ToString();
                        menu.MenuLevel = 2;
                        menu.Parent = menuId;
                        menu.IsExpanded = "true";
                        menu.IsLeave = "true";
                    }
                    if (menu.MenuID == (menuId + 8))
                    {
                        menu.NameMenu = "Question Groups";
                        menu.URL = " ";
                        menu.MenuLevel = 2;
                        menu.Parent = menuId;
                        menu.IsExpanded = "false";
                        menu.IsLeave = "false";
                    }
                    if (menu.MenuID == (menuId + 9))
                    {
                        menu.NameMenu = "Add";
                        menu.URL = "pg/assessment/SectionEdit.aspx?aid=" + ass.AssessmentOID.ToString();
                        menu.MenuLevel = 3;
                        menu.Parent = (menuId + 8);
                        menu.IsExpanded = "true";
                        menu.IsLeave = "true";
                    }
                    if (menu.MenuID == (menuId + 10))
                    {
                        menu.NameMenu = "No Score";
                        menu.URL = "pg/assessment/NoScore.aspx?soid=" + section.SectionOID.ToString();
                        menu.MenuLevel = 2;
                        menu.Parent = menuId;
                        menu.IsExpanded = "true";
                        menu.IsLeave = "true";
                    }


                    menuList.Add(menu);
                }
                #endregion

                //Save it to database
                foreach (CVTCMenu m in menuList)
                {
                    menu.SaveAssessmentMenuItem(m);
                }
                PopulateGrid();
                LabelMessage.Text = "Saved Successfully.";
            }
            else
            {
                LabelMessage.Text = "Please Enter Assessment Name.";
                TextBoxAssessment.Focus();
                return;
            }



            
            //ass.UpdateAssessmentRef();
        }
        catch (Exception ex)
        { 
        
        }
    }


    protected void gridAssessment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gridAssessment.PageIndex = e.NewPageIndex;
        PopulateGrid();
    }
    protected void gridAssessment_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gridAssessment_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }

    protected void ImageButtonDelete_Click(object sender, ImageClickEventArgs e)
    {
        Assessment ass = new Assessment();  
        GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
        HiddenField HiddenFieldAssessmentID = (HiddenField)row.Cells[0].FindControl("HiddenFieldAssessmentID");
        string OID = HiddenFieldAssessmentID.Value;
        string AssessmentName = null;
        
        if (ass.GetAssessmentStatusByOID(Convert.ToInt32(OID)) == 1)
        {
            PopulateGrid();
            lblStatus.Text = "This Assessment can not be Deleted.It has been Locked";
            return;
        }

        if (OID != null && OID != "")
        {
            ass = ass.GetAssessmentByOID(Convert.ToInt32(OID));
            if (ass != null)
            {
                AssessmentName = ass.AssessmentName;
            }
        }
        if (ass.DisableAssessmentStatus(Convert.ToInt32(OID)))
        {

            
            CVTCMenu menu = new CVTCMenu();
            menu = menu.GetMenuByMenuName(AssessmentName);
            if (menu != null)
            {
                int MenuID = menu.MenuID;
                int parentID = menu.Parent;

                while (MenuID <= (menu.MenuID + 8))
                {
                    menu.DeleteMenuByParent(MenuID);
                    MenuID++;
                }
                menu.DeleteMenuByMenuID(menu.MenuID);
            }
            PopulateGrid();
            lblStatus.Text = "This Assessment has been deleted successfully";
        }
    }

    protected void ImageButtonLock_Click(object sender, ImageClickEventArgs e)
    {
        Assessment ass = new Assessment();
        GridViewRow row = ((ImageButton)sender).Parent.Parent as GridViewRow;
        HiddenField HiddenFieldAssessmentID = (HiddenField)row.Cells[0].FindControl("HiddenFieldAssessmentID");
        string OID = HiddenFieldAssessmentID.Value;
        if (ass.GetAssessmentStatusByOID(Convert.ToInt32(OID)) == 1)
        {
            PopulateGrid();
            lblStatus.Text = "This Assessment can not be Locked.It has already been Locked";
        }
        else
        {
            if (ass.UpdateAssessmentStatus(Convert.ToInt32(OID)))
            {
                PopulateGrid();
                lblStatus.Text = "This Assessment  has been locked Successfully";
            }
            
        }
    }
}
