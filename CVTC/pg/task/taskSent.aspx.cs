using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pg_task_taskSent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PopulateGridview();
        }
    }

    private void PopulateGridview()
    {
        try
        {
            User u = (User)Session["CurrentUser"];
            int OID = (u != null) ? u.UserOID : 0;

            Task task = new Task();

            GridViewTask.DataSource = task.GetTaskByUserOID(OID);//mess.GetMessages(numberOfRows, pageIndex, sortColumnName, sortOrderBy, out totalRecords);
            GridViewTask.DataBind();
        }
        catch (Exception ex)
        { }
    }
    protected void GridViewTask_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridViewTask.PageIndex = e.NewPageIndex;
            PopulateGridview();
        }
        catch (Exception ex)
        { }
    }
    protected void ButtonRefresh_Click(object sender, EventArgs e)
    {
        PopulateGridview();
    }

    protected void ButtonDelete_Click(object sender, EventArgs e)
    {
        bool status = false;
        foreach (GridViewRow row in GridViewTask.Rows)
        {
            CheckBox checkBox = (CheckBox)row.Cells[0].FindControl("CheckBoxTask");
            if (checkBox.Checked)
            {
                HiddenField hiddenField = (HiddenField)row.Cells[0].FindControl("HiddenFieldTask");
                string taskOID = hiddenField.Value;
                MessageCenter messageCenter = new MessageCenter();
                Task task = new Task();
                if (task.UpdateTaskStatus(Convert.ToInt32(taskOID), "Completed")) status = true;
            }
        }
        if (status)
        {
            PopulateGridview();
        }
    }
}
