<%@ Page Language="C#" AutoEventWireup="true" CodeFile="studentreport.aspx.cs" Inherits="pg_student_studentreport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" media="screen" href="../../themes/redmond/jquery-ui-1.8.1.custom.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../themes/ui.jqgrid.css" />


    <script src="../../js/jquery.js" type="text/javascript"></script> 
    <script src="../../js/grid.locale-en.js" type="text/javascript"></script> 
    <script src="../../js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script type ="text/javascript">
    $('button_selector').click(function(){
        document.location.href='the_link_to_go_to.html';})
    </script>
    
    
  <script type="text/javascript">
      function checkit(){
         var hiddenColumns = $("#HiddenColumns").val();
         hiddenColumns = hiddenColumns.split("&");
         for(x in hiddenColumns)
         {
            var namevalue = hiddenColumns[x].split("=");
               if(namevalue[1] == "true")
               {
                $("#StudentsGrid").hideCol(namevalue[0]);
               }
         }
         $("#StudentsGrid").trigger("reloadGrid");
      }

      $(function() {
          var reportname = $("#HiddenReportName").val();
          var query = $("#Hiddenquery").val();
          //alert(query);
          $("#StudentsGrid").jqGrid({
              url: 'StudentHandler.ashx?isreport=yes' + query,
              datatype: 'json',
              height: 650,
             // width: 1050,
              autowidth: false,
              shrinktofit: true,
              colNames: ['StudentOID', 'NAME', 'ID', 'TERM', 'Full/Part', 'GPA', 'Credit Attempted', 'Earned Credit', 'Prealgebra', 'Algebra', 'Writting', 'Reading', 'English', 'Math', 'Reading Score', 'Science Score', 'Testing Date', 'High School', 'HS_GRAD_DATE', 'Phone', 'ADDR1', 'ADDR2', 'ADDR3', 'CITY', 'STATE', 'ZIP', 'Email', 'ImportDate', 'PPGMIND', 'MAJOR', 'BirthDate', 'ALERT', 'MC', 'NTO', 'PELL', 'RVP'], //, 'Email',
              colModel: [
                        { name: 'StudentOID', index: 'StudentID', width: 100, hidden: true, sortable: false },
                        { name: 'NAME', width: 100, editable: true },
                        { name: 'BID', width: 90, sortable: true, editable: true },
                        { name: 'TERM', width: 60, sortable: true, editable: true },
                        { name: 'FullPart', width: 60, sortable: true, editable: true },
                        { name: 'GPA', width: 60, sortable: true, editable: true },
                        { name: 'CreditAttempted', width: 60, sortable: true, editable: true },
                        { name: 'EarnedCredit', width: 80, sortable: true, editable: true },
                        { name: 'Prealgebra', width: 80, sortable: true, editable: true },
                        { name: 'Algebra', width: 80, sortable: true, editable: true },
                        { name: 'Writting', width: 80, sortable: true, editable: true },
                        { name: 'Reading', width: 80, sortable: true, editable: true },
                        { name: 'English', width: 80, sortable: true, editable: true },
                        { name: 'Math', width: 80, sortable: true, editable: true },
                        { name: 'ReadingScore', width: 80, sortable: true, editable: true },
                        { name: 'ScienceScore', width: 80, sortable: true, editable: true },
                        { name: 'TestingDate', width: 80, sortable: true, editable: true },
                        { name: 'HighSchool', width: 80, sortable: true, editable: true },
                        { name: 'HS_GRAD_DATE', width: 80, sortable: true, editable: true },
                        { name: 'Phone', width: 80, sortable: true, editable: true },
                        { name: 'ADDR1', width: 80, sortable: true, editable: true },
                        { name: 'ADDR2', width: 80, sortable: true, editable: true },
                        { name: 'ADDR3', width: 80, sortable: true, editable: true },
                        { name: 'CITY', width: 80, sortable: true, editable: true },
                        { name: 'STATE', width: 80, sortable: true, editable: true },
                        { name: 'ZIP', width: 80, sortable: true, editable: true },
                        { name: 'Email', width: 80, sortable: true, editable: true },
                        { name: 'ImportDate', width: 80, sortable: true, editable: true },
                        { name: 'PPGMIND', width: 80, sortable: true, editable: true },
                        { name: 'MAJOR', width: 150, sortable: true, editable: true },
                        { name: 'BirthDate', width: 150, sortable: true, editable: true },
                        { name: 'ALERT', width: 50, sortable: true, editable: true },
                        { name: 'MC', width: 50, sortable: true, editable: true },
                        { name: 'NTO', width: 50, sortable: true, editable: true },
                        { name: 'PELL', width: 50, sortable: true, editable: true },
                        { name: 'RVP', width: 50, sortable: true, editable: true }
                    ],
              rowNum: 25,
              rowList: [25, 50, 100],
              pager: '#StudentsGridPager',
              sortname: 'StudentOID',
              viewrecords: true,
              sortorder: 'asc',
              altRows: true,
              sortable: true,

              loadComplete: function(request) {
                  checkit();
              },

              editurl: 'StudentHandler.ashx',
              caption: reportname
          });
          //          var sgrid = $("#StudentsGrid")[0];
          //          sgrid.triggerToolbar();
      }); 
    </script>
</head>
<body>
<input id="HiddenReportName" runat="server" name="HiddenReportName" type="hidden" value="0" />
<input id="Hiddenquery" runat="server" name="Hiddenquery" type="hidden" value="0" />
<input id="HiddenColumns" runat="server" name="HiddenColumns" type="hidden" value="0" />    
  
    <form id="form1" runat="server">
       <table id="StudentsGrid" cellpadding="0" cellspacing="0">
      </table>

        <div id="StudentsGridPager"> </div>       
 
   <%--<input id="reportname" name = "reportname" type="hidden" value="" />  --%>   
<asp:Button ID="ButtonCSV" runat="server" Text="Export as CSV" onclick="ButtonCSV_Click" />     
 <asp:Button ID="ButtonExcel" runat="server" Visible="false" Text="Export as Excel" onclick="ButtonExcel_Click" />
 <asp:Button ID="ButtonPDF" runat="server" Text="Export as PDF" onclick="ButtonPDF_Click" />
    &nbsp;&nbsp;
       <asp:Button ID="ButtonDelete" runat="server" Text="Delete Report" 
           onclick="ButtonDelete_Click" OnClientClick="return confirm('Do you really want to delete this Report?','Report Delete');"/>
    <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="False"></asp:Label>
    </form>
    
</body>
</html>
