<%@ WebHandler Language="C#" Class="UserHandler" %>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Script.Serialization;
using System.Data.Odbc;

public class UserHandler : IHttpHandler {

    //string connectionString = @"Data Source=.\SQLEXPRESS; Initial Catalog=cvtc; User ID=cvtcuser; Password=cvtcuser";
    string connectionString = "";
    public void ProcessRequest (HttpContext context) {
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();

        HttpRequest request = context.Request;
        HttpResponse response = context.Response;

        string _search = request["_search"];
        string numberOfRows = request["rows"];
        string pageIndex = request["page"];
        string sortColumnName = request["sidx"];
        string sortOrderBy = request["sord"];
        string operation = request["oper"];
        string field = request["searchField"];
        string val = request["searchString"];


        if (_search == "true")
        {
            int totalRecords;
            Collection<User> users = SearchUser(numberOfRows, pageIndex, sortColumnName, sortOrderBy, out totalRecords, request["UserName"], request["Password"], request["FirstName"], request["LastName"], request["Phone"], request["Email"]);
            string output = BuildJQGridResults(users, Convert.ToInt32(numberOfRows), Convert.ToInt32(pageIndex), Convert.ToInt32(totalRecords));

            response.Write(output);
        }
        else if (operation == "add")
        {
            AddUser(request["UserName"], request["Password"], request["FirstName"], request["LastName"], request["Phone"], request["Email"]);
        }
        else if (operation == "edit")
        {
            UpdateUser(Convert.ToInt32(request["id"].ToString()), request["UserName"], request["Password"], request["FirstName"], request["LastName"], request["Phone"], request["Email"]);
            //UpdateUser(1, request["UserName"], request["Password"], request["FirstName"], request["LastName"], request["Phone"], request["Email"]);
        }
        else if (operation == "del")
        {
            DeleteUser(Convert.ToInt32(request["id"]));
        }
        else//(operation == null)
        {
            int totalRecords;
            Collection<User> users = GetUsers(numberOfRows, pageIndex, sortColumnName, sortOrderBy, out totalRecords);
            string output = BuildJQGridResults(users, Convert.ToInt32(numberOfRows), Convert.ToInt32(pageIndex), Convert.ToInt32(totalRecords));

           response.Write(output);
        }
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    private string BuildJQGridResults(Collection<User> users, int numberOfRows, int pageIndex, int totalRecords)
    {

        JQGridResults result = new JQGridResults();
        List<JQGridRow> rows = new List<JQGridRow>();
        foreach (User user in users)
        {
            JQGridRow row = new JQGridRow();
            row.id = user.UserOID;
            row.cell = new string[7];
            row.cell[0] = user.UserOID.ToString();
            row.cell[1] = user.UserName;
            row.cell[2] = user.Password;
            row.cell[3] = user.FirstName;
            row.cell[4] = user.LastName;
            row.cell[5] = user.Phone;
            row.cell[6] = user.Email;
            rows.Add(row);
        }
        result.rows = rows.ToArray();
        result.page = pageIndex;
        result.total = totalRecords / numberOfRows;
        if (totalRecords % numberOfRows != 0) result.total += 1;
        result.records = totalRecords;
        return new JavaScriptSerializer().Serialize(result);
    }
    

    private bool AddUser(string userName, string password, string firstName, string lastName, string phone, string email)
    {
        bool result = false;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand())
            {
                //command.Connection = connection;
                ////command.CommandText = "UPDATE dbo.Users SET " + "UserName='" + userName + "', " + " FirstName='" + firstName + "', " + " MiddleName='" + middleName + "'," + "LastName='" + lastName + "'," + "EmailID='" + emailID + "'" + " where UserID='" + userid + "' ";
                //command.CommandText = "INSERT INTO Users (UserName, FirstName, LastName, MiddleName, EmailID) VALUES (" + "'" + userName + "','" + firstName + "','" + lastName + "','" + middleName + "','" + emailID + "')";
                //connection.Open();
                //command.ExecuteNonQuery();

                command.Connection = connection;
                command.CommandText = "User_insert";
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUserName = new SqlParameter("@UserName", SqlDbType.VarChar, 100);
                paramUserName.Value = userName;
                command.Parameters.Add(paramUserName);

                SqlParameter paramPassword = new SqlParameter("@Password", SqlDbType.VarChar, 100);
                paramPassword.Value = password;
                command.Parameters.Add(paramPassword);

                SqlParameter paramLastName = new SqlParameter("@LastName", SqlDbType.VarChar, 100);
                paramLastName.Value = lastName;
                command.Parameters.Add(paramLastName);

                SqlParameter paramFirstName = new SqlParameter("@FirstName", SqlDbType.VarChar, 100);
                paramFirstName.Value = firstName;
                command.Parameters.Add(paramFirstName);

                SqlParameter paramPhone = new SqlParameter("@Phone", SqlDbType.VarChar, 100);
                paramPhone.Value = phone;
                command.Parameters.Add(paramPhone);

                SqlParameter paramEmail = new SqlParameter("@Email", SqlDbType.VarChar, 100);
                paramEmail.Value = email;
                command.Parameters.Add(paramEmail);
               

                connection.Open();
                int n=command.ExecuteNonQuery();
                if (n == 1)
                    result = true;
                else
                    result = false;
            }
        }

