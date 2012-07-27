<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tttt.aspx.cs" Inherits="pg_student_tttt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" media="screen" href="../../themes/redmond/jquery-ui-1.8.1.custom.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../themes/ui.jqgrid.css" />


    <script src="../../js/jquery.js" type="text/javascript"></script> 
    <script src="../../js/grid.locale-en.js" type="text/javascript"></script> 
    <script src="../../js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function() {
            $('#treeGrid').jqGrid({
            url: "cars_tree.xml",
            //url: "t.aspx",
                datatype: "xml",
                height: "auto",
                mType: 'GET',
                treeGridModel: 'adjacency',
                colNames: ["my_id", "my_name","url"],
                colModel: [
                { name: "id", width: 1, hidden: true, key: true },
                { name: "name" },
                { name: "url", hidden: true}
                ],
                treeGrid: true,
                caption: "Menu",
                ExpandColumn: "name",
                ExpandColClick: true,
                autowidth: true
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
   <div style="width:180px;">
    <table id="treeGrid">
    </table>
    </div>
    </form>
</body>
</html>
