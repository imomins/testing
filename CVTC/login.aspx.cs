using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string[] cookie = null;
        bool isCookieUser= false;
        bool isCookiepwd = false;
        //if (Request.QueryString["ReturnUrl"] == "%2fcvtc")
        //{
        //    Response.Redirect("Default.aspx");
        //}
        if (!Page.IsPostBack)
        {
            //Session.Clear();

            cookie = Request.Cookies.AllKeys;
            for (int i = 0; i < cookie.Length;i++ )
            {
                if (cookie[i] == "UName")
                {
                    isCookieUser = true;
                }
                
                if (cookie[i] == "PWD")
                {
                    isCookiepwd = true;
                }
               
            }


            if (isCookiepwd && isCookieUser)
            {


                if (Request.Cookies["UName"] != null)
                {
                    TextBoxUserName.Text = Request.Cookies["UName"].Value;
                }

                if (Request.Cookies["PWD"] != null)
                {
                    //TextBoxPassword.Text = Request.Cookies["PWD"].Value;
                    TextBoxPassword.Attributes.Add("value", Request.Cookies["PWD"].Value);
                }

                //TextBoxPassword.Text.Attributes.Add("value", Request.Cookies["PWD"].Value);
                if (Request.Cookies["UName"] != null && Request.Cookies["PWD"] != null)
                {
                    chkRememberMe.Checked = true;
                }
                else
                {
                    chkRememberMe.Checked = false;
                }
            }
            //else
            //{
            //    HttpCookie cookieName = new HttpCookie("UName");
            //    cookieName["UName"] = null;
            //    Response.Cookies.Add(cookieName);
            //    HttpCookie cookiepwd = new HttpCookie("PWD");
            //    cookiepwd["PWD"] = null;
            //    Response.Cookies.Add(cookiepwd);

            //    ////TextBoxPassword.Text.Attributes.Add("value", Request.Cookies["PWD"].Value);
            //    if (Request.Cookies["UName"] != null && Request.Cookies["PWD"] != null)
            //    {
            //        chkRememberMe.Checked = true;
            //    }
            //    else
            //    {
            //        chkRememberMe.Checked = false ;
            //    }
            //}
        }

        
        
    }

    private bool Validation()
    {
        bool result = false;
        if (TextBoxPassword.Text == "" || TextBoxUserName.Text == "")
        {
            result = false;
        }
        else
        {
            result = true;
        }
        return result;
    }
    
    protected void ButtonLogin_Click(object sender, EventArgs e)
    {
        User user = new User();
        if (Validation())
        {
            
            string freez = "Yes";
            freez = user.GetUserFreezByUserName(TextBoxUserName.Text);
            user = user.GetUserByNameAndPassword(TextBoxUserName.Text, TextBoxPassword.Text);
            #region Check User
            if (user != null && freez != "No")
            {
                if (TextBoxPassword.Text == user.Password && TextBoxUserName.Text == user.UserName)
                {
                    
                    if (chkRememberMe.Checked == true)
                    {
                        Response.Cookies["UName"].Value = TextBoxUserName.Text;
                        Response.Cookies["PWD"].Value = TextBoxPassword.Text;
                        Response.Cookies["UName"].Expires = DateTime.Now.AddMonths(2);
                        Response.Cookies["PWD"].Expires = DateTime.Now.AddMonths(2);
                    }
                    else
                    {
                        Response.Cookies["UName"].Expires = DateTime.Now.AddMonths(-1);
                        Response.Cookies["PWD"].Expires = DateTime.Now.AddMonths(-1);
                    }
                    Session.Clear();
                    Session["CurrentUser"] = user;
                    Session.Timeout = 600;
                    FormsAuthentication.RedirectFromLoginPage(this.TextBoxUserName.Text, false);
                    Response.Redirect("Default.aspx");
                }
                else 
                {
                    LabelStatus.Text = "Login Fail";
                }
            }
            #endregion
            else if (user == null)
            {
               LabelStatus.Text = "Login Fail";
            }
            else if (freez == "No")
            {
                LabelStatus.Text = "User has been Freezed";
            }
            else
            {
                LabelStatus.Text = "Login Fail";
            }
                       
        }
        else
        {
            LabelStatus.Text = "Login Fail";
        }
        
    }


    protected void ButtonForget_Click(object sender, EventArgs e)
    {
        Response.Redirect("ForgetPassword.aspx");
    }
}
