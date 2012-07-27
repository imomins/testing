<%@ Page Language="C#" AutoEventWireup="true" CodeFile="coursereport.aspx.cs" Inherits="pg_student_coursereport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Course Management</title>
    <link rel="stylesheet" type="text/css" media="screen" href="../../themes/redmond/jquery-ui-1.8.1.custom.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../themes/ui.jqgrid.css" />


    <script src="../../js/jquery.js" type="text/javascript"></script> 
    <script src="../../js/grid.locale-en.js" type="text/javascript"></script> 
    <script src="../../js/jquery.jqGrid.min.js" type="text/javascript"></script>
    
  <script type="text/javascript">
      function checkit(){
         var hiddenColumns = $("#HiddenColumns").val();
         hiddenColumns = hiddenColumns.split("&");
         for(x in hiddenColumns)
         {
            var namevalue = hiddenColumns[x].split("=");
               if(namevalue[1] == "true")
               {
                $("#CoursesGrid").hideCol(namevalue[0]);
               }
         }
         $("#CoursesGrid").trigger("reloadGrid");
      }
      

    $(function() 
    {
    jQuery("#printButtonPDF").click( function()
        {
        $("#HiddenReportType").val("pdf");
        $("#CoursesGrid").trigger("reloadGrid");
        }); 
    });         
       
      $(function() {
      var reportname=$("#HiddenReportName").val();
      var query=$("#Hiddenquery").val();
      $("#CoursesGrid").jqGrid({
              url:'CourseHandler.ashx?isreport=yes' + query, 
              datatype: 'json',
              height: 550,
             // width: 1050,
              autowidth: false,
              shrinktofit:true ,
              colNames: ['CourseOID', 'NAME', 'ID', 'TERMEFF', 'CRSENO', 'CRSETITLE', 'FINALGRDE', 'CRSETERM', 'DeliveryMethod',
              'ImportDate', 'Full/Part', 'GPA', 'Credit Attempted', 'Earned Credit',
                'Prealgebra', 'Algebra', 'Writting', 'Reading', 'English', 'Math', 'Reading Score', 'Science Score',
                'Testing Date', 'High School', 'HS_GRAD_DATE', 'Phone', 'ADDR1', 'ADDR2', 'ADDR3', 'CITY', 'STATE', 'ZIP',
                'Email', 'ImportDate', 'PPGMIND', 'MAJOR', 'ALERT', 'MC', 'NTO', 'PELL', 'RVP'],
              colModel: [
                        { name: 'CourseOID', index: 'CourseID', width: 100, hidden: true, sortable: false },
                        { name: 'NAME', width: 100, editable: true },
                        { name: 'BID', width: 100, editable: true },
                        { name: 'TERMEFF', width: 100, sortable: true, editable: true },
                        { name: 'CRSENO', width: 100, sortable: true, editable: true },
                        { name: 'CRSETITLE', width: 100, sortable: true, editable: true },
                        { name: 'FINALGRDE', width: 100, sortable: true, editable: true },
                        { name: 'CRSETERM', width: 100, sortable: true, editable: true },
                        { name: 'DeliveryMethod', width: 100, sortable: true, editable: true },
                        { name: 'ImportDate', width: 100, sortable: true, editable: true },
                         { name: 'FullPart', width: 100, sortable: true, editable: true },
                        { name: 'GPA', width: 60, sortable: true, editable: true },
                        { name: 'CreditAttempted', width: 60, sortable: true, editable: true },
                        { name: 'EarnedCredit', width: 60, sortable: true, editable: true },
                        { name: 'Prealgebra', width: 60, sortable: true, editable: true },
                        { name: 'Algebra', width: 60, sortable: true, editable: true },
                        { name: 'Writting', width: 60, sortable: true, editable: true },
                        { name: 'Reading', width: 60, sortable: true, editable: true },
                        { name: 'English', width: 60, sortable: true, editable: true },
                        { name: 'Math', width: 60, sortable: true, editable: true },
                        { name: 'ReadingScore', width: 60, sortable: true, editable: true },
                        { name: 'ScienceScore', width: 60, sortable: true, editable: true },
                        { name: 'TestingDate', width: 60, sortable: true, editable: true },
                        { name: 'HighSchool', width: 60, sortable: true, editable: true },
                        { name: 'HS_GRAD_DATE', width: 100, sortable: true, editable: true },
                        { name: 'Phone', width: 100, sortable: true, editable: true },
                        { name: 'ADDR1', width: 100, sortable: true, editable: true },
                        { name: 'ADDR2', width: 100, sortable: true, editable: true },
                        { name: 'ADDR3', width: 100, sortable: true, editable: true },
                        { name: 'CITY', width: 100, sortable: true, editable: true },
                        { name: 'STATE', width: 100, sortable: true, editable: true },
                        { name: 'ZIP', width: 100, sortable: true, editable: true },
                        { name: 'Email', width: 100, sortable: true, editable: true },
                        { name: 'ImportDate', width: 100, sortable: true, editable: true },
                        { name: 'PPGMIND', width: 100, sortable: true, editable: true },
                        { name: 'MAJOR', width: 100, sortable: true, editable: true },
                        { name: 'ALERT', width: 50, sortable: true, editable: true },
                        { name: 'MC', width: 50, sortable: true, editable: true },
                        { name: 'NTO', width: 50, sortable: true, editable: true },
                        { name: 'PELL', width: 50, sortable: true, editable: true },
                        { name: 'RVP', width: 50, sortable: true, editable: true }      
                    ],
              rowNum: 20,
              rowList: [20, 50, 100],
              pager: '#CoursesGridPager',
              sortname: 'CourseOID',
              viewrecords: true,
              sortorder: 'asc',
              altRows: true,
              
              loadComplete : function (request) {
                checkit();
               },
              editurl: 'CourseHandler.ashx',
              caption: reportname
          });

//          var sgrid = $("#CoursesGrid")[0];
//          sgrid.triggerToolbar();
      });
 
    </script>
</head>
<body>
<input id="HiddenReportType" runat="server" name="HiddenReportType" type="hidden" value="0" />
<input id="HiddenReportName" runat="server" name="HiddenReportName" type="hidden" value="0" />
<input id="Hiddenquery" runat="server" name="Hiddenquery" type="hidden" value="0" />
<input id="HiddenColumns" runat="server" name="HiddenColumns" type="hidden" value="0" />   
   <form id="form1" runat="server">
   <table id="CoursesGrid" cellpadding="0" cellspacing="0"></table>  
   <div id="CoursesGridPager"></div>    
   <%--<input id="reportname" name = "reportname" type="hidden" value="" />  --%>  
   <asp:Button ID="ButtonCSV" runat="server" Text="Export as CSV" onclick="ButtonCSV_Click" />  
   <asp:Button ID="ButtonExcel" runat="server" Visible="false" Text="Export as Excel" onclick="ButtonExcel_Click" />  
   <asp:Button ID="ButtonPDF" runat="server" Text="Export as PDF" onclick="ButtonPDF_Click" />  
    &nbsp;&nbsp;  
    <asp:Button ID="ButtonDelete" runat="server" Text="Delete Report" 
           onclick="ButtonDelete_Click" OnClientClick="return confirm('Do you really want to delete this Report?','Report Delete');"/>
    </form>
</body>
</html>
