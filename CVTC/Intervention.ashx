<%@ WebHandler Language="C#" Class="Intervention" %>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Script.Serialization;

public class Intervention : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
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
        string UserOID = request["ioid"];

        int totalRecords;
        Interventions inv = new Interventions();
        Collection<Interventions> invList = inv.GetAllIntervention(numberOfRows, pageIndex, sortColumnName, sortOrderBy, out totalRecords, Convert.ToInt32(UserOID));
        string output = BuildJQGridResults(invList, Convert.ToInt32(numberOfRows), Convert.ToInt32(pageIndex), Convert.ToInt32(totalRecords));

        response.Write(output);
    }

    private string BuildJQGridResults(Collection<Interventions> Tasks, int numberOfRows, int pageIndex, int totalRecords)
    {

        JQGridResults result = new JQGridResults();
        List<JQGridRow> rows = new List<JQGridRow>();
        foreach (Interventions Task in Tasks)
        {
            JQGridRow row = new JQGridRow();
            row.id = Task.PrescriptionOID;
            row.cell = new string[5];
            row.cell[0] = Task.PrescriptionOID.ToString();
            row.cell[1] = Task.LatestActionDate;
            row.cell[2] = Task.LatestContact;
            row.cell[3] = Task.DomainName;
            row.cell[4] = Task.Comment;
            
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