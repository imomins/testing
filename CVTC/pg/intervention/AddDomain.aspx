<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddDomain.aspx.cs" Inherits="pg_intervention_AddDomainIntervention" %>

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
    <table style="width: 60%; margin :40px;40px;40px;40px;">
    <tr><td colspan="3">
    
    <div id="print_div1" runat="server" style="display:none;">
    &nbsp; 
    
    </div>
    
    <h2 > Add Domain</h2>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" 
            onclick="btnRefresh_Click" />
        </td></tr>
        
        
        <tr>
        <td colspan="2" class="style1">&nbsp;</td>
        
        </tr>
           
       <tr>
       <td colspan="2" class="style1">
           Domain&nbsp;&nbsp; &nbsp;&nbsp;
           
               <asp:TextBox ID="txtIntervention" runat="server" 
                Width="230px" Height="19px"></asp:TextBox>*
&nbsp; 
<%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorBannerID" runat="server" 
                    ErrorMessage="Please Enter Domain Name" ControlToValidate="txtIntervention"></asp:RequiredFieldValidator>--%>
                </td>
       </tr>
        <tr>
        <td align="center"   class="style1">
            <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" 
                onclick="ButtonSubmit_Click" Width="80px" />
                <br />
                <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
                <td>
                
            </td>
            </tr>
        
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
