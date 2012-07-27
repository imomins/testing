using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Odbc;

/// <summary>
/// Summary description for Task
/// </summary>

[Serializable]
public class Task
{
//    string connectionString = @"Data Source=.\SQLEXPRESS; Initial Catalog=cvtc; User ID=cvtcuser; Password=cvtcuser";
    string connectionString;
    public Task()
    {
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();
    }
    public int TaskOID
    { get; set; }

    public string SendFrom
    { get; set; }
    public string TaskFrom
    { get; set; }

    public string Recipient
    { get; set; }

    public string Subject
    { get; set; }

    public string MessageBody
    { get; set; }

    public DateTime CompletionDate
    { get; set; }

    public int Priority
    { get; set; }

    public DateTime CreatedDate
    { get; set; }

    public int TaskUserOID
    { get; set; }

    public string Status
    { get; set; }

    public int Mark
    { get; set; }


    public string MarkURL
    { get; set; }
    public string FlagURL
    { get; set; }
    
    public bool AddTask()
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
                command.CommandText = "{CALL Task_insert(?,?,?,?,?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@SendFrom", Convert.ToInt32(this.SendFrom));
                command.Parameters.AddWithValue("@Recipient", this.Recipient);
                command.Parameters.AddWithValue("@Subject", this.Subject);
                command.Parameters.AddWithValue("@MessageBody", this.MessageBody);
                command.Parameters.AddWithValue("@CompletionDate", this.CompletionDate);                
                command.Parameters.AddWithValue("@Priority", this.Priority);
                
                command.Parameters.AddWithValue("@Status", this.Status);
                command.Parameters.AddWithValue("@Mark", this.Mark);

                //command.Parameters.AddWithValue("@SendFrom", this.SendFrom);
                
                //OdbcParameter paramUserName = new OdbcParameter("@SendFrom", OdbcType.Int, 500);
                //paramUserName.Value = Convert.ToInt32(this.SendFrom);
                //command.Parameters.Add(paramUserName);

                //OdbcParameter paramPassword = new OdbcParameter("@Recipient", OdbcType.VarChar, 500);
                //paramPassword.Value = this.Recipient;
                //command.Parameters.Add(paramPassword);

                //OdbcParameter paramLastName = new OdbcParameter("@Subject", OdbcType.VarChar, 500);
                //paramLastName.Value = this.Subject;
                //command.Parameters.Add(paramLastName);

                //OdbcParameter paramFirstName = new OdbcParameter("@Priority", OdbcType.Int);
                //paramFirstName.Value = this.Priority;
                //command.Parameters.Add(paramFirstName);

                //OdbcParameter paramPhone = new OdbcParameter("@CompletionDate", OdbcType.DateTime);
                //paramPhone.Value = this.CompletionDate;
                //command.Parameters.Add(paramPhone);

                //OdbcParameter paramEmail = new OdbcParameter("@MessageBody", OdbcType.VarChar, 1000);
                //paramEmail.Value = this.MessageBody;
                //command.Parameters.Add(paramEmail);


                connection.Open();
                int n = command.ExecuteNonQuery();
                if (n > 0)
                    result = true;
                else
                    result = false;
            }
        }

        return result;
    }
    public Collection<Task> GetAllTask()
    {
        Collection<Task> tasks = new Collection<Task>();
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
                command.CommandText = "Task_GetAllTask";
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    Task task;
                    while (dataReader.Read())
                    {
                        //MessageOID, SendFrom, Recipient, Subject, Status, Mark, MessageBody, CreatedDate
                        task = new Task();
                        task.TaskOID = (int)dataReader["TaskOID"];
                        task.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);                        
                        task.MessageBody = Convert.ToString(dataReader["MessageBody"]);
                        task.Recipient = Convert.ToString(dataReader["Recipient"]);
                        task.SendFrom = Convert.ToString(dataReader["SendFrom"]);                        
                        task.Subject = Convert.ToString(dataReader["Subject"]);
                        task.Priority = Convert.ToInt32(dataReader["Priority"]);

                        tasks.Add(task);
                    }
                }

            }

            return tasks;
        }
    }

    public Collection<Task> GetTaskByUserOID(int userOID)
    {
        Collection<Task> tasks = new Collection<Task>();
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
                command.CommandText = "{CALL Task_GetByUserOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                OdbcParameter paramuserOID = new OdbcParameter("@UserOID", OdbcType.Int);
                paramuserOID.Value = userOID;
                command.Parameters.Add(paramuserOID);


                connection.Open();
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    Task task;
                    while (dataReader.Read())
                    {
                        //MessageOID, SendFrom, Recipient, Subject, Status, Mark, MessageBody, CreatedDate
                        task = new Task();
                        task.TaskOID = (int)dataReader["TaskOID"];
                        task.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        task.MessageBody = Convert.ToString(dataReader["MessageBody"]);
                        task.Recipient = Convert.ToString(dataReader["Recipient"]);
                        task.SendFrom = Convert.ToString(dataReader["SendFrom"]);
                        task.Subject = Convert.ToString(dataReader["Subject"]);
                        task.Priority = Convert.ToInt32(dataReader["Priority"]);
                        task.TaskFrom = Convert.ToString(dataReader["TaskFrom"]);
                        task.CompletionDate = Convert.ToDateTime(dataReader["CompletionDateTime"]);
                        task.Mark = (int)dataReader["Mark"]; //This must come from "Mark"
                        task.MarkURL = (task.Mark == 1) ? "~/images/StarIconGold.png" : "";

                        tasks.Add(task);
                    }
                }

            }

            return tasks;
        }
    }

    public Collection<Task> GetAllTaskRecipientByUserOID(int userOID)
    {
        Collection<Task> tasks = new Collection<Task>();
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
                command.CommandText = "{CALL Task_GetAllRecipientByUserOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                OdbcParameter paramuserOID = new OdbcParameter("@UserOID", OdbcType.Int);
                paramuserOID.Value = userOID;
                command.Parameters.Add(paramuserOID);


                connection.Open();
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    Task task;
                    while (dataReader.Read())
                    {
                        //MessageOID, SendFrom, Recipient, Subject, Status, Mark, MessageBody, CreatedDate
                        task = new Task();
                        task.TaskOID = (int)dataReader["TaskOID"];
                        if (dataReader["CreatedDate"] != null && dataReader["CreatedDate"] != DBNull.Value)
                        {
                            task.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        }
                        if (dataReader["MessageBody"] != null && dataReader["MessageBody"] != DBNull.Value)
                        {
                            task.MessageBody = Convert.ToString(dataReader["MessageBody"]);
                        }
                        if (dataReader["Recipient"] != null && dataReader["Recipient"] != DBNull.Value)
                        {
                            task.Recipient = Convert.ToString(dataReader["Recipient"]);
                        }
                        if (dataReader["SendFrom"] != null && dataReader["SendFrom"] != DBNull.Value)
                        {
                            task.SendFrom = Convert.ToString(dataReader["SendFrom"]);
                        }
                        if (dataReader["Subject"] != null && dataReader["Subject"] != DBNull.Value)
                        {
                            task.Subject = Convert.ToString(dataReader["Subject"]);
                        }
                        if (dataReader["Priority"] != null && dataReader["Priority"] != DBNull.Value)
                        {
                            task.Priority = Convert.ToInt32(dataReader["Priority"]);
                        }
                        if (dataReader["TaskFrom"] != null && dataReader["TaskFrom"] != DBNull.Value)
                        {
                            task.TaskFrom = Convert.ToString(dataReader["TaskFrom"]);
                        }
                        if (dataReader["CompletionDateTime"] != null && dataReader["CompletionDateTime"] != DBNull.Value)
                        {
                            task.CompletionDate = Convert.ToDateTime(dataReader["CompletionDateTime"]);
                        }
                        //dbo.TaskUser.TaskUserOID, dbo.TaskUser.UStatus, dbo.TaskUser.UMark
                        if (dataReader["TaskUserOID"] != null && dataReader["TaskUserOID"] != DBNull.Value)
                        {
                            task.TaskUserOID = (int)dataReader["TaskUserOID"];
                        }
                        if (dataReader["UStatus"] != null && dataReader["UStatus"] != DBNull.Value)
                        {
                            task.Status = Convert.ToString(dataReader["UStatus"]);
                        }
                        if (dataReader["UMark"] != null && dataReader["UMark"] != DBNull.Value)
                        {
                            task.Mark = (int)dataReader["UMark"];
                        }
                      
                            task.MarkURL = (task.Mark == 1) ? "~/images/StarIconGold.png" : "";
                       
                       
                            task.FlagURL = (task.Status == "Unread") ? "~/images/flag_icon.png" : "";
                        

                        tasks.Add(task);
                    }
                }

            }

            return tasks;
        }
    }

    public Collection<Task> GetAllTaskRecipientCompletedByUserOID(int userOID)
    {
        Collection<Task> tasks = new Collection<Task>();
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
                command.CommandText = "{CALL Task_GetAllRecipientCompletedByUserOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                OdbcParameter paramuserOID = new OdbcParameter("@UserOID", OdbcType.Int);
                paramuserOID.Value = userOID;
                command.Parameters.Add(paramuserOID);


                connection.Open();
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    Task task;
                    while (dataReader.Read())
                    {
                        //MessageOID, SendFrom, Recipient, Subject, Status, Mark, MessageBody, CreatedDate
                        task = new Task();
                        task.TaskOID = (int)dataReader["TaskOID"];
                        task.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        task.MessageBody = Convert.ToString(dataReader["MessageBody"]);
                        task.Recipient = Convert.ToString(dataReader["Recipient"]);
                        task.SendFrom = Convert.ToString(dataReader["SendFrom"]);
                        task.Subject = Convert.ToString(dataReader["Subject"]);
                        task.Priority = Convert.ToInt32(dataReader["Priority"]);
                        task.TaskFrom = Convert.ToString(dataReader["TaskFrom"]);
                        task.CompletionDate = Convert.ToDateTime(dataReader["CompletionDateTime"]);
                        //dbo.TaskUser.TaskUserOID, dbo.TaskUser.UStatus, dbo.TaskUser.UMark

                        task.TaskUserOID = (int)dataReader["TaskUserOID"];
                        task.Status = Convert.ToString(dataReader["UStatus"]);
                        task.Mark = (int)dataReader["UMark"];

                        task.MarkURL = (task.Mark == 1) ? "~/images/StarIconGold.png" : "";
                        tasks.Add(task);
                    }
                }

            }

            return tasks;
        }
    }

    public Collection<Task> GetTaskByStartAndEndDate(DateTime start,DateTime end)
    {
        Collection<Task> tasks = new Collection<Task>();
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
                command.CommandText = "{CALL Task_GetTaskByStartAndEndDate(?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                OdbcParameter paramStart = new OdbcParameter("@start", OdbcType.DateTime);
                paramStart.Value = start;
                command.Parameters.Add(paramStart);

                OdbcParameter paramEnd = new OdbcParameter("@end", OdbcType.DateTime);
                paramEnd.Value = end;
                command.Parameters.Add(paramEnd);

                //OdbcParameter paramUserOID = new OdbcParameter("@userOID", OdbcType.Int);
                //paramUserOID.Value = UserOID;
                //command.Parameters.Add(paramUserOID);


                connection.Open();
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    Task task;
                    while (dataReader.Read())
                    {
                        //MessageOID, SendFrom, Recipient, Subject, Status, Mark, MessageBody, CreatedDate
                        task = new Task();
                        task.TaskOID = (int)dataReader["TaskOID"];
                        task.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        task.MessageBody = Convert.ToString(dataReader["MessageBody"]);
                        task.Recipient = Convert.ToString(dataReader["Recipient"]);
                        task.SendFrom = Convert.ToString(dataReader["SendFrom"]);
                        task.Subject = Convert.ToString(dataReader["Subject"]);
                        task.Priority = Convert.ToInt32(dataReader["Priority"]);
                        task.TaskFrom = Convert.ToString(dataReader["TaskFrom"]);
                        task.CompletionDate = Convert.ToDateTime(dataReader["CompletionDateTime"]);
                        tasks.Add(task);
                    }
                }

            }

            return tasks;
        }
    }

    public Collection<Task> GetTasks(string numberOfRows, string pageIndex, string sortColumnName, string sortOrderBy, out int totalRecords)
    {
        Collection<Task> tasks = new Collection<Task>();

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                command.Connection = connection;
                command.CommandText = "{CALL Task_Select{0}(?,?,?,?,?)}";
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
                    Task task;
                    while (dataReader.Read())
                    {
                        //MessageOID, SendFrom, Recipient, Subject, Status, Mark, MessageBody, CreatedDate
                        task = new Task();
                        task.TaskOID = (int)dataReader["TaskOID"];
                        task.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        task.MessageBody = Convert.ToString(dataReader["MessageBody"]);
                        task.Recipient = Convert.ToString(dataReader["Recipient"]);
                        task.SendFrom = Convert.ToString(dataReader["SendFrom"]);
                        task.Subject = Convert.ToString(dataReader["Subject"]);
                        task.Priority = Convert.ToInt32(dataReader["Priority"]);
                        
                        tasks.Add(task);
                    }
                }
                totalRecords = (int)paramTotalRecords.Value;
            }

            return tasks;
        }

    }

    public Task GetTaskByOID(int tOID)
    {
        Task task = null;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                command.Connection = connection;
                command.CommandText = "{CALL Task_GetByOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                OdbcParameter paramODI = new OdbcParameter("@TaskOID", OdbcType.Int);
                paramODI.Value = Convert.ToInt32(tOID);
                command.Parameters.Add(paramODI);


                connection.Open();
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    dataReader.Read();
                    //MessageOID, SendFrom, Recipient, Subject, Status, Mark, MessageBody, CreatedDate
                    task = new Task();
                    task.TaskOID = (int)dataReader["TaskOID"];
                    task.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                    task.MessageBody = Convert.ToString(dataReader["MessageBody"]);
                    task.Recipient = Convert.ToString(dataReader["Recipient"]);
                    task.SendFrom = Convert.ToString(dataReader["SendFrom"]);
                    task.Subject = Convert.ToString(dataReader["Subject"]);
                    task.Priority = Convert.ToInt32(dataReader["Priority"]);
                    task.CompletionDate = Convert.ToDateTime(dataReader["CompletionDateTime"]);
                }
            }
        }

        return task;

    }

    public Collection<Task> GetTaskByStartAndEndDateUOID(DateTime start, DateTime end, int UserOID)
    {
        Collection<Task> tasks = new Collection<Task>();
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
                command.CommandText = "{CALL Task_GetTaskByStartAndEndDateUOID(?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                OdbcParameter paramStart = new OdbcParameter("@start", OdbcType.DateTime);
                paramStart.Value = start;
                command.Parameters.Add(paramStart);

                OdbcParameter paramEnd = new OdbcParameter("@end", OdbcType.DateTime);
                paramEnd.Value = end;
                command.Parameters.Add(paramEnd);

                OdbcParameter paramUserOID = new OdbcParameter("@userOID", OdbcType.Int);
                paramUserOID.Value = UserOID;
                command.Parameters.Add(paramUserOID);


                connection.Open();
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    Task task;
                    while (dataReader.Read())
                    {
                        //MessageOID, SendFrom, Recipient, Subject, Status, Mark, MessageBody, CreatedDate
                        task = new Task();
                        task.TaskOID = (int)dataReader["TaskOID"];
                        task.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        task.MessageBody = Convert.ToString(dataReader["MessageBody"]);
                        task.Recipient = Convert.ToString(dataReader["Recipient"]);
                        task.SendFrom = Convert.ToString(dataReader["SendFrom"]);
                        task.Subject = Convert.ToString(dataReader["Subject"]);
                        task.Priority = Convert.ToInt32(dataReader["Priority"]);
                        task.TaskFrom = Convert.ToString(dataReader["TaskFrom"]);
                        task.CompletionDate = Convert.ToDateTime(dataReader["CompletionDateTime"]);
                        tasks.Add(task);
                    }
                }

            }

            return tasks;
        }
    }


    public bool UpdateTaskStatus(int TaskOID, string Status)
    {
        bool result = false;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                command.Connection = connection;
                command.CommandText = "{CALL TaskStatus_Update(?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@TaskOID", TaskOID);
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

    public bool UpdateTaskUserUStatus(int TaskUserOID, string UStatus)
    {
        bool result = false;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                command.Connection = connection;
                command.CommandText = "{CALL TaskUserUStatus_Update(?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@TaskUserOID", TaskUserOID);
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
    public bool UpdateTaskUserUMark(int TaskUserOID, int UMark)
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                command.Connection = connection;
                command.CommandText = "{CALL TaskUserUMark_Update(?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@TaskUserOID", TaskUserOID);
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
    public bool DeleteTaskUserByOID(int taskUserOID)
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                command.Connection = connection;
                command.CommandText = "{CALL TaskUser_Delete(?)}";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@TaskUserOID", taskUserOID);


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
