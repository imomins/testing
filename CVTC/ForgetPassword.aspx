<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgetPassword.aspx.cs" Inherits="ForgetPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Retrieve Password</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
    <tr>
    
    <td>
    <h2 align="center" 
            style="font-family: Calibri; font-size: large; line-height: normal; vertical-align: middle; background-color: #FFFFCC">Enter Your 
        Valid Email Address to Get Username and Password</h2>
    </td>
    <td>
    </td>
    </tr>
    <tr>
    <td align="center" 
            style="font-family: Calibri; font-size: medium; line-height: normal; vertical-align: baseline; background-color: #CCFFFF">
    Email:   <asp:TextBox ID="txtEmail" runat="server" Width="233px"></asp:TextBox>   
    
        <br />
    
        <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
        <br />
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
            onclick="btnSubmit_Click" />
    </td>
    <td>
        <br />
    </td>
    </tr>
        
    </table>
    </div>
    </form>
</body>
</html>
