<%@ WebHandler Language="C#" Class="InterventionHandler" %>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Script.Serialization;
using System.Data.Odbc;

public class InterventionHandler : IHttpHandler {
    //string connectionString = @"Data Source=.AETHER\\SQLEXPRESS; Initial Catalog=cvtc; User ID=cvtcuser; Password=cvtcuser";
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

        int totalRecords;
      //  Collection<Interventions> ints = GetIntervention(numberOfRows, pageIndex, sortColumnName, sortOrderBy, out totalRecords);
       // string output = BuildJQGridResults(ints, Convert.ToInt32(numberOfRows), Convert.ToInt32(pageIndex), Convert.ToInt32(totalRecords));

       // response.Write(output);   
        
    }

    private Collection<Interventions> GetCourse(string numberOfRows, string pageIndex, string sortColumnName, string sortOrderBy, out int totalRecords)
    {
        Collection<Interventions> ints = new Collection<Interventions>();

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                command.Connection = connection;
                command.CommandText = "{CALL Interventions_SelectjqGrid(?,?,?,?,?)}";
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
                    Interventions u = null;
                    while (dataReader.Read())
                    {
                        u = new Interventions();
                        u.PrescriptionOID = (int)dataReader["PrescriptionOID"];
                        u.StudentOID = (int)dataReader["StudentOID"];
                        u.UserOID = (int)dataReader["UserOID"];
                        u.DomainOID = (int)dataReader["DomainOID"];
                        u.InterventionOID = (int)dataReader["InterventionOID"];
                        if (dataReader["LatestActionDate"] != null)
                        {
                            u.LatestActionDate = Convert.ToDateTime(dataReader["LatestActionDate"]).ToShortDateString();
                        }
                        if (dataReader["LatestContact"] != null)
                        {
                            u.LatestContact = Convert.ToDateTime(dataReader["LatestContact"]).ToShortDateString();
                        }
                        u.Internal = (int)dataReader["Internal"];
                        u.Prescribed = (int)dataReader["Prescribed"];
                        u.Completed = (int)dataReader["Completed"];
                        u.Email = (int)dataReader["Email"];
                        u.Telephone = (int)dataReader["Telephone"];
                        u.InPerson = (int)dataReader["InPerson"];
                        u.HandOff = (int)dataReader["HandOff"];
                        if (dataReader["CreatedDate"] != null)
                        {
                            u.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]).ToShortDateString();
                        }
                        u.CreatedBy = (int)dataReader["CreatedBy"];
                        if (dataReader["LastModifiedDate"] != null)
                        {
                            u.LastModifiedDate = Convert.ToDateTime(dataReader["LastModifiedDate"]).ToShortDateString();
                        }
                        u.LastModifiedBy = (int)dataReader["LastModifiedBy"];
                        ints.Add(u);
                    }
                }
                totalRecords = (int)paramTotalRecords.Value;
            }

            return ints;
        }

    }

    //private string BuildJQGridResults(Collection<Interventions> ints, int numberOfRows, int pageIndex, int totalRecords)
    //{

        //JQGridResults result = new JQGridResults();
        //List<JQGridRow> rows = new List<JQGridRow>();
        //foreach (Interventions  i in ints)
        //{
        //    JQGridRow row = new JQGridRow();
        //    row.id = i.PrescriptionOID;
        //    row.cell = new string[10];
        //    row.cell[0] = i.PrescriptionOID.ToString();
        //    row.cell[1] = course.BannerStudentName;
        //    row.cell[2] = course.BannerStudentIDNumber;
        //    row.cell[3] = course.TermCodeofProgramEnrollment;
        //    row.cell[4] = course.CourseNumber;
        //    row.cell[5] = course.CourseTitle;
        //    row.cell[6] = course.FinalGrade;
        //    row.cell[7] = course.TermCodeOfCourseEnrollment;
        //    row.cell[8] = course.MethodOfDelivery;
        //    row.cell[9] = course.ImportDate.ToShortDateString();
        //    rows.Add(row);
        //}
        //result.rows = rows.ToArray();
        //result.page = pageIndex;
        //result.total = totalRecords / numberOfRows;
        //if (totalRecords % numberOfRows != 0) result.total += 1;
        //result.records = totalRecords;
        //return new JavaScriptSerializer().Serialize(result);
    //}

    
    public bool IsReusable {
        get {
            return false;
        }
    }

}