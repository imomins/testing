using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.Odbc;

public partial class pg_student_studentreport : System.Web.UI.Page
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
            lblError.Text = "";
            string reportOIDStr = Request.QueryString["ReportOID"].ToString();
            MenuURL = "pg/student/studentreport.aspx?ReportOID=" + reportOIDStr;
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

            string reportname = Convert.ToString(Session["reportname"]);
            string numberOfRows = Convert.ToString(Session["numberOfRows"]);
            string pageIndex = Convert.ToString(Session["pageIndex"]);

            string sortColumnName = Convert.ToString(Session["sortColumnName"]);
            string sortOrderBy = Convert.ToString(Session["sortOrderBy"]);
            int BannerID = Convert.ToInt32(Session["BannerID"]);


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


            
            //Collection<Student> students = SearchStudent(reportname, numberOfRows, pageIndex, sortColumnName, sortOrderBy, out totalRecords, Convert.ToInt32("0"), request["NAME"], request["StudentOID"], request["TERM"], request["FullPart"], Convert.ToDouble(request["GPA"]), Convert.ToDouble(request["CreditAttempted"]), Convert.ToDouble(request["EarnedCredit"]), request["Prealgebra"], request["Algebra"], request["Writting"], request["Reading"], request["English"], request["Math"], request["ReadingScore"], request["ScienceScore"], Convert.ToDateTime(request["TestingDate"]), request["HighSchool"], Convert.ToDateTime(request["HS_GRAD_DATE"]), request["ADDR1"], request["ADDR2"], request["ADDR3"], request["CITY"], request["STATE"], request["ZIP"], Convert.ToDateTime(request["ImportDate"]), request["PPGMIND"], request["MAJOR"], request["Email"], request["Phone"]);
            Collection<Student> students = SearchStudent(reportname, numberOfRows, pageIndex, sortColumnName, sortOrderBy, out totalRecords, Convert.ToInt32("0"), NAME, StudentOID, TERM, FullPart, GPA, CreditAttempted, EarnedCredit, Prealgebra, Algebra, Writting, Reading, English, Math, ReadingScore, ScienceScore,TestingDate, HighSchool,HS_GRAD_DATE, ADDR1, ADDR2, ADDR3, CITY, STATE, ZIP, ImportDate, PPGMIND, MAJOR, Email, Phone);
            DataTable studentReportDt = this.ConvertListToDataTable(students);

            string reportOIDStr = Request.QueryString["ReportOID"].ToString();

            ExportToExcel.ExportToSpreadsheet(studentReportDt, reportOIDStr, "CSV");

        }
        catch (Exception ex)
        { }

       
    }





    // Duplicate Method Start

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
            dt.Columns.Add("BirthDate");
            dt.Columns.Add("ALERT");
            dt.Columns.Add("RVP");
            dt.Columns.Add("PELL");
            dt.Columns.Add("MC");
            dt.Columns.Add("NTO");
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
                row["BirthDate"] = student.BirthDate.ToShortDateString();

                row["ALERT"] = (student.ALLERT == "0") ? "" : "ALERT";
                row["RVP"] = (student.RVP == "0") ? "" : "RVP";
                row["PELL"] = (student.PELL == "0") ? "" : "PELL";
                row["MC"] = (student.MC == "0") ? "" : "MC";
                row["NTO"] = (student.NTO == "0") ? "" : "NTO";

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
                command.CommandText = "{CALL Student_Search_Report(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)}";
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
                        if (dataReader["BirthDate"] != null)
                        {
                            student.BirthDate = Convert.ToDateTime(dataReader["BirthDate"]);
                        }
                        student.NTO = Convert.ToString(dataReader["NTO"]);

                        student.MC = Convert.ToString(dataReader["MC"]);

                        student.PELL = Convert.ToString(dataReader["PELL"]);

                        student.RVP = Convert.ToString(dataReader["RVP"]);

                        student.ALLERT = Convert.ToString(dataReader["ALERT"]);

                        student.FileCreatedDate = Convert.ToDateTime(dataReader["FileCreationDate"]);

                        students.Add(student);

                    }
                }
                totalRecords = (int)paramTotalRecords.Value;
            }

            return students;
        }
    }


    //Duplicate Method End


    protected void ButtonExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string reportType = "CSV";
            DataTable studentReportDt = Session["StudentReportDt"] as DataTable;
            string reportOIDStr = Request.QueryString["ReportOID"].ToString();
            Reports report = null;
            if (!string.IsNullOrEmpty(reportOIDStr))
            {
                report = new Reports().GetReportByOID(Int32.Parse(reportOIDStr));
            }
            if ((report == null) || (studentReportDt == null))
            {
                return;
            }
            else
            {
                String gridColumnsStr = Convert.ToString(report.GridColumns);
                String[] gridColumns = gridColumnsStr.Split('&');

                foreach (String gridColumn in gridColumns)
                {
                    String[] nameValue = gridColumn.Split('=');
                    if (nameValue[1] == "true")
                    {
                        try
                        {
                            studentReportDt.Columns.Remove(nameValue[0]);
                        }
                        catch
                        { }
                    }
                }
            }

            if (reportType == "CSV")
            {
                //Remove Comma from Text
                string tempStr = "";
                for (int i = 0; i < studentReportDt.Rows.Count; i++)
                {
                    for (int j = 0; j < studentReportDt.Columns.Count; j++)
                    {
                        tempStr = Convert.ToString(studentReportDt.Rows[i][j]);
                        if (tempStr.Contains(','))
                        {
                            studentReportDt.Rows[i][j] = tempStr.Replace(',', ' ');
                        }
                    }
                }

                //HttpContext context = HttpContext.Current;
                StringWriter sw = new StringWriter();
                //context.Response.Clear();
                //context.Response.ContentType = "text/csv";
                //context.Response.AddHeader("Content-Disposition", "attachment; filename=" + report.ReportName + ".csv");

                //Write a row for column names
                foreach (DataColumn dataColumn in studentReportDt.Columns)
                    sw.WriteLine(dataColumn.ColumnName + ",");
                    sw.WriteLine(Environment.NewLine);
                    //context.Response.Write(dataColumn.ColumnName + ",");
                //context.Response.Write(Environment.NewLine);

                //Write one row for each DataRow
                foreach (DataRow dataRow in studentReportDt.Rows)
                {

                    for (int dataColumnCount = 0; dataColumnCount < studentReportDt.Columns.Count; dataColumnCount++)
                        sw.WriteLine(dataRow[dataColumnCount].ToString() + ",");
                        sw.WriteLine(Environment.NewLine);
                        //context.Response.Write(dataRow[dataColumnCount].ToString() + ",");
                    //context.Response.Write(Environment.NewLine);
                }
                Response.AddHeader("Content-Disposition", "attachment; filename=" + report.ReportName + ".csv");
                Response.ContentType = "text/csv";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                Response.Write(sw);
                Response.End();
                //context.Response.End();
            }


           // ExportToExcel.ExportToSpreadsheet(studentReportDt, reportOIDStr, "Excel");
        }
        catch (Exception ex)
        { }
    }

    protected void ButtonPDF_Click(object sender, EventArgs e)
    {

        try
        {
            //DataTable studentReportDt = Session["StudentReportDt"] as DataTable;
            #region Session Declaration
            //Palash

            string reportname = Convert.ToString(Session["reportname"]);
            string numberOfRows = Convert.ToString(Session["numberOfRows"]);
            string pageIndex = Convert.ToString(Session["pageIndex"]);

            string sortColumnName = Convert.ToString(Session["sortColumnName"]);
            string sortOrderBy = Convert.ToString(Session["sortOrderBy"]);
            int BannerID = Convert.ToInt32(Session["BannerID"]);


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
            //Collection<Student> students = SearchStudent(reportname, numberOfRows, pageIndex, sortColumnName, sortOrderBy, out totalRecords, Convert.ToInt32("0"), request["NAME"], request["StudentOID"], request["TERM"], request["FullPart"], Convert.ToDouble(request["GPA"]), Convert.ToDouble(request["CreditAttempted"]), Convert.ToDouble(request["EarnedCredit"]), request["Prealgebra"], request["Algebra"], request["Writting"], request["Reading"], request["English"], request["Math"], request["ReadingScore"], request["ScienceScore"], Convert.ToDateTime(request["TestingDate"]), request["HighSchool"], Convert.ToDateTime(request["HS_GRAD_DATE"]), request["ADDR1"], request["ADDR2"], request["ADDR3"], request["CITY"], request["STATE"], request["ZIP"], Convert.ToDateTime(request["ImportDate"]), request["PPGMIND"], request["MAJOR"], request["Email"], request["Phone"]);
            Collection<Student> students = SearchStudent(reportname, numberOfRows, pageIndex, sortColumnName, sortOrderBy, out totalRecords, Convert.ToInt32("0"), NAME, StudentOID, TERM, FullPart, GPA, CreditAttempted, EarnedCredit, Prealgebra, Algebra, Writting, Reading, English, Math, ReadingScore, ScienceScore, TestingDate, HighSchool, HS_GRAD_DATE, ADDR1, ADDR2, ADDR3, CITY, STATE, ZIP, ImportDate, PPGMIND, MAJOR, Email, Phone);
            DataTable studentReportDt = this.ConvertListToDataTable(students);

            string reportOIDStr = Request.QueryString["ReportOID"].ToString();

            #region Erite To Disk


            // lblError.Visible = true;
            //lblError.Text = "CSV Try Starting...";
            try
            {

                // lblError.Visible = false;
                string reportType = "PDF";


                Reports report = null;
                if (!string.IsNullOrEmpty(reportOIDStr))
                {
                    report = new Reports().GetReportByOID(Int32.Parse(reportOIDStr));
                }

                if ((report == null) || (studentReportDt == null))
                {
                    lblError.Visible = true;
                    lblError.Text = "No Data...";
                    return;
                }
                else
                {
                    String gridColumnsStr = Convert.ToString(report.GridColumns);
                    String[] gridColumns = gridColumnsStr.Split('&');

                    foreach (String gridColumn in gridColumns)
                    {
                        String[] nameValue = gridColumn.Split('=');
                        if (nameValue[1] == "true")
                        {
                            try
                            {
                                studentReportDt.Columns.Remove(nameValue[0]);
                            }
                            catch
                            { }
                        }
                    }
                }

                if (reportType == "PDF")
                {
                    //Remove Comma from Text

                    lblError.Visible = true;
                    lblError.Text = "Report Type PDF Mode ...";
                    int count = 0;
                    string tempStr = "";
                    for (int i = 0; i < studentReportDt.Rows.Count; i++)
                    {
                        for (int j = 0; j < studentReportDt.Columns.Count; j++)
                        {
                            tempStr = Convert.ToString(studentReportDt.Rows[i][j]);
                            if (tempStr.Contains(','))
                            {
                                studentReportDt.Rows[i][j] = tempStr.Replace(',', ' ');
                            }
                        }
                    }


                    StringWriter sw = new StringWriter();

                    //Write a row for column names
                    if (count == 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (DataColumn dataColumn in studentReportDt.Columns)
                        {
                            sb.Append(dataColumn.ColumnName + ",");

                        }

                        sw.WriteLine(sb.ToString());
                    }
                    //Write one row for each DataRow

                    foreach (DataRow dataRow in studentReportDt.Rows)
                    {
                        count++;
                        StringBuilder sb1 = new StringBuilder();
                        for (int dataColumnCount = 0; dataColumnCount < studentReportDt.Columns.Count; dataColumnCount++)
                        {

                            sb1.Append(dataRow[dataColumnCount].ToString() + ",");
                        }
                        sw.WriteLine(sb1.ToString());

                    }

                    string path = @"C:\Export\ExportedData_PDF.pdf";
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }

                    // Create the file.
                    using (FileStream fs = File.Create(path, 1024))
                    {

                        //Byte[] info = new UTF8Encoding(true).GetBytes(sw.ToString());
                        Byte[] info = new UTF8Encoding(true).GetBytes("Palash");
                        // Add some information to the file.
                        fs.Write(info, 0, info.Length);

                    }


                }
                lblError.Visible = true;
                lblError.Text = "Successfully Exported";


            }
            catch (Exception ex)
            {
               
                lblError.Visible = true;
                lblError.Text = "Fatal error : " + ex.Message + ".";
            }

            #endregion

            ExportToExcel.ExportToSpreadsheet(studentReportDt, reportOIDStr, "PDF");

            
        }
        catch (Exception ex)
        { }



        #region Modified
        ////lblError.Visible = true;
        ////lblError.Text = "PDF Try Starting...";
        //try
        //{
            
        //    //lblError.Visible = false;
        //    string reportType = "CSV";
        //    DataTable studentReportDt = Session["StudentReportDt"] as DataTable;
        //    string reportOIDStr = Request.QueryString["ReportOID"].ToString();
        //    Reports report = null;
        //    if (!string.IsNullOrEmpty(reportOIDStr))
        //    {
        //        report = new Reports().GetReportByOID(Int32.Parse(reportOIDStr));
        //    }
        //    if ((report == null) || (studentReportDt == null))
        //    {
        //        lblError.Visible = true;
        //        lblError.Text = "No Data...";
        //        return;
        //    }
        //    else
        //    {
        //        String gridColumnsStr = Convert.ToString(report.GridColumns);
        //        String[] gridColumns = gridColumnsStr.Split('&');

        //        foreach (String gridColumn in gridColumns)
        //        {
        //            String[] nameValue = gridColumn.Split('=');
        //            if (nameValue[1] == "true")
        //            {
        //                try
        //                {
        //                    studentReportDt.Columns.Remove(nameValue[0]);
        //                }
        //                catch
        //                { }
        //            }
        //        }
        //    }

        //    if (reportType == "CSV")
        //    {
        //        //Remove Comma from Text
        //        int count = 0;
        //        string tempStr = "";
        //        for (int i = 0; i < studentReportDt.Rows.Count; i++)
        //        {
        //            for (int j = 0; j < studentReportDt.Columns.Count; j++)
        //            {
        //                tempStr = Convert.ToString(studentReportDt.Rows[i][j]);
        //                if (tempStr.Contains(','))
        //                {
        //                    studentReportDt.Rows[i][j] = tempStr.Replace(',', ' ');
        //                }
        //            }
        //        }


        //        StringWriter sw = new StringWriter();

        //        //Write a row for column names
        //        if (count == 0)
        //        {
        //            StringBuilder sb = new StringBuilder();
        //            foreach (DataColumn dataColumn in studentReportDt.Columns)
        //            {
        //                sb.Append(dataColumn.ColumnName + ",");

        //            }

        //            sw.WriteLine(sb.ToString());
        //        }
        //        //Write one row for each DataRow

        //        foreach (DataRow dataRow in studentReportDt.Rows)
        //        {
        //            count++;
        //            StringBuilder sb1 = new StringBuilder();
        //            for (int dataColumnCount = 0; dataColumnCount < studentReportDt.Columns.Count; dataColumnCount++)
        //            {

        //                sb1.Append(dataRow[dataColumnCount].ToString() + ",");
        //            }
        //            sw.WriteLine(sb1.ToString());

        //        }
        //        // Response.AddHeader("Content-Disposition", "attachment; filename=" + report.ReportName + ".csv");
        //        // Response.ContentType = "text/csv";
        //        //Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
        //        // Response.Write(sw);
        //        //  Response.End();
        //        string path = @"C:\Export\ExportedData_PDF.txt";
        //        if (File.Exists(path))
        //        {
        //            File.Delete(path);
        //        }

        //        // Create the file.
        //        using (FileStream fs = File.Create(path, 1024))
        //        {
        //            //sw.WriteLine("Student Name,XUID,Student Type,Status,Level,Semister,Year,Semister GPA,Overall GPA,Overall Hours,Major,Exp. Grad Date,Birth Date,Email");
        //            Byte[] info = new UTF8Encoding(true).GetBytes(sw.ToString());
        //            // Add some information to the file.
        //            fs.Write(info, 0, info.Length);
                    
        //        }
        //    }

        //    // ExportToExcel.ExportToSpreadsheet(studentReportDt, reportOIDStr, "CSV");
        //    lblError.Visible = true;
        //    lblError.Text = "Successfully Exported";

        //}
        //catch (Exception ex)
        //{

        //   // CreateLogFiles Err = new CreateLogFiles();
        //   // Err.ErrorLog(Server.MapPath("Logs/ErrorLog"), ex.Message);
        //    lblError.Visible = true;
        //    lblError.Text = "Fatal error : " + ex.Message + ".";
        //}
       #endregion

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

                OdbcParameter paramURL = new OdbcParameter("@url", OdbcType.VarChar,200);
                paramURL.Value = url;
                command.Parameters.Add(paramURL);

                connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch(Exception ae)
                { }
               
            }
        }
       
    }


    protected void ButtonDelete_Click(object sender, EventArgs e)
    {
        DeleteReport(MenuURL);
        lblError.Text = "Report has been Deleted Successfully..";
        Response.Redirect("ReportDelete.aspx", false);
    }
}
