<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NoScore.aspx.cs" Inherits="pg_assessment_Section" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Section Edit</title>
    <link href="../../css/jquery-ui-1.8.1.custom.css" rel="stylesheet" type="text/css" />
    <script src="../../js/jquery.js" type="text/javascript"></script>
    <link href="../../css/global-style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var rowId = 1;
        var AddRow = 1;
        $(document).ready(function() {
            $("#ButtonAddMore").click(function() {
                // AddRow();
                rowId = parseInt($('#<%=HiddenRowId.ClientID %>').val());
                var newRowId = rowId + 1;
                var newRow = '<tr id="Row' + newRowId + '" ><td><input id="TextOrderNumber' + newRowId + '" name="TextOrderNumber' + newRowId + '" type="text" value="' + newRowId + '" class="orderNo" /></td><td><input id="TextQuestion"' + newRowId + ' name="TextQuestion' + newRowId + '" type="text" class="question" /></td><td style="Display:none"><select id="SelectResponseAction' + newRowId + '" name="SelectResponseAction' + newRowId + '" style ="width:120px" ><option>Radio Button</option><option>Drop Down</option></select></td>  <td><input id="TextResponse' + newRowId + '" name="TextResponse' + newRowId + '" type="text" class="response" /></td>           <td><input id="TextKeyword' + newRowId + '" name="TextKeyword' + newRowId + '" type="text" value="" class="keyword" /><td><button id="ButtonAdd" type="button" class="ui-button ui-button-text-only ui-widget ui-state-default ui-corner-all" ><span class="ui-button-text">Add Response</span></button></td></tr>';
                //$("#Row" + rowId).after(newRow);
                $("#Row_Ind").before(newRow);
                rowId = newRowId;
                $('#<%=TextBoxTotalQuestion.ClientID %>').val(newRowId)
                $('#<%=HiddenRowId.ClientID %>').val(newRowId)                
                
            });

            $("#ButtonAdd").live('click', function() {
                //AddSubRow();

                var oldRowId_t = ($(this).closest('tr').attr('id'));
                var oldRowId=oldRowId_t.substring(3,oldRowId_t.length);
                
                //alert(("#HiddenSubRow1").val());
                
//                alert(oldRowId_t);
                var nxtRowId=($(this).closest('tr').next('tr').attr('id'));
                var tmpRowId=nxtRowId;
//                nxtRowId
            
                //while(nxtRowId.length<5)nxtRowId.indexOf("Row") !== -1
                
                while(nxtRowId.indexOf("Row") == -1)
                {
                
                    tmpRowId=nxtRowId;                    
                    nxtRowId=($('#'+nxtRowId).next('tr').closest('tr').attr('id'));                
                    
                    
                }
                
                //alert(tmpRowId);
                //alert(nxtRowId);
                if(nxtRowId==tmpRowId)
                {                                
                    AddRow=2;                       
                }
                else
                {
                //alert(tmpRowId);
                    var splitResult=tmpRowId.split("_");                    
                    AddRow=parseInt(splitResult[1]);                    
                    AddRow+=1;
                }               
                
                //AddRow=1; 
                //alert(oldRowId + '_' + AddRow);
                var newRow = '<tr id="' + oldRowId + '_' + AddRow + '" >&nbsp;</td><td>&nbsp;</td>          <td> &nbsp;</td><td><input id="TextResponse' + oldRowId + '_' + AddRow + '" type="text" name="TextResponse' + oldRowId + '_' + AddRow + '" class="response"/></td>  <td>&nbsp;</td><td>&nbsp;</td> </tr>';
               // $("#" + oldRowId_t).after(newRow);
                $("#" + nxtRowId).before(newRow);
                
                
            });

        });
//    function AddRow1() {
//        var newRowId = rowId + 1;
//        var newRow = '<tr id="Row' + newRowId + '"  ><td><input id="TextOrderNumber' + newRowId + '" type="text" value="' + newRowId + '" /></td><td><input id="TextQuestion"' + newRowId + ' type="text" /></td><td><select id="SelectResponseAction' + newRowId + '" ><option>Check Box</option><option>Radio Button</option><option>Drop Down</option></select></td> <td> <input id="CheckboxMultiple' + newRowId + '" type="checkbox" /></td> <td><input id="TextResponse' + newRowId + '" type="text" /></td>           <td><select id="SelectFalgRating' + newRowId + '" > <option>0</option> <option>1</option><option>2</option> <option>3</option> <option>4</option> </select></td><td><input id="TextKeyword' + newRowId + '" type="text" /></td> <td><input id="ButtonAdd" type="button" value="+" /></td></tr>';

//        
//        $("#Row"+rowId).after(newRow);
//        rowId = newRowId;
//    }

    function AddSubRow() {
        var oldRowId = ($(this).closest('tr').attr('id'));
        
        var newRow = '<tr id="' + oldRowId + '_' + AddRow + '" >&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td> <td> &nbsp;</td> <td><input id="TextResponse' + oldRowId + '_' + AddRow + '" type="text" /></td>           <td><select id="SelectFalgRating' + oldRowId + '_' + AddRow + '" > <option>0</option> <option>1</option><option>2</option> <option>3</option> <option>4</option> </select></td><td>&nbsp;</td> <td>&nbsp;</td></tr>';

        
        $("#" + oldRowId).after(newRow);
    }
    </script>
    <style type="text/css">
        .style1
        {
            width: 180px;
        }
    </style>
    
    <style type="text/css">
    .orderNo{ width:30px;}
    .question{ width:200px;}
    .response{ width:100px;}
    .keyword{ width:100px;}
        
    </style>
    
    <script type="text/javascript">
     $(document).ready(function() {
          
            $("#BtnSaveMyWork").click(function() {            
                $("#<%=ButtonSaveMyWork.ClientID %>")[0].click();            
            });
      });
    </script>
    
     <script type ="text/javascript" >
         function DeleteQuestion(index) {
             if (!confirm('Are you sure you want to Delete?')) {
                 return;
             } else {
                 var LockeAss = document.getElementById('HiddenAssessmentLocked').value;
                 if (LockeAss == '1') {
                     alert('This question can not be deleted , because this assessment has been locked.');
                 }
                 else {
                     var OrderNo = index;
                     var secID = document.getElementById('HiddenSectionID').value;

                     $.ajax({
                         type: "POST",
                         url: "NoScore.aspx/DeleteQuestion",
                         data: "{OrderNo: '" + OrderNo + "', SectionID :'" + secID + "'}",
                         contentType: "application/json; charset=utf-8",
                         dataType: "json",
                         async: false,
                         cache: false,
                         success: function(result) {
                             alert('Successfully Deleted');
                             window.location.reload()
                         },
                         error: function() {
                             alert('Not Deleted');
                         }
                     });
                 }
             }

         }

    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <input id="HiddenRowId" type="hidden" runat="server" value="1"/>
     <input id="HiddenSectionID" type="hidden" runat="server"/>
    <input id="HiddenAssessmentLocked" type="hidden" runat="server"/>
    <table>
    <tr><td colspan="2"> <span class="ui-button ui-button-text-only ui-widget ui-state-default ui-corner-all ui-button-text">
        <asp:Button ID="ButtonRefresh" runat="server" Text="Refresh" 
            onclick="ButtonRefresh_Click" Font-Bold="True" /></span></td></tr>
    </table>
    <table>
        <tr><td > Total Question &nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBoxTotalQuestion" runat="server" Text="0"></asp:TextBox>
        <asp:Label ID="LabelMessage" runat="server" Text=""></asp:Label>
        </td></tr>
        <tr><td align="right">
            </td></tr>
        <tr><td>&nbsp;</td></tr>
        <tr><td>
        <div runat="server"  id="Div1"></div>
            <table style="width: 100%;">
            
      
       <tr>
       <td>
       <button id="BtnSaveMyWork" runat="server"  type="button" class="ui-button ui-button-text-only ui-widget ui-state-default ui-corner-all"><span class="ui-button-text">Save</span> </button> 
          <asp:Button ID="ButtonSaveMyWork" runat="server" Text="" 
               onclick="ButtonSaveMyWork_Click" Visible="true" Width="1px" ForeColor="White" BorderColor="White" BorderStyle="None" BackColor="White" />
       </td>
       <td>
       <button id="ButtonAddMore" class="ui-button ui-button-text-only ui-widget ui-state-default ui-corner-all" type="button" ><span class="ui-button-text">Add More</span></button>
       </td>
       </tr>
       <tr id="Row_Ind"><td colspan="8">&nbsp;</td></tr>
       <tr><td colspan="8" align="right"></td></tr>
       <%--<tr style="visibility:hidden"><td colspan="8" align="right"><input id="ButtonSave" type="button"  value="Save My Work" /></td></tr>--%>
       <tr><td colspan="8" align="right">
        </td> 
        </tr> 
          
    </table>
        </td></tr>
    </table>
    
    </form>
</body>
</html>
