using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;

public partial class pg_assessment_SectionEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
           
           string aid = Request.QueryString["aid"].ToString();
           Session["aid"] = aid;
          // Response.Write(aid);
        }
    }
    protected void ButtonSaveMyWork_Click(object sender, EventArgs e)
    {
        string ordNum = null, ques = null, RespAct = null, Resp = null, Flag = null,Reverse=null;
        int assessmentOID=0;
        Question questn;//=new Question();
        try
        {
          
            assessmentOID = (Session["aid"] != null) ? Convert.ToInt32(Session["aid"]) : -1;
            Collection<Question> quesList;// = new Collection<Question>(); 
            Collection<QuestionResponse> respList;//=new Collection<QuestionResponse>();
           
            QuestionResponse quesResp;
            Section section=new Section();
            section.SectionName = TextBoxSectionName.Text;
            section.AssessmentOID = assessmentOID;
            section.LastModifiedBy = 1;
            section.CreatedBy = 1;
            section.FlagPointTotal = 0;
            section.PassingTotal = 0;
            section.SectionOID = 0;
            section.TotalFlag = 0;
            section.TotalQuestion = 0;
            section.Flag =Convert.ToInt32( TextBoxFlag.Text);
            section.Low = Convert.ToInt32(TextBoxLow.Text);
            section.Medium = Convert.ToInt32(TextBoxMedium.Text);
            section.High = 67;

            
            
            #region Question
            
            //Loop For Questions
            quesList = new Collection<Question>();
           
            for(int i=1;;i+=6)
            {
                questn = new Question();
                ordNum = "TextOrderNumber" + i.ToString();
                
                if (ordNum==null )
                {
                    break;
                }
                ques = "TextQuestion" + i.ToString();
                RespAct = "SelectResponseAction" + i.ToString();
             
                //Loop For Question Responses

                respList = new Collection<QuestionResponse>();
                for (int j = 0; j < 6; j++)
                {
                    quesResp = new QuestionResponse();
                    Resp = "TextResponse" + (i+j).ToString();
                    Flag = "SelectFalgRating" + (i + j).ToString();
                    
                    //Get Value From Form
                    Resp = Request.Form[Resp];
                    Flag = Request.Form[Flag];
               
               
                //Assign values Question Response
               
                quesResp.CreatedBy = 1;
                quesResp.LastModifiedBy = 1;
                quesResp.FlagRating = Convert.ToInt32(Flag);
                quesResp.Response = Resp;
                respList.Add(quesResp);
                
                }


               
                ordNum = Request.Form[ordNum];
                if (ordNum == null) break;
                ques = Request.Form[ques];
                RespAct = Request.Form[RespAct];
               

                //Assign Values to question
               // quesList= new Collection<Question>(); 
                //questn = new Question();
                questn.CreatedBy = 1;//Set current user
                questn.LastModifiedBy = 1;//Set current user
                questn.Keyword = " ";
                questn.MultipleAllow = 1;
                questn.OrderNo = Convert.ToInt32(ordNum);
                questn.QuestionText = ques;
                questn.RespAction = RespAct;
                
                Reverse = Convert.ToString("chkReverse" + i.ToString());
                Reverse = Request.Form[Reverse];
                if (Reverse == "on")
                {
                    questn.Reverse = 1;
                }
                else
                {
                    questn.Reverse = 0;
                }
                quesList.Add(questn);
                questn.QuestionRespList = respList;
               

            }


               #region dummy
                ////Process
                //for (int j = 1; ; j++)
                //{
                //    Resp = "TextResponse" + i.ToString() + "_" + j.ToString();
                //    Flag = "SelectFalgRating" + i.ToString() + "_" + j.ToString();

                //    Resp = Request.Form[Resp];
                //    Flag = Request.Form[Flag];
                //    if (Resp == null) break;

                //    //Assign values Question Response
                //    quesResp = new QuestionResponse();
                //    quesResp.CreatedBy = 1;
                //    quesResp.LastModifiedBy = 1;
                //    quesResp.FlagRating = Convert.ToInt32(Flag);
                //    quesResp.Response = Resp;
                //    respList.Add(quesResp);
                //}
                #endregion


               

            
#endregion

            section.QuestionList = quesList;
            section.AddSection();
            section.UpdateAssessmentSection();
            TextBoxSectionName.Text = "";
            //TextBoxPassingTotal.Text = "0";
            TextBoxTotalQuestion.Text = "1";

            Assessment ass = new Assessment();
            ass = ass.GetAssessmentByOID(assessmentOID);

            CVTCMenu menu = new CVTCMenu();
            CVTCMenu tmp = menu.GetMenuByOID(ass.RefMenuID);
            menu.NameMenu = section.SectionName;
            
            int menuId = new CVTCMenu().GetMaxMenuID();
            menuId += 1;
            menu.MenuID = menuId;

            menu.URL = "pg/assessment/section.aspx?soid=" + section.SectionOID.ToString();
            menu.MenuLevel = 3;
            menu.Parent = tmp.MenuID+8;
            menu.IsExpanded = "true";
            menu.IsLeave = "true";

            menu.SaveAssessmentMenuItem(menu);

            LabelMessage.Text = "Saved Successfully.";
            
        }
        catch (Exception ex)
        { 
        
        }
    }
}
