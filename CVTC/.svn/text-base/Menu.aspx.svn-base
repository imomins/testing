<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Menu.aspx.cs" Inherits="Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" media="screen" href="themes/redmond/jquery-ui-1.8.1.custom.css" />
<link rel="stylesheet" type="text/css" media="screen" href="themes/ui.jqgrid.css" />


    <script src="js/jquery.js" type="text/javascript"></script>
 
    <script src="js/grid.locale-en.js" type="text/javascript"></script>
 
    <script src="js/jquery.jqGrid.min.js" type="text/javascript"></script>
     <script type="text/javascript">
        $(function() {
        $("#UsersGrid").jqGrid({
                treeGridModel: 'adjacency',
                ExpandColumn: 'name',
                url: 'MyHandler.ashx',
                datatype: "xml", 
                    mtype: "POST", 
                 //       url: '',
               // datatype: 'json',
                height: 250,
                colNames: ['UserOID', 'User Name' ],
                colModel: [
                        { name: 'UserOID', index: 'UserID', width: 100, hidden:true,sortable: false },
                        { name: 'UserName', width: 100, editable: true },                        
                    ],
                rowNum: 10,
                rowList: [10, 20, 30],
                pager: '#UsersGridPager',
                sortname: 'UserOID',
                viewrecords: true,
                sortorder: 'asc',
                altRows: true,
               // editurl: 'UserHandler.ashx',
                caption: 'User Management '
            });

            $("#UsersGrid").jqGrid('navGrid', '#UsersGridPager', { edit: true, add: true, del: true  });

            options = { autosearch: true };
            $("#UsersGrid").filterToolbar(options);



            var sgrid = $("#UsersGrid")[0];
             sgrid.triggerToolbar();

          
             //$("#myButton").click(function () {
             //$("#UsersGrid").setColumns({ 'UserOID': '', 'UserName': '' });
               //  return false;
             //});


        });





    </script>
</head>
<body>
    <form id="form1" runat="server">
   <table id="UsersGrid" cellpadding="0" cellspacing="0">
      
        <div id="UsersGridPager">
        </div>       
      
        
    </table>  
    </form>
</body>
</html>
