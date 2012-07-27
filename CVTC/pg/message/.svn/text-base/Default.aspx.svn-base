<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="pg_message_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.3/jquery.min.js" type="text/javascript"></script>

     <script type ="text/javascript">

         $(document).ready(function() {



             // default text values go here 
             document.getElementById('txt').value = '1982, SLC, LAX\n1987, SLC, RENO\n2009, SLC, DENVER\n 7777, DENVER, FLORIDA\n';
             // Wire Button onclick event
             document.getElementById('btn').onclick = show;

             $('#<%=Button1.ClientID %>').click(function() {

                
                 $.ajax({
                     type: "POST",
                     url: "Default.aspx/ServerSideMethod",
                     data: "{'param1':4}",
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     async: true,
                     cache: false,
                     success: function(msg) {
                         $('#myDiv').text(msg.d);

                     }

                 })

                 return false;

             });

             function show() {
                 var result = parseCSV(document.getElementById('txt').value, ',', '"', false);
                 var jsonData = JSON.stringify(result);

                 $.ajax({
                     type: "POST",
                     url: "Default.aspx/ServerSideMethod",
                     data: "{'param1':jsonData}",
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     async: true,
                     cache: false,
                     success: function(msg) {
                         $('#myDiv').text(msg.d);

                     }

                 })

              

             }




             function parseCSV(csvString) {
                 var fieldEndMarker = /([,\015\012] *)/g; /* Comma is assumed as field separator */
                 var qFieldEndMarker = /("")*"([,\015\012] *)/g; /* Double quotes are assumed as the quote character */
                 var startIndex = 0;
                 var records = [], currentRecord = [];
                 do {
                     // If the to-be-matched substring starts with a double-quote, use the qFieldMarker regex, otherwise use fieldMarker. 
                     var endMarkerRE = (csvString.charAt(startIndex) == '"') ? qFieldEndMarker : fieldEndMarker;
                     endMarkerRE.lastIndex = startIndex;
                     var matchArray = endMarkerRE.exec(csvString);
                     if (!matchArray || !matchArray.length) {
                         break;
                     }
                     var endIndex = endMarkerRE.lastIndex - matchArray[matchArray.length - 1].length;
                     var match = csvString.substring(startIndex, endIndex);
                     if (match.charAt(0) == '"') { // The matching field starts with a quoting character, so remove the quotes 
                         match = match.substring(1, match.length - 1).replace(/""/g, '"');
                     }
                     currentRecord.push(match);
                     var marker = matchArray[0];
                     if (marker.indexOf(',') < 0) { // Field ends with newline, not comma 
                         records.push(currentRecord);
                         currentRecord = [];
                     }
                     startIndex = endMarkerRE.lastIndex;
                 } while (true);
                 if (startIndex < csvString.length) { // Maybe something left over? 
                     var remaining = csvString.substring(startIndex).trim();
                     if (remaining) currentRecord.push(remaining);
                 }
                 if (currentRecord.length > 0) { // Account for the last record 
                     records.push(currentRecord);
                 }
                 return records;
             };






         });

     </script>
     
     <script type="text/javascript">


         
         
                                
function txt_onclick() {

}

        </script>
</head>
<body>

<button id="btn">Does not work</button>


<br/><br />   
<textarea id="txt" rows="6" cols="80" onclick="return txt_onclick()"> 
</textarea> 
<br/>
<div id="shown"> </div>
    <form id="form1" runat="server">
    <div>
    <asp:Button ID="Button1" runat="server" Text="This works" />
    <br />
    <br />
    <div id="myDiv">
    
    
    
    </div>
    </div>
    </form>
</body>
</html>