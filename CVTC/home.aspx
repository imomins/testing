<%@ Page Language="C#" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="home" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home </title>
    <link rel="stylesheet" type="text/css" media="screen" href="themes/redmond/jquery-ui-1.8.1.custom.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="themes/ui.jqgrid.css" />
    <link href="css/global-style.css" rel="stylesheet" type="text/css" />     
    <link href="themes/thickbox.css" rel="stylesheet" type="text/css" />
    
    <script src="js/jquery-1.4.2.js" type="text/javascript"></script>
    <script src="js/jquery.accordion.js" type="text/javascript"></script>
    <script src="js/grid.locale-en.js" type="text/javascript"></script> 
    <script src="js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="js/thickbox.js" type="text/javascript"></script>
    
      <script type="text/javascript">
        $(function() {
            $("#TaskManagerGrid").jqGrid({
                url: 'TaskHandler.ashx?toid='+$('#<%=HiddenFieldCurrentUser.ClientID %>').val(),
                datatype: 'json',
                height: 250,
                width: 540,
                //autowidth:true ,
                ShrinkToFit :true,
                colNames: ['TaskOID','Completed Date', 'Create date', 'Assign By', 'Task', 'Priority'],
                colModel: [
                        { name: 'TaskOID', index: 'TaskOID', hidden:true,sortable: false },
                        { name: 'CompletedBy', width:80, editable: true },
                        { name: 'CreatedDate', width:100, editable: true },
                        { name: 'AssignBy',width:100,  editable: true,   hidden: true },
                        { name: 'Task', width:120,  sortable: true, editable: true },
                        { name: 'Priority', width:80, sortable: true, editable: true }                        
                        
                    ],
                rowNum: 10,
                rowList: [10, 20, 30],
                pager: '#TaskManagerGridPager',
                sortname: 'UserOID',
                viewrecords: true,
                sortorder: 'asc',
                altRows: true,
                //editurl: 'UserHandler.ashx',
                caption: 'Task Manager ',
                ondblClickRow:function(id){ 
                
                  var tmp = $("#Displayid").attr("href");
                  
                  tmp = tmp.substring(0, 25) + "taskOID=" + id + "&" + tmp.substring(26, tmp.length);                
                  
                  $("#Displayid").attr("href", tmp);                
                  $("#Displayid").click();
               }
            });

            $("#TaskManagerGrid").jqGrid('navGrid', '#TaskManagerGridPager', { edit: false, add: false, del: false  });

        });




        

    </script>
    
      <script type="text/javascript">
        $(function() {
        
           // $("#InterventionGrid").fluidGrid();
        
            $("#InterventionGrid").jqGrid({
                url: 'Intervention.ashx?ioid='+$('#<%=HiddenFieldCurrentUser.ClientID %>').val(),
                datatype: 'json',
                height: 250,
                width:485,
                //autowidth:true ,
                ShrinkToFit : true,
                colNames: ['InterventionOID', 'Action Date', 'Contact Date', 'Student', 'Comment'],
                colModel: [
                        { name: 'InterventionOID', index: 'InterventionOID', hidden:true,sortable: false },
                        { name: 'LatestActionDate', width: 100, editable: true },
                        { name: 'LatestContact', width: 100, sortable: true, editable: true },
                        { name: 'BannerStudentIDNumber', width: 100, sortable: true, editable: true },
                        { name: 'Comment',width:100,  sortable: true, editable: true }                        
                
                    ],
                rowNum: 10,
                rowList: [10, 20, 30],
                pager: '#InterventionGridPager',
                sortname: 'LatestActionDate',
                viewrecords: true,
                sortable: true,
                sortorder: 'desc',
                altRows: true,
                //editurl: 'UserHandler.ashx',
                caption: 'Intervention Measures '
                
            });

            $("#InterventionGrid").jqGrid('navGrid', '#InterventionGridPager', { edit: false, add: false, del: false  });

                 
             


        });



        

    </script>

    <link href="css/global-style-cvtc.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        .style1
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: xx-small;
            color :White ;
        }
    </style>
    
    </head>
