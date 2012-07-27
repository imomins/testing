<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddDomain.aspx.cs" Inherits="pg_student_AddDomain" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head id="Head1" runat="server">
    <title>Add Domain</title>
    

<link rel="stylesheet" type="text/css" media="screen" href="../../themes/redmond/jquery-ui-1.8.1.custom.css" />
<link rel="stylesheet" type="text/css" media="screen" href="../../themes/ui.jqgrid.css" />


    <script src="../../js/jquery.js" type="text/javascript"></script>
 
    <script src="../../js/grid.locale-en.js" type="text/javascript"></script>
 
    <script src="../../js/jquery.jqGrid.min.js" type="text/javascript"></script>
 
    <script type="text/javascript">
        $(function() {
            $("#DomainGrid").jqGrid({
                url: 'DomainHandler.ashx',
                datatype: 'json',
                width:200,
                height: 150,
                colNames: ['DomainOID', 'Domain Name'],
                colModel: [
                        { name: 'DomainOID', index: 'UserID', width: 100, hidden:true,sortable: false },
                        { name: 'DomainName', width: 200, editable: true,editrules: {required:true} }
                        
                    ],
                rowNum: 10,
                rowList: [10, 20, 30],
                pager: '#DomainGridPager',
                sortname: 'DomainOID',
                viewrecords: true,
                sortorder: 'asc',
                altRows: true,
                editurl: 'DomainHandler.ashx',
                caption: 'Add Domain '
            });

            $("#DomainGrid").jqGrid('navGrid', '#DomainGridPager', { edit: true, add: true, del: true  });

            options = { autosearch: true };
            $("#DomainGrid").filterToolbar(options);



            var sgrid = $("#DomainGrid")[0];
             sgrid.triggerToolbar();

          

        });




        function Button1_onclick() {
            var tmp;
            tmp = $('#DomainGrid').jqGrid('getDataIDs');
            var i=0;
            for (i = 0; i < tmp.length; i++) {
                alert(tmp[i]);
                         }
        }

    </script>
 
 
 
 
</head>
<body>
    <form id="HtmlForm" runat="server">
    <table id="DomainGrid" cellpadding="0" cellspacing="0">
    </table>   
        <div id="DomainGridPager">
        </div>       
      
        
     
    <input id="Button1" style="display:none;" type="button" value="button" onclick="return Button1_onclick()" />
    </form>
</body>
</html>