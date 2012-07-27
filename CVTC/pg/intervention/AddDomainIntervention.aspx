<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddDomainIntervention.aspx.cs" Inherits="pg_intervention_AddDomainIntervention" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../../js/jquery.js"></script>
<script type="text/javascript" src="../../js/jquery-ui-1.8.1.custom.min.js"></script>
    <link href="../../css/global-style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../js/jquery-ui-timepicker-addon.js"></script>
<link href="../../themes/jquery-ui-1.7.2.custom.css" rel='stylesheet' type='text/css' />
<link href="../../themes/jquery.ui.datepicker.css" rel='stylesheet' type='text/css' />

    
 
    <style type="text/css">
        .style1
        {
            width: 504px;
        }
    </style>

    
 
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 60%;">
    <tr><td colspan="3">
    
    <div id="print_div1" runat="server" style="display:none;">
    &nbsp; 
    
    </div><b>Add Intervention</b></td></tr>
        
        
        <tr>
        <td colspan="2" class="style1">Domain &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            <asp:DropDownList ID="DropDownListDomain" runat="server" 
                DataSourceID="SqlDataSourceDomain" DataTextField="DomainName" Width="150px"
                DataValueField="DomainName" 
                AutoPostBack="True" onload="DropDownListDomain_Load"></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSourceDomain" runat="server" 
                ConnectionString="<%$ ConnectionStrings:cvtcConnectionString %>" 
                SelectCommand="SELECT [DomainName] FROM [Domain]"></asp:SqlDataSource>
                &nbsp;<%--<asp:HyperLink ID="hplAddDomain" runat="server"  
                NavigateUrl="~/pg/intervention/AddDomain.aspx" ToolTip="Add Domain" Visible ="false" >Add 
 Domain</asp:HyperLink>
            <br />--%>
            <asp:LinkButton ID="lbtnAddDomain" runat="server" onclick="lbtnAddDomain_Click" 
                Visible="true" PostBackUrl="~/pg/intervention/AddDomain.aspx" 
                Font-Bold="True" Font-Overline="False" Font-Size="Small" Font-Underline="True">Add Domain</asp:LinkButton>
            
                </td>
        
        </tr>
           
       <tr>
       <td colspan="2" class="style1">
       Intervention &nbsp;&nbsp; &nbsp;&nbsp;
            <%--<asp:DropDownList ID="DropDownListIntervention" runat="server" Width="150px" 
                >
                </asp:DropDownList>--%><asp:TextBox ID="txtIntervention" runat="server" 
                Width="186px" Height="19px"></asp:TextBox>
&nbsp;<%--       <asp:SqlDataSource ID="SqlDataSourceInterventions" runat="server" 
                ConnectionString="<%$ ConnectionStrings:cvtcConnectionString %>" 
                
                SelectCommand="SELECT Intervention.InterventionName FROM Domain INNER JOIN Intervention ON Domain.DomainOID = Intervention.DomainID">
            </asp:SqlDataSource>--%>
                </td>
       </tr>
        <tr><td align="right" colspan="2" class="style1">
            <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" 
                onclick="ButtonSubmit_Click" Width="80px" /></td><td>
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </td></tr>
        
    </table>
    </form>
    <script type="text/javascript">
<!--
function printContent(id){
str=document.getElementById(id).innerHTML
newwin=window.open('','printwin','left=100,top=100,width=850,height=900')
newwin.document.write('<HTML>\n<HEAD>\n')
newwin.document.write('<TITLE>Print Page</TITLE>\n')
newwin.document.write('<script>\n')
newwin.document.write('function chkstate(){\n')
newwin.document.write('if(document.readyState=="complete"){\n')
newwin.document.write('window.close()\n')
newwin.document.write('}\n')
newwin.document.write('else{\n')
newwin.document.write('setTimeout("chkstate()",2000)\n')
newwin.document.write('}\n')
newwin.document.write('}\n')
newwin.document.write('function print_win(){\n')
newwin.document.write('window.print();\n')
newwin.document.write('chkstate();\n')
newwin.document.write('}\n')
newwin.document.write('<\/script>\n')
newwin.document.write('</HEAD>\n')
newwin.document.write('<BODY onload="print_win()">\n')
newwin.document.write(str)
newwin.document.write('</BODY>\n')
newwin.document.write('</HTML>\n')
newwin.document.close()
}
//-->
</script>
</body>
</html>
