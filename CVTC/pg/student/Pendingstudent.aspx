<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pendingstudent.aspx.cs" Inherits="pg_student_Pendingstudent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pending Student Management</title>
    <link rel="stylesheet" type="text/css" media="screen" href="../../themes/redmond/jquery-ui-1.8.1.custom.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../themes/ui.jqgrid.css" />

    <link href="../../css/global-style.css" rel="stylesheet" type="text/css" />
    <script src="../../js/jquery.js" type="text/javascript"></script> 
    <script src="../../js/grid.locale-en.js" type="text/javascript"></script> 
    <script src="../../js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="../../js/thickbox.js" type="text/javascript"></script>
    <link href="../../themes/thickbox.css" rel="stylesheet" type="text/css" />
    <script type ="text/javascript">
    $('button_selector').click(function(){
   document.location.href='the_link_to_go_to.html';
})

    </script>
  <script type="text/javascript">

      $(function() {

          $("#StudentsGrid").jqGrid({
              url: 'PendingStudentHandler.ashx', //''UserHandler.ashx',
              datatype: 'json',
              height: 550,
              autowidth: false ,
              //shrinktofit:true ,
              colNames: ['StudentOID', 'NAME', 'ID', 'TERM', 'Full/Part', 'GPA', 'Credit Attempted', 'Earned Credit', 'Prealgebra', 'Algebra', 'Writting', 'Reading', 'English', 'Math', 'Reading Score', 'Science Score', 'Testing Date', 'High School', 'HS_GRAD_DATE', 'Phone', 'ADDR1', 'ADDR2', 'ADDR3', 'CITY', 'STATE', 'ZIP', 'Email', 'ImportDate', 'PPGMIND', 'MAJOR','Birth Date', 'Status', 'Actions' ],
              //colNames: ['StudentOID', 'NAME', 'ID', 'TERM', 'Full/Part', 'GPA', 'Credit Attempted', 'Earned Credit', 'Prealgebra', 'Algebra', 'Writting', 'Reading', 'English', 'Math', 'Reading Score', 'Science Score', 'Testing Date', 'High School', 'HS_GRAD_DATE', 'Phone', 'ADDR1', 'ADDR2', 'ADDR3', 'CITY', 'STATE', 'ZIP', 'Email', 'ImportDate', 'PPGMIND', 'Program Interest', 'Status',], //, 'Email',
              colModel: [
                        { name: 'StudentOID', index: 'StudentID', width: 50, hidden: true, sortable: false },
                        { name: 'NAME', width: 150, editable: true, editrules: { required: true} },
                        {name: 'BID', index: 'StudentOID', width: 140, sortable: true, editrules: { required: true }, edittype: 'text', editable: true, editoptions: { size: "20", maxlength: "9", minlength: "9" } },
                        { name: 'TERM', width: 80, sortable: true, editable: true },
                        { name: 'FullPart', width: 60, sortable: true,hidden: true, editable: true, edittype: "select", editoptions: { value: 'Full:Full;Part:Part'} },
                        { name: 'GPA', width: 60, sortable: true, editable: true,hidden: true, editrules: { required: true, number: true} },
                        { name: 'CreditAttempted', width: 60, sortable: true,hidden: true, editable: true, editrules: { required: true, number: true} },
                        { name: 'EarnedCredit', width: 60, sortable: true,hidden: true, editable: true, editrules: { required: true, number: true} },
                        { name: 'Prealgebra', width: 60, sortable: true, editable: true,hidden: true, editrules: { required: true, number: true} },
                        { name: 'Algebra', width: 60, sortable: true, editable: true,hidden: true, editrules: { required: true, number: true} },
                        { name: 'Writting', width: 60, sortable: true, editable: true,hidden: true, editrules: { required: true, number: true} },
                        { name: 'Reading', width: 60, sortable: true, editable: true,hidden: true, editrules: { required: true, number: true} },
                        { name: 'English', width: 60, sortable: true, editable: true,hidden: true, editrules: { required: true, number: true} },
                        { name: 'Math', width: 60, sortable: true, editable: true, hidden: true, editrules: { required: true, number: true} },
                        { name: 'ReadingScore', width: 60, sortable: true, editable: true,hidden: true, editrules: { required: true, number: true} },
                        { name: 'ScienceScore', width: 60, sortable: true, editable: true,hidden: true, editrules: { required: true, number: true} },
                        { name: 'TestingDate', width: 60, sortable: true, editable: false, hidden: true },
                        { name: 'HighSchool', width: 60, sortable: true,hidden: true, editable: true },
                        {name: 'HS_GRAD_DATE', width: 100, sortable: true, hidden: true, editable: true, formoptions: { elmsuffix: " mm/dd/yyyy" }, hidden: true }, 
                        { name: 'Phone', width: 100, sortable: true,hidden: true, editable: true },
                        { name: 'ADDR1', width: 100, sortable: true,hidden: true, editable: true },
                        { name: 'ADDR2', width: 100, sortable: true, hidden: true,editable: true },
                        { name: 'ADDR3', width: 100, sortable: true, editable: true, hidden: true },
                        { name: 'CITY', width: 120, sortable: true,hidden: true, editable: true },
                        { name: 'STATE', width: 100, sortable: true,hidden: true, editable: true },
                        { name: 'ZIP', width: 100, sortable: true,hidden: true, editable: true },
                        {name: 'Email', width: 130, sortable: true, editable: true },
                        { name: 'ImportDate', width: 100, sortable: true, editable: false, hidden: true },
                        { name: 'PPGMIND', width: 200, sortable: true, editable: false, hidden: true },
                        { name: 'MAJOR', width: 250, sortable: true, editable: true, hidden: false },
                        { name: 'BirthDate', width: 100, sortable: true, editable: true, hidden: false },
                        { name: 'Status', width: 100, sortable: true, editable: true, edittype: "select", editoptions: { value: 'Pending:Pending;Approved:Approved'} },
                        { name: 'Actions', index: 'Actions', width: 100,classes: 'banner', formatter: 'showlink', formatoptions: { baseLinkUrl: 'Update.aspx', StudentID: 'StudentID'} }

                    ],
              rowNum: 25,
              rowList: [25, 50, 100],
              pager: '#StudentsGridPager',
              sortname: 'StudentOID',
              viewrecords: true,
              sortorder: 'desc',
              altRows: true,
              sortable: true,
             // closeAfterEdit: false,
              
              postData: {
                  reportname: function() { return jQuery("#reportname").val(); },
                  hiddenfields: function() { return jQuery("#hiddenfields").val(); }
              },
              //afterSubmit: function(response, postdata) {
              //alert("after complete row");
             // },
              //alert(myCellData);
              //ondblClickRow: function(id) { alert("You double click row with id: " + myCellData); },
              //multiselect: true,
              editurl: 'PendingStudentHandler.ashx',
              caption: 'Pending Student Management'
          });

          // jQuery("#saveButton").click(function(id) { alert("You click row with id: " + id.toString()); });

          $("#StudentsGrid").jqGrid('navGrid', '#StudentsGridPager', { edit: false, add: false, del: false }, { width: 580, height: 800, closeAfterEdit: true });


          jQuery("#grid_id").jqGrid('editRow', $("#StudentsGrid").jqGrid('getGridParam', 'selrow'),
            {
            keys: true,
            oneditfunc: function() {
            alert("edited");
            }
            });
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
      
    //});
    </script>
</head>
<body>
    <form id="form1" runat="server">
    
    
    <b>Student Information(Pending)</b>
    <br/>
    <asp:Label ID="lblError" runat="server" Font-Size="Larger" ForeColor="Red" 
        Visible="False"></asp:Label>
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
   <input id="myButton" type="button" value="Show/Hide column" style ="display :none "/>
   <input id="saveButton" type="button" value="Save as Report" style ="display :none "/>
   <%--<input type="button" value="Test" onclick="javascript: window.parent.testit('#tt3', 'StudentTest');" />--%>
   
  
    </form>
</body>
</html>
