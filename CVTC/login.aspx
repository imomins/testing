<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>CVTC</title>
<script type="text/javascript" src="js/jquery.js"></script>
<%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
--%>
<link href="css/jquery-ui-1.8.1.custom.css" rel="stylesheet" type="text/css" />
<link href="css/global-style.css" rel="stylesheet" type="text/css" />
    <link href="css/login-style.css" rel="stylesheet" type="text/css" />
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
        width: 100px;
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
        	
      .button {
   border-top: 1px solid #96d1f8;
   background: #65a9d7;
   background: -webkit-gradient(linear, left top, left bottom, from(#3e779d), to(#65a9d7));
   background: -webkit-linear-gradient(top, #3e779d, #65a9d7);
   background: -moz-linear-gradient(top, #3e779d, #65a9d7);
   background: -ms-linear-gradient(top, #3e779d, #65a9d7);
   background: -o-linear-gradient(top, #3e779d, #65a9d7);
   padding: 5px 10px;
   -webkit-border-radius: 8px;
   -moz-border-radius: 8px;
   border-radius: 8px;
   -webkit-box-shadow: rgba(0,0,0,1) 0 1px 0;
   -moz-box-shadow: rgba(0,0,0,1) 0 1px 0;
   box-shadow: rgba(0,0,0,1) 0 1px 0;
   text-shadow: rgba(0,0,0,.4) 0 1px 0;
   color: white;
   font-size: 14px;
   font-family: Georgia, serif;
   text-decoration: none;
   vertical-align: middle;
   }
.button:hover {
   border-top-color: #28597a;
   background: #28597a;
   color: #ccc;
   }
.button:active {
   border-top-color: #1b435e;
   background: #1b435e;
   }
    </style>
   
<script type="text/javascript">
    $(document).ready(function() {

        var id = '#dialog';

        //Get the screen height and width
        var maskHeight = $(document).height();
        var maskWidth = $(window).width();

        //Set heigth and width to mask to fill up the whole screen
        $('#mask').css({ 'width': maskWidth, 'height': maskHeight });

        //transition effect		
        $('#mask').fadeIn(1000);
        $('#mask').fadeTo("slow", 0.8);

        //Get the window height and width
        var winH = $(window).height();
        var winW = $(window).width();

        //Set the popup window to center
        $(id).css('top', winH / 2 - $(id).height() / 2);
        $(id).css('left', winW / 2 - $(id).width() / 2);

        //transition effect
        $(id).fadeIn(2000);

        //if close button is clicked
        $('.window .close').click(function(e) {
            //Cancel the link behavior
            e.preventDefault();

            $('#mask').hide();
            $('.window').hide();
        });

        //if mask is clicked
        $('#mask').click(function() {
            $(this).hide();
            $('.window').hide();
        });

    });

</script>

<style type="text/css">
body {
font-family:verdana;
font-size:15px;
}

a {color:#333; text-decoration:none}
a:hover {color:#ccc; text-decoration:none}

#mask {
  position:absolute;
  left:0;
  top:0;
  z-index:9000;
  background-color:#000;
  display:none;
}  
#boxes .window {
  position:absolute;
  left:0;
  top:0;
  width:440px;
  height:200px;
  display:none;
  z-index:9999;
  padding:20px;
}
#boxes #dialog {
  width:375px; 
  height:203px;
  padding:0px;
  background-color:#ffffff;
}
</style>
</head>
<body>

<div id="boxes">
<div style="top: 199.5px; left: 551.5px; display: block;" id="dialog" class="window">
<form id="Form1" runat="server">
    <table style="width: 100%; " >
        <tr >
            <td colspan="3" style="background-color: #85B5D9" align="center">
               <span style ="font-size :medium "><b>Login</b></span> 
            </td>
            
        </tr>
        <tr >
            <td colspan="3">
                &nbsp;
            </td>
            
        </tr>
        <tr >
            <td colspan="3">
             <span>Username and password are case sensitive.</span> 
            </td>
            
        </tr>
        <tr>
            <td style="padding-left:10px">
                <span ><b>User Name</b></span>
                
            </td>
            <td>
                <asp:TextBox ID="TextBoxUserName" runat="server" Width="250" CssClass="TextBox " 
                ></asp:TextBox>
                
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
       <tr>
            <td style="padding-left:10px">
            <span><b>Password</b></span>      
            </td>
            <td>
                <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password" Width="250" CssClass="TextBox "></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
         <tr>
            <td>
                &nbsp;
            </td>
            <td align="left">
                <%--<button id="Buttonlg" class="ui-button ui-button-text-only ui-widget ui-state-default ui-corner-all" type="button"  ><span class="ui-button-text">Login</span></button>
                <button id="ButtonForgetPassword" class="ui-button ui-button-text-only ui-widget ui-state-default ui-corner-all" type="button" ><span class="ui-button-text">Forget Password</span></button>--%>
                <asp:Button ID="ButtonLogin" OnClick="ButtonLogin_Click" runat="server" 
                    Text="Login" BorderColor="White" CssClass ="button" Height ="30px" Width ="87px"/>
                          
                           &nbsp;&nbsp;
               
                <asp:Button ID="ButtonForget"  runat="server" Text="Forgot Password" 
                            OnClientClick="window.open('ForgetPassword.aspx')" Width="160px" CssClass ="button" Height ="30px"/>
                   
                <br />
                <asp:CheckBox ID="chkRememberMe" runat="server" Text="Remember me next time." CssClass ="Label " Width ="200px" /> 
                <br>
                  <asp:Label ID="LabelStatus" runat="server" Text="" ForeColor="Red"></asp:Label>                     
            
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        
    </table>
    
</form>

</div>
<!-- Mask to cover the whole screen -->
<div style="width: 1478px; height: 602px; display: block; opacity: 0.8;" id="mask">

</div>
</div>
</body>
</html>