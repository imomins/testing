using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data.Odbc;

/// <summary>
/// Summary description for MessageCenter
/// </summary>
 [Serializable]
public class MessageCenter
{
     //string connectionString = @"Data Source=.\SQLEXPRESS; Initial Catalog=cvtc; User ID=cvtcuser; Password=cvtcuser";
     string connectionString;
     public MessageCenter()
     {
         connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString(); 
     }
      public int MessageOID
    { get; set; }
   
    public int SendFrom
    { get; set; }
   
    public string Recipient
    { get; set; }
   
    public string Subject
    { get; set; }

    public string Status
    { get; set; }
   
    public int Mark
    { get; set; }

     public string MessageBody
    { get; set; }

       public DateTime CreatedDate
    { get; set; }
       public string MarkURL
       { get; set; }
       public string FlagURL
       { get; set; }
       public int MessageUserOID
       { get; set; }
       public string UStatus
       { get; set; }
       public bool AddMessage()
       {
           bool result = false;

           using (OdbcConnection connection = new OdbcConnection(connectionString))
           {
               using (OdbcCommand command = new OdbcCommand())
               {
                   //command.Connection = connection;
                   ////command.CommandText = "UPDATE dbo.Users SET " + "UserName='" + userName + "', " + " FirstName='" + firstName + "', " + " MiddleName='" + middleName + "'," + "LastName='" + lastName + "'," + "EmailID='" + emailID + "'" + " where UserID='" + userid + "' ";
                   //command.CommandText = "INSERT INTO Users (UserName, FirstName, LastName, MiddleName, EmailID) VALUES (" + "'" + userName + "','" + firstName + "','" + lastName + "','" + middleName + "','" + emailID + "')";
                   //connection.Open();
                   //command.ExecuteNonQuery();

                   command.Connection = connection;
                   command.CommandText = "{CALL MessageCenter_insert(?,?,?,?,?,?)}";
                   command.CommandType = CommandType.StoredProcedure;

                   OdbcParameter paramUserName = new OdbcParameter("@SendFrom", OdbcType.Int);
                   paramUserName.Value = this.SendFrom;
                   command.Parameters.Add(paramUserName);

                   OdbcParameter paramPassword = new OdbcParameter("@Recipient", OdbcType.VarChar, 500);
                   paramPassword.Value = this.Recipient;
                   command.Parameters.Add(paramPassword);

                   OdbcParameter paramLastName = new OdbcParameter("@Subject", OdbcType.VarChar, 500);
                   paramLastName.Value = this.Subject;
                   command.Parameters.Add(paramLastName);

                   OdbcParameter paramFirstName = new OdbcParameter("@Status", OdbcType.VarChar, 50);
                   paramFirstName.Value = this.Status;
                   command.Parameters.Add(paramFirstName);

                   OdbcParameter paramPhone = new OdbcParameter("@Mark", OdbcType.VarChar, 100);
                   paramPhone.Value = this.Mark;
                   command.Parameters.Add(paramPhone);

                   OdbcParameter paramEmail = new OdbcParameter("@MessageBody", OdbcType.VarChar, 1000);
                   paramEmail.Value = this.MessageBody;
                   command.Parameters.Add(paramEmail);


                   connection.Open();
                   int n = command.ExecuteNonQuery();
                   if (n == 1)
                       result = true;
                   else
                       result = false;
               }
           }

           return result;
       }


       public Collection<MessageCenter> GetAllMessage()
       {
           Collection<MessageCenter> messages = new Collection<MessageCenter>();           
           using (OdbcConnection connection = new OdbcConnection(connectionString))
           {
               using (OdbcCommand command = new OdbcCommand())
               {
                   //command.Connection = connection;
                   ////command.CommandText = "UPDATE dbo.Users SET " + "UserName='" + userName + "', " + " FirstName='" + firstName + "', " + " MiddleName='" + middleName + "'," + "LastName='" + lastName + "'," + "EmailID='" + emailID + "'" + " where UserID='" + userid + "' ";
                   //command.CommandText = "INSERT INTO Users (UserName, FirstName, LastName, MiddleName, EmailID) VALUES (" + "'" + userName + "','" + firstName + "','" + lastName + "','" + middleName + "','" + emailID + "')";
                   //connection.Open();
                   //command.ExecuteNonQuery();

                   command.Connection = connection;
                   command.CommandText = "Message_GetAllMessage";
                   command.CommandType = CommandType.StoredProcedure;
                   connection.Open();
                   using (OdbcDataReader dataReader = command.ExecuteReader())
                   {
                       MessageCenter message;
                       while (dataReader.Read())
                       {
                           //MessageOID, SendFrom, Recipient, Subject, Status, Mark, MessageBody, CreatedDate
                           message = new MessageCenter();
                           message.MessageOID = (int)dataReader["MessageOID"];
                           message.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                           //message.Mark = Convert.ToString(dataReader["Mark"]) == "Mark" ? 1 : 0;
                           message.Mark = Convert.ToInt32(dataReader["Mark"]);
                           message.MessageBody = Convert.ToString(dataReader["MessageBody"]);
                           message.Recipient = Convert.ToString(dataReader["Recipient"]);
                           message.SendFrom = Convert.ToInt32(dataReader["SendFrom"]);
                           message.Status = Convert.ToString(dataReader["Status"]);
                           message.Subject = Convert.ToString(dataReader["Subject"]);
                           message.MarkURL = (message.Mark == 1) ? "~/images/StarIconGold.png" : "";
                           messages.Add(message);
                       }
                   }

               }

               return messages;
           }
       }

