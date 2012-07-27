<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendEmail.aspx.cs" Inherits="SendEmail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Send Email</title>
    <link rel="stylesheet" type="text/css" media="screen" href="themes/redmond/jquery-ui-1.8.1.custom.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="themes/ui.jqgrid.css" />
    <link href="css/global-style.css" rel="stylesheet" type="text/css" />     
    <link href="themes/thickbox.css" rel="stylesheet" type="text/css" />
    
    <script src="js/jquery-1.4.2.js" type="text/javascript"></script>
    <script src="js/jquery.accordion.js" type="text/javascript"></script>
    <script src="js/grid.locale-en.js" type="text/javascript"></script> 
    <script src="js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="js/thickbox.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
    <tr>
    <td>
    
    </td>
    <td align ="right" >
        <asp:Button ID="btnSendEmail" runat="server" Text="Send Email"  Width ="200px" 
            onclick="btnSendEmail_Click"/>
            <br />
        <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label> 
        &nbsp;&nbsp;&nbsp;&nbsp;<br /><asp:Label ID="Label1" runat="server" Text="" Font-Bold="True" ForeColor="#FF0066" Font-Size="10"></asp:Label>
    </td>
    </tr>
    </table>
        
    
    </div>
    </form>
</body>
</html>
