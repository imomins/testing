<%@ Page Language="C#" AutoEventWireup="true" CodeFile="createAssessment.aspx.cs" Inherits="pg_assessment_createAssessment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/global-style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <table >
         <tr>
            <td style="width:150px">
               Name of Assessment/Test: 
               
            </td>
            <td style="width:130px">
                <asp:TextBox ID="TextBoxAssessment" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td style="width:100px">
                *&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;           <asp:Button ID="ButtonCreate" runat="server" Text="Create"  onclick="ButtonCreate_Click" />
            </td>
            <td style="width:350px">
                <asp:Label ID="LabelMessage" runat="server" Text=""></asp:Label>
                   <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorAssessmentName" 
                    runat="server" ErrorMessage="Please Put an assessment name" 
                    ControlToValidate="TextBoxAssessment"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        
        <tr>
        
        <td colspan ="2">
        <h2>Assessment List</h2>
            <asp:GridView ID="gridAssessment" runat="server" AllowPaging="True" 
                AutoGenerateColumns="False" onpageindexchanging="gridAssessment_PageIndexChanging" 
                onselectedindexchanging="gridAssessment_SelectedIndexChanging" 
                Font-Names="Calibri" Font-Size="10pt" Width ="100%" 
                onrowdatabound="gridAssessment_RowDataBound" HorizontalAlign="Left">
                
                <RowStyle BackColor="#FFFFCC" />
                
                <Columns>
                
                <asp:BoundField DataField="AssessmentOID" HeaderText="ID" Visible ="false" 
                        ItemStyle-Width="0" HeaderStyle-BackColor="#9999FF" >
                <HeaderStyle BackColor="#9999FF"></HeaderStyle>
                <ItemStyle Width="0px"></ItemStyle>
                    </asp:BoundField>
                
                <asp:BoundField DataField="AssessmentOID" HeaderText="ID" Visible ="false" 
                        ItemStyle-Width="0" HeaderStyle-BackColor="#9999FF" >
                <HeaderStyle BackColor="#9999FF"></HeaderStyle>
                <ItemStyle Width="0px"></ItemStyle>
                    </asp:BoundField>
                    
                <asp:BoundField DataField="AssessmentName" HeaderText="Assessment Name" 
                        ItemStyle-Width="200" HeaderStyle-BackColor="#9999FF" >
                <HeaderStyle BackColor="#9999FF"></HeaderStyle>
                <ItemStyle Width="200px"></ItemStyle>
                    </asp:BoundField>
                    
                    <asp:BoundField DataField="LockStatus" HeaderText="Status" 
                        ItemStyle-Width="50" HeaderStyle-BackColor="#9999FF" >
                <HeaderStyle BackColor="#9999FF"></HeaderStyle>
                <ItemStyle Width="50px"></ItemStyle>
                    </asp:BoundField>
                    
                 <asp:TemplateField HeaderText="Actions" ItemStyle-Width="40" HeaderStyle-BackColor="#9999FF" HeaderStyle-Height="20">
                                <ItemTemplate>
                                    <asp:HiddenField ID="HiddenFieldAssessmentID" runat="server" Value='<%# Bind("AssessmentOID") %>' />                                                                    
                                    <asp:ImageButton ID="ImageButtonLock" runat="server" ImageUrl="~/images/change-password.png"
                                        OnClick="ImageButtonLock_Click" ToolTip="Lock This Assessment" OnClientClick="javascript:return confirm(&quot;Are you sure you want to Lock This assessment? Once You lock You will never unlock it..!&quot;);" Width="15" AlternateText="Lock" />
                                    
                                    <asp:ImageButton ID="ImageButtonDelete" runat="server"  ImageUrl="~/Images/delete-icon.png" OnClientClick="javascript:return confirm(&quot;Are you sure you want to delete?&quot;);"
                                        OnClick="ImageButtonDelete_Click" ToolTip="Delete this Assessment"  Width="15" AlternateText="Delete" />
                                </ItemTemplate>

                  <HeaderStyle BackColor="#9999FF" Height="20px"></HeaderStyle>

                  <ItemStyle Width="40px"></ItemStyle>
                                </asp:TemplateField>
                </Columns> 
                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:GridView>
            
        </td>
        
        
        </tr>
 
    </table>
    <asp:Label ID="lblStatus" runat="server" Font-Bold="True" Font-Names="Calibri" 
                Font-Size="12pt" ForeColor="#FF0066" ></asp:Label>
    </form>
</body>
</html>
