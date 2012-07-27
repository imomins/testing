<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Update.aspx.cs" Inherits="pg_student_Update" %>
<%@ Register TagPrefix="ucl" TagName="CalendarDatePicker" Src="~/Control/DatePicker.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Update Student</title>
    <style type="text/css">
        .TextBox
        {
         border:1px solid #c0c0c0;
         padding:4px;
         font-size:14px;
      
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
        	
      
        .style1
        {
            font-weight: bold;
            text-align: right;
        }
        	
      
    </style>
    <script type="text/javascript" src="../../js/jquery.js"></script>
    <script src="../../js/thickbox.js" type="text/javascript"></script>
    <link href="../../themes/thickbox.css" rel="stylesheet" type="text/css" />
    <link href="../../css/global-style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../js/jquery-ui-1.8.1.custom.min.js"></script>
    <script type="text/javascript" src="../../js/jquery-ui-timepicker-addon.js"></script>
    <link href="../../themes/jquery-ui-1.7.2.custom.css" rel='stylesheet' type='text/css' />
    <link href="../../themes/jquery.ui.datepicker.css" rel='stylesheet' type='text/css' />
    


  <script type="text/javascript">
//      $(function() {
//      $('#TextHSGradDate').datetimepicker({
//              ampm: true,
//              showTime: true,
//              timeFormat: 'hh:mm TT'

//          });

//          $('#TextLatestActionDate').datetimepicker({
//              ampm: true,
//              showTime: true,
//              timeFormat: 'hh:mm TT'
//          });
//      });
    
</script>



</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HiddenField ID="HiddenFieldName" runat="server" />
        <asp:HiddenField ID="HiddenFieldTerm" runat="server" />
        <asp:HiddenField ID="HiddenFieldBirthDate" runat="server" />
    
  
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
      <h1 class="H1" style ="width :100%;">Update Student Information</h1>
    </td>
    </tr>
    <tr>
    <td>
    
    </td>
    <td>
    <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
    </td>
    </tr>
    <tr>
   <td style ="width :200px" class="style1">
 
       <asp:Label ID="Label31" runat="server" Text="Banner ID :" CssClass ="Label"></asp:Label>
   </td>
   <td align ="left" >
    <asp:TextBox ID="TextBoxBannerID"  Width ="200px" runat="server"  CssClass ="TextBox "
          Height ="26px"  ReadOnly="True"></asp:TextBox>
       *<%--<asp:Button ID="btnMerge" runat="server" 
           Text="Merge With Original ID" Width="142px" OnClientClick="javascript:window.open('"++"','Merging Student', 'width=400,height=200,scrollbars=yes');" />--%>
           <asp:Button ID="btnMerge" runat="server" 
           Text="Merge With Original ID" Width="142px" onclick="btnMerge_Click" OnClientClick="aspnetForm.target ='_blank';" />
        </td>
  
 </tr>
 
 <tr>
    <td class="style1" >
    
        <asp:Label ID="Label2" runat="server" Text="First Name :" CssClass ="Label "></asp:Label>
   </td>
   <td align ="left" >
    <asp:TextBox ID="TextFirstName"  Width ="200px" runat="server" Height ="26px" CssClass ="TextBox "
            ></asp:TextBox>
       *</td>
  
    </tr>
    
     <tr>
   <td style ="width :200px" class="style1">
 
       <asp:Label ID="Label1" runat="server" Text="Last Name :" CssClass ="Label"></asp:Label>
   </td>
   <td align ="left" >
    <asp:TextBox ID="TextLastName"  Width ="200px" runat="server"  CssClass ="TextBox "
          Height ="26px"  ></asp:TextBox>
       *</td>
  
 </tr>
    
     
   
    <tr>
    <td class="style1" >
    
        <asp:Label ID="Label4" runat="server" Text="Middle Name :" CssClass ="Label "></asp:Label>
   </td>
   <td align ="left" >
    <%--<asp:TextBox ID="TextMiddleName"  Width ="200px" runat="server" Height ="26px" CssClass ="TextBox "
            ></asp:TextBox>--%>
            <input  maxlength ="1" type ="text" runat ="server" id="TextMiddleName" style='text-transform:uppercase' class ="TextBox"/>
        </td>
  
    </tr>
     
     <tr>
    <td class="style1">
    
    
        <asp:Label ID="Label3" runat="server" Text="Term :" CssClass ="Label "></asp:Label>
   </td>
  <td align ="left" >
   <asp:TextBox ID="TextTerm" Width ="200px" runat="server" Height ="26px" CssClass ="TextBox "
            ></asp:TextBox>
   </td>
  
    </tr>
    
     <tr>
    <td class="style1" >
    
  
        <asp:Label ID="Label27" runat="server" Text="Program Interest :" CssClass ="Label "></asp:Label>
   </td>
   <td align ="left" >
   <asp:DropDownList DataMember ="MajorProgramEnrollment" DataValueField ="MajorProgramEnrollment" ToolTip="Select Program Interest" ID="txtProgramInterest" CssClass ="TextBox " Width ="210px" runat="server" height="30px" 
            ></asp:DropDownList>
         
   </td>
   
  
        
   
    </tr>
    
   <tr>
    <td class="style1">
    
    
        <asp:Label ID="Label28" runat="server" Text="Birth Date :" CssClass ="Label "></asp:Label>
   </td>
  <td align ="left" >
  <input runat="server" id="TextBirthDate" style="width:200px; Height:27px"  
          Class ="TextBox " maxlength="10" visible ="true" />
    <asp:RequiredFieldValidator runat="server"
          id="RequiredFieldDOB" ControlToValidate="TextBirthDate" SetFocusOnError="true"
          cssClass="cred2"
          ErrorMessage = "Please enter your Date of Birth."
          display="Dynamic"> </asp:RequiredFieldValidator>
             <asp:RegularExpressionValidator id="RegularExpressionDOB" runat="server"
            ControlToValidate="TextBirthDate" SetFocusOnError="true"
            ValidationExpression="^((((0[13578])|([13578])|(1[02]))[\/](([1-9])|([0-2][0-9])|(3[01])))|(((0[469])|([469])|(11))[\/](([1-9])|([0-2][0-9])|(30)))|((2|02)[\/](([1-9])|([0-2][0-9]))))[\/]\d{4}$|^\d{4}$"
            Display="Static"
            cssClass="cred2">
 Invalid date format. Date format should be 1/1/2009 or 01/01/2009.
 </asp:RegularExpressionValidator>      
    <%--<ucl:CalendarDatePicker ID="Date1" runat="server" />  --%>      
   </td>
  
    </tr>
    
    <tr>
    <td class="style1">
    
    
        <asp:Label ID="Label29" runat="server" Text="Prior Credit Question :" CssClass ="Label "></asp:Label>
   </td>
  <td align ="left" >
   <asp:TextBox ID="TextBoxPriorCredit" Width ="200px" runat="server" Height ="26px" CssClass ="TextBox "
            ></asp:TextBox>
   </td>
  
    </tr>
    
    <tr>
    <td class="style1" >
    
  
        <asp:Label ID="Label30" runat="server" Text="Status :" CssClass ="Label "></asp:Label>
   </td>
   <td align ="left" >
   <asp:DropDownList  ToolTip="Select Status" ID="DropDownListStatus" CssClass ="TextBox " Width ="210px" runat="server" height="30px" 
           
            Enabled="False"></asp:DropDownList>
         
   </td>
   
  
        
   
    </tr>
    
   <tr>
    <td align ="right" >
        <asp:Button ID="btnSubmit" runat="server" Text="Update" 
            onclick="btnSubmit_Click" CssClass ="Button" Height ="30px" Width ="90px" />
    </td>
    <td align ="left" >
     <asp:Button ID="btnCancel" runat="server" Text="Reset" 
            onclick="btnCancel_Click" CssClass ="Button" Height ="30px" Width ="60px"/>
            
        
    </td>
   
    </tr>
 
    
    </table>
    </div>
    </form>
</body>
</html>
