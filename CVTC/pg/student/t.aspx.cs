using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pg_student_t : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.ContentType = "application/xml";
        //Response.Write("<?xml version=\"1.0\"?>");
        //Response.Write("rows>  <row>        <cell>0</cell>    <cell>Car</cell>    <cell>Car</cell>    <cell>0</cell>    <cell></cell>    <cell>false</cell>    <cell>false</cell>  </row> <row>    <cell>1</cell>    <cell>Honda</cell>    <cell>Honda</cell>    <cell>1</cell>    <cell>0</cell>    <cell>false</cell>    <cell>false</cell>  </row></rows>");
        ////Response.End();

        AddControl();

    }

    private void AddControl()
    {
        TextBox txt1 = new TextBox();
        txt1.ID = "TextName";
        txt1.Text = "Hello";
        PlaceHolder1.Controls.Add(txt1);
        Button btn = new Button();
        btn.ID = "ButtonOKay";
        btn.Text = "OKay";
        btn.Click += new EventHandler(ButtonOKay_Click);
        PlaceHolder1.Controls.Add(btn);

        PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
        PlaceHolder1.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"));
        PlaceHolder1.Controls.Add(new LiteralControl("&nbsp;"));
        PlaceHolder1.Controls.Add(new LiteralControl("&nbsp;"));

        //CheckBoxList chkList = new CheckBoxList();
        //chkList.Items.Add("S/W");
        //chkList.Items.Add("TELCOM");
        //chkList.Items.Add("NON IT");
        //PlaceHolder1.Controls.Add(chkList);
        //DropDownList ddl = new DropDownList();
        //ddl.Items.Add("0");
        //ddl.Items.Add("1");
        //ddl.Items.Add("2");
        //ddl.Items.Add("3");
        //ddl.Items.Add("4");
        //PlaceHolder1.Controls.Add(ddl);

        RadioButtonList rbList = new RadioButtonList();
        rbList.ID="rbList1";
        rbList.Items.Add("S/W");
        rbList.Items.Add("IT");
        rbList.Items.Add("Banking");

        PlaceHolder1.Controls.Add(rbList);
    }

    
    protected void ButtonOKay_Click(object sender, EventArgs e)
    {
        Response.Write("Bangladesh");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       RadioButtonList r= (RadioButtonList)PlaceHolder1.FindControl("rbList1");
       for (int i = 0; i < r.Items.Count; i++)
       {
           if (r.Items[i].Selected)
           {
               Response.Write(r.Items[i].Text);
               break;
           }
       }
    }
}
