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

    //string connectionString= @"Data Source=.AETHER\\SQLEXPRESS; Initial Catalog=cvtc; User ID=cvtcuser; Password=cvtcuser";
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

        
        //if (_search == "true")
        //{
        //    int totalRecords;
        //    Collection<User> users = SearchUser(numberOfRows, pageIndex, sortColumnName, sortOrderBy, out totalRecords, request["UserName"], request["Password"], request["FirstName"], request["LastName"], request["Phone"], request["Email"]);
        //    string output = BuildJQGridResults(users, Convert.ToInt32(numberOfRows), Convert.ToInt32(pageIndex), Convert.ToInt32(totalRecords));

        //    response.Write(output);
        //}

        if (_search == "true")
        {
            int totalRecords;
            Collection<User> users = SearchUser(numberOfRows, pageIndex, sortColumnName, sortOrderBy, out totalRecords, request["UserName"], request["Password"], request["FirstName"], request["LastName"], request["Phone"], request["Email"], request["Advocacy"], request["Freez"]);
            string output = BuildJQGridResults(users, Convert.ToInt32(numberOfRows), Convert.ToInt32(pageIndex), Convert.ToInt32(totalRecords));

            response.Write(output);
        }
        else if (operation == "add")
        {
            //if (!CheckUser(request["UserName"]))
            //{
            AddUser(request["UserName"], request["Password"], request["FirstName"], request["LastName"], request["Phone"], request["Email"], request["Advocacy"], request["Freez"]);
            //}
            
        }
        else if (operation == "edit")
        {
            //if (!CheckUser(request["UserName"]))
            //{
                UpdateUser(Convert.ToInt32(request["id"].ToString()), request["UserName"], request["Password"], request["FirstName"], request["LastName"], request["Phone"], request["Email"], request["Advocacy"], request["Freez"]);
                //UpdateUser(1, request["UserName"], request["Password"], request["FirstName"], request["LastName"], request["Phone"], request["Email"]);
            //}
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

    public bool CheckUser(string userName)
    {
        bool result = true;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                command.Connection = connection;
                command.CommandText = "{CALL User_ByUserName(?)}";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserName", userName);
                connection.Open();
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {

                    if (dataReader.Read())
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
        }

        return result;
    }
    
    private string BuildJQGridResults(Collection<User> users, int numberOfRows, int pageIndex, int totalRecords)
    {

        JQGridResults result = new JQGridResults();
        List<JQGridRow> rows = new List<JQGridRow>();
        foreach (User user in users)
        {
            JQGridRow row = new JQGridRow();
            row.id = user.UserOID;
            row.cell = new string[10];
            row.cell[0] = user.UserOID.ToString();
            row.cell[1] = user.UserName;
            row.cell[2] = user.Password;
            row.cell[3] = user.FirstName;
            row.cell[4] = user.LastName;
            row.cell[5] = user.Phone;
            row.cell[6] = user.Email;
            row.cell[7] = user.Advocacy;
            row.cell[8] = user.Freez;
            row.cell[9] = "Delete";
            rows.Add(row);
        }
        result.rows = rows.ToArray();
        result.page = pageIndex;
        result.total = totalRecords / numberOfRows;
        if (totalRecords % numberOfRows != 0) result.total += 1;
        result.records = totalRecords;
        return new JavaScriptSerializer().Serialize(result);
    }


    private bool AddUser(string userName, string password, string firstName, string lastName, string phone, string email, string advocacy,string freez)
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
               
                command.Connection = connection;
                command.CommandText = "{CALL User_insert(?,?,?,?,?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                OdbcParameter paramUserName = new OdbcParameter("@UserName", OdbcType.VarChar, 100);
                paramUserName.Value = userName;
                command.Parameters.Add(paramUserName);

                OdbcParameter paramPassword = new OdbcParameter("@Password", OdbcType.VarChar, 100);
                paramPassword.Value = password;
                command.Parameters.Add(paramPassword);

                OdbcParameter paramLastName = new OdbcParameter("@LastName", OdbcType.VarChar, 100);
                paramLastName.Value = lastName;
                command.Parameters.Add(paramLastName);

                OdbcParameter paramFirstName = new OdbcParameter("@FirstName", OdbcType.VarChar, 100);
                paramFirstName.Value = firstName;
                command.Parameters.Add(paramFirstName);

                OdbcParameter paramPhone = new OdbcParameter("@Phone", OdbcType.VarChar, 100);
                paramPhone.Value = phone;
                command.Parameters.Add(paramPhone);

                OdbcParameter paramEmail = new OdbcParameter("@Email", OdbcType.VarChar, 100);
                paramEmail.Value = email;
                command.Parameters.Add(paramEmail);
                
                OdbcParameter paramAdvocacy = new OdbcParameter("@Advocacy", OdbcType.VarChar, 10);
                paramAdvocacy.Value = advocacy;
                command.Parameters.Add(paramAdvocacy);

             
                OdbcParameter paramFreez = new OdbcParameter("@Freez", OdbcType.VarChar, 10);
                paramFreez.Value = freez;
                command.Parameters.Add(paramFreez);

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




    private Collection<User> SearchUser(string numberOfRows, string pageIndex, string sortColumnName, string sortOrderBy, out int totalRecords, string userName, string password, string firstName, string lastName, string phone, string email, string advocacy, string freez)
    {
        userName = (userName == null) ? "" : userName;
        password = (password == null) ? "" : password;
        firstName = (firstName == null) ? "" : firstName;
        lastName = (lastName == null) ? "" : lastName;
        phone = (phone == null) ? "" : phone;
        email = (email == null) ? "" : email;
        advocacy = (advocacy == null) ? "" : advocacy;
        freez = (freez == null) ? "" : freez;
        //ng advocacy, string freez

        Collection<User> users = new Collection<User>();
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
                command.CommandText = "{CALL User_Search(?,?,?,?,?,?,?,?,?,?,?,?,?)}";
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

                OdbcParameter paramUserName = new OdbcParameter("@UserName", OdbcType.VarChar, 100);
                paramUserName.Value = userName;
                command.Parameters.Add(paramUserName);

                OdbcParameter paramPassword = new OdbcParameter("@Password", OdbcType.VarChar, 100);
                paramPassword.Value = password;
                command.Parameters.Add(paramPassword);

                OdbcParameter paramLastName = new OdbcParameter("@LastName", OdbcType.VarChar, 100);
                paramLastName.Value = lastName;
                command.Parameters.Add(paramLastName);

                OdbcParameter paramFirstName = new OdbcParameter("@FirstName", OdbcType.VarChar, 100);
                paramFirstName.Value = firstName;
                command.Parameters.Add(paramFirstName);

                OdbcParameter paramPhone = new OdbcParameter("@Phone", OdbcType.VarChar, 100);
                paramPhone.Value = phone;
                command.Parameters.Add(paramPhone);

                OdbcParameter paramEmail = new OdbcParameter("@Email", OdbcType.VarChar, 100);
                paramEmail.Value = email;
                command.Parameters.Add(paramEmail);

                OdbcParameter paramadvocacy = new OdbcParameter("@Advocacy", OdbcType.VarChar, 20);
                paramadvocacy.Value = advocacy;
                command.Parameters.Add(paramadvocacy);

                OdbcParameter paramFreez = new OdbcParameter("@Freez", OdbcType.VarChar, 30);
                paramFreez.Value = freez;
                command.Parameters.Add(paramFreez);

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
                        users.Add(user);
                    }
                }
                totalRecords = (int)paramTotalRecords.Value;
            }

            return users;
        }
    }
    
    
    private bool UpdateUser(int useroid,string userName, string password, string firstName, string lastName, string phone, string email,string advocacy,string freez)
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {                

                command.Connection = connection;
                command.CommandText = "{CALL User_Update(?,?,?,?,?,?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                OdbcParameter paramUserOID = new OdbcParameter("@UserOID", OdbcType.Int);
                paramUserOID.Value = useroid;
                command.Parameters.Add(paramUserOID);

                OdbcParameter paramUserName = new OdbcParameter("@UserName", OdbcType.VarChar, 100);
                paramUserName.Value = userName;
                command.Parameters.Add(paramUserName);

                OdbcParameter paramPassword = new OdbcParameter("@Password", OdbcType.VarChar, 100);
                paramPassword.Value = password;
                command.Parameters.Add(paramPassword);

                OdbcParameter paramLastName = new OdbcParameter("@LastName", OdbcType.VarChar, 100);
                paramLastName.Value = lastName;
                command.Parameters.Add(paramLastName);

                OdbcParameter paramFirstName = new OdbcParameter("@FirstName", OdbcType.VarChar, 100);
                paramFirstName.Value = firstName;
                command.Parameters.Add(paramFirstName);

                OdbcParameter paramPhone = new OdbcParameter("@Phone", OdbcType.VarChar, 100);
                paramPhone.Value = phone;
                command.Parameters.Add(paramPhone);

                OdbcParameter paramEmail = new OdbcParameter("@Email", OdbcType.VarChar, 100);
                paramEmail.Value = email;
                command.Parameters.Add(paramEmail);
                
                OdbcParameter paramAdvocacy = new OdbcParameter("@Advocacy", OdbcType.VarChar, 10);
                paramAdvocacy.Value = advocacy;
                command.Parameters.Add(paramAdvocacy);

                OdbcParameter paramFreez = new OdbcParameter("@Freez", OdbcType.VarChar, 10);
                paramFreez.Value = freez;
                command.Parameters.Add(paramFreez);

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

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL User_Delete(?)}";
                command.CommandType = CommandType.StoredProcedure;

                OdbcParameter paramUserOID = new OdbcParameter("@UserOID", OdbcType.Int);
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
        
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                command.Connection = connection;
                command.CommandText = "{CALL SelectjqGrid(?,?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;
 
                OdbcParameter paramPageIndex = new OdbcParameter("@PageIndex", OdbcType.Int);
                paramPageIndex.Value =Convert.ToInt32(pageIndex);
                command.Parameters.Add(paramPageIndex);
 
                OdbcParameter paramColumnName = new OdbcParameter("@SortColumnName", OdbcType.VarChar, 50);
                paramColumnName.Value = sortColumnName;
                command.Parameters.Add(paramColumnName);
 
                OdbcParameter paramSortorderBy = new OdbcParameter("@SortOrderBy", OdbcType.VarChar, 4);
                paramSortorderBy.Value = sortOrderBy;
                command.Parameters.Add(paramSortorderBy);
 
                OdbcParameter paramNumberOfRows = new OdbcParameter("@NumberOfRows", OdbcType.Int);
                paramNumberOfRows.Value =Convert.ToInt32(numberOfRows);
                command.Parameters.Add(paramNumberOfRows);
 
                OdbcParameter paramTotalRecords= new OdbcParameter("@TotalRecords", OdbcType.Int);
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
                        user.Advocacy = Convert.ToString(dataReader["Advocacy"]);
                        user.Freez = Convert.ToString(dataReader["Freez"]);
                        //string active = null;
                        //active = Convert.ToString(dataReader["Freez"]);
                        //if (active == "Yes")
                        //{
                        //    user.Freez = "No";
                        //}
                        //else
                        //{
                        //    user.Freez = "Yes";
                        //}
                        users.Add(user);
                    }
                }
                totalRecords = (int)paramTotalRecords.Value;
            }
             
            return users;
        }
 
    }

    


}