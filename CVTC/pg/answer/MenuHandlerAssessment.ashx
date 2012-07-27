<%@ WebHandler Language="C#" Class="MenuHandlerAssessment" %>

using System;
using System.Web;
using System.Collections.ObjectModel;

public class MenuHandlerAssessment : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");

        HttpRequest request = context.Request;
        HttpResponse response = context.Response;
        

        //XML Write
        response.ContentType = "application/xml";
        response.Write("<?xml version=\"1.0\"?>");
        Assessment ass = new Assessment();
        //assList = ass.GetAllAssessmnet();
        
        int aid = Convert.ToInt32(request["aid"]);
        ass = ass.GetAssessmentByOID(aid);
        

        int count = 1;
        response.Write("<rows>  <page>1</page>  <total>1</total><records>1</records>");
        foreach (Section s in ass.SectionList)
        {

            response.Write("<row> ");
            response.Write("<cell>" + count++.ToString() + "</cell>");
            response.Write("<cell>" + s.SectionName + "</cell>");
            response.Write("<cell>" + "QuestionSheet.aspx?sid="+s.SectionOID.ToString() + "</cell>");
            response.Write("<cell>" + "1" + "</cell>");
            //if (m.Parent == 0) response.Write("<cell></cell>");
            //else
                response.Write("<cell>" + "0" + "</cell>");
            response.Write("<cell>" + "true" + "</cell>");
            response.Write("<cell>" + "true" + "</cell>");
            response.Write("</row>");
        }
        response.Write("</rows>");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}