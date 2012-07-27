<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewInterventionReport.aspx.cs" Inherits="pg_intervention_ViewInterventionReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Intervention</title>
    
    <link rel="stylesheet" type="text/css" media="screen" href="../../themes/redmond/jquery-ui-1.8.1.custom.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../themes/ui.jqgrid.css" />
    <link href="../../themes/thickbox.css" rel="stylesheet" type="text/css" />
    <script src="../../js/jquery.js" type="text/javascript"></script> 
    <script src="../../js/grid.locale-en.js" type="text/javascript"></script> 
    <script src="../../js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="../../js/thickbox.js" type="text/javascript"></script>
    
    <script type="text/javascript">
      function checkit(){
         var hiddenColumns = $("#HiddenColumns").val();
         hiddenColumns = hiddenColumns.split("&");
         for(x in hiddenColumns)
         {
            var namevalue = hiddenColumns[x].split("=");
               if(namevalue[1] == "true")
               {
                $("#InterventionGrid").hideCol(namevalue[0]);
               }
         }
         $("#InterventionGrid").trigger("reloadGrid");
      }
          
          
        $(function() {
        var reportname=$("#HiddenReportName").val();
        var query=$("#Hiddenquery").val();
            $("#InterventionGrid").jqGrid({
                url: 'Intervention.ashx?isreport=yes' + query,
                datatype: 'json',
                height: 650,
                width: 1050,
                colNames: ['PrescriptionOID', 'Datestamp','Banner Id','Participating', 'Action Date', 'Sponsor', 'Criteria Type', 'Outcome Type','Urgent', 'Internal', 'Prescribed', 'Completed', 'Email', 'Telephone','In Person', 'Hand off', 'Contact Date','Comment'],
                colModel: [
                        { name: 'PrescriptionOID', index: 'PrescriptionOID', width: 100, hidden:true,sortable: false },
                        { name: 'Datestamp', width: 100, editable: true ,editrules: {required:true} },
                        { name: 'Banner Id', width: 100, editable: true ,editrules: {required:true} },
                        { name: 'Participating', width: 100, editable: true ,editrules: {required:true} },
                        { name: 'Action Date', width: 100, editable: true ,editrules: {required:true} },
                        { name: 'Sponsor', width: 100, editable: true ,editrules: {required:true} },
                        { name: 'Criteria Type', width: 100, editable: true ,editrules: {required:true} },
                        { name: 'Outcome Type', width: 80, editable: true ,editrules: {required:true} },
                        { name: 'Urgent', width: 40, editable: true ,editrules: {required:true} },
                        { name: 'Internal', width: 40, editable: true ,editrules: {required:true} },
                        { name: 'Prescribed', width: 40, editable: true ,editrules: {required:true} },
                        { name: 'Completed', width: 40, editable: true ,editrules: {required:true} },
                        { name: 'Email', width: 40, editable: true ,editrules: {required:true} },
                        { name: 'Telephone', width: 40, editable: true ,editrules: {required:true} },
                        { name: 'In Person', width: 40, editable: true ,editrules: {required:true} },
                        { name: 'Hand off', width: 40, editable: true ,editrules: {required:true} },                        
                        { name: 'Contact Date', width: 100, editable: true ,editrules: {required:true} },
                        { name: 'Comment', width: 100, editable: true ,editrules: {required:true} }
                        
                    ],
                rowNum: 20,
                rowList: [20, 30, 50],
                pager: '#InterventionGridPager',
                sortname: 'PrescriptionOID',
                viewrecords: true,
                sortorder: 'asc',
                altRows: true, 
                  loadComplete : function (request) {
                    checkit();
                   },
                               
                caption: reportname,
                autowidth: true
 
            });

 



//            var sgrid = $("#InterventionGrid")[0];
//             sgrid.triggerToolbar();

          

        });
         </script>
 
</head>
<body>
<input id="HiddenReportName" runat="server" name="HiddenReportName" type="hidden" value="0" />
<input id="Hiddenquery" runat="server" name="Hiddenquery" type="hidden" value="0" />
<input id="HiddenColumns" runat="server" name="HiddenColumns" type="hidden" value="0" />   

    <form id="form1" runat="server">
    <a style="display:none;" id="PID" href="Prescription.aspx?keepThis=true&TB_iframe=true&height=480&width=800" class="thickbox" title="View Intervention">No-scroll content</a>  
    <table id="InterventionGrid" cellpadding="0" cellspacing="0">
    </table>    
        <div id="InterventionGridPager">
        </div> 
<%--<input id="reportname" name = "reportname" type="hidden" value="" />     --%>      
<asp:Button ID="ButtonCSV" runat="server" Text="Export as CSV" onclick="ButtonCSV_Click" />  
<asp:Button ID="ButtonExcel" runat="server" Visible="false" Text="Export as Excel" onclick="ButtonExcel_Click" />  
<asp:Button ID="ButtonPDF" runat="server" Text="Export as PDF" onclick="ButtonPDF_Click" />  
 <asp:Button ID="ButtonDelete" runat="server" Text="Delete Report" 
           onclick="ButtonDelete_Click" OnClientClick="return confirm('Do you really want to delete this Report?','Report Delete');"/>
    </form>
</body>
</html>
