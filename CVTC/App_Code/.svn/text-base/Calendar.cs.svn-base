//using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.ObjectModel;

// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
[System.Web.Script.Services.ScriptService()]
[System.Web.Services.WebService(Namespace = "http://tempuri.org/")]
[System.Web.Services.WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ToolboxItem(true)]
public class Calendar : System.Web.Services.WebService
{

    [WebMethod()]
    public string EventList(string startDate, string endDate)
    {
        List<CalendarDTO> events=null;
        try
        {
            DateTime start, end;
            start = FromUnixTimespan(startDate);
            end = FromUnixTimespan(endDate);
            Task task = new Task();
            Collection<Task> taskList = task.GetTaskByStartAndEndDate(start, end);
             //List to hold events
            events = new List<CalendarDTO>();

            DateTime starting = FromUnixTimespan(startDate);
             CalendarDTO value;
            // Loop through events to be added
            //for (int i = 0; i <= 4; i++)
             foreach (Task t in taskList)
            {
                // Create a new event and start to populate
                value = new CalendarDTO();
                // Date is required to be in a unix format
                value.StartDate = ToUnixTimespan(t.CompletionDate);
                value.EndDate = ToUnixTimespan(t.CompletionDate);
                value.id = t.TaskOID;
                value.title =t.Subject ;
                
                events.Add(value);
            }
        }
        catch (Exception ex)
        { }
        // Serialize the return value so it can be decoded in java.
        System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
        return js.Serialize(events);
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


}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Services;

///// <summary>
///// Summary description for Calendar
///// </summary>
//[WebService(Namespace = "http://tempuri.org/")]
//[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
//public class Calendar : System.Web.Services.WebService {

//    public Calendar () {

//        //Uncomment the following line if using designed components 
//        //InitializeComponent(); 
//    }

//    [WebMethod]
//    public string HelloWorld() {
//        return "Hello World";
//    }
    
//}

