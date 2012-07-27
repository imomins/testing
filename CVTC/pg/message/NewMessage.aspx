<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeFile="NewMessage.aspx.cs" Inherits="pg_message_NewMessage" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
               theme_advanced_buttons1: "cut,copy,paste,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull",
                theme_advanced_buttons2: "bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,|,forecolor,backcolor",
                theme_advanced_buttons3: "justifyleft,justifycenter,justifyright,justifyfull,|,styleselect,formatselect,fontselect,fontsizeselect",
               // theme_advanced_buttons3: "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media,advhr,|,print,|,ltr,rtl,|,fullscreen",
                //theme_advanced_buttons4: "insertlayer,moveforward,movebackward,absolute,|,styleprops,spellchecker,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,template,blockquote,pagebreak,|,insertfile,insertimage",
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
    
  
    
    <table style="border-style: solid; border-width: 1px; width: 1060px; border-color:#d7d7d7;">
        <tr>
            <td style="width:25%; border-right:1px solid #d7d7d7; padding-right:6px;" valign="top" >
                <table style="width: 100%;">
                    <tr>
                        <td style=" font-size:11px;">
                          <h2 style=" font-size:14px; padding:0px; margin:0px 0px 4px 0px; color:#000;"> Contact List</h2>
                            Select your Users by clicking on the checkbox next to their email or name. To select all click the check box at top of list

                        </td>                        
                    </tr>
                    <tr>
                        <td  style=" font-size:12px; font-weight:bold; padding-bottom:5px;">
                        <%--    Search : <asp:TextBox ID="TextBox3"  runat="server" Width="60%"></asp:TextBox>  --%>
                        <p style="text-align:left;width:100%;">
                               <span style="font-weight:bold;"><img  src="../../images/magnifier-zoom-icon.png" /></span> <input type="text" id="txtSearch" name="txtSearch" maxlength="50" style="width:160px; margin-left:10px;" value=""  />
                              
                                <img id="imgSearch" src="../../images/cancel.gif" alt="Cancel Search" 
                                  title="Cancel Search" style="width:14px;height:14px;" align="right" />
                          </p>
                        </td>                        
                    </tr>
                    <tr>
                        <td valign="top">
                        <div class="datagrid-label" style=" left:10px;
                                     top:150px; 
                                     width:200px;
                                     height:300px;
                                     overflow:auto; font-size:11px; font-family:Arial;">
                            <asp:CheckBoxList ID="CheckBoxListUser" runat="server" CssClass="alt-color">
                                                        </asp:CheckBoxList>
                            
                            </div>
                        </td>                        
                    </tr>
                    
                </table>
            </td>
            <td style="width:75%; padding-left:6px;" valign="top" >
            
            <div id="Message">
                <table style=" width: 100%; font-size:12px; font-family:Arial;">
                    <tr>
                        <td>
                            <span style="color:Navy; font-weight:bold;" >Current Date :</span> &nbsp;<asp:Label ID="LabelDate" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                            <span style="color:Navy; font-weight:bold;">Current Time :</span>&nbsp;<asp:Label ID="LabelTime" runat="server" Text=""></asp:Label>
                            <span style="color:Navy; font-weight:bold;padding-left:10px"><img alt="Star" src="../../images/StarIconGold.png"/></span>&nbsp;&nbsp;&nbsp;<asp:DropDownList
                                    ID="DropDownListMark" runat="server" Width="80px">
                                <asp:ListItem>Mark</asp:ListItem>
                                <asp:ListItem Value="NotMark">Not Mark</asp:ListItem>
                                </asp:DropDownList>
                        </td>
                        
                    </tr>
                   <tr><td>&nbsp;</td></tr>
                    <tr>
                        <td >
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <span style="color:Navy; font-weight:bold;" > To</span>
                                    </td>
                                    <td>
                                        
                                        <asp:TextBox ID="TextBoxTo" Width="200px" runat="server"></asp:TextBox>  
                                        <input id="HiddenTo" type="hidden" />
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td>
                                        <span style="color:Navy; font-weight:bold;" > Subject </span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxSubject" runat="server" Width="400px"></asp:TextBox>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td>
                                       <span style="color:Navy; font-weight:bold;" > Message </span>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>                                    
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:TextBox ID="MyTextBox" runat="server" Rows="10" TextMode="MultiLine" Width="557px" Height="267px" ></asp:TextBox>                                        
                                         <%--<cc1:Editor ID="EditorMessageBody" runat="server"  Height="200" Width="500"/>--%>
                                    </td>
                                    
                                    
                                </tr>
                                <tr><td colspan="2">
                                    <input id="Submit" type="button" value="Sumbit" style="width:80px"/>&nbsp;&nbsp;&nbsp;
                                    <%--<asp:Button ID="ButtonSubmit" runat="server" Text="Sumbit" Width="100px" 
                                         />--%>
                                         <%--<asp:Button ID="ButtonRefresh" runat="server" Text="Refresh" Width="80px"                                          />--%>
                                    </td></tr>
                            </table>
                        </td>
                        
                    </tr>
                </table> 
             </div>
             <div id="AfterMessage" style=" display: None; " > 
                 <table style="width: 100%;" valign="top">
                     <tr>
                         <td>
                              <span style="color:Navy; font-weight:bold;" >Current Date :</span> &nbsp;<asp:Label ID="Label1" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                            <span style="color:Navy; font-weight:bold;">Current Time :</span>&nbsp;<asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                            
                         </td><td align="right">
                             <input id="BtnCompose" type="button" value="Compose New " /></td>
                         
                     </tr>
                     <tr><td>&nbsp;</td></tr>
                     <tr><td>&nbsp;</td></tr>
                     <tr><td>&nbsp;</td></tr>
                     <tr><td>&nbsp;</td></tr>
                     <tr><td>&nbsp;</td></tr>
                     <tr>
                     <td>Your Message has been sent Successfully  </td>
                     </tr>
                     <tr>
                     <td>Click the Close in the top right corner to close this page  </td>
                     </tr>
                     <tr>
                     <td>Or </td>
                     </tr>
                     <tr>
                     <td>Click the new compose button to create a new message </td>
                     </tr>
                     
                 </table>
             </div>
            </td>
            
        </tr>
        
    </table>
  
    <asp:HiddenField ID="HiddenFieldCurrentUser" runat="server" />
    </form>
    <script type="text/javascript">
        var str = "";
        var id = "";
        //$('#btnTest').click(function() {
        $('#<%=CheckBoxListUser.ClientID %> input:checkbox').change(function() {
            str = "";
            $('#<%=CheckBoxListUser.ClientID %> input[type=checkbox]:checked').each(function() {
                //alert($('label[for=' + this.id + ']').html());

                str += $('label[for=' + this.id + ']').html() + ', ';
                //alert($(this).next('label').text());
                

            });
            str = str.substring(0, str.length - 2);
            
            $("#<%= TextBoxTo.ClientID %>").val(str);
           
        });

     
     
