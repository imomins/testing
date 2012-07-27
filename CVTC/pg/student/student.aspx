<%@ Page Language="C#" AutoEventWireup="true" CodeFile="student.aspx.cs" Inherits="pg_student_student" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student Management</title>
    <link rel="stylesheet" type="text/css" media="screen" href="../../themes/redmond/jquery-ui-1.8.1.custom.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../themes/ui.jqgrid.css" />


    <script src="../../js/jquery.js" type="text/javascript"></script> 
    <script src="../../js/grid.locale-en.js" type="text/javascript"></script> 
    <script src="../../js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script type ="text/javascript">
    $('button_selector').click(function(){
   document.location.href='the_link_to_go_to.html';
})

    </script>
  
   
   
   <script type="text/javascript">



       $(function() {
           $("#StudentsGrid").jqGrid({
               url: 'StudentHandler.ashx', //''UserHandler.ashx',
               datatype: 'json',
               height: 550,
               //width: 1050,
               autowidth:false  ,
               colNames: ['StudentOID', 'NAME', 'ID', 'TERM', 'Full/Part', 'GPA', 'Credit Attempted', 'Earned Credit', 'Prealgebra', 'Algebra', 'Writting', 'Reading', 'English', 'Math', 'Reading Score', 'Science Score', 'Testing Date', 'High School', 'HS_GRAD_DATE', 'Phone', 'ADDR1', 'ADDR2', 'ADDR3', 'CITY', 'STATE', 'ZIP', 'Email', 'ImportDate', 'PPGMIND', 'MAJOR','Birth Date','ALERT','MC','NTO','PELL','RVP'], //, 'Email',
               colModel: [
                        { name: 'StudentOID', index: 'StudentID', width: 50, hidden: true, sortable: false },
                        { name: 'NAME', width: 100, editable: true },
                        {name: 'StudentOID', index: 'StudentOID', width: 80, sortable: true, editable: true, classes: 'banner', formatter: 'showlink', formatoptions: { baseLinkUrl: 'studentProfile.aspx', StudentID: 'StudentID'} },
                        { name: 'TERM', width: 70, sortable: true, editable: true },
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
                        { name: 'BirthDate', width: 100, sortable: true, editable: true },
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
               postData: {
                   reportname: function() { return jQuery("#reportname").val(); },
                   hiddenfields: function() { return jQuery("#hiddenfields").val(); }
               },
              
               editurl: 'StudentHandler.ashx',
               caption: 'Student Management'
           });

           $("#StudentsGrid").jqGrid('navGrid', '#StudentsGridPager', { edit: false, add: false, del: false });

           options = { autosearch: true };
           $("#StudentsGrid").filterToolbar(options);


           var sgrid = $("#StudentsGrid")[0];
           sgrid.triggerToolbar();


           $("#myButton").click(function() {
               $("#StudentsGrid").setColumns(options);
               return false;
           });

           $("a").live('click', function(event) {
               event.preventDefault();
           });


           $(".banner").live('click', function() {
               elemnt = $(".banner").find("a");
               var title = $(this).attr('title');
               var sendid = "#" + title.substr(1);
               var viewtitle = $(this).parent().find("td:nth-child(2)").attr('title');
               viewtitle = viewtitle.replace(",", "")
               var link = $(this).find("a").attr('href');
               //alert(sendid);
               window.parent.testit(sendid, viewtitle, link);
           });

           $(function() {
               jQuery("#saveButton").click(function() {

                   var colModel = jQuery("#StudentsGrid").jqGrid('getGridParam', 'colModel');
                   var hiddenfields = "";
                   for (x in colModel) {
                       if (x == 0)
                       { hiddenfields += "" }
                       else
                       { hiddenfields += "&" }
                       hiddenfields += colModel[x].name + "=" + colModel[x].hidden;
                   }
                   $("#hiddenfields").val(hiddenfields);


                   var name = prompt("Please enter report name", "");
                   if (name == null) return;
                   $("#reportname").val(name);
                   $("#StudentsGrid").trigger("reloadGrid");
                   $("#reportname").val("");

               });
           });

       }); 
      
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    
    
    <b>Student Information</b>
    <br/>
       <table id="StudentsGrid" cellpadding="0" cellspacing="0">
      </table>

        <div id="StudentsGridPager"> </div>           
        <div >
    <table >
    <tr>
    <td>
        <%--May be this is unused grid--%>
        <%--<asp:GridView runat="server" AutoGenerateColumns="False"></asp:GridView>--%>
     </td>
    </tr>
    </table>
    </div>
   <input id="reportname" name = "reportname" type="hidden" value="" />    
   <input id="hiddenfields" name = "hiddenfields" type="hidden" value="" /> 
   <input id="myButton" type="button" value="Show/Hide column" />
   <input id="saveButton" type="button" value="Save as Report" />
   <%--<input type="button" value="Test" onclick="javascript: window.parent.testit('#tt3', 'StudentTest');" />--%>
    </form>
</body>
</html>
