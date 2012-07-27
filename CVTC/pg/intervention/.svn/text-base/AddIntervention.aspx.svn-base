<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddIntervention.aspx.cs" Inherits="pg_intervention_AddIntervention" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Add Intervention</title>
    

<link rel="stylesheet" type="text/css" media="screen" href="../../themes/redmond/jquery-ui-1.8.1.custom.css" />
<link rel="stylesheet" type="text/css" media="screen" href="../../themes/ui.jqgrid.css" />


    <script src="../../js/jquery.js" type="text/javascript"></script>
 
    <script src="../../js/grid.locale-en.js" type="text/javascript"></script>
 
    <script src="../../js/jquery.jqGrid.min.js" type="text/javascript"></script>
 
    <script type="text/javascript">
        $(function() {
            $("#InterventionGrid").jqGrid({
                url: 'InterventionHandler.ashx',
                datatype: 'json',
                height: 200,
                width: 450,
                colNames: ['InterventionOID', 'Domain Name','InterventionName'],
                colModel: [
                        { name: 'InterventionOID', index: 'InterventionID', width: 100, hidden:true,sortable: false },
                        { name: 'DomainName', width: 300, editable: true ,editrules: {required:true}},
                         {name: 'InterventionName', width: 300, editable: true ,editrules: {required:true}  }
                        
                    ],
                rowNum: 10,
                rowList: [10, 20, 30],
                pager: '#InterventionGridPager',
                sortname: 'DomainOID',
                viewrecords: true,
                sortorder: 'asc',
                altRows: true,
                editurl: 'InterventionHandler.ashx',
                caption: 'View Prescription Choices'
            });

            $("#InterventionGrid").jqGrid('navGrid', '#InterventionGridPager', { edit: false , add: false, del: true  });

            options = { autosearch: true };
            $("#InterventionGrid").filterToolbar(options);



            var sgrid = $("#InterventionGrid")[0];
             sgrid.triggerToolbar();

          

        });




        function Button1_onclick() {
            var tmp;
            tmp = $('#InterventionGrid').jqGrid('getDataIDs');
            var i=0;
            for (i = 0; i < tmp.length; i++) {
                alert(tmp[i]);
                         }
        }

    </script>
 
 
 
 
</head>
<body>
    <form id="HtmlForm" runat="server">
    <table id="InterventionGrid" cellpadding="0" cellspacing="0">
    </table>    
        <div id="InterventionGridPager">
        </div>       
      
        
    
    <input id="Button1" style="display:none;" type="button" value="button" onclick="return Button1_onclick()" />
    </form>
</body>
</html>
