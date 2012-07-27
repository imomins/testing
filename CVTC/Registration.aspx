<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Registration" %>
<%@ Register TagPrefix="ucl" TagName="CalendarDatePicker" Src="Control/DatePicker.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration Form</title>
    <style type="text/css">
        .TextBox
        {
         border:1px solid #c0c0c0;
         padding:4px;
         font-size:14px;
         color:#B4886B;
         background-color:#ffffff;
         font-family: Verdana, Arial, Helvetica, sans-serif;
          vertical-align :middle;
         
         
        }
        
         .Label
        {
        
        float: left;
        width: 200px;
        font-weight: bold;
         vertical-align :middle;
         color: #72A9D3;
          display: block;
        }
         .H1
        {
        
        float:none ;
        width: 600px;
        font-weight: bold;
         vertical-align :middle;
         color: #72A9D3;
          display: block;
        }
        .TableMargin
        {
        	margin-top:40px;
        	margin-left:70px;
        	margin-right:20px;

        	}
        	
        .Button
        {
    display:block;
  
    margin:0 7px 0 0;
    background-color:#f5f5f5;
    border:1px solid #dedede;
    border-top:1px solid #eee;
    border-left:1px solid #eee;

    font-family:"Lucida Grande", Tahoma, Arial, Verdana, sans-serif;
    font-size:12px;
    line-height:130%;
    text-decoration:none;
    font-weight:bold;
    color:#565656;
    cursor:pointer;
   padding:5px 10px 6px 7px; /* Links */

        	}
        	
      
    </style>
<link href="../../css/global-style.css" rel="stylesheet" type="text/css" />
 <script type="text/javascript" src="../../js/jquery.js"></script>
<script type="text/javascript" src="../../js/jquery-ui-1.8.1.custom.min.js"></script>
<script type="text/javascript" src="../../js/jquery-ui-timepicker-addon.js"></script>
<link href="../../themes/jquery-ui-1.7.2.custom.css" rel='stylesheet' type='text/css' />
<link href="../../themes/jquery.ui.datepicker.css" rel='stylesheet' type='text/css' />
    
 
  <script type="text/javascript">
      //$(function() {
      $(document).ready(function () {
      $('#TextBirthDate').datetimepicker({
              ampm: true,
              showTime: true,
              timeFormat: 'hh:mm TT'

          });

      });
    
</script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    
    
    <table width ="100%" id ="MainTable" cellpadding="5" cellspacing ="10" class="TableMargin">
   <tr>
   <td colspan ="2" align ="center" >
 
   </td>
   </tr>
  
    <tr>
   <td style ="width :80%" align ="center" >
   
     
    <table id ="LeftTable"      width ="100%"  cellpadding ="2" cellspacing ="10">
    <tr>
    <td align ="left" colspan ="2">
      <h1 class="H1" style ="width :100%;">Student Registration Form</h1>
    </td>
    </tr>
     
    
     <tr>
    <td >
    
   <b> <asp:Label ID="Label2" runat="server" Text="First Name :" CssClass ="Label "></asp:Label></b>
   </td>
   <td align ="left" >
    <asp:TextBox ID="TextFirstName"  Width ="200px" runat="server" height="21px" CssClass ="TextBox "
            ></asp:TextBox>
       *</td>
  
    </tr>
   
   <tr>
   <td style ="width :200px">
 
   <b> <asp:Label ID="Label1" runat="server" Text="Last Name :" CssClass ="Label"></asp:Label></b>
   </td>
   <td align ="left" >
    <asp:TextBox ID="TextLastName"  Width ="200px" runat="server"  CssClass ="TextBox "
          Height ="21px"  ></asp:TextBox>
       *</td>
  
 </tr>
   
   <tr>
    <td >
    
        <asp:Label ID="Label6" runat="server" Text="Middle Initial :" 
            CssClass ="Label "></asp:Label>
   </td>
   <td align ="left" >
    <%--<asp:TextBox ID="TextBoxMiddleName"  Width ="200px" runat="server" height="21px" 
           CssClass ="TextBox " MaxLength="1"
            ></asp:TextBox>--%>
            <input  maxlength ="1" type ="text" runat ="server" id="TextBoxMiddleName"   style="text-transform:uppercase; Width:200px;" class ="TextBox"/>
            
       </td>
  
    </tr>
   
   
     <tr>
    <td>
    
    
   <b> <asp:Label ID="Label3" runat="server" Text="Term :" CssClass ="Label "></asp:Label></b>
   </td>
  <td align ="left" >
   <%--<asp:TextBox ID="TextTerm" Width ="200px" runat="server" height="21px" CssClass ="TextBox "
            ></asp:TextBox>*--%>
    <asp:DropDownList DataMember ="ProgramEnrollment" DataValueField ="ProgramEnrollment" ToolTip="Select Term" ID="TextTerm" CssClass ="TextBox " Width ="210px" runat="server" height="30px" >
   </asp:DropDownList>*        
   </td>
  
    </tr>
    
    
     <tr>
   <td style ="width :200px">
 
   <b> <asp:Label ID="Label7" runat="server" Text="Email :" CssClass ="Label"></asp:Label></b>
   </td>
   <td align ="left" >
    <asp:TextBox ID="TextBoxEmail"  Width ="200px" runat="server"  CssClass ="TextBox "
          Height ="21px"  ></asp:TextBox>
       *</td>
  
 </tr>
 
    
     <tr>
    <td >
    
  
    <b> <asp:Label ID="Label27" runat="server" Text="Program Interest :" CssClass ="Label "></asp:Label></b>
   </td>
   <td align ="left" >
   <asp:DropDownList DataMember ="MajorProgramEnrollment" DataValueField ="MajorProgramEnrollment" ToolTip="Select Program Interest" ID="txtProgramInterest" CssClass ="TextBox " Width ="210px" runat="server" height="30px" >
   </asp:DropDownList>*
         
   </td>
   
  
        
   
    </tr>
    
    <tr>
    <td>
    
    
   <b> <asp:Label ID="Label4" runat="server" Text="Do you have prior college credit? :" CssClass ="Label "></asp:Label></b>
   </td>
  <td align ="left" >
  <asp:DropDownList ID ="TextBoxPriorCredit" runat ="server" ToolTip ="Select Prior Credit Question">
  <asp:ListItem Text ="yes" Value ="yes" Selected ="True" ></asp:ListItem>
  <asp:ListItem Text ="no" Value ="no" Selected ="False" ></asp:ListItem>
  </asp:DropDownList>*
   <%--<asp:TextBox ID="TextBoxPriorCredit" Width ="200px" runat="server" height="21px" CssClass ="TextBox "
            ></asp:TextBox>--%>
   </td>
  
    </tr>
   
   <tr>
    <td>
    
    
   <b> <asp:Label ID="Label5" runat="server" Text="Birth Date :" CssClass ="Label "></asp:Label></b>
   </td>
  <td align ="left" >
  <input runat="server" id="TextBirthDate"  visible ="false"  style="width:200px;  Height:21px"  
          Class ="TextBox " maxlength="10"/>
 <ucl:CalendarDatePicker ID="Date1" runat="server" />         
   </td>
  
    </tr>
   
   <tr>
    <td align ="right" >
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
            onclick="btnSubmit_Click" CssClass ="Button" />
    </td>
    <td align ="left" >
     <asp:Button ID="btnCancel" runat="server" Text="Reset" 
            onclick="btnCancel_Click" CssClass ="Button"/>
            <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
        
    </td>
    
    </tr>
   
   
   

   
    </table>
    
   </td>
  
    </tr>
    
    
    </table>
    </div>
    </form>
</body>
</html>
