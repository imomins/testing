<%@ WebHandler Language="C#" Class="DomainHandler" %>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Script.Serialization;
using System.Data.Odbc;

public class DomainHandler : IHttpHandler {

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
        if (operation == "add")
        {
            AddDomain(request["DomainName"]);
        }
        else if (operation == "edit")
        {
            UpdateDomain(Convert.ToInt32(request["id"]), request["DomainName"]);
           
        }
        else if (operation == "del")
        {
            DeleteDomain(Convert.ToInt32(request["id"]));
        }
        else
        {
            int totalRecords;
            Collection<Domain> domains = GetDomain(numberOfRows, pageIndex, sortColumnName, sortOrderBy, out totalRecords);
            string output = BuildJQGridResults(domains, Convert.ToInt32(numberOfRows), Convert.ToInt32(pageIndex), Convert.ToInt32(totalRecords));

            response.Write(output);
        }
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    private string BuildJQGridResults(Collection<Domain > domains, int numberOfRows, int pageIndex, int totalRecords)
    {

        JQGridResults result = new JQGridResults();
        List<JQGridRow> rows = new List<JQGridRow>();
        foreach (Domain  domain in domains )
        {
            JQGridRow row = new JQGridRow();
            row.id = domain.DomainOID;
            row.cell = new string[2];
            row.cell[0] = domain.DomainOID.ToString();
            row.cell[1] = domain.DomainName;
            
            rows.Add(row);
        }
        result.rows = rows.ToArray();
        result.page = pageIndex;
        result.total = totalRecords / numberOfRows;
        if (totalRecords % numberOfRows != 0) result.total += 1;
        result.records = totalRecords;
        return new JavaScriptSerializer().Serialize(result);
    }


    private bool AddDomain(string domainName)
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                
                command.Connection = connection;
                command.CommandText = "{CALL Domain_insert(?)}";
                command.CommandType = CommandType.StoredProcedure;

                OdbcParameter paramdomainName = new OdbcParameter("@DomainName", OdbcType.VarChar, 100);
                paramdomainName.Value = domainName;
                command.Parameters.Add(paramdomainName);
               
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


   
    
    private bool UpdateDomain(int domainoid,string domainName)
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {                

                command.Connection = connection;
                command.CommandText = "{CALL Domain_Update(?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                OdbcParameter paramdomainoid = new OdbcParameter("@DomainOID", OdbcType.Int);
                paramdomainoid.Value = domainoid;
                command.Parameters.Add(paramdomainoid);

                OdbcParameter paramdomainName = new OdbcParameter("@DomainName", OdbcType.VarChar, 100);
                paramdomainName.Value = domainName;
                command.Parameters.Add(paramdomainName);

                
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

    private bool DeleteDomain(int DomainOID)
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Domain_Delete(?)}";
                command.CommandType = CommandType.StoredProcedure;

                OdbcParameter paramdomainOID = new OdbcParameter("@DomainOID", OdbcType.Int);
                paramdomainOID.Value = DomainOID;
                command.Parameters.Add(paramdomainOID);

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

    private Collection<Domain> GetDomain(string numberOfRows, string pageIndex, string sortColumnName, string sortOrderBy, out int totalRecords)
    {
        Collection<Domain> domains = new Collection<Domain>();

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                command.Connection = connection;
                command.CommandText = "{CALL SelectjqGrid_Domain(?,?,?,?,?)}";
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
                    Domain domain;
                    while (dataReader.Read())
                    {
                        domain = new Domain();
                        domain.DomainOID = (int)dataReader["DomainOID"];
                        domain.DomainName = Convert.ToString(dataReader["DomainName"]);
                        domains.Add(domain);
                    }
                }
                totalRecords = (int)paramTotalRecords.Value;
            }

            return domains;
        }

    }

    


}