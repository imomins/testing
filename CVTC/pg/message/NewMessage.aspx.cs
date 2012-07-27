using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Collections.ObjectModel;

public partial class pg_message_NewMessage : System.Web.UI.Page
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
                HiddenFieldCurrentUser.Value = u.UserOID.ToString() ;
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
            CheckBoxListUser.DataSource = u.GetAllUser();
            CheckBoxListUser.DataValueField = "UserOID";
            CheckBoxListUser.DataTextField = "LastName";
            CheckBoxListUser.DataBind();
        }
        catch { }
    }
    [WebMethod]
    public static string InsertMessage(string Recipient, string Mark,string BoyContent, string Subject,string from)//string Recipient, string Subject, string Status, string Mark, string BoyContent)
    {
        try
        {
            //User u = new User();
            //Collection<User> list = u.GetAllUser();
            
            MessageCenter messCenter = new MessageCenter();
            messCenter.Mark = (Mark=="Mark")?1:0;//Convert.ToInt32(Mark);
            messCenter.MessageBody = BoyContent;
            messCenter.Recipient = Recipient;
            messCenter.Subject = Subject;
            messCenter.SendFrom = Convert.ToInt32(from);
            messCenter.Status = "Unread";//Status;

            messCenter.AddMessage();
        }
        catch (Exception ex)
        {
            return "false";
        }
        return "true";
    }
    [WebMethod]
    public static string temp(string Recipient, string BoyContent, string Subject)//string Recipient ,string BoyContent,string Subject
    {
        try
        {
            User u = new User();
            Collection<User> list = u.GetAllUser();
        }
        catch (Exception ex)
        {
            return "false";
        }
        return "true";
    }

    [WebMethod]
    public static string ServerSideMethod()
    {
        User u = new User();
        Collection<User> list = u.GetAllUser();

        return "Message from server.";
    }
}
