using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pg_message_DisplayMessage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["mid"] != null)
        {
            
            string OID = Request.QueryString["mid"].ToString();
            //TextBox1.Text = tmp;
            Populate(Convert.ToInt32(OID));
        }
        if (Request.QueryString["muoid"] != null)
        {
            string MUOID = Request.QueryString["muoid"].ToString();
            MessageCenter messageCenter = new MessageCenter();
            messageCenter.UpdateMessageUserUStatus(Convert.ToInt32(MUOID), "Read");
        }
    }

    private void Populate(int OID)
    {
        try
        {
            MessageCenter m = new MessageCenter();
            m = m.GetMessageByOID(OID);
            if (m != null)
            {
                LabelDate.Text = m.CreatedDate.ToShortDateString();
                LabelFrom.Text = "";//m.SendFrom;
                LabelMessage.Text = m.MessageBody;//System.Text.RegularExpressions.Regex.Replace(m.MessageBody, "<[^>]*>", string.Empty); ;
                LabelSubject.Text = m.Subject;
                LabelTime.Text = m.CreatedDate.ToShortTimeString();
                User u = new User();
                u=u.GetUserByOID(m.SendFrom);
                if(u!=null)
                {
                    LabelFrom.Text = u.LastName + ", " + u.FirstName;
                }
            }
        }
        catch (Exception ex)
        { }
    }
}
