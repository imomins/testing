<%@ Page Language="C#" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="pg_user_User" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head id="Head1" runat="server">
    <title>User Management</title>
    

<link rel="stylesheet" type="text/css" media="screen" href="../../themes/redmond/jquery-ui-1.8.1.custom.css" />
<link rel="stylesheet" type="text/css" media="screen" href="../../themes/ui.jqgrid.css" />


    <script src="../../js/jquery.js" type="text/javascript"></script>
 
    <script src="../../js/grid.locale-en.js" type="text/javascript"></script>
 
    <script src="../../js/jquery.jqGrid.min.js" type="text/javascript"></script>
 
    <script type="text/javascript">
        $(function() {
       
        
            $("#UsersGrid").jqGrid({
                url: 'UserHandler.ashx',
                datatype: 'json',
                height: 250,
                width:1050,
                colNames: ['UserOID', 'User Name', 'Password', 'First Name', 'Last Name', 'Phone', 'Email', 'Advocacy','Active','Action'],
                colModel: [
                        { name: 'UserOID', index: 'UserID', width: 100, hidden:true,sortable: false },
                        { name: 'UserName', width: 100, editable: true,editrules: {required:true} },
                        { name: 'Password', width: 100, editable: true, editrules: {edithidden:true,required:true},  hidden: true, edittype:'password' },
                        { name: 'FirstName', width: 100, sortable: true, editable: true },
                        { name: 'LastName', width: 100, sortable: true, editable: true },                        
                        { name: 'Phone', width: 100, sortable: true, editable: true },
                        { name: 'Email', width: 150, sortable: true, editable: true ,editrules: {email:true,required:true}},
//                        { name: 'Email', width: 150, sortable: true, editable: true ,editrules: {email:true}},
                        { name: 'Advocacy', index: "Advocacy", width: 80, align: "center", sortable: false, editable: true, edittype: "checkbox", editoptions: { value: "Yes:No"} },
                        { name: 'Freez', index: "Freez", width: 80, align: "center", sortable: false, editable: true, edittype: "checkbox", editoptions: { value: "Yes:No"} },
                        { name: 'Action', width: 60, sortable: true,hidden:true  }
                    ],
                rowNum: 10,
                rowList: [10, 20, 30],
                pager: '#UsersGridPager',
                sortname: 'UserOID',
                viewrecords: true,
                sortorder: 'asc',
                altRows: true,
                editurl: 'UserHandler.ashx',
                caption: 'User Management '
//                 col: {
//                     caption: "Show/Hide Columns",
//                     bSubmit: "Submit",
//                     bCancel: "Cancel"
//                 } 
            });

            $("#UsersGrid").jqGrid('navGrid', '#UsersGridPager', { edit: true, add: true, del: true  });

            options = { autosearch: true };
            $("#UsersGrid").filterToolbar(options);



            var sgrid = $("#UsersGrid")[0];
             sgrid.triggerToolbar();

//            $("#myButton").click(function () {
//            $("#UsersGrid").setColumns(options);
//                 return false;
//             });
             
//            $("#myButton").click(function () {
//           $("#UsersGrid").setColumns();
//                return false;
//             });


        });




        function Button1_onclick() {
//            var rows = jQuery("#UsersGrid").jqGrid('getRowData');
//            var paras = new Array();
//            for (var i = 0; i < rows.length; i++) {
//                var row = rows[i];
//                paras.push($.param(row));
//                alert(row[0]);
//            }
//            $.ajax({
//                type: "POST",
//                url: "User.aspx/ServerSideMethod",
//                data: paras.join('and'),
//                success: function(msg) {
//                    //alert(msg);
//                }
            //            });
            var tmp;
            //tmp = $('#UsersGrid').jqGrid('getGridParam', 'rowNum')
//            alert(tmp);
//            tmp = $('#UsersGrid').jqGrid('getGridParam', 'sortname');
//            alert(tmp);
//            tmp = $('#UsersGrid').jqGrid('getGridParam', 'sortorder');
//            alert(tmp);
//            tmp = $('#UsersGrid').jqGrid('getGridParam', 'page');
//            alert(tmp);

            //tmp = $('#UsersGrid').jqGrid('getGridParam', 'postData');
            tmp = $('#UsersGrid').jqGrid('getDataIDs');
            //alert(tmp);
            var i=0;
            for (i = 0; i < tmp.length; i++) {
                alert(tmp[i]);
                         }
//            var savedata = $("#UsersGrid").jqGrid('jqGridExport', { exptype: "jsonstring" });
//            alert(savedata);
//            var t = "{ 'str': '" + savedata + "'} ";
//            $.ajax({
//                type: "POST",
//                url: "User.aspx/ServerSideMethod",
//                //data: "{}",
//                data: t,
//                contentType: "application/json; charset=utf-8",
//                dataType: "json",
//                async: true,
//                cache: false,
//                success: function(msg) {
//                    //alert(msg.d);
//                    //$('#myDiv').text(msg.d);

//                }

//            })
        }

    </script>
 
 
 
 
</head>
<body>
    <form id="HtmlForm" runat="server">
    <table id="UsersGrid" cellpadding="0" cellspacing="0">
    </table> 
        <div id="UsersGridPager">
        </div>       
      
        
     
     <%--<input id="myButton" type="button" value="Show/Hide column" />--%>
    </form>
</body>
</html>