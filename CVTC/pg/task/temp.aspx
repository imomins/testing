<%@ Page Language="C#" AutoEventWireup="true" CodeFile="temp.aspx.cs" Inherits="pg_message_temp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.3/jquery.min.js" type="text/javascript"></script>--%>
<script type="text/javascript" src="../../js/jquery.js"></script>
    <script type ="text/javascript">

        $(document).ready(function() {

        $('#Button1').click(function() {
            
                var str = $('#<%=TextBox1.ClientID %>').val();
                var t = "{ 'param1': '" + str + "' }";
                // alert(str);
                $.ajax({
                    type: "POST",
                    url: "temp.aspx/ServerSideMethod",
                    //data: "{}",
                    data: t,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function(msg) {
                    $('#myDiv').text(msg.d);
                    $('#<%=TextBox1.ClientID %>').val("");
                    }

                })

                return false;

            });

        });

   </script>
    
</head>
<body>
    <form id="form1" runat="server">
       <%-- <asp:Button ID="Button1" runat="server" Text="Click" />--%><input id="Button1" type="button" value="button" />
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>

    <br /><br />

    <div id="myDiv"></div>
    </form>
</body>
</html>
