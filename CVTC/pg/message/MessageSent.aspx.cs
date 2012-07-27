using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pg_message_MessageSent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PopulateMessageBox();
        }
    }

    private void PopulateMessageBox()
    {
        try
        {
            //int totalRecords;
            //string numberOfRows = "10";
            //string pageIndex = "1";
            //string sortColumnName = "CreatedDate";
            //string sortOrderBy = "desc";
            //Collection<User> users = GetUsers(numberOfRows, pageIndex, sortColumnName, sortOrderBy, out totalRecords);
            User u = (User)Session["CurrentUser"];
            int OID = (u != null) ? u.UserOID : 0;

            MessageCenter mess = new MessageCenter();
            GridViewMessageBox.DataSource = mess.GetSentMessageByUser(OID);//mess.GetMessages(numberOfRows, pageIndex, sortColumnName, sortOrderBy, out totalRecords);
            GridViewMessageBox.DataBind();

        }
        catch (Exception ex)
        { }
    }
    protected void GridViewMessageBox_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridViewMessageBox.PageIndex = e.NewPageIndex;
            PopulateMessageBox();
        }
        catch (Exception ex)
        { }
    }
    protected void ButtonRefresh_Click(object sender, EventArgs e)
    {
        PopulateMessageBox();
    }

    protected void ButtonDelete_Click(object sender, EventArgs e)
    {
        bool status = false;
        foreach (GridViewRow row in GridViewMessageBox.Rows)
        {
            CheckBox checkBox = (CheckBox)row.Cells[0].FindControl("CheckBoxMessage");
            if (checkBox.Checked)
            {
                HiddenField hiddenField = (HiddenField)row.Cells[0].FindControl("HiddenFieldMessage");
                string messageOID = hiddenField.Value;
                MessageCenter messageCenter = new MessageCenter();
                if (messageCenter.UpdateMessageCenterStatus(Convert.ToInt32(messageOID), "Trashed")) status = true;
            }
        }
        if (status)
        {
            PopulateMessageBox();
        }
    }
}
