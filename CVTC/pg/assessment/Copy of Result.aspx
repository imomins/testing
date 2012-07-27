    <%@ Page Language="C#" AutoEventWireup="true" CodeFile="Copy of Result.aspx.cs" Inherits="pg_assessment_Result" %>

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
    <asp:Label ID="lblErorr" runat="server" Font-Names="Calibri" Font-Size="10pt" 
        ForeColor="Red"></asp:Label>
   <table id="ResultsGrid" cellpadding="0" cellspacing="0">
    </table>     
    <div id="ResultsPager">
    </div>  
    
   <script type="text/javascript">
       $(function() {

           var aid = $("#HiddenAOID").val();
           var t = "{ 'aoid': '" + aid + "'} ";
           $.ajax({

               type: "POST",
               url: "Result.aspx/GetColumnNameList",
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
                           tmp = { name: '"+data[i]+"', index: '"+data[i]+"', width: 80, hidden: true, editable: true, editrules: { required: true} };
                       }
                       else if (data[i] == 'StudentOID') {
                           tmp = { name: '"+data[i]+"', index: '"+data[i]+"', width: 80, hidden: true, editable: true, editrules: { required: true} };
                       }
                       else {
                           if (i > 6) {


                               //tmp = { name: '"+data[i]+"', index: '"+data[i]+"', width: 40, hidden: false, editable: true, editrules: { required: true} };
                               tmp = { name: data[i], index: data[i], width: 40, hidden: false, editable: true, editrules: { required: true} };

                           }
                           else {


                               if (data[i] == 'NumberOfPrinted' || data[i] == 'Flag') {
                                   tmp = { name: data[i], index: data[i], width: 65, hidden: false, frozen: true, editable: false, search: false, sortable: false, editrules: { required: true} };

                               }
                               else {
                                   tmp = { name: data[i], index: data[i], width: 65, hidden: false, editable: true, editrules: { required: true} };
                               }

                           }
                       }


                       //Push to Array
                       colName.push(data[i]);

                       colMod.push(tmp);

                   }
                   var dummy = "";
                   var dummy1 = '';
                   
	
                   //var StudentCols = 'hidden,FullOrPart,GPA,CreditAttempted,EarnedCredit,Prealgebra,Algebra,Writting,Reading,English,Math,ReadingScore,ScienceScore,TestingDate,HighSchool,HS_GRAD_DATE,Phone,ADDR1,ADDR2,ADDR3,CITY,STATE,ZIP,Email';
                   var StudentCols = 'hidden,FullTimeOrPartTimeIndicator,CumulativeGPA,CreditsAttempted,CreditsEarned,LatestCompassPrealgebraTestScore,LatestCompassAlgebraTestScore,LatestCompassWritingTestScore,LatestCompassReadingTestScore,LatestACTEnglishAssessmentScore,LatestACTMathAssessmentScore,LatestACTReadingAssessmentScore,LatestACTScienceAssessmentScore,LatestTestingDate,HighSchoolName,HighSchoolGraduationDate,HomeTelephoneNumber,MailingAddressLineOne,MailingAddressLineTwo,MailingAddressLineThree,City,StateName,ZipCode,EmailAddress';
                   var StudentCol = StudentCols.split(",");
                   for (var i = 0; i < StudentCol.length; i++) {
                       dummy1 = StudentCol[i];
                       
                       if (dummy1 == 'hidden') {
                           dummy = { name: StudentCol[i], index: StudentCol[i], width: 65, hidden: true, frozen: true, editable: true, editrules: { required: true} };
                       }
                       else {
                           dummy = { name: StudentCol[i], index: StudentCol[i], width: 65, hidden: false, editable: true, editrules: { required: true} };
                       }
                       colMod.push(dummy);
                       colName.push(dummy1);
                   }





                   $("#ResultsGrid").jqGrid({
                       url: 'Result.ashx?aid=' + aid,
                       datatype: 'json',
                       height: 450,
                       autowidth: true,
                       shrinkToFit:true ,
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

                   //$(function() {
                   jQuery("#saveButton").click(function() {
                       //alert('sumon');
                       var colModel = jQuery("#ResultsGrid").jqGrid('getGridParam', 'colModel');

                       var hiddenfields = "";
                       for (x in colModel) {
                           if (x == 0)
                           { hiddenfields += "" }
                           else
                           { hiddenfields += "&" }
                           hiddenfields += colModel[x].name + "=" + colModel[x].hidden;

                       }
                       alert(hiddenfields);
                       $("#hiddenfields").val(hiddenfields);


                       var name = prompt("Please enter report name", "");
                       if (name == null) return;
                       $("#reportname").val(name);
                       $("#ResultsGrid").trigger("reloadGrid");
                       $("#reportname").val("");
                   });



                   //});
                   //
               }, //success Complete
               Error: function() {
                   alert('Erreur de transmission au serveur');
               }


           }); //ajax complete



           //another probable place


       });                          //function complete

    </script>
    
    
  
    
    <%--May be unused code--%>
    <script type="text/javascript">
    $(document).ready(function()
    {
        $('#btn').click(function()
        {
        alert('Hello');
        var aid=16;
        var t = "{ 'aoid': '"+aid+"'} ";
        $.ajax({
                 type: "POST",
                 url: "Result.aspx/GetColumnNameList",                 
                 data: t,
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 async: true,
                 cache: false,
                 success: function(msg) {
                     alert(msg.d);                     

                 },
                 Error: function () {
                        alert('Erreur de transmission au serveur');
                    }

             })
        });
    });
   </script>
   
   <table >
   <tr>
   <td>
    <%--<input id="myButton" type="button" value="Show/Hide column" />--%>
   </td>
   <td>
   
   </td>
   <td>
   <input id="reportname" name = "reportname" type="hidden" value="" />    
   <input id="hiddenfields" name = "hiddenfields" type="hidden" value="" /> 
   </td>
   <td>
    <input id="btnRefresh" type="button" value="Refresh" />
       <asp:Button ID="ButtonViewResult" runat="server" Text="Force Load" 
           onclick="ButtonViewResult_Click" />
   </td>
   <td>
   <input id="myButton" type="button" value="Show/Hide column" />     
   </td>
   
   <td>
   <input id="saveButton" type="button" value="Save as Report" /> 
   </td>
   </tr>
   
   </table>
  
    </form>
 
</body>


</html>
