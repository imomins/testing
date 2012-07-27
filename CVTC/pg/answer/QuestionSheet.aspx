<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuestionSheet.aspx.cs" Inherits="pg_answer_QuestionSheet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Assessment Question Page</title>

    <script src="../../js/jquery.js" type="text/javascript"></script>

    <style type="text/css">
        .table_border tr td
        {
            border: 1px solid #999;
        }
        .answer_table
        {
            border: 1px solid #d7d7d7;
        }
        .answer_table tr td
        {
            border: 1px solid #d7d7d7;
        }
        .answer
        {
            transform: rotate(90deg);
            -ms-transform: rotate(90deg); /* Internet Explorer */
            -moz-transform: rotate(90deg); /* Firefox */
            -webkit-transform: rotate(90deg); /* Safari and Chrome */
            -o-transform: rotate(90deg); /* Opera */
            width: 10px;
            color: #333;
            border: 0px solid red;
            white-space: nowrap;
            display: block;
            margin: 0px;
            padding: 0px;
            margin-bottom: 100px;
            margin-left: 0px;
            float: right;
            position: relative;
            text-align: center;
            vertical-align: top;
            padding-right: 8px;
            clear: both;
        }
        .qustion_title_bg
        {
            background: #d7d7d7;
            padding: 5px;
            margin: 5px;
            width: 350px;
        }
        .qustion_title_bg_alt
        {
            background: #fff;
            padding: 5px;
            margin: 5px;
            width: 350px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%; padding-left: 5px;">
        <%--   <tr style="background:#85B5D9"><td>
        <asp:Label ID="LabelAssessment" runat="server" Text="Label"></asp:Label></td></tr>       
       <tr><td>&nbsp;</td></tr>--%>
        <tr>
            <td>
                <table width="100%" style="border: 1px solid #999999;" class="table_border" cellpadding="8"
                    cellspacing="0">
                    <tr>
                        <td width="40%" colspan="2" style="background: #999999;" class="style1">
                            <span><b>Assessment Name:</b></span>
                            <asp:Label ID="lblAssessmentName" runat="server" Style="color: #fff;"></asp:Label>
                            <%--<span >CVTC INVENTORY OF STUDENT SUCCESS</span>--%>
                        </td>
                        <td width="25%" align="center" style="background: #d7d7d7;" class="style2">
                            <b>First Name</b>
                        </td>
                        <td width="25%" align="center" style="background: #d7d7d7;" class="style2">
                            <b>Last Name</b>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="background: #d7d7d7;">
                            <b>StudentID :</b>
                        </td>
                        <td>
                            <asp:Label ID="lblStudentID" runat="server" Style="font-weight: 700"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Label ID="lblFirstName" runat="server" Style="font-weight: 700"></asp:Label>
                        </td>
                        <td width="25%" align="center">
                            <asp:Label ID="lblLastName" runat="server" Style="font-weight: 700"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="background: #d7d7d7;">
                            <b>Program Applying For:</b>
                        </td>
                        <td>
                            <asp:Label ID="lblProgram" runat="server" Style="font-weight: 700"></asp:Label>
                        </td>
                        <td align="right" style="background: #d7d7d7;">
                            <b>Birth Date :</b>
                        </td>
                        <td width="25%">
                            <asp:Label ID="lblBirthDate" runat="server" Style="font-weight: 700"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="Label1" runat="server"></asp:Label>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <div id="Title">
                    These questions open an important communication channel between you and CVTC, and
                    reflect your thoughts and feelings on many issues related to college. Results as
                    a whole will be used to plan campus-wide programs of support service, and a comparative
                    report of your individual responses will be provided to you.
                </div>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <asp:PlaceHolder ID="PlaceHolderMain" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divSoreTable" runat="server">
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divNonScoreTable" runat="server">
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <input type="button" id="ButtonSubmit" value="Submit" onclick="nonscore()" />&nbsp;
                <asp:Label ID="lblValidation" runat="server" ForeColor="#FF3300"></asp:Label>
                <asp:Button ID="Button1submit" runat="server" Text="Submits" OnClick="ButtonSubmit_Click"
                    ToolTip="Submit" Visible="true" />&nbsp;
            </td>
        </tr>
    </table>
    </form>



    <script type="text/javascript">
        $("#Button1submit").css('display', 'none');
        function nonscore() {
            var iz_checked1 = false;
            var classes = $("[class^='Groupnonscorequestion_']");
            for (var j = 1; j <= classes.length; j++) {

                var allInputs = $('.Groupnonscorequestion_' + j).find(":input");
                var iz_checked = false;

                for (var i = 1; i <= allInputs.length; i++) {
                    if ($('.nonscorequestions_' + j).is(':checked')) {
                        iz_checked = true;
                    }
                }
                iz_checked1 = iz_checked;
                //alert(iz_checked1);
                if (!iz_checked)
                    break;
            }
            if (iz_checked1) {
                $("#Button1submit").click();
            }
            else {
                alert('Please Answer All Questions....');
            }
        }


        
        

        
        
   
    
    </script>

</body>
</html>