        return result;
    }


    private Collection<User> SearchUser(string numberOfRows, string pageIndex, string sortColumnName, string sortOrderBy, out int totalRecords, string userName, string password, string firstName, string lastName, string phone, string email)
    {
        userName = (userName == null) ? "" : userName;
        password = (password == null) ? "" : password;
        firstName = (firstName == null) ? "" : firstName;
        lastName = (lastName == null) ? "" : lastName;
        phone = (phone == null) ? "" : phone;
        email = (email == null) ? "" : email;
        
        Collection<User> users = new Collection<User>();   
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand())
            {
                //command.Connection = connection;
                ////command.CommandText = "UPDATE dbo.Users SET " + "UserName='" + userName + "', " + " FirstName='" + firstName + "', " + " MiddleName='" + middleName + "'," + "LastName='" + lastName + "'," + "EmailID='" + emailID + "'" + " where UserID='" + userid + "' ";
                //command.CommandText = "INSERT INTO Users (UserName, FirstName, LastName, MiddleName, EmailID) VALUES (" + "'" + userName + "','" + firstName + "','" + lastName + "','" + middleName + "','" + emailID + "')";
                //connection.Open();
                //command.ExecuteNonQuery();

                command.Connection = connection;
                command.CommandText = "User_Search{0}";
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUserName = new SqlParameter("@UserName", SqlDbType.VarChar, 100);
                paramUserName.Value = userName;
                command.Parameters.Add(paramUserName);

                SqlParameter paramPassword = new SqlParameter("@Password", SqlDbType.VarChar, 100);
                paramPassword.Value = password;
                command.Parameters.Add(paramPassword);

                SqlParameter paramLastName = new SqlParameter("@LastName", SqlDbType.VarChar, 100);
                paramLastName.Value = lastName;
                command.Parameters.Add(paramLastName);

                SqlParameter paramFirstName = new SqlParameter("@FirstName", SqlDbType.VarChar, 100);
                paramFirstName.Value = firstName;
                command.Parameters.Add(paramFirstName);

                SqlParameter paramPhone = new SqlParameter("@Phone", SqlDbType.VarChar, 100);
                paramPhone.Value = phone;
                command.Parameters.Add(paramPhone);

                SqlParameter paramEmail = new SqlParameter("@Email", SqlDbType.VarChar, 100);
                paramEmail.Value = email;
                command.Parameters.Add(paramEmail);

                SqlParameter paramPageIndex = new SqlParameter("@PageIndex", SqlDbType.Int);
                paramPageIndex.Value = Convert.ToInt32(pageIndex);
                command.Parameters.Add(paramPageIndex);

                SqlParameter paramColumnName = new SqlParameter("@SortColumnName", SqlDbType.VarChar, 50);
                paramColumnName.Value = sortColumnName;
                command.Parameters.Add(paramColumnName);

                SqlParameter paramSortorderBy = new SqlParameter("@SortOrderBy", SqlDbType.VarChar, 4);
                paramSortorderBy.Value = sortOrderBy;
                command.Parameters.Add(paramSortorderBy);

                SqlParameter paramNumberOfRows = new SqlParameter("@NumberOfRows", SqlDbType.Int);
                paramNumberOfRows.Value = Convert.ToInt32(numberOfRows);
                command.Parameters.Add(paramNumberOfRows);

                SqlParameter paramTotalRecords = new SqlParameter("@TotalRecords", SqlDbType.Int);
                totalRecords = 0;
                paramTotalRecords.Value = totalRecords;
                paramTotalRecords.Direction = ParameterDirection.Output;
                command.Parameters.Add(paramTotalRecords);

                connection.Open();
                using (SqlDataReader dataReader = command.ExecuteReader())
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
                        users.Add(user);
                    }
                }
                totalRecords = (int)paramTotalRecords.Value;
            }

            return users;
        }
    }
    
    
    private bool UpdateUser(int useroid,string userName, string password, string firstName, string lastName, string phone, string email)
    {
        bool result = false;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand())
            {                

                command.Connection = connection;
                command.CommandText = "User_Update";
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUserOID = new SqlParameter("@UserOID", SqlDbType.Int);
                paramUserOID.Value = useroid;
                command.Parameters.Add(paramUserOID);

                SqlParameter paramUserName = new SqlParameter("@UserName", SqlDbType.VarChar, 100);
                paramUserName.Value = userName;
                command.Parameters.Add(paramUserName);

                SqlParameter paramPassword = new SqlParameter("@Password", SqlDbType.VarChar, 100);
                paramPassword.Value = password;
                command.Parameters.Add(paramPassword);

                SqlParameter paramLastName = new SqlParameter("@LastName", SqlDbType.VarChar, 100);
                paramLastName.Value = lastName;
                command.Parameters.Add(paramLastName);

                SqlParameter paramFirstName = new SqlParameter("@FirstName", SqlDbType.VarChar, 100);
                paramFirstName.Value = firstName;
                command.Parameters.Add(paramFirstName);

                SqlParameter paramPhone = new SqlParameter("@Phone", SqlDbType.VarChar, 100);
                paramPhone.Value = phone;
                command.Parameters.Add(paramPhone);

                SqlParameter paramEmail = new SqlParameter("@Email", SqlDbType.VarChar, 100);
                paramEmail.Value = email;
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

    private bool DeleteUser(int useroid)
    {
        bool result = false;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand())
            {

                command.Connection = connection;
                command.CommandText = "User_Delete";
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUserOID = new SqlParameter("@UserOID", SqlDbType.Int);
                paramUserOID.Value = useroid;
                command.Parameters.Add(paramUserOID);

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
    
    private  Collection<User>  GetUsers(string numberOfRows,string pageIndex,string sortColumnName, string sortOrderBy,out int totalRecords)
    {
        Collection<User> users = new Collection<User>();        
        
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "SelectjqGrid{0}";
                command.CommandType = CommandType.StoredProcedure;
 
                SqlParameter paramPageIndex = new SqlParameter("@PageIndex", SqlDbType.Int);
                paramPageIndex.Value =Convert.ToInt32(pageIndex);
                command.Parameters.Add(paramPageIndex);
 
                SqlParameter paramColumnName = new SqlParameter("@SortColumnName", SqlDbType.VarChar, 50);
                paramColumnName.Value = sortColumnName;
                command.Parameters.Add(paramColumnName);
 
                SqlParameter paramSortorderBy = new SqlParameter("@SortOrderBy", SqlDbType.VarChar, 4);
                paramSortorderBy.Value = sortOrderBy;
                command.Parameters.Add(paramSortorderBy);
 
                SqlParameter paramNumberOfRows = new SqlParameter("@NumberOfRows", SqlDbType.Int);
                paramNumberOfRows.Value =Convert.ToInt32(numberOfRows);
                command.Parameters.Add(paramNumberOfRows);
 
                SqlParameter paramTotalRecords= new SqlParameter("@TotalRecords", SqlDbType.Int);
                totalRecords = 0;
                paramTotalRecords.Value = totalRecords;
                paramTotalRecords.Direction = ParameterDirection.Output;
                command.Parameters.Add(paramTotalRecords);                                                               
                 
                //SqlParameter paramfield = new SqlParameter("@field", SqlDbType.VarChar,200);
                //paramfield.Value=field;                
                //command.Parameters.Add(paramfield);
                
                //SqlParameter paramvalue = new SqlParameter("@val", SqlDbType.VarChar,200);
                //paramvalue.Value=val;                
                //command.Parameters.Add(paramvalue);
                
                 
                connection.Open();
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    User user;
                    while (dataReader.Read())
                    {
                        user = new User();
                        user.UserOID = (int) dataReader["UserOID"];
                        user.UserName = Convert.ToString(dataReader["UserName"]);
                        user.Password = Convert.ToString(dataReader["Password"]);
                        user.FirstName = Convert.ToString(dataReader["FirstName"]);                        
                        user.LastName = Convert.ToString(dataReader["LastName"]);
                        user.Phone = Convert.ToString(dataReader["Phone"]);
                        user.Email = Convert.ToString(dataReader["Email"]);
                        users.Add(user);
                    }
                }
                totalRecords = (int)paramTotalRecords.Value;
            }
             
            return users;
        }
 
    }

    


}