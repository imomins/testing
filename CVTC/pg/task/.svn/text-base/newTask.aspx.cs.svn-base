using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.Services;
using System.Collections.ObjectModel;

public partial class pg_task_newTask : System.Web.UI.Page
{
 
    protected void Page_Load(object sender, EventArgs e)
    {
        TextBoxTo.Text = "";
        if (!Page.IsPostBack)
        {
            TextBoxTo.Text = "";
            LabelDate.Text = DateTime.Now.ToShortDateString();
            LabelTime.Text = DateTime.Now.ToShortTimeString();
            PopulateChecklist();
            Initialize();
        }
    }
    private void Initialize()
    {
        try
        {
            if (Session["CurrentUser"] != null)
            {
                User u = (User)Session["CurrentUser"];
                HiddenFieldCurrentUser.Value = u.UserOID.ToString();
            }
        }
        catch (Exception ex)
        { }
    }

    private void PopulateChecklist()
    {
        try
        {
            User u = new User();
            CheckBoxListUser.DataSource=u.GetAllUser();
            CheckBoxListUser.DataValueField = "UserOID";
            CheckBoxListUser.DataTextField = "LastName";
            CheckBoxListUser.DataBind();

            for (int i = 0; i < CheckBoxListUser.Items.Count; i++)
            {
                if (i%2==0)
                {
                    CheckBoxListUser.Items[i].Attributes.Add("style", "background-color: #f1f1f1;");
                }
                else
                {
                    CheckBoxListUser.Items[i].Attributes.Add("style", "background-color: #fff;");
                }
            }

        }
        catch { }
    }
    //protected void CheckBoxListUser_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    TextBoxTo.Text = "";
    //    for (int i = 0; i < CheckBoxListUser.Items.Count; i++)
    //    {
    //        if (CheckBoxListUser.Items[i].Selected)
    //        {
    //            TextBoxTo.Text += CheckBoxListUser.Items[i].Text;
    //        }
    //    }
    //}

    [System.Web.Services.WebMethod]
    public static string InsertTask(string Recipient,  string BoyContent, string Subject, string from,int priority,string completedDate, string status, string mark)//string Recipient, string Subject, string Status, string Mark, string BoyContent)
    {
        try
        {
            //User u = new User();
            //Collection<User> list = u.GetAllUser();
            Task task = new Task();
            task.CompletionDate = Convert.ToDateTime(completedDate);
            task.MessageBody = BoyContent;
            task.Recipient = Recipient;
            task.Subject = Subject;
            task.SendFrom = from;
            task.Priority = priority;//status;

            task.Status = status;
            task.Mark = (mark=="Mark")?1:0;
            
            task.AddTask();
        }
        catch (Exception ex)
        {
            return "false";
        }
        return "true";
    }

    [System.Web.Services.WebMethod]
    public static ArrayList GetGenders()
    {
        return new ArrayList()
    {
        new { Value = 1, Display = "Male" },
        new { Value = 2, Display = "Female" }
    };
    }
    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        TextBoxTo.Text = "SSTL";
    }
    
    

    [WebMethod]
    public static string ServerSideMethod()
    {
        User u = new User();
        Collection<User> list = u.GetAllUser();        
        return "Message from server.";
    }
}
