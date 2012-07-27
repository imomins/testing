    <%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResultReport.aspx.cs" Inherits="pg_assessment_Result" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Result </title>
    <link rel="stylesheet" type="text/css" media="screen" href="../../themes/redmond/jquery-ui-1.8.1.custom.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../themes/ui.jqgrid.css" />

    <script src="../../js/jquery.js" type="text/javascript"></script> 
    <script src="../../js/grid.locale-en.js" type="text/javascript"></script> 
    <script src="../../js/jquery.jqGrid.min.js" type="text/javascript"></script>
    
      <script type="text/javascript">

 jQuery(document).ready(function() {
    $('#btnRefresh').click(function() {
           
            $('#ResultsGrid').trigger("reloadGrid");
            });
 });
</script>




  
 
</head>
<body>

<input id="HiddenAOID" runat="server" name="HiddenAOID" type="hidden" value="0" />
    <form id="form1" runat="server">
    <%--<button id="ColSectionButton" class="ui-button ui-button-text-only ui-widget ui-state-default ui-corner-all" type="button" ><span class="ui-button-text">Column Selection</span></button>--%>
    <asp:Label ID="lblError" runat="server" Font-Names="Calibri" Font-Size="10pt" 
        ForeColor="Red"></asp:Label>
   <table id="ResultsGrid" cellpadding="0" cellspacing="0">
    </table>     
    <div id="ResultsPager">
    </div>  
    
   <script type="text/javascript">

       function checkit() {
           var hiddenColumns = $("#HiddenColumns").val();
          // alert(hiddenColumns);
           hiddenColumns = hiddenColumns.split("&");
           for (x in hiddenColumns) {
               var namevalue = hiddenColumns[x].split("=");
               if (namevalue[1] == "true") {
                   $("#ResultsGrid").hideCol(namevalue[0]);
               }
           }
           $("#ResultsGrid").trigger("reloadGrid");
       }
       $(function() {

           var aid = $("#HiddenAOID").val();
           var t = "{ 'aoid': '" + aid + "'} ";
           $.ajax({

               type: "POST",
               url: "ResultReport.aspx/GetColumnNameList",
               data: t,
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               async: true,
               cache: false,
               //width:'1080',
               success: function(msg) {
                   var data = msg.d.split(", ");
                   //alert(data);
                   var tmp = "";
                   var colName = [];
                   var colMod = [];

                   for (var i = 0; i < data.length; i++) {
                       if (data[i] == 'AssessmentOID') {
                           tmp = { name: data[i], index: data[i], width: 80, hidden: true, editable: true,sortable:true ,editrules: { required: true} };
                       }
                       else if (data[i] == 'StudentOID') {
                       tmp = { name: data[i], index: data[i], width: 80, hidden: true, editable: true, sortable: true, editrules: { required: true} };
                       }
                       else {
                           if (i > 6) {



                               tmp = { name: data[i], index: data[i], width: 40, hidden: false, editable: true, sortable: true, editrules: { required: true} };

                           }
                           else {


                               if (data[i] == 'NumberOfPrinted' || data[i] == 'Flag') {
                                   tmp = { name: data[i], index: data[i], width: 65, hidden: false, frozen: true, sortable: true, editable: false, search: false, sortable: false, editrules: { required: true} };

                               }
                               else {
                                   tmp = { name: data[i], index: data[i], width: 65, hidden: false, editable: true, sortable: true, editrules: { required: true} };
                               }

                           }
                       }


                       //Push to Array
                       colName.push(data[i]);

                       colMod.push(tmp);

                   }
                   var dummy = "";
                   var dummy1 = '';
                   var StudentCols = 'hidden,FullTimeOrPartTimeIndicator,CumulativeGPA,CreditsAttempted,CreditsEarned,LatestCompassPrealgebraTestScore,LatestCompassAlgebraTestScore,LatestCompassWritingTestScore,LatestCompassReadingTestScore,LatestACTEnglishAssessmentScore,LatestACTMathAssessmentScore,LatestACTReadingAssessmentScore,LatestACTScienceAssessmentScore,LatestTestingDate,HighSchoolName,HighSchoolGraduationDate,HomeTelephoneNumber,MailingAddressLineOne,MailingAddressLineTwo,MailingAddressLineThree,City,StateName,ZipCode,EmailAddress';
                   var StudentCol = StudentCols.split(",");
                   for (var i = 0; i < StudentCol.length; i++) {
                       dummy1 = StudentCol[i];

                       if (dummy1 == 'hidden') {
                           dummy = { name: StudentCol[i], index: StudentCol[i], width: 65, hidden: true, sortable: true, frozen: true, editable: true, editrules: { required: true} };
                       }
                       else {
                           dummy = { name: StudentCol[i], index: StudentCol[i], width: 65, hidden: false, sortable: true, editable: true, editrules: { required: true} };
                       }
                       colMod.push(dummy);
                       colName.push(dummy1);
                   }
                   var reportname = $("#HiddenReportName").val();
                   var query = $("#Hiddenquery").val();
                   
                   //alert(query);
                   $("#ResultsGrid").jqGrid({
                       url: 'Result.ashx?aid=' + aid + '&isreport=yes' + query,
                       datatype: 'json',
                       height: 450,
                       autowidth: false ,
                       shrinkToFit: true,
                       colModel: colMod,
                       colNames: colName,
                       rowNum: 20,
                       rowList: [20, 50, 100],
                       pager: '#ResultsPager',
                       sortname: 'StudentOID',
                       viewrecords: true,
                       sortorder: 'asc',
                       altRows: true,
                       editurl: 'Result.ashx',
                       mtype: "POST",
                       caption: 'Result',
                       loadComplete: function(request) {
                           checkit();
                       },
                       postData: {
                           reportname: function() { return jQuery("#reportname").val(); },
                           hiddenfields: function() { return jQuery("#hiddenfields").val(); },
                           query: function() { return jQuery("#Hiddenquery").val(); },
                           notReport: function() { return jQuery("#notReport").val(); },
                       },
                       col: {
                           caption: "Show/Hide Columns",
                           bSubmit: "Submit",
                           bCancel: "Cancel"
                       }
                   });

                   $("#ResultsGrid").jqGrid('navGrid', '#ResultsPager', { edit: false, add: false, del: false });

                   options = { autosearch: true };
                   $("#ResultsGrid").filterToolbar(options);

                   var sgrid = $("#ResultsGrid")[0];
                   sgrid.triggerToolbar();

                   //

                   $("#myButton").click(function() {
                       $("#ResultsGrid").setColumns(options);
                       return false;
                   });


               }, //success Complete
               Error: function() {
                   alert('Erreur de transmission au serveur');
               }


           });


       });                        

    </script>
    
    
  
  
  
   
   <table >
   <tr>
   <td>
    <%--<input id="myButton" type="button" value="Show/Hide column" />--%>
   </td>
   <td>
   <div id="ReportHeaderDiv" runat ="server" style ="display :none ">
   
   </div>
   
   <div id="Columns" runat ="server" style ="display :none ">
   
   </div>
   </td>
   <td>
   <input id="reportname" name = "reportname" type="hidden" value="" />    
   <input id="hiddenfields" name = "hiddenfields" type="hidden" value="" /> 
   <input id="HiddenReportName" runat="server" name="HiddenReportName" type="hidden" value="0" />
    <input id="Hiddenquery" runat="server" name="Hiddenquery" type="hidden" value="0" />
    <input id="notReport" name = "hiddenfields" type="hidden" value="Report" />
    <input id="HiddenColumns" runat="server" name="HiddenColumns" type="hidden" value="0" />    
   </td>
   <td>
    <input id="btnRefresh" type="button" value="Refresh" visible ="false" style ="display:none "/>
       
   </td>
   <td>
   <input id="myButton" type="button" value="Show/Hide column" visible ="false" style ="display:none "/>     
   </td>
   
   <td>
   <input id="saveButton" type="button" value="Save as Report" visible ="false" style ="display:none "/> 
   </td>
   </tr>
   
   </table>
   <input type ="button" id ="ButtonCSV" value ="Export as CSV" onclick ="csvExport()" style ="display :none " />
 <%-- <asp:Button ID="ButtonCSV" runat="server" Text="Export as CSV" OnClientClick ="csvExport()"/>  --%>
