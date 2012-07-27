<%@ WebHandler Language="C#" Class="Result" %>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Script.Serialization;
using System.Data.Odbc;

public class Result : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    string connectionString = "";
    public void ProcessRequest (HttpContext context) 
    {
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();
        HttpRequest request = context.Request;
        HttpResponse response = context.Response;

        string _search = request["_search"];
        string SearchColName = "";
        string SearchVal = "";
        string bannerID = "";
        string program = "";
        string SortOrSearchFlag = "";
        string studentName = "";
        int count = 0;
        string MultipleSearch = "Yes";
        string isreport = request["isreport"];

        string numberOfRows = request["rows"];
        string pageIndex = request["page"];
        string sortColumnName = request["sidx"];
        string sortOrderBy = request["sord"];
        string operation = request["oper"];
        string field = request["searchField"];
        string val = request["searchString"];
        
        string reportname = request["reportname"];
        string hiddenfields = request["hiddenfields"];
        string notreport = request["notReport"];
        //string spparams = request.RawUrl.ToString();
        string spparams = request.Form.ToString();

        int aid = Convert.ToInt32(request["aid"]);
        int totalRecords;
        string query = request["query"];
        
        if (isreport == "yes")
        {
            _search = "true";
        }

        if (_search == "true")
        {
            if (reportname != "" || reportname != "0")
            {
                context.Session["reportname"] = reportname;
            }
            context.Session["numberOfRows"] = numberOfRows;
            context.Session["pageIndex"] = pageIndex;
            context.Session["sortColumnName"] = sortColumnName;
            context.Session["sortOrderBy"] = request["sord"];
            context.Session["totalRecords"] = 0;
            context.Session["BannerID"] = Convert.ToInt32("0");
            context.Session["NAME"] = request["NAME"];
            context.Session["StudentOID"] = request["StudentOID"];
            context.Session["TERM"] = request["TERM"];
            context.Session["FullPart"] = request["FullPart"];
            context.Session["GPA"] = Convert.ToString(request["GPA"]);
            context.Session["CreditAttempted"] = Convert.ToString(request["CreditAttempted"]);
            context.Session["EarnedCredit"] = Convert.ToString(request["EarnedCredit"]);
            context.Session["Prealgebra"] = request["Prealgebra"];
            context.Session["Algebra"] = request["Algebra"];
            context.Session["Writting"] = request["Writting"];
            context.Session["Reading"] = request["Reading"];
            context.Session["English"] = request["English"];
            context.Session["Math"] = request["Math"];
            context.Session["ReadingScore"] = request["ReadingScore"];
            context.Session["ScienceScore"] = request["ScienceScore"];
            context.Session["TestingDate"] = Convert.ToDateTime(request["TestingDate"]);
            context.Session["HighSchool"] = request["HighSchool"];
            context.Session["HS_GRAD_DATE"] = Convert.ToDateTime(request["HS_GRAD_DATE"]);
            context.Session["ADDR1"] = request["ADDR1"];
            context.Session["ADDR2"] = request["ADDR2"];
            context.Session["ADDR3"] = request["ADDR3"];
            context.Session["CITY"] = request["CITY"];
            context.Session["STATE"] = request["STATE"];
            context.Session["ZIP"] = request["ZIP"];
            context.Session["ImportDate"] = Convert.ToDateTime(request["ImportDate"]);
            context.Session["PPGMIND"] = request["PPGMIND"];
            context.Session["MAJOR"] = request["MAJOR"];
            context.Session["Email"] = request["Email"];
            context.Session["Phone"] = request["Phone"];
            
            if (notreport == "notReport")
            {
                SearchColName = ((System.Collections.Specialized.NameValueCollection)request.Form).AllKeys[9];
                SortOrSearchFlag = "Search";
                count = ((System.Collections.Specialized.NameValueCollection)request.Form).AllKeys.Length;
                MultipleSearch = SearchColName;
                SearchVal = request[SearchColName];
                for (int i = 10; i < count; i++)
                {

                    // MultipleSearch = MultipleSearch +","+ SearchColName;
                    MultipleSearch = MultipleSearch + ',' + ((System.Collections.Specialized.NameValueCollection)request.Form).AllKeys[i];

                    SearchVal = SearchVal + ',' + request[((System.Collections.Specialized.NameValueCollection)request.Form).AllKeys[i]];

                }
            }
            if (notreport == "Report")
            {
                
                
                string strParameter = HttpUtility.UrlDecode(query);
                SortOrSearchFlag = "Search";
                MultipleSearch = SearchColName;
                if (!strParameter.Equals(string.Empty))
                {
                    foreach (string str in strParameter.Substring(1).Split('&'))
                    {
                        string[] str1 = str.Split('=');

                        if (str1.Length > 0)
                        {
                            MultipleSearch = MultipleSearch + str1[0] + ",";
                            SearchVal = SearchVal + str1[1] + ",";
                        }
                    }
                    MultipleSearch = MultipleSearch.Substring(0, MultipleSearch.Length - 1);
                    SearchVal = SearchVal.Substring(0, SearchVal.Length - 1);
                }
                
            }
        }
        else
        {
            SortOrSearchFlag = "Sort";
        }
        
       

       
       
       

        if ((!string.IsNullOrEmpty(reportname)) && (reportname != ""))
        {
            string[] stringSeparatorsasc = new string[] { "&sord=asc" };
            string[] stringSeparatorsdesc = new string[] { "&sord=desc" };

            if (spparams.Contains("&sord=asc"))
            {
                spparams = spparams.Split(stringSeparatorsasc, StringSplitOptions.None)[1];
            }
            else if (spparams.Contains("&sord=desc"))
            {
                spparams = spparams.Split(stringSeparatorsdesc, StringSplitOptions.None)[1];
            }
            else
            {
                spparams = "";
            }

            bool status = InsertSearchStudentReport(aid,reportname, spparams, hiddenfields);
        }
        else
        {

            Assessment ass = new Assessment();
            string output = ass.GetAssessmentResult(aid, numberOfRows, pageIndex, out totalRecords, MultipleSearch, SearchVal, sortColumnName, sortOrderBy, bannerID, program, studentName, SortOrSearchFlag);
            //string output = ass.GetAssessmentResult(aid, numberOfRows, pageIndex, out totalRecords, sortColumnName, sortOrderBy, bannerID, program, studentName, field, val);
            response.Write(output);
        }
        
        
        
       
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
    private bool InsertSearchStudentReport(int aid,string reportname, string spparams, string gridcolumns)//, string numberOfRows, string pageIndex, string sortColumnName, string sortOrderBy, out int totalRecords, int StudentOID, string NAME, string ID, string TERM, string PFIND, double GPA, double ATMPCR, double EARNCR, string C1, string C2, string CW, string CR, string AE, string AM, string AR, string SA, DateTime SDATE, string HSDESC, DateTime HS_GRAD_DATE, string ADDR1, string ADDR2, string ADDR3, string CITY, string STATE, string ZIP, DateTime FILEDATE, string PPGMIND, string MAJOR, string Email, string Phone)
    {
        bool result = false;

        int reportOID = 0;
        String ReportName = (reportname == null) ? "" : reportname;
        String SPName = "";
        String SPParams = (spparams == null) ? "" : spparams;
        String GridColumns = (gridcolumns == null) ? "" : gridcolumns;
        DateTime CreatedDate = System.DateTime.Now;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                command.Connection = connection;
                command.CommandText = "{CALL Reports_Insert(?,?,?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                OdbcParameter returnParam = command.Parameters.Add("@ReportOID", OdbcType.Int);
                returnParam.Direction = ParameterDirection.ReturnValue;

                //command.Parameters.AddWithValue("@ReportOID",0);
                command.Parameters.AddWithValue("@ReportName", ReportName);
                command.Parameters.AddWithValue("@SPName", SPName);
                command.Parameters.AddWithValue("@SPParams", SPParams);
                command.Parameters.AddWithValue("@GridColumns", GridColumns);
                command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

                connection.Open();
                int n = command.ExecuteNonQuery();
                if (n == 1)
                    result = true;
                else
                    result = false;
                if (result)
                {
                    reportOID = (int)command.Parameters["@ReportOID"].Value;

                    //To Insert menu
                    CVTCMenu menu = new CVTCMenu();
                    menu.NameMenu = ReportName;
                    int menuId = new CVTCMenu().GetMaxMenuID();
                    menuId += 1;
                    menu.MenuID = menuId;
                    menu.URL = "pg/assessment/ResultReport.aspx?ReportOID=" + reportOID + "&aid=" + aid;
                   //menu.URL = "pg/assessment/ResultReport.aspx?ReportOID=" + reportOID ;
                    menu.MenuLevel = 1;
                    menu.Parent = 6;
                    menu.IsExpanded = "true";
                    menu.IsLeave = "true";
                    menu.SaveAssessmentMenuItem(menu);
                }
                else
                {
                    reportOID = 0;
                }
            }
        }
        return true;
    }
}