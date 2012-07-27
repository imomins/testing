<%@ Page Language="C#" AutoEventWireup="true" CodeFile="studentProfile.aspx.cs" Inherits="pg_student_studentProfile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student Profile</title>
    <link rel="stylesheet" type="text/css" media="screen" href="../../themes/redmond/jquery-ui-1.8.1.custom.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../themes/ui.jqgrid.css" />

    <link href="../../css/global-style.css" rel="stylesheet" type="text/css" />
    <script src="../../js/jquery.js" type="text/javascript"></script> 
    <script src="../../js/grid.locale-en.js" type="text/javascript"></script> 
    <script src="../../js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="../../js/thickbox.js" type="text/javascript"></script>
    <link href="../../themes/thickbox.css" rel="stylesheet" type="text/css" />
    
    
    <style type="text/css">
     .Unread
        {
        	 background-color :Gray ;

        }
        .Unread
        {
        	 background-color:Transparent ;

        }
        	
    </style>
    
    <script type ="text/javascript" >
//        $(document).ready(function() {
//            //        function changeTabName() {
//            var studentName = document.getElementById("<%= lblStudentName.ClientID %>").innerHTML;
//            //alert(studentName);
//           $('lbtnNext').live('click', function() {
//           $('#00005421').val(studentName);
//            });
//        });
    </script>
    
    <script type="text/javascript">
        $(document).ready(function() {
            $('#btnPrescrip').live('click', function() {
                $("#popupid").click();
            });
        	
            window.parent.changeTabName(document.getElementById("<%= lblStudentName.ClientID %>").innerHTML);
            
           
            $(".iamabull").click(function() {
                // printDiv();
            });

        });


  </script>
  
  <script type ="text/javascript" >
  function printDiv()
{
  var divToPrint=document.getElementById('print_div1');
  var newWin=window.open('','Print-Window','width=800,height=810,scrollbars=1');
  newWin.document.open();  
  newWin.document.write('<html><body onload="window.print()">'+divToPrint.innerHTML+'</body></html>');
  newWin.document.close();
  }
  
  </script>
  
    </head>
<body>

<%--<a href="../intervention/Prescription.aspx?studentID=@036464&keepThis=true&TB_iframe=true&height=450&width=550" class="thickbox" id="popupid" style="display:none;" >Dislay</a>--%>
    <form id="form1" runat="server">
<a href="../intervention/Prescription.aspx?keepThis=true&TB_iframe=true&height=450&width=650" class="thickbox" id="popupid" style="display:none;" >Dislay</a>
    <div id = "print_div1" runat ="server" visible ="true" style="display:none;" >
   
    </div>
   <table width="100%">
   <tr>
   
   <td width="100%">
   <div width="100%">
    <asp:Button ID="Button1" runat="server" Text="Refresh" 
        onclick="ButtonRefresh_Click" style ="width :100px; height :25px"/>
    <asp:Label ID="LabelRisk"  runat="server" Text="" ForeColor="Red"
           Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;
               
    <asp:Label ID="LabelAllert" Visible ="False"  runat="server" Text="Allert" 
           Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;
    <asp:Label ID="LabelMC" runat="server" Visible ="False" Text="Multicultural" 
           Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;
    <asp:Label ID="LabelPell" runat="server" Visible ="False"  Text="Pell" 
           Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;
    <asp:Label ID="LabelNTO" runat="server" Visible ="False" Text="NTO" 
           Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;
    <asp:Label ID="LabelRVP" runat="server" Visible ="False" Text="RVP" 
           Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;
    </div>
   <div width="100%">
       <b> Select Assesment:</b>
      <asp:DropDownList ID="ddlAssesment" runat="server" Height="20px" Width="174px" 
           AutoPostBack="True" DataMember="AssessmentName" DataValueField="AssessmentName" 
           ToolTip="Select Assesment Name" 
           onselectedindexchanged="ddlAssesment_SelectedIndexChanged" 
           Font-Names="Calibri"> </asp:DropDownList>          
       Exam Date :&nbsp;<asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       Birth Date :&nbsp;<asp:Label ID="lblBirth" runat="server" Text=""></asp:Label>
         
   </div>
    <hr id="Hr1" width="100%"  align="left" /> 
   </td>
   
   </tr>
   <tr><td> 
   <table width ="100%">
   <tr >
   <td >  
       <asp:LinkButton ID="lbtnPrev" runat="server" onclick="lbtnPrev_Click" 
           Font-Names="Calibri" Font-Underline="True"><b >‹‹Previous Student</b>
           </asp:LinkButton>  
   </td>
   <td >
   <b >Student Name:</b>
   <%--<asp:Label ID="lblStudentName" runat="server" Width="173px" ReadOnly="True"></asp:Label>--%>
       <asp:Label ID="lblStudentName" runat="server" Text="" Width="173px" ReadOnly="True"></asp:Label>
    </td>
      <td >
      <b >ID#</b>
          <asp:Label ID="lblID" runat="server" Width="107px" ReadOnly="True"></asp:Label>
      </td>
      <td>
      <b >Telephone:</b>
          <asp:Label ID="lblTelephone" runat="server" Width="102px" ReadOnly="True"></asp:Label>     
      </td>
      <td>
      <b>Imported:</b>
      </td>
      <td >
         
      <%--<asp:LinkButton ID="lbtnNext" runat="server" onclick="lbtnNext_Click" 
              Font-Names="Calibri" OnClientClick="window.parent.changeTabName('Delowar');"><b >Next Student</b></asp:LinkButton>--%>
              <asp:LinkButton ID="lbtnNext" runat="server" onclick="lbtnNext_Click" 
              Font-Names="Calibri" Font-Underline="True"><b >Next Student	››</b></asp:LinkButton>
      </td>
   </tr>
   </table>
   </td></tr>
   <tr><td>
   <hr id="Hr2" width="100%"  align="left" /> 
   
   </td></tr>
   <tr><td> 
   <table width ="100%" >
   <tr >
   <td class="style19">
   <b>CVTC Program:</b>
   <asp:Label ID ="lblCVTCProgram" runat="server" ReadOnly ="true " Width="500px"  ></asp:Label>
   </td>   
   
   <td  >
   <b>Pre Program:</b>
   <asp:Label ID ="lblPreProgram" runat="server" ReadOnly ="true " Width="156px"  ></asp:Label>
   </td>
   </tr>
  
   </table></td></tr>
   </table>
   <table  id ="AssesmentTable"width ="100%" >
    <tr>
    <td>
        <b>ISS Assessment Scores</b>
        
        <hr id="ISSAssesment" width="100%" align="left"  
             />
        </td>
     <td><b>Testing Scores
         </b>
         <hr id="TestingScore" width="100%"  align="left" /></td>
    </tr>
    
    <tr>
        
            <td width="50%" valign="top">
            
                <table id ="AssesmentScoreTable" width ="100%">
                    <tr>
                        <td width="35%" valign="top">
                           <table id ="SectionTable" width ="100%" >
                           <tr>
                           <td valign ="top" align="left" style="height:50px;">
                               <asp:GridView ID="GridViewSection" runat="server" AutoGenerateColumns="False" 
           onpageindexchanging="GridViewSection_PageIndexChanging" CssClass="datagrid-white"
           Font-Names="Calibri" Font-Size="8pt" GridLines="None" Width="200px">
           <Columns>
               <asp:BoundField DataField="SectionsName"  HeaderText="Section Name">
                   <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                   <ItemStyle Width="200px" VerticalAlign="Top"/>
               </asp:BoundField>
               <asp:BoundField DataField="Score" HeaderText="Rank" />
           </Columns>
           
           <HeaderStyle BackColor="#85B5D9" VerticalAlign="Top"/>
           
       </asp:GridView>
                           </td>
                           </tr>
                                                    
                           </table>
                           <br />
                           <br />
                           
                           <table id="StaticValueTable1" width ="100%" style ="display :none ">
                          <tr>
                           <td>
                           High School:
                           <asp:Label ID="lblHSname" runat="server" ReadOnly ="true" Width="127px"></asp:Label>
                           </td>
                           </tr>
                           
                           <tr>
                           <td>
                           HS Size:
                           <asp:Label ID="lblHSSize" runat="server" ReadOnly ="true" Width="89px"></asp:Label>
                           </td>
                           </tr>
                           
                           <tr>
                           <td>
                           HS Program:
                           <asp:Label ID="lblHSProgram" runat="server" ReadOnly ="true" Width="89px"></asp:Label>
                           </td>
                           </tr>
                           
                           <tr>
                           <td>
                          HS Standard:
                           <asp:Label ID="lblHSStandard" runat="server" ReadOnly ="true" Width="89px"></asp:Label>    
                           </td>
                           </tr>
                           
                           <tr>
                           <td>
                           ISS Date:
                           <asp:Label ID="lblISSdate" runat="server" ReadOnly ="true" Width="89px"></asp:Label>    
                           </td>
                           </tr>
                           </table>
                         </td>
                        <td valign="top">
                         <table id ="QuestionTable" width ="100%" >
                         <tr>
                         <td valign ="top" align="justify" style="height:50px;">
       <asp:GridView ID="GridViewScore" runat="server" AutoGenerateColumns="False"  CssClass="datagrid-white"          
           Font-Names="Calibri" Font-Size="8pt" GridLines="None" Width="380px">
           <Columns>
               <asp:BoundField DataField="SectionsName"  HeaderText="Question">
                   <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                   <ItemStyle Width="130px" />
               </asp:BoundField>
               <asp:BoundField DataField="Response" HeaderText="Answer" >
                   <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                   <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="250px" />
               </asp:BoundField>
           </Columns>
           
           <HeaderStyle BackColor="#85B5D9" />
           
       </asp:GridView>
                         </td>
                         </tr>
                         
                         <%--  <tr>
                           <td>
                           Carrier Choice:
                           </td>
                           </tr>
                           
                           <tr>
                           <td>
                           Carrier Choice:
                           </td>
                           </tr>
                           
                           <tr>
                           <td>
                           Carrier Choice:
                           </td>
                           </tr>
                           
                           <tr>
                           <td>
                           Carrier Choice:
                           </td>
                           </tr>
                           
                           <tr>
                           <td>
                           Carrier Choice:
                           </td>
                           </tr>
                           
                           <tr>
                           <td>
                           Carrier Choice:
                           </td>
                           </tr>
                           
                           <tr>
                           <td>
                           Carrier Choice:
                           </td>
                           </tr>
                           
                           <tr>
                           <td>
                           Carrier Choice:
                           </td>
                           </tr>
                           
                           <tr>
                           <td>
                           Carrier Choice:
                           </td>
                           </tr>
                           
                           <tr>
                           <td>
                           Carrier Choice:
                           </td>
                           </tr>--%>
                           
                           </table>
                           <br />
          
                           <br />
                           
                           <table id="StaticValueTable2" width ="100%" style="display :none ">
                           <tr>
                           <td >
                               Grad Date:
                           <asp:Label ID="lblgraddate" runat="server" ReadOnly ="true" Width="89px"></asp:Label>
                           </td>
                           </tr>
                           
                           <tr>
                           <td>
                           HS Grade:
                           <asp:Label ID="lblhsgrade" runat="server" ReadOnly ="true" Width="89px"></asp:Label>
                           </td>
                           </tr>
                           
                           <tr>
                           <td>
                               Term:
                           <asp:Label ID="lblTerm" runat="server" ReadOnly ="true" Width="89px"></asp:Label>
                           </td>
                           </tr>
                           
                           <tr>
                           <td>
       MultiCulturual pell NTO:
       <asp:Label ID="lblMulti" runat="server" ReadOnly ="true" Width="79px"></asp:Label>    
                           </td>
                           </tr>
                           
                           <tr>
                           <td>
                               BirthDate:
                           <asp:Label ID="lblBirthDate" runat="server" ReadOnly ="true" Width="89px"></asp:Label>    
                           </td>
                           </tr>
                           </table>
                           
                        </td>
                    </tr>
                </table>
            
            </td>
            
            <td width="50%" valign="top">
            <table id= "StaticValueTable3" width ="100%">
                        
            <tr>
            <td>
           
            <table width ="100%">
            <tr>
             <td width="30%" valign ="top">
            <table width ="100%">
            <tr>
            <td>
                <b>ACT
            </b>
            </td>
            </tr>
            
            <tr>
            <td>
            ACTE :<asp:Label ID="lblacte" runat="server" ReadOnly ="true" Width="70px"></asp:Label>
            </td>
            </tr>
            
            <tr>
            <td>
            ACTM :<asp:Label ID="lblactm" runat="server" ReadOnly ="true" Width="70px"></asp:Label>
            </td>
            </tr>
            
            <tr>
            <td>
            ACTR :<asp:Label ID="lblactr" runat="server" ReadOnly ="true" Width="70px"></asp:Label>
            </td>
            </tr>
            
            <tr>
            <td>
            ACTS :<asp:Label ID="lblacts" runat="server" ReadOnly ="true" Width="70px"></asp:Label>
            </td>
            </tr>
            </table>
            </td>
            
             <td width="40%" valign ="top">
            <table width ="100%">
            <tr>
            <td>
                <b>Compass</b></td>
            </tr>
            
            <tr>
            <td>
                Pre Algebra :<asp:Label ID="lblprealgebra" runat="server" ReadOnly ="true" 
                    Width="71px" Height="16px"></asp:Label>
            </td>
            </tr>
            
            <tr>
            <td>
                Algebra :<asp:Label ID="lblalgebra" runat="server" ReadOnly ="true" Width="89px"></asp:Label>
            </td>
            </tr>
            
            <tr>
            <td>
                Reading :<asp:Label ID="lblreading" runat="server" ReadOnly ="true" Width="89px"></asp:Label>
            </td>
            </tr>
            
            <tr>
            <td>
                Writting :<asp:Label ID="lblwritting" runat="server" ReadOnly ="true" Width="89px"></asp:Label>    
            </td>
            </tr>
            </table>
            </td>
            
             <td width="30%" valign ="top" >
            <table width ="100%">
            <tr valign ="top" >
            <td valign ="top">
                &nbsp;</td>
            </tr>
              <tr>
              <td>
              </td>
              </tr>         
           
              <tr>
              <td>
              </td>
              </tr>
              <tr>
              <td>
              </td>
              </tr>
              <tr>
              <td>
              </td>
              </table>
            </td>
            
            </tr>
            
            <tr>
            <td>
            Cum. GPA:
            <asp:Label ID="lblCumulativeGPA" runat="server" ReadOnly ="true" Width="43px"></asp:Label>
            </td>
            
            <td>
            Credit Hrs Attemted:
            <asp:Label ID="lblCreditAttempted" runat="server" ReadOnly ="true" Width="35px"></asp:Label>
            </td>
            
            <td>
            Earned:
            <asp:Label ID="lblCreditEarned" runat="server" ReadOnly ="true" Width="49px" 
                    Height="19px"></asp:Label>
            </td>
            </tr>
                        
            </table>
           
            </td>
          </tr>
          
          
          <tr>
          <td width="100%">
    <asp:GridView ID="CourseGrid" runat="server" AutoGenerateColumns="False" 
           AllowPaging ="True" onpageindexchanging="CourseGrid_PageIndexChanging" 
                style="margin-left: 0px" PageSize="5" Font-Names="Calibri" 
               Font-Size="8pt" Width="95%">
       <Columns>
           <asp:BoundField DataField="TermCodeOfCourseEnrollment" HeaderText="Term" >
           <ItemStyle  Width ="25%" Height ="30px"/>
           </asp:BoundField>
           <asp:BoundField DataField="CourseTitle" HeaderText="Course Title" >
            <ItemStyle  Width ="45%" Height ="30px"/>
           </asp:BoundField>
           <asp:BoundField DataField="CourseNumber" HeaderText="Course ID">
           <ItemStyle  Width ="15%" Height ="30px"/>
            </asp:BoundField>
           <asp:BoundField DataField="FinalGrade" HeaderText="Grade">
           <ItemStyle  Width ="15%" Height ="30px"/>
            </asp:BoundField>
       </Columns>
       <HeaderStyle BackColor="#85B5D9" />
       </asp:GridView>
          
           </td>
          </tr>
        </table>
            </td>
        </tr>
    </table>
    
    <table id="PrescriptionTable" width ="100%">
    <tr>
    <td>
    <b>Intervention</b>
    <hr  width="100%"/>
    </td>
    </tr>
    <tr>
    <td align ="right" >
     
        <asp:Button ID="btnPrintAll" runat="server" Text="Print All Interventions"  
            onclick="btnPrintAll_Click" CssClass="iamabull" Visible="False" />
     
    </td>
    </tr>
    <tr>
    
    <td width="100%">
        <asp:HiddenField ID="HiddenRiskOID" runat="server" />
      <asp:GridView ID="GridViewIntervention" runat="server" AllowPaging ="True" 
          AutoGenerateColumns="False" CssClass="datagrid-white"
          onpageindexchanging="GridViewIntervention_PageIndexChanging" 
          onselectedindexchanging="GridViewIntervention_SelectedIndexChanging" 
          Font-Names="Calibri" Font-Size="8pt" Width ="100%" 
            onrowdatabound="GridViewIntervention_RowDataBound" >
          <Columns>
          
           <asp:TemplateField>
                  <EditItemTemplate>
                   
                  </EditItemTemplate>
                  <ItemTemplate>
                     
                     <asp:Image ID="HandOffURLs" runat="server" ImageUrl='<%# Eval("FlagURL") %>' ImageAlign="Middle"  OnLoad ="HandOffURLs_Load"/>
                     <asp:HiddenField ID="HiddenFieldUnread" runat="server" Value='<%# Bind("Unread") %>' />
                     <asp:HiddenField ID="HiddenFieldFlag" runat="server" Value='<%# Bind("HandOff") %>' />
                     <asp:HiddenField ID="HiddenFieldUser" runat="server" Value='<%# Bind("UserOID") %>' />
                      <asp:HiddenField ID="HiddenFieldPOID" runat="server" Value='<%# Bind("PrescriptionOID") %>' />
                  </ItemTemplate>
              </asp:TemplateField>
              
              
              <asp:BoundField DataField="CreatedDate" HeaderText="Datestamp">
                  <ItemStyle Height="18px" Width="135px" />
              </asp:BoundField>
              <asp:TemplateField HeaderText="Banner ID">
                  <EditItemTemplate>
                      <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("BannerID") %>'></asp:TextBox>
                  </EditItemTemplate>
                  <ItemTemplate>
                      <a href='../intervention/Prescription.aspx?poid=<%# Eval("PrescriptionOID")%>&studentID=<%# Eval("BannerID")%>&riskOID=<%# Eval("RiskOID")%>&keepThis=true&TB_iframe=true&height=450&width=650'
                            class="thickbox" id="insidegrid">
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("BannerID") %>' Font-Underline="True"></asp:Label>
                            </a>
                  </ItemTemplate>
              </asp:TemplateField>
              
             <%-- <asp:BoundField HeaderText="Participating" DataField="Participating" />--%>
             <%-- <asp:TemplateField HeaderText="Participating">
                  <EditItemTemplate>
                      <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("Participating") %>'></asp:TextBox>
                  </EditItemTemplate>
                  <ItemTemplate>
                     
                      <asp:Image ID="ParticipatingURL" runat="server" ImageUrl='<%# Eval("ParticipatingURL") %>' ImageAlign="Middle" />
                  </ItemTemplate>
              </asp:TemplateField>--%>
              
              
              
              <asp:BoundField DataField="LatestActionDate" HeaderText="Action Date" />
              <asp:BoundField HeaderText="Sponsor(Avoc.)" DataField="UserName" />
              <asp:BoundField DataField="DomainName" HeaderText="Criteria Type(Domain)" />
              <asp:BoundField HeaderText="Outcome Type(Intervention)" 
                  DataField="InterventionName" />
              <asp:TemplateField HeaderText="Prescribed">
                  <EditItemTemplate>
                      <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Prescribed") %>'></asp:TextBox>
                  </EditItemTemplate>
                  <ItemTemplate>
                      <%--<asp:Label ID="Label1" runat="server" Text='<%# Bind("Prescribed") %>'></asp:Label>--%>
                      <asp:Image ID="PrescribedULR" runat="server" ImageUrl='<%# Eval("PrescribedULR") %>' ImageAlign="Middle" />
                  </ItemTemplate>
              </asp:TemplateField>
              
              
               <asp:TemplateField HeaderText="Participating">
                  <EditItemTemplate>
                      <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("Participating") %>'></asp:TextBox>
                  </EditItemTemplate>
                  <ItemTemplate>
                      <%--<asp:Label ID="Label1" runat="server" Text='<%# Bind("Prescribed") %>'></asp:Label>--%>
                      <asp:Image ID="ParticipatingURL" runat="server" ImageUrl='<%# Eval("ParticipatingURL") %>' ImageAlign="Middle" />
                  </ItemTemplate>
              </asp:TemplateField>
              
              
              
              <asp:TemplateField HeaderText="Completed">
                  <EditItemTemplate>
                      <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Completed") %>'></asp:TextBox>
                  </EditItemTemplate>
                  <ItemTemplate>
                      <asp:Image ID="CompletedURL" runat="server" ImageUrl='<%# Eval("CompletedURL") %>' ImageAlign="Middle" />
                  </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField HeaderText="Email">
                  <EditItemTemplate>
                      <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                  </EditItemTemplate>
                  <ItemTemplate>
                      <asp:Image ID="EmailURL" runat="server" ImageUrl='<%# Eval("EmailURL") %>' ImageAlign="Middle" />
                  </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField HeaderText="Telephone">
                  <EditItemTemplate>
                      <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Telephone") %>'></asp:TextBox>
                  </EditItemTemplate>
                  <ItemTemplate>
                      <asp:Image ID="TelephoneURL" runat="server" ImageUrl='<%# Eval("TelephoneURL") %>' ImageAlign="Middle" />
                  </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField HeaderText="In person">
                  <EditItemTemplate>
                      <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("InPerson") %>'></asp:TextBox>
                  </EditItemTemplate>
                  <ItemTemplate>
                      <asp:Image ID="InPersonURL" runat="server" ImageUrl='<%# Eval("InPersonURL") %>' ImageAlign="Middle" />
                  </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField HeaderText="Hand-Off">
                  <EditItemTemplate>
                      <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("HandOff") %>'></asp:TextBox>
                  </EditItemTemplate>
                  <ItemTemplate>
                      <asp:Image ID="HandOffURL" runat="server" ImageUrl='<%# Eval("HandOffURL") %>' ImageAlign="Middle" />
                  </ItemTemplate>
              </asp:TemplateField>
              
              <asp:BoundField HeaderText="Comment" DataField="Comment" Visible="False" />
          </Columns>
          <HeaderStyle BackColor="#85B5D9" />
      </asp:GridView>
      
    
    <input id="btnPrescrip" type="button" value="Prescribe" align="right"  style ="width :100px; height :25px"
            onclick='<%# String.Format("ShowCustomerPopup(\"{0}\");",Eval("studentID")) %>'/>
           <%-- <input id="Button2" type="button" value="Prescribe" align="right" 
            onclick='<%# String.Format("ShowCustomerPopup(\"{0}\");", Eval("studentID")) %>&studentID=<%# Eval("BannerID")%>'/>--%>
    
    </td>
    </tr>
    </table>
    </form>
    
    </body>
</html>
