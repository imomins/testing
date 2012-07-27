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
using System.Data.Odbc;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Web.Script.Serialization;


public partial class pg_assessment_Result : System.Web.UI.Page
{
    string connectionString;
    string aid;
    string MenuURL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();
        if (!IsPostBack)
        {
            //Columns.InnerHtml = HttpUtility.HtmlEncode(GetColumnNameListJSON(Convert.ToInt32(aid)));
            //ReportHeaderDiv.InnerHtml = HttpUtility.HtmlEncode(CreateJSONObject(Convert.ToInt32(aid)));
            //DataTable json = CreateJSONObject(Convert.ToInt32(aid));
        }
    }


    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            aid = Request.QueryString["aid"].ToString();
            HiddenAOID.Value = aid;
            string reportOIDStr = Request.QueryString["ReportOID"].ToString();
           
            MenuURL = "pg/assessment/ResultReport.aspx?ReportOID=" + reportOIDStr+"&aid="+aid;
                       
            Reports report = null;
            if (!string.IsNullOrEmpty(reportOIDStr))
            {
                report = new Reports().GetReportByOID(Int32.Parse(reportOIDStr));
            }

            HiddenReportName.Value = report.ReportName;
            Hiddenquery.Value = report.SPParams;
            HiddenColumns.Value = report.GridColumns;
            //ReportHeaderDiv.InnerHtml =HttpUtility.HtmlEncode( GetColumnNameListJSON(Convert .ToInt32 ( aid)));
            
            
        }
        catch (Exception ex)
        { }

    }

    


    [System.Web.Services.WebMethod]
    public static string GetColumnNameList(int aoid)
    {
        Assessment ass = new Assessment();
        string col = ass.GetColumnNameList(aoid);
        string JsonCol = "";
        string[] colArray = col.Split(',');
        foreach (string str in colArray)
        {
            JsonCol = JsonCol + "\"" + str + "\"" + ":" + str + ",";
        }
        JsonCol = JsonCol.Substring(0, JsonCol.Length - 1);
        return ass.GetColumnNameList(aoid);
    }

   
    public string GetColumnNameListJSON(int aoid)
    {
        Assessment ass = new Assessment();
         JQGridResults result = new JQGridResults();
            List<JQGridRow> rows = new List<JQGridRow>();

        string col = ass.GetColumnNameList(aoid);
        string staticCol= col+ ",hidden,FullTimeOrPartTimeIndicator,CumulativeGPA,CreditsAttempted,CreditsEarned,LatestCompassPrealgebraTestScore,LatestCompassAlgebraTestScore,LatestCompassWritingTestScore,LatestCompassReadingTestScore,LatestACTEnglishAssessmentScore,LatestACTMathAssessmentScore,LatestACTReadingAssessmentScore,LatestACTScienceAssessmentScore,LatestTestingDate,HighSchoolName,HighSchoolGraduationDate,HomeTelephoneNumber,MailingAddressLineOne,MailingAddressLineTwo,MailingAddressLineThree,City,StateName,ZipCode,EmailAddress";
        string JsonCol = "";
        string[] colArray = staticCol.Split(',');
        JQGridRow row = new JQGridRow(); 
        int i = 0;
        row.cell = new string[colArray.Length];
        foreach (string str in colArray)
        {
            
             row.cell[i++] = str;

           
        }
           rows.Add(row);
        
            result.rows = rows.ToArray();
            result.page = 0;
            result.total = 0;
            result.records = 0;
          
        return new JavaScriptSerializer().Serialize(result);
       
        
    }


    public string GetColumnNamesJSON(int aoid)
    {
        Assessment ass = new Assessment();
        JQGridResults result = new JQGridResults();
        List<JQGridRow> rows = new List<JQGridRow>();

        string col = ass.GetColumnNameList(aoid);
        string staticCol = col + ",hidden,FullTimeOrPartTimeIndicator,CumulativeGPA,CreditsAttempted,CreditsEarned,LatestCompassPrealgebraTestScore,LatestCompassAlgebraTestScore,LatestCompassWritingTestScore,LatestCompassReadingTestScore,LatestACTEnglishAssessmentScore,LatestACTMathAssessmentScore,LatestACTReadingAssessmentScore,LatestACTScienceAssessmentScore,LatestTestingDate,HighSchoolName,HighSchoolGraduationDate,HomeTelephoneNumber,MailingAddressLineOne,MailingAddressLineTwo,MailingAddressLineThree,City,StateName,ZipCode,EmailAddress";
        return staticCol;

    }

    public DataTable CreateJSONObject(int aoid)
    {
        #region Session Declaration
        //Palash

        string reportname = Convert.ToString(Session["reportname"]);
        string numberOfRows = Convert.ToString(Session["numberOfRows"]);
        string pageIndex = Convert.ToString(Session["pageIndex"]);

        string sortColumnName = Convert.ToString(Session["sortColumnName"]);
        string sortOrderBy = Convert.ToString(Session["sortOrderBy"]);
        string BannerID = Convert.ToString(Session["BannerID"]);


        string NAME = Convert.ToString(Session["NAME"]);
        string StudentOID = Convert.ToString(Session["StudentOID"]);
        string TERM = Convert.ToString(Session["TERM"]);

        string FullPart = Convert.ToString(Session["FullPart"]);
        string GPA = Convert.ToString(Session["GPA"]);
        string CreditAttempted = Convert.ToString(Session["CreditAttempted"]);
        string EarnedCredit = Convert.ToString(Session["EarnedCredit"]);
        string Prealgebra = Convert.ToString(Session["Prealgebra"]);
        string Algebra = Convert.ToString(Session["Algebra"]);
        string Writting = Convert.ToString(Session["Writting"]);
        string Reading = Convert.ToString(Session["Reading"]);
        string English = Convert.ToString(Session["English"]);
        string Math = Convert.ToString(Session["Math"]);
        string ReadingScore = Convert.ToString(Session["ReadingScore"]);
        string ScienceScore = Convert.ToString(Session["ScienceScore"]);
        DateTime TestingDate = Convert.ToDateTime(Session["TestingDate"]);
        string HighSchool = Convert.ToString(Session["HighSchool"]);
        DateTime HS_GRAD_DATE = Convert.ToDateTime(Session["HS_GRAD_DATE"]);

        string ADDR1 = Convert.ToString(Session["ADDR1"]);
        string ADDR2 = Convert.ToString(Session["ADDR2"]);
        string ADDR3 = Convert.ToString(Session["ADDR3"]);
        string CITY = Convert.ToString(Session["CITY"]);
        string STATE = Convert.ToString(Session["STATE"]);
        string ZIP = Convert.ToString(Session["ZIP"]);

        DateTime ImportDate = Convert.ToDateTime(Session["ImportDate"]);

        string PPGMIND = Convert.ToString(Session["PPGMIND"]);
        string MAJOR = Convert.ToString(Session["MAJOR"]);
        string Email = Convert.ToString(Session["Email"]);
        string Phone = Convert.ToString(Session["Phone"]);
        //numberOfRows = "";
        //pageIndex = "";

        //Palash

        #endregion

        int totalRecords=0;
       // string aoid = Request.QueryString["aid"].ToString();
        string SortOrSearchFlag = "Search";
        string query = Hiddenquery.Value;


        string strParameter = HttpUtility.UrlDecode(query);
        string MultipleSearch = "";
        string SearchColName = "";
        string SearchVal = "";

        SortOrSearchFlag = "Search";
        MultipleSearch = SearchColName;
        if (!strParameter.Equals(string.Empty))
        {
            foreach (string str in strParameter.Substring(1).Split('&'))
            {
                string[] str1 = str.Split('=');

                if (str1.Length > 0)
                {
                    MultipleSearch = MultipleSearch + str1[0] + ",";
                    SearchVal = SearchVal + str1[1] + ",";
                }
            }
            MultipleSearch = MultipleSearch.Substring(0, MultipleSearch.Length - 1);
            SearchVal = SearchVal.Substring(0, SearchVal.Length - 1);
        }

        DataTable output = GetResult(Convert.ToInt32(aoid), numberOfRows, pageIndex,  totalRecords, MultipleSearch, SearchVal, sortColumnName, sortOrderBy, BannerID, MAJOR, NAME, SortOrSearchFlag);
      
        return output;
    }

    public DataTable getDataTableFromJson(List<JQGridRow> rows)
    {
        DataTable dt = new DataTable();




        string columnNames = GetColumnNamesJSON(Convert.ToInt32(aid));

       foreach (string str in columnNames.Split(','))
       {
           dt.Columns.Add(str);
       }


        foreach (JQGridRow orow in rows)
        {
           int countCell = orow.cell.Count();
          DataRow dataRow = dt.NewRow();
           for (int i = 0; i < countCell; i++)
           {
               dataRow[i] = orow.cell[i];
           }
           dt.Rows.Add(dataRow);
        }


        return dt;
    }

    public DataTable GetResult(int AOID, string numberOfRows, string pageIndex,  int totalRecords, string searchColumnName, string searchVal, string sortColumnName, string sortOrderBy, string bannerID, string program, string studentName, string SortOrSearchFlag)
    {
        DataTable dt = null;
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
                    command.CommandText = "{CALL Assessement_ResultByAOID_Report(?,?,?,?,?,?,?,?,?,?,?,?,?)}";
                    command.CommandType = CommandType.StoredProcedure;

                
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
                    command.Parameters.AddWithValue("@SearchColumnName", searchColumnName + ",");
                    command.Parameters.AddWithValue("@SortOrderBy", sortOrderBy);
                    command.Parameters.AddWithValue("@BannerID", bannerID);
                    command.Parameters.AddWithValue("@Program", program);
                    command.Parameters.AddWithValue("@StudentName", studentName);
                    command.Parameters.AddWithValue("@SearchValue", searchVal + ",");
                    command.Parameters.AddWithValue("@SortColumnName", sortColumnName);
                    command.Parameters.AddWithValue("@SortOrSearchFlag", SortOrSearchFlag);
                    command.Parameters.AddWithValue("@MultiCoulumn", MultipleSearch);
                    connection.Open();
                    #region Reader
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

                                if (dataReader[j].ToString().Contains(','))
                                {
                                    row.cell[j] = dataReader[j].ToString().Replace(',', ' ');
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
                                if (j == 6)
                                {
                                    string SOID = Convert.ToString(dataReader[j - 1]);
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
                    #endregion
                   dt = getDataTableFromJson(rows);


            result.rows = rows.ToArray();
            result.page = Convert.ToInt32(pageIndex);
            result.total = totalRecords / Convert.ToInt32(numberOfRows);
            if (totalRecords % Convert.ToInt32(numberOfRows) != 0) result.total += 1;
            result.records = totalRecords;
            //return new JavaScriptSerializer().Serialize(result);
            return dt;
        }
        catch (Exception ex)
        {
            //totalRecords = 20;
            //return "";
            //return dt;
            dt = null;
        }
        return dt;
    }


    protected void ButtonCSV_Click(object sender, EventArgs e)
    {
        try
        {

            #region Session Declaration
            //Palash

            string reportname = Convert.ToString(Session["reportname"]);
            string numberOfRows = Convert.ToString(Session["numberOfRows"]);
            string pageIndex = Convert.ToString(Session["pageIndex"]);

            string sortColumnName = Convert.ToString(Session["sortColumnName"]);
            string sortOrderBy = Convert.ToString(Session["sortOrderBy"]);
            string BannerID = Convert.ToString(Session["BannerID"]);


            string NAME = Convert.ToString(Session["NAME"]);
            string StudentOID = Convert.ToString(Session["StudentOID"]);
            string TERM = Convert.ToString(Session["TERM"]);

            string FullPart = Convert.ToString(Session["FullPart"]);
            string GPA = Convert.ToString(Session["GPA"]);
            string CreditAttempted = Convert.ToString(Session["CreditAttempted"]);
            string EarnedCredit = Convert.ToString(Session["EarnedCredit"]);
            string Prealgebra = Convert.ToString(Session["Prealgebra"]);
            string Algebra = Convert.ToString(Session["Algebra"]);
            string Writting = Convert.ToString(Session["Writting"]);
            string Reading = Convert.ToString(Session["Reading"]);
            string English = Convert.ToString(Session["English"]);
            string Math = Convert.ToString(Session["Math"]);
            string ReadingScore = Convert.ToString(Session["ReadingScore"]);
            string ScienceScore = Convert.ToString(Session["ScienceScore"]);
            DateTime TestingDate = Convert.ToDateTime(Session["TestingDate"]);
            string HighSchool = Convert.ToString(Session["HighSchool"]);
            DateTime HS_GRAD_DATE = Convert.ToDateTime(Session["HS_GRAD_DATE"]);

            string ADDR1 = Convert.ToString(Session["ADDR1"]);
            string ADDR2 = Convert.ToString(Session["ADDR2"]);
            string ADDR3 = Convert.ToString(Session["ADDR3"]);
            string CITY = Convert.ToString(Session["CITY"]);
            string STATE = Convert.ToString(Session["STATE"]);
            string ZIP = Convert.ToString(Session["ZIP"]);

            DateTime ImportDate = Convert.ToDateTime(Session["ImportDate"]);

            string PPGMIND = Convert.ToString(Session["PPGMIND"]);
            string MAJOR = Convert.ToString(Session["MAJOR"]);
            string Email = Convert.ToString(Session["Email"]);
            string Phone = Convert.ToString(Session["Phone"]);



            //Palash

            #endregion
            int totalRecords;
            string  aoid = Request.QueryString["aid"].ToString();
            string SortOrSearchFlag = "Search";
            string query = Hiddenquery.Value;


            string strParameter = HttpUtility.UrlDecode(query);
            string MultipleSearch = "";
            string SearchColName = "";
            string SearchVal = "";

            SortOrSearchFlag = "Search";
            MultipleSearch = SearchColName;
            if (!strParameter.Equals(string.Empty))
            {
                foreach (string str in strParameter.Substring(1).Split('&'))
                {
                    string[] str1 = str.Split('=');

                    if (str1.Length > 0)
                    {
                        MultipleSearch = MultipleSearch + str1[0] + ",";
                        SearchVal = SearchVal + str1[1] + ",";
                    }
                }
                MultipleSearch = MultipleSearch.Substring(0, MultipleSearch.Length - 1);
                SearchVal = SearchVal.Substring(0, SearchVal.Length - 1);
            }


            Assessment ass = new Assessment();
            string output = ass.GetAssessmentResult(Convert .ToInt32 ( aoid), numberOfRows, pageIndex, out totalRecords, MultipleSearch, SearchVal, sortColumnName, sortOrderBy, BannerID, MAJOR, NAME, SortOrSearchFlag);

            //Collection<Student> students = SearchStudent(reportname, numberOfRows, pageIndex, sortColumnName, sortOrderBy, out totalRecords, Convert.ToInt32("0"), request["NAME"], request["StudentOID"], request["TERM"], request["FullPart"], Convert.ToDouble(request["GPA"]), Convert.ToDouble(request["CreditAttempted"]), Convert.ToDouble(request["EarnedCredit"]), request["Prealgebra"], request["Algebra"], request["Writting"], request["Reading"], request["English"], request["Math"], request["ReadingScore"], request["ScienceScore"], Convert.ToDateTime(request["TestingDate"]), request["HighSchool"], Convert.ToDateTime(request["HS_GRAD_DATE"]), request["ADDR1"], request["ADDR2"], request["ADDR3"], request["CITY"], request["STATE"], request["ZIP"], Convert.ToDateTime(request["ImportDate"]), request["PPGMIND"], request["MAJOR"], request["Email"], request["Phone"]);
            Collection<Student> students = SearchStudent(reportname, numberOfRows, pageIndex, sortColumnName, sortOrderBy, out totalRecords, Convert.ToInt32("0"), NAME, StudentOID, TERM, FullPart, GPA, CreditAttempted, EarnedCredit, Prealgebra, Algebra, Writting, Reading, English, Math, ReadingScore, ScienceScore, TestingDate, HighSchool, HS_GRAD_DATE, ADDR1, ADDR2, ADDR3, CITY, STATE, ZIP, ImportDate, PPGMIND, MAJOR, Email, Phone);
            DataTable studentReportDt = this.ConvertListToDataTable(students);

            string reportOIDStr = Request.QueryString["ReportOID"].ToString();



            ExportToExcel.ExportToSpreadsheet(studentReportDt, reportOIDStr, "CSV");

        }
        catch (Exception ex)
        { }
    }

    private DataTable ConvertListToDataTable(Collection<Student> students)
    {
        DataTable dt = new DataTable();
        try
        {

            dt.Columns.Add("StudentOID");
            dt.Columns.Add("NAME");
            dt.Columns.Add("BID");
            dt.Columns.Add("TERM");
            dt.Columns.Add("FullPart");
            dt.Columns.Add("GPA");
            dt.Columns.Add("CreditAttempted");
            dt.Columns.Add("EarnedCredit");
            dt.Columns.Add("Prealgebra");
            dt.Columns.Add("Algebra");
            dt.Columns.Add("Writting");
            dt.Columns.Add("Reading");
            dt.Columns.Add("English");
            dt.Columns.Add("Math");
            dt.Columns.Add("ReadingScore");
            dt.Columns.Add("ScienceScore");
            dt.Columns.Add("TestingDate");
            dt.Columns.Add("HighSchool");
            dt.Columns.Add("HS_GRAD_DATE");
            dt.Columns.Add("Phone");
            dt.Columns.Add("ADDR1");
            dt.Columns.Add("ADDR2");
            dt.Columns.Add("ADDR3");
            dt.Columns.Add("CITY");
            dt.Columns.Add("STATE");
            dt.Columns.Add("ZIP");
            dt.Columns.Add("Email");
            dt.Columns.Add("ImportDate");
            dt.Columns.Add("PPGMIND");
            dt.Columns.Add("MAJOR");

            foreach (Student student in students)
            {
                DataRow row = dt.NewRow();

                row["StudentOID"] = student.StudentOID.ToString();
                row["NAME"] = student.FullName;
                row["BID"] = student.StudentID;
                row["TERM"] = student.ProgramEnrollment;
                row["FullPart"] = student.TimeIndicator;
                row["GPA"] = student.CumulativeGPA.ToString();
                row["CreditAttempted"] = student.CreditAttempted.ToString();
                row["EarnedCredit"] = student.CreditEarned.ToString();
                row["Prealgebra"] = student.PrealgebraTestScore;
                row["Algebra"] = student.CompassalgebraTestScore;
                row["Writting"] = student.CompassWrittingTestScore;
                row["Reading"] = student.CompassReadingTestScore;
                row["English"] = student.EnglishAssessmentScore;
                row["Math"] = student.MathAssessmentScore;
                row["ReadingScore"] = student.ReadingAssessmentScore;
                row["ScienceScore"] = student.ScienceAssessmentScore;

                row["TestingDate"] = student.LatestTestingDate.ToShortDateString();
                row["HighSchool"] = student.HighSchoolName;
                row["HS_GRAD_DATE"] = student.HighSchoolGraduationDate.ToShortDateString();
                row["Phone"] = student.HomeTelephoneNumber;
                row["ADDR1"] = student.AddressOne;
                row["ADDR2"] = student.AddressTwo;
                row["ADDR3"] = student.AddressThree;
                row["CITY"] = student.City;

                row["STATE"] = student.State;
                row["ZIP"] = student.ZIPCode;
                row["Email"] = student.EmailAddress;
                row["ImportDate"] = student.ImportDate.ToShortDateString();
                row["PPGMIND"] = student.PreprogramIndicator;
                row["MAJOR"] = student.MajorProgramEnrollment;

                dt.Rows.Add(row);
            }
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    private Collection<Student> SearchStudent(string reportname, string numberOfRows, string pageIndex, string sortColumnName, string sortOrderBy, out int totalRecords, int StudentOID, string NAME, string ID, string TERM, string PFIND, string GPA, string ATMPCR, string EARNCR, string C1, string C2, string CW, string CR, string AE, string AM, string AR, string SA, DateTime SDATE, string HSDESC, DateTime HS_GRAD_DATE, string ADDR1, string ADDR2, string ADDR3, string CITY, string STATE, string ZIP, DateTime FILEDATE, string PPGMIND, string MAJOR, string Email, string Phone)
    {

        double startGpa = 0.00;
        double endGpa = 0.00;
        string[] strArrGPA = null;

        double StartCreditAttempted = 0.00;
        double EndCreditAttempted = 0.00;
        string[] strCreditArr = null;

        double StartCreditEarned = 0.00;
        double EndCreditEarned = 0.00;
        string[] strCreditEarnedArr = null;

        double StartPreAlgebra = 0.00;
        double EndPreAlgebra = 0.00;
        string[] strPreAlgebraArr = null;

        double StartAlgebra = 0.00;
        double EndAlgebra = 0.00;
        string[] strAlgebraArr = null;

        double StartWritting = 0.00;
        double EndWritting = 0.00;
        string[] strWrittingArr = null;

        double StartReading = 0.00;
        double EndReading = 0.00;
        string[] strReadingArr = null;

        double StartMath = 0.00;
        double EndMath = 0.00;
        string[] strMathArr = null;

        double StartEnglish = 0.00;
        double EndEnglish = 0.00;
        string[] strEnglishArr = null;

        double StartReadingScore = 0.00;
        double EndReadingScore = 0.00;
        string[] strReadingScoreArr = null;

        double StartScienceScore = 0.00;
        double EndScienceScore = 0.00;
        string[] strScienceScoreArr = null;

        StudentOID = (StudentOID.ToString() == null) ? StudentOID : StudentOID;
        NAME = (NAME == null) ? "" : NAME;
        ID = (ID == null) ? "" : ID;
        TERM = (TERM == null) ? "" : TERM;
        PFIND = (PFIND == null) ? "" : PFIND;

        if (GPA != null && GPA != "")
        {

            //gpa = GPA.ToString();

            if (GPA.Contains("-"))
            {
                strArrGPA = GPA.Split('-');
                startGpa = Convert.ToDouble(strArrGPA[0]);
                endGpa = Convert.ToDouble(strArrGPA[1]);
            }
            else
            {
                endGpa = (GPA == null) ? endGpa : Convert.ToDouble(GPA);
                startGpa = (GPA == null) ? endGpa : Convert.ToDouble(GPA);

            }
        }
        else
        {

            //endGpa = (GPA == null) ? endGpa : Convert.ToDouble(GPA);
            //startGpa = (GPA == null) ? endGpa : Convert.ToDouble(GPA);
            endGpa = 10;
            startGpa = 0;

            //endGpa = (GPA.ToString() == null) ? GPA : GPA;
        }

        if (ATMPCR != null && ATMPCR != "")
        {
            if (ATMPCR.Contains("-"))
            {
                strCreditArr = ATMPCR.Split('-');
                StartCreditAttempted = Convert.ToDouble(strCreditArr[0]);
                EndCreditAttempted = Convert.ToDouble(strCreditArr[1]);
            }
            else
            {
                EndCreditAttempted = (ATMPCR == null) ? EndCreditAttempted : Convert.ToDouble(ATMPCR);
                StartCreditAttempted = (ATMPCR == null) ? EndCreditAttempted : Convert.ToDouble(ATMPCR);

            }
        }
        else
        {
            //EndCreditAttempted = (ATMPCR == null) ? EndCreditAttempted : Convert.ToDouble(ATMPCR);
            //StartCreditAttempted = (ATMPCR == null) ? EndCreditAttempted : Convert.ToDouble(ATMPCR);
            EndCreditAttempted = 1000;
            StartCreditAttempted = 0;


        }
        //ATMPCR = (ATMPCR.ToString() == null) ? ATMPCR : ATMPCR;


        if (EARNCR != null && EARNCR != "")
        {
            if (EARNCR.Contains("-"))
            {
                strCreditEarnedArr = EARNCR.Split('-');
                StartCreditEarned = Convert.ToDouble(strCreditEarnedArr[0]);
                EndCreditEarned = Convert.ToDouble(strCreditEarnedArr[1]);
            }
            else
            {
                EndCreditEarned = (EARNCR == null) ? EndCreditEarned : Convert.ToDouble(EARNCR);
                StartCreditEarned = (EARNCR == null) ? EndCreditEarned : Convert.ToDouble(EARNCR);

            }
        }
        else
        {
            //EndCreditAttempted = (ATMPCR == null) ? EndCreditAttempted : Convert.ToDouble(ATMPCR);
            //StartCreditAttempted = (ATMPCR == null) ? EndCreditAttempted : Convert.ToDouble(ATMPCR);
            EndCreditEarned = 1000;
            StartCreditEarned = 0;


        }
        // EARNCR = (EARNCR.ToString() == null) ? EARNCR : EARNCR;


        if (C1 != null && C1 != "")
        {
            if (C1.Contains("-"))
            {
                strPreAlgebraArr = C1.Split('-');
                StartPreAlgebra = Convert.ToDouble(strPreAlgebraArr[0]);
                EndPreAlgebra = Convert.ToDouble(strPreAlgebraArr[1]);
            }
            else
            {
                EndPreAlgebra = (C1 == null) ? EndPreAlgebra : Convert.ToDouble(C1);
                StartPreAlgebra = (C1 == null) ? EndPreAlgebra : Convert.ToDouble(C1);

            }
        }
        else
        {

            EndPreAlgebra = 1000;
            StartPreAlgebra = 0;


        }

        //C1 = (C1 == null) ? "" : C1;


        if (C2 != null && C2 != "")
        {
            if (C2.Contains("-"))
            {
                strAlgebraArr = C2.Split('-');
                StartAlgebra = Convert.ToDouble(strAlgebraArr[0]);
                EndAlgebra = Convert.ToDouble(strAlgebraArr[1]);
            }
            else
            {
                EndAlgebra = (C2 == null) ? EndAlgebra : Convert.ToDouble(C2);
                StartAlgebra = (C2 == null) ? EndAlgebra : Convert.ToDouble(C2);

            }
        }
        else
        {

            EndAlgebra = 1000;
            StartAlgebra = 0;


        }

        //C2 = (C2 == null) ? "" : C2;

        if (CW != null && CW != "")
        {
            if (CW.Contains("-"))
            {
                strWrittingArr = CW.Split('-');
                StartWritting = Convert.ToDouble(strWrittingArr[0]);
                EndWritting = Convert.ToDouble(strWrittingArr[1]);
            }
            else
            {
                EndWritting = (CW == null) ? EndWritting : Convert.ToDouble(CW);
                StartWritting = (CW == null) ? EndWritting : Convert.ToDouble(CW);

            }
        }
        else
        {

            EndWritting = 1000;
            StartWritting = 0;


        }


        //CW = (CW == null) ? "" : CW;

        if (CR != null && CR != "")
        {
            if (CR.Contains("-"))
            {
                strReadingArr = CR.Split('-');
                StartReading = Convert.ToDouble(strReadingArr[0]);
                EndReading = Convert.ToDouble(strReadingArr[1]);
            }
            else
            {
                EndReading = (CR == null) ? EndReading : Convert.ToDouble(CR);
                StartReading = (CR == null) ? EndReading : Convert.ToDouble(CR);

            }
        }
        else
        {

            EndReading = 1000;
            StartReading = 0;


        }

        //CR = (CR == null) ? "" : CR;

        if (AE != null && AE != "")
        {
            if (AE.Contains("-"))
            {
                strEnglishArr = AE.Split('-');
                StartEnglish = Convert.ToDouble(strEnglishArr[0]);
                EndEnglish = Convert.ToDouble(strEnglishArr[1]);
            }
            else
            {
                EndEnglish = (AE == null) ? EndEnglish : Convert.ToDouble(AE);
                StartEnglish = (AE == null) ? EndEnglish : Convert.ToDouble(AE);

            }
        }
        else
        {

            EndEnglish = 1000;
            StartEnglish = 0;


        }

        // AE = (AE == null) ? "" : AE;

        if (AM != null && AM != "")
        {
            if (AM.Contains("-"))
            {
                strMathArr = AM.Split('-');
                StartMath = Convert.ToDouble(strMathArr[0]);
                EndMath = Convert.ToDouble(strMathArr[1]);
            }
            else
            {
                EndMath = (AM == null) ? EndMath : Convert.ToDouble(AM);
                StartMath = (AM == null) ? EndMath : Convert.ToDouble(AM);

            }
        }
        else
        {

            EndMath = 1000;
            StartMath = 0;


        }

        //AM = (AM == null) ? "" : AM;

        if (AR != null && AR != "")
        {
            if (AR.Contains("-"))
            {
                strReadingScoreArr = AR.Split('-');
                StartReadingScore = Convert.ToDouble(strReadingScoreArr[0]);
                EndReadingScore = Convert.ToDouble(strReadingScoreArr[1]);
            }
            else
            {
                EndReadingScore = (AR == null) ? EndReadingScore : Convert.ToDouble(AR);
                StartReadingScore = (AR == null) ? EndReadingScore : Convert.ToDouble(AR);

            }
        }
        else
        {

            EndReadingScore = 1000;
            StartReadingScore = 0;


        }

        //AR = (AR == null) ? "" : AR;
        if (SA != null && SA != "")
        {
            if (SA.Contains("-"))
            {
                strScienceScoreArr = SA.Split('-');
                StartScienceScore = Convert.ToDouble(strScienceScoreArr[0]);
                EndScienceScore = Convert.ToDouble(strScienceScoreArr[1]);
            }
            else
            {
                EndScienceScore = (SA == null) ? EndScienceScore : Convert.ToDouble(SA);
                StartScienceScore = (SA == null) ? EndScienceScore : Convert.ToDouble(SA);

            }
        }
        else
        {

            EndScienceScore = 1000;
            StartScienceScore = 0;


        }
        //SA = (SA == null) ? "" : SA;

        SDATE = (SDATE == Convert.ToDateTime("1/1/0001")) ? Convert.ToDateTime("1/1/1900") : SDATE;
        HSDESC = (HSDESC == null) ? "" : HSDESC;

        HS_GRAD_DATE = (HS_GRAD_DATE == Convert.ToDateTime("1/1/0001")) ? Convert.ToDateTime("1/1/1900") : HS_GRAD_DATE;
        ADDR1 = (ADDR1 == null) ? "" : ADDR1;
        ADDR2 = (ADDR2 == null) ? "" : ADDR2;
        ADDR3 = (ADDR3 == null) ? "" : ADDR3;
        CITY = (CITY == null) ? "" : CITY;
        STATE = (STATE == null) ? "" : STATE;

        ZIP = (ZIP == null) ? "" : ZIP;
        FILEDATE = (FILEDATE == Convert.ToDateTime("1/1/0001")) ? Convert.ToDateTime("1/1/1900") : FILEDATE;
        PPGMIND = (PPGMIND == null) ? "" : PPGMIND;
        MAJOR = (MAJOR == null) ? "" : MAJOR;
        Email = (Email == null) ? "" : Email;
        Phone = (Phone == null) ? "" : Phone;
        //FileCreateDate = (FileCreateDate == null) ? Convert.ToDateTime("1/1/1900") : FileCreateDate;





        //This code for searching

        Collection<Student> students = new Collection<Student>();
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                //command.CommandText = "{CALL Student_Search(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)}";
                command.CommandText = "{CALL Student_Search(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                //OdbcParameter paramStudentOID = new OdbcParameter("@StudentOID", OdbcType.Int, 10);
                //paramStudentOID.Value = StudentOID;
                //command.Parameters.Add(paramStudentOID);

                OdbcParameter paramNAME = new OdbcParameter("@NAME", OdbcType.VarChar, 100);
                paramNAME.Value = NAME;
                command.Parameters.Add(paramNAME);

                OdbcParameter paramID = new OdbcParameter("@ID", OdbcType.VarChar, 100);
                paramID.Value = ID;
                command.Parameters.Add(paramID);

                OdbcParameter paramTERM = new OdbcParameter("@TERM", OdbcType.VarChar, 100);
                paramTERM.Value = TERM;
                command.Parameters.Add(paramTERM);

                OdbcParameter paramPFIND = new OdbcParameter("@PFIND", OdbcType.VarChar, 100);
                paramPFIND.Value = PFIND;
                command.Parameters.Add(paramPFIND);

                //

                OdbcParameter paramGPA1 = new OdbcParameter("@STARTGPA", OdbcType.VarChar, 15);//5
                paramGPA1.Value = startGpa;
                command.Parameters.Add(paramGPA1);

                //

                OdbcParameter paramGPA = new OdbcParameter("@ENDGPA", OdbcType.VarChar, 15);//5
                paramGPA.Value = endGpa;
                command.Parameters.Add(paramGPA);



                OdbcParameter paramATMPCR = new OdbcParameter("@STARTATMPCR", OdbcType.VarChar, 15);
                paramATMPCR.Value = StartCreditAttempted;
                command.Parameters.Add(paramATMPCR);


                OdbcParameter paramATMPCR1 = new OdbcParameter("@ENDATMPCR", OdbcType.VarChar, 15);
                paramATMPCR1.Value = EndCreditAttempted;
                command.Parameters.Add(paramATMPCR1);



                OdbcParameter paramEARNCR = new OdbcParameter("@STARTEARNCR", OdbcType.VarChar, 15);
                paramEARNCR.Value = StartCreditEarned;
                command.Parameters.Add(paramEARNCR);


                OdbcParameter paramEARNCR1 = new OdbcParameter("@ENDEARNCR", OdbcType.VarChar, 15);
                paramEARNCR1.Value = EndCreditEarned;
                command.Parameters.Add(paramEARNCR1);


                OdbcParameter paramC1 = new OdbcParameter("@STARTC1", OdbcType.VarChar, 100);
                paramC1.Value = StartPreAlgebra;
                command.Parameters.Add(paramC1);

                OdbcParameter paramC11 = new OdbcParameter("@ENDC1", OdbcType.VarChar, 100);
                paramC11.Value = EndPreAlgebra;
                command.Parameters.Add(paramC11);



                OdbcParameter paramC2 = new OdbcParameter("@STARTC2", OdbcType.VarChar, 15);
                paramC2.Value = StartAlgebra;
                command.Parameters.Add(paramC2);

                OdbcParameter paramC21 = new OdbcParameter("@ENDC2", OdbcType.VarChar, 15);
                paramC21.Value = EndAlgebra;
                command.Parameters.Add(paramC21);

                OdbcParameter paramCW = new OdbcParameter("@STARTCW", OdbcType.VarChar, 15);//10
                paramCW.Value = StartWritting;
                command.Parameters.Add(paramCW);


                OdbcParameter paramCW1 = new OdbcParameter("@ENDCW", OdbcType.VarChar, 15);//10
                paramCW1.Value = EndWritting;
                command.Parameters.Add(paramCW1);


                OdbcParameter paramCR = new OdbcParameter("@STARTCR", OdbcType.VarChar, 15);
                paramCR.Value = StartReading;
                command.Parameters.Add(paramCR);


                OdbcParameter paramCR1 = new OdbcParameter("@ENDCR", OdbcType.VarChar, 15);
                paramCR1.Value = EndReading;
                command.Parameters.Add(paramCR1);


                OdbcParameter paramAE = new OdbcParameter("@STARTAE", OdbcType.VarChar, 15);
                paramAE.Value = StartEnglish;
                command.Parameters.Add(paramAE);

                OdbcParameter paramAE1 = new OdbcParameter("@ENDAE", OdbcType.VarChar, 15);
                paramAE1.Value = EndEnglish;
                command.Parameters.Add(paramAE1);


                OdbcParameter paramAM = new OdbcParameter("@SATRTAM", OdbcType.VarChar, 15);
                paramAM.Value = StartMath;
                command.Parameters.Add(paramAM);

                OdbcParameter paramAM1 = new OdbcParameter("@ENDAM", OdbcType.VarChar, 15);
                paramAM1.Value = EndMath;
                command.Parameters.Add(paramAM1);

                OdbcParameter paramAR = new OdbcParameter("@STARTAR", OdbcType.VarChar, 15);
                paramAR.Value = StartReadingScore;
                command.Parameters.Add(paramAR);

                OdbcParameter paramAR1 = new OdbcParameter("@ENDAR", OdbcType.VarChar, 15);
                paramAR1.Value = EndReadingScore;
                command.Parameters.Add(paramAR1);

                OdbcParameter paramSA = new OdbcParameter("@SATRTSA", OdbcType.VarChar, 15);//15
                paramSA.Value = StartScienceScore;
                command.Parameters.Add(paramSA);


                OdbcParameter paramSA1 = new OdbcParameter("@ENDSA", OdbcType.VarChar, 15);//15
                paramSA1.Value = EndScienceScore;
                command.Parameters.Add(paramSA1);

                OdbcParameter paramSDATE = new OdbcParameter("@SDATE", OdbcType.DateTime);
                paramSDATE.Value = SDATE;
                command.Parameters.Add(paramSDATE);

                OdbcParameter paramHSDESC = new OdbcParameter("@HSDESC", OdbcType.VarChar, 100);
                paramHSDESC.Value = HSDESC;
                command.Parameters.Add(paramHSDESC);

                OdbcParameter paramHS_GRAD_DATE = new OdbcParameter("@HS_GRAD_DATE", OdbcType.DateTime);
                paramHS_GRAD_DATE.Value = HS_GRAD_DATE;
                command.Parameters.Add(paramHS_GRAD_DATE);

                //
                OdbcParameter paramHTNumber = new OdbcParameter("@HTNumber", OdbcType.VarChar, 20);
                paramHTNumber.Value = Phone;
                command.Parameters.Add(paramHTNumber);
                //

                OdbcParameter paramADDR1 = new OdbcParameter("@ADDR1", OdbcType.VarChar, 100);//20
                paramADDR1.Value = ADDR1;
                command.Parameters.Add(paramADDR1);

                OdbcParameter paramADDR2 = new OdbcParameter("@ADDR2", OdbcType.VarChar, 100);
                paramADDR2.Value = ADDR2;
                command.Parameters.Add(paramADDR2);

                OdbcParameter paramADDR3 = new OdbcParameter("@ADDR3", OdbcType.VarChar, 100);
                paramADDR3.Value = ADDR3;
                command.Parameters.Add(paramADDR3);

                OdbcParameter paramCITY = new OdbcParameter("@CITY", OdbcType.VarChar, 100);
                paramCITY.Value = CITY;
                command.Parameters.Add(paramCITY);

                OdbcParameter paramSTATE = new OdbcParameter("@STATE", OdbcType.VarChar, 100);
                paramSTATE.Value = STATE;
                command.Parameters.Add(paramSTATE);

                OdbcParameter paramZIP = new OdbcParameter("@ZIP", OdbcType.VarChar, 100);//25
                paramZIP.Value = ZIP;
                command.Parameters.Add(paramZIP);

                OdbcParameter paramEmail = new OdbcParameter("@Email", OdbcType.VarChar, 100);
                paramEmail.Value = Email;
                command.Parameters.Add(paramEmail);

                OdbcParameter paramFILEDATE = new OdbcParameter("@FILEDATE", OdbcType.DateTime);
                paramFILEDATE.Value = FILEDATE;
                command.Parameters.Add(paramFILEDATE);

                //OdbcParameter paramFileCreateDate = new OdbcParameter("@FileCreateDate", OdbcType.DateTime);
                //paramFileCreateDate.Value = FILEDATE;
                //command.Parameters.Add(paramFileCreateDate);

                OdbcParameter paramPPGMIND = new OdbcParameter("@PPGMIND", OdbcType.VarChar, 100);
                paramPPGMIND.Value = PPGMIND;
                command.Parameters.Add(paramPPGMIND);

                OdbcParameter paramMAJOR = new OdbcParameter("@MAJOR", OdbcType.VarChar, 100);
                paramMAJOR.Value = MAJOR;
                command.Parameters.Add(paramMAJOR);

                OdbcParameter paramPageIndex = new OdbcParameter("@PageIndex", OdbcType.Int);//30
                paramPageIndex.Value = Convert.ToInt32(pageIndex);
                command.Parameters.Add(paramPageIndex);

                OdbcParameter paramColumnName = new OdbcParameter("@SortColumnName", OdbcType.VarChar, 50);
                paramColumnName.Value = sortColumnName;
                command.Parameters.Add(paramColumnName);

                OdbcParameter paramSortorderBy = new OdbcParameter("@SortOrderBy", OdbcType.VarChar, 4);
                paramSortorderBy.Value = sortOrderBy;
                command.Parameters.Add(paramSortorderBy);

                OdbcParameter paramNumberOfRows = new OdbcParameter("@NumberOfRows", OdbcType.Int);
                paramNumberOfRows.Value = Convert.ToInt32(numberOfRows);
                command.Parameters.Add(paramNumberOfRows);

                OdbcParameter paramTotalRecords = new OdbcParameter("@TotalRecords", OdbcType.Int);
                totalRecords = 0;
                paramTotalRecords.Value = totalRecords;
                paramTotalRecords.Direction = ParameterDirection.Output;
                command.Parameters.Add(paramTotalRecords);

                connection.Open();
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    Student student;
                    while (dataReader.Read())
                    {
                        student = new Student();
                        student.StudentOID = Convert.ToInt32(dataReader["StudentOID"]);
                        if (dataReader["BannerStudentName"] != null)
                        {
                            student.FullName = Convert.ToString(dataReader["BannerStudentName"]);

                        }

                        if (dataReader["BannerStudentIDNumber"] != null)
                        {
                            student.StudentID = Convert.ToString(dataReader["BannerStudentIDNumber"]);
                        }
                        if (dataReader["TermCodeofProgramEnrollment"] != null)
                        {
                            student.ProgramEnrollment = Convert.ToString(dataReader["TermCodeofProgramEnrollment"]);
                        }
                        if (dataReader["FullTimeOrPartTimeIndicator"] != null)
                        {
                            student.TimeIndicator = Convert.ToString(dataReader["FullTimeOrPartTimeIndicator"]);
                        }
                        if (dataReader["CumulativeGPA"] != null)
                        {
                            student.CumulativeGPA = Convert.ToDouble(dataReader["CumulativeGPA"]);
                        }
                        if (dataReader["CreditsAttempted"] != null)
                        {
                            student.CreditAttempted = Convert.ToDouble(dataReader["CreditsAttempted"]);
                        }
                        if (dataReader["CreditsEarned"] != null)
                        {
                            student.CreditEarned = Convert.ToDouble(dataReader["CreditsEarned"]);
                        }
                        if (dataReader["LatestCompassPrealgebraTestScore"] != null)
                        {
                            student.PrealgebraTestScore = Convert.ToString(dataReader["LatestCompassPrealgebraTestScore"]);
                        }
                        if (dataReader["LatestCompassAlgebraTestScore"] != null)
                        {
                            student.CompassalgebraTestScore = Convert.ToString(dataReader["LatestCompassAlgebraTestScore"]);
                        }
                        if (dataReader["LatestCompassWritingTestScore"] != null)
                        {
                            student.CompassWrittingTestScore = Convert.ToString(dataReader["LatestCompassWritingTestScore"]);
                        }
                        if (dataReader["LatestCompassReadingTestScore"] != null)
                        {
                            student.CompassReadingTestScore = Convert.ToString(dataReader["LatestCompassReadingTestScore"]);
                        }
                        if (dataReader["LatestACTEnglishAssessmentScore"] != null)
                        {
                            student.EnglishAssessmentScore = Convert.ToString(dataReader["LatestACTEnglishAssessmentScore"]);
                        }
                        if (dataReader["LatestACTMathAssessmentScore"] != null)
                        {
                            student.MathAssessmentScore = Convert.ToString(dataReader["LatestACTMathAssessmentScore"]);
                        }
                        if (dataReader["LatestACTReadingAssessmentScore"] != null)
                        {
                            student.ReadingAssessmentScore = Convert.ToString(dataReader["LatestACTReadingAssessmentScore"]);
                        }
                        if (dataReader["LatestACTScienceAssessmentScore"] != null)
                        {
                            student.ScienceAssessmentScore = Convert.ToString(dataReader["LatestACTScienceAssessmentScore"]);
                        }
                        if (dataReader["LatestTestingDate"] != null)
                        {
                            student.LatestTestingDate = Convert.ToDateTime(dataReader["LatestTestingDate"]);
                        }
                        if (dataReader["HighSchoolName"] != null)
                        {
                            student.HighSchoolName = Convert.ToString(dataReader["HighSchoolName"]);
                        }
                        if (dataReader["HighSchoolGraduationDate"] != null)
                        {
                            student.HighSchoolGraduationDate = Convert.ToDateTime(dataReader["HighSchoolGraduationDate"]);
                        }
                        if (dataReader["HomeTelephoneNumber"] != null)
                        {
                            student.HomeTelephoneNumber = Convert.ToString(dataReader["HomeTelephoneNumber"]);
                        }
                        if (dataReader["MailingAddressLineOne"] != null)
                        {
                            student.AddressOne = Convert.ToString(dataReader["MailingAddressLineOne"]);
                        }
                        if (dataReader["MailingAddressLineTwo"] != null)
                        {
                            student.AddressTwo = Convert.ToString(dataReader["MailingAddressLineTwo"]);
                        }
                        if (dataReader["MailingAddressLineThree"] != null)
                        {
                            student.AddressThree = Convert.ToString(dataReader["MailingAddressLineThree"]);
                        }
                        if (dataReader["City"] != null)
                        {
                            student.City = Convert.ToString(dataReader["City"]);
                        }
                        if (dataReader["StateName"] != null)
                        {
                            student.State = Convert.ToString(dataReader["StateName"]);
                        }
                        if (dataReader["ZipCode"] != null)
                        {
                            student.ZIPCode = Convert.ToString(dataReader["ZipCode"]);
                        }
                        if (dataReader["EmailAddress"] != null)
                        {
                            student.EmailAddress = Convert.ToString(dataReader["EmailAddress"]);
                        }
                        if (dataReader["ImportDateFileCreationDate"] != null)
                        {
                            student.ImportDate = Convert.ToDateTime(dataReader["ImportDateFileCreationDate"]);
                        }
                        if (dataReader["PreprogramIndicator"] != null)
                        {
                            student.PreprogramIndicator = Convert.ToString(dataReader["PreprogramIndicator"]);
                        }
                        if (dataReader["MajorProgramEnrollmentName"] != null)
                        {
                            student.MajorProgramEnrollment = Convert.ToString(dataReader["MajorProgramEnrollmentName"]);
                        }
                        student.FileCreatedDate = Convert.ToDateTime(dataReader["FileCreationDate"]);
                        students.Add(student);

                    }
                }
                totalRecords = (int)paramTotalRecords.Value;
            }

            return students;
        }
    }

    protected void ButtonPDF_Click(object sender, EventArgs e)
    {
        try
        {

          
            string reportOIDStr = Request.QueryString["ReportOID"].ToString();
            DataTable json = CreateJSONObject(Convert.ToInt32(aid));
            
            ExportToExcel.ExportToSpreadsheet(json, reportOIDStr, "CSV");

        }
        catch (Exception ex)
        { }
    }

    private bool  DeleteReport(string url)
    {

        bool ret = false;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Report_Delete(?)}";
                command.CommandType = CommandType.StoredProcedure;

                OdbcParameter paramURL = new OdbcParameter("@url", OdbcType.VarChar, 200);
                paramURL.Value = url;
                command.Parameters.Add(paramURL);

                connection.Open();
                try
                {
                   int n= command.ExecuteNonQuery();
                   if (n == 1)
                   {
                       ret = true;
                   }
                   else
                   {
                       ret = false;
                   }
                }
                catch (Exception ae)
                {
                    return ret;
                }

            }
            return ret;
        }

    }

    protected void ButtonDelete_Click(object sender, EventArgs e)
    {

        if(DeleteReport(MenuURL))
        {
        lblError.Text = "Report has been Deleted Successfully..";
        Response.Redirect("../student/ReportDelete.aspx", false);
        }
    }
    protected void ButtonCSV_Click1(object sender, EventArgs e)
    {
        try
        {


            string reportOIDStr = Request.QueryString["ReportOID"].ToString();
            DataTable json = CreateJSONObject(Convert.ToInt32(aid));

            ExportToExcel.ExportToSpreadsheet(json, reportOIDStr, "PDF");

        }
        catch (Exception ex)
        { }
    }
}
