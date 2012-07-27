<%@ Page Language="C#" AutoEventWireup="true" CodeFile="temp.aspx.cs" Inherits="pg_student_temp" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Demonstrate how to use Tree Grid for the Adjacency Set Model</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link rel="stylesheet" type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.10/themes/redmond/jquery-ui.css" />
    <link rel="stylesheet" type="text/css" href="http://www.ok-soft-gmbh.com/jqGrid/jquery.jqGrid-3.8.2/css/ui.jqgrid.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.5.1/jquery.js"></script>
    <script type="text/javascript" src="http://www.ok-soft-gmbh.com/jqGrid/jquery.jqGrid-3.8.2/js/i18n/grid.locale-en.js"></script>

    <script type="text/javascript" src="http://www.ok-soft-gmbh.com/jqGrid/jquery.jqGrid-3.8.2/js/jquery.jqGrid.min.js"></script>

    <script type="text/javascript">
        //<![CDATA[
        $(function() {
            var mydata = [
                {id:"1",Name:"Main Menu",MenuId:"1",MenuName:"Menu1",
                 level:"0", parent:"", isLeaf:false, expanded:false},
                {id:"1_1",Name:"Sub Menu",MenuId:"1",MenuName:"Menu1",
                 level:"1", parent:"1", isLeaf:false, expanded:false},
                {id:"1_1_1",Name:"Sub Sub Menu",MenuId:"1",MenuName:"Menu1",
                 level:"2", parent:"1_1", isLeaf:true, expanded:false},
                {id:"1_2",Name:"Sub Menu1",MenuId:"1",MenuName:"Menu1",
                 level:"1", parent:"1", isLeaf:true, expanded:false},
                {id:"2",Name:"Main Menu1",MenuId:"2",MenuName:"Menu2",
                 level:"0", parent:"", isLeaf:false, expanded:true},
                {id:"2_1",Name:"Main Menu",MenuId:"2",MenuName:"Menu2",
                 level:"1", parent:"2", isLeaf:true, expanded:false},
                {id:"2_2",Name:"Main Menu",MenuId:"2",MenuName:"Menu2",
                 level:"1", parent:"2", isLeaf:true, expanded:false},
                {id:"3",Name:"Main Menu2",MenuId:"3",MenuName:"Menu3",
                 level:"0", parent:"", isLeaf:false, expanded:false},
                {id:"3_1",Name:"Main Menu",MenuId:"3",MenuName:"Menu3",
                 level:"1", parent:"3", isLeaf:true, expanded:false},
                {id:"3_2",Name:"Main Menu",MenuId:"3",MenuName:"Menu3",
                 level:"1", parent:"3", isLeaf:true, expanded:false}
            ],
             grid = $("#treegridsamp");

            grid.jqGrid({
                datatype: "local",
                data: mydata, // will not used at the loading,
                // but during expandedanding/collapsing the nodes
                colNames: ['id', 'Name', 'MenuId', 'Menu Name'],
                colModel: [
                    { name: 'id', index: 'id', width: 100, hidden: true },
                    { name: 'Name', index: 'Name', width: 150, hidden: false },
                    { name: 'MenuId', index: 'MenuId', width: 100, hidden: true },
                    { name: 'MenuName', index: 'MenuName', width: 100, hidden: true }
                ],
                height: '100%',
                rowNum: 10000,
                //pager : "#ptreegrid",
                sortname: 'id',
                treeGrid: true,
                treeGridModel: 'adjacency',
                treedatatype: "local",
                expandedandColumn: 'Name',
                caption: "Sample Tree View Model"
            });
            // we have to use addJSONData to load the data
            grid[0].addJSONData({
                total: 1,
                page: 1,
                records: mydata.length,
                rows: mydata
            });
        });
        //]]>
    </script>
</head>
<body>
    <table id="treegridsamp"><tr><td/></tr></table>
    <div id="dvtreegridsamp"/>
</body>
</html>
