<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeFile="ReminderEmail.aspx.cs" Inherits="pg_assessment_ReminderEmail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">



</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../css/global-style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
 .alt-color { margin-top:2px; margin-bottom:2px; border-bottom:1px solid #999; width:100%;}
 </style>   
 <link href="../../css/global-style.css" rel="stylesheet" type="text/css" />
 <script type="text/javascript" src="../../js/jquery.js"></script>
 <script language="javascript" type="text/javascript" src="../../tinymce/jscripts/tiny_mce/tiny_mce.js"></script>      
        <script type="text/javascript">
            tinyMCE.init({
            // General options

                encoding: "xml",

                 //mode: "exact",
                mode: "textareas",
                theme: "advanced",
                //MyTextBox
                
                plugins: "autolink,lists,spellchecker,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template",

                // Theme options
                theme_advanced_buttons1: "save,newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,styleselect,formatselect,fontselect,fontsizeselect",
                theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor",
                theme_advanced_buttons3: "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media,advhr,|,print,|,ltr,rtl,|,fullscreen",
                theme_advanced_buttons4: "insertlayer,moveforward,movebackward,absolute,|,styleprops,spellchecker,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,template,blockquote,pagebreak,|,insertfile,insertimage",
                theme_advanced_toolbar_location: "top",
                theme_advanced_toolbar_align: "left",
                theme_advanced_statusbar_location: "bottom",
                theme_advanced_resizing: true,

                // Skin options
                skin: "o2k7",
                skin_variant: "silver",

                // Example content CSS (should be your site CSS)
                content_css: "css/example.css",

                // Drop lists for link/image/media/template dialogs
                template_external_list_url: "js/template_list.js",
                external_link_list_url: "js/link_list.js",
                external_image_list_url: "js/image_list.js",
                media_external_list_url: "js/media_list.js",

                // Replace values for the template plugin
                template_replace_values: {
                    username: "Some User",
                    staffid: "991234"
                }
            });
</script>
</head>
<body>
    <form id="form1" runat="server">
    <table  style="width:97%">
        <tr>
            <td >
                <%--<span class="ui-button ui-button-text-only ui-widget ui-state-default ui-corner-all ui-button-text"></span>--%>
                <table style="width:100%">
                    <tr>
                        <td colspan="2">
                        This email is automatically generated to all students imported from banner.<br />
                        You can choose to <i>not send</i> it to students with certain term codes. <br />
                        Select the term codes you would not like to send the email to. <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        <asp:CheckBoxList ID="CheckBoxListTermCode" runat="server" Width="400px"></asp:CheckBoxList>
                        
                        </td>
                        <td valign="top" align="right">
                        <asp:Button ID="ButtonSendEMails" runat="server" Text="Send EMails!"   
                                onclick="ButtonSendEMails_Click" Font-Bold="True" Width="120px" />
                        <br />
                        <asp:Label ID="LabelStatus" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>

            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="TextBoxEMail" runat="server" TextMode="MultiLine" Rows="5" Width="100" Height="250"></asp:TextBox>
                 
            </td>
        </tr>
        
    </table>
    <table>
 
     
    <tr><td>
        <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" 
            onclick="ButtonSubmit_Click" />
            <asp:Label ID="LabelMessage" runat="server" Text=""></asp:Label>
            </td></tr>
     
    </table>
    </form>
</body>
</html>
