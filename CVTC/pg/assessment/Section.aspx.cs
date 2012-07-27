using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using System.Web.Services;

public partial class pg_assessment_Section : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int sectOID;
        if (!Page.IsPostBack)
        {
            LabelMessage.Text = "";
            sectOID = Convert.ToInt32(Request.QueryString["soid"].ToString());
            Session["soid"] = sectOID;
            HiddenSectionID.Value = sectOID.ToString ();
            InitializeQuestion(sectOID);
            Sections sec = new Sections();
            Assessment ass = new Assessment();
            int AssOID = sec.GetAssessmentOIDBySectionOID(sectOID);
            if (ass.GetAssessmentStatusByOID(Convert.ToInt32(AssOID)) == 1)
            {
                HiddenAssessmentLocked.Value = "1";
            }
            else
            {
                HiddenAssessmentLocked.Value = "0";
            }
        }
        else
        {
           // sectOID=Convert.ToInt32(Session["soid"]);
            //InitializeQuestion(sectOID);
        }

    }


    private string AddFullRow(string i,string question,string response,string k,int flag, string responseAction,int reverse)
    {
        string optionFlag="";
        string optionRespAct = "";

        if (responseAction == "Radio Button")
        {            
            optionRespAct += "<option selected='true'>Radio Button</option>";
            optionRespAct += "<option>Drop Down</option>";
        }
        else if (responseAction == "Radio Button")
        {
            optionRespAct += "<option selected='true'>Radio Button</option>";
            optionRespAct += "<option>Drop Down</option>";
        }
       
       
        if (flag == 1)
        {
            optionFlag += "1";
            //optionFlag += "<option selected='true' >1</option>";
           
        }
        else if (flag == 2)
        {
            optionFlag += "2";
            //optionFlag += "<option selected='true'>2</option>";
            
        }
        else if (flag == 3)
        {
            optionFlag += "3";
            //optionFlag += "<option selected='true'>3</option>";
           
        }
        else  if (flag == 4)
        {
            optionFlag += "4";
            //optionFlag += "<option selected='true'>4</option>";
           
        }
        else if (flag == 5)
        {
            optionFlag += "5";
            //optionFlag += "<option selected='true' >5</option>";
           
        }
        else if (flag == 6)
        {
            optionFlag += "6";
           // optionFlag += "<option selected='true'>6</option>";
        }
        string str="";

        #region Dummy JavaScript Reverse Function
        //JS Function For Reverse Flag Value

        str += "<script type='text/javascript'>";
        str += " function ReverseValue(checkBox,textboxidprefix,index){";
        
        // For First Checkbox
        str += "if (index<7){";
        str += " if (document.getElementById(checkBox).checked) { for (i=1;i<=6;i++) {   if (i<2) {elementid='SelectFalgRating'+i; } else { elementid = textboxidprefix+i;} document.getElementById(elementid).value = 7-i; }  }";
        str += " else {  for (i=1;i<=6;i++){  if (i<2) {elementid='SelectFalgRating'+i; } else { elementid = textboxidprefix+i;}  document.getElementById(elementid).value =i; }}}";
        
        // For Others Checkbox(s)
        str += " else {";
        str += "  if (document.getElementById(checkBox).checked) {  for (j=1;j<=6;j++){  var val = index + j -1;  elementid = textboxidprefix+val;   document.getElementById(elementid).value = 7-j; }}";
        str += "else {  for (j=1;j<=6;j++){ var val = index + j -1;  elementid = textboxidprefix+val; document.getElementById(elementid).value = j;  }}}}";
        
        str += "</script>";

        // End Of JS Reverse Function
        #endregion

        #region JavaScript Reverse Function
        //JS Function For Reverse Flag Value

        str += "<script type='text/javascript'>";
        str += "function ReverseValue(checkBox,textboxidprefix,index){";

       //Start First IF
        str += " if (document.getElementById(checkBox).checked){";
        str += " for (i=1;i<=6;i++){";
        str += " if (i<2) { elementid='SelectFalgRating'+index;} else { elementid = textboxidprefix + i;}";
        str += "document.getElementById(elementid).value = 7 - i; }} ";
        

        str += " else {";
        str += " for (j=1;j<=6;j++){";
        str += " if (j<2) { elementid='SelectFalgRating'+index;} else { elementid = textboxidprefix + j;}";
        str += "document.getElementById(elementid).value = j; }}} ";
        //End First IF
        str += "</script>";
        // End Of JS Reverse Function



        //JS Function for Delete Question

        str += "<script type='text/javascript'>";
        str += "function DeleteQuestion1(index){";
        str += "alert(index)";
        //str += "document.getElementById('HiddenQuestionID').value = index;";
        str += "}";
        
        str += "</script>";

        //End Delete Question
        
        #endregion

        
        // = " <table>";
        str += "<tr id='Row" + i + "'>";
        str += "<td ><input id='TextOrderNumber" + i + "' readonly ='readonly' name='TextOrderNumber" + i + "' type='text' value='"+i+"' class='orderNo' /></td>";
        str += "<td><input id='TextQuestion" + i + "' name='TextQuestion" + i + "'  type='text' class='question' value='" + question + "' />";
        str += " <input id='btnDeleteQuestion" + i + "' type='button' value='Delete' onclick= \"DeleteQuestion(" + i + ")\"/>";
        str += "</td>";
        str += "<td style='display :none'><select id='SelectResponseAction" + i + "' style ='width:120px' name='SelectResponseAction" + i + "' value='"+responseAction+"'>";
        str += optionRespAct;
      
        str += "</select></td>";

        str += "<td><input id='TextResponse" + i + "' type='text' readonly ='readonly' name='TextResponse" + i + "' class='response' value='" + response + "'/></td>";
        str += "<td><input id='SelectFalgRating" + i + "'  readonly ='readonly' name='SelectFalgRating" + i + "' value='" + optionFlag + "'>";
        
        str += "</td>";
       
        str += "<td>";
       
        string strCheckBoxesID = "chkReverse" + i;
        if (reverse == 1)
        {
            str += " <input id='chkReverse" + i + "' name='chkReverse" + i + "' checked='checked' type='checkbox' onchange= \"ReverseValue('chkReverse" + i + "','SelectFalgRating" + i + "_'," + i + ")\"/>";
        }
        else
        {
            str += " <input id='chkReverse" + i + "' name='chkReverse" + i + "'  type='checkbox' onchange= \"ReverseValue('chkReverse" + i + "','SelectFalgRating" + i + "_'," + i + ")\"/>";
        }
                
        str += "</td>";
        str += "</tr>";


        return str;
    }

    private string AddSubRow(string i,string j,string response,int flag)
    {
        string optionFlag = "";

        if (flag == 1)
        {
          
            optionFlag += "1";
            //optionFlag += "<option selected='true' >1</option>";
           
        }
        else if (flag == 2)
        {
           
            //optionFlag += "<option selected='true'>2</option>";
            optionFlag += "2";
           
        }
        else if (flag == 3)
        {
           
            //optionFlag += "<option selected='true'>3</option>";
            optionFlag += "3";
            
        }
        else  if (flag == 4)
        {
            
            //optionFlag += "<option selected='true'>4</option>";
            optionFlag += "4";
            
        }
        else if (flag == 5)
        {
            
           // optionFlag += "<option selected='true'>5</option>";
            optionFlag += "5";
           
        }
        else if (flag == 6)
        {
           
            //optionFlag += "<option selected='true'>6</option>";
            optionFlag += "6";
        }

        string str = "";

        str += "<tr id='" + i + '_' + j + "' >&nbsp;</td><td>&nbsp;</td>  <td>&nbsp;</td>  <td><input id='TextResponse" + i + '_' + j + "' type='text' readonly ='readonly' name='TextResponse" + i + '_' + j + "' class='response' value='" + response + "'/></td>           <td><input id='SelectFalgRating" + i + '_' + j + "' readonly ='readonly' name='SelectFalgRating" + i + '_' + j + "' value='" + optionFlag + "'/></td><td>&nbsp;</td> </tr>";
        //str += "<tr id='" + i + '_' + j + "' >&nbsp;</td><td>&nbsp;</td>  <td>&nbsp;</td>  <td><input id='TextResponse" + i + '_' + j + "' readonly ='readonly' type='text' name='TextResponse" + i + '_' + j + "' class='response' value='" + response + "' value='" + flag + "'/></td>           <td><select id='SelectFalgRating" + i + '_' + j + "' name='SelectFalgRating" + i + '_' + j + "'> " + optionFlag + " </select></td><td>&nbsp;</td> </tr>";

        return str;
    }


    private void InitializeQuestion(int SOID)
    {
        string str = " <table>";
        //str += "<tr>       <td class='style2' >Order#</td> <td>Enter Question</td> <td>Response Action</td>             <td>Responses </td>       <td>Point</td>       <td>Reverse</td>       <td>&nbsp;</td>       </tr>";
        str += "<tr>       <td class='style2' >Order#</td> <td>Enter Question</td>              <td>Responses </td>       <td>Point</td>   <td>Reverse</td>       </tr>";
          try
            {
                Section s = new Section();
                s = s.GetSectionByOID(SOID);
                if (s != null)
                {
                    //TextBoxPassingTotal.Text = s.PassingTotal.ToString();
                    TextBoxSectionName.Text = s.SectionName;
                    TextBoxTotalQuestion.Text = s.TotalQuestion.ToString();
                    TextBoxFlag.Text = s.Flag.ToString();
                    TextBoxLow.Text = s.Low.ToString();
                    TextBoxMedium.Text = s.Medium.ToString();
                    Strong.InnerHtml = s.Medium.ToString();
                    //TextBoxhi.Text = s.High.ToString();
                    int i = 1;
                    int k=1;
                    foreach (Question q in s.QuestionList)
                    {
                        
                        //Code for question
                        int j = 1;
                       
                        foreach (QuestionResponse qr in q.QuestionRespList)
                        { 
                            //Code for Question Response
                            if (j == 1)
                            {
                                k=q.QuestionRespList.Count;
                                str += AddFullRow(i.ToString(), q.QuestionText,  qr.Response,k.ToString(),qr.FlagRating,q.RespAction,q.Reverse);
                            }
                            else
                            {
                                str += AddSubRow(i.ToString(), j.ToString(), qr.Response, qr.FlagRating);
                            }

                            j++;

                        }
                        i++;
                    }
                    HiddenRowId.Value = (i - 1).ToString();
                }
            }
            catch (Exception ex)
            { }

        str+="<tr id='RInd'><td colspan='8'>&nbsp;</td></tr>";
        str += "</table>";
        Div1.InnerHtml = str;
    }

    protected void ButtonSaveMyWork_Click(object sender, EventArgs e)
    {
        string ordNum, ques, RespAct, Multi, Resp, Flag, reverse =null;
        int assessmentOID,sectionOID,totalQuestion=0;
        assessmentOID = (Session["aid"] != null) ? Convert.ToInt32(Session["aid"]) : -1;
        sectionOID = (Request.QueryString["soid"] != null) ? Convert.ToInt32(Request.QueryString["soid"]) : -1;

        Section sectionExiting = null;
        Collection<Question> quesList = new Collection<Question>(); ;
        Collection<QuestionResponse> respList;//=new Collection<QuestionResponse>();
        Question questn;
        QuestionResponse quesResp;
        Section section = new Section();
        sectionExiting = section.GetSectionByOID(sectionOID);

        section.SectionOID = sectionExiting != null ? sectionExiting.SectionOID : 0;
        section.SectionName = TextBoxSectionName.Text;
        section.AssessmentOID = assessmentOID;
        section.LastModifiedBy = 1;
        section.CreatedBy = 1;
        section.FlagPointTotal = 0;
        section.PassingTotal = 0;
        //section.SectionOID = 0;
        section.TotalFlag = 0;
        section.TotalQuestion = 0;
        section.PassingTotal = 0;
        section.Flag = Convert.ToInt32(TextBoxFlag.Text);
        section.Low = Convert.ToInt32(TextBoxLow.Text);
        section.Medium = Convert.ToInt32(TextBoxMedium.Text);
        section.High = 67;

        totalQuestion = sectionExiting != null ? sectionExiting.QuestionList.Count : 0;
        
         
        #region Question

        for (int i = 1; ; i++)
        {
            ordNum = "TextOrderNumber" + i.ToString();
            ques = "TextQuestion" + i.ToString();
            RespAct = "SelectResponseAction" + i.ToString();
            Resp = "TextResponse" + i.ToString();
            Flag = "SelectFalgRating" + i.ToString();
            reverse = "chkReverse" + i.ToString();
            
            ordNum = Request.Form[ordNum];
            if (ordNum == null) break;
            ques = Request.Form[ques];
            RespAct = Request.Form[RespAct];
            Resp = Request.Form[Resp];
            Flag = Request.Form[Flag];
            reverse = Request.Form[reverse];

            //Assign Values to question
            questn = new Question();
            if (i > totalQuestion)
            {                
                questn.QuestionOID = 0;
            }
            else
            {
                questn.QuestionOID = sectionExiting.QuestionList[i-1].QuestionOID;
            }
            questn.SectionOID = section.SectionOID;
            questn.CreatedBy = 1;//Set current user
            questn.LastModifiedBy = 1;//Set current user
            questn.Keyword =" ";// Key;
            questn.MultipleAllow = 1;
            questn.OrderNo = Convert.ToInt32(ordNum);
            questn.QuestionText = ques;
            questn.RespAction = RespAct;
            int rev = 0;
            if (reverse == "on")
            {
                rev = 1;
            }
            questn.Reverse = rev;
            quesList.Add(questn);


            //Assign values Question Response
            respList = new Collection<QuestionResponse>();
            quesResp = new QuestionResponse();  
            if( questn.QuestionOID !=0)
            {
                quesResp.QuestionResponseOID = sectionExiting.QuestionList[i - 1].QuestionRespList[0].QuestionResponseOID;
            }
            quesResp.CreatedBy = 1;
            quesResp.LastModifiedBy = 1;
            quesResp.FlagRating = Convert.ToInt32(Flag);
            quesResp.Response = Resp;
            respList.Add(quesResp);

            //Process
            for (int j = 2; ; j++)
            {
                Resp = "TextResponse" + i.ToString() + "_" + j.ToString();
                Flag = "SelectFalgRating" + i.ToString() + "_" + j.ToString();

                Resp = Request.Form[Resp];
                Flag = Request.Form[Flag];
                if (Resp == null) break;

                //Assign values Question Response
                quesResp = new QuestionResponse();
                if (i > totalQuestion)
                {
                    quesResp.QuestionResponseOID = 0;
                }
                else
                {
                    if (j > sectionExiting.QuestionList[i-1].QuestionRespList.Count)
                    {
                        quesResp.QuestionResponseOID = 0;
                    }
                    else
                    {
                        quesResp.QuestionResponseOID = sectionExiting.QuestionList[i-1].QuestionRespList[j-1].QuestionResponseOID;
                    }
                }
                
                //quesResp.QuestionResponseOID = sectionExiting.QuestionList[i].QuestionRespList
                quesResp.CreatedBy = 1;
                quesResp.LastModifiedBy = 1;
                
                quesResp.FlagRating = Convert.ToInt32(Flag);
                quesResp.Response = Resp;
                respList.Add(quesResp);
            }

            questn.QuestionRespList = respList;

        }
        #endregion

        section.QuestionList = quesList;
        section.UpdateSection();
        section.UpdateAssessmentSection();

        this.InitializeQuestion(sectionOID);
        LabelMessage.Text = "Saved Successfully.";
    }

    #region OLD
    //private void InitializeQuestion(int soid)
    //{
    //    try
    //    {
    //        Section s = new Section();
    //        s = s.GetSectionByOID(soid);
    //        Label lblSect;
    //        Label lineBreak = new Label();
    //        lineBreak.Text = "<br />";
    //        lblSect = new Label();

    //        //Section
    //        //lblSect.Text = s.SectionName;
    //        // PlaceHolderMain.Controls.Add(lblSect);

    //        PlaceHolderMain.Controls.Add(InsertLineBreaks(1));
    //        //Each Question
    //        foreach (Question q in s.QuestionList)
    //        {
    //            // PlaceHolderMain.Controls.Add(InsertLineBreaks(1));
    //            lblSect = new Label();
    //            lblSect.Text = q.OrderNo + ". " + q.QuestionText;
    //            PlaceHolderMain.Controls.Add(lblSect);

    //            if (q.RespAction == "Radio Button")
    //            {
    //                //Panel p = new Panel();

    //                RadioButtonList rblist = new RadioButtonList();
    //                rblist.ID = q.QuestionOID.ToString();


    //                //Each Question Response
    //                foreach (QuestionResponse resp in q.QuestionRespList)
    //                {
    //                    rblist.Items.Add(resp.Response);
    //                }
    //                //p.Controls.Add(rblist);
    //                ///PlaceHolderMain.Controls.Add(InsertLineBreaks(1));
    //                PlaceHolderMain.Controls.Add(rblist);
    //            }
    //            else if (q.RespAction == "Check Box")
    //            {
    //                //Panel p = new Panel();
    //                CheckBoxList chlist = new CheckBoxList();
    //                chlist.ID = q.QuestionOID.ToString();

    //                //Each Question Response
    //                foreach (QuestionResponse resp in q.QuestionRespList)
    //                {
    //                    chlist.Items.Add(resp.Response);
    //                }
    //                //p.Controls.Add(chlist);
    //                //PlaceHolderMain.Controls.Add(InsertLineBreaks(1));
    //                PlaceHolderMain.Controls.Add(chlist);
    //            }

    //            else if (q.RespAction == "Drop Down")
    //            {
    //                //Panel p = new Panel();
    //                DropDownList ddl = new DropDownList();
    //                ddl.ID = q.QuestionOID.ToString();


    //                //Each Question Response
    //                foreach (QuestionResponse resp in q.QuestionRespList)
    //                {
    //                    ddl.Items.Add(resp.Response);
    //                }
    //                //p.Controls.Add(ddl);
    //                PlaceHolderMain.Controls.Add(ddl);
    //            }


    //        }
    //        PlaceHolderMain.Controls.Add(InsertLineBreaks(2));
    //    }
    //    catch (Exception ex)
    //    { }
    //}
    //private static Label InsertLineBreaks(int breaks)
    //{
    //    Label lblLineBreak = new Label();

    //    for (int i = 0; i < breaks; i++)
    //    {
    //        lblLineBreak.Text += "<br/>";
    //    }
    //    return lblLineBreak;
    //}
    //private string GetCharByInterger(int i)
    //{
    //    return Char.ConvertFromUtf32(i + 97);
    //}
    #endregion

    public System.Boolean IsNumeric(System.Object Expression)
    {
        if (Expression == null || Expression is DateTime)
            return false;

        if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
            return true;

        try
        {
            if (Expression is string)
                Double.Parse(Expression as string);
            else
                Double.Parse(Expression.ToString());
            return true;
        }
        catch { } // just dismiss errors but return false
        return false;
    }

    protected void ButtonRefresh_Click(object sender, EventArgs e)
    {
        int sectOID = Convert.ToInt32(Request.QueryString["soid"].ToString());
        //int sectOID = Convert.ToInt32(Session["soid"]);
        InitializeQuestion(sectOID);
        LabelMessage.Text = "";
    }

    protected void btnDeleteSection_Click(object sender, EventArgs e)
    {
        Sections sec = new Sections();
        Question ques=new Question ();
        Assessment ass = new Assessment();
        AnswerDetail ansDetail = new AnswerDetail();
        CVTCMenu menu=new CVTCMenu ();
        QuestionResponse qr=new QuestionResponse ();
        int sectOID = Convert.ToInt32(Request.QueryString["soid"].ToString());
        int AssOID = sec.GetAssessmentOIDBySectionOID(sectOID);
        if (ass.GetAssessmentStatusByOID(Convert.ToInt32(AssOID)) == 1)
        {
            LabelMessage.Text = "This group can not be deleted.It has been Locked";
            return;
        }
        else
        {
            string menuURL = "pg/assessment/section.aspx?soid=" + sectOID + "";
            //Delete from AnswerDetails and Question Table by SectionOID
            bool DeleteFromAnswerDetail=ansDetail.DeleteAnswerDetailBySectionOID(sectOID);
            Collection<Question> qList = new Collection<Question>();
            qList = ques.GetQuestionOIDBySectionOID(sectOID);
            foreach (Question q in qList )
            {
                qr.DeleteQuestionBySectionOID(q.QuestionOID);
            }
            bool DeleteFromQuestion = ques.DeleteQuestionBySectionOID(sectOID);
            bool DeleteFromMenu = menu.DeleteMenuByMenuURL(menuURL);
            bool DeleteFromSection = sec.DeleteSectionBySectionOID(sectOID);
            if (DeleteFromSection && DeleteFromMenu)
            {

                InitializeQuestion(sectOID);
                LabelMessage.Text = "This question group has been deleted successfully";
            }
            else
            {
                LabelMessage.Text = "This question group can not be deleted.Please try again..";
            }
            ButtonRefresh_Click(null, null);
        }
    }

   [WebMethod]
    public static void  DeleteQuestion( string OrderNo,string SectionID)
    {
       Question qu=new Question ();
       QuestionResponse qr = new QuestionResponse();
        int OrderNumber = Convert.ToInt32(OrderNo);
        int secID = Convert.ToInt32(SectionID);
        int QuestionOID = qu.GetQuestionOIDBySectionIDAndOrderNo(OrderNumber, secID);
        qr.DeleteQuestionBySectionOID(QuestionOID);
        if (qu.DeleteQuestionByQuestionOID(QuestionOID))
        {
            qr.UpdateQuestionResponseTotalQuestion(secID);
            Alert.Show("Successfully Deleted this question..");
            return;
        }
        else
        {
         
            Alert.Show("Not Deleted .try again..");
            return;
        }
    }

}
