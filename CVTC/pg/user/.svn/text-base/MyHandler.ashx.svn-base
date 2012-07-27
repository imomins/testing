<%@ WebHandler Language="C#" Class="MyHandler" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

using System.Web.Script.Serialization;

public class MyHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
       // context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        HttpRequest request = context.Request;
        HttpResponse response = context.Response;
        
        CVTCMenu menu = new CVTCMenu();
        Collection<CVTCMenu> MenuList=null;
        if (request["nodeid"] == null)
        {
            MenuList = menu.GetAllMenu();
        }
        else
        {
            MenuList = menu.GetAllMenuByParentID(request["nodeid"].ToString());
        }

        int totalRecords = MenuList.Count;
        string output = menu.BuildJQGridResults(MenuList, Convert.ToInt32(1), Convert.ToInt32(1), Convert.ToInt32(totalRecords));
        response.Write(output);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}