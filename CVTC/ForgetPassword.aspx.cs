using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing;
using System.Data.Odbc;
using System.Net.Mail;

public partial class ForgetPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblStatus.Text = "";
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
        string fromEmail = ConfigurationManager.AppSettings["fromEmail"].ToString();
        string smtpServer = ConfigurationManager.AppSettings["smtpServer"].ToString();
        string displayName = ConfigurationManager.AppSettings["displayName"].ToString();
                
        string  result =GetUserIDByUserEmail(txtEmail.Text .ToString ());
        if (result != ""&&result !=null)
        {
            
          bool MailSent=  SendMail(txtEmail.Text, "Your CVTC UserName And Password", "Your UserName: "+result);
          if (MailSent)
          {
              lblStatus.Text = "Your Username and Password has been sent to " + txtEmail.Text + " Check your email";
              lblStatus.ForeColor = Color.Black;
          }
          else
          {
              lblStatus.Text = "Eamil Not Sent  .. Please contact with admin ";
              lblStatus.ForeColor = Color.Red ;
          }
        }
        else
        {
            lblStatus.Text = "You are Not Valid User!!";
            lblStatus.ForeColor = Color.Red;
        }
    }



    public string  GetUserIDByUserEmail(string Email)
    {
        //Collection<Student> studentList = new Collection<Student>();
        string connectionString;
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();
        string UserName = null;
        string Password = null;
        string UserNamePassword = null;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL GetUserNameAndPasswordByEmail(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@Email", Email);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    //User user = null;
                    if (dataReader.Read())
                    {
                        //student = new Student();
                        //user = new User();
                       UserName = Convert.ToString(dataReader["UserName"]);
                       Password = Convert.ToString(dataReader["Password"]);
                       UserNamePassword = UserName + "   and   Password:   " + Password;
                        // studentList.Add(student);

                    }
                }
                return UserNamePassword;

            }
        }

    }

    private bool SendMail(string toAddress, string subject, string body)
    {
        string fromEmail = ConfigurationManager.AppSettings["fromEmail"].ToString();
        string smtpServer = ConfigurationManager.AppSettings["smtpServer"].ToString();
        string displayName = ConfigurationManager.AppSettings["displayName"].ToString();

        try
        {
            using (var message = new MailMessage())
            {
                message.From = new MailAddress(fromEmail, displayName);
                message.To.Add(toAddress);
                message.Subject = subject;
                message.Body = body;
                var client = new SmtpClient(smtpServer);
                client.Send(message);
            }
            return true;
        }
        catch (Exception ex)
        {
            lblStatus.Text = "Eamil Not Sent  .. Please contact with admin ";
            return false;
        }
        
    }

}
