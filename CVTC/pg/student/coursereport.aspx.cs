using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.Odbc;
using System.Collections.ObjectModel;
 

public partial class pg_student_coursereport : System.Web.UI.Page
{
    string connectionString = "";
    string MenuURL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString(); 
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            string reportOIDStr = Request.QueryString["ReportOID"].ToString();
            MenuURL = "pg/student/coursereport.aspx?ReportOID=" + reportOIDStr;
            Reports report = null;
            if (!string.IsNullOrEmpty(reportOIDStr))
            {
                report = new Reports().GetReportByOID(Int32.Parse(reportOIDStr));
            }

            HiddenReportName.Value = report.ReportName;
            Hiddenquery.Value = report.SPParams;
            HiddenColumns.Value = report.GridColumns;
        }
        catch (Exception ex)
        { }
    }


    protected void ButtonCSV_Click(object sender, EventArgs e)
    {
        try
        {
            #region Session Declaration
            //Palash
            int BannerID = Convert.ToInt32(Session["BannerID"]);
            string NAME = Convert.ToString(Session["NAME"]);
            string StudentOID = Convert.ToString(Session["StudentOID"]);
            string TERM = Convert.ToString(Session["TERM"]);
            string CRSENO = Convert.ToString(Session["CRSENO"]);
            string CRSETITLE = Convert.ToString(Session["CRSETITLE"]);
            string FINALGRDE = Convert.ToString(Session["FINALGRDE"]);
            string CRSETERM = Convert.ToString(Session["CRSETERM"]);
            string DeliveryMethod = Convert.ToString(Session["DeliveryMethod"]);
            DateTime  ImportDate_Course = Convert.ToDateTime(Session["ImportDate"]);
            int CourseOID = Convert.ToInt32(Session["CourseOID"]);
            DateTime  FileCreationDate = Convert.ToDateTime(Session["FileCreationDate"]);




            string reportname = Convert.ToString(Session["reportname"]);
            string numberOfRows = Convert.ToString(Session["numberOfRows"]);
            string pageIndex = Convert.ToString(Session["pageIndex"]);

            string sortColumnName = Convert.ToString(Session["sortColumnName"]);
            string sortOrderBy = Convert.ToString(Session["sortOrderBy"]);
            


            

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
            int totalRecords;


            //Palash

            #endregion

            Collection<Course> courses = SearchCourse(numberOfRows, pageIndex, sortColumnName, sortOrderBy, out totalRecords,NAME ,StudentOID ,TERM , CRSENO, CRSETITLE, FINALGRDE, CRSETERM, DeliveryMethod, ImportDate, CourseOID, FileCreationDate, FullPart, GPA, CreditAttempted, EarnedCredit, Prealgebra, Algebra, Writting, Reading, English, Math, ReadingScore, ScienceScore, TestingDate, HighSchool, HS_GRAD_DATE, ADDR1, ADDR2, ADDR3, CITY, STATE, ZIP, ImportDate, PPGMIND, MAJOR, Email, Phone);
            
            
            //DataTable courseReportDt = Session["CourseReportDt"] as DataTable;
            DataTable courseReportDt = ConvertListToDataTable(courses);
            string reportOIDStr = Request.QueryString["ReportOID"].ToString();




            ExportToExcel.ExportToSpreadsheet(courseReportDt, reportOIDStr, "CSV");

        }
        catch (Exception ex)
        { }
    }


    private DataTable ConvertListToDataTable(Collection<Course > courses)
    {
        DataTable dt = new DataTable();
        try
        {

            dt.Columns.Add("StudentOID");
            dt.Columns.Add("NAME");
            dt.Columns.Add("BID");
            dt.Columns.Add("TERM");


            dt.Columns.Add("CRSENO");
            dt.Columns.Add("CRSETITLE");
            dt.Columns.Add("FINALGRDE");
            dt.Columns.Add("CRSETERM");
            dt.Columns.Add("DeliveryMethod");
            dt.Columns.Add("ImportDate_Course");
            dt.Columns.Add("CourseOID");
            dt.Columns.Add("FileCreationDate");
           
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

            dt.Columns.Add("ALERT");
            dt.Columns.Add("RVP");
            dt.Columns.Add("PELL");
            dt.Columns.Add("MC");
            dt.Columns.Add("NTO");


            foreach (Course  course in courses)
            {
                DataRow row = dt.NewRow();

                row["StudentOID"] = course.StudentOID.ToString();
                row["NAME"] = course.BannerStudentName ;
                row["BID"] = course.BannerStudentIDNumber ;
                row["TERM"] = course.TermCodeofProgramEnrollment ;

                row["CRSENO"]=course .CourseNumber ;
                row["CRSETITLE"]=course .CourseTitle ;
                row["FINALGRDE"]=course .FinalGrade ;
                row["CRSETERM"]=course .TermCodeOfCourseEnrollment ;
                row["DeliveryMethod"]=course .MethodOfDelivery ;
                row["ImportDate_Course"]=course .ImportDate ;
                row["CourseOID"]=course .CourseOID ;
                row["FileCreationDate"]=course .FileCreationDate ;


                row["FullPart"] = course.TimeIndicator;
                row["GPA"] = course.CumulativeGPA.ToString();
                row["CreditAttempted"] = course.CreditAttempted.ToString();
                row["EarnedCredit"] = course.CreditEarned.ToString();
                row["Prealgebra"] = course.PrealgebraTestScore;
                row["Algebra"] = course.CompassalgebraTestScore;
                row["Writting"] = course.CompassWrittingTestScore;
                row["Reading"] = course.CompassReadingTestScore;
                row["English"] = course.EnglishAssessmentScore;
                row["Math"] = course.MathAssessmentScore;
                row["ReadingScore"] = course.ReadingAssessmentScore;
                row["ScienceScore"] = course.ScienceAssessmentScore;

                row["TestingDate"] = course.LatestTestingDate.ToShortDateString();
                row["HighSchool"] = course.HighSchoolName;
                row["HS_GRAD_DATE"] = course.HighSchoolGraduationDate.ToShortDateString();
                row["Phone"] = course.HomeTelephoneNumber;
                row["ADDR1"] = course.AddressOne;
                row["ADDR2"] = course.AddressTwo;
                row["ADDR3"] = course.AddressThree;
                row["CITY"] = course.City;

                row["STATE"] = course.State;
                row["ZIP"] = course.ZIPCode;
                row["Email"] = course.EmailAddress;
                row["ImportDate"] = course.ImportDate_Student.ToShortDateString();
                row["PPGMIND"] = course.PreprogramIndicator;
                row["MAJOR"] = course.MajorProgramEnrollment;

                row["ALERT"] = (course.ALLERT == "0") ? "" : "ALERT";
                row["RVP"] = (course.RVP == "0") ? "" : "RVP";
                row["PELL"] = (course.PELL == "0") ? "" : "PELL";
                row["MC"] = (course.MC == "0") ? "" : "MC";
                row["NTO"] = (course.NTO == "0") ? "" : "NTO";
              

                dt.Rows.Add(row);
            }
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    private Collection<Course> SearchCourse(string numberOfRows, string pageIndex, string sortColumnName, string sortOrderBy, out int totalRecords, string bannerStudentName, string bannerStudentIDNumber, string termCodeofProgramEnrollment, string courseNumber, string courseTitle, string finalGrade, string termCodeOfCourseEnrollment, string methodOfDelivery, DateTime importDate, Int32 courseOID, DateTime fileCreationDate, string PFIND, string GPA, string ATMPCR, string EARNCR, string C1, string C2, string CW, string CR, string AE, string AM, string AR, string SA, DateTime SDATE, string HSDESC, DateTime HS_GRAD_DATE, string ADDR1, string ADDR2, string ADDR3, string CITY, string STATE, string ZIP, DateTime FILEDATE, string PPGMIND, string MAJOR, string Email, string Phone)
    {
        #region Variable

        //For Course
        bannerStudentName = (bannerStudentName == null) ? "" : bannerStudentName;
        bannerStudentIDNumber = (bannerStudentIDNumber == null) ? "" : bannerStudentIDNumber;
        termCodeofProgramEnrollment = (termCodeofProgramEnrollment == null) ? "" : termCodeofProgramEnrollment;
        courseNumber = (courseNumber == null) ? "" : courseNumber;
        courseTitle = (courseTitle == null) ? "" : courseTitle;
        finalGrade = (finalGrade == null) ? "" : finalGrade;

        termCodeOfCourseEnrollment = (termCodeOfCourseEnrollment == null) ? "" : termCodeOfCourseEnrollment;
        methodOfDelivery = (methodOfDelivery == null) ? "" : methodOfDelivery;

        importDate = ((importDate == null) || (importDate == Convert.ToDateTime("1/1/0001"))) ? Convert.ToDateTime("1/1/1900") : importDate;
        courseOID = (courseOID < 1) ? 0 : courseOID;
        fileCreationDate = ((fileCreationDate == null) || (fileCreationDate == Convert.ToDateTime("1/1/0001"))) ? Convert.ToDateTime("1/1/1900") : fileCreationDate;
        //End For Course



        //For Student

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
        #endregion
        //End Student

        Collection<Course> courses = new Collection<Course>();

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                command.Connection = connection;
                //command.CommandText = "{CALL Course_Search(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)}";
                command.CommandText = "{CALL Course_Search_Report(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                #region SP Parameter

                OdbcParameter paramPageIndex = new OdbcParameter("@PageIndex", OdbcType.Int);
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

                //For Course

                OdbcParameter paramBannerStudentName = new OdbcParameter("@BannerStudentName", OdbcType.VarChar, 80);
                paramBannerStudentName.Value = bannerStudentName;
                command.Parameters.Add(paramBannerStudentName);

                OdbcParameter paramBannerStudentIDNumber = new OdbcParameter("@BannerStudentIDNumber", OdbcType.VarChar, 12);
                paramBannerStudentIDNumber.Value = bannerStudentIDNumber;
                command.Parameters.Add(paramBannerStudentIDNumber);

                OdbcParameter paramTermCodeofProgramEnrollment = new OdbcParameter("@TermCodeofProgramEnrollment", OdbcType.VarChar, 20);
                paramTermCodeofProgramEnrollment.Value = termCodeofProgramEnrollment;
                command.Parameters.Add(paramTermCodeofProgramEnrollment);

                OdbcParameter paramCourseNumber = new OdbcParameter("@CourseNumber", OdbcType.VarChar, 15);
                paramCourseNumber.Value = courseNumber;
                command.Parameters.Add(paramCourseNumber);

                OdbcParameter paramCourseTitle = new OdbcParameter("@CourseTitle", OdbcType.VarChar, 100);
                paramCourseTitle.Value = courseTitle;
                command.Parameters.Add(paramCourseTitle);

                OdbcParameter paramFinalGrade = new OdbcParameter("@FinalGrade", OdbcType.VarChar, 15);
                paramFinalGrade.Value = finalGrade;
                command.Parameters.Add(paramFinalGrade);

                OdbcParameter paramTermCodeOfCourseEnrollment = new OdbcParameter("@TermCodeOfCourseEnrollment", OdbcType.VarChar, 15);
                paramTermCodeOfCourseEnrollment.Value = termCodeOfCourseEnrollment;
                command.Parameters.Add(paramTermCodeOfCourseEnrollment);

                OdbcParameter paramMethodOfDelivery = new OdbcParameter("@MethodOfDelivery", OdbcType.VarChar, 50);
                paramMethodOfDelivery.Value = methodOfDelivery;
                command.Parameters.Add(paramMethodOfDelivery);

                OdbcParameter paramImportDate = new OdbcParameter("@ImportDate", OdbcType.DateTime);
                paramImportDate.Value = importDate;
                command.Parameters.Add(paramImportDate);

                OdbcParameter paramCourseOID = new OdbcParameter("@CourseOID", OdbcType.Int);
                paramCourseOID.Value = courseOID;
                command.Parameters.Add(paramCourseOID);

                OdbcParameter paramFileCreationDate = new OdbcParameter("@FileCreationDate", OdbcType.DateTime);
                paramFileCreationDate.Value = fileCreationDate;
                command.Parameters.Add(paramFileCreationDate);

                //End For Course



                //Parameter for Student
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



                OdbcParameter paramPPGMIND = new OdbcParameter("@PPGMIND", OdbcType.VarChar, 100);
                paramPPGMIND.Value = PPGMIND;
                command.Parameters.Add(paramPPGMIND);

                OdbcParameter paramMAJOR = new OdbcParameter("@MajorProgramEnrollmentName", OdbcType.VarChar, 100);
                paramMAJOR.Value = MAJOR;
                command.Parameters.Add(paramMAJOR);

                #endregion
                //End parameter Student


                        #region Reader

                connection.Open();
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    Course course;
                    while (dataReader.Read())
                    {
                        course = new Course();
                        if (dataReader["CourseOID"] != null)
                        {
                            course.CourseOID = Convert.ToInt32(dataReader["CourseOID"]);
                        }
                        if (dataReader["BannerStudentName"] != null)
                        {
                            course.BannerStudentName = Convert.ToString(dataReader["BannerStudentName"]);
                        }
                        if (dataReader["BannerStudentIDNumber"] != null)
                        {
                            course.BannerStudentIDNumber = Convert.ToString(dataReader["BannerStudentIDNumber"]);
                        }
                        if (dataReader["TermCodeofProgramEnrollment"] != null)
                        {
                            course.TermCodeofProgramEnrollment = Convert.ToString(dataReader["TermCodeofProgramEnrollment"]);
                        }
                        if (dataReader["CourseNumber"] != null)
                        {
                            course.CourseNumber = Convert.ToString(dataReader["CourseNumber"]);
                        }
                        if (dataReader["CourseTitle"] != null)
                        {
                            course.CourseTitle = Convert.ToString(dataReader["CourseTitle"]);
                        }
                        if (dataReader["FinalGrade"] != null)
                        {
                            course.FinalGrade = Convert.ToString(dataReader["FinalGrade"]);
                        }
                        if (dataReader["TermCodeOfCourseEnrollment"] != null)
                        {
                            course.TermCodeOfCourseEnrollment = Convert.ToString(dataReader["TermCodeOfCourseEnrollment"]);
                        }
                        if (dataReader["MethodOfDelivery"] != null)
                        {
                            course.MethodOfDelivery = Convert.ToString(dataReader["MethodOfDelivery"]);
                        }
                        if (dataReader["ImportDate"] != null)
                        {
                            course.ImportDate = Convert.ToDateTime(dataReader["ImportDate"]);
                        }
                        if (dataReader["FileCreationDate"] != null)
                        {
                            course.FileCreationDate = Convert.ToDateTime(dataReader["FileCreationDate"]);
                        }

                        if (dataReader["FullTimeOrPartTimeIndicator"] != null && dataReader["FullTimeOrPartTimeIndicator"] != DBNull.Value)
                        {
                            course.TimeIndicator = Convert.ToString(dataReader["FullTimeOrPartTimeIndicator"]);
                        }
                        if (dataReader["CumulativeGPA"] != null && dataReader["CumulativeGPA"] != DBNull.Value)
                        {
                            course.CumulativeGPA = Convert.ToDouble(dataReader["CumulativeGPA"]);
                        }
                        if (dataReader["CreditsAttempted"] != null && dataReader["CreditsAttempted"] != DBNull.Value)
                        {
                            course.CreditAttempted = Convert.ToDouble(dataReader["CreditsAttempted"]);
                        }
                        if (dataReader["CreditsEarned"] != null && dataReader["CreditsEarned"] != DBNull.Value)
                        {
                            course.CreditEarned = Convert.ToDouble(dataReader["CreditsEarned"]);
                        }
                        if (dataReader["LatestCompassPrealgebraTestScore"] != null && dataReader["LatestCompassPrealgebraTestScore"] != DBNull.Value)
                        {
                            course.PrealgebraTestScore = Convert.ToString(dataReader["LatestCompassPrealgebraTestScore"]);
                        }
                        if (dataReader["LatestCompassAlgebraTestScore"] != null && dataReader["LatestCompassAlgebraTestScore"] != DBNull.Value)
                        {
                            course.CompassalgebraTestScore = Convert.ToString(dataReader["LatestCompassAlgebraTestScore"]);
                        }
                        if (dataReader["LatestCompassWritingTestScore"] != null && dataReader["LatestCompassWritingTestScore"] != DBNull.Value)
                        {
                            course.CompassWrittingTestScore = Convert.ToString(dataReader["LatestCompassWritingTestScore"]);
                        }
                        if (dataReader["LatestCompassReadingTestScore"] != null && dataReader["LatestCompassReadingTestScore"] != DBNull.Value)
                        {
                            course.CompassReadingTestScore = Convert.ToString(dataReader["LatestCompassReadingTestScore"]);
                        }
                        if (dataReader["LatestACTEnglishAssessmentScore"] != null && dataReader["LatestACTEnglishAssessmentScore"] != DBNull.Value)
                        {
                            course.EnglishAssessmentScore = Convert.ToString(dataReader["LatestACTEnglishAssessmentScore"]);
                        }
                        if (dataReader["LatestACTMathAssessmentScore"] != null && dataReader["LatestACTMathAssessmentScore"] != DBNull.Value)
                        {
                            course.MathAssessmentScore = Convert.ToString(dataReader["LatestACTMathAssessmentScore"]);
                        }
                        if (dataReader["LatestACTReadingAssessmentScore"] != null && dataReader["LatestACTReadingAssessmentScore"] != DBNull.Value)
                        {
                            course.ReadingAssessmentScore = Convert.ToString(dataReader["LatestACTReadingAssessmentScore"]);
                        }
                        if (dataReader["LatestACTScienceAssessmentScore"] != null && dataReader["LatestACTScienceAssessmentScore"] != DBNull.Value)
                        {
                            course.ScienceAssessmentScore = Convert.ToString(dataReader["LatestACTScienceAssessmentScore"]);
                        }
                        if (dataReader["LatestTestingDate"] != null && dataReader["LatestTestingDate"] != DBNull.Value)
                        {

                            course.LatestTestingDate = Convert.ToDateTime(dataReader["LatestTestingDate"]);
                        }
                        if (dataReader["HighSchoolName"] != null && dataReader["HighSchoolName"] != DBNull.Value)
                        {
                            course.HighSchoolName = Convert.ToString(dataReader["HighSchoolName"]);
                        }
                        if (dataReader["HighSchoolGraduationDate"] != null && dataReader["HighSchoolGraduationDate"] != DBNull.Value)
                        {
                            course.HighSchoolGraduationDate = Convert.ToDateTime(dataReader["HighSchoolGraduationDate"]);
                        }
                        if (dataReader["HomeTelephoneNumber"] != null && dataReader["HomeTelephoneNumber"] != DBNull.Value)
                        {
                            course.HomeTelephoneNumber = Convert.ToString(dataReader["HomeTelephoneNumber"]);
                        }
                        if (dataReader["MailingAddressLineOne"] != null && dataReader["MailingAddressLineOne"] != DBNull.Value)
                        {
                            course.AddressOne = Convert.ToString(dataReader["MailingAddressLineOne"]);
                        }
                        if (dataReader["MailingAddressLineTwo"] != null && dataReader["MailingAddressLineTwo"] != DBNull.Value)
                        {
                            course.AddressTwo = Convert.ToString(dataReader["MailingAddressLineTwo"]);
                        }
                        if (dataReader["MailingAddressLineThree"] != null && dataReader["MailingAddressLineThree"] != DBNull.Value)
                        {
                            course.AddressThree = Convert.ToString(dataReader["MailingAddressLineThree"]);
                        }
                        if (dataReader["City"] != null && dataReader["City"] != DBNull.Value)
                        {
                            course.City = Convert.ToString(dataReader["City"]);
                        }
                        if (dataReader["StateName"] != null && dataReader["StateName"] != DBNull.Value)
                        {
                            course.State = Convert.ToString(dataReader["StateName"]);
                        }
                        if (dataReader["ZipCode"] != null && dataReader["ZipCode"] != DBNull.Value)
                        {
                            course.ZIPCode = Convert.ToString(dataReader["ZipCode"]);
                        }
                        if (dataReader["EmailAddress"] != null && dataReader["EmailAddress"] != DBNull.Value)
                        {
                            course.EmailAddress = Convert.ToString(dataReader["EmailAddress"]);
                        }

                        if (dataReader["ImportDateFileCreationDate"] != null && dataReader["ImportDateFileCreationDate"] != DBNull.Value)
                        {
                            course.ImportDate_Student = Convert.ToDateTime(dataReader["ImportDateFileCreationDate"]);
                        }
                        if (dataReader["PreprogramIndicator"] != null && dataReader["PreprogramIndicator"] != DBNull.Value)
                        {
                            course.PreprogramIndicator = Convert.ToString(dataReader["PreprogramIndicator"]);
                        }
                        if (dataReader["MajorProgramEnrollmentName"] != null && dataReader["MajorProgramEnrollmentName"] != DBNull.Value)
                        {
                            course.MajorProgramEnrollment = Convert.ToString(dataReader["MajorProgramEnrollmentName"]);
                        }
                        course.NTO = Convert.ToString(dataReader["NTO"]);

                        course.MC = Convert.ToString(dataReader["MC"]);

                        course.PELL = Convert.ToString(dataReader["PELL"]);

                        course.RVP = Convert.ToString(dataReader["RVP"]);

                        course.ALLERT = Convert.ToString(dataReader["ALERT"]);
                        course.FileCreatedDate = Convert.ToDateTime(dataReader["FileCreationDate"]);
                #endregion


                        courses.Add(course);

                    }
                }
                totalRecords = (int)paramTotalRecords.Value;
            }

            return courses;
        }

    }



    protected void ButtonExcel_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable courseReportDt = Session["CourseReportDt"] as DataTable;
            string reportOIDStr = Request.QueryString["ReportOID"].ToString();

            ExportToExcel.ExportToSpreadsheet(courseReportDt, reportOIDStr, "Excel");
           
        }
        catch (Exception ex)
        { }
    }
    protected void ButtonPDF_Click(object sender, EventArgs e)
    {
        try
        {

            #region Session Declaration
            //Palash
            int BannerID = Convert.ToInt32(Session["BannerID"]);
            string NAME = Convert.ToString(Session["NAME"]);
            string StudentOID = Convert.ToString(Session["StudentOID"]);
            string TERM = Convert.ToString(Session["TERM"]);
            string CRSENO = Convert.ToString(Session["CRSENO"]);
            string CRSETITLE = Convert.ToString(Session["CRSETITLE"]);
            string FINALGRDE = Convert.ToString(Session["FINALGRDE"]);
            string CRSETERM = Convert.ToString(Session["CRSETERM"]);
            string DeliveryMethod = Convert.ToString(Session["DeliveryMethod"]);
            DateTime ImportDate_Course = Convert.ToDateTime(Session["ImportDate"]);
            int CourseOID = Convert.ToInt32(Session["CourseOID"]);
            DateTime FileCreationDate = Convert.ToDateTime(Session["FileCreationDate"]);




            string reportname = Convert.ToString(Session["reportname"]);
            string numberOfRows = Convert.ToString(Session["numberOfRows"]);
            string pageIndex = Convert.ToString(Session["pageIndex"]);

            string sortColumnName = Convert.ToString(Session["sortColumnName"]);
            string sortOrderBy = Convert.ToString(Session["sortOrderBy"]);





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
            int totalRecords;


            //Palash

            #endregion

            Collection<Course> courses = SearchCourse(numberOfRows, pageIndex, sortColumnName, sortOrderBy, out totalRecords, NAME, StudentOID, TERM, CRSENO, CRSETITLE, FINALGRDE, CRSETERM, DeliveryMethod, ImportDate, CourseOID, FileCreationDate, FullPart, GPA, CreditAttempted, EarnedCredit, Prealgebra, Algebra, Writting, Reading, English, Math, ReadingScore, ScienceScore, TestingDate, HighSchool, HS_GRAD_DATE, ADDR1, ADDR2, ADDR3, CITY, STATE, ZIP, ImportDate, PPGMIND, MAJOR, Email, Phone);


            //DataTable courseReportDt = Session["CourseReportDt"] as DataTable;
            DataTable courseReportDt = ConvertListToDataTable(courses);
            
           // DataTable courseReportDt = Session["CourseReportDt"] as DataTable;
            string reportOIDStr = Request.QueryString["ReportOID"].ToString();

            ExportToExcel.ExportToSpreadsheet(courseReportDt, reportOIDStr, "PDF");

        }
        catch (Exception ex)
        { }
    }

    protected void ButtonDelete_Click(object sender, EventArgs e)
    {
        DeleteReport(MenuURL);
        Response.Redirect("ReportDelete.aspx", false);
    }

    private void DeleteReport(string url)
    {


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
                    command.ExecuteNonQuery();
                }
                catch (Exception ae)
                { }

            }
        }

    }
}
