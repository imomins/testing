<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Prescription.aspx.cs" Inherits="pg_intervention_Prescription" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr ><td>Success Prescription</td>
            <td colspan="2">
                <asp:DropDownList ID="DropDownListAdvocate" runat="server">
                </asp:DropDownList>&nbsp;&nbsp;&nbsp;Advocate
            </td>
        </tr>
        <tr>
        <td>Domain &nbsp;&nbsp; <asp:DropDownList ID="DropDownListDomain" runat="server"></asp:DropDownList></td>
        <td colspan="2">Intervention &nbsp;&nbsp; <asp:DropDownList ID="DropDownListIntervention" runat="server"></asp:DropDownList></td>
        </tr>
        <tr><td>latest Action &nbsp;&nbsp;<asp:TextBox ID="TextBoxLatestAction" runat="server"></asp:TextBox></td>
            <td colspan="2">Latest Contact&nbsp;&nbsp;<asp:TextBox ID="TextBoxLatestContact" runat="server"></asp:TextBox> </td></tr>
        <tr><td><br /></td><td colspan="2"></td></tr>
        <tr >
        <td align="left" valign="top">    <asp:CheckBoxList ID="CheckBoxListLeft" runat="server">
                <asp:ListItem>Urgent</asp:ListItem>
                <asp:ListItem>Internal</asp:ListItem>
                <asp:ListItem>Prescribed</asp:ListItem>
                <asp:ListItem>Participating</asp:ListItem>
                <asp:ListItem>Completed</asp:ListItem>
            </asp:CheckBoxList></td>
            <td align="left" valign="top"><asp:CheckBoxList ID="CheckBoxListRight" runat="server">
                <asp:ListItem>Email</asp:ListItem>
                <asp:ListItem>Telephone</asp:ListItem>
                <asp:ListItem>In Person</asp:ListItem>
                <asp:ListItem>Hand-Off</asp:ListItem>
                </asp:CheckBoxList>
                </td> 
            <td align="left" valign="top">
                <asp:CheckBox ID="CheckBoxTesting" runat="server" Text="Testing" />
                <img alt="printer" src="../../images/Printer.JPG" /></td> 
                    </tr>
        <tr><td >Contact Notes<br />
            <asp:TextBox ID="TextBoxContactNotes" runat="server" Height="100px" 
                Width="500px"></asp:TextBox></td></tr>
        <tr><td align="right">
            <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" 
                onclick="ButtonSubmit_Click" Width="80px" /></td><td colspan="2"></td></tr>
        
    </table>
    </form>
</body>
</html>
