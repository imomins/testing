using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pg_TaskCompleted : System.Web.UI.Page
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

            GridViewTask.DataSource = task.GetAllTaskRecipientCompletedByUserOID(OID);//mess.GetMessages(numberOfRows, pageIndex, sortColumnName, sortOrderBy, out totalRecords);
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
                string taskUserOID = hiddenField.Value;
                Task task = new Task();
                if (task.DeleteTaskUserByOID(Convert.ToInt32(taskUserOID))) status = true;
            }
        }
        if (status)
        {
            PopulateGridview();
        }
    }
    protected void DropDownListMark_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool status = false;
        foreach (GridViewRow row in GridViewTask.Rows)
        {
            CheckBox checkBox = (CheckBox)row.Cells[0].FindControl("CheckBoxTask");
            if (checkBox.Checked)
            {
                HiddenField hiddenField = (HiddenField)row.Cells[0].FindControl("HiddenFieldTask");
                string taskUserOID = hiddenField.Value;
                Task task = new Task();
                if ((DropDownListMark.SelectedItem.Text == "Mark") || (DropDownListMark.SelectedItem.Text == "Star"))
                {
                    if (task.UpdateTaskUserUMark(Convert.ToInt32(taskUserOID), 1)) status = true;
                }
                else
                {
                    if (task.UpdateTaskUserUStatus(Convert.ToInt32(taskUserOID), DropDownListMark.SelectedItem.Text)) status = true;
                }

            }
        }
        if (status)
        {
            PopulateGridview();
        }



    }

}
