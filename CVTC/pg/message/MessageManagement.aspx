<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MessageManagement.aspx.cs" Inherits="pg_message_MessageManagement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Message Center</title>
    
    <script src="../../js/jquery.js" type="text/javascript"></script>
    <script src="../../js/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../js/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../js/jquery.ui.tabs.js" type="text/javascript"></script>
    <link href="../../themes/jquery.ui.tabs.css" rel="stylesheet" type="text/css" />
    <link href="../../themes/jquery.ui.all.css" rel="stylesheet" type="text/css" />
     <script src="../../js/thickbox.js" type="text/javascript"></script>
    <link href="../../themes/thickbox.css" rel="stylesheet" type="text/css" />
    
    <link rel="stylesheet" type="text/css" media="screen" href="../../themes/redmond/jquery-ui-1.8.1.custom.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../themes/ui.jqgrid.css" />


    <script src="../../js/jquery.js" type="text/javascript"></script>
 
    <script src="../../js/grid.locale-en.js" type="text/javascript"></script>
 
    <script src="../../js/jquery.jqGrid.min.js" type="text/javascript"></script>
     <%--<script src="../../js/thickbox.js" type="text/javascript"></script>
    <link href="../../themes/thickbox.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript" >
        function AfterClick() {
    //        alert("Hello");
        }
//         $(document).ready(function() {
//            $("#TB_closeWindowButton").live('click',function(event){
//              event.preventDefault();
//              alert('jewel');
//               
//            
//            });
//         });
                
        
    </script>
    <link href="../../css/global-style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/global-style-cvtc.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 1060px;">
        <tr>
            <td align="left" style=" padding:0px; margin:0px;" >
                <ul  style=" padding:10px; margin:0px;">
                <li > 
   <span class="ui-button ui-button-text-only ui-widget ui-state-default ui-corner-all ui-button-text"><a href="MessageManagement.aspx" style="font-weight:bold; padding:2px 10px 2px 10px; margin-right:5px; ">Inbox </a></span>
</li><li > 
   <span class="ui-button ui-button-text-only ui-widget ui-state-default ui-corner-all ui-button-text"><a href="MessageSent.aspx" style="font-weight:bold; padding:2px 10px 2px 10px; margin-right:5px;">Sent </a></span>
</li><li > 
   <span class="ui-button ui-button-text-only ui-widget ui-state-default ui-corner-all ui-button-text"><a href="MessageTrash.aspx" style="font-weight:bold; padding:2px 10px 2px 10px; margin-right:5px;margin-right:5px;">Trash </a></span>
</li><li > 
   <span class="ui-button ui-button-text-only ui-widget ui-state-default ui-corner-all ui-button-text"><a href="NewMessage.aspx?keepThis=true&TB_iframe=true&height=460&width=800" class="thickbox" onunload="AfterClick();" style="font-weight:bold; padding:2px 10px 2px 10px; margin-right:5px; ">Compose </a></span>
</li>
                               
                </ul>
                
                    
            </td>
            
        </tr>
         <tr>
            <td>
                &nbsp;
            </td>
            
        </tr>
       <tr><td><span class="ui-button ui-button-text-only ui-widget ui-state-default ui-corner-all ui-button-text">
        <asp:Button ID="ButtonRefresh" runat="server" Text="Refresh" 
            onclick="ButtonRefresh_Click" Font-Bold="True" /></span>
                      
                  </td></tr>
        <tr>
            <td>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <asp:Label id="lblProgress" runat="server" Text="Loading...." Width="200px" BackColor="#85B5D9" ForeColor="Maroon" Font-Bold="True" Style="padding: 5px"></asp:Label>
                </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                <div class="ContentHolder">
                  <div class="title-style-bar">
                    <h2> Message Inbox</h2>
                      &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="ButtonDelete" runat="server" Text="Delete" 
                          onclick="ButtonDelete_Click"  OnClientClick="javascript:return confirm(&quot;Are you sure you want to delete?&quot;);" />
                      <%--onChange="javascript:return confirm(&quot;Are you sure you want to change status?&quot;);"--%>
                      <asp:DropDownList ID="DropDownListMark" runat="server" Width="80px" AutoPostBack="true" 
                          onselectedindexchanged="DropDownListMark_SelectedIndexChanged">
                        <asp:ListItem>Mark</asp:ListItem>
                        <asp:ListItem>Read</asp:ListItem>
                        <asp:ListItem>Unread</asp:ListItem>
                        <asp:ListItem>Trashed</asp:ListItem>
                        <asp:ListItem>Star</asp:ListItem>
                        </asp:DropDownList>
                  </div>
                  <div class="title-style-bar-body skyblue">
                    
                   <asp:GridView ID="GridViewMessageBox" runat="server" 
                    AutoGenerateColumns="False" AllowPaging="True" 
                    onpageindexchanging="GridViewMessageBox_PageIndexChanging" CssClass="datagrid" 
                          Width="100%" >
                    <Columns>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:CheckBox ID="CheckBoxMessage" runat="server" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBoxMessage" runat="server" />
                                <asp:HiddenField ID="HiddenFieldMessage" runat="server" Value='<%# Bind("MessageUserOID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderImageUrl="~/images/flag_icon.png">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Image  ID="ImageFlg1" runat="server" ImageUrl='<%# Eval("FlagURL") %>'/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderImageUrl="~/images/StarIconGold.png">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>                            
                                <asp:Image ID="ImageFlg2" runat="server" ImageUrl='<%# Eval("MarkURL") %>'/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Subject">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Subject") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                            <script type="text/javascript">
                                $(document).ready(function() {
                                    //                                    var tmp = $("#insidegrid").attr("href");
                                    //                                    tmp = tmp.substring(0, 19) + "calEventId=" + 11 + "&" + tmp.substring(20, tmp.length);
                                    //                                    $('#insidegrid').attr("href", tmp);
                                    //                                    alert(tmp);
                                    //                                    tmp = "";
                                });
                                //var temp = '<%# Eval("MessageOID")%>';
                                //alert(temp);
                            </script>
                            
                            <a href='DisplayMessage.aspx?muoid=<%# Eval("MessageUserOID")%>&mid=<%# Eval("MessageOID")%>&keepThis=true&TB_iframe=true&height=460&width=800'
                            class="thickbox" id="insidegrid"><asp:Label ID="Label1" runat="server" Text='<%# Bind("Subject") %>' Font-Underline="True"></asp:Label></a>
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Recipient"  HeaderText="Recipient" 
                            HeaderStyle-Width="200" >
                        <HeaderStyle Width="200px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CreatedDate" HeaderText="Date" 
                            DataFormatString="{0:MM/dd/yyyy}" />
                        <asp:BoundField DataField="CreatedDate" DataFormatString="{0:hh:mm tt}" 
                            HeaderText="Time" />
                    </Columns>
                    <HeaderStyle BackColor="#85B5D9" />
                       <AlternatingRowStyle CssClass="altrow" />
                </asp:GridView> 
                    
                  </div>
                </div>
               
                </ContentTemplate>
                </asp:UpdatePanel>
                </td>
            
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            
        </tr>
        
    </table>
    </form>
</body>
</html>
