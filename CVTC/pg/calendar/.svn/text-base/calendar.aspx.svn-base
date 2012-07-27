<%@ Page Language="C#" AutoEventWireup="true" CodeFile="calendar.aspx.cs" Inherits="pg_calendar_calendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Calendar</title>
    <link rel='stylesheet' type='text/css' href="../../themes/cupertino/theme.css" />
    <link rel='stylesheet' type='text/css' href="../../css/fullcalendar.css" />
    <link rel='stylesheet' type='text/css' href="../../themes/fullcalendar.print.css" media='print' />
    <script src="../../js/jquery-1.4.2.js" type="text/javascript"></script>
    <script src="../../js/fullcalendar.js" type="text/javascript"></script>    
    <script src="../../js/json2.js" type="text/javascript"></script>
    <script src="../../js/thickbox.js" type="text/javascript"></script>
    <link href="../../themes/thickbox.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript'>

        $(document).ready(function() {

            var date = new Date();
            var d = date.getDate();
            var m = date.getMonth();
            var y = date.getFullYear();

            $('#calendar').fullCalendar({
                theme: true,
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay'
                },
                editable: false,
                events: "../../Calendar.asmx/EventList",


                eventClick: function(calEvent, jsEvent, view) {
                    //alert("Hello Calendar");
                
                var tmp = $("#popupid").attr("href");
                tmp = tmp.substring(0, 25) + "taskOID=" + calEvent.id + "&" + tmp.substring(26, tmp.length);
                
                $("#popupid").attr("href", tmp);
                
                $("#popupid").click();
                
                }

            });

        });

</script>
    
    <script type='text/javascript'>
        $(document).ready(function() {                  
            $("#ButtonRefresh").click(function() {             
            $('#calendar').fullCalendar( 'refetchEvents' );            
            });            
      });
    </script>
    <style type='text/css'>

	body {
		margin-top: 40px;
		text-align: center;
		font-size: 13px;
		font-family: "Lucida Grande",Helvetica,Arial,Verdana,sans-serif;
		}

	#calendar {
		width: 900px;
		margin: 0 auto;
		}

</style>

</head>
<body>
    <form id="form1" runat="server">
    <button id="ButtonRefresh" class="ui-button ui-button-text-only ui-widget ui-state-default ui-corner-all" type="button" ><span class="ui-button-text">Refresh</span></button>
    <asp:HiddenField ID="HiddenFieldCurrentUser" runat="server" />    
    <a href="../task/DisplayTask.aspx?keepThis=true&TB_iframe=true&height=460&width=800" class="thickbox" id="popupid" style="display:none;" >Dislay</a>
   <div id='calendar'></div>

    </form>
</body>
</html>
