﻿using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Net.Mail;


/// <summary>
/// Summary description for MailHelper
/// </summary>
public class MailHelper
{
    
    private MailHelper()
    {
        
    }

    public static bool SendMail(string form, string to, string subject, string body)
    {

        bool valid = true;
        MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(form);
        mailMessage.To.Add(to);
        mailMessage.Subject = subject;
        mailMessage.Body = body;

        SmtpClient smtpClient = new SmtpClient();

        try
        {
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
        }
        catch (Exception ex)
        {
            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception e)
            {
                valid = false;
                throw new Exception(e.Message, e.InnerException);
            }

            valid = false;

        }

        return valid;
    }

    public static bool SendMail(string form, List<string> toAddressList, string subject, string body)
    {

        bool valid = true;
        MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(form);
        foreach (string toAddress in toAddressList)
        {
            mailMessage.To.Add(toAddress);
        }
        mailMessage.Subject = subject;
        mailMessage.Body = body;

        SmtpClient smtpClient = new SmtpClient();

        try
        {
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
        }
        catch (Exception ex)
        {
            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception e)
            {
                valid = false;
                throw new Exception(e.Message, e.InnerException);
            }

            valid = false;

        }

        return valid;
    }

    public static bool SendMail(string form, string to, string subject, string body, List<Attachment> attachments)
    {

        bool valid = true;

        MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(form);
        mailMessage.To.Add(to);
        mailMessage.Subject = subject;
        mailMessage.Body = body;

        foreach (Attachment attachment in attachments)
        {
            mailMessage.Attachments.Add(attachment);
        }

        SmtpClient smtpClient = new SmtpClient();

        try
        {
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
            smtpClient = null;
        }
        catch (Exception ex)
        {
            try
            {
                smtpClient.Send(mailMessage);
                smtpClient = null;
            }
            catch (Exception e)
            {
                valid = false;
                throw new Exception(e.Message, e.InnerException);
            }

            valid = false;

        }

        return valid;
    }

    public static bool SendMailWithCredential(string from, string to, string subject, string body)
    {
        bool result = false;
        string emailAdress, Password, serverName;
        int SSL, Port;

        emailAdress = "alopotal@gmail.com";
        Password = "alopotal";
        serverName = "smtp.gmail.com";
        SSL = 1;
        Port = 587;

        try
        {
            MailMessage mailMessage = new MailMessage();

            mailMessage.To.Add(to);            
            mailMessage.Subject = subject;
            mailMessage.Body = body;


            SmtpClient smtpClient = new SmtpClient();
            mailMessage.From = new MailAddress(from);
            mailMessage.IsBodyHtml = true;
            smtpClient.EnableSsl = SSL == 1 ? true : false;
            smtpClient.Host = serverName;
            smtpClient.Port = Convert.ToInt32(Port);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential(emailAdress, Password);
            smtpClient.Send(mailMessage);
            smtpClient = null;

            result = true;
        }
        catch (Exception ex)
        {
            result = false;
        }
        return result;
    }

    //Active
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
           
            return false;
        }

    }
}
