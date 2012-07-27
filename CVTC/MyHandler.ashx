<%@ WebHandler Language="C#" Class="MyHandler" %>

using System;
using System.Web;
using System.Collections.ObjectModel;

public class MyHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) 
    {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        HttpRequest request = context.Request;
        HttpResponse response = context.Response;
        ////string start=request["start"].ToString();
        //EventList("1","2");
        response.ContentType = "application/xml";
        response.Write("<?xml version=\"1.0\"?>");
        response.Write("<rows><page>1</page><total>1</total><records>1</records><row><cell>1</cell><cell>Home</cell><cell>home.aspx</cell><cell>0</cell><cell>1</cell><cell>2</cell><cell>true</cell>s<cell>true</cell></row>");
        response.Write("<row>    <cell>2</cell>    <cell>Student/Course Database</cell>    <cell/>    <cell>0</cell>    <cell>3</cell>    <cell>8</cell>    <cell>false</cell>    <cell>false</cell>  </row>  <row>    <cell>3</cell>    <cell>Student</cell>    <cell>pg/student/student.aspx</cell>    <cell>1</cell>    <cell>4</cell>    <cell>5</cell>    <cell>true</cell>    <cell>true</cell>  </row>  <row>    <cell>4</cell>    <cell>Course</cell>    <cell>pg/student/course.aspx</cell>    <cell>1</cell>    <cell>6</cell>    <cell>7</cell>    <cell>true</cell>    <cell>true</cell> </row></rows>");

        //response.End();

    }
    public string EventList(string start, string end)//string startDat, string endDat//DateTime startDate, DateTime endDate
    {

        string startDate = ToUnixTimespan(System.DateTime.Now).ToString();
        string endDate = ToUnixTimespan(DateTime.Now.AddDays(1)).ToString();
        DateTime starting = FromUnixTimespan(startDate);

        Collection<CalendarDTO> events = new Collection<CalendarDTO>();
        System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();

        // System.DateTime starting=FromUnixTimespan(startDate);
        for (int i = 0; i < 4; i++)
        {
            CalendarDTO e = new CalendarDTO();

            e.id = i;
            e.title = "Tittle " + i.ToString();
            e.StartDate = ToUnixTimespan(starting.AddDays(i * 2));//FromUnixTimespan( System.DateTime.Now.AddDays(i + 1));//ToUnixTimespan(starting.AddDays(i * 2));//
            //e.EndDate = //ToUnixTimespan(e.StartDate.AddDays(1 + (i * 3)))//System.DateTime.Now.AddDays(i + 1);
            //if (i % 2 == 1)
            //{
            //    e.EndDate = ToUnixTimespan(starting.AddDays(1 + (i * 3)));
            //}

            events.Add(e);

        }

        return js.Serialize(events);
        //string startDate = ToUnixTimespan(System.DateTime.Now).ToString();
        //string endDate = ToUnixTimespan(DateTime.Now.AddDays(1)).ToString();

        //List<CalendarDTO> events = new List<CalendarDTO>();

        //DateTime starting = FromUnixTimespan(startDate);
        //for (int i = 0; i <= 4; i++)
        //{
        //    CalendarDTO value = new CalendarDTO();
        //    value.StartDate = ToUnixTimespan(starting.AddDays(i * 2));
        //    value.id = i;
        //    value.title = "Title of event number " + i.ToString();
        //    //value.editable = true;

        //    if (i % 2 == 1)
        //    {
        //        value.end = ToUnixTimespan(starting.AddDays(1 + (i * 3)));
        //    }
        //    events.Add(value);
        //}
        //System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
        //Response.Clear();
        //Response.Write(js.Serialize(events));
        //Response.End();

    }

    private Int64 ToUnixTimespan(DateTime d)
    {
        TimeSpan time = new TimeSpan();
        time = d.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0));

        return (Int64)Math.Truncate(time.TotalSeconds);
    }

    private DateTime FromUnixTimespan(string s)
    {
        DateTime time = new DateTime(1970, 1, 1, 0, 0, 0);
        return time.AddSeconds(Int32.Parse(s));
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}