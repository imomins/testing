<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FinishRegistration.aspx.cs" Inherits="pg_answer_FinishRegistration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <style type="text/css">
       
        .DivMargin
        {
        	margin-top:40px;
        	margin-left:150px;
        	margin-right:20px;

        	}
    </style>
    <title>Finish Registration</title>
     <script type="text/javascript" src="../../js/jquery.js"></script>
<script type="text/javascript" src="../../js/jquery-ui-1.8.1.custom.min.js"></script>
    <link href="../../css/global-style.css" rel="stylesheet" type="text/css" />
    
<link href="../../themes/jquery-ui-1.7.2.custom.css" rel='stylesheet' type='text/css' />
<link href="../../themes/jquery.ui.datepicker.css" rel='stylesheet' type='text/css' />
<script type="text/javascript" src="../../js/jquery-ui-timepicker-addon.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class ="DivMargin ">
    <h1 style="background-color:#85B5D9">You Have Successfully Completed Registration Process....!</h1>
    <b>
        <asp:Label ID="Label1" runat="server" Text="BannerID : "></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </b><asp:Label ID="lblBannerID" runat="server" Text=""></asp:Label><br />
        
    <b>
        <asp:Label ID="Label2" runat="server" Text="LastName :"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </b><asp:Label ID="lblLastName" runat="server" Text=""></asp:Label><br />
    <b>
        <asp:Label ID="Label3" runat="server" Text="FirstName : "></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </b><asp:Label ID="lblFirstName" runat="server" Text=""></asp:Label><br />
        
        <b>
        <asp:Label ID="Label7" runat="server" Text="Middle Initial Name : "></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </b><asp:Label ID="lblMiddleName" runat="server" Text=""></asp:Label><br />
    <b>
        <asp:Label ID="Label5" runat="server" Text="Term : "></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </b><asp:Label ID="lblTerm" runat="server" Text=""></asp:Label><br />    
        <b>
        <asp:Label ID="Label6" runat="server" Text="Program Interest : "></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </b><asp:Label ID="lblProgram" runat="server" Text=""></asp:Label><br />
        
        <h2 style="background-color:#85B5D9">
            <asp:Label ID="Label4" runat="server" Text="Now You Can take Your Exam By Above Information ........"></asp:Label></h2>
    
        <p>
            <asp:Label ID="lblError" runat="server" ForeColor="#FF0066"></asp:Label>
        </p>
    
    </div>
    </form>
</body>
</html>
