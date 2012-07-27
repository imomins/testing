<%@ WebHandler Language="C#" Class="MenuHandler" %>

using System;
using System.Web;
using System.Collections.ObjectModel;

public class MenuHandler : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");

        HttpRequest request = context.Request;
        HttpResponse response = context.Response;
        Collection<CVTCMenu> menuList = new Collection<CVTCMenu>();
        
        Collection<CVTCMenu> parentMenuList = null;
        Collection<CVTCMenu> childMenuList = null;
        Collection<CVTCMenu> subChildMenuList = null;
        Collection<CVTCMenu> subSChildMenuList = null;
        
        //XML Write
        response.ContentType = "application/xml";
        response.Write("<?xml version=\"1.0\"?>");
        CVTCMenu menu = new CVTCMenu();
        //menuList = menu.GetAllMenu();
        parentMenuList = menu.GetAllParentMenu();

        foreach (CVTCMenu m in parentMenuList)
        {
            menuList.Add(m);
            if ((m.IsLeave == "false"))
            {
                childMenuList = menu.GetAllMenuByParentID(m.MenuID.ToString());

                if (childMenuList.Count == 0)
                {
                    m.IsLeave = "true";
                    m.IsExpanded = "true";
                }
                
                foreach (CVTCMenu cm in childMenuList)
                {
                    menuList.Add(cm);
                    if (cm.IsLeave == "false")
                    {
                        subChildMenuList = menu.GetAllMenuByParentID(cm.MenuID.ToString());
                        foreach (CVTCMenu scm in subChildMenuList)
                        {
                            menuList.Add(scm);
                            if (scm.IsLeave == "false")
                            {
                                subSChildMenuList = menu.GetAllMenuByParentID(scm.MenuID.ToString());
                                foreach (CVTCMenu sscm in subSChildMenuList)
                                {
                                    menuList.Add(sscm);
                                }
                            }
                        }
                    }
           
                }   
            }
            
        }
        
         
        response.Write("<rows>  <page>1</page>  <total>1</total><records>1</records>");
        try
        {
            foreach (CVTCMenu m in menuList)
            {

                response.Write("<row> ");
                response.Write("<cell>" + m.MenuID.ToString() + "</cell>");
                response.Write("<cell>" + m.NameMenu + "</cell>");
                response.Write("<cell>" + HttpUtility.HtmlEncode( m.URL) + "</cell>");
                response.Write("<cell>" + m.MenuLevel.ToString() + "</cell>");
                if (m.Parent == 0) response.Write("<cell></cell>");
                else
                    response.Write("<cell>" + m.Parent.ToString() + "</cell>");
                response.Write("<cell>" + m.IsLeave + "</cell>");
                response.Write("<cell>" + m.IsExpanded + "</cell>");
                response.Write("</row>");
            }
            response.Write("</rows>");
        }
        catch (Exception ex)
        {
            
            
        }

    }

    private void WriteXML()
    { 
//        response.Write("  <?xml version="1.0" encoding="utf-8" ?><rows>  <row>        <cell>0</cell>    <cell>Car</cell>    <cell>Car</cell>    <cell>0</cell>    <cell></cell>    <cell>false</cell>    <cell>false</cell>  </row>    <row>    <cell>1</cell>    <cell>Honda</cell>    <cell>Honda</cell>    <cell>1</cell>    <cell>0</cell>    <cell>false</cell>    <cell>false</cell>  </row>    <row>    <cell>2</cell>    <cell>Civic</cell>    <cell>Civic</cell>    <cell>2</cell>    <cell>1</cell>    <cell>true</cell>    <cell>true</cell>  </row>  <row>    <cell>3</cell>    <cell>Cr-v</cell>    <cell>Cr-v</cell>    <cell>2</cell>    <cell>1</cell>    <cell>true</cell>    <cell>true</cell>  </row>  <row>    <cell>4</cell>    <cell>Hummer</cell>    <cell>Hummer</cell>    <cell>1</cell>    <cell>0</cell>    <cell>false</cell>    <cell>false</cell>  </row>  <row>    <cell>5</cell>    <cell>H2</cell>    <cell>H2</cell>    <cell>2</cell>    <cell>4</cell>    <cell>true</cell>    <cell>true</cell>  </row>  <row>    <cell>6</cell>    <cell>Ford</cell>    <cell>Ford</cell>    <cell>1</cell>    <cell>0</cell>    <cell>true</cell>    <cell>false</cell>  </row></rows>");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}