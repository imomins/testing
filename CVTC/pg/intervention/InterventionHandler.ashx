<%@ WebHandler Language="C#" Class="InterventionHandler" %>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Script.Serialization;
using System.Data.Odbc;

public class InterventionHandler : IHttpHandler
{

    //string connectionString= @"Data Source=.AETHER\\SQLEXPRESS; Initial Catalog=cvtc; User ID=cvtcuser; Password=cvtcuser";
    string connectionString = "";
    public void ProcessRequest (HttpContext context) 
    {
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

        if (operation == "add")
        {
            AddIntervention(request["InterventionName"]);
        }
        else if (operation == "edit")
        {
            UpdateIntervention(Convert.ToInt32(request["id"]), request["InterventionName"]);
           
        }
        else if (operation == "del")
        {
            DeleteIntervention(Convert.ToInt32(request["id"]));
        }
        else
        {
            int totalRecords;
            Collection<Interventions> interventions = GetIntervention(numberOfRows, pageIndex, sortColumnName, sortOrderBy, out totalRecords);
            string output = BuildJQGridResults(interventions, Convert.ToInt32(numberOfRows), Convert.ToInt32(pageIndex), Convert.ToInt32(totalRecords));

            response.Write(output);
        }
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    private string BuildJQGridResults(Collection<Interventions> interventions, int numberOfRows, int pageIndex, int totalRecords)
    {

        JQGridResults result = new JQGridResults();
        List<JQGridRow> rows = new List<JQGridRow>();
        foreach (Interventions intervention in interventions)
        {
            JQGridRow row = new JQGridRow();
            row.id = intervention.InterventionOID;
            row.cell = new string[3];
            row.cell[0] = intervention.InterventionOID.ToString();
            row.cell[1] = intervention.DomainName;
            row.cell[2] = intervention.InterventionName;
            
            rows.Add(row);
        }
        result.rows = rows.ToArray();
        result.page = pageIndex;
        result.total = totalRecords / numberOfRows;
        if (totalRecords % numberOfRows != 0) result.total += 1;
        result.records = totalRecords;
        return new JavaScriptSerializer().Serialize(result);
    }


    private bool AddIntervention(string InterventionName)
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                
                command.Connection = connection;
                command.CommandText = "{CALL Intervention_insert(?)}";
                command.CommandType = CommandType.StoredProcedure;

                OdbcParameter paramInterventionName = new OdbcParameter("@InterventionName", OdbcType.VarChar, 100);
                paramInterventionName.Value = InterventionName;
                command.Parameters.Add(paramInterventionName);
               
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




    private bool UpdateIntervention(int Interventionoid, string InterventionName)
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {                

                command.Connection = connection;
                command.CommandText = "{CALL Intervention_Update(?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                OdbcParameter paramInterventionoid = new OdbcParameter("@InterventionOID", OdbcType.Int);
                paramInterventionoid.Value = Interventionoid;
                command.Parameters.Add(paramInterventionoid);

                OdbcParameter paramInterventionName = new OdbcParameter("@InterventionName", OdbcType.VarChar, 100);
                paramInterventionName.Value = InterventionName;
                command.Parameters.Add(paramInterventionName);

                
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

    private bool DeleteIntervention(int InterventionOID)
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Intervention_Delete(?)}";
                command.CommandType = CommandType.StoredProcedure;

                OdbcParameter paramInterventionOID = new OdbcParameter("@InterventionOID", OdbcType.Int);
                paramInterventionOID.Value = InterventionOID;
                command.Parameters.Add(paramInterventionOID);

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

    private Collection<Interventions> GetIntervention(string numberOfRows, string pageIndex, string sortColumnName, string sortOrderBy, out int totalRecords)
    {
        Collection<Interventions> interventions = new Collection<Interventions>();

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                command.Connection = connection;
                command.CommandText = "{CALL SelectjqGrid_Intervention(?,?,?,?,?)}";
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

                connection.Open();
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    Interventions intervention;
                    while (dataReader.Read())
                    {
                        intervention = new Interventions();
                        intervention.InterventionOID = (int)dataReader["InterventionOID"];
                        intervention.DomainName = Convert.ToString(dataReader["DomainName"]);
                        intervention.InterventionName = Convert.ToString(dataReader["InterventionName"]);
                        
                        interventions.Add(intervention);
                    }
                }
                totalRecords = (int)paramTotalRecords.Value;
            }

            return interventions;
        }

    }

    


}