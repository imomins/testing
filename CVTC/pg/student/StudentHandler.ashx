<%@ WebHandler Language="C#" Class="StudentHandler" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Data.Odbc;
using System.Web.SessionState;
using System.IO;
using  System.Text;
public class StudentHandler : IHttpHandler, IReadOnlySessionState
{

    string connectionString = "";   //@"Data Source=AETHER\SQLEXPRESS; Initial Catalog=cvtc; User ID=cvtcuser; Password=cvtcuser";
    public void ProcessRequest (HttpContext context) 
    {
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();

        HttpRequest request = context.Request;
        HttpResponse response = context.Response;
        string _search = request["_search"];
        
        string numberOfRows = request["rows"];
        string pageIndex = request["page"];
        string sortColumnName = request["sidx"];
        string sortOrderBy = request["sord"];
        string operation = request["oper"];
        string field = request["searchField"];
        string val = request["searchString"];

        string reportname = request["reportname"];
        string hiddenfields = request["hiddenfields"];
        string spparams = request.RawUrl.ToString();
        string isreport = request["isreport"];
        if (isreport == "yes")
        {
            _search = "true";
        }
        
 

        if ((!string.IsNullOrEmpty(reportname)) && (reportname != ""))
        {
            string[] stringSeparatorsasc = new string[] { "&sord=asc" };
            string[] stringSeparatorsdesc = new string[] { "&sord=desc" };

            if (spparams.Contains("&sord=asc"))
            {
                spparams = spparams.Split(stringSeparatorsasc, StringSplitOptions.None)[1];
            }
            else if (spparams.Contains("&sord=desc"))
            {
                spparams = spparams.Split(stringSeparatorsdesc, StringSplitOptions.None)[1];
            }
            else
            {
                spparams = "";
            }

            bool status = InsertSearchStudentReport(reportname, spparams, hiddenfields);
        }
        else if (_search == "true")
        {
            context.Session["reportname"] = reportname;
            context.Session["numberOfRows"] = numberOfRows;
            context.Session["pageIndex"] = pageIndex;
            context.Session["sortColumnName"] = sortColumnName;
            context.Session["sortOrderBy"] = request["sord"];
            context.Session["totalRecords"] = 0;
            context.Session["BannerID"] = Convert.ToInt32("0");
            context.Session["NAME"] = request["NAME"];
            context.Session["StudentOID"] = request["StudentOID"];
            context.Session["TERM"] = request["TERM"];
            context.Session["FullPart"] = request["FullPart"];
            context.Session["GPA"] = Convert.ToString(request["GPA"]);
            context.Session["CreditAttempted"] = Convert.ToString(request["CreditAttempted"]);
            context.Session["EarnedCredit"] = Convert.ToString(request["EarnedCredit"]);
            context.Session["Prealgebra"] = request["Prealgebra"];
            context.Session["Algebra"] = request["Algebra"];
            context.Session["Writting"] = request["Writting"];
            context.Session["Reading"] = request["Reading"];
            context.Session["English"] = request["English"];
            context.Session["Math"] = request["Math"];
            context.Session["ReadingScore"] = request["ReadingScore"];
            context.Session["ScienceScore"] = request["ScienceScore"];
            context.Session["TestingDate"] = Convert.ToDateTime(request["TestingDate"]);
            context.Session["HighSchool"] = request["HighSchool"];
            context.Session["HS_GRAD_DATE"] = Convert.ToDateTime(request["HS_GRAD_DATE"]);
            context.Session["ADDR1"] = request["ADDR1"];
            context.Session["ADDR2"] = request["ADDR2"];
            context.Session["ADDR3"] = request["ADDR3"];
            context.Session["CITY"] = request["CITY"];
            context.Session["STATE"] = request["STATE"];
            context.Session["ZIP"] = request["ZIP"];
            context.Session["ImportDate"] = Convert.ToDateTime(request["ImportDate"]);
            context.Session["PPGMIND"] = request["PPGMIND"];
            context.Session["MAJOR"] = request["MAJOR"];
            context.Session["Email"] = request["Email"];
            context.Session["Phone"] = request["Phone"];
            
            int totalRecords;
            
            //Collection<Student> students = SearchStudent(reportname, numberOfRows, pageIndex, sortColumnName, sortOrderBy, out totalRecords, Convert.ToInt32("0"), request["NAME"], request["StudentOID"], request["TERM"], request["FullPart"], Convert.ToDouble(request["GPA"]), Convert.ToDouble(request["CreditAttempted"]), Convert.ToDouble(request["EarnedCredit"]), request["Prealgebra"], request["Algebra"], request["Writting"], request["Reading"], request["English"], request["Math"], request["ReadingScore"], request["ScienceScore"], Convert.ToDateTime(request["TestingDate"]), request["HighSchool"], Convert.ToDateTime(request["HS_GRAD_DATE"]), request["ADDR1"], request["ADDR2"], request["ADDR3"], request["CITY"], request["STATE"], request["ZIP"], Convert.ToDateTime(request["ImportDate"]), request["PPGMIND"], request["MAJOR"], request["Email"], request["Phone"]);
            Collection<Student> students = SearchStudent(reportname, numberOfRows, pageIndex, sortColumnName, sortOrderBy, out totalRecords, Convert.ToInt32("0"), request["NAME"], request["StudentOID"], request["TERM"], request["FullPart"], Convert.ToString(request["GPA"]), Convert.ToString(request["CreditAttempted"]), Convert.ToString(request["EarnedCredit"]), request["Prealgebra"], request["Algebra"], request["Writting"], request["Reading"], request["English"], request["Math"], request["ReadingScore"], request["ScienceScore"], Convert.ToDateTime(request["TestingDate"]), request["HighSchool"], Convert.ToDateTime(request["HS_GRAD_DATE"]), request["ADDR1"], request["ADDR2"], request["ADDR3"], request["CITY"], request["STATE"], request["ZIP"], Convert.ToDateTime(request["ImportDate"]), request["PPGMIND"], request["MAJOR"], request["Email"], request["Phone"]);
            string output = BuildJQGridResults(students, Convert.ToInt32(numberOfRows), Convert.ToInt32(pageIndex), Convert.ToInt32(totalRecords));
            response.Write(output);

            //Add data to Session variable
           
               //context.Session["StudentReportDt"] = this.ConvertListToDataTable(students);
                      
        }
        else if (operation == "edit")
        {
            UpdateStudent(Convert.ToInt32(request["id"]), request["NAME"], request["BID"], request["TERM"], request["FullPart"], Convert.ToDouble(request["GPA"].ToString()), Convert.ToDouble(request["CreditAttempted"]), Convert.ToDouble(request["EarnedCredit"]), request["Prealgebra"], request["Algebra"], request["Writting"], request["Reading"], request["English"], request["Math"], request["ReadingScore"], request["ScienceScore"], Convert.ToDateTime(request["TestingDate"]), request["HighSchool"], Convert.ToDateTime(request["HS_GRAD_DATE"]), request["Phone"], request["ADDR1"], request["ADDR2"], request["ADDR3"], request["CITY"], request["STATE"], request["ZIP"], request["Email"], Convert.ToDateTime(request["ImportDate"]), request["PPGMIND"], request["MAJOR"] );
        }
        else
        {
            int totalRecords;
            Collection<Student> student = GetAllStudent(numberOfRows, pageIndex, sortColumnName, sortOrderBy, out totalRecords);
            string output = BuildJQGridResults(student, Convert.ToInt32(numberOfRows), Convert.ToInt32(pageIndex), Convert.ToInt32(totalRecords));

            response.Write(output);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
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

                row["StudentOID"]= student.StudentOID.ToString();
                row["NAME"]= student.FullName;
                row["BID"]= student.StudentID;
                row["TERM"]= student.ProgramEnrollment;
                row["FullPart"]= student.TimeIndicator;
                row["GPA"]= student.CumulativeGPA.ToString();
                row["CreditAttempted"]= student.CreditAttempted.ToString();
                row["EarnedCredit"]= student.CreditEarned.ToString();
                row["Prealgebra"]= student.PrealgebraTestScore;
                row["Algebra"]= student.CompassalgebraTestScore;
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
        
    private void UpdateStudent(int StudentOID, string BannerStudentName, string BannerStudentIDNumber, string TermCodeofProgramEnrollment, string FullTimeOrPartTimeIndicator, double CumulativeGPA, double CreditsAttempted, double CreditsEarned, string LatestCompassPrealgebraTestScore, string LatestCompassAlgebraTestScore, string LatestCompassWritingTestScore, string LatestCompassReadingTestScore, string LatestACTEnglishAssessmentScore, string LatestACTMathAssessmentScore, string LatestACTReadingAssessmentScore, string LatestACTScienceAssessmentScore, DateTime LatestTestingDate, string HighSchoolName, DateTime HighSchoolGraduationDate, string HomeTelephoneNumber, string MailingAddressLineOne, string MailingAddressLineTwo, string MailingAddressLineThree, string City, string StateName, string ZipCode, string EmailAddress, DateTime ImportDateFileCreationDate, string PreprogramIndicator, string MajorProgramEnrollmentName)
    {
        try
        { 
            Student std=new Student();
            //assign
            std.StudentOID=StudentOID;
            std.StudentID=BannerStudentIDNumber;
            std.FullName=BannerStudentName;
            std.ProgramEnrollment=TermCodeofProgramEnrollment;
            std.TimeIndicator=FullTimeOrPartTimeIndicator;
            std.CumulativeGPA=CumulativeGPA;
            std.CreditAttempted=CreditsAttempted;
            std.CreditEarned=CreditsEarned;
            std.PrealgebraTestScore=LatestCompassPrealgebraTestScore;
            std.CompassalgebraTestScore=LatestCompassAlgebraTestScore;
            std.CompassReadingTestScore=LatestCompassReadingTestScore;
            std.CompassWrittingTestScore=LatestCompassWritingTestScore;
            std.EnglishAssessmentScore=LatestACTEnglishAssessmentScore;
            std.MathAssessmentScore=LatestACTMathAssessmentScore;
            std.ReadingAssessmentScore=LatestACTReadingAssessmentScore;
            std.ScienceAssessmentScore=LatestACTScienceAssessmentScore;
            std.LatestTestingDate=LatestTestingDate;
            std.HighSchoolName=HighSchoolName;
            std.HighSchoolGraduationDate=HighSchoolGraduationDate;
            std.HomeTelephoneNumber=HomeTelephoneNumber;
            std.AddressOne=MailingAddressLineOne;
            std.AddressThree=MailingAddressLineThree;
            std.AddressTwo=MailingAddressLineTwo;
            std.City=City;
            std.State=StateName;
            std.ZIPCode=ZipCode;
            std.ImportDate=ImportDateFileCreationDate;
            std.EmailAddress=EmailAddress;
            std.PreprogramIndicator=PreprogramIndicator;
            std.MajorProgramEnrollment=MajorProgramEnrollmentName;
            
            
            std.UpdateStudent(std);
        }
        catch
        ( Exception ex)
        {}


    }
    
    private Collection<Student> GetAllStudent(string numberOfRows, string pageIndex, string sortColumnName, string sortOrderBy, out int totalRecords)
    {
       
        Collection<Student> students = new Collection<Student>();
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Student_SelectjqGrid(?,?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;

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

                connection.Open();
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    Student student;
                    while (dataReader.Read())
                    {
                        student = new Student();
                        student.StudentOID = Convert.ToInt32(dataReader["StudentOID"]);
                        if (dataReader["BannerStudentName"] != null && dataReader["BannerStudentName"] != DBNull.Value)
                        {
                            student.FullName = Convert.ToString(dataReader["BannerStudentName"]);
  
                        }

                        if (dataReader["BannerStudentIDNumber"] != null && dataReader["BannerStudentIDNumber"] != DBNull.Value)
                        {
                            student.StudentID = Convert.ToString(dataReader["BannerStudentIDNumber"]);
                        }
                        if (dataReader["TermCodeofProgramEnrollment"] != null && dataReader["TermCodeofProgramEnrollment"] != DBNull.Value)
                        {
                            student.ProgramEnrollment = Convert.ToString(dataReader["TermCodeofProgramEnrollment"]);
                        }
                        if (dataReader["FullTimeOrPartTimeIndicator"] != null && dataReader["FullTimeOrPartTimeIndicator"] != DBNull.Value)
                        {
                            student.TimeIndicator = Convert.ToString(dataReader["FullTimeOrPartTimeIndicator"]);
                        }
                        if (dataReader["CumulativeGPA"] != null && dataReader["CumulativeGPA"] != DBNull.Value)
                        {
                            student.CumulativeGPA = Convert.ToDouble(dataReader["CumulativeGPA"]);
                        }
                        if (dataReader["CreditsAttempted"] != null && dataReader["CreditsAttempted"] != DBNull.Value)
                        {
                            student.CreditAttempted = Convert.ToDouble(dataReader["CreditsAttempted"]);
                        }
                        if (dataReader["CreditsEarned"] != null && dataReader["CreditsEarned"] != DBNull.Value)
                        {
                            student.CreditEarned = Convert.ToDouble(dataReader["CreditsEarned"]);
                        }
                        if (dataReader["LatestCompassPrealgebraTestScore"] != null && dataReader["LatestCompassPrealgebraTestScore"] != DBNull.Value)
                        {
                            student.PrealgebraTestScore = Convert.ToString(dataReader["LatestCompassPrealgebraTestScore"]);
                        }
                        if (dataReader["LatestCompassAlgebraTestScore"] != null && dataReader["LatestCompassAlgebraTestScore"] != DBNull.Value)
                        {
                            student.CompassalgebraTestScore = Convert.ToString(dataReader["LatestCompassAlgebraTestScore"]);
                        }
                        if (dataReader["LatestCompassWritingTestScore"] != null && dataReader["LatestCompassWritingTestScore"] != DBNull.Value)
                        {
                            student.CompassWrittingTestScore = Convert.ToString(dataReader["LatestCompassWritingTestScore"]);
                        }
                        if (dataReader["LatestCompassReadingTestScore"] != null && dataReader["LatestCompassReadingTestScore"] != DBNull.Value)
                        {
                            student.CompassReadingTestScore = Convert.ToString(dataReader["LatestCompassReadingTestScore"]);
                        }
                        if (dataReader["LatestACTEnglishAssessmentScore"] != null && dataReader["LatestACTEnglishAssessmentScore"] != DBNull.Value)
                        {
                            student.EnglishAssessmentScore = Convert.ToString(dataReader["LatestACTEnglishAssessmentScore"]);
                        }
                        if (dataReader["LatestACTMathAssessmentScore"] != null && dataReader["LatestACTMathAssessmentScore"] != DBNull.Value)
                        {
                            student.MathAssessmentScore = Convert.ToString(dataReader["LatestACTMathAssessmentScore"]);
                        }
                        if (dataReader["LatestACTReadingAssessmentScore"] != null && dataReader["LatestACTReadingAssessmentScore"] != DBNull.Value)
                        {
                            student.ReadingAssessmentScore = Convert.ToString(dataReader["LatestACTReadingAssessmentScore"]);
                        }
                        if (dataReader["LatestACTScienceAssessmentScore"] != null && dataReader["LatestACTScienceAssessmentScore"] != DBNull.Value)
                        {
                            student.ScienceAssessmentScore = Convert.ToString(dataReader["LatestACTScienceAssessmentScore"]);
                        }
                        if (dataReader["LatestTestingDate"] != null && dataReader["LatestTestingDate"] != DBNull.Value)
                        {
                            
                            student.LatestTestingDate = Convert.ToDateTime(dataReader["LatestTestingDate"]);
                        }
                        if (dataReader["HighSchoolName"] != null && dataReader["HighSchoolName"] != DBNull.Value)
                        {
                            student.HighSchoolName = Convert.ToString(dataReader["HighSchoolName"]);
                        }
                        if (dataReader["HighSchoolGraduationDate"] != null && dataReader["HighSchoolGraduationDate"] != DBNull.Value)
                        {
                            student.HighSchoolGraduationDate = Convert.ToDateTime(dataReader["HighSchoolGraduationDate"]);
                        }
                        if (dataReader["HomeTelephoneNumber"] != null && dataReader["HomeTelephoneNumber"] != DBNull.Value)
                        {
                            student.HomeTelephoneNumber = Convert.ToString(dataReader["HomeTelephoneNumber"]);
                        }
                        if (dataReader["MailingAddressLineOne"] != null && dataReader["MailingAddressLineOne"] != DBNull.Value)
                        {
                            student.AddressOne = Convert.ToString(dataReader["MailingAddressLineOne"]);
                        }
                        if (dataReader["MailingAddressLineTwo"] != null && dataReader["MailingAddressLineTwo"] != DBNull.Value)
                        {
                            student.AddressTwo = Convert.ToString(dataReader["MailingAddressLineTwo"]);
                        }
                        if (dataReader["MailingAddressLineThree"] != null && dataReader["MailingAddressLineThree"] != DBNull.Value)
                        {
                            student.AddressThree = Convert.ToString(dataReader["MailingAddressLineThree"]);
                        }
                        if (dataReader["City"] != null && dataReader["City"] != DBNull.Value)
                        {
                            student.City = Convert.ToString(dataReader["City"]);
                        }
                        if (dataReader["StateName"] != null && dataReader["StateName"] != DBNull.Value)
                        {
                            student.State = Convert.ToString(dataReader["StateName"]);
                        }
                        if (dataReader["ZipCode"] != null && dataReader["ZipCode"] != DBNull.Value)
                        {
                            student.ZIPCode = Convert.ToString(dataReader["ZipCode"]);
                        }
                        if (dataReader["EmailAddress"] != null && dataReader["EmailAddress"] != DBNull.Value)
                        {
                            student.EmailAddress = Convert.ToString(dataReader["EmailAddress"]);
                        }
                        if (dataReader["ImportDateFileCreationDate"] != null && dataReader["ImportDateFileCreationDate"] != DBNull.Value)
                        {
                            student.ImportDate = Convert.ToDateTime(dataReader["ImportDateFileCreationDate"]);
                        }
                        if (dataReader["PreprogramIndicator"] != null && dataReader["PreprogramIndicator"] != DBNull.Value)
                        {
                            student.PreprogramIndicator = Convert.ToString(dataReader["PreprogramIndicator"]);
                        }
                        if (dataReader["MajorProgramEnrollmentName"] != null && dataReader["MajorProgramEnrollmentName"] != DBNull.Value)
                        {
                            student.MajorProgramEnrollment = Convert.ToString(dataReader["MajorProgramEnrollmentName"]);
                        }
                        if (dataReader["BirthDate"] != null && dataReader["BirthDate"] != DBNull.Value)
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

    //private Collection<Student> SearchStudent(string reportname, string numberOfRows, string pageIndex, string sortColumnName, string sortOrderBy, out int totalRecords, int StudentOID, string NAME, string ID, string TERM, string PFIND, double   GPA, double ATMPCR, double EARNCR, string C1, string C2, string CW, string CR, string AE, string AM, string AR, string SA, DateTime SDATE, string HSDESC, DateTime HS_GRAD_DATE, string ADDR1, string ADDR2, string ADDR3, string CITY, string STATE, string ZIP, DateTime FILEDATE, string PPGMIND, string MAJOR, string Email, string Phone)
    private Collection<Student> SearchStudent(string reportname, string numberOfRows, string pageIndex, string sortColumnName, string sortOrderBy, out int totalRecords, int StudentOID, string NAME, string ID, string TERM, string PFIND, string    GPA, string  ATMPCR, string  EARNCR, string C1, string C2, string CW, string CR, string AE, string AM, string AR, string SA, DateTime SDATE, string HSDESC, DateTime HS_GRAD_DATE, string ADDR1, string ADDR2, string ADDR3, string CITY, string STATE, string ZIP, DateTime FILEDATE, string PPGMIND, string MAJOR, string Email, string Phone)
      
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
      
        if (ATMPCR!=null &&ATMPCR !="")
        {
            if (ATMPCR.Contains("-"))
            {
                strCreditArr = ATMPCR.Split('-');
                StartCreditAttempted  = Convert.ToDouble(strCreditArr[0]);
                EndCreditAttempted  = Convert.ToDouble(strCreditArr[1]);
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
                paramATMPCR.Value = StartCreditAttempted ;
                command.Parameters.Add(paramATMPCR);


                OdbcParameter paramATMPCR1 = new OdbcParameter("@ENDATMPCR", OdbcType.VarChar, 15);
                paramATMPCR1.Value = EndCreditAttempted ;
                command.Parameters.Add(paramATMPCR1);
                
                

                OdbcParameter paramEARNCR = new OdbcParameter("@STARTEARNCR", OdbcType.VarChar, 15);
                paramEARNCR.Value = StartCreditEarned ;
                command.Parameters.Add(paramEARNCR);


                OdbcParameter paramEARNCR1 = new OdbcParameter("@ENDEARNCR", OdbcType.VarChar, 15);
                paramEARNCR1.Value = EndCreditEarned ;
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
                        if (dataReader["BannerStudentName"] != null || dataReader["BannerStudentName"] != DBNull.Value)
                        {
                            student.FullName = Convert.ToString(dataReader["BannerStudentName"]);

                        }

                        if (dataReader["BannerStudentIDNumber"] != null)
                        {
                            student.StudentID = Convert.ToString(dataReader["BannerStudentIDNumber"]);
                        }
                        if (dataReader["TermCodeofProgramEnrollment"] != null || dataReader["TermCodeofProgramEnrollment"] != DBNull.Value)
                        {
                            student.ProgramEnrollment = Convert.ToString(dataReader["TermCodeofProgramEnrollment"]);
                        }
                        if (dataReader["FullTimeOrPartTimeIndicator"] != null || dataReader["FullTimeOrPartTimeIndicator"] != DBNull.Value)
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
                        if (dataReader["LatestCompassPrealgebraTestScore"] != null || dataReader["LatestCompassPrealgebraTestScore"] != DBNull.Value)
                        {
                            student.PrealgebraTestScore = Convert.ToString(dataReader["LatestCompassPrealgebraTestScore"]);
                        }
                        if (dataReader["LatestCompassAlgebraTestScore"] != null || dataReader["LatestCompassAlgebraTestScore"] != DBNull.Value)
                        {
                            student.CompassalgebraTestScore = Convert.ToString(dataReader["LatestCompassAlgebraTestScore"]);
                        }
                        if (dataReader["LatestCompassWritingTestScore"] != null || dataReader["LatestCompassWritingTestScore"] != DBNull.Value)
                        {
                            student.CompassWrittingTestScore = Convert.ToString(dataReader["LatestCompassWritingTestScore"]);
                        }
                        if (dataReader["LatestCompassReadingTestScore"] != null || dataReader["LatestCompassReadingTestScore"] != DBNull.Value)
                        {
                            student.CompassReadingTestScore = Convert.ToString(dataReader["LatestCompassReadingTestScore"]);
                        }
                        if (dataReader["LatestACTEnglishAssessmentScore"] != null || dataReader["LatestACTEnglishAssessmentScore"] != DBNull.Value)
                        {
                            student.EnglishAssessmentScore = Convert.ToString(dataReader["LatestACTEnglishAssessmentScore"]);
                        }
                        if (dataReader["LatestACTMathAssessmentScore"] != null || dataReader["LatestACTMathAssessmentScore"] != DBNull.Value)
                        {
                            student.MathAssessmentScore = Convert.ToString(dataReader["LatestACTMathAssessmentScore"]);
                        }
                        if (dataReader["LatestACTReadingAssessmentScore"] != null || dataReader["LatestACTReadingAssessmentScore"] != DBNull.Value)
                        {
                            student.ReadingAssessmentScore = Convert.ToString(dataReader["LatestACTReadingAssessmentScore"]);
                        }
                        if (dataReader["LatestACTScienceAssessmentScore"] != null || dataReader["LatestACTScienceAssessmentScore"] != DBNull.Value)
                        {
                            student.ScienceAssessmentScore = Convert.ToString(dataReader["LatestACTScienceAssessmentScore"]);
                        }
                        if (dataReader["LatestTestingDate"] != null || dataReader["LatestTestingDate"] != DBNull.Value)
                        {
                            student.LatestTestingDate = Convert.ToDateTime(dataReader["LatestTestingDate"]);
                        }
                        if (dataReader["HighSchoolName"] != null || dataReader["HighSchoolName"] != DBNull.Value)
                        {
                            student.HighSchoolName = Convert.ToString(dataReader["HighSchoolName"]);
                        }
                        if (dataReader["HighSchoolGraduationDate"] != null || dataReader["HighSchoolGraduationDate"]!=DBNull .Value )
                        {
                            student.HighSchoolGraduationDate = Convert.ToDateTime(dataReader["HighSchoolGraduationDate"]);
                        }
                        if (dataReader["HomeTelephoneNumber"] != null || dataReader["HomeTelephoneNumber"] != DBNull.Value)
                        {
                            student.HomeTelephoneNumber = Convert.ToString(dataReader["HomeTelephoneNumber"]);
                        }
                        if (dataReader["MailingAddressLineOne"] != null || dataReader["MailingAddressLineOne"] != DBNull.Value)
                        {
                            student.AddressOne = Convert.ToString(dataReader["MailingAddressLineOne"]);
                        }
                        if (dataReader["MailingAddressLineTwo"] != null || dataReader["MailingAddressLineTwo"] != DBNull.Value)
                        {
                            student.AddressTwo = Convert.ToString(dataReader["MailingAddressLineTwo"]);
                        }
                        if (dataReader["MailingAddressLineThree"] != null || dataReader["MailingAddressLineThree"] != DBNull.Value)
                        {
                            student.AddressThree = Convert.ToString(dataReader["MailingAddressLineThree"]);
                        }
                        if (dataReader["City"] != null || dataReader["City"] != DBNull.Value)
                        {
                            student.City = Convert.ToString(dataReader["City"]);
                        }
                        if (dataReader["StateName"] != null || dataReader["StateName"] != DBNull.Value)
                        {
                            student.State = Convert.ToString(dataReader["StateName"]);
                        }
                        if (dataReader["ZipCode"] != null || dataReader["ZipCode"] != DBNull.Value)
                        {
                            student.ZIPCode = Convert.ToString(dataReader["ZipCode"]);
                        }
                        if (dataReader["EmailAddress"] != null || dataReader["EmailAddress"] != DBNull.Value)
                        {
                            student.EmailAddress = Convert.ToString(dataReader["EmailAddress"]);
                        }
                        if (dataReader["ImportDateFileCreationDate"] != null || dataReader["ImportDateFileCreationDate"] != DBNull.Value)
                        {
                            student.ImportDate = Convert.ToDateTime(dataReader["ImportDateFileCreationDate"]);
                        }
                        if (dataReader["PreprogramIndicator"] != null || dataReader["PreprogramIndicator"] != DBNull.Value)
                        {
                            student.PreprogramIndicator = Convert.ToString(dataReader["PreprogramIndicator"]);
                        }
                        if (dataReader["MajorProgramEnrollmentName"] != null || dataReader["MajorProgramEnrollmentName"] != DBNull.Value)
                        {
                            student.MajorProgramEnrollment = Convert.ToString(dataReader["MajorProgramEnrollmentName"]);
                        }
                        if (dataReader["BirthDate"] != null || dataReader["BirthDate"] != DBNull.Value)
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


    //private bool InsertSearchStudentReport(string reportname, string numberOfRows, string pageIndex, string sortColumnName, string sortOrderBy, out int totalRecords, int StudentOID, string NAME, string ID, string TERM, string PFIND, double GPA, double ATMPCR, double EARNCR, string C1, string C2, string CW, string CR, string AE, string AM, string AR, string SA, DateTime SDATE, string HSDESC, DateTime HS_GRAD_DATE, string ADDR1, string ADDR2, string ADDR3, string CITY, string STATE, string ZIP, DateTime FILEDATE, string PPGMIND, string MAJOR, string Email, string Phone)
    private bool InsertSearchStudentReport(string reportname, string spparams, string gridcolumns)//, string numberOfRows, string pageIndex, string sortColumnName, string sortOrderBy, out int totalRecords, int StudentOID, string NAME, string ID, string TERM, string PFIND, double GPA, double ATMPCR, double EARNCR, string C1, string C2, string CW, string CR, string AE, string AM, string AR, string SA, DateTime SDATE, string HSDESC, DateTime HS_GRAD_DATE, string ADDR1, string ADDR2, string ADDR3, string CITY, string STATE, string ZIP, DateTime FILEDATE, string PPGMIND, string MAJOR, string Email, string Phone)
    {
            bool result = false;
            
            int reportOID = 0;
            String ReportName = (reportname == null) ? "" : reportname;
            String SPName = "";
            String SPParams = (spparams == null) ? "" : spparams;
            String GridColumns = (gridcolumns == null) ? "" : gridcolumns;  
            DateTime CreatedDate = System.DateTime.Now;
        
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                using (OdbcCommand command = new OdbcCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "{CALL Reports_Insert(?,?,?,?,?,?)}";
                    command.CommandType = CommandType.StoredProcedure;

                    OdbcParameter returnParam = command.Parameters.Add("@ReportOID", OdbcType.Int);
                    returnParam.Direction = ParameterDirection.ReturnValue;

                    //command.Parameters.AddWithValue("@ReportOID",0);
                    command.Parameters.AddWithValue("@ReportName", ReportName);
                    command.Parameters.AddWithValue("@SPName", SPName);
                    command.Parameters.AddWithValue("@SPParams", SPParams);
                    command.Parameters.AddWithValue("@GridColumns", GridColumns);
                    command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

                    connection.Open();
                    int n = command.ExecuteNonQuery();
                    if (n == 1)
                        result = true;
                    else
                        result = false;
                    if (result)
                    {
                        reportOID = (int)command.Parameters["@ReportOID"].Value;
                        
                        //To Insert menu
                        CVTCMenu menu = new CVTCMenu();
                        menu.NameMenu = ReportName;
                        int menuId = new CVTCMenu().GetMaxMenuID();
                        menuId += 1;
                        menu.MenuID = menuId;
                        menu.URL = "pg/student/studentreport.aspx?ReportOID=" + reportOID;
                        menu.MenuLevel = 1;
                        menu.Parent = 6;
                        menu.IsExpanded = "true";
                        menu.IsLeave = "true";
                        menu.SaveAssessmentMenuItem(menu);                        
                    }
                    else
                    {
                        reportOID = 0;
                    }
                }
            }
        return true;
    }
    
    
    
    
    
    
        
    private string BuildJQGridResults(Collection<Student> students, int numberOfRows, int pageIndex, int totalRecords)
    {

        JQGridResults result = new JQGridResults();
        List<JQGridRow> rows = new List<JQGridRow>();
        foreach (Student student in students)
        {
            JQGridRow row = new JQGridRow();
            row.id = student.StudentOID ;
            row.cell = new string[36];
            row.cell[0] = student.StudentOID.ToString ();
            row.cell[1] = student.FullName;
            row.cell[2] = student.StudentID;
            row.cell[3] = student.ProgramEnrollment;
            row.cell[4] = student.TimeIndicator;
            row.cell[5] = student.CumulativeGPA.ToString ();
            row.cell[6] = student.CreditAttempted.ToString ();
            row.cell[7] = student.CreditEarned.ToString ();
            row.cell[8] = student.PrealgebraTestScore;
            row.cell[9] = student.CompassalgebraTestScore;
            row.cell[10] = student.CompassWrittingTestScore;
            row.cell[11] = student.CompassReadingTestScore;
            row.cell[12] = student.EnglishAssessmentScore;
            row.cell[13] = student.MathAssessmentScore;
            row.cell[14] = student.ReadingAssessmentScore;
            row.cell[15] = student.ScienceAssessmentScore;

            row.cell[16] = student.LatestTestingDate.ToShortDateString ();
            row.cell[17] = student.HighSchoolName;
            row.cell[18] = student.HighSchoolGraduationDate.ToShortDateString();
            row.cell[19] = student.HomeTelephoneNumber;
            row.cell[20] = student.AddressOne;
            row.cell[21] = student.AddressTwo;
            row.cell[22] = student.AddressThree;
            row.cell[23] = student.City;

            row.cell[24] = student.State;
            row.cell[25] = student.ZIPCode;
            row.cell[26] = student.EmailAddress;
            row.cell[27] = student.ImportDate.ToShortDateString ();
            row.cell[28] = student.PreprogramIndicator;
            row.cell[29] = student.MajorProgramEnrollment;
            row.cell[30] = student.BirthDate.ToShortDateString();
            
            if (student.ALLERT != "0")
            {
               
                row.cell[31] = "ALERT";
            }
            else
            {
                row.cell[31] = "";  
            }
            
            if (student.MC != "0")
            {
              
                row.cell[32] = "MC";
            }
            else
            {
                row.cell[32] = "";  
            }
            
            if (student.NTO != "0")
            {
                row.cell[33] = "NTO";
            }
            else
            {
               
                row.cell[33] = "";
            }
            
            if (student.PELL != "0")
            {
               
                row.cell[34] = "PELL";
            }
            else
            {
                row.cell[34] = "";
            }
            if (student.RVP != "0")
            {
              
                row.cell[35] = "RVP";
            }
            else
            {
                row.cell[35] = ""; 
            }
                                  
            rows.Add(row);
        }
        result.rows = rows.ToArray();
        result.page = pageIndex;
        result.total = totalRecords / numberOfRows;
        if (totalRecords % numberOfRows != 0) result.total += 1;
        result.records = totalRecords;
        return new JavaScriptSerializer().Serialize(result);
    }
    
}