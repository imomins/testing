<%@ Page Language="C#" AutoEventWireup="true" CodeFile="assessmentList.aspx.cs" Inherits="pg_answer_assessmentList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr><td>&nbsp;</td></tr>
        <tr><td>&nbsp;</td></tr>
        <tr><td>
            <asp:GridView ID="GridViewAssessment" runat="server" 
                AutoGenerateColumns="False" >
                
                <Columns>
                    <asp:BoundField DataField="AssessmentOID" HeaderText="AssessmentOID" 
                        Visible="False" />
                    <asp:BoundField DataField="AssessmentName" HeaderText="Assessment" />
                    <asp:BoundField DataField="TotalSection" HeaderText="Total Section" />
                    <asp:BoundField DataField="TotalQuestion" HeaderText="Total Question" />
                    <asp:BoundField DataField="TotalFlag" HeaderText="Flag Total" />
                    <asp:BoundField DataField="TotalFlagPoint" HeaderText="Flag Point Total" />
                    <asp:TemplateField>
                        <ItemTemplate>
                        
                            <asp:HyperLink ID="HyperLink1" runat="server" 
                                NavigateUrl='<%# Eval("AssessmentOID","~/pg/answer/QuestionSheet.aspx?aid={0}") %>' Text="exam"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#85B5D9" />
            </asp:GridView>
        </td></tr>
        <tr><td>&nbsp;</td></tr>
    </table>
    </form>
</body>
</html>
