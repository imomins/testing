<%@ Page Language="C#" AutoEventWireup="true" CodeFile="course.aspx.cs" Inherits="pg_student_course" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Course Management</title>
    <link rel="stylesheet" type="text/css" media="screen" href="../../themes/redmond/jquery-ui-1.8.1.custom.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../themes/ui.jqgrid.css" />


    <script src="../../js/jquery.js" type="text/javascript"></script> 
    <script src="../../js/grid.locale-en.js" type="text/javascript"></script> 
    <script src="../../js/jquery.jqGrid.min.js" type="text/javascript"></script>
    
  <%--<script type="text/javascript">
       
      $(function() {
      $("#CoursesGrid").jqGrid({
              url:'CourseHandler.ashx',//''UserHandler.ashx',
              datatype: 'json',
              height: 450,
              width:1050,
              colNames: ['CourseOID', 'NAME', 'ID', 'TERMEFF', 'CRSENO', 'CRSETITLE', 'FINALGRDE', 'CRSETERM', 'DeliveryMethod', 'ImportDate'],
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
                        { name: 'ImportDate', width: 100, sortable: true, editable: true } 
                        
                             
                    ],
              rowNum: 20,
              rowList: [20, 50, 100],
              pager: '#CoursesGridPager',
              sortname: 'CourseOID',
              viewrecords: true,
              sortorder: 'asc',
              altRows: true,
              postData: {
                reportname: function() { return jQuery("#reportname").val(); },
                hiddenfields: function() { return jQuery("#hiddenfields").val(); }
              },
              editurl: 'CourseHandler.ashx',
              caption: 'Courses Management '
          });

          $("#CoursesGrid").jqGrid('navGrid', '#CoursesGridPager', { edit: true, add: false, del: false });

          options = { autosearch: true };
          $("#CoursesGrid").filterToolbar(options);
          

          var sgrid = $("#CoursesGrid")[0];
          sgrid.triggerToolbar();


             $("#myButton").click(function () {
                 $("#CoursesGrid").setColumns(options);
                 return false;
             });
             
        $(function() 
            {
            jQuery("#saveButton").click( function()
                {
                var colModel = jQuery("#CoursesGrid").jqGrid ('getGridParam', 'colModel');
                var hiddenfields = ""; 
                for(x in colModel)
                {
                 if(x==0)
                 {hiddenfields += ""}
                 else
                 {hiddenfields += "&"}
                 hiddenfields += colModel[x].name + "=" +colModel[x].hidden;
                }
                $("#hiddenfields").val(hiddenfields);
            
            
                 var name = prompt("Please enter report name","");
                 if(name == null) return;
                 $("#reportname").val(name);
                 $("#CoursesGrid").trigger("reloadGrid");
                 $("#reportname").val("");
                }); 
            });              

      });

    </script>--%>
   
   <script type="text/javascript">

       $(function() {
           $("#CoursesGrid").jqGrid({
               url: 'CourseHandler.ashx', //''UserHandler.ashx',
               datatype: 'json',
               height: 450,
               autowidth:false ,
               colNames: ['CourseOID', 'NAME', 'ID', 'TERMEFF', 'CRSENO', 'CRSETITLE', 'FINALGRDE', 'CRSETERM',
               'DeliveryMethod', 'ImportDate'/*, 'TERM'*/, 'Full/Part', 'GPA', 'Credit Attempted', 'Earned Credit',
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

               //                        { name: 'TERM', width: 70, sortable: true, editable: true },
                        {name: 'FullPart', width: 100, sortable: true, editable: true },
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
               postData: {
                   reportname: function() { return jQuery("#reportname").val(); },
                   hiddenfields: function() { return jQuery("#hiddenfields").val(); }
               },
               editurl: 'CourseHandler.ashx',
               caption: 'Courses Management '
           });

           $("#CoursesGrid").jqGrid('navGrid', '#CoursesGridPager', { edit: true, add: false, del: false });

           options = { autosearch: true };
           $("#CoursesGrid").filterToolbar(options);


           var sgrid = $("#CoursesGrid")[0];
           sgrid.triggerToolbar();


           $("#myButton").click(function() {
               $("#CoursesGrid").setColumns(options);
               return false;
           });

           $(function() {
               jQuery("#saveButton").click(function() {
                   var colModel = jQuery("#CoursesGrid").jqGrid('getGridParam', 'colModel');

                   var hiddenfields = "";
                   for (x in colModel) {
                       if (x == 0)
                       { hiddenfields += "" }
                       else
                       { hiddenfields += "&" }
                       hiddenfields += colModel[x].name + "=" + colModel[x].hidden;
                       //alert(hiddenfields);
                   }
                   $("#hiddenfields").val(hiddenfields);


                   var name = prompt("Please enter report name", "");
                   if (name == null) return;
                   $("#reportname").val(name);
                   $("#CoursesGrid").trigger("reloadGrid");
                   $("#reportname").val("");
               });
           });

       });

    </script>
</head>
<body>
    <form id="form1" runat="server">
   <table id="CoursesGrid" cellpadding="0" cellspacing="0">
    </table>  
        <div id="CoursesGridPager">
        </div>    
   <input id="reportname" name = "reportname" type="hidden" value="" />    
   <input id="hiddenfields" name = "hiddenfields" type="hidden" value="" />            
<input id="myButton" type="button" value="Show/Hide column" />     
<input id="saveButton" type="button" value="Save as Report" />         
    </form>
</body>
</html>
