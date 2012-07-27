using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Net.Mail;
using System.Net;
using System.Configuration;



public partial class SendEmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        lblStatus.ForeColor = Color.Blue;
        Label1.Text = "";
    }
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        bool res = false;
        string toEmail = ConfigurationManager.AppSettings["toEmail"].ToString();
        string fromEmail= ConfigurationManager.AppSettings["fromEmail"].ToString();

        string smtpServer = ConfigurationManager.AppSettings["smtpServer"].ToString();
        string Subject = ConfigurationManager.AppSettings["Subjectheader"].ToString();
        string displayName = ConfigurationManager.AppSettings["displayName"].ToString();
        string Body = ConfigurationManager.AppSettings["body"].ToString();
        //string isHTML = ConfigurationManager.AppSettings["isHTML"].ToString();
        bool  isHTML = true ;
        //res = this.SendMail(toEmail, "Test Email(www.iss.cvtc.edu/cvtc)", "Test Email Body(www.iss.cvtc.edu/cvtc)");
        res = this.SendMessage(smtpServer, fromEmail, displayName, toEmail, Subject, Body, isHTML);
        if (res)
        {
            lblStatus.Text = "Email has been Sent Successfully";
            lblStatus.ForeColor = Color.Blue;
        }
        else
        {
            lblStatus.Text = "Error Occured!! Email Not Sent";
            lblStatus.ForeColor = Color.Red;
        }
    }

    private bool SendMail_(string toAddress, string subject, string body)
    {
        try
        {
            MailMessage mail = new MailMessage();
            //mail.From = new MailAddress("hitauthority@gmail.com");
            mail.From = new MailAddress("studentperformance@cvtc.edu");
            mail.To.Add(toAddress);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Credentials = new System.Net.NetworkCredential ("hitauthority", "hit@authority");
            smtpClient.EnableSsl = bool.Parse("True");
            smtpClient.Port = Int32.Parse("587");
            smtpClient.Send(mail);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private bool SendMail(string toAddress, string subject, string body)
    {
        
       try
        {
            string fromEmail = ConfigurationManager.AppSettings["fromEmail"].ToString();
            MailMessage message = new MailMessage(fromEmail, toAddress);
            message.Subject = subject;//subject of email
            message.IsBodyHtml = true ;//To determine email body is html or not
            message.Body = body;
           
            SmtpClient smtp = new SmtpClient();
            //smtp.EnableSsl = true;
            smtp.Send(message);
            return true;
        }
        catch(Exception ax)
        {
            Label1.Text = ax.ToString();
            return false;
            
        }
    }


    private bool SendMessage(string smtpMailServer, string eMailFromAddress, string fromDisplayName, string eMailAddressTo, string subjectHeader, string messageBody, bool isHTML)
         
    {
        try
        {
            using (var message = new MailMessage())
            {
                message.From = new MailAddress(eMailFromAddress, fromDisplayName);
                message.To.Add(eMailAddressTo);
                message.Subject = subjectHeader;
                message.IsBodyHtml = isHTML;
                message.Body = messageBody;
                var client = new SmtpClient(smtpMailServer);
                client.Send(message);
            }
            return true;
        }
        catch (Exception ex)
        {
            lblStatus.Text = "Email Not Sent Erorr::  "+ex.ToString();
            return false;
        }
    }
}
