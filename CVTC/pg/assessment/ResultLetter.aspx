<%@ Page Language="C#" Buffer="False" ValidateRequest="false" AutoEventWireup="true" CodeFile="ResultLetter.aspx.cs" Inherits="pg_assessment_ResultLetter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title></title>
 <style type="text/css">
 .alt-color { margin-top:2px; margin-bottom:2px; border-bottom:1px solid #999; width:100%;}
 .modonmc 
 {
    width:600px !important; 
 }
     
 </style>  
 
 <link href="../../css/global-style.css" rel="stylesheet" type="text/css" />
 <script type="text/javascript" src="../../js/jquery.js"></script>
 <script type ="text/javascript" src ="../../tinymce/jscripts/tiny_mce/plugins/paste/js/pastetext.js"></script>
  <script language="javascript" type="text/javascript" src="../../tinymce/jscripts/tiny_mce/tiny_mce.js"></script>      
        
        <script type ="text/javascript" >

            $(document).ready(function() {
            $("p:empty").remove();
            });
        </script>
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
                paste_text_use_dialog: true, //Added By Mominul
                //theme_advanced_resizing: false ,//Added By Mominul

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
                
                force_p_newlines : false ,//Added By Mominul
                force_br_newlines : false,//Added By Mominul
                remove_linebreaks : true ,//Added By Mominul
                convert_newlines_to_brs: false, //Added By Mominul

                force_p_newlines: false,
                force_br_newlines: true,
                forced_root_block: '',


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
      <asp:ScriptManager ID="ScriptManager1" runat="server" />
      
    <table style="width:100%">
    <tr>
    <td>
        <table style="width:100%">
            <tr>
                <td align="left">
                    <span class="ui-button ui-button-text-only ui-widget ui-state-default ui-corner-all ui-button-text">
                    <asp:Button ID="ButtonRefresh" runat="server" Text="Refresh" 
                        onclick="ButtonRefresh_Click" Font-Bold="True" Width ="100px" /></span>
                    <asp:Label ID="LabelMessage" runat="server" Text=""></asp:Label>
                        
                        <asp:HiddenField ID="HiddenFieldAID" runat="server" />
                </td>
                <td align="center" style="width:100%">
                
                <div  id="RiskDiv" style ="width:400px;position:relative;display:none; text-align:center;height:30px; z-index:5; position:relative;  background-color:Gray;">                     
                <b>Risk:</b><asp:DropDownList ID="ddlRisk" runat="server" Width ="150px"> </asp:DropDownList>
                   <br />
           
               </div>
                <br />
                     <div id="mygetDiv" style="display:none; text-align:center; width:400px; height:80px; z-index:5; position:relative;  background-color:Gray; " >
                      <br />
                      Print letters for users that have recieved this letter <input type="text" name="number_of_times" id="number_of_times" size="2" runat ="server" /> times.
                      <br /><br />
                      <input type="button" style="width:100px" value="Cancel" onclick="$('#mygetDiv').hide();" id="btnCancel" />
                      <%--<input type="button" style="width:100px" value="Ok" id="process_mydiv1" runat ="server" />--%>
                         <asp:Button ID="process_mydiv1" runat="server" Text="Ok" style="width:100px" OnClick ="process_mydiv1_Click" OnClientClick="aspnetForm.target ='_blank';"/>
                         

                      <br />
                    </div>
                    <asp:HyperLink ID="hplPrint" runat="server" Visible ="False" Target ="_blank" 
                        BackColor="#99CCFF" BorderStyle="None" Font-Bold="True" Font-Names="Calibri" 
                        Font-Size="Large" Height="20px" > </asp:HyperLink>
               </td>
                <%--<td align="right" style="width:40%">
                
                </td>--%>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                <td align="right">
                
                <a href="#null" onclick="getvalue();"><img alt="printer" src="../../images/Actions-document-print-icon.png" /></a>
                
                <asp:UpdateProgress DynamicLayout="false" ID="UpdateProgress1" runat="server">
                  <ProgressTemplate>
                      Loading ...
                  </ProgressTemplate>
                 </asp:UpdateProgress>
                 
                </td>
                </ContentTemplate>
                </asp:UpdatePanel>
            </tr>
        </table>

            </td>
            </tr>
    <tr>
    <td>
    <asp:TextBox ID="TextBoxHeader" runat="server" TextMode="MultiLine" Rows="5" Height="200" Width ="600" CssClass="modonmc" ></asp:TextBox>
    
    </td>
    </tr>
    <tr>
    <td >
        <asp:PlaceHolder ID="PlaceHolderSectionDefinition" runat="server"></asp:PlaceHolder>
        <br />
    </td>
    </tr>
    <tr>
    <td >
    <asp:TextBox ID="TextBoxShowAboveResult" runat="server" TextMode="MultiLine" Rows="5" Width="600" Height="200"></asp:TextBox>
    </td>
    </tr>
    <tr><td >
        <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" 
            onclick="ButtonSubmit_Click" />
        
        <div id="print_div1" runat="server" style="display:none;"></div>            
            </td></tr>
    </table>
    </form>
    
    
    

    
    
    
    <script type="text/javascript">
    function getvalue()
    {
      //alert('rgheirughiuer');
        $("#mygetDiv").show();
        $("#RiskDiv").show();
    }

    $(document).ready(function() {
   $("#btnCancel").click(function() {
   $("#mygetDiv").hide();
   $("#RiskDiv").hide();

    });
    
    
        $("#process_mydiv1").click(function() {
            //var to_process = $("#number_of_times").val();
            //var aid = $("#HiddenFieldAID").val();
            //var RiskName = $("#ddlRisk").val();
            //alert(RiskName);
            $('#mygetDiv').hide();
            $('#RiskDiv').hide();
            //url = 'PrintResultLetter.aspx?aid=' + aid + '&mypid=' + to_process + '&risk=' + RiskName;
            //url = 'PrintResultLetter.aspx?aid=' + aid + '&mypid=' + to_process;
           // $.get(url, function(data) {
                //alert(data);
               // $('#print_div1').html(data);
               // printContent();
                //return false;
            //});
        });
        

    });
    
    function printContent(){   
    str=document.getElementById('print_div1').innerHTML
    newwin=window.open('','printwin','left=100,top=100,width=850,height=900')
    newwin.document.write('<HTML>\n<HEAD>\n')
    newwin.document.write('<TITLE>Print Page</TITLE>\n')
    newwin.document.write('<script>\n')
    newwin.document.write('function chkstate(){\n')
    newwin.document.write('if(document.readyState=="complete"){\n')
    newwin.document.write('window.close()\n')
    newwin.document.write('}\n')
    newwin.document.write('else{\n')
    newwin.document.write('setTimeout("chkstate()",2000)\n')
    newwin.document.write('}\n')
    newwin.document.write('}\n')
    newwin.document.write('function print_win(){\n')
    newwin.document.write('window.print();\n')
    newwin.document.write('chkstate();\n')
    newwin.document.write('}\n')
    newwin.document.write('<\/script>\n')
    newwin.document.write('</HEAD>\n')
    newwin.document.write('<BODY onload="print_win()">\n')
    newwin.document.write(str)
    newwin.document.write('</BODY>\n')
    newwin.document.write('</HTML>\n')
    newwin.document.close()
    }



   

    </script>    
</body>
</html>
