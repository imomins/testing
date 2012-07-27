using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data.Odbc;

/// <summary>
/// Summary description for Answer
/// </summary>
public class Answer
{
    string connectionString;
	public Answer()
	{
        
		//
		// TODO: Add constructor logic here
		//
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();
	}
    
    #region Public Properties
    public int AnswerOID
    { get; set; }

    public int AssessmentOID
    { get; set; }

    public int StudentOID
    { get; set; }

    public string BannerID
    { get; set; }

    public int NumberOfPrinted
    { get; set; }

    public DateTime CreatedDate
    { get; set; }

    public int CreatedBy
    { get; set; }

    public Collection<AnswerDetail> AnswerDetailList
    { get; set; }

    #endregion

    #region Public Method

    public int AddAnswer()
    {
        bool result = false;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Answer_insert(?,?,?,?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                OdbcParameter returnParam = command.Parameters.Add("@AnswerOID", OdbcType.Int);
                //// set the direction flag so that it will be filled with the return value
                returnParam.Direction = ParameterDirection.ReturnValue;

                command.Parameters.AddWithValue("@AssessmentOID", this.AssessmentOID);
                command.Parameters.AddWithValue("@CreatedBy", this.CreatedBy);
                command.Parameters.AddWithValue("@NumberOfPrinted", this.NumberOfPrinted);
                command.Parameters.AddWithValue("@StudentOID", this.StudentOID);
                command.Parameters.AddWithValue("@BannerID", this.BannerID);
                command.Parameters.AddWithValue("@CreatedDate", this.CreatedDate);                
                connection.Open();
                int n = command.ExecuteNonQuery();
                if (n == 1)
                    result = true;
                else
                    result = false;
                AnswerOID = (int)command.Parameters["@AnswerOID"].Value;
                
                foreach (AnswerDetail ad in AnswerDetailList)
                {
                    ad.AnswerOID = AnswerOID;
                    ad.AddAnswerDetail(AnswerOID);
                }

            }
        }
        return AnswerOID;
    }

    public bool addAnswerDetails(int answerOID)
    {
        bool result = false;
        try
        {
            foreach (AnswerDetail ad in AnswerDetailList)
            {
                ad.AnswerOID = answerOID;
                ad.AddAnswerDetail(answerOID);
            }
            result = true;
        }
        catch (Exception ex)
        {
            result = false;
        }
        return result;
    }

    public Answer GetAnswerBySOIDAndAOID(int soid, int aoid)
    { 
        //Answer_BySOIDAndAOID
        Answer ans = null;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Answer_BySOIDAndAOID(?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@SOID", soid);
                command.Parameters.AddWithValue("@AOID", aoid);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {


                    if (dataReader.Read())
                    {
                        ans = new Answer();

                        ans.AnswerOID = Convert.ToInt32(dataReader["AnswerOID"]);
                        ans.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        ans.CreatedBy = Convert.ToInt32(dataReader["CreatedBy"]);
                        ans.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        ans.NumberOfPrinted = Convert.ToInt32(dataReader["NumberOfPrinted"]);
                        ans.StudentOID = Convert.ToInt32(dataReader["StudentOID"]);
                    }
                }

            }
        }
        return ans;

    }

    public Answer GetAnswerByStudentOIDAndAOID(int soid, int aoid)
    {
        //Answer_BySOIDAndAOID
        Answer ans = null;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Answer_ByStudentOIDAndAOID(?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@SOID", soid);
                command.Parameters.AddWithValue("@AOID", aoid);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {


                    if (dataReader.Read())
                    {
                        ans = new Answer();

                        ans.AnswerOID = Convert.ToInt32(dataReader["AnswerOID"]);
                        ans.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        ans.CreatedBy = Convert.ToInt32(dataReader["CreatedBy"]);
                        ans.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        ans.NumberOfPrinted = Convert.ToInt32(dataReader["NumberOfPrinted"]);
                        ans.StudentOID = Convert.ToInt32(dataReader["StudentOID"]);
                    }
                }

            }
        }
        return ans;

    }

    public Collection<Answer> GetAnswerByAOIDAndRLST(int aoid, int rlst)
    {
        //Answer_BySOIDAndAOID
        Collection<Answer> answers = new Collection<Answer>();
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Answer_ByRLSTAndAOID(?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@RLST", rlst);
                command.Parameters.AddWithValue("@AOID", aoid);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    Answer ans = null;                   
                    while(dataReader.Read())
                    {
                        ans = new Answer();
                        ans.AnswerOID = Convert.ToInt32(dataReader["AnswerOID"]);
                        ans.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        ans.CreatedBy = Convert.ToInt32(dataReader["CreatedBy"]);
                        ans.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        ans.NumberOfPrinted = Convert.ToInt32(dataReader["NumberOfPrinted"]);
                        ans.StudentOID = Convert.ToInt32(dataReader["StudentOID"]);
                        ans.BannerID = Convert.ToString(dataReader["BannerStudentIDNumber"]);
                        answers.Add(ans);
                    }
                }

            }
        }
        return answers;

    }

    public Collection<Answer> GetAnswerByAOIDAndRLST_ForPrintResultLetter(int aoid, int rlst)
    {
        //Answer_BySOIDAndAOID
        Collection<Answer> answers = new Collection<Answer>();
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Answer_ByRLSTAndAOID_ForResultLetter(?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@RLST", rlst);
                command.Parameters.AddWithValue("@AOID", aoid);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    Answer ans = null;
                    while (dataReader.Read())
                    {
                        ans = new Answer();
                        ans.AnswerOID = Convert.ToInt32(dataReader["AnswerOID"]);
                        ans.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        ans.CreatedBy = Convert.ToInt32(dataReader["CreatedBy"]);
                        ans.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        ans.NumberOfPrinted = Convert.ToInt32(dataReader["NumberOfPrinted"]);
                        ans.StudentOID = Convert.ToInt32(dataReader["StudentOID"]);
                        ans.BannerID = Convert.ToString(dataReader["BannerStudentIDNumber"]);
                        answers.Add(ans);
                    }
                }

            }
        }
        return answers;

    }

    public bool UpdateAnswer_ForNumberofPrinted(int aoid, int rlst,int SOID)
    {
        bool res = false;
        try
        {
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                using (OdbcCommand command = new OdbcCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "{CALL Answer_Update_NumberofPrinted(?,?,?)}";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RLST", rlst);
                    command.Parameters.AddWithValue("@AOID", aoid);
                    command.Parameters.AddWithValue("@StudentOID", SOID);
                    connection.Open();
                    int n = command.ExecuteNonQuery();
                    if (n == 1)
                    {
                        res = true;
                    }
                    else
                    {
                        res = false;
                    }

                }
            }


        }
        catch (Exception ax)
        {

        }
        return res;
    }

    public bool UpdateScoreDetailsTable_ForNumberofPrinted(int aoid, int rlst, int SOID)
    {
        bool res = false;
        try
        {
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                using (OdbcCommand command = new OdbcCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "{CALL ScoreDetails_Update_NumberofPrinted(?,?,?)}";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RLST", rlst);
                    command.Parameters.AddWithValue("@AOID", aoid);
                    command.Parameters.AddWithValue("@StudentOID", SOID);
                    connection.Open();
                    int n = command.ExecuteNonQuery();
                    if (n >= 1)
                    {
                        res = true;
                    }
                    else
                    {
                        res = false;
                    }

                }
            }


        }
        catch (Exception ax)
        {

        }
        return res;
    }


    public Answer GetAnswerByBannerID(string BannerID)
    {
        //Answer_BySOIDAndAOID
        Answer ans = null;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Answer_BySOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@BannerID", BannerID);
                
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {


                    if (dataReader.Read())
                    {
                        ans = new Answer();

                        ans.AnswerOID = Convert.ToInt32(dataReader["AnswerOID"]);
                        ans.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        ans.CreatedBy = Convert.ToInt32(dataReader["CreatedBy"]);
                        ans.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        ans.NumberOfPrinted = Convert.ToInt32(dataReader["NumberOfPrinted"]);
                        ans.StudentOID = Convert.ToInt32(dataReader["StudentOID"]);
                        ans.BannerID = Convert.ToString(dataReader["BannerStudentIDNumber"]);
                    }
                }

            }
        }
        return ans;

    }


    public Answer GetAnswerByStudentID(string BannerID)
    {
        //Answer_BySOIDAndAOID
        Answer ans = null;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Answer_BySOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@SOID", BannerID);

                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {


                    if (dataReader.Read())
                    {
                        ans = new Answer();

                        ans.AnswerOID = Convert.ToInt32(dataReader["AnswerOID"]);
                        ans.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        ans.CreatedBy = Convert.ToInt32(dataReader["CreatedBy"]);
                        ans.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        ans.NumberOfPrinted = Convert.ToInt32(dataReader["NumberOfPrinted"]);
                        ans.StudentOID = Convert.ToInt32(dataReader["StudentOID"]);
                        ans.BannerID = Convert.ToString(dataReader["BannerStudentIDNumber"]);
                    }
                }

            }
        }
        return ans;

    }

    public Answer GetAnswerByAssessmentandStudentID(int  AssOID,string  StuID)
    {
        //Answer_BySOIDAndAOID
        Answer ans = null;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Answer_BySOIDAndAOID(?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@SOID", StuID);
                command.Parameters.AddWithValue("@AOID", AssOID);

                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {


                    if (dataReader.Read())
                    {
                        ans = new Answer();

                        ans.AnswerOID = Convert.ToInt32(dataReader["AnswerOID"]);
                        ans.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        ans.CreatedBy = Convert.ToInt32(dataReader["CreatedBy"]);
                        ans.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        ans.NumberOfPrinted = Convert.ToInt32(dataReader["NumberOfPrinted"]);
                        ans.StudentOID = Convert.ToInt32(dataReader["StudentOID"]);
                        ans.BannerID = Convert.ToString(dataReader["BannerStudentIDNumber"]);
                    }
                }

            }
        }
        return ans;

    }

    public bool IsAnswerExist( int aoid, int StudentOID)
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Answer_ExistBySOIDAOID(?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                OdbcParameter returnParam = command.Parameters.Add("@ReturnValue", OdbcType.Int);
                //// set the direction flag so that it will be filled with the return value
                returnParam.Direction = ParameterDirection.ReturnValue;

                //Set Parameter Value                
                command.Parameters.AddWithValue("@AOID", Convert.ToInt32( aoid));
                //command.Parameters.AddWithValue("@SOID", Convert.ToInt32(soid));
                command.Parameters.AddWithValue("@StudentOID", Convert.ToInt32(StudentOID));
                //@StudentOID
                //Open connection
                connection.Open();
                //Read using reader
                command.ExecuteNonQuery();
                int count = (int)command.Parameters["@ReturnValue"].Value;
                if (count > 0) result = true;                        
                    
            }
        }
        return result;
    }

    public bool UpdateAnswer(int StudentOID,string BannerID)
    {
        bool res = false;
        try
        {
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                using (OdbcCommand command = new OdbcCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "{CALL Answer_Update(?,?)}";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@StudentOID", Convert.ToInt32(StudentOID));
                    command.Parameters.AddWithValue("@BannerID", Convert.ToString(BannerID));
                    connection.Open();
                    int n = command.ExecuteNonQuery();
                    if (n == 1)
                    {
                        res = true;
                    }
                    else
                    {
                        res = false ;
                    }

                }
            }
 
  
        }
        catch (Exception ax)
        { 
        
        }
        return res;
    }

    public bool UpdateScoreDetailTable(int StudentOID, string BannerID)
    {
        bool res = false;
        try
        {
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                using (OdbcCommand command = new OdbcCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "{CALL ScoreDetailTable_Update(?,?)}";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@StudentOID", Convert.ToInt32(StudentOID));
                    command.Parameters.AddWithValue("@BannerID", Convert.ToString(BannerID));
                    connection.Open();
                    int n = command.ExecuteNonQuery();
                    if (n == 1)
                    {
                        res = true;
                    }
                    else
                    {
                        res = false;
                    }

                }
            }


        }
        catch (Exception ax)
        {

        }
        return res;
    }


    public Answer CheckBannerID_Answer(string BannerID,int aoid)
    {
        //Answer_BySOIDAndAOID
        Answer ans = null;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Answer_CheckBannerID(?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@BannerID", BannerID);
                command.Parameters.AddWithValue("@AOID", aoid);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {


                    if (dataReader.Read())
                    {
                        ans = new Answer();

                        ans.AnswerOID = Convert.ToInt32(dataReader["AnswerOID"]);
                        ans.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        ans.CreatedBy = Convert.ToInt32(dataReader["CreatedBy"]);
                        if (dataReader["CreatedDate"] != null && dataReader["CreatedDate"]!=DBNull .Value )
                        {
                        ans.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        }
                        ans.NumberOfPrinted = Convert.ToInt32(dataReader["NumberOfPrinted"]);
                        ans.StudentOID = Convert.ToInt32(dataReader["StudentOID"]);
                        ans.BannerID = Convert.ToString(dataReader["BannerStudentIDNumber"]);
                    }
                }

            }
        }
        return ans;

    }

    public Collection<Answer> GetAOIDByStudentID(string  StudentID)
    {
        //Answer_BySOIDAndAOID
        Collection<Answer> answers = new Collection<Answer>();
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Answer_ByStudentID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@StudentID", StudentID);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    Answer ans = null;
                    while (dataReader.Read())
                    {
                        ans = new Answer();
                       
                        ans.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        
                        answers.Add(ans);
                    }
                }

            }
        }
        return answers;

    }

    #endregion


}

public class AnswerDetail
{
    string connectionString;
    public AnswerDetail()
    {
        
        //constructor logic here
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();
    }
    
    #region Public Properties
    public int AnswerDetailOID
    { get; set; }

    public int AnswerOID
    { get; set; }

    public int SectionOID
    { get; set; }

    public int QuestionOID
    { get; set; }

    public int AnswerPoint
    { get; set; }

    public string Response
    { get; set; }

    public DateTime CreatedDate
    { get; set; }

    public int CreatedBy
    { get; set; }
    #endregion

    #region Public Method

    public bool AddAnswerDetail(int answerOID)
    {
        bool result = false;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {                

                command.Connection = connection;
                command.CommandText = "{CALL AnswerDetail_insert(?,?,?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;


                //OdbcParameter returnParam = command.Parameters.Add("@ReturnValue", OdbcType.Int);
                //// set the direction flag so that it will be filled with the return value
                //returnParam.Direction = ParameterDirection.ReturnValue;


                command.Parameters.AddWithValue("@AnswerOID", answerOID);
                command.Parameters.AddWithValue("@SectionOID", this.SectionOID);
                command.Parameters.AddWithValue("@QuestionOID", this.QuestionOID);                
                command.Parameters.AddWithValue("@Response", this.Response);
                command.Parameters.AddWithValue("@AnswerPoint", this.AnswerPoint);
                command.Parameters.AddWithValue("@CreatedBy", this.CreatedBy);
                

                connection.Open();
                int n = command.ExecuteNonQuery();
                if (n == 1)
                    result = true;
                else
                    result = false;
                
            }
        }
        return result;
    }

    public bool DeleteAnswerDetailBySectionOID(int SectionOID)
    {
        bool result = false;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL AnswerDetail_DeleteBySectionOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@SectionOID", SectionOID);
                
                connection.Open();
                int n = command.ExecuteNonQuery();
                if (n == 1)
                    result = true;
                else
                    result = false;

            }
        }
        return result;
    }

    #endregion
}

public class StudentAnswer
{
    string connectionString;
    public StudentAnswer()
    {
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();
    }
    #region Public Properties
    
        public string StudentName
        { get; set; }

        public string BannerID
        { get; set; }

        public int SectionOID
        { get; set; }
        public int questionOID
        { get; set; }
        public string Response
        { get; set; }

        public int IsRight
        { get; set; }


    #endregion

    #region Public Method

        public Collection<StudentAnswer> GetStudentAnswerByAOID(int AOID)
        {
            Collection<StudentAnswer> answerList = new Collection<StudentAnswer>();
            StudentAnswer stdAns;
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                using (OdbcCommand command = new OdbcCommand())
                {

                    command.Connection = connection;
                    command.CommandText = "{CALL StudentRespWithAnswer_ByAOID(?)}";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@AOID",AOID);

                    connection.Open();

                    using (OdbcDataReader dataReader = command.ExecuteReader())
                    {
                        
                        while (dataReader.Read())
                        {
                            stdAns = new StudentAnswer();
                            stdAns.IsRight = Convert.ToInt32(dataReader["IsRight"]);
                            stdAns.questionOID = Convert.ToInt32(dataReader["questionOID"]);
                            stdAns.Response = Convert.ToString(dataReader["Response"]);
                            stdAns.SectionOID = Convert.ToInt32(dataReader["SectionOID"]);
                            stdAns.StudentName = Convert.ToString(dataReader["StudentName"]);
                            stdAns.BannerID = Convert.ToString(dataReader["BannerID"]);
                                
                            answerList.Add(stdAns);
                        }
                    }

                }
            }
            return answerList;
        }

    #endregion


}


