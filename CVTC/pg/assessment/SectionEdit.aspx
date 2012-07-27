<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SectionEdit.aspx.cs" Inherits="pg_assessment_SectionEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Section Edit</title>
    <link href="../../css/jquery-ui-1.8.1.custom.css" rel="stylesheet" type="text/css" />
    <script src="../../js/jquery.js" type="text/javascript"></script>
    <link href="../../css/global-style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var rowId = 6;
        var AddRow = 1;
        var sub= 0 ;
        $(document).ready(function() {
            $("#ButtonAddMore").click(function() {
                // AddRow();
                var newRowId = rowId + 1;
                sub=sub + 5;
                var orders = newRowId - sub;
                //var newRow = '<tr id="Row' + newRowId + '" ><td><input id="TextOrderNumber' + newRowId + '" name="TextOrderNumber' + newRowId + '" type="text" value="' + newRowId + '" class="orderNo" /></td><td><input id="TextQuestion"' + newRowId + ' name="TextQuestion' + newRowId + '" type="text" class="question" /></td><td><select id="SelectResponseAction' + newRowId + '" name="SelectResponseAction' + newRowId + '" ><option>Radio Button</option><option>Drop Down</option></select></td>  <td><input id="TextResponse' + newRowId + '" name="TextResponse' + newRowId + '" type="text" class="response" /></td>           <td><select id="SelectFalgRating' + newRowId + '" name="SelectFalgRating' + newRowId + '" >  <option>1</option><option>2</option> <option>3</option> <option>4</option><option>5</option><option>6</option> </select></td><td><button id="ButtonAdd" type="button" class="ui-button ui-button-text-only ui-widget ui-state-default ui-corner-all" ><span class="ui-button-text">+</span></button></td></tr>';
                 var chkid='chkReverse'+newRowId;
                 var chkName='SelectFalgRating';
                 var newRow = '<tr id="Row' + newRowId + '" ><td><input id="TextOrderNumber' + newRowId + '" name="TextOrderNumber' + newRowId + '" readonly ="readonly" type="text" value="' + orders  + '" class="orderNo" /></td><td><input id="TextQuestion' + newRowId + '" name="TextQuestion' + newRowId + '" type="text" class="question" /></td><td style="display :none"><select id="SelectResponseAction' + newRowId + '" name="SelectResponseAction' + newRowId + '" visible ="false" style ="width:120px" ><option>Radio Button</option><option>Drop Down</option></select></td>  <td><input id="TextResponse' + newRowId + '" readonly ="readonly" name="TextResponse' + newRowId + '" type="text" class="response" value="Not true at all" /></td>           <td>      <input type ="text" id ="SelectFalgRating' + newRowId + '" name="SelectFalgRating' + newRowId + '" value ="1"  readonly ="readonly" />  </td><td> <input type ="checkbox" id="chkReverse' + newRowId + '" name ="chkReverse' + newRowId + '" onchange="ReverseValue(\'' + chkid + '\',\'' + chkName + '\',' + newRowId + ')" /></td></tr>';
                 newRowId = newRowId + 1;
                 newRow+= '<tr id="Row' + newRowId + '" ><td></td><td></td>  <td><input id="TextResponse' + newRowId + '" readonly ="readonly" name="TextResponse' + newRowId + '" type="text" class="response" value="Somewhat Untrue"/></td>           <td> <input type ="text" id ="SelectFalgRating' + newRowId + '" name="SelectFalgRating' + newRowId + '" value ="2"  readonly ="readonly" /></td><td></td></tr>';
                 newRowId = newRowId + 1;
                 newRow+= '<tr id="Row' + newRowId + '" ><td></td><td></td>  <td><input id="TextResponse' + newRowId + '" readonly ="readonly" name="TextResponse' + newRowId + '" type="text" class="response" value="Slightly Untrue"/></td>  <td> <input type ="text" id ="SelectFalgRating' + newRowId + '" name="SelectFalgRating' + newRowId + '" value ="3"  readonly ="readonly" /></td><td></td></tr>';
                 newRowId = newRowId + 1;
                 newRow+= '<tr id="Row' + newRowId + '" ><td></td><td></td>  <td><input id="TextResponse' + newRowId + '" readonly ="readonly" name="TextResponse' + newRowId + '" type="text" class="response" value="Slightly True"/></td>    <td> <input type ="text" id ="SelectFalgRating' + newRowId + '" name="SelectFalgRating' + newRowId + '" value ="4"  readonly ="readonly" /></td><td></td></tr>';
                 newRowId = newRowId + 1;
                 newRow+= '<tr id="Row' + newRowId + '" ><td></td><td></td>  <td><input id="TextResponse' + newRowId + '"  readonly ="readonly" name="TextResponse' + newRowId + '" type="text" class="response" value="Somewhat True "/></td>  <td> <input type ="text" id ="SelectFalgRating' + newRowId + '" name="SelectFalgRating' + newRowId + '" value ="5"  readonly ="readonly" /></td><td></td></tr>';
                 newRowId = newRowId + 1;
                 newRow+= '<tr id="Row' + newRowId + '" ><td></td><td></td>  <td><input id="TextResponse' + newRowId + '"  readonly ="readonly" name="TextResponse' + newRowId + '" type="text" class="response" value="Completely True"/></td>   <td> <input type ="text" id ="SelectFalgRating' + newRowId + '" name="SelectFalgRating' + newRowId + '" value ="6"  readonly ="readonly" /></td><td></td></tr>';
                //$("#Row" + rowId).after(newRow);
                $("#RInd").before(newRow);
                rowId = newRowId;
                $('#<%=TextBoxTotalQuestion.ClientID %>').val(orders)
                
            });

            $("#ButtonAdd").live('click', function() {
                //AddSubRow();

                var oldRowId_t = ($(this).closest('tr').attr('id'));
                var oldRowId=oldRowId_t.substring(3,oldRowId_t.length);
                //alert(oldRowId);
                var nxtRowId=($(this).closest('tr').next('tr').attr('id'));
                
                AddRow=1;                                
                if(nxtRowId.length>4)
                {
                
                    var splitResult=nxtRowId.split("_");                    
                    AddRow=parseInt(splitResult[1]);                    
                    AddRow+=1;
                }               
                
                var newRow = '<tr id="' + oldRowId_t + '_' + AddRow + '" >&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td> <td> &nbsp;</td><td><input id="TextResponse' + oldRowId + '_' + AddRow + '" type="text" name="TextResponse' + oldRowId + '_' + AddRow + '" class="response"/></td>           <td><select id="SelectFalgRating' + oldRowId + '_' + AddRow + '" name="SelectFalgRating' + oldRowId + '_' + AddRow + '">  <option>1</option><option>2</option> <option>3</option> <option>4</option><option>5</option><option>6</option> </select></td> <td>&nbsp;</td></tr>';
                $("#" + oldRowId_t).after(newRow);
                
            });

        });
    function AddRow1() {
        var newRowId = rowId + 1;
        //var newRow = '<tr id="Row' + newRowId + '"  ><td><input id="TextOrderNumber' + newRowId + '" type="text" value="' + newRowId + '" /></td><td><input id="TextQuestion"' + newRowId + ' type="text" /></td><td><select id="SelectResponseAction' + newRowId + '" ><option>Check Box</option><option>Radio Button</option><option>Drop Down</option></select></td> <td> <input id="CheckboxMultiple' + newRowId + '" type="checkbox" /></td> <td><input id="TextResponse' + newRowId + '" type="text" /></td>           <td><select id="SelectFalgRating' + newRowId + '" > <option>0</option> <option>1</option><option>2</option> <option>3</option> <option>4</option> </select></td><td><input id="TextKeyword' + newRowId + '" type="text" /></td> <td><input id="ButtonAdd" type="button" value="+" /></td></tr>';
        var newRow = '<tr id="Row' + newRowId + '"  ><td><input id="TextOrderNumber' + newRowId + '" type="text" value="' + newRowId + '" /></td><td><input id="TextQuestion"' + newRowId + ' type="text" /></td><td><select id="SelectResponseAction' + newRowId + '" ><option>Check Box</option><option>Radio Button</option><option>Drop Down</option></select></td> <td> <input id="CheckboxMultiple' + newRowId + '" type="checkbox" /></td> <td><input id="TextResponse' + newRowId + '" type="text" /></td>           <td><select id="SelectFalgRating' + newRowId + '" > <option>0</option> <option>1</option><option>2</option> <option>3</option> <option>4</option> </select></td><td><input id="TextKeyword' + newRowId + '" type="text" /></td> <td><input type ="checkbox" id="chkReverse' + newRowId + '" /></td></tr>';
            newRow+= '<tr id="Row' + newRowId+1 + '"  ><td><input id="TextOrderNumber' + newRowId+1 + '" type="text" value="' + newRowId+1 + '" /></td><td><input id="TextQuestion"' + newRowId+1 + ' type="text" /></td><td><select id="SelectResponseAction' + newRowId+1 + '" ><option>Check Box</option><option>Radio Button</option><option>Drop Down</option></select></td> <td> <input id="CheckboxMultiple' + newRowId+1 + '" type="checkbox" /></td> <td><input id="TextResponse' + newRowId+1 + '" type="text" /></td>           <td><select id="SelectFalgRating' + newRowId+1 + '" > <option>0</option> <option>1</option><option>2</option> <option>3</option> <option>4</option> </select></td><td><input id="TextKeyword' + newRowId+1 + '" type="text" /></td> <td><input type ="checkbox" id="chkReverse' + newRowId+1 + '" /></td></tr>';
        
        $("#Row"+rowId).after(newRow);
        rowId = newRowId;
    }

    function AddSubRow() {
        var oldRowId = ($(this).closest('tr').attr('id'));
        
        var newRow = '<tr id="' + oldRowId + '_' + AddRow + '" >&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td> <td> &nbsp;</td> <td><input id="TextResponse' + oldRowId + '_' + AddRow + '" type="text" /></td>           <td><select id="SelectFalgRating' + oldRowId + '_' + AddRow + '" > <option>0</option> <option>1</option><option>2</option> <option>3</option> <option>4</option> </select></td><td>&nbsp;</td> <td>&nbsp;</td></tr>';

        
        $("#" + oldRowId).after(newRow);
    }
    </script>
   <%-- <style type="text/css">
        .style1
        {
            width: 180px;
        }
    </style>--%>
    
    <style type="text/css">
    .orderNo{ width:30px;}
    .question{ width:153px;
        }
    .response{ width:119px;
        }
    .keyword{ width:100px;}
        
        .style1
        {
            height: 48px;
        }
        
    </style>
    
    <script type ="text/javascript" >
    
   function ReverseValue(checkBox,textboxidprefix,index){
   if (index<7)
   {
   
        if (document.getElementById(checkBox).checked) 
        {
   
            for (i=1;i<=6;i++)
            {
                elementid = textboxidprefix+i;
                document.getElementById(elementid).value = 7-i;           
            }
   
        }
        else 
        {
    
            for (i=1;i<=6;i++)
            {
                elementid = textboxidprefix+i;
                document.getElementById(elementid).value =i;           
            }  
        }   
    
    }
    else 
    {
    //alert (">6");
    
       if (document.getElementById(checkBox).checked) 
        {
       
            for (j=1;j<=6;j++)
            {
                var val = index + j -1;
                elementid = textboxidprefix+val;
                document.getElementById(elementid).value = 7-j;           
            }
       
        }
        else 
        {
    
            for (j=1;j<=6;j++)
            {
                var val = index + j -1;
                elementid = textboxidprefix+val;
                document.getElementById(elementid).value = j;           
            }  
        } 
    
    }
    
    }
    </script>
    
    
    <script type="text/javascript">
     $(document).ready(function() {
          
            $("#BtnSaveMyWork").click(function() {            
                $("#<%=ButtonSaveMyWork.ClientID %>")[0].click();            
            });
            $("#<%=TextBoxMedium.ClientID %>").change(function(){
            $("#Strong").text($('#<%=TextBoxMedium.ClientID %>').val()); 
            });
      });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr><td>
        <table>
        <tr><td>
        <table>
        <tr ><td class="style1">Total Questions: </td><td>
            <asp:TextBox ID="TextBoxTotalQuestion" runat="server">1</asp:TextBox>&nbsp;</td></tr>
        <tr ><td >Low = % < </td><td>
            <asp:TextBox ID="TextBoxLow" runat="server">0</asp:TextBox>&nbsp;</td></tr>
            <tr ><td >Medium = % < </td><td>
            <asp:TextBox ID="TextBoxMedium" runat="server">0</asp:TextBox>&nbsp;</td></tr>
        <tr ><td >Strong = % or > </td><td>
        <span id="Strong"></span></td></tr>
        <tr ><td >Flag if  < </td><td>
            <asp:TextBox ID="TextBoxFlag" runat="server">0</asp:TextBox>&nbsp;</td></tr>
        
        
        <tr><td>&nbsp;</td><td>&nbsp;</td></tr>
        <tr><td>Group Name</td><td>
            <asp:TextBox ID="TextBoxSectionName" runat="server" Width="162px"></asp:TextBox>
            &nbsp;
            <asp:RequiredFieldValidator
                ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please input a Group Name." 
                                            ControlToValidate="TextBoxSectionName"></asp:RequiredFieldValidator>
            
                                            </td></tr>
        </table>
        </td></tr>
        <tr><td></td></tr>
        </table>
        </td></tr>
        <tr><td>
            <table style="width: 100%;">
       <tr>
       <td  >Order #</td>
       <td>Enter Question</td>
       <td style ="display :none ">Response Action</td>
       
      <td>Responses </td>
       <td>Points</td>
       
       <td>Reverse</td>
       </tr>
       
         <tr id="Row1">
       <td ><input id="TextOrderNumber1" name="TextOrderNumber1" type="text" value="1" readonly ="readonly" class="orderNo" /></td>
       <td><input id="TextQuestion1" name="TextQuestion1"  type="text" class="question" /></td>
       <td style="display :none"><select id="SelectResponseAction1" name="SelectResponseAction1" visible ="false" style ="width:120px">               
               <option>Radio Button</option>
               <option>Drop Down</option>
           </select></td>
                  
           <td>
           <input id="TextResponse1" type="text"  name="TextResponse1"  class="response" 
                   value="Not true at all" readonly="readonly" />
           </td>
           <td>
           <input type ="text" id ="SelectFalgRating1" name ="SelectFalgRating1" value ="1"  readonly ="readonly" />
          <%-- <select id="SelectFalgRating1" name="SelectFalgRating1">               
               <option>1</option>
               
           </select>--%>
           
           </td>           
           <td>
             
              <input type ="checkbox" id="chkReverse1"  name ="chkReverse1" onchange="ReverseValue('chkReverse1','SelectFalgRating',1)"/>
               </td>
               <td>
                   
               </td>
       </tr>
       
         <tr id="Row2">
       <td ></td>
      <%-- <td></td>--%>
       <td>
       
       </td>
                  
           <td>
           <input id="TextResponse2" type="text"  name="TextResponse2" class="response" 
                   value="Somewhat Untrue" readonly="readonly"/>
           </td>
           <td>
            <input type ="text" id ="SelectFalgRating2" name ="SelectFalgRating2" value ="2"  readonly ="readonly" />
           <%--<select id="SelectFalgRating2" name="SelectFalgRating2">               
               <option>2</option>
               
           </select>--%>
           
           </td>           
           <td>
             
               </td>
       </tr>
       
         <tr id="Row3">
       <td ></td>
       <td></td>
       <%--<td></td>--%>
                  
           <td>
           <input id="TextResponse3" type="text" readonly ="readonly" name="TextResponse3" class="response" 
                   value="Slightly Untrue"/>
           </td>
           <td>
           <input type ="text" id ="SelectFalgRating3" name ="SelectFalgRating3" value ="3"  readonly ="readonly" />
           <%--<select id="SelectFalgRating3" name="SelectFalgRating3">               
               <option>3</option>
               
           </select>--%>
           
           </td>           
           <td>
             
               </td>
       </tr>
       
         <tr id="Row4">
       <td ></td>
       <td></td>
       <%--<td></td>--%>
                  
           <td>
           <input id="TextResponse4" type="text" readonly ="readonly" name="TextResponse4" class="response" 
                   value="Slightly True "/>
           </td>
           <td>
            <input type ="text" id ="SelectFalgRating4" name ="SelectFalgRating4" value ="4"  readonly ="readonly" />
           <%--<select id="SelectFalgRating4" name="SelectFalgRating4">               
               <option>4</option>
               
           </select>--%>
           
           </td>           
           <td>
             
               </td>
       </tr>
       
         <tr id="Row5">
       <td ></td>
       <td></td>
      <%-- <td></td>--%>
                  
           <td>
           <input id="TextResponse5" type="text" readonly ="readonly" name="TextResponse5" class="response" 
                   value="Somewhat True"/>
           </td>
           <td>
            <input type ="text" id ="SelectFalgRating5" value ="5" name ="SelectFalgRating5" readonly ="readonly" />
           <%--<select id="SelectFalgRating5" name="SelectFalgRating5">               
               <option>5</option>
               
           </select>--%>
           
           </td>           
           <td>
             
               </td>
       </tr>
       
         <tr id="Row6">
       <td ></td>
       <td></td>
       <%--<td></td>--%>
                  
           <td>
           <input id="TextResponse6" type="text" readonly ="readonly" name="TextResponse6" class="response" 
                   value="Completely True"/>
           </td>
           <td>
           <input type ="text" id ="SelectFalgRating6" value ="6" name ="SelectFalgRating6" readonly ="readonly" />
           <%--<select id="SelectFalgRating6" name="SelectFalgRating6">               
               <option>6</option>
               
           </select>--%>
           
           </td>           
           <td>
              
               </td>
       </tr>
       
       <tr id="RInd"><td colspan="9">
           
       <tr><td colspan="6" align="right"><button id="ButtonAddMore" class="ui-button ui-button-text-only ui-widget ui-state-default ui-corner-all" type="button" ><span class="ui-button-text">Add More</span></button></td></tr>
       <%--<tr style="visibility:hidden"><td colspan="8" align="right"><input id="ButtonSave" type="button"  value="Save My Work" /></td></tr>--%>
       <tr><td colspan="6" align="right">
        <asp:Label ID="LabelMessage" runat="server" Text=""></asp:Label>
          <button id="BtnSaveMyWork" runat="server"  type="button" class="ui-button ui-button-text-only ui-widget ui-state-default ui-corner-all"><span class="ui-button-text">Save</span> </button> 
          <asp:Button ID="ButtonSaveMyWork" runat="server" Text="" 
               onclick="ButtonSaveMyWork_Click" Visible="true" Width="1px" ForeColor="White" BorderColor="White" BorderStyle="None" BackColor="White" /></td></tr>
    </table>
        </td></tr>
    </table>
    
    </form>
</body>
</html>
