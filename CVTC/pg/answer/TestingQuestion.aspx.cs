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

public partial class pg_answer_TestingQuestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int aid = (Session["aoid"] != null) ? Convert.ToInt32(Session["aoid"].ToString()) : 0;
        if (!IsExist(aid))
        {
            //InitializeQuestion(sectOID);
            Initialize(aid);
        }
        else
        {
           // ButtonSubmit.Visible = false;
            Response.Write("You have already sit for this section");
        }
    }


    private void Initialize(int aoid)
    {
        try
        {
            Assessment ass = new Assessment();
            ass = ass.GetAssessmentByOID(aoid);

            foreach (Section s in ass.SectionList)
            {
                foreach (Question q in s.QuestionList)
                {
                    
                    if (q.RespAction == "Radio Button")
                    {
                        ;//For Radion Button
                    }
                    else if (q.RespAction == "Drop Down")
                    {
                        ;//For DropDown
                    }
                
                }
            
            }
        }
        catch (Exception ex)
        { }
    }
    private bool IsExist(int AOID)
    {
        bool result = false;

        try
        {
            Student student = (Student)(Session["currentStd"]);
            if (student != null)
            {
                Answer ans = new Answer();
                result = ans.IsAnswerExist(AOID, student.StudentOID);
            }
            //result = true;
        }
        catch (Exception ex)
        { }
        return result;
    }
}
