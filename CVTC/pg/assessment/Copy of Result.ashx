<%@ WebHandler Language="C#" Class="Result" %>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Script.Serialization;
using System.Data.Odbc;

public class Result : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) 
    {
        
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
        if (_search == "true")
        {
            SearchColName = ((System.Collections.Specialized.NameValueCollection)request.Form).AllKeys[6];
            SortOrSearchFlag = "Search";
            count = ((System.Collections.Specialized.NameValueCollection)request.Form).AllKeys.Length;
            MultipleSearch = SearchColName;
            SearchVal = request[SearchColName];
           for(int i=7;i<count ;i++)
           {

              // MultipleSearch = MultipleSearch +","+ SearchColName;
               MultipleSearch = MultipleSearch + ',' + ((System.Collections.Specialized.NameValueCollection)request.Form).AllKeys[i];

               SearchVal = SearchVal +','+ request[((System.Collections.Specialized.NameValueCollection)request.Form).AllKeys[i]];
              
            }
        }
        else
        {
            SortOrSearchFlag = "Sort";
        }
        
        string numberOfRows = request["rows"];
        string pageIndex = request["page"];
        string sortColumnName = request["sidx"];
        string sortOrderBy = request["sord"];
        string operation = request["oper"];
        string field = request["searchField"];
        string val = request["searchString"];
        
        int aid = Convert.ToInt32(request["aid"]);
        int totalRecords;

        
        
        
        

        Assessment ass = new Assessment();
        string output = ass.GetAssessmentResult(aid, numberOfRows, pageIndex, out totalRecords, MultipleSearch, SearchVal, sortColumnName, sortOrderBy, bannerID, program, studentName, SortOrSearchFlag);
        //string output = ass.GetAssessmentResult(aid, numberOfRows, pageIndex, out totalRecords, sortColumnName, sortOrderBy, bannerID, program, studentName, field, val);
        response.Write(output);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}