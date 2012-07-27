using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pg_task_DisplayTask : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //
            if (Request.QueryString["taskUOID"] != null)
            {
                string taskUOID = Request.QueryString["taskUOID"].ToString();
                Task task = new Task();
                task.UpdateTaskUserUStatus(Convert.ToInt32(taskUOID), "Read");
                
            }
            if (Request.QueryString["taskOID"] != null)
            {
                string OID = Request.QueryString["taskOID"].ToString();
                Populate(Convert.ToInt32(OID));
            }
        }
    }

    private void Populate(int taskOID)
    {

        try
        {
            Task task = new Task();
            task = task.GetTaskByOID(taskOID);

            if (task != null)
            {
                TextBoxMessage.Text = System.Text.RegularExpressions.Regex.Replace(task.MessageBody, "<[^>]*>", string.Empty); 
                LabelPriority.Text = task.Priority.ToString();
                LabelTittle.Text = task.Subject;
                LabelTo.Text = task.Recipient;
                LabelCompletionDateTime.Text = task.CompletionDate.ToString();
            }
        }
        catch (Exception ex)
        { }
    }
    protected void TextBoxMessage_TextChanged(object sender, EventArgs e)
    {

    }
}

