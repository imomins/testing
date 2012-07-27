<%@ Page Language="C#" AutoEventWireup="true" CodeFile="prescription.aspx.cs" Inherits="pg_assessment_prescription" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
 <link href="../../css/global-style.css" rel="stylesheet" type="text/css" />
 <script type="text/javascript" src="../../js/jquery.js"></script>
<script type="text/javascript" src="../../js/jquery-ui-1.8.1.custom.min.js"></script>
<script type="text/javascript" src="../../js/jquery-ui-timepicker-addon.js"></script>
<link href="../../themes/jquery-ui-1.7.2.custom.css" rel='stylesheet' type='text/css' />
<link href="../../themes/jquery.ui.datepicker.css" rel='stylesheet' type='text/css' />

<script type="text/javascript">
    $(function() {
        $('#TextBoxLatestAction').datetimepicker({
            ampm: true,
            showTime: true,
            timeFormat: 'hh:mm TT'

        });

        $('#TextBoxLatestContact').datetimepicker({
            ampm: true,
            showTime: true,
            timeFormat: 'hh:mm TT'
        });
    });
    
</script>

</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 60%;">
        <tr ><td colspan="2"><h3>Success Prescription</h3><br />
            <br />
                <asp:Label ID="LabelStatus" runat="server" Text="" Font-Size="Medium" Font-Bold="True"></asp:Label>
            </td>
            <td>Advocate&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="DropDownListAdvocate" runat="server" 
                    DataSourceID="SqlDataSourceUser" DataTextField="UserName" Width="150"
                    DataValueField="UserName">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceUser" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:cvtcConnectionString %>" 
                    SelectCommand="SELECT [UserName],[UserOID] FROM [User] WHERE ([Advocacy] = @Advocacy)">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="Yes" Name="Advocacy" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                
            </td>
        </tr>
        <tr>
        <td colspan="2">Domain &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            <asp:DropDownList ID="DropDownListDomain" runat="server" 
                DataSourceID="SqlDataSourceDomain" DataTextField="DomainName" Width="150"
                DataValueField="DomainName" 
                onselectedindexchanged="DropDownListDomain_SelectedIndexChanged" AutoPostBack ="true" ></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSourceDomain" runat="server" 
                ConnectionString="<%$ ConnectionStrings:cvtcConnectionString %>" 
                SelectCommand="SELECT [DomainName] FROM [Domain]"></asp:SqlDataSource>
                </td>
        <td>Intervention &nbsp;&nbsp; &nbsp;&nbsp;
            <asp:DropDownList ID="DropDownListIntervention" runat="server" Width="150"
                DataSourceID="SqlDataSourceInterventions" DataTextField="InterventionName" 
                DataValueField="InterventionName"></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSourceInterventions" runat="server" 
                ConnectionString="<%$ ConnectionStrings:cvtcConnectionString %>" 
                SelectCommand="SELECT [InterventionName] FROM [Intervention]">
            </asp:SqlDataSource>
                </td>
        </tr>
        <tr><td colspan="2">latest Action &nbsp;&nbsp;<input runat="server" id="TextBoxLatestAction" style="width:130px;"/> <%--<asp:TextBox ID="TextBoxLatestAction" runat="server"></asp:TextBox>--%><asp:Label 
                ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
                </td>
            <td>Latest Contact&nbsp;&nbsp;<input runat="server" id="TextBoxLatestContact" style="width:130px;"/><%--<asp:TextBox ID="TextBoxLatestContact" runat="server"></asp:TextBox>--%> 
                <asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td></tr>
        <tr><td colspan="2"><br /></td><td></td></tr>
        <tr >
        <td align="left" valign="top">    <asp:CheckBoxList ID="CheckBoxListLeft" 
                runat="server" Width="146px">
                <asp:ListItem>Urgent</asp:ListItem>
                <asp:ListItem>Internal</asp:ListItem>
                <asp:ListItem>Prescribed</asp:ListItem>
                <asp:ListItem>Participating</asp:ListItem>
                <asp:ListItem>Completed</asp:ListItem>
            </asp:CheckBoxList></td>
        <td align="left" valign="top">   <asp:CheckBoxList ID="CheckBoxListRight" 
                runat="server" Width="135px">
                <asp:ListItem>Email</asp:ListItem>
                <asp:ListItem>Telephone</asp:ListItem>
                <asp:ListItem>In Person</asp:ListItem>
                <asp:ListItem>Hand-Off</asp:ListItem>
                </asp:CheckBoxList>
                <asp:CheckBox ID="CheckBoxTesting" runat="server" Text="Testing" /></td>
            <td align="right" valign="bottom" style ="display :none ">
                <img alt="printer" src="../../images/Actions-document-print-icon.png" /></td> 
                    </tr>
        <tr><td colspan="3">Contact Notes<br />
            <asp:TextBox ID="TextBoxContactNotes" runat="server" Height="100px" 
                Width="500px" TextMode="MultiLine"></asp:TextBox></td></tr>
        <tr><td align="right" colspan="2">
            <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" 
                onclick="ButtonSubmit_Click" Width="80px" /></td><td>
                &nbsp;</td></tr>
        
    </table>
    </form>
</body>
</html>