       public Collection<MessageCenter> GetMessageByUser(int userOID)
       {
           Collection<MessageCenter> messages = new Collection<MessageCenter>();
           using (OdbcConnection connection = new OdbcConnection(connectionString))
           {
               using (OdbcCommand command = new OdbcCommand())
               {
                   //command.Connection = connection;
                   ////command.CommandText = "UPDATE dbo.Users SET " + "UserName='" + userName + "', " + " FirstName='" + firstName + "', " + " MiddleName='" + middleName + "'," + "LastName='" + lastName + "'," + "EmailID='" + emailID + "'" + " where UserID='" + userid + "' ";
                   //command.CommandText = "INSERT INTO Users (UserName, FirstName, LastName, MiddleName, EmailID) VALUES (" + "'" + userName + "','" + firstName + "','" + lastName + "','" + middleName + "','" + emailID + "')";
                   //connection.Open();
                   //command.ExecuteNonQuery();

                   command.Connection = connection;
                   command.CommandText = "{CALL Message_GetAllRecipientByUserOID(?)}";
                   command.CommandType = CommandType.StoredProcedure;

                   OdbcParameter paramODI = new OdbcParameter("@UserOID", OdbcType.Int);
                   paramODI.Value = Convert.ToInt32(userOID);
                   command.Parameters.Add(paramODI);

                   connection.Open();
                   using (OdbcDataReader dataReader = command.ExecuteReader())
                   {
                       MessageCenter message;
                       while (dataReader.Read())
                       {
                           //MessageOID, SendFrom, Recipient, Subject, Status, Mark, MessageBody, CreatedDate
                           message = new MessageCenter();
                           message.MessageOID = (int)dataReader["MessageOID"];
                           message.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                           //message.Mark = Convert.ToInt32(dataReader["Mark"]);
                           message.Mark = Convert.ToInt32(dataReader["UMark"]);
                           message.MessageBody = Convert.ToString(dataReader["MessageBody"]);
                           message.Recipient = Convert.ToString(dataReader["Recipient"]);
                           message.SendFrom = Convert.ToInt32(dataReader["SendFrom"]);
                           message.Status = Convert.ToString(dataReader["Status"]);
                           message.Subject = Convert.ToString(dataReader["Subject"]);
                           message.MarkURL = (message.Mark == 1) ? "~/images/StarIconGold.png" : "";

                           message.MessageUserOID = (int)dataReader["MessageUserOID"];
                           message.UStatus = Convert.ToString(dataReader["UStatus"]);
                           message.FlagURL = (message.UStatus == "Unread") ? "~/images/flag_icon.png" : "";


                           messages.Add(message);
                       }
                   }

               }

               return messages;
           }
       }
       public Collection<MessageCenter> GetMessageTrashedByUser(int userOID)
       {
           Collection<MessageCenter> messages = new Collection<MessageCenter>();
           using (OdbcConnection connection = new OdbcConnection(connectionString))
           {
               using (OdbcCommand command = new OdbcCommand())
               {
                   //command.Connection = connection;
                   ////command.CommandText = "UPDATE dbo.Users SET " + "UserName='" + userName + "', " + " FirstName='" + firstName + "', " + " MiddleName='" + middleName + "'," + "LastName='" + lastName + "'," + "EmailID='" + emailID + "'" + " where UserID='" + userid + "' ";
                   //command.CommandText = "INSERT INTO Users (UserName, FirstName, LastName, MiddleName, EmailID) VALUES (" + "'" + userName + "','" + firstName + "','" + lastName + "','" + middleName + "','" + emailID + "')";
                   //connection.Open();
                   //command.ExecuteNonQuery();

                   command.Connection = connection;
                   command.CommandText = "{CALL Message_GetAllRecipientTrashedByUserOID(?)}";
                   command.CommandType = CommandType.StoredProcedure;

                   OdbcParameter paramODI = new OdbcParameter("@UserOID", OdbcType.Int);
                   paramODI.Value = Convert.ToInt32(userOID);
                   command.Parameters.Add(paramODI);

                   connection.Open();
                   using (OdbcDataReader dataReader = command.ExecuteReader())
                   {
                       MessageCenter message;
                       while (dataReader.Read())
                       {
                           //MessageOID, SendFrom, Recipient, Subject, Status, Mark, MessageBody, CreatedDate
                           message = new MessageCenter();
                           message.MessageOID = (int)dataReader["MessageOID"];
                           message.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                           //message.Mark = Convert.ToInt32(dataReader["Mark"]);
                           message.Mark = Convert.ToInt32(dataReader["UMark"]);
                           message.MessageBody = Convert.ToString(dataReader["MessageBody"]);
                           message.Recipient = Convert.ToString(dataReader["Recipient"]);
                           message.SendFrom = Convert.ToInt32(dataReader["SendFrom"]);
                           message.Status = Convert.ToString(dataReader["Status"]);
                           message.Subject = Convert.ToString(dataReader["Subject"]);
                           message.MarkURL = (message.Mark == 1) ? "~/images/StarIconGold.png" : "";

                           message.MessageUserOID = (int)dataReader["MessageUserOID"];
                           message.UStatus = Convert.ToString(dataReader["UStatus"]);
                           message.FlagURL = (message.UStatus == "Unread") ? "~/images/flag_icon.png" : "";

                           messages.Add(message);
                       }
                   }

               }

               return messages;
           }
       }
       public Collection<MessageCenter> GetSentMessageByUser(int userOID)
       {
           Collection<MessageCenter> messages = new Collection<MessageCenter>();
           using (OdbcConnection connection = new OdbcConnection(connectionString))
           {
               using (OdbcCommand command = new OdbcCommand())
               {
                   //command.Connection = connection;
                   ////command.CommandText = "UPDATE dbo.Users SET " + "UserName='" + userName + "', " + " FirstName='" + firstName + "', " + " MiddleName='" + middleName + "'," + "LastName='" + lastName + "'," + "EmailID='" + emailID + "'" + " where UserID='" + userid + "' ";
                   //command.CommandText = "INSERT INTO Users (UserName, FirstName, LastName, MiddleName, EmailID) VALUES (" + "'" + userName + "','" + firstName + "','" + lastName + "','" + middleName + "','" + emailID + "')";
                   //connection.Open();
                   //command.ExecuteNonQuery();

                   command.Connection = connection;
                   command.CommandText = "{CALL Message_GetByUserOID(?)}";
                   command.CommandType = CommandType.StoredProcedure;

                   OdbcParameter paramODI = new OdbcParameter("@UserOID", OdbcType.Int);
                   paramODI.Value = Convert.ToInt32(userOID);
                   command.Parameters.Add(paramODI);

                   connection.Open();
                   using (OdbcDataReader dataReader = command.ExecuteReader())
                   {
                       MessageCenter message;
                       while (dataReader.Read())
                       {
                           //MessageOID, SendFrom, Recipient, Subject, Status, Mark, MessageBody, CreatedDate
                           message = new MessageCenter();
                           message.MessageOID = (int)dataReader["MessageOID"];
                           message.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                           //message.Mark = Convert.ToString(dataReader["Mark"]) == "Mark" ? 1 : 0;
                           message.Mark = Convert.ToInt32(dataReader["Mark"]); //This must comes from "Mark"
                           message.MessageBody = Convert.ToString(dataReader["MessageBody"]);
                           message.Recipient = Convert.ToString(dataReader["Recipient"]);
                           message.SendFrom = Convert.ToInt32(dataReader["SendFrom"]);
                           message.Status = Convert.ToString(dataReader["Status"]);
                           message.Subject = Convert.ToString(dataReader["Subject"]);
                           message.MarkURL = (message.Mark == 1) ? "~/images/StarIconGold.png" : "";
                           messages.Add(message);
                       }
                   }

               }

               return messages;
           }
       }

       public Collection<MessageCenter> GetMessages(string numberOfRows, string pageIndex, string sortColumnName, string sortOrderBy, out int totalRecords)
       {
           Collection<MessageCenter> messages = new Collection<MessageCenter>();

           using (OdbcConnection connection = new OdbcConnection(connectionString))
           {
               using (OdbcCommand command = new OdbcCommand())
               {
                   command.Connection = connection;
                   command.CommandText = "{CALL MessageCenter_Search{0}(?,?,?,?,?)}";
                   command.CommandType = CommandType.StoredProcedure;

                   OdbcParameter paramPageIndex = new OdbcParameter("@PageIndex", OdbcType.Int);
                   paramPageIndex.Value = Convert.ToInt32(pageIndex);
                   command.Parameters.Add(paramPageIndex);

                   OdbcParameter paramColumnName = new OdbcParameter("@SortColumnName", OdbcType.VarChar, 50);
                   paramColumnName.Value = sortColumnName;
                   command.Parameters.Add(paramColumnName);

                   OdbcParameter paramSortorderBy = new OdbcParameter("@SortOrderBy", OdbcType.VarChar, 4);
                   paramSortorderBy.Value = sortOrderBy;
                   command.Parameters.Add(paramSortorderBy);

                   OdbcParameter paramNumberOfRows = new OdbcParameter("@NumberOfRows", OdbcType.Int);
                   paramNumberOfRows.Value = Convert.ToInt32(numberOfRows);
                   command.Parameters.Add(paramNumberOfRows);

                   OdbcParameter paramTotalRecords = new OdbcParameter("@TotalRecords", OdbcType.Int);
                   totalRecords = 0;
                   paramTotalRecords.Value = totalRecords;
                   paramTotalRecords.Direction = ParameterDirection.Output;
                   command.Parameters.Add(paramTotalRecords);

                   //OdbcParameter paramfield = new OdbcParameter("@field", OdbcType.VarChar,200);
                   //paramfield.Value=field;                
                   //command.Parameters.Add(paramfield);

                   //OdbcParameter paramvalue = new OdbcParameter("@val", OdbcType.VarChar,200);
                   //paramvalue.Value=val;                
                   //command.Parameters.Add(paramvalue);


                   connection.Open();
                   using (OdbcDataReader dataReader = command.ExecuteReader())
                   {
                       MessageCenter message;
                       while (dataReader.Read())
                       {
                           //MessageOID, SendFrom, Recipient, Subject, Status, Mark, MessageBody, CreatedDate
                           message = new MessageCenter();
                           message.MessageOID = (int)dataReader["MessageOID"];
                           message.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                           message.Mark = Convert.ToString(dataReader["Mark"]) == "Mark" ? 1 : 0;
                           message.MessageBody = Convert.ToString(dataReader["MessageBody"]);
                           message.Recipient = Convert.ToString(dataReader["Recipient"]);
                           message.SendFrom = Convert.ToInt32(dataReader["SendFrom"]);
                           message.Status = Convert.ToString(dataReader["Status"]);
                           message.Subject = Convert.ToString(dataReader["Subject"]);
                           message.MarkURL = (message.Mark == 1) ? "~/images/StarIconGold.png" : "";

                           messages.Add(message);
                       }
                   }
                   totalRecords = (int)paramTotalRecords.Value;
               }

               return messages;
           }

       }

