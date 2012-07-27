using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections.ObjectModel;
using System.Drawing;

public partial class pg_intervention_AddDomainIntervention : System.Web.UI.Page
{
    private string studentid = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.ForeColor = Color.Black;
        Label1.Text = "";
        txtIntervention.Focus();
        //if (!Page.IsPostBack)
        //{
           

        //    try
        //    {
                
        //    }
        //    catch (Exception ex)
        //    { }
        //}
    }

  

    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Interventions interventions = new Interventions();
           // Domain domain = new Domain();
                    
          //int  strInterventionOID = interventions.GetInterventionOIDByInterventionName(DropDownListIntervention .SelectedItem .ToString ());
           // interventions.DomainName = txtIntervention.Text;
            if (txtIntervention.Text != null && txtIntervention .Text!="")
            {
                interventions.AddDomain(txtIntervention .Text);
                Label1.Text = "Successfuly Saved";
                Response.Redirect("AddDomainIntervention.aspx");
            }
            else
            {
                Label1.ForeColor = Color.Red;
                Label1.Text = "Not Saved  !! Please Enter Domain Name ";
                txtIntervention.Focus();
            }
        }
            

        catch (Exception ex)
        { 
        
        }
    }

    protected void DropDownListDomain_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Interventions iv;
        //if (DropDownListDomain.Text != null)
        //{
        //    iv = new Interventions();
        //    Collection<Interventions> inList = new Collection<Interventions>();
        //    inList = iv.GetInterventionByDomainName(DropDownListDomain.Text);
        //    DropDownListIntervention.Items.Clear();
        //    foreach (Interventions inter in inList)
        //    {
        //        DropDownListIntervention.Items.Add(inter.ToString());
        //    }
        //}
    }
    protected void DropDownListDomain_Load(object sender, EventArgs e)
    {

    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        Label1.ForeColor = Color.Black ;
        txtIntervention.Focus();

    }
}
