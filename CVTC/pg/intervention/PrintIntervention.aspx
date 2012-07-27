<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintIntervention.aspx.cs" Inherits="pg_intervention_PrintIntervention" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Intervention</title>
    <link rel="stylesheet" type="text/css" media="screen" href="../../themes/redmond/jquery-ui-1.8.1.custom.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../themes/ui.jqgrid.css" />

    <link href="../../css/global-style.css" rel="stylesheet" type="text/css" />
    <script src="../../js/jquery.js" type="text/javascript"></script> 
    <script src="../../js/grid.locale-en.js" type="text/javascript"></script> 
    <script src="../../js/jquery.jqGrid.min.js" type="text/javascript"></script>
     <script src="../../js/thickbox.js" type="text/javascript"></script>
    <link href="../../themes/thickbox.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style ="display :none ">
    <b>Assessment :</b>
     <asp:DropDownList 
                ID="ddlAssessment" runat="server" Height="20px" Width="148px">
            </asp:DropDownList>
            <input type="button" value="Print" onclick="window.print();" visible ="false" >

    </div>
    </form>
</body>
</html>
