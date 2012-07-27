<%@ WebHandler Language="C#" Class="TaskHandler" %>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Script.Serialization;
using System.Data.Odbc;


public class TaskHandler : IHttpHandler {
    string connectionString ;//= @"Data Source=.\SQLEXPRESS; Initial Catalog=cvtc; User ID=cvtcuser; Password=cvtcuser";
    
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
            //int totalRecords;
            //Collection<Task> Tasks = SearchUser(numberOfRows, pageIndex, sortColumnName, sortOrderBy, out totalRecords, request["UserName"], request["Password"], request["FirstName"], request["LastName"], request["Phone"], request["Email"]);
            //string output = BuildJQGridResults(Tasks, Convert.ToInt32(numberOfRows), Convert.ToInt32(pageIndex), Convert.ToInt32(totalRecords));

            //response.Write(output);
        }
        else if (operation == "add")
        {
            //AddUser(request["UserName"], request["Password"], request["FirstName"], request["LastName"], request["Phone"], request["Email"]);
        }
        else if (operation == "edit")
        {
          //  UpdateUser(Convert.ToInt32(request["id"].ToString()), request["UserName"], request["Password"], request["FirstName"], request["LastName"], request["Phone"], request["Email"]);
            //UpdateUser(1, request["UserName"], request["Password"], request["FirstName"], request["LastName"], request["Phone"], request["Email"]);
        }
        else if (operation == "del")
        {
           // DeleteUser(Convert.ToInt32(request["id"]));
        }
        else//(operation == null)
        {
            int totalRecords;
            Collection<Task> tasks = GetTasks(numberOfRows, pageIndex, sortColumnName, sortOrderBy, out totalRecords);
            string output = BuildJQGridResults(tasks, Convert.ToInt32(numberOfRows), Convert.ToInt32(pageIndex), Convert.ToInt32(totalRecords));

            response.Write(output);
        }
    }


    private Collection<Task> GetTasks(string numberOfRows, string pageIndex, string sortColumnName, string sortOrderBy, out int totalRecords)
    {
        Collection<Task> Tasks = new Collection<Task>();

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                command.Connection = connection;
                command.CommandText = "CALL Task_Select{0}(?,?,?,?,?)}";
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
                        task = new Task();
                        task.TaskOID = (int)dataReader["TaskOID"];
                        //task.AMOrPM = Convert.ToString(dataReader["AMOrPM"]);
                        task.CompletionDate = Convert.ToDateTime(dataReader["CompletionDate"]);
                        //task.CompletionTime = Convert.ToString(dataReader["CompletionTime"]);
                        task.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        task.MessageBody = Convert.ToString(dataReader["MessageBody"]);
                        task.Recipient = Convert.ToString(dataReader["Recipient"]);
                        task.SendFrom = Convert.ToString(dataReader["SendFrom"]);
                        task.Subject = Convert.ToString(dataReader["Subject"]);

                        Tasks.Add(task);
                    }
                }
                totalRecords = (int)paramTotalRecords.Value;
            }

            return Tasks;
        }

    }

    private string BuildJQGridResults(Collection<Task> Tasks, int numberOfRows, int pageIndex, int totalRecords)
    {

        JQGridResults result = new JQGridResults();
        List<JQGridRow> rows = new List<JQGridRow>();
        foreach (Task Task in Tasks)
        {
            JQGridRow row = new JQGridRow();
            row.id = Task.TaskOID;
            row.cell = new string[8];
            row.cell[0] = Task.TaskOID.ToString();
            row.cell[1] = "false";
            row.cell[2] = "";
            row.cell[3] = "";
            row.cell[4] = Task.Subject;
            row.cell[5] = Task.Recipient;
            row.cell[6] = Task.CreatedDate.ToShortDateString();
            row.cell[7] = Task.CreatedDate.ToShortTimeString();
            rows.Add(row);
        }
        result.rows = rows.ToArray();
        result.page = pageIndex;
        result.total = totalRecords / numberOfRows;
        if (totalRecords % numberOfRows != 0) result.total += 1;
        result.records = totalRecords;
        return new JavaScriptSerializer().Serialize(result);
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}