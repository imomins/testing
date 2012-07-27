using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data.Odbc;

/// <summary>
/// Summary description for Report
/// </summary>
[Serializable]
public class Report
{
//    string connectionString = @"Data Source=.\SQLEXPRESS; Initial Catalog=cvtc; User ID=cvtcuser; Password=cvtcuser";
    string connectionString;
	public Report()
	{
		//
		// TODO: Add constructor logic here
		//
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString(); 
	}

    //OID, ColModel, ColNames, CurrentData, ReportName, TableName, CreatedDate
    public int OID
    { get; set; }

    public string ColModel
    { get; set; }

    public string ColNames
    { get; set; }

    public string CurrentData
    { get; set; }

    public string ReportName
    { get; set; }

    public string TableName
    { get; set; }

    public bool InsertReport()
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {                

                command.Connection = connection;
                command.CommandText = "{CALL Report_insert(?,?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                OdbcParameter paramColModel = new OdbcParameter("@ColModel", OdbcType.VarChar, 5000);
                paramColModel.Value = this.ColModel;
                command.Parameters.Add(paramColModel);

                OdbcParameter paramColNames = new OdbcParameter("@ColNames", OdbcType.VarChar, 500);
                paramColNames.Value = this.ColNames;
                command.Parameters.Add(paramColNames);

                OdbcParameter paramCurrentData = new OdbcParameter("@CurrentData", OdbcType.VarChar, 5000);
                paramCurrentData.Value = this.ReportName;
                command.Parameters.Add(paramCurrentData);

                OdbcParameter paramReportName = new OdbcParameter("@ReportName", OdbcType.VarChar, 200);
                paramReportName.Value = this.ReportName;
                command.Parameters.Add(paramReportName);


                OdbcParameter paramTableName = new OdbcParameter("@TableName", OdbcType.VarChar, 50);
                paramTableName.Value = this.TableName;
                command.Parameters.Add(paramTableName);              
                
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
