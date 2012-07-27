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
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;
using System.Web.Script.Serialization;


/// <summary>
/// Summary description for Assessment
/// </summary>
/// 
[Serializable]
public class Assessment
{
    
    

    //string connectionString = @"Data Source=.\SQLEXPRESS; Initial Catalog=cvtc; User ID=cvtcuser; Password=cvtcuser";
    string connectionString;

	public Assessment()
	{
		//
		// TODO: Add constructor logic here
		//
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();
    }
    #region Properties
    
    public int AssessmentOID
    { get; set; }   
    public string AssessmentName
    { get; set; }
    public int RefMenuID
    { get; set; }
    public int TotalQuestion
    { get; set; }
    public int TotalFlag
    { get; set; }
    public int TotalFlagPoint
    { get; set; }
    public int TotalSection
    { get; set; }
    public int CreatedBy
    { get; set; }
    public int LastModifiedBy
    { get; set; }
    public DateTime? CreatedDate
    { get; set; }
    public DateTime? LastModifiedDate
    { get; set; }
    public Collection<Section> SectionList
    { get; set; }
    public string RedirectURL
    { get; set; }
    public string Note
    { get; set; }
    public int Locked
    { get; set; }

    public string  LockStatus
    { get; set; }
    #endregion


    public bool AddAssessment()
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Assessment_insert(?,?,?,?,?,?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                OdbcParameter returnParam = command.Parameters.Add("@AOID", OdbcType.Int);
                //// set the direction flag so that it will be filled with the return value
                returnParam.Direction = ParameterDirection.ReturnValue;

                OdbcParameter paramAssessmentName = new OdbcParameter("@AssessmentName", OdbcType.VarChar, 100);
                paramAssessmentName.Value = this.AssessmentName;
                command.Parameters.Add(paramAssessmentName);

                OdbcParameter paramCreatedBy = new OdbcParameter("@CreatedBy", OdbcType.Int);
                paramCreatedBy.Value = this.CreatedBy;
                command.Parameters.Add(paramCreatedBy);

                //OdbcParameter paramCreatedDate = new OdbcParameter("@CreatedDate", OdbcType.DateTime);
                //paramCreatedDate.Value = this.CreatedDate;
                //command.Parameters.Add(paramCreatedDate);

                OdbcParameter paramLastModifiedBy = new OdbcParameter("@LastModifiedBy", OdbcType.Int);
                paramLastModifiedBy.Value = this.LastModifiedBy;
                command.Parameters.Add(paramLastModifiedBy);

                //OdbcParameter paramLastModifiedDate = new OdbcParameter("@LastModifiedDate", OdbcType.DateTime);
                //paramLastModifiedDate.Value = this.LastModifiedDate;
                //command.Parameters.Add(paramLastModifiedDate);

                OdbcParameter paramRefMenuID = new OdbcParameter("@RefMenuID", OdbcType.Int);
                paramRefMenuID.Value = this.RefMenuID;
                command.Parameters.Add(paramRefMenuID);

                OdbcParameter paramTotalFlag = new OdbcParameter("@TotalFlag", OdbcType.Int);
                paramTotalFlag.Value = this.TotalFlag;
                command.Parameters.Add(paramTotalFlag);

                OdbcParameter paramTotalFlagPoint = new OdbcParameter("@TotalFlagPoint", OdbcType.Int);
                paramTotalFlagPoint.Value = this.TotalFlagPoint;
                command.Parameters.Add(paramTotalFlagPoint);

                OdbcParameter paramTotalQuestion = new OdbcParameter("@TotalQuestion", OdbcType.Int);
                paramTotalQuestion.Value = this.TotalQuestion;
                command.Parameters.Add(paramTotalQuestion);

                OdbcParameter paramTotalSection = new OdbcParameter("@TotalSection", OdbcType.Int);
                paramTotalSection.Value = this.TotalSection;
                command.Parameters.Add(paramTotalSection);

                
                connection.Open();
                int n = command.ExecuteNonQuery();
                //command.CommandText = "Assessment_RefUpdate";
                //command.ExecuteNonQuery();
                if (n == 1)
                    result = true;
                else
                    result = false;
                this.AssessmentOID = (int)command.Parameters["@AOID"].Value;
            }
        }

        return result;
    }

    public bool UpdateAssessmentRef()
    { 
         bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "Assessment_RefUpdate";
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                int n = command.ExecuteNonQuery();                
                
                if (n !=0 )
                    result = true;
                else
                    result = false;
            }
        }

        return result;
    }

    public int GetAssessmentOIDByAssessmentName(string  AssName)
    {
        //Assessment ass = null;
        int strAssOID = 0;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL AssessmentOIDBY_NMAE(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Parameter Setting
                command.Parameters.AddWithValue("@AssName", AssName);

                connection.Open();

                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    Section sect = new Section();
                    if (dataReader.Read())
                    {
                        strAssOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                    }
                }

            }
        }

        return strAssOID;
    }


    public Assessment GetAssessmentByOID(int OID)
    {
        //Assessment ass = null;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Assessment_BYOID(?)}";
                command.CommandType = CommandType.StoredProcedure;
                                               
                //Parameter Setting
                command.Parameters.AddWithValue("@AOID", OID);
                
                connection.Open();                
                
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    Section sect = new Section();
                    if (dataReader.Read())
                    {
                        //this = new Assessment();
                        this.AssessmentName = Convert.ToString(dataReader["AssessmentName"]);
                        this.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        this.CreatedBy = Convert.ToInt32(dataReader["CreatedBy"]);
                        this.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        this.LastModifiedBy = Convert.ToInt32(dataReader["LastModifiedBy"]);
                        this.LastModifiedDate = Convert.ToDateTime(dataReader["LastModifiedDate"]);
                        this.RefMenuID = Convert.ToInt32(dataReader["RefMenuID"]);
                        this.TotalFlag = Convert.ToInt32(dataReader["TotalFlag"]);
                        this.TotalFlagPoint = Convert.ToInt32(dataReader["TotalFlagPoint"]);
                        this.TotalQuestion = Convert.ToInt32(dataReader["TotalQuestion"]);
                        this.TotalSection = Convert.ToInt32(dataReader["TotalSection"]);
                        this.SectionList = sect.GetSectionByAssessmentOID_ResultEmail(this.AssessmentOID);
                    }
                }
                
            }
        }

        return this;
    }

    public Assessment GetAssessmentByOID_QuestionSheet(int OID)
    {
        //Assessment ass = null;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Assessment_BYOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Parameter Setting
                command.Parameters.AddWithValue("@AOID", OID);

                connection.Open();

                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    Section sect = new Section();
                    if (dataReader.Read())
                    {
                        //this = new Assessment();
                        this.AssessmentName = Convert.ToString(dataReader["AssessmentName"]);
                        this.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        this.CreatedBy = Convert.ToInt32(dataReader["CreatedBy"]);
                        this.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        this.LastModifiedBy = Convert.ToInt32(dataReader["LastModifiedBy"]);
                        this.LastModifiedDate = Convert.ToDateTime(dataReader["LastModifiedDate"]);
                        this.RefMenuID = Convert.ToInt32(dataReader["RefMenuID"]);
                        this.TotalFlag = Convert.ToInt32(dataReader["TotalFlag"]);
                        this.TotalFlagPoint = Convert.ToInt32(dataReader["TotalFlagPoint"]);
                        this.TotalQuestion = Convert.ToInt32(dataReader["TotalQuestion"]);
                        this.TotalSection = Convert.ToInt32(dataReader["TotalSection"]);
                        this.SectionList = sect.GetSectionByAssessmentOID(this.AssessmentOID);
                    }
                }

            }
        }

        return this;
    }


    public Collection<Assessment> GetAssessmentByAssessmentOID(int OID)
    {
        //Assessment ass = null;
        Collection<Assessment> assList = new Collection<Assessment>();
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Assessment_BYOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Parameter Setting
                command.Parameters.AddWithValue("@AOID", OID);

                connection.Open();

                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    Section sect = new Section();
                    if (dataReader.Read())
                    {
                       
                        this.AssessmentName = Convert.ToString(dataReader["AssessmentName"]);
                        this.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        this.CreatedBy = Convert.ToInt32(dataReader["CreatedBy"]);
                        this.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        this.LastModifiedBy = Convert.ToInt32(dataReader["LastModifiedBy"]);
                        this.LastModifiedDate = Convert.ToDateTime(dataReader["LastModifiedDate"]);
                        this.RefMenuID = Convert.ToInt32(dataReader["RefMenuID"]);
                        this.TotalFlag = Convert.ToInt32(dataReader["TotalFlag"]);
                        this.TotalFlagPoint = Convert.ToInt32(dataReader["TotalFlagPoint"]);
                        this.TotalQuestion = Convert.ToInt32(dataReader["TotalQuestion"]);
                        this.TotalSection = Convert.ToInt32(dataReader["TotalSection"]);
                        assList.Add(this);
                    }
                }

            }
        }

        return assList;
    }


    public int   GetAnswerOIDByAssessmentOID(int AOID)
    {
        //Assessment ass = null;
        int strAnsOID = 0;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL AnsOID_AssOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Parameter Setting
                command.Parameters.AddWithValue("@AOID", AOID);

                connection.Open();

                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    Section sect = new Section();
                    if (dataReader.Read())
                    {
                        strAnsOID = Convert.ToInt32(dataReader["AnswerOID"]); 
                    }
                }

            }
        }

        return strAnsOID;
    }

    public Collection<Assessment> GetAllAssessmnet()
    {
        Collection<Assessment> assList = new Collection<Assessment>();
        Assessment assessment;
        try
        { 
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "Assessment_GetAll";
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    Section sect = new Section();
                    while (dataReader.Read())
                    {//this = new Assessment();
                        assessment = new Assessment();
                        assessment.AssessmentName = Convert.ToString(dataReader["AssessmentName"]);
                        assessment.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        assessment.CreatedBy = Convert.ToInt32(dataReader["CreatedBy"]);
                        assessment.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        assessment.LastModifiedBy = Convert.ToInt32(dataReader["LastModifiedBy"]);
                        assessment.LastModifiedDate = Convert.ToDateTime(dataReader["LastModifiedDate"]);
                        assessment.RefMenuID = Convert.ToInt32(dataReader["RefMenuID"]);
                        assessment.TotalFlag = Convert.ToInt32(dataReader["TotalFlag"]);
                        assessment.TotalFlagPoint = Convert.ToInt32(dataReader["TotalFlagPoint"]);
                        assessment.TotalQuestion = Convert.ToInt32(dataReader["TotalQuestion"]);
                        assessment.TotalSection = Convert.ToInt32(dataReader["TotalSection"]);
                        assessment.Locked = Convert.ToInt32(dataReader["Locked"]);
                        assessment.LockStatus = (assessment.Locked == 1) ? "Locked" : "Unlocked";
                        //assessment.SectionList = sect.GetSectionByAssessmentOID(this.AssessmentOID);
                        assList.Add(assessment);
                    }
                }

            }
        }
                  
        }
        catch (Exception ex)
        { }

        return assList;
    }

    public bool SentLetterEmail(string from, string to, string subject, string body)
    {
        bool result = false;
        try
        {
            MailHelper.SendMailWithCredential(from,to,subject,body);
        }
        catch (Exception ex)
        { }
        return result;

    }

    public bool SendLetterEmail()
    {
        bool result = false;
        try
        {
            StringBuilder html = new StringBuilder();
            ResultLetter resultLetter = new ResultLetter();
            resultLetter = resultLetter.GetResultLetterByAOID(this.AssessmentOID);
            if (resultLetter != null)
            {
                html.Append("<table border='0' cellspacing='0' cellpadding='0'><tr>    <td bgcolor='#000000'>");
                html.Append("<table bgcolor='#000000' cellspacing='1' cellpadding='0'  >");
                html.Append("<tr>");
                html.Append("<td bgcolor='#FFFFFF'>Category</td><td bgcolor='#FFFFFF'>&nbsp;</td>");
                html.Append("</tr>");
                //Section Detail
                foreach(ResultLetterDetail ld in resultLetter.LetterDetail)
                {
                    html.Append("<tr>");
                    html.Append("<td>" + ld.SectionOID.ToString() + "</td>" + "<td>" + ld.SectionDefinition + "</td>");

                    html.Append("</tr>");
                }

                //html.Append("</tr>");
                //END third
                html.Append("</table>");
                html.Append(" </td></tr></table>");
            }
        }
        catch (Exception ex)
        { }
        return result;
    }

    public bool SendResultEmail()
    {

        bool result = false;

        return result;
    }

    //public string GetAssessmentResult(int AOID, string numberOfRows, string pageIndex, out int totalRecords, string sortColumnName, string sortOrderBy, string bannerID, string program, string studentName, string flag, string numberOfPrinted)
    //public string GetAssessmentResult(int AOID, string numberOfRows, string pageIndex, out int totalRecords, string sortColumnName, string sortOrderBy, string bannerID, string program, string studentName,string searchingField,string searchngValue)
    public string GetAssessmentResult(int AOID, string numberOfRows, string pageIndex, out int totalRecords, string searchColumnName, string searchVal, string sortColumnName, string sortOrderBy, string bannerID, string program, string studentName,string SortOrSearchFlag)
    {
        try
        {
            string MultipleSearch = "Yes";
            searchColumnName = (searchColumnName == null) ? "" : searchColumnName;
            sortOrderBy = (sortOrderBy == null) ? "" : sortOrderBy;
            bannerID = (bannerID == null) ? "" : bannerID;
            program = (program == null) ? "" : program;
            studentName = (studentName == null) ? "" : studentName;
            //flag  = (flag  == null) ? "" : flag ;
            //numberOfPrinted = (numberOfPrinted == null) ? "" : numberOfPrinted;
            //string sortingVal = "";
            totalRecords = 0;            
            //int numberOfRows=10,  pageIndex=1,  totalRecords=10;
            int Start = (Convert.ToInt32(pageIndex) - 1) * Convert.ToInt32(numberOfRows);
            int Last = Convert.ToInt32(pageIndex) * Convert.ToInt32(numberOfRows);
            JQGridResults result = new JQGridResults();
            List<JQGridRow> rows = new List<JQGridRow>();

            #region Get Flag
            ////To get FlagRating
            DataTable flagDt = new DataTable();
            flagDt.Columns.Add("StudentOID", typeof(string));
            flagDt.Columns.Add("FlagRating", typeof(string));
            flagDt.Columns.Add("NumberOfPrinted", typeof(string));

            DataRow dr = null;

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                using (OdbcCommand command = new OdbcCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "{CALL GetNoOfFlagByAOID1(?)}";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AOID", AOID);

                    connection.Open();

                    using (OdbcDataReader dataReader = command.ExecuteReader())
                    {

                        while (dataReader.Read())
                        {
                            dr = flagDt.NewRow();
                            dr["StudentOID"] = Convert.ToString(dataReader["StudentOID"]);
                            dr["FlagRating"] = Convert.ToString(dataReader["FlagRating"]);
                            dr["NumberOfPrinted"] = Convert.ToString(dataReader["NumberOfPrinted"]);
                            flagDt.Rows.Add(dr);
                        }
                        #region Unused
                        //while (dataReader.Read())
                        //{
                        //    dr = flagDt.NewRow();
                        //    dr["StudentOID"] = Convert.ToString(dataReader["StudentOID"]);
                        //    dr["FlagRating"] = Convert.ToString(dataReader["FlagRating"]);
                        //    //flagDt.Rows.Add(dr);
                        //    //int flag = 0;
                        //    //for (int i = 0; i < flagDt.Rows.Count; i++)
                        //    //{
                        //    //    //if (dr["StudentOID"] == flagDt.Rows[i]["StudentOID"] && dr["AssessmentOID"] == flagDt.Rows[i]["AssessmentOID"])
                        //    //    string sb =Convert .ToString ( flagDt.Rows[i]["StudentOID"]);
                        //    //    if (Convert.ToString(dr["StudentOID"]) == sb)
                        //    //    {
                                    
                        //    //        flag = 1;
                        //    //        int updatedVal = Convert.ToInt32(dataReader["FlagRating"]) + Convert.ToInt32(flagDt.Rows[i]["FlagRating"]);
                        //    //        flagDt.Rows[i]["FlagRating"] = updatedVal;
                                    
                        //    //    }
                        //    //}
                        //    //if (flag == 0)
                        //    //{
                        //    //    flagDt.Rows.Add(dr);
                        //    //}
                        //    //else
                        //    //{
                        //    //    //Do Something
                        //    //}


                        //}
                        #endregion
                    }
                }
            }
            //////
            #endregion

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                using (OdbcCommand command = new OdbcCommand())
                {

                    command.Connection = connection;
                    command.CommandText = "{CALL Assessement_ResultByAOID(?,?,?,?,?,?,?,?,?,?,?,?,?)}";
                    command.CommandType = CommandType.StoredProcedure;

                    //OdbcParameter returnParam = command.Parameters.Add("@totalRecords", OdbcType.Int);
                    ////// set the direction flag so that it will be filled with the return value
                    //returnParam.Direction = ParameterDirection.ReturnValue;


                    if (searchColumnName == "" || searchColumnName == null || searchColumnName == "StudentOID")
                    {
                        searchColumnName = "BannerID";
                    }
                    if (searchVal == "" || searchVal == null)
                    {
                        searchVal = "@";
                    }

                    if (sortColumnName == "" || sortColumnName == null || sortColumnName == "StudentOID")
                    {
                        sortColumnName = "BannerID";
                    }

                    //Parameter Setting

                    OdbcParameter paramPageIndex = new OdbcParameter("@PageIndex", OdbcType.Int);
                    paramPageIndex.Value = Convert.ToInt32(pageIndex);
                    command.Parameters.Add(paramPageIndex);
                     
                    OdbcParameter paramNumberOfRows = new OdbcParameter("@NumberOfRows", OdbcType.Int);
                    paramNumberOfRows.Value = Convert.ToInt32(numberOfRows);
                    command.Parameters.Add(paramNumberOfRows);

                    OdbcParameter paramTotalRecords = new OdbcParameter("@TotalRecords", OdbcType.Int);
                    totalRecords = 0;
                    paramTotalRecords.Value = totalRecords;
                    paramTotalRecords.Direction = ParameterDirection.Output;
                    command.Parameters.Add(paramTotalRecords);

                    command.Parameters.AddWithValue("@AOID", AOID);
                    command.Parameters.AddWithValue("@SearchColumnName", searchColumnName+",");
                    command.Parameters.AddWithValue("@SortOrderBy", sortOrderBy);
                    command.Parameters.AddWithValue("@BannerID", bannerID);
                    command.Parameters.AddWithValue("@Program", program);
                    command.Parameters.AddWithValue("@StudentName", studentName);
                    command.Parameters.AddWithValue("@SearchValue", searchVal+",");
                    command.Parameters.AddWithValue("@SortColumnName", sortColumnName);
                    command.Parameters.AddWithValue("@SortOrSearchFlag", SortOrSearchFlag);
                    command.Parameters.AddWithValue("@MultiCoulumn", MultipleSearch);
                    connection.Open();

                    using (OdbcDataReader dataReader = command.ExecuteReader())
                    {
                       
                        JQGridRow row;
                        //..dataReader.FieldCount
                        while (dataReader.Read())
                        {
                                //string SectionRank = null;
                                row = new JQGridRow();
                                row.cell = new string[dataReader.FieldCount];

                                for (int j = 0; j < dataReader.FieldCount; j++)
                                {
                                    row.cell[j] = Convert.ToString(dataReader[j]);

                                    if (dataReader[j].ToString ().Contains (','))
                                    {
                                        row.cell[j] =dataReader[j].ToString ().Replace (',',' ');
                                    }

                                    if (j == 5)
                                    {
                                        string SOID = Convert.ToString(dataReader[j]);
                                        //row.cell[j] = GetNoOfFlagByAOIDANDSOID(AOID, Convert.ToInt32(SOID)).ToString();
                                        row.cell[j] = "0";
                                        for (int k = 0; k < flagDt.Rows.Count; k++)
                                        {
                                            if (Convert.ToString(flagDt.Rows[k]["StudentOID"]) == SOID)
                                            {
                                                row.cell[j] = Convert.ToString(flagDt.Rows[k]["FlagRating"]);
                                                break;
                                            }
                                        }
                                    }
                                    if (j==6)
                                    {
                                        string SOID = Convert.ToString(dataReader[j-1]);
                                        row.cell[j] = "0";
                                        for (int k = 0; k < flagDt.Rows.Count; k++)
                                        {
                                            if (Convert.ToString(flagDt.Rows[k]["StudentOID"]) == SOID)
                                            {
                                                row.cell[j] = Convert.ToString(flagDt.Rows[k]["NumberOfPrinted"]);
                                                break;
                                            }
                                        }
                                    }
                                }
                                
                                rows.Add(row);
                            //}
                                //totalRecords++;
                        }
//                        totalRecords = (command.Parameters["@totalRecords"].Value==null)?0:(int)command.Parameters["@totalRecords"].Value;
                    }
                    totalRecords = (int)paramTotalRecords.Value;
                }
            }
            //totalRecords = GetRowCount();

            result.rows = rows.ToArray();
            result.page = Convert.ToInt32(pageIndex);
            result.total = totalRecords /Convert.ToInt32( numberOfRows);
            if (totalRecords % Convert.ToInt32(numberOfRows) != 0) result.total += 1;
            result.records = totalRecords;
            return new JavaScriptSerializer().Serialize(result);
        }
        catch (Exception ex)
        {
            totalRecords = 20;
            return "";
        }
    }

    public int GetNoOfFlagByAOIDANDSOID(int AOID, int SOID)
    {
        int totalRow = 0;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                command.Connection = connection;
                command.CommandText = "{CALL GetNoOfFlagByAOIDANDSOID(?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@AOID", AOID);
                command.Parameters.AddWithValue("@SOID", SOID);
                
                OdbcParameter returnParam = command.Parameters.Add("@Counter", OdbcType.Int);
                returnParam.Direction = ParameterDirection.ReturnValue;
                
                connection.Open();
                command.ExecuteNonQuery();

                totalRow = (int)command.Parameters["@Counter"].Value;

            }
        }
        return totalRow;
    }


    public int GetRowCount()
    {
        int totalRow = 0;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                command.Connection = connection;
                command.CommandText = "{CALL GetRowCount(?)}";
                command.CommandType = CommandType.StoredProcedure;

                OdbcParameter returnParam = command.Parameters.Add("@totalRecords", OdbcType.Int);
                ////// set the direction flag so that it will be filled with the return value
                returnParam.Direction = ParameterDirection.ReturnValue;
                connection.Open();
                command.ExecuteNonQuery();
                
                totalRow = (int)command.Parameters["@totalRecords"].Value;
                
            }
        }
        return totalRow;
    }

    public string GetColumnNameList(int AOID)
    {
        string ColumnNameList = " ";
        string[] Columns = null;
        try
        {                  
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                using (OdbcCommand command = new OdbcCommand())
                {

                    command.Connection = connection;
                    //command.CommandText = "{CALL Assessement_ResultByAOID(?)}";
                    command.CommandText = "{CALL Assessement_ColumnByAOID(?)}";
                    command.CommandType = CommandType.StoredProcedure;

                    //Parameter Setting
                    command.Parameters.AddWithValue("@AOID", AOID);

                    connection.Open();

                    using (OdbcDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            ColumnNameList = Convert.ToString(dataReader["colName"]);

                        }
                        //if (ColumnNameList.Contains(','))
                        //{
                            
                        //    Columns = ColumnNameList.Split(',');

                        //}
                        //string temp = "";
                        //for (int i = 0; i < Columns.Length;i++ )
                        //{
                        //    if (Columns[i].Contains(' '))
                        //    {
                        //    Columns[i]=Columns[i].Replace (' ','_');
                        //    }
                        //    if (Columns[i].Contains(':'))
                        //    {
                        //        Columns[i] = Columns[i].Replace(':', '_');
                        //    }
                        //    if (Columns[i].Contains('-'))
                        //    {
                        //        Columns[i] = Columns[i].Replace('-', '_');
                        //    }
                        //    temp = temp + Columns[i]+",";
                            
                        //}
                        //temp = temp.Substring(0, temp.Length - 1);
                        //ColumnNameList = temp;
                                                
                    }
                }
            }
            
        }
        catch (Exception ex)
        {            
            
        }

        return ColumnNameList;
    }


    public Collection<Assessment> GetAssessmentBySOID(string BannerID)
    {
        //Collection<Student> studentList = new Collection<Student>();
        //Student student = null;
        Collection<Assessment> assList = new Collection<Assessment>();
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Assessment_BYSOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@SOID", BannerID);
                //Open connection
                connection.Open();
                //Read using reader

                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    Assessment a;
                    //Question q = new Question();
                    //dataReader.Read();
                    assList.Clear();
                    while (dataReader.Read())
                    {
                        //student = new Student();
                        a = new Assessment();
                        a.AssessmentName = Convert.ToString(dataReader["AssessmentName"]);
                        a.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        assList.Add(a);

                    }
                }

            }
        }
        return assList;
    }

    public int GetAssessmentStatusByOID(int AOID)
    {
        //Assessment ass = null;
        int ReturnVal = 0;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Assessment_BYOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Parameter Setting
                command.Parameters.AddWithValue("@AOID", AOID);

                connection.Open();

                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    Section sect = new Section();
                    if (dataReader.Read())
                    {
                        ReturnVal = Convert.ToInt32(dataReader["Locked"]);
                    }
                }

            }
        }

        return ReturnVal;
    }

    public bool UpdateAssessmentStatus( int AOID)
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Assessment_UpdateStatus(?)}";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@AOID", AOID);
                connection.Open();
                int n = command.ExecuteNonQuery();

                if (n != 0)
                    result = true;
                else
                    result = false;
            }
        }

        return result;
    }

    public bool DisableAssessmentStatus(int AOID)
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Assessment_DisableStatus(?)}";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@AOID", AOID);
                connection.Open();
                int n = command.ExecuteNonQuery();

                if (n != 0)
                    result = true;
                else
                    result = false;
            }
        }

        return result;
    }

    public bool DisableAssessmentMenu(int AOID)
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Assessment_DisableStatus(?)}";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@AOID", AOID);
                connection.Open();
                int n = command.ExecuteNonQuery();

                if (n != 0)
                    result = true;
                else
                    result = false;
            }
        }

        return result;
    }

}

[Serializable]
public class RiskCalculation
{
    string connectionString;
    public RiskCalculation()
    {
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();
        
    }

    #region Public Properties
    //RiskOID, AssessmentOID, NumSection, NumFlag, CreatedDate, CreatedBy
    public int RiskOID    
    { get; set; }
    public string RiskName
    { get; set; }
    public int AssessmentOID
    { get; set; }
    public int NumSection
    { get; set; }
    public int NumFlag
    { get; set; }
    public DateTime CreatedDate
    { get; set; }
    public int CreatedBy
    { get; set; }
    #endregion

    #region Public Method

    public bool AddRiskCalculation()
    { 
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {                

                command.Connection = connection;
                command.CommandText = "{CALL Risk_insert(?,?,?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;


                OdbcParameter returnParam = command.Parameters.Add("@RiskOID", OdbcType.Int);
                // set the direction flag so that it will be filled with the return value
                returnParam.Direction = ParameterDirection.ReturnValue;

                //command.Parameters.AddWithValue("@SectionOID",this.SectionOID);@RiskName
                
                command.Parameters.AddWithValue("@RiskName", this.RiskName);
                command.Parameters.AddWithValue("@AssessmentOID", this.AssessmentOID);                
                command.Parameters.AddWithValue("@NumSection", this.NumSection);
                command.Parameters.AddWithValue("@NumFlag", this.NumFlag);
                command.Parameters.AddWithValue("@CreatedBy", this.CreatedBy);
                

                connection.Open();
                int n = command.ExecuteNonQuery();
                if (n == 1)
                    result = true;
                else
                    result = false;
                RiskOID = (int)command.Parameters["@RiskOID"].Value;
            }
        }
        return result;
    }

    public bool EditRiskCalculation(int oid,int numSect, int Numflg)
    { 
        bool result = false;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {                

                command.Connection = connection;
                command.CommandText = "{CALL Risk_Update(?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@OID", oid);
                command.Parameters.AddWithValue("@NumSection", numSect);
                command.Parameters.AddWithValue("@NumFlag", Numflg);

                //Open Connection
                connection.Open();

                //Execute Stored Procedure
                int n = command.ExecuteNonQuery();
                if (n == 1)
                    result = true;
                else
                    result = false;                
            }
        }
        return result;


    }

    public Collection<RiskCalculation> GetRiskCalulationByAOID(int aoid)
    {
        Collection<RiskCalculation> list = new Collection<RiskCalculation>();
        RiskCalculation risk = null;

        try
        {
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                using (OdbcCommand command = new OdbcCommand())
                {

                    command.Connection = connection;
                    command.CommandText = "{CALL Risk_GetByAOID(?)}";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@AOID", aoid);
                    connection.Open();

                    using (OdbcDataReader dataReader = command.ExecuteReader())
                    {
                        
                        while (dataReader.Read())
                        {//this = new Assessment();
                            risk = new RiskCalculation();
                            risk.RiskOID = Convert.ToInt32(dataReader["RiskOID"]);
                            risk.NumFlag = Convert.ToInt32(dataReader["NumFlag"]);
                            risk.NumSection = Convert.ToInt32(dataReader["NumSection"]);
                            risk.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                            risk.CreatedBy = Convert.ToInt32(dataReader["CreatedBy"]);
                            risk.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                            risk.RiskName = Convert.ToString(dataReader["RiskName"]);

                            list.Add(risk);
                        }
                    }

                }
            }

        }
        catch (Exception ex)
        { }


        return list;
    }

    public RiskCalculation GetAssessmentOIDByRiskOID(int RiskOID)
    {
        
        RiskCalculation risk = null;

        try
        {
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                using (OdbcCommand command = new OdbcCommand())
                {

                    command.Connection = connection;
                    command.CommandText = "{CALL Risk_GetByOID(?)}";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ROID", RiskOID);
                    connection.Open();

                    using (OdbcDataReader dataReader = command.ExecuteReader())
                    {

                        while (dataReader.Read())
                        {//this = new Assessment();
                            risk = new RiskCalculation();
                            risk.RiskOID = Convert.ToInt32(dataReader["RiskOID"]);
                            risk.NumFlag = Convert.ToInt32(dataReader["NumFlag"]);
                            risk.NumSection = Convert.ToInt32(dataReader["NumSection"]);
                            risk.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                            risk.CreatedBy = Convert.ToInt32(dataReader["CreatedBy"]);
                            risk.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                            risk.RiskName = Convert.ToString(dataReader["RiskName"]);

                           // list.Add(risk);
                        }
                    }

                }
            }

        }
        catch (Exception ex)
        { }


        return risk;
    }


    public RiskCalculation GetRiskCalculationByAOIDAndSOID(int AOID, int SOID)
    {
        RiskCalculation riskCalculation = null;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL GetRiskCalculationByAOIDANDSOID(?,?)}";
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 600;
                //Set Parameter Value
                command.Parameters.AddWithValue("@AOID", AOID);
                command.Parameters.AddWithValue("@SOID", SOID);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        riskCalculation = new RiskCalculation();

                        riskCalculation.RiskOID = Convert.ToInt32(dataReader["RiskOID"]);
                        riskCalculation.RiskName = dataReader["RiskName"].ToString();
                        riskCalculation.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        riskCalculation.NumSection = Convert.ToInt32(dataReader["NumSection"]);
                        riskCalculation.NumFlag = Convert.ToInt32(dataReader["NumFlag"]);
                        riskCalculation.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        riskCalculation.CreatedBy = Convert.ToInt32(dataReader["CreatedBy"]);
                    }
                }

            }
        }
        return riskCalculation;
    }


    public RiskCalculation GetRiskCalculationByAOIDAndSOIDAndRiskName(int AOID, int SOID,string riskName)
    {
        RiskCalculation riskCalculation = null;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL GetRiskCalculationByAOIDANDSOIDANDRISKNAME(?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 600;
                //Set Parameter Value
                command.Parameters.AddWithValue("@AOID", AOID);
                command.Parameters.AddWithValue("@SOID", SOID);
                command.Parameters.AddWithValue("@RISK_NAME", riskName);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        riskCalculation = new RiskCalculation();

                        riskCalculation.RiskOID = Convert.ToInt32(dataReader["RiskOID"]);
                        riskCalculation.RiskName = dataReader["RiskName"].ToString();
                        riskCalculation.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        riskCalculation.NumSection = Convert.ToInt32(dataReader["NumSection"]);
                        riskCalculation.NumFlag = Convert.ToInt32(dataReader["NumFlag"]);
                        riskCalculation.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        riskCalculation.CreatedBy = Convert.ToInt32(dataReader["CreatedBy"]);
                    }
                }

            }
        }
        return riskCalculation;
    }


    public int  GetRiskOIDByAOIDAndName(int AOID, string  RiskName)
    {
        int ROID = -1;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL GetRiskOID_ByAOIDAndName(?,?)}";
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 600;
                //Set Parameter Value
                command.Parameters.AddWithValue("@AOID", AOID);
                command.Parameters.AddWithValue("@RiskName", RiskName);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        ROID = Convert.ToInt32(dataReader["RiskOID"]);   
                    }
                }

            }
        }
        return ROID ;
    }
    #endregion


}

[Serializable]
public class ResultEmail
{
    string connectionString = "";
    /// <summary>
    /// Constructor
    /// </summary>
    public ResultEmail()
    {
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();
    }
    //AssessmentResultOID, AssessmentOID, Header, ShowAboveResult, CreatedDate, CreatedBy, LastModifiedDate, LastModifiedBy


    #region Public Properties

    public int AssessmentResultOID
    { set; get; }
    public int AssessmentOID
    { set; get; }
    public string Header
    { set; get; }
    public string ShowAboveResult
    { set; get; }
    public DateTime CreatedDate
    { set; get; }
    public int CreatedBy
    { set; get; }
    public DateTime LastModifiedDate
    { set; get; }
    public int LastModifiedBy
    { set; get; }

    public Collection<ResultEmailDetail> ResultDetail
    { set; get; }

    #endregion

    #region Public Method

    public bool AddResultEmail()
    {
        bool result = false;
         
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL ResultEmail_insert(?,?,?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;


                OdbcParameter returnParam = command.Parameters.Add("@OID", OdbcType.Int);
                // set the direction flag so that it will be filled with the return value
                returnParam.Direction = ParameterDirection.ReturnValue;

                //@AssessmentOID, @Header, @ShowAboveResult, @CreatedBy, @LastModifiedBy
                //command.Parameters.AddWithValue("@SectionOID",this.SectionOID);@RiskName
                command.Parameters.AddWithValue("@AssessmentOID", this.AssessmentOID);
                command.Parameters.AddWithValue("@Header", this.Header);
                command.Parameters.AddWithValue("@ShowAboveResult", this.ShowAboveResult);
                command.Parameters.AddWithValue("@CreatedBy", this.CreatedBy);
                command.Parameters.AddWithValue("@LastModifiedBy", this.LastModifiedBy);

                connection.Open();
                int n = command.ExecuteNonQuery();
                if (n == 1)
                    result = true;
                else
                    result = false;
               int  resultEmailOID = (int)command.Parameters["@OID"].Value;
                foreach(ResultEmailDetail rd in this.ResultDetail)
                {
                    rd.AssessmentResultOID = resultEmailOID;
                    rd.AddResultEmailDetail();
                }
            }
        }
        return result;
    }

    public bool UpdateResultEmail()
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL ResultEmail_UPDATE(?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;
                
                
                //@AssessmentOID, @Header, @ShowAboveResult, @CreatedBy, @LastModifiedBy
                //command.Parameters.AddWithValue("@SectionOID",this.SectionOID);@RiskName
                command.Parameters.AddWithValue("@OID", this.AssessmentResultOID);
                command.Parameters.AddWithValue("@Header", this.Header);
                command.Parameters.AddWithValue("@ShowAboveResult", this.ShowAboveResult);                
                command.Parameters.AddWithValue("@LastModifiedBy", this.LastModifiedBy);

                connection.Open();
                int n = command.ExecuteNonQuery();
                if (n == 1)
                    result = true;
                else
                    result = false;
                
                foreach (ResultEmailDetail rd in this.ResultDetail)
                {
                    rd.AssessmentResultOID = AssessmentResultOID;
                    if (rd.ResultSectionOID == 0)
                    {
                        rd.AddResultEmailDetail();
                    }
                    else
                    {
                        rd.UpdateResultEmailDetail();
                    }
                }
            }
        }
        return result;
    }

    public ResultEmail GetResultEmailByOID(int OID)
    {
        ResultEmail resultEmail = null;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL ResultEmail_ByOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@AssessmentResultOID", OID);
                
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {

                    ResultEmailDetail eDetail = new ResultEmailDetail();
                    if (dataReader.Read())
                    {
                        resultEmail = new ResultEmail();

                        resultEmail.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        resultEmail.AssessmentResultOID = Convert.ToInt32(dataReader["AssessmentResultOID"]);
                        resultEmail.CreatedBy = Convert.ToInt32(dataReader["CreatedBy"]);
                        resultEmail.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        resultEmail.Header = Convert.ToString(dataReader["Header"]);
                        resultEmail.LastModifiedBy = Convert.ToInt32(dataReader["LastModifiedBy"]);
                        resultEmail.LastModifiedDate = Convert.ToDateTime(dataReader["LastModifiedDate"]);
                        resultEmail.ShowAboveResult = Convert.ToString(dataReader["ShowAboveResult"]);

                        resultEmail.ResultDetail = eDetail.GetEmailDetailByEmailOID(resultEmail.AssessmentResultOID);
                    }
                }

            }
        }
        return resultEmail;
    }

    public ResultEmail GetResultEmailByAOID(int AOID)
    {
        ResultEmail resultEmail = null;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL ResultEmail_ByAOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@AOID", AOID);

                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {

                    ResultEmailDetail eDetail = new ResultEmailDetail();
                    if (dataReader.Read())
                    {
                        resultEmail = new ResultEmail();

                        resultEmail.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        resultEmail.AssessmentResultOID = Convert.ToInt32(dataReader["AssessmentResultOID"]);
                        resultEmail.CreatedBy = Convert.ToInt32(dataReader["CreatedBy"]);
                        resultEmail.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        resultEmail.Header = Convert.ToString(dataReader["Header"]);
                        resultEmail.LastModifiedBy = Convert.ToInt32(dataReader["LastModifiedBy"]);
                        resultEmail.LastModifiedDate = Convert.ToDateTime(dataReader["LastModifiedDate"]);
                        resultEmail.ShowAboveResult = Convert.ToString(dataReader["ShowAboveResult"]);

                        resultEmail.ResultDetail = eDetail.GetEmailDetailByEmailOID(resultEmail.AssessmentResultOID);
                    }
                }

            }
        }
        return resultEmail;
    }


    #endregion
}

[Serializable]
public class ResultEmailDetail
{
    string connectionString = "";
    /// <summary>
    /// Constructor
    /// </summary>
    public ResultEmailDetail()
    {
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();
    }
    
    #region Public Properties
    public int ResultSectionOID
    { set; get; }
    public int AssessmentResultOID
    { set; get; }
    public int SectionOID
    { set; get; }
    public string PositiveResult
    { set; get; }
    public string NegativeResult
    { set; get; }
    public string LowResult
    { set; get; }
    public string MediumResult
    { set; get; }
    public string HighResult
    { set; get; }
    public DateTime CreatedDate
    { set; get; }
    public int CreatedBy
    { set; get; }
    public DateTime LastModifiedDate
    { set; get; }
    public int LastModifiedBy
    { set; get; }
    #endregion

    #region Public Method

    public bool AddResultEmailDetail()
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL ResultEmailDetail_insert(?,?,?,?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;
                                
                //command.Parameters.AddWithValue("@SectionOID",this.SectionOID);@RiskName
                command.Parameters.AddWithValue("@AssessmentResultOID", this.AssessmentResultOID);
                command.Parameters.AddWithValue("@SectionOID", this.SectionOID);
                command.Parameters.AddWithValue("@LowResult", this.LowResult);
                command.Parameters.AddWithValue("@MediumResult", this.MediumResult);
                command.Parameters.AddWithValue("@HighResult", this.HighResult);
                command.Parameters.AddWithValue("@CreatedBy", this.CreatedBy);
                command.Parameters.AddWithValue("@LastModifiedBy", this.LastModifiedBy);

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

    public bool UpdateResultEmailDetail()
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL ResultEmailDetail_Update(?,?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                //command.Parameters.AddWithValue("@SectionOID",this.SectionOID);@RiskName
                command.Parameters.AddWithValue("@OID", this.ResultSectionOID);
                command.Parameters.AddWithValue("@LowResult", this.LowResult);
                command.Parameters.AddWithValue("@MediumResult", this.MediumResult);
                command.Parameters.AddWithValue("@HighResult", this.HighResult);
                command.Parameters.AddWithValue("@LastModifiedBy", this.LastModifiedBy);

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

    public Collection<ResultEmailDetail> GetEmailDetailByEmailOID(int OID)
    {
        Collection<ResultEmailDetail> _list = new Collection<ResultEmailDetail>();
        ResultEmailDetail emailDetail = null;
        try
        {
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                using (OdbcCommand command = new OdbcCommand())
                {

                    command.Connection = connection;
                    command.CommandText = "{CALL ResultEmailDetail_ByResutEmailOID(?)}";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@AssessmentResultOID", OID);
                    connection.Open();

                    using (OdbcDataReader dataReader = command.ExecuteReader())
                    {

                        while (dataReader.Read())
                        {//this = new Assessment();
                            emailDetail = new ResultEmailDetail();

                            emailDetail.AssessmentResultOID = Convert.ToInt32(dataReader["AssessmentResultOID"]);
                            emailDetail.CreatedBy = Convert.ToInt32(dataReader["CreatedBy"]);
                            emailDetail.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                            emailDetail.LastModifiedBy = Convert.ToInt32(dataReader["LastModifiedBy"]);
                            emailDetail.LastModifiedDate = Convert.ToDateTime(dataReader["LastModifiedDate"]);
                            //emailDetail.NegativeResult = Convert.ToString(dataReader["NegativeResult"]);
                            emailDetail.MediumResult = Convert.ToString(dataReader["MediumResult"]);
                            emailDetail.HighResult = Convert.ToString(dataReader["HighResult"]);
                            emailDetail.LowResult = Convert.ToString(dataReader["LowResult"]);
                            emailDetail.SectionOID = Convert.ToInt32(dataReader["SectionOID"]);
                            emailDetail.ResultSectionOID = Convert.ToInt32(dataReader["ResultSectionOID"]);

                            _list.Add(emailDetail);
                        }
                    }

                }
            }

        }
        catch (Exception ex)
        { }

        return _list;

    }

    #endregion
}


[Serializable]
public class ResultLetter
{
    string connectionString = "";
    /// <summary>
    /// Constructor
    /// </summary>
    public ResultLetter()
    {
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();
    }
    ////AssessmentResultLetter, AssessmentOID, Header, ShowAboveResult, CreatedDate, CreatedBy, LastModifiedDate, LastModifiedBy
    #region Public Properties
    public int AssessmentResultLetter
    { set; get; }
    public int AssessmentOID
    { set; get; }
    public string Header
    { set; get; }
    public string ShowAboveResult
    { set; get; }
    public DateTime CreatedDate
    { set; get; }
    public int CreatedBy
    { set; get; }
    public DateTime LastModifiedDate
    { set; get; }
    public int LastModifiedBy
    { set; get; }
    public Collection<ResultLetterDetail> LetterDetail
    { set; get; }
    #endregion

    #region Public Method
    public bool AddResultLetter()
    {
        
        bool result = false;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL ResultLetter_insert(?,?,?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;


                OdbcParameter returnParam = command.Parameters.Add("@OID", OdbcType.Int);
                // set the direction flag so that it will be filled with the return value
                returnParam.Direction = ParameterDirection.ReturnValue;

                //@AssessmentOID, @Header, @ShowAboveResult, @CreatedBy, @LastModifiedBy
                //command.Parameters.AddWithValue("@SectionOID",this.SectionOID);@RiskName
                command.Parameters.AddWithValue("@AssessmentOID", this.AssessmentOID);
                command.Parameters.AddWithValue("@Header", this.Header);
                command.Parameters.AddWithValue("@ShowAboveResult", this.ShowAboveResult);
                command.Parameters.AddWithValue("@CreatedBy", this.CreatedBy);
                command.Parameters.AddWithValue("@LastModifiedBy", this.LastModifiedBy);

                connection.Open();
                int n = command.ExecuteNonQuery();
                if (n == 1)
                    result = true;
                else
                    result = false;
                int resultEmailOID = (int)command.Parameters["@OID"].Value;
                foreach (ResultLetterDetail rd in this.LetterDetail)
                {
                    rd.AssessmentLetterOID = resultEmailOID;
                    rd.AddResultLetterDetail();
                }
            }
        }
        return result;
    }

    public bool UpdateResultLetter()
    {

        bool result = false;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL ResultLetter_Update(?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 999;

                
                //@AssessmentOID, @Header, @ShowAboveResult, @CreatedBy, @LastModifiedBy
                //command.Parameters.AddWithValue("@SectionOID",this.SectionOID);@RiskName
                command.Parameters.AddWithValue("@OID", this.AssessmentResultLetter);
                command.Parameters.AddWithValue("@Header", this.Header);
                command.Parameters.AddWithValue("@ShowAboveResult", this.ShowAboveResult);                
                command.Parameters.AddWithValue("@LastModifiedBy", this.LastModifiedBy);

                connection.Open();
                int n = command.ExecuteNonQuery();
                if (n == 1)
                    result = true;
                else
                    result = false;
                
                foreach (ResultLetterDetail rd in this.LetterDetail)
                {
                    if (rd.ResultLetterSectionCommentOID == -1)
                    {
                        rd.AddResultLetterDetail();
                    }
                    else
                    {
                        rd.UpdateResultLetterDetail();
                    }
                }
            }
        }
        return result;
    }

    public ResultLetter GetResultLetterByOID(int OID)
    {
        ResultLetter resultLetter = null;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL ResultLetter_ByOID(?)}";
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 999;

                //Set Parameter Value
                command.Parameters.AddWithValue("@LetterOID", OID);

                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {

                    ResultLetterDetail lDetail = new ResultLetterDetail();
                    if (dataReader.Read())
                    {
                        resultLetter = new ResultLetter();

                        resultLetter.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        resultLetter.AssessmentResultLetter = Convert.ToInt32(dataReader["AssessmentResultLetter"]);
                        resultLetter.CreatedBy = Convert.ToInt32(dataReader["CreatedBy"]);
                        resultLetter.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        resultLetter.Header = Convert.ToString(dataReader["Header"]);
                        resultLetter.LastModifiedBy = Convert.ToInt32(dataReader["LastModifiedBy"]);
                        resultLetter.LastModifiedDate = Convert.ToDateTime(dataReader["LastModifiedDate"]);
                        resultLetter.ShowAboveResult = Convert.ToString(dataReader["ShowAboveResult"]);
                        resultLetter.LetterDetail = lDetail.GetLetterDetailByLetterOID(resultLetter.AssessmentResultLetter); ;
                    }
                }

            }
        }
        return resultLetter;
    }

    public ResultLetter GetResultLetterByAOID(int AOID)
    {
        ResultLetter resultLetter = null;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL ResultLetter_ByAOID(?)}";
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 999;

                //Set Parameter Value
                command.Parameters.AddWithValue("@AOID", AOID);

                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {

                    ResultLetterDetail lDetail = new ResultLetterDetail();
                    if (dataReader.Read())
                    {
                        resultLetter = new ResultLetter();

                        resultLetter.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        resultLetter.AssessmentResultLetter = Convert.ToInt32(dataReader["AssessmentResultLetter"]);
                        resultLetter.CreatedBy = Convert.ToInt32(dataReader["CreatedBy"]);
                        resultLetter.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        resultLetter.Header = Convert.ToString(dataReader["Header"]);
                        resultLetter.LastModifiedBy = Convert.ToInt32(dataReader["LastModifiedBy"]);
                        resultLetter.LastModifiedDate = Convert.ToDateTime(dataReader["LastModifiedDate"]);
                        resultLetter.ShowAboveResult = Convert.ToString(dataReader["ShowAboveResult"]);
                        resultLetter.LetterDetail = lDetail.GetLetterDetailByLetterOID(resultLetter.AssessmentResultLetter); ;
                    }
                }

            }
        }
        return resultLetter;
    }
    #endregion
}

[Serializable]
public class ResultLetterDetail
{
    string connectionString = "";
    /// <summary>
    /// Constructor
    /// </summary>
    public ResultLetterDetail()
    {
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();

    }

    
    #region Public Properties
    public int ResultLetterSectionCommentOID
    { set; get; }
    public int AssessmentLetterOID
    { set; get; }
    public int SectionOID
    { set; get; }
    public string SectionDefinition
    { set; get; }
    public DateTime CreatedDate
    { set; get; }
    public int CreatedBy
    { set; get; }
    public DateTime LastModifiedDate
    { set; get; }
    public int LastModifiedBy
    { set; get; }
    #endregion

    #region Public Method
    public bool AddResultLetterDetail()
    {
        bool result = false;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL ResultLetterDetail_insert(?,?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@AssessmentLetterOID", this.AssessmentLetterOID);
                command.Parameters.AddWithValue("@SectionOID", this.SectionOID);
                command.Parameters.AddWithValue("@SectionDefinition", this.SectionDefinition);
                command.Parameters.AddWithValue("@CreatedBy", this.CreatedBy);
                command.Parameters.AddWithValue("@LastModifiedBy", this.LastModifiedBy);

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

    public bool UpdateResultLetterDetail()
    {
        bool result = false;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL ResultLetterDetail_Update(?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@OID", this.ResultLetterSectionCommentOID);                
                command.Parameters.AddWithValue("@SectionDefinition", this.SectionDefinition);                
                command.Parameters.AddWithValue("@LastModifiedBy", this.LastModifiedBy);

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

    public Collection<ResultLetterDetail> GetLetterDetailByLetterOID(int OID)
    {
        Collection<ResultLetterDetail> _list = new Collection<ResultLetterDetail>();
        ResultLetterDetail letterDetail = null;
        try
        {
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                using (OdbcCommand command = new OdbcCommand())
                {

                    command.Connection = connection;
                    command.CommandText = "{CALL ResultLetterDetail_ByLetterOID(?)}";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@AssessmentLetterOID", OID);
                    connection.Open();

                    using (OdbcDataReader dataReader = command.ExecuteReader())
                    {
                        
                        while (dataReader.Read())
                        {//this = new Assessment();
                            letterDetail = new ResultLetterDetail();

                            letterDetail.AssessmentLetterOID = Convert.ToInt32(dataReader["AssessmentLetterOID"]);
                            letterDetail.CreatedBy = Convert.ToInt32(dataReader["CreatedBy"]);
                            letterDetail.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                            letterDetail.LastModifiedBy = Convert.ToInt32(dataReader["LastModifiedBy"]);
                            letterDetail.LastModifiedDate = Convert.ToDateTime(dataReader["LastModifiedDate"]);
                            letterDetail.ResultLetterSectionCommentOID = Convert.ToInt32(dataReader["ResultLetterSectionCommentOID"]);
                            letterDetail.SectionDefinition = Convert.ToString(dataReader["SectionDefinition"]);
                            letterDetail.SectionOID = Convert.ToInt32(dataReader["SectionOID"]);
                            
                            
                            _list.Add(letterDetail);
                        }
                    }

                }
            }

        }
        catch (Exception ex)
        { }

        return _list;
    
    }


    #endregion
}

[Serializable]
public class StudentRank
{     
    string connectionString;

    public StudentRank()
	{
	
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();
    }

    #region Properties
    public int AssessmentOID
    { set; get; }
    public int SectionOID
    { set; get; }
    public string SectionName
    { set; get; }
    public double Score
    { set; get; }
    public int Rank
    { set; get; }
    public string Comment
    { set; get; }

    #endregion

    #region Method

    public Collection<StudentRank> GetStudentRankByOID(int SOID)
    {
        StudentRank studentRank = null;
        Collection<StudentRank> _list = new Collection<StudentRank>();
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL GetStudentRankByOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Parameter Setting
                command.Parameters.AddWithValue("@SOID", SOID);

                connection.Open();

                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        studentRank = new StudentRank();
                        studentRank.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        studentRank.Comment = Convert.ToString(dataReader["Comment"]);
                        studentRank.Rank = Convert.ToInt32(dataReader["Rank"]);
                        //studentRank.Score = Convert.ToDouble(dataReader["Score"]);
                        studentRank.SectionName = Convert.ToString(dataReader["SectionName"]);
                        studentRank.SectionOID = Convert.ToInt32(dataReader["SectionOID"]);
                        _list.Add(studentRank);

                    }
                }
            }
        }
        return _list;
    }

    public Collection<StudentRank> GetStudentRankBySOIDandAOID(int SOID, int AOID)
    {
        StudentRank studentRank = null;
        Collection<StudentRank> _list = new Collection<StudentRank>();
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL GetStudentRankBySOIDandAOID1(?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Parameter Setting
                command.Parameters.AddWithValue("@SOID", SOID);
                command.Parameters.AddWithValue("@AOID", AOID);
                command.CommandTimeout = 900;
                connection.Open();

                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        studentRank = new StudentRank();
                        studentRank.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        studentRank.Comment = Convert.ToString(dataReader["Comment"]);
                        studentRank.Rank = Convert.ToInt32(dataReader["Rank"]);
                        //studentRank.Score = Convert.ToDouble(dataReader["Score"]);
                        studentRank.SectionName = Convert.ToString(dataReader["SectionName"]);
                        studentRank.SectionOID = Convert.ToInt32(dataReader["SectionOID"]);
                        _list.Add(studentRank);

                    }
                }
            }
        }
        return _list;
    }


    public Collection<StudentRank> GetStudentRankBySOIDandAOID_Print(int SOID, int AOID)
    {
        StudentRank studentRank = null;
        Collection<StudentRank> _list = new Collection<StudentRank>();
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL GetStudentRankBySOIDandAOID1_Print(?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Parameter Setting
                command.Parameters.AddWithValue("@SOID", SOID);
                command.Parameters.AddWithValue("@AOID", AOID);
                command.CommandTimeout = 900;
                connection.Open();

                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        studentRank = new StudentRank();
                        studentRank.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        studentRank.Comment = Convert.ToString(dataReader["Comment"]);
                        studentRank.Rank = Convert.ToInt32(dataReader["Rank"]);
                        //studentRank.Score = Convert.ToDouble(dataReader["Score"]);
                        studentRank.SectionName = Convert.ToString(dataReader["SectionName"]);
                        studentRank.SectionOID = Convert.ToInt32(dataReader["SectionOID"]);
                        _list.Add(studentRank);

                    }
                }
            }
        }
        return _list;
    }


    #endregion
}


[Serializable]
public class ScoreDetailTable
{
    string connectionString;

    public ScoreDetailTable()
    {

        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();
    }

    #region Properties
    public int AssessmentOID
    { set; get; }
    public int SectionOID
    { set; get; }
    public string ScoreName
    { set; get; }
    public string RankName
    { set; get; }
    public int StudentOID
    { set; get; }
    public string BannerID
    { set; get; }
    public string StudentName
    { set; get; }
    public string Program
    { set; get; }
    public double Score
    { set; get; }
    public double Rank
    { set; get; }

    #endregion

    #region Method

    public List<ScoreDetailTable> GetScoreDetailTableByAOID(int AOID)
    {
        ScoreDetailTable scoreDetailTable = null;
        List<ScoreDetailTable> _list = new List<ScoreDetailTable>();
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL GetScoreDetailTableByAOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Parameter Setting
                command.Parameters.AddWithValue("@AOID", AOID);

                connection.Open();

                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        scoreDetailTable = new ScoreDetailTable();

                        scoreDetailTable.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        scoreDetailTable.SectionOID = Convert.ToInt32(dataReader["SectionOID"]);
                        scoreDetailTable.ScoreName = Convert.ToString(dataReader["ScoreName"]);
                        scoreDetailTable.RankName = Convert.ToString(dataReader["RankName"]);
                        scoreDetailTable.StudentOID = Convert.ToInt32(dataReader["StudentOID"]);
                        if (dataReader["BannerID"] != null && dataReader["BannerID"] != DBNull.Value)
                        {
                            scoreDetailTable.BannerID = Convert.ToString(dataReader["BannerID"]);
                        }
                        if (dataReader["StudentName"] != null && dataReader["StudentName"] != DBNull.Value)
                        {
                            scoreDetailTable.StudentName = Convert.ToString(dataReader["StudentName"]);
                        }
                        if (dataReader["Program"] != null && dataReader["Program"] != DBNull.Value)
                        {
                            scoreDetailTable.Program = Convert.ToString(dataReader["Program"]);
                        }
                        scoreDetailTable.Score = Convert.ToDouble(dataReader["Score"]);
                        scoreDetailTable.Rank = Convert.ToDouble(dataReader["Rank"]);

                        _list.Add(scoreDetailTable);

                    }
                }
            }
        }
        return _list;
    }

    //public Collection<StudentRank> GetStudentRankBySOIDandAOID(int SOID, int AOID)
    //{
    //    StudentRank studentRank = null;
    //    Collection<StudentRank> _list = new Collection<StudentRank>();
    //    using (OdbcConnection connection = new OdbcConnection(connectionString))
    //    {
    //        using (OdbcCommand command = new OdbcCommand())
    //        {

    //            command.Connection = connection;
    //            command.CommandText = "{CALL GetStudentRankBySOIDandAOID(?,?)}";
    //            command.CommandType = CommandType.StoredProcedure;

    //            //Parameter Setting
    //            command.Parameters.AddWithValue("@SOID", SOID);
    //            command.Parameters.AddWithValue("@AOID", AOID);

    //            connection.Open();

    //            using (OdbcDataReader dataReader = command.ExecuteReader())
    //            {
    //                while (dataReader.Read())
    //                {
    //                    studentRank = new StudentRank();
    //                    studentRank.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
    //                    studentRank.Comment = Convert.ToString(dataReader["Comment"]);
    //                    studentRank.Rank = Convert.ToInt32(dataReader["Rank"]);
    //                    //studentRank.Score = Convert.ToDouble(dataReader["Score"]);
    //                    studentRank.SectionName = Convert.ToString(dataReader["SectionName"]);
    //                    studentRank.SectionOID = Convert.ToInt32(dataReader["SectionOID"]);
    //                    _list.Add(studentRank);

    //                }
    //            }
    //        }
    //    }
    //    return _list;
    //}
    #endregion
}



[Serializable]
public class ReminderEmail
{
    string connectionString = "";
    /// <summary>
    /// Constructor
    /// </summary>
    public ReminderEmail()
    {
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();
    }
    //AssessmentResultOID, AssessmentOID, Header, ShowAboveResult, CreatedDate, CreatedBy, LastModifiedDate, LastModifiedBy


    #region Public Properties

    public int AssessmentReminderOID
    { set; get; }
    public int AssessmentOID
    { set; get; }
 
    public string EmailBody
    { set; get; }


    public DateTime CreatedDate
    { set; get; }
    public int CreatedBy
    { set; get; }
    public DateTime LastModifiedDate
    { set; get; }
    public int LastModifiedBy
    { set; get; }


    #endregion

    #region Public Method

    public bool AddReminderEmail()
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL ReminderEmail_Insert(?,?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;


                OdbcParameter returnParam = command.Parameters.Add("@OID", OdbcType.Int);
                // set the direction flag so that it will be filled with the return value
                returnParam.Direction = ParameterDirection.ReturnValue;

                command.Parameters.AddWithValue("@AssessmentOID", this.AssessmentOID);
                command.Parameters.AddWithValue("@ShowAboveResult", this.EmailBody);
                command.Parameters.AddWithValue("@CreatedBy", this.CreatedBy);
                command.Parameters.AddWithValue("@LastModifiedBy", this.LastModifiedBy);

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

    public bool UpdateReminderEmail()
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL ReminderEmail_Update(?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;


                //@AssessmentOID, @Header, @ShowAboveResult, @CreatedBy, @LastModifiedBy
                //command.Parameters.AddWithValue("@SectionOID",this.SectionOID);@RiskName
                command.Parameters.AddWithValue("@OID", this.AssessmentReminderOID);
                command.Parameters.AddWithValue("@EmailBody", this.EmailBody);
                command.Parameters.AddWithValue("@LastModifiedBy", this.LastModifiedBy);

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

    public ReminderEmail GetReminderEmailByOID(int OID)
    {
        ReminderEmail reminderEmail = null;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL ReminderEmail_ByOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@AssessmentReminderOID", OID);

                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {

                    ResultEmailDetail eDetail = new ResultEmailDetail();
                    if (dataReader.Read())
                    {
                        reminderEmail = new ReminderEmail();

                        reminderEmail.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        reminderEmail.AssessmentReminderOID = Convert.ToInt32(dataReader["AssessmentReminderOID"]);
                        reminderEmail.CreatedBy = Convert.ToInt32(dataReader["CreatedBy"]);
                        reminderEmail.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        reminderEmail.EmailBody = Convert.ToString(dataReader["EmailBody"]);
                        reminderEmail.LastModifiedBy = Convert.ToInt32(dataReader["LastModifiedBy"]);
                        reminderEmail.LastModifiedDate = Convert.ToDateTime(dataReader["LastModifiedDate"]);
                    }
                }

            }
        }
        return reminderEmail;
    }

    public ReminderEmail GetReminderEmailByAOID(int AOID)
    {
        ReminderEmail reminderEmail = null;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL ReminderEmail_ByAOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@AOID", AOID);

                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {

                    ResultEmailDetail eDetail = new ResultEmailDetail();
                    if (dataReader.Read())
                    {
                        reminderEmail = new ReminderEmail();

                        reminderEmail.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        reminderEmail.AssessmentReminderOID = Convert.ToInt32(dataReader["AssessmentReminderOID"]);
                        reminderEmail.CreatedBy = Convert.ToInt32(dataReader["CreatedBy"]);
                        reminderEmail.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        reminderEmail.EmailBody = Convert.ToString(dataReader["EmailBody"]);
                        reminderEmail.LastModifiedBy = Convert.ToInt32(dataReader["LastModifiedBy"]);
                        reminderEmail.LastModifiedDate = Convert.ToDateTime(dataReader["LastModifiedDate"]);
                    }
                }

            }
        }
        return reminderEmail;
    }
    #endregion
}
