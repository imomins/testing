using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pg_message_MessageManagement : System.Web.UI.Page
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
            int OID = (u!=null)?u.UserOID:0;
            
            MessageCenter mess = new MessageCenter();
            GridViewMessageBox.DataSource = mess.GetMessageByUser(OID);//mess.GetMessages(numberOfRows, pageIndex, sortColumnName, sortOrderBy, out totalRecords);
            GridViewMessageBox.DataBind();
            
        }
        catch (Exception ex)
        { }
    }
    protected void GridViewMessageBox_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridViewMessageBox_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridViewMessageBox_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridViewMessageBox.PageIndex = e.NewPageIndex;
            PopulateMessageBox();
        }
        catch (Exception ex)
        { 
        
        }
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
                string messageUserOID = hiddenField.Value;
                MessageCenter messageCenter = new MessageCenter();
                if (messageCenter.DeleteMessageUserByOID(Convert.ToInt32(messageUserOID))) status = true;
            }
        }
        if (status)
        {
            PopulateMessageBox();
        }
    }
    protected void DropDownListMark_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool status = false;
        foreach (GridViewRow row in GridViewMessageBox.Rows)
        {
            CheckBox checkBox = (CheckBox)row.Cells[0].FindControl("CheckBoxMessage");
            if (checkBox.Checked)
            {
                HiddenField hiddenField = (HiddenField)row.Cells[0].FindControl("HiddenFieldMessage");
                string messageUserOID = hiddenField.Value;
                MessageCenter messageCenter = new MessageCenter();
                if ((DropDownListMark.SelectedItem.Text == "Mark") || (DropDownListMark.SelectedItem.Text == "Star"))
                {
                    if (messageCenter.UpdateMessageUserUMark(Convert.ToInt32(messageUserOID), 1)) 
                        status = true;
                }
                else
                {
                    if (messageCenter.UpdateMessageUserUStatus(Convert.ToInt32(messageUserOID), DropDownListMark.SelectedItem.Text)) 
                        status = true;
                }
                
            }
        }
        if (status)
        {
            PopulateMessageBox();
        }



    }
}
