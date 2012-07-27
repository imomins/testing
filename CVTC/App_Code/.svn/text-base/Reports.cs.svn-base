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
public class Reports
{
    string connectionString;
	public Reports()
	{
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString(); 
	}

    public int ReportOID
    { get; set; }

    public string ReportName
    { get; set; }

    public string SPName
    { get; set; }

    public string SPParams
    { get; set; }

    public string GridColumns
    { get; set; }

    public DateTime CreatedDate
    { get; set; }

    public bool InsertReport()
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {                

                command.Connection = connection;
                command.CommandText = "{CALL Reports_Insert(?,?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                OdbcParameter paramColModel = new OdbcParameter("@ReportName", OdbcType.VarChar, 100);
                paramColModel.Value = this.ReportName;
                command.Parameters.Add(paramColModel);

                OdbcParameter paramColNames = new OdbcParameter("@SPName", OdbcType.VarChar, 1000);
                paramColNames.Value = this.SPName;
                command.Parameters.Add(paramColNames);

                OdbcParameter paramCurrentData = new OdbcParameter("@SPParams", OdbcType.VarChar, 2000);
                paramCurrentData.Value = this.SPParams;
                command.Parameters.Add(paramCurrentData);

                OdbcParameter paramReportName = new OdbcParameter("@GridColumns", OdbcType.VarChar, 2000);
                paramReportName.Value = this.GridColumns;
                command.Parameters.Add(paramReportName);


                OdbcParameter paramTableName = new OdbcParameter("@CreatedDate", OdbcType.DateTime);
                paramTableName.Value = this.CreatedDate;
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


    public Reports GetReportByOID(int ReportOID)
    {
        Reports report = null;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                command.Connection = connection;
                command.CommandText = "{CALL Reports_GetByOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ReportOID", ReportOID);

                connection.Open();
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        report = new Reports();
                        report.ReportOID = (int)dataReader["ReportOID"];
                        report.ReportName = (string)dataReader["ReportName"];
                        report.SPName = (string)dataReader["SPName"];
                        report.SPParams = (string)dataReader["SPParams"];
                        report.GridColumns = (string)dataReader["GridColumns"];
                        report.CreatedDate = (DateTime)dataReader["CreatedDate"];

                        if (dataReader["CreatedDate"] != null)
                        {
                            report.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        }
                        else
                        {
                            report.CreatedDate = System.DateTime.Now;
                        }
                    }
                }

            }
        }
        return report;
    }

}