       public MessageCenter GetMessageByOID(int mOID)
       {
           MessageCenter msg = null;
           using (OdbcConnection connection = new OdbcConnection(connectionString))
           {
               using (OdbcCommand command = new OdbcCommand())
               {
                   command.Connection = connection;
                   command.CommandText = "{CALL MessageCenter_GetByOID(?)}";
                   command.CommandType = CommandType.StoredProcedure;

                   OdbcParameter paramODI = new OdbcParameter("@MessageOID", OdbcType.Int);
                   paramODI.Value = Convert.ToInt32(mOID);
                   command.Parameters.Add(paramODI);


                   connection.Open();
                   using (OdbcDataReader dataReader = command.ExecuteReader())
                   {
                       dataReader.Read();
                       //MessageOID, SendFrom, Recipient, Subject, Status, Mark, MessageBody, CreatedDate
                       msg = new MessageCenter();
                       msg.MessageOID = (int)dataReader["MessageOID"];
                       msg.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                       //msg.Mark = Convert.ToString(dataReader["Mark"]) == "Mark" ? 1 : 0;
                       msg.Mark = Convert.ToInt32(dataReader["Mark"]);
                       msg.MessageBody = Convert.ToString(dataReader["MessageBody"]);
                       msg.Recipient = Convert.ToString(dataReader["Recipient"]);
                       msg.SendFrom = Convert.ToInt32(dataReader["SendFrom"]);
                       msg.Status = Convert.ToString(dataReader["Status"]);
                       msg.Subject = Convert.ToString(dataReader["Subject"]);
                       msg.MarkURL = (msg.Mark == 1) ? "~/images/StarIconGold.png" : "";
                   }
               }
           }
           
           return msg;

       }

       public bool UpdateMessageCenterStatus(int MessageOID, string Status)
       {
           bool result = false;
           using (OdbcConnection connection = new OdbcConnection(connectionString))
           {
               using (OdbcCommand command = new OdbcCommand())
               {
                   command.Connection = connection;
                   command.CommandText = "{CALL MessageCenterStatus_Update(?,?)}";
                   command.CommandType = CommandType.StoredProcedure;

                   command.Parameters.AddWithValue("@MessageOID", MessageOID);
                   command.Parameters.AddWithValue("@Status", Status);

                   connection.Open();
                   int n = command.ExecuteNonQuery();
                   if (n == 1)
                       result = true;
                   else
                       result = false;
               }
           }

           return result;
       }




       public bool UpdateMessageUserUStatus(int MessageUserOID, string UStatus)
       {
           bool result = false;
           using (OdbcConnection connection = new OdbcConnection(connectionString))
           {
               using (OdbcCommand command = new OdbcCommand())
               {
                   command.Connection = connection;
                   command.CommandText = "{CALL MessageUserUStatus_Update(?,?)}";
                   command.CommandType = CommandType.StoredProcedure;

                   command.Parameters.AddWithValue("@MessageUserOID", MessageUserOID);
                   command.Parameters.AddWithValue("@UStatus", UStatus);

                   connection.Open();
                   int n = command.ExecuteNonQuery();
                   if (n == 1)
                       result = true;
                   else
                       result = false;
               }
           }

           return result;
       }
       public bool UpdateMessageUserUMark(int MessageUserOID, int UMark)
       {
           bool result = false;

           using (OdbcConnection connection = new OdbcConnection(connectionString))
           {
               using (OdbcCommand command = new OdbcCommand())
               {
                   command.Connection = connection;
                   command.CommandText = "{CALL MessageUserUMark_Update(?,?)}";
                   command.CommandType = CommandType.StoredProcedure;

                   command.Parameters.AddWithValue("@MessageUserOID", MessageUserOID);
                   command.Parameters.AddWithValue("@UMark", UMark);

                   connection.Open();
                   int n = command.ExecuteNonQuery();
                   if (n == 1)
                       result = true;
                   else
                       result = false;
               }
           }

           return result;
       }
       public bool DeleteMessageUserByOID(int MessageUserOID)
       {
           bool result = false;

           using (OdbcConnection connection = new OdbcConnection(connectionString))
           {
               using (OdbcCommand command = new OdbcCommand())
               {
                   command.Connection = connection;
                   command.CommandText = "{CALL MessageUser_Delete(?)}";
                   command.CommandType = CommandType.StoredProcedure;

                   command.Parameters.AddWithValue("@MessageUserOID", MessageUserOID);
                   

                   connection.Open();
                   int n = command.ExecuteNonQuery();
                   if (n == 1)
                       result = true;
                   else
                       result = false;
               }
           }

           return result;
       }



}
