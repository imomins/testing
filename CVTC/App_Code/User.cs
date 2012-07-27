using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data.Odbc;



public struct JQGridResults
{
    public int page;
    public int total;
    public int records;
    public JQGridRow[] rows;

}
public struct JQGridRow
{
    public int id;
    public string[] cell;
}

/// <summary>
/// Summary description for User
/// </summary>
[Serializable]
public class User
{
    string connectionString;
    public User()
    {
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();
    }
    public int UserOID
    { get; set; }

   
    public string UserName
    { get; set; }

    public string Password
    { get; set; }

    public string FirstName
    { get; set; }

    public string LastName
    { get; set; }

    public string Phone
    { get; set; }


    public string Email
    { get; set; }

    public string Advocacy
    { get; set; }

    public string Freez
    { get; set; }
    
    public User GetUserByNameAndPassword(string userName, string password)
    {
        User u=null;
       // string connectionString = @"Data Source=.\SQLEXPRESS; Initial Catalog=cvtc; User ID=cvtcuser; Password=cvtcuser";
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
                command.CommandText = "{CALL User_GetByUserNameAndPassword(?,?)}";
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 900;

                OdbcParameter paramUserName = new OdbcParameter("@UserName", OdbcType.VarChar, 100);
                paramUserName.Value = userName;
                command.Parameters.Add(paramUserName);
                //connection.Open();

                OdbcParameter paramPassword = new OdbcParameter("@Password", OdbcType.VarChar, 100);
                paramPassword.Value = password;
                command.Parameters.Add(paramPassword);
                connection.Close();
                connection.Open();
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    
                    while (dataReader.Read())
                    {
                        u = new User();
                        u.UserOID = (int)dataReader["UserOID"];
                        u.UserName = Convert.ToString(dataReader["UserName"]);
                        u.Password = Convert.ToString(dataReader["Password"]);
                        u.FirstName = Convert.ToString(dataReader["FirstName"]);
                        u.LastName = Convert.ToString(dataReader["LastName"]);
                        u.Phone = Convert.ToString(dataReader["Phone"]);
                        u.Email = Convert.ToString(dataReader["Email"]);
                        u.Advocacy = Convert.ToString(dataReader["Advocacy"]);
                        //u.Freez = Convert.ToString(dataReader["Freez"]);

                    }
                }

            }
        }
        return u;
    }

    public User GetUserByOID(int OID)
    {
        User u = null;
        //string connectionString = @"Data Source=.\SQLEXPRESS; Initial Catalog=cvtc; User ID=cvtcuser; Password=cvtcuser";
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
                command.CommandText = "{CALL User_BYOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                OdbcParameter paramUserOID = new OdbcParameter("@UserOD", OdbcType.Int);
                paramUserOID.Value = OID;
                command.Parameters.Add(paramUserOID);                               
                
                connection.Open();
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {

                    while (dataReader.Read())
                    {
                        u = new User();
                        u.UserOID = (int)dataReader["UserOID"];
                        u.UserName = Convert.ToString(dataReader["UserName"]);
                        u.Password = Convert.ToString(dataReader["Password"]);
                        u.FirstName = Convert.ToString(dataReader["FirstName"]);
                        u.LastName = Convert.ToString(dataReader["LastName"]);
                        u.Phone = Convert.ToString(dataReader["Phone"]);
                        u.Email = Convert.ToString(dataReader["Email"]);
                        u.Advocacy = Convert.ToString(dataReader["Advocacy"]);
                        u.Freez = Convert.ToString(dataReader["Freez"]);

                    }
                }

            }
        }
        return u;
    }

    public Collection<User> GetAllUser()
    {
        Collection<User> users = new Collection<User>();
        //string connectionString = @"Data Source=.\SQLEXPRESS; Initial Catalog=cvtc; User ID=cvtcuser; Password=cvtcuser";
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
                command.CommandText = "User_GETAllUser";
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    User user;
                    while (dataReader.Read())
                    {
                        user = new User();
                        user.UserOID = (int)dataReader["UserOID"];
                        user.UserName = Convert.ToString(dataReader["UserName"]);
                        user.Password = Convert.ToString(dataReader["Password"]);
                        user.FirstName = Convert.ToString(dataReader["FirstName"]);
                        user.LastName = Convert.ToString(dataReader["LastName"]);
                        user.Phone = Convert.ToString(dataReader["Phone"]);
                        user.Email = Convert.ToString(dataReader["Email"]);
                        user.Advocacy = Convert.ToString(dataReader["Advocacy"]);
                        user.Freez = Convert.ToString(dataReader["Freez"]);
                        user.LastName = user.LastName + " " + user.FirstName;
                        users.Add(user);
                    }
                }
         
            }

            return users;
        }
    }


    public int GetUserOIDByUserName(string Name)
    {
        //Collection<Student> studentList = new Collection<Student>();
        Student student = null;
        int UserOID;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL UserOID_ByUserName(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@Name", Name);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    //Question q = new Question();
                    dataReader.Read();
                    //while (dataReader.Read())
                    {
                        //student = new Student();
                        UserOID = Convert.ToInt32(dataReader["UserOID"]);
                        // studentList.Add(student);

                    }
                }

            }
        }
        return UserOID;
    }

    public string  GetUserFreezByUserName(string Name)
    {
        string  Freez=null ;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL UserFreez_ByUserName(?)}";
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 900;
                //Set Parameter Value
                command.Parameters.AddWithValue("@Name", Name);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                  
                   if ( dataReader.Read())
                  
                    {
                  
                        Freez = Convert.ToString(dataReader["Freez"]);
                  

                    }
                }

            }
        }
        return Freez;
    }


   
}
