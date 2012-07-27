<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="newTask.aspx.cs" Inherits="pg_task_newTask" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
 <style type="text/css">
 .alt-color { margin-top:2px; margin-bottom:2px; border-bottom:1px solid #999; width:100%;}

 </style>   
 <link href="../../css/global-style.css" rel="stylesheet" type="text/css" />
 <script type="text/javascript" src="../../js/jquery.js"></script>
<script type="text/javascript" src="../../js/jquery-ui-1.8.1.custom.min.js"></script>
<script type="text/javascript" src="../../js/jquery-ui-timepicker-addon.js"></script>
<link href="../../themes/jquery-ui-1.7.2.custom.css" rel='stylesheet' type='text/css' />
<link href="../../themes/jquery.ui.datepicker.css" rel='stylesheet' type='text/css' />
    
 <%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.3/jquery.min.js" type="text/javascript"></script>--%>
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
                //content_css: "css/example.css",

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
<script type="text/javascript">
    $(function() {
        $('#CompletedDate').datetimepicker({
            ampm: true,
            showTime: true,
            timeFormat: 'hh:mm TT'
            
        });
    });
    
</script>
    <form id="form1" runat="server">
    
  
    <asp:HiddenField ID="HiddenFieldCurrentUser" runat="server" />
    
    <table style="border-style: solid; border-width: 1px; width: 1060px; border-color:#70a8d2;">
        <tr>
            <td style="width:25%; border-right:1px solid #70a8d2; padding-right:6px;" valign="top" >
                <table style="width: 100%;">
                    <tr>
                        <td style=" font-size:11px;">
                          <h2 style=" font-size:14px; padding:0px; margin:0px 0px 4px 0px; color:#000;"> Contact List</h2>
                            Select your Users by clicking on the checkbox next to their email or name. To select all click the check box at top of list

                        </td>                        
                    </tr>
                    <tr>
                        <td  style=" font-size:12px; font-weight:bold; padding-bottom:5px;">
                        <p style="text-align:left;width:100%;">
                              <span style="font-weight:bold;"><img  src="../../images/magnifier-zoom-icon.png" /></span>
                               <input type="text" id="txtSearch" name="txtSearch" maxlength="50" style="width:160px; margin-left:10px;" value=""  />
                              
                                <img id="imgSearch" src="../../images/cancel.gif" alt="Cancel Search" 
                                  title="Cancel Search" style="width:14px;height:14px;" align="right" />
                          </p>
                                               <%-- Search : <asp:TextBox ID="TextBoxSearch"  runat="server" Width="60%"></asp:TextBox>  --%>
                        </td>                        
                    </tr>
                    <tr>
                        <td valign="top"  >
                        <div class="datagrid-label" style=" left:10px;
                                     top:150px; 
                                     width:200px;
                                     height:300px;
                                     
                                     overflow:auto; font-size:11px; font-family:Arial;">
                            <asp:CheckBoxList ID="CheckBoxListUser" runat="server" >
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
                        </td>
                        
                    </tr>
                    <tr>
                        <td align="left"colspan="2" valign="top">
                            <span style="color:Navy; font-weight:bold;" > Completion Date and Time:</span> 
                            <input id="CompletedDate" />
                            &nbsp;&nbsp;&nbsp;
                           <span style="color:Navy; font-weight:bold;" > Priority:</span><asp:DropDownList ID="DropDownListPriority" runat="server">
                            <asp:ListItem>1 </asp:ListItem>
                            <asp:ListItem>2 </asp:ListItem>
                            <asp:ListItem>3 </asp:ListItem>
                            <asp:ListItem>4 </asp:ListItem>
                            <asp:ListItem>5 </asp:ListItem>
                            <asp:ListItem>6 </asp:ListItem>
                            <asp:ListItem>7 </asp:ListItem>
                            <asp:ListItem>8 </asp:ListItem>
                            <asp:ListItem>9 </asp:ListItem>
                            <asp:ListItem>10 </asp:ListItem>
                            </asp:DropDownList>
                            
                            
                            
                                <span style="color:Navy; font-weight:bold;padding-left:10px"><img alt="Star" src="../../images/StarIconGold.png"/></span>&nbsp;&nbsp;&nbsp;<asp:DropDownList
                                    ID="DropDownListMark" runat="server" Width="80px">
                                <asp:ListItem>Mark</asp:ListItem>
                                <asp:ListItem Value="NotMark">Not Mark</asp:ListItem>
                                </asp:DropDownList>                            
                            
                        </td>
                        
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table style="width: 100%;" cellspacing="0" cellpadding="5" >
                                <tr>
                                    <td >
                                        <span style="color:Navy; font-weight:bold;" > To</span>
                                    </td>
                                    <td>
                                        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate >--%>
                                        <asp:TextBox ID="TextBoxTo" Width="250px" runat="server"></asp:TextBox>  
                                        <%--</ContentTemplate>
                                        <Triggers >
                                        <asp:AsyncPostBackTrigger  ControlID="CheckBoxListUser" EventName="SelectedIndexChanged"/> 
                                        </Triggers>
                                        </asp:UpdatePanel>--%>
                                        
                                        
                                        
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td >
                                        <span style="color:Navy; font-weight:bold;" > Title </span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxSubject" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td >
                                       <span style="color:Navy; font-weight:bold;" > Message </span>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>                                    
                                </tr>
                                <tr>
                                    <td colspan="2">
                                    <asp:TextBox ID="MyTextBox" runat="server" Rows="10" TextMode="MultiLine" Width="250px" Height="267px" ></asp:TextBox>                                        
                                        <%--<cc1:Editor ID="EditorMessageBody" runat="server"  Height="200" Width="500"/>--%>
                                    </td>
                                    
                                    
                                </tr>
                                <tr><td >
                                    <input id="Submit" type="button" value="Submit" style="width:80px"/>
                                    <%--<asp:Button ID="ButtonSubmit" runat="server" Text="Sumbit" Width="100px" 
                                         />--%>
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
  
    </form>
    <script type="text/javascript">
        var str = "";
        //$('#btnTest').click(function() {
        $('#<%=CheckBoxListUser.ClientID %> input:checkbox').change(function() {
            str = "";
            $('#<%=CheckBoxListUser.ClientID %> input[type=checkbox]:checked').each(function() {
                //alert($('label[for=' + this.id + ']').html());

                str += $('label[for=' + this.id + ']').html() + ', ';
                //alert(str);

            });
            str = str.substring(0, str.length - 2);
            $("#<%= TextBoxTo.ClientID %>").val(str);
        });

     
     
</script>

  <%--<script type="text/javascript">
      $(document).ready(function() {
          $('#Submit').click(function() {
              $('#AfterMessage').css({ 'display': 'block' }).fadeIn(1000);
              $('#Message').css({ 'display': 'none' });
          });

          $('#BtnCompose').click(function() {
              $('#AfterMessage').css({ 'display': 'none' });
              $('#Message').css({ 'display': 'block' }).fadeIn(1000);

              $('#<%=TextBoxTo.ClientID %>').val("");
              $('#<%=TextBoxSubject.ClientID %>').val("");

              $('#<%=TextBox1.ClientID %>').val("");
              $('#<%=TextBox2.ClientID %>').val("");
              $('#<%=TextBoxCompletedDate.ClientID %>').val("");

              $.ajax({
                  type: "POST",
                  url: "newTask.aspx/ServerSideMethod",
                  //data: "{}",
                  data: {},
                  contentType: "application/json; charset=utf-8",
                  dataType: "json",
                  async: true,
                  cache: false,
                  success: function(msg) {
                      alert(msg);
                  }

              })

              return false;

          });
      });
 </script>--%>
 
 <script type ="text/javascript">

     $(document).ready(function() {

         $('#Submit').click(function() {
             $('#AfterMessage').css({ 'display': 'block' }).fadeIn(1000);
             $('#Message').css({ 'display': 'none' });

             var str = $('#<%=TextBoxTo.ClientID %>').val();
             var t = "{ 'Recipient': '" + str + "', ";
             str = $('#<%=TextBoxSubject.ClientID %>').val();
             t += " 'Subject': '" + str + "', ";
             str = $("#DropDownListPriority option:selected").text();
             t += " 'priority': '" + str + "', ";
             str = $('#CompletedDate').val();
             t += " 'completedDate': '" + str + "', ";
             
             t += " 'status': 'Unread', ";
             str = $("#DropDownListMark option:selected").text();
             t += " 'mark': '" + str + "', ";

             str = $('#<%=HiddenFieldCurrentUser.ClientID %>').val();
             t += " 'from': '" + str + "', ";
             //str = $($("div[id*=EditorMessageBody]").find("iframe")[2].contentDocument).find("body").html();
             str = tinyMCE.get('MyTextBox').getContent();
             t += " 'BoyContent': '" + str + "'" + "}";
             // alert(str);
             $('#<%=CheckBoxListUser.ClientID %> input[type=checkbox]:checked').each(function() {
                 $('#<%=CheckBoxListUser.ClientID %> input[type=checkbox]:checked').attr('checked', false);
             });
             $.ajax({
                 type: "POST",
                 url: "newTask.aspx/InsertTask",//GetColumnNameList
                 //data: "{}",
                 data: t,
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 async: true,
                 cache: false,
                 success: function(msg) {
                     
                     //$('#myDiv').text(msg.d);

                 }

             })

             return false;

         });

     });

    

 </script>
 <script type="text/javascript">
     $(document).ready(function() {


         $('#BtnCompose').click(function() {
             $('#AfterMessage').css({ 'display': 'none' });
             $('#Message').css({ 'display': 'block' }).fadeIn(1000);
             $('#CompletedDate').val("");
             $('#<%=TextBoxTo.ClientID %>').val("");
             $('#<%=TextBoxSubject.ClientID %>').val("");
             //$($("div[id*=EditorMessageBody]").find("iframe")[2].contentDocument).find("body").text("");
             $("#DropDownListPriority option:selected").text("1");
             tinyMCE.get('MyTextBox').setContent("");
                
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
