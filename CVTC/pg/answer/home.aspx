<%@ Page Language="C#" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="pg_answer_home" %>
<%@ Register TagPrefix="ucl" TagName="CalendarDatePicker" Src="~/Control/DatePicker.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login For Assessment</title>
    
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
          width :350px;
         
         
        }
        
         .Label
        {
        
        float: left;
        width: 250px;
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
    
    
</head>


<body>
    <form id="form1" runat="server">
    <table  width="100%">
        <tr style="background-color:#85B5D9">
            <td colspan="3" >
                <h2 > Please Provide Information given Below</h2>
               
            </td>
            
        </tr>
        <tr>
            <td style ="width :25%;">
               <span class ="Label">BannerID :</span> 
                </td>
            <td style ="width :45%;">
                <asp:TextBox ID="TextBoxBannerID" runat="server" CssClass ="TextBox"></asp:TextBox>
                </td>
            <td style ="width :30%;">
               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidatorBannerID" runat="server" 
                    ErrorMessage="Please input Banner ID" ControlToValidate="TextBoxBannerID"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <tr>
            <td>
                
                <span class ="Label">First Name:</span>             </td>
            <td>
                <asp:TextBox ID="TextBoxFirstName" runat="server" CssClass ="TextBox"></asp:TextBox>
                *</td>
            <td>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorFirstName" runat="server" 
                    ErrorMessage="Please input first name" 
                    ControlToValidate="TextBoxFirstName"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        
        <tr>
            <td>
                
                 <span class ="Label">Last Name:</span>              </td>
            <td>
                <asp:TextBox ID="TextBoxLastName" runat="server" CssClass ="TextBox"></asp:TextBox>
                *</td>
            <td>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorLastName" runat="server" 
                    ErrorMessage="Please input Last Name" ControlToValidate="TextBoxLastName"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        
        
        <tr>
            <td>
                
                <span class ="Label">Middle Initial&nbsp; Name:</span>             </td>
            <td>
               <%-- <asp:TextBox ID="TextBoxMiddleName" runat="server" CssClass ="TextBox" 
                    MaxLength="1" ></asp:TextBox>--%>
                    <input  maxlength ="1" type ="text" runat ="server" id="TextBoxMiddleName" style='text-transform:uppercase' class ="TextBox"/>
                </td>
            <td>
               
            </td>
        </tr> 
        
        <tr>
         <td>
       
        <span class ="Label"> Entering Term :</span>
        </td>
         <td >
         <asp:TextBox ID="TextBoxTerm" runat="server" CssClass ="TextBox"></asp:TextBox>
         
        </td>
         <td>
       
        </td>
        </tr>
        
       <tr>
         <td>
        
         <span class ="Label" style ="display :none "> Program Interest :</span>
        </td>
         <td >
       
          <asp:DropDownList Visible ="false"  ID="TextBoxProgram" CssClass ="TextBox " Width ="360px" runat="server" height="30px"> 
            
           </asp:DropDownList>  
        </td>
         <td>
      
        </td>
        </tr>
        
        
         <tr>
        <td>
        
        <span class ="Label" style ="display :none ">Prior Credits question:</span>             
        </td>
         <td>
        
             <asp:TextBox ID="TextBoxPrior" runat="server" CssClass ="TextBox" Visible ="false" ></asp:TextBox>
             </td>
        
        </tr>
        
        
        
        
        <tr>
        <td>
       
         <span class ="Label" >  Date of Birth :</span>
        </td>
        <td>
             <ucl:CalendarDatePicker ID="Date1" runat="server" Visible ="true"  />
             <input id="TextBoxDOB" type="text" class ="TextBox " runat ="server" visible ="false"  />
            
        </td>
        <td>
      
        </td>
        </tr>
        
        <tr>
            <td>
                
            </td>
            <td>
                <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" 
                    onclick="ButtonSubmit_Click" CssClass ="Button"/>
                <asp:Label ID="LabelMsg" runat="server" ForeColor="#FF3300"></asp:Label>
                
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               <%-- <asp:HyperLink ID="hplRegistration" runat="server" 
                    NavigateUrl="/cvtc/Registration.aspx" Target="_blank">Registration</asp:HyperLink>--%>
            </td>
            
            <td>
           <%-- <asp:HyperLink ID="hplRegistration" runat="server" 
                    NavigateUrl="/cvtc/Registration.aspx" Target="_blank">Registration</asp:HyperLink> --%>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Registration</asp:LinkButton>                 
            </td>
        </tr>
        <tr>
        <td colspan ="2" valign ="top" >
       <h2>Instruction for Existing Students</h2>
       If You Have BannerID <br />Just fill up above fields(mandatory) and click on Submit
        </td>
        <td valign ="top" >
        <h2>Instruction for New Students</h2>
         If You Have No BannerID then Follow these steps:<br />1.Click on  <b>Registration</b> link .
        After Completing Registration Process.<br /> 2.A Temporary BannerID Will be Provided to you.<br />
        3.By This temporary BannerID and Other Informations you will be able to Login for taking Assesment.
        </td>
        <td></td>
        </tr>
    </table>
    </form>
</body>
</html>
