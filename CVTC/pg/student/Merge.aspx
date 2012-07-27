<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Merge.aspx.cs" Inherits="pg_student_Merge" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <link rel="stylesheet" type="text/css" media="screen" href="../../themes/redmond/jquery-ui-1.8.1.custom.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../themes/ui.jqgrid.css" />

    <link href="../../css/global-style.css" rel="stylesheet" type="text/css" />
    <script src="../../js/jquery.js" type="text/javascript"></script> 
    <script src="../../js/grid.locale-en.js" type="text/javascript"></script> 
    <script src="../../js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="../../js/thickbox.js" type="text/javascript"></script>
    <link href="../../themes/thickbox.css" rel="stylesheet" type="text/css" />
    <title>Merge Student</title>
    
    <script type="text/javascript">
function openWin()
{
myWindow=window.open("","","width=200,height=100");
myWindow.document.write("<p>This is 'myWindow'</p>");
}

function closeWin()
{
myWindow.close();
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h2>Merging Student Information with Original Information</h2>
        <asp:GridView ID="gridMerge" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" PageSize="5" Width ="780px" 
            onpageindexchanging="gridMerge_PageIndexChanging">
             <Columns>
          
              <%-- <asp:BoundField DataField="StudentID" HeaderText="Banner ID"  />--%>
               
               
               <asp:TemplateField HeaderText="Banner ID">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("StudentID") %>'></asp:Label>
                                    <asp:HiddenField ID="HiddenFieldStudentID" runat="server" Value='<%# Bind("StudentOID") %>' />
                                </ItemTemplate>
                                
                            </asp:TemplateField>
               
              
              <asp:BoundField DataField="FullName" HeaderText="Student Name" />
              
               <asp:BoundField DataField="BirthDate" HeaderText="Birth Date" />
              
               <asp:BoundField DataField="ProgramEnrollment" HeaderText="Term" />
               
               <asp:BoundField DataField="MajorProgramEnrollment" HeaderText="Program Interest" />
               
               <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                <asp:Button ID="btnMerge" runat="server" Text="Merge" OnClick ="btnMerge_Click" OnClientClick="return confirm('Do you want to merge?After Merging Data Will be Deleted','Merging Student Information');"/>
                </ItemTemplate>
                </asp:TemplateField>
               </Columns>
        </asp:GridView>
        <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
        <br />
        <br />
         <a href="" onclick="javascript:window.close()" style="font-size: large; font-weight: bold;">Close</a>
&nbsp;</div>
    </form>
</body>
</html>