<body>

    <form id="form1" runat="server" >
    <asp:HiddenField ID="HiddenFieldCurrentUser" runat="server" />
    <a style="display:none;" id="tid" href="pg/task/newTask.aspx?keepThis=true&TB_iframe=true&height=480&width=800" class="thickbox" title="Create New Task">No-scroll content</a>  
    <a style="display:none;" id="mid" href="pg/message/NewMessage.aspx?keepThis=true&TB_iframe=true&height=500&width=800" class="thickbox" title="Create New Message">No-scroll content</a>  
    <a style="display:none;" id="Displayid"  href="pg/task/DisplayTask.aspx?keepThis=true&TB_iframe=true&height=460&width=600" class="thickbox"  >Dislay</a>
   
   <table cellpadding="0" cellspacing="0" width="1080">
   <tr>
   <td><table width="1080">
    <tr>
    <td>
        <asp:Button ID="ButtonRefresh" runat="server" Text="Refresh" 
            onclick="ButtonRefresh_Click" /></td>
     <td colspan="1" align="right"><asp:Label ID="LabelUserName" runat="server" Text=""></asp:Label>
     </td>
     </tr>
    <tr>
    <td colspan="2"> 
        
    <table width="96%" border="0" cellspacing="0" cellpadding="0" class="calender-grid">
                      <tr>
                        <th height="20" bgcolor="#70a8d2">Mon</th>
                        <th height="20" bgcolor="#70a8d2">Tue</th>
                        <th height="20" bgcolor="#70a8d2">Wed</th>
                        <th height="20" bgcolor="#70a8d2">Thr</th>
                        <th height="20" bgcolor="#70a8d2">Fri</th>
                        <th height="20" bgcolor="#70a8d2">Sat</th>
                        <th height="20" bgcolor="#70a8d2">Sun</th>
                      </tr>
                      <tr>
                        <td  width="100" height="100" valign="top" id="lbl1" runat="server">
                        <div>
                        <strong>
                            <asp:Label ID="Label1" runat="server" Text="1"></asp:Label></strong>
                            </div>
                            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                            </td>
                        <td  width="100" valign="top" id="lbl2" runat="server"><div><strong><asp:Label ID="Label2" runat="server" Text="2"></asp:Label></strong></div>
                            <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                        </td>
                        <td width="100" valign="top" id="lbl3" runat="server"><div><strong><asp:Label ID="Label3" runat="server" Text="3"></asp:Label></strong></div>
                        <asp:PlaceHolder ID="PlaceHolder3" runat="server"></asp:PlaceHolder>
                        </td>
                        <td width="100" valign="top" id="lbl4" runat="server"><div><strong><asp:Label ID="Label4" runat="server" Text="4"></asp:Label></strong></div>
                        <asp:PlaceHolder ID="PlaceHolder4" runat="server"></asp:PlaceHolder>
                        </td>
                        <td width="100" valign="top"  id="lbl5" runat="server" ><div ><strong><asp:Label ID="Label5" runat="server" Text="5"></asp:Label></strong></div>
                        <asp:PlaceHolder ID="PlaceHolder5" runat="server"></asp:PlaceHolder>
                        </td>
                        <td width="100" valign="top" id="lbl6" runat="server"><div><strong><asp:Label ID="Label6" runat="server" Text="6"></asp:Label></strong></div>
                        <asp:PlaceHolder ID="PlaceHolder6" runat="server"></asp:PlaceHolder>
                        </td>
                        <td width="100" valign="top" id="lbl7" runat="server"><div><strong><asp:Label ID="Label7" runat="server" Text="7"></asp:Label></strong></div>
                        <asp:PlaceHolder ID="PlaceHolder7" runat="server"></asp:PlaceHolder>
                        </td>
                      </tr>
                      <tr>
                        <td width="100" height="100" valign="top" id="lbl8" runat="server"><div><strong><asp:Label ID="Label8" runat="server" Text="8"></asp:Label></strong></div>
                        <asp:PlaceHolder ID="PlaceHolder8" runat="server"></asp:PlaceHolder>
                        </td>                        
                        <td width="100" valign="top" id="lbl9" runat="server"><div><strong><asp:Label ID="Label9" runat="server" Text="9"></asp:Label></strong></div>
                        <asp:PlaceHolder ID="PlaceHolder9" runat="server"></asp:PlaceHolder>
                        </td>
                        <td width="100" valign="top" id="lbl10" runat="server"><div><strong><asp:Label ID="Label10" runat="server" Text="10"></asp:Label></strong></div>
                        <asp:PlaceHolder ID="PlaceHolder10" runat="server"></asp:PlaceHolder>
                        </td>
                        <td width="100" valign="top" id="lbl11" runat="server"><div><strong><asp:Label ID="Label11" runat="server" Text="11"></asp:Label></strong></div>
                        <asp:PlaceHolder ID="PlaceHolder11" runat="server"></asp:PlaceHolder>
                        </td>
                        <td width="100" valign="top" id="lbl12" runat="server"><div><strong><asp:Label ID="Label12" runat="server" Text="12"></asp:Label></strong></div>
                        <asp:PlaceHolder ID="PlaceHolder12" runat="server"></asp:PlaceHolder>
                        </td>
                        <td width="100" valign="top" id="lbl13" runat="server"><div><strong><asp:Label ID="Label13" runat="server" Text="13"></asp:Label></strong></div>
                        <asp:PlaceHolder ID="PlaceHolder13" runat="server"></asp:PlaceHolder>
                        </td>
                        <td width="100" valign="top" id="lbl14" runat="server"><div><strong><asp:Label ID="Label14" runat="server" Text="14"></asp:Label></strong></div>
                        <asp:PlaceHolder ID="PlaceHolder14" runat="server"></asp:PlaceHolder>
                        </td>
                      </tr>
                    </table>
        <br />
     
  </td>
 </tr>
 </table>
 </td>
   </tr>
      <tr>
   <td><table>
   <tr>
   
    <td style="width:540px;">
    <table style="width:540px;" id="TaskManagerGrid" cellpadding="0" cellspacing="0">      
           
    </table> 
    <div style="width:100%;" id="TaskManagerGridPager">
        
        </div>
    </td>
    <td style="width:540px;">
        <table style="width:540px;" id="InterventionGrid" cellpadding="0" cellspacing="0">      
           </table>
           <div style="width:100%;" id="InterventionGridPager">
           </div>
   </td>
    </tr>
   </table></td>
   </tr>
      <tr>
   <td valign ="top" >
   <table><tr><td valign ="top">
   <table cellpadding ="0" cellspacing ="0" border ="0" width ="540">
    <tr>
    <td class="ui-jqgrid-titlebar ui-widget-header ">
    <asp:Button ID="lbtnPrev" runat="server" Text="<<" onclick="lbtnPrev_Click" ForeColor ="Black" BackColor ="#72A9D3" />
        <asp:Label ID="lbtnStudentName" runat="server" ForeColor ="White" 
            BackColor ="#72A9D3" Font-Bold="True" Font-Size="8pt"></asp:Label>
        <asp:Button ID="lbtnNext" runat="server" Text=">>" onclick="btnNext_Click" ForeColor ="Black" BackColor ="#72A9D3"/>
         
    </td>
    <td class="ui-jqgrid-titlebar ui-widget-header"><span class="style1">Testing 
        Date/Time:&nbsp;</span>&nbsp;&nbsp;
   <asp:Label ID ="lblDate" runat ="server" ForeColor ="Black" BackColor ="#72A9D3" 
            Font-Bold="True" Font-Size="8pt">DateTime</asp:Label>
                       
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lbtnID" runat="server" ForeColor ="White" BackColor ="#72A9D3" 
            Font-Bold="True" Font-Size="8pt">ID</asp:LinkButton>
                     
            </td>
    </tr> 
    
    <tr>
    <td valign ="top">
    <br />
     <br />
    <asp:GridView ID="GridViewSection" runat="server" AutoGenerateColumns="False" 
           onpageindexchanging="GridViewSection_PageIndexChanging" CssClass="datagrid-white"
           Font-Names="Calibri" Font-Size="8pt" GridLines="None" Width="220px">
           <Columns>
               <asp:BoundField DataField="SectionsName"  HeaderText="Section Name">
                   <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                   <ItemStyle Width="150px" />
               </asp:BoundField>
               <asp:BoundField DataField="Score" HeaderText="Rank">
               <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top"/>
               <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"/>
               </asp:BoundField>
           </Columns>
           
           <HeaderStyle BackColor="#85B5D9" />
           
       </asp:GridView>
    </td>
    
    <td valign ="top" align ="right" colspan="2" style="padding-left:20px;" >
     Assessment Name:<asp:DropDownList ID="ddlAssesment" runat="server" Height="28px" Width="174px" 
           AutoPostBack="True" DataMember="AssessmentName" DataValueField="AssessmentName" 
           ToolTip="Select Assesment Name" 
           onselectedindexchanged="ddlAssesment_SelectedIndexChanged" 
           Font-Names="Calibri"> </asp:DropDownList>   
       
    <asp:GridView ID="GridViewScore" runat="server" AutoGenerateColumns="False"  CssClass="datagrid-white"          
           Font-Names="Calibri" Font-Size="8pt" GridLines="None" Width="290px" 
            onselectedindexchanged="GridViewScore_SelectedIndexChanged">
           <Columns>
               <asp:BoundField DataField="SectionsName"  HeaderText="Question">
                   <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                   <ItemStyle Width="130px" HorizontalAlign="Left" VerticalAlign="Top"/>
               </asp:BoundField>
               <asp:BoundField DataField="Response" HeaderText="Answer" >
                   <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                   <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="230px" />
               </asp:BoundField>
           </Columns>
           
           <HeaderStyle BackColor="#85B5D9" />
           
       </asp:GridView>
       <br />
       
       <%--<a href =SendEmail.aspx target ="_blank" >SendEmail</a>--%>
    </td>
    </tr>
       
    </table>
    </td>
    <td valign ="top"> 
    <div id="main_div">
        <div id="chart_div" style="float:left;display:block; width:50%">
        
        <asp:Chart ID="Chart1" runat="server" DataSourceID="SqlDataSource1" 
              Height="500px" Width="460px" ImageStorageMode="UseImageLocation" EnableViewState="False"  ImageType="Png" ImageLocation="~/c/ChartPic_#SEQ(300,3)">
              <%--<asp:Chart ID="Chart1" runat="server" DataSourceID="SqlDataSource1" 
              Height="400px" Width="700px" ImageStorageMode="UseImageLocation"  EnableViewState="False"  ImageType="Png">--%>
                
               
            
            <Titles>
            <asp:Title Name="DefaultTitle" Font="Trebuchet MS, 10pt, style=Bold"
                  Text = "All Student Testing Moving Average" ForeColor="White" BackColor="#72A9D3" Alignment ="MiddleLeft"  />
               
           </Titles>
           
              <Series>
             
                  <asp:Series Name="LPrealgebra" Label="15" YValueMembers="minPrealgebra" 
                      YValueType="Auto" IsValueShownAsLabel ="true"  Color="#4D82A9" Legend ="Legend1"> 
                                 
                  </asp:Series>
                  
                   <asp:Series Name="APrealgebra" Label="58.29" YValueMembers="avgPrealgebra" 
                   YValueType="Auto" IsValueShownAsLabel ="true" Color="#94A1BE" Legend ="Legend1">
                  </asp:Series>
                  
                   <asp:Series Name="HPrealgebra" Label="99" YValueMembers="maxPrealgebra" 
                   YValueType="Auto" IsValueShownAsLabel ="true" Color="#096C5D" Legend ="Legend1">
                  </asp:Series>


                 </Series>
          

              <Series>
             
                  <asp:Series Name="LAlgebra" Label="11" YValueMembers="minAlgebra" 
                      YValueType="Auto" IsValueShownAsLabel ="true"  Color="#4D82A9" Legend ="Legend1"> 
                                  
                  </asp:Series>
                   <asp:Series Name="AAlgebra" Label="37.65" YValueMembers="avgAlgebra" 
                   YValueType="Auto" IsValueShownAsLabel ="true" Color="#94A1BE" Legend ="Legend1">
                  </asp:Series>
                   <asp:Series Name="HAlgebra" Label="97" YValueMembers="maxAlgebra" 
                   YValueType="Auto" IsValueShownAsLabel ="true" Color="#096C5D" Legend ="Legend1">
                  </asp:Series>


                                   
          
              </Series>
              
              <Series>
                  <asp:Series Name="LWritting" Label="7" YValueMembers="minWritting" 
                      YValueType="Auto" Color="#4D82A9" Legend ="Legend1">               
                  </asp:Series>
                   <asp:Series Name="AWritting" Label="71.31" YValueMembers="avgWritting" 
                   YValueType="Auto" Color="#94A1BE" Legend ="Legend1">
                  </asp:Series>
                   <asp:Series Name="HWritting" Label="99" YValueMembers="maxWritting" 
                   YValueType="Auto" Color="#096C5D" Legend ="Legend1">
                  </asp:Series>
                  
                  </Series>
               
                 <Series>
                  <asp:Series Name="LReading" Label="21" YValueMembers="minReading" 
                      YValueType="Auto" Color="#4D82A9">               
                  </asp:Series>
                   <asp:Series Name="AReading" Label="84.31" YValueMembers="avgReading" 
                   YValueType="Auto" Color="#94A1BE">
                  </asp:Series>
                   <asp:Series Name="HReading" Label="99" YValueMembers="maxReading" 
                   YValueType="Auto" Color="#096C5D">
                  </asp:Series>
                  
                  </Series>
                  
                  
                 <Series>
                  <asp:Series Name="LEnglish" Label="7" YValueMembers="minAssessmentEnglish" 
                      YValueType="Auto" Color="#4D82A9">               
                  </asp:Series>
                   <asp:Series Name="AEnglish" Label="16.91" YValueMembers="avgAssessmentEnglish" 
                   YValueType="Auto" Color="#94A1BE">
                  </asp:Series>
                   <asp:Series Name="HEnglish" Label="30" YValueMembers="maxAssessmentEnglish" 
                   YValueType="Auto" Color="#096C5D">
                  </asp:Series>
                  
                  </Series>
                  
                  
                   <Series>
                  <asp:Series Name="LMath" Label="11" YValueMembers="minAssessmentMath" 
                      YValueType="Auto" Color="#4D82A9">               
                  </asp:Series>
                   <asp:Series Name="AMath" Label="17.92" YValueMembers="avgAssessmentMath" 
                   YValueType="Auto" Color="#94A1BE">
                  </asp:Series>
                   <asp:Series Name="HMath" Label="58" YValueMembers="maxAssessmentMath" 
                   YValueType="Auto" Color="#096C5D">
                  </asp:Series>
                  
                  </Series>
                  
                   <Series>
                  <asp:Series Name="LReadingAssessment" Label="9" YValueMembers="minReadingAssessment" 
                      YValueType="Auto" Color="#4D82A9" >               
                  </asp:Series>
                   <asp:Series Name="AReadingAssessment" Label="18.03" YValueMembers="avgReadingAssessment" 
                   YValueType="Auto" Color="#94A1BE">
                  </asp:Series>
                   <asp:Series Name="HReadingAssessment" Label="31" YValueMembers="maxReadingAssessment" 
                   YValueType="Auto" Color="#096C5D">
                  </asp:Series>
                  
                  </Series>
                  
                   <Series>
                  <asp:Series Name="LScienceAssessment" Label="9" YValueMembers="minScienceAssessment" 
                      YValueType="Auto" Color="#4D82A9" >               
                  </asp:Series>
                   <asp:Series Name="AScienceAssessment" Label="19.07" YValueMembers="avgScienceAssessment" 
                   YValueType="Auto" Color="#94A1BE">
                  </asp:Series>
                   <asp:Series Name="HScienceAssessment" Label="87" YValueMembers="maxScienceAssessment" 
                   YValueType="Auto" Color="#096C5D">
                  </asp:Series>
                  
                  </Series>
                  
              <ChartAreas>
               <asp:ChartArea Name="ChartArea1">
                <InnerPlotPosition X="10" Y="10" Height="80" Width="80" />
                 
                  <AxisY Title ="Score" Interval ="10">
                       <MajorGrid Enabled="false" /> 
                  </AxisY>
                  <AxisX  Title ="Subject" IsMarginVisible="False">
                  <%--<LabelStyle Enabled="true"  />

                   <MajorGrid LineWidth="1" />--%>
                   <MajorGrid Enabled ="false" />
                   
                    <CustomLabels>
                   <asp:CustomLabel FromPosition="1.3" ToPosition="0" Text="PreAlg"/>
                    
                    <%--<asp:CustomLabel FromPosition="1.25" ToPosition="0" Text="L"/>
                    <asp:CustomLabel FromPosition="1.30" ToPosition="0" Text="A"/>
                    <asp:CustomLabel FromPosition="1.35" ToPosition="0" Text="H"/>--%>
                    
                   <asp:CustomLabel FromPosition="1.4" ToPosition="0.1" Text="Alg" />
                    
                    <%--<asp:CustomLabel FromPosition="1.35" ToPosition="0" Text="L"/>
                    <asp:CustomLabel FromPosition="1.40" ToPosition="0" Text="A"/>
                    <asp:CustomLabel FromPosition="1.45" ToPosition="0" Text="H"/>--%>
                    
                    <asp:CustomLabel FromPosition="1.5" ToPosition="0.2" Text="Writ" />
                    
                    <%--<asp:CustomLabel FromPosition="1.5" ToPosition="0" Text="L"/>
                    <asp:CustomLabel FromPosition="1.5" ToPosition="0.05" Text="A"/>
                    <asp:CustomLabel FromPosition="1.5" ToPosition="0.1" Text="H"/>
                    --%>
                    <asp:CustomLabel FromPosition="1.6" ToPosition="0.3" Text="Read" />
                    
                   <%-- <asp:CustomLabel FromPosition="1.6" ToPosition="0" Text="L"/>
                    <asp:CustomLabel FromPosition="1.6" ToPosition="0.05" Text="A"/>
                    <asp:CustomLabel FromPosition="1.6" ToPosition="0.1" Text="H"/>--%>
                    
                    <asp:CustomLabel FromPosition="1.7" ToPosition="0.4" Text="Eng" />
                    
                    <%--<asp:CustomLabel FromPosition="1.7" ToPosition="0" Text="L"/>
                    <asp:CustomLabel FromPosition="1.7" ToPosition="0.05" Text="A"/>
                    <asp:CustomLabel FromPosition="1.7" ToPosition="0.1" Text="H"/>
                    --%>
                    <asp:CustomLabel FromPosition="1.8" ToPosition="0.5" Text="Math" />
                    
                   <%-- <asp:CustomLabel FromPosition="1.8" ToPosition="0" Text="L"/>
                    <asp:CustomLabel FromPosition="1.8" ToPosition="0.05" Text="A"/>
                    <asp:CustomLabel FromPosition="1.8" ToPosition="0.1" Text="H"/>--%>
                    
                    <asp:CustomLabel FromPosition="1.9" ToPosition="0.6" Text="Read" />
                    
                   <%-- <asp:CustomLabel FromPosition="1.9" ToPosition="0" Text="L"/>
                    <asp:CustomLabel FromPosition="1.9" ToPosition="0.05" Text="A"/>
                    <asp:CustomLabel FromPosition="1.9" ToPosition="0.1" Text="H"/>--%>
                    
                    <asp:CustomLabel FromPosition="2" ToPosition="0.7" Text="Sci" />
                 <%--   
                    <asp:CustomLabel FromPosition="2" ToPosition="0.7" Text="L"/>
                    <asp:CustomLabel FromPosition="2" ToPosition="0.05" Text="A"/>
                    <asp:CustomLabel FromPosition="2" ToPosition="0.1" Text="H"/>--%>
                    
                    </CustomLabels>        
                  </AxisX>
            </asp:ChartArea>
                 
              </ChartAreas>
             
           <%-- <Legends>  
                <asp:Legend   
                    Name="Legend1" LegendStyle="Column" LegendItemOrder="SameAsSeriesOrder">  
                    
                </asp:Legend>  
                
            </Legends> --%>
          </asp:Chart>
          
          </div> <!--chart div -->
          <div id="Legend" style="font-weight:bold;  float:left;  margin-left:40px; margin-top :15px ">
          <ul style=" list-style:none;" >
          <li style=" float: none;list-style: none;"><img src="images/Low.png"  />
          <span style="font-size:18px;"> Low</span></li>
          <li style="  float: none;list-style: none;"><img src="images/avg.png" />
          <span style="font-size:18px;"> Average</span></li>
          <li style=" float: none;list-style: none;"><img src="images/High.png"  />
          <span style="font-size:18px;"> High</span></li>
          </ul>
          </div><!--Legend div -->
          </div> <!-- main div-->
          <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
              ConnectionString="<%$ ConnectionStrings:cvtc_tempConnectionString %>" SelectCommand="								SELECT     
					  MAX(CAST(LatestCompassPrealgebraTestScore AS numeric(11, 3))) AS maxPrealgebra, 
					  MIN(CAST(LatestCompassPrealgebraTestScore AS numeric(11, 3))) AS minPrealgebra, 
                      AVG(CAST(LatestCompassPrealgebraTestScore AS numeric(11, 3))) AS avgPrealgebra, 
					  
					  MAX(CAST(LatestCompassAlgebraTestScore AS numeric(11, 3))) AS maxAlgebra, 
                      MIN(CAST(LatestCompassAlgebraTestScore AS numeric(11, 3))) AS minAlgebra, 
                      AVG(CAST(LatestCompassAlgebraTestScore AS numeric(11, 3))) AS avgAlgebra,


					  MAX(CAST(LatestCompassWritingTestScore AS numeric(11, 3))) AS maxWritting, 
                      MIN(CAST(LatestCompassWritingTestScore AS numeric(11, 3))) AS minWritting, 
                      AVG(CAST(LatestCompassWritingTestScore AS numeric(11, 3))) AS avgWritting,
                                            
                      MAX(CAST(LatestCompassReadingTestScore AS numeric(11, 3))) AS maxReading,
                      MIN(CAST(LatestCompassReadingTestScore AS numeric(11, 3))) AS minReading, 
                      AVG(CAST(LatestCompassReadingTestScore AS numeric(11, 3))) AS avgReading,


                      
                      MAX(CAST(LatestACTEnglishAssessmentScore AS numeric(11, 3))) AS maxAssessmentEnglish, 
                      MIN(CAST(LatestACTEnglishAssessmentScore AS numeric(11, 3))) AS minAssessmentEnglish, 
                      AVG(CAST(LatestACTEnglishAssessmentScore AS numeric(11, 3))) AS avgAssessmentEnglish,
                      
                      
                      MAX(CAST(LatestACTMathAssessmentScore AS numeric(11, 3))) AS maxAssessmentMath, 
                      MIN(CAST(LatestACTMathAssessmentScore AS numeric(11, 3))) AS minAssessmentMath, 
                      AVG(CAST(LatestACTMathAssessmentScore AS numeric(11, 3))) AS avgAssessmentMath,
                      
                      MAX(CAST(LatestACTReadingAssessmentScore AS numeric(11, 3))) AS maxReadingAssessment, 
                      MIN(CAST(LatestACTReadingAssessmentScore AS numeric(11, 3))) AS minReadingAssessment, 
                      AVG(CAST(LatestACTReadingAssessmentScore AS numeric(11, 3))) AS avgReadingAssessment,
                      
                      MAX(CAST(LatestACTScienceAssessmentScore AS numeric(11, 3))) AS maxScienceAssessment, 
                      MIN(CAST(LatestACTScienceAssessmentScore AS numeric(11, 3))) AS minScienceAssessment, 
                      AVG(CAST(LatestACTScienceAssessmentScore AS numeric(11, 3))) AS avgScienceAssessment
                      
FROM         dbo.Student
where LatestCompassPrealgebraTestScore!='0' and LatestCompassAlgebraTestScore!='0' and LatestCompassWritingTestScore!='0' and
LatestCompassReadingTestScore!='0' and LatestACTEnglishAssessmentScore!='0' and LatestACTMathAssessmentScore!='0' and
LatestACTReadingAssessmentScore!='0' and LatestACTScienceAssessmentScore!='0'" 
              
              
              ProviderName="<%$ ConnectionStrings:cvtc_tempConnectionString.ProviderName %>"></asp:SqlDataSource></td></tr></table></td>
   </tr>
   </table>
   
    </form>
    
    <script type="text/javascript" >
          function task(){
              $("#tid").click(); 
            }
            function message(){
              $("#mid").click(); 
            }
    </script>
    
</body>
</html>
