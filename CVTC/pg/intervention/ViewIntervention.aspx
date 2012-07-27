<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewIntervention.aspx.cs" Inherits="pg_intervention_ViewIntervention" %>

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
        $(function() {
            $("#InterventionGrid").jqGrid({
                url: 'Intervention.ashx',
                datatype: 'json',
                height: 400,
                //autowidth:false  ,
                width:1050,
                colNames: ['PrescriptionOID', 'Datestamp','Banner Id','Participating', 'Action Date', 'Sponsor', 'Criteria Type', 'Outcome Type','Urgent', 'Internal', 'Prescribed', 'Completed', 'Email', 'Telephone','In Person', 'Hand off', 'Contact Date','Comment'],
                colModel: [
                        { name: 'PrescriptionOID', index: 'PrescriptionOID', width: 20, hidden:true,sortable: false },
                        { name: 'LatestContact', width: 40, sortable: true, editable: true, editrules: { required: true} },
                        { name: 'BannerStudentIDNumber', sortable: true, sortable: true, width: 50, editable: true, editrules: { required: true} },
                        { name: 'Participating', width: 30, sortable: true, editable: true, editrules: { required: true} },
                        { name: 'LatestActionDate', width: 60, sortable: true, editable: true, editrules: { required: true} },
                        { name: 'UserName', width: 50, sortable: true, editable: true, editrules: { required: true} },
                        { name: 'DomainName', width: 40, sortable: true, editable: true, editrules: { required: true} },
                        { name: 'InterventionName', width: 50, sortable: true, editable: true, editrules: { required: true} },
                        { name: 'Urgent', width: 40, sortable: true, editable: true, editrules: { required: true} },
                        { name: 'Internal', width: 40, sortable: true, editable: true, editrules: { required: true} },
                        { name: 'Prescribed', width: 40, sortable: true, editable: true, editrules: { required: true} },
                        { name: 'Completed', width: 40, sortable: true, editable: true, editrules: { required: true} },
                        { name: 'Email', width: 40, sortable: true, editable: true, editrules: { required: true} },
                        { name: 'Telephone', width: 40, sortable: true, editable: true, editrules: { required: true} },
                        { name: 'InPerson', width: 40, sortable: true, editable: true, editrules: { required: true} },
                        { name: 'Handoff', width: 40, sortable: true, editable: true, editrules: { required: true} },
                        { name: 'LatestContact', width: 50, sortable: true, editable: true, editrules: { required: true} },
                        { name: 'Comment', width: 50, sortable: true, editable: true, editrules: { required: true} }
                        
                    ],
                rowNum: 20,
                rowList: [20, 30, 50],
                pager: '#InterventionGridPager',
                sortname: 'PrescriptionOID',
                viewrecords: true,
                sortable: true,
                sortorder: 'asc',
                altRows: true,                
                caption: 'Interventions ',
               // autowidth: true,
                  postData: {
                    reportname: function() { return jQuery("#reportname").val(); },
                    hiddenfields: function() { return jQuery("#hiddenfields").val(); }
                  },                
                
                ondblClickRow:function(id){ 
                
                  var tmp = $("#PID").attr("href");
                  
                  //alert(tmp.substring(0, 18));
                  tmp = tmp.substring(0, 18) + "poid=" + id + "&" + tmp.substring(19, tmp.length);                
                  
                  $("#PID").attr("href", tmp);                
                  $("#PID").click();
               }
            });

            $("#InterventionGrid").jqGrid('navGrid', '#InterventionGridPager', { edit: false, add: false, del: false  });

            options = { autosearch: true };
            $("#InterventionGrid").filterToolbar(options);



            var sgrid = $("#InterventionGrid")[0];
             sgrid.triggerToolbar();

             $("#myButton").click(function () {
                 $("#InterventionGrid").setColumns(options);
                 return false;
             });
             
             
        $(function() {
            jQuery("#saveButton").click( function(){

                var colModel = jQuery("#InterventionGrid").jqGrid ('getGridParam', 'colModel');
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
                 $("#InterventionGrid").trigger("reloadGrid");
                 $("#reportname").val("");
                             
            }); 
        }); 
          

        });
         </script>
 
</head>
<body>
    <form id="form1" runat="server">
    <a style="display:none;" id="PID" href="Prescription.aspx?keepThis=true&TB_iframe=true&height=480&width=800" class="thickbox" title="View Intervention">No-scroll content</a>  
    <table id="InterventionGrid" cellpadding="0" cellspacing="0">
    </table>    
        <div id="InterventionGridPager">
        </div> 
       <input id="reportname" name = "reportname" type="hidden" value="" />    
       <input id="hiddenfields" name = "hiddenfields" type="hidden" value="" /> 
       <input id="myButton" type="button" value="Show/Hide column" />
       <input id="saveButton" type="button" value="Save as Report" />
        
    </form>
</body>
</html>