<asp:Button ID="ButtonPDF" runat="server" Text="Export as CSV" onclick="ButtonPDF_Click" Visible ="true" />  
<asp:Button ID="ButtonCSV" runat="server" Text="Export as PDF" Visible ="true" 
        onclick="ButtonCSV_Click1" /> 
 <asp:Button ID="ButtonDelete" runat="server" Text="Delete Report" 
           onclick="ButtonDelete_Click" OnClientClick="return confirm('Do you really want to delete this Report?','Report Delete');"/>
    </form>
    <script type="text/javascript">
        function csvExport() {
            // var json3 = { "d": "[{\"Id\":1,\"UserName\":\"Sam Smith\"},{\"Id\":2,\"UserName\":\"Fred Frankly\"},{\"Id\":1,\"UserName\":\"Zachary Zupers\"}]" }
            var str = '';
            var json2 = $("#Columns").text();
            var json3 = $("#ReportHeaderDiv").text();
            DownloadJSON2CSV(JSON.parse(json2));
            DownloadJSON2CSV(JSON.parse(json3));

            function DownloadJSON2CSV(objArray) {

                var array = typeof objArray != 'object' ? JSON.parse(objArray) : objArray;

                for (var i = 0; i < array.rows.length; i++) {
                    var line = '';

                    for (var index in array.rows[i]) {
                        for (var j = 0; j < array.rows[i].cell.length; j++)
                            line += array.rows[i].cell[j] + ',';
                    }

                    line.slice(0, line.Length - 1);
                   
                    str += line + '\r\n';
                }

                //alert(navigator.appName);

            }
            //alert(str);
           //if (navigator.appName != 'Microsoft Internet Explorer'){
            window.open("data:text/csv;charset=utf-8," + escape(str))
       // } else {

           // var popup = window.open('', 'csv', '');

          //  popup.document.body.innerHTML = '<pre>' + str + '</pre>';

        //}
        }

    </script>
</body>


</html>