</script>

  <script type="text/javascript">
      $(document).ready(function() {
         

          $('#BtnCompose').click(function() {
              $('#AfterMessage').css({ 'display': 'none' });
              $('#Message').css({ 'display': 'block' }).fadeIn(1000);

              $('#<%=TextBoxTo.ClientID %>').val("");
              $('#<%=TextBoxSubject.ClientID %>').val("");
              //$($("div[id*=EditorMessageBody]").find("iframe")[2].contentDocument).find("body").text("");
              $('#<%=MyTextBox.ClientID %>').val("")
              $("#DropDownListMark option:selected").text("Mark");
              tinyMCE.get('MyTextBox').setContent("");

          });
      });
 </script>
 
 <script type="text/javascript">
     $(document).ready(function() {

         
     });
  </script>
  
   
   <script type ="text/javascript">

       $(document).ready(function() {

           $('#Submit').click(function() {
               $('#AfterMessage').css({ 'display': 'block' }).fadeIn(1000);
               $('#Message').css({ 'display': 'none' });

               var str = $('#<%=TextBoxTo.ClientID %>').val();
               var t = "{ 'Recipient': '" + str + "', ";
               str = $('#<%=TextBoxSubject.ClientID %>').val();
               t += " 'Subject': '" + str + "', ";
               str = $("#DropDownListMark option:selected").text();
               t += " 'Mark': '" + str + "', ";
               str = $('#<%=HiddenFieldCurrentUser.ClientID %>').val();
               t += " 'from': '" + str + "', ";

               str = tinyMCE.get('MyTextBox').getContent();
               //alert(str);
               //alert($('#MyTextBox').val("sdsdsdsd"));
               //str = $($("div[id*=EditorMessageBody]").find("iframe")[2].contentDocument).find("body").html();
               t += " 'BoyContent': '" + str + "'" + "}";
               

               $('#<%=CheckBoxListUser.ClientID %> input[type=checkbox]:checked').each(function() {

                   $('#<%=CheckBoxListUser.ClientID %> input[type=checkbox]:checked').attr('checked', false);


               });
               $.ajax({
                   type: "POST",
                   url: "NewMessage.aspx/InsertMessage",
                   //data: "{}",
                   data: t,
                   contentType: "application/json; charset=utf-8",
                   dataType: "json",
                   async: true,
                   cache: false,
                   success: function(msg) {
                       //alert(msg.d);
                       //$('#myDiv').text(msg.d);

                   }

               })

               return false;

           });

       });

   </script>
   
   <script language="javascript" type="text/javascript">

          jQuery.expr[":"].containsNoCase = function(el, i, m) {

              var search = m[3];

           
              if (!search) return false;

              return eval("/" + search + "/i").test($(el).text());

          };

   

          jQuery(document).ready(function() {

              // used for the first example in the blog post

             //jQuery('li:contains(\'DotNetNuke\')').css('color', '#0000ff').css('font-weight', 'bold');

   

              // hide the cancel search image

              jQuery('#imgSearch').hide();

   

              // reset the search when the cancel image is clicked

              jQuery('#imgSearch').click(function() {

                  resetSearch();

              });

   

              // cancel the search if the user presses the ESC key

              jQuery('#txtSearch').keyup(function(event) {

                  if (event.keyCode == 27) {

                      resetSearch();

                  }

              });

   

              // execute the search

              jQuery('#txtSearch').keyup(function() {

                  // only search when there are 3 or more characters in the textbox

                  if (jQuery('#txtSearch').val().length > 0) {

                      // hide all rows

                      jQuery('#CheckBoxListUser tr').hide();

                      // show the header row

                      //jQuery('#CheckBoxListUser tr:first').show();

                      // show the matching rows (using the containsNoCase from Rick Strahl)

                      jQuery('#CheckBoxListUser tr td:containsNoCase(\'' + jQuery('#txtSearch').val() + '\')').parent().show();

                      // show the cancel search image

                      jQuery('#imgSearch').show();

                  }

                  else if (jQuery('#txtSearch').val().length == 0) {

                      // if the user removed all of the text, reset the search
                    
                      resetSearch();

                  }

   

                  // if there were no matching rows, tell the user

                  if (jQuery('#CheckBoxListUser tr:visible').length == 0) {

                      // remove the norecords row if it already exists

                      jQuery('.norecords').remove();

                 // add the norecords row

                     jQuery('#CheckBoxListUser').append('<tr class="norecords"><td colspan="5" class="Normal">No records were found</td></tr>');

                 }

             });

         });

  

         function resetSearch() {

             // clear the textbox

             jQuery('#txtSearch').val('');

             // show all table rows

             jQuery('#CheckBoxListUser tr').show();

             // remove any no records rows

             jQuery('.norecords').remove();

             // remove the cancel search image

             jQuery('#imgSearch').hide();

             // make sure we re-focus on the textbox for usability

             jQuery('#txtSearch').focus();

         }

     

</script>
</body>
</html>
