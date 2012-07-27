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
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data.Odbc;


/// <summary>
/// Summary description for Student
/// </summary>
[Serializable]
public class Student
{
    string connectionString;
	public Student()
	{
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();
    }
    #region Public Properties

    public int RowID
    { get; set; }
  
    public int StudentOID
    { get; set; }

   
   
    public string FullName
    { get; set; }

    public string StudentID
    { get; set; }

    public string ProgramEnrollment
    { get; set; }

    public string TimeIndicator
    { get; set; }

    public double  CumulativeGPA
    { get; set; }

    public double CreditAttempted
    { get; set; }

    public double CreditEarned
    { get; set; }

    public string PrealgebraTestScore
    { get; set; }

    public string CompassalgebraTestScore
    { get; set; }

    public string CompassWrittingTestScore
    { get; set; }

    public string CompassReadingTestScore
    { get; set; }

    public string EnglishAssessmentScore
    { get; set; }

    public string MathAssessmentScore
    { get; set; }

    public string ReadingAssessmentScore
    { get; set; }

    public string ScienceAssessmentScore
    { get; set; }

    public DateTime LatestTestingDate
    { get; set; }

    public string HighSchoolName
    { get; set; }

    public  DateTime HighSchoolGraduationDate
    { get; set; }

    public string HomeTelephoneNumber
    { get; set; }

    public string AddressOne
    { get; set; }

    public string AddressTwo
    { get; set; }

    public string AddressThree
    { get; set; }

    public string City
    { get; set; }

    public string State
    { get; set; }

    public string ZIPCode
    { get; set; }

    public string EmailAddress
    { get; set; }

    public DateTime ImportDate
    { get; set; }

    public string  PreprogramIndicator
    { get; set; }

    public string MajorProgramEnrollment
    { get; set; }

    public DateTime FileCreatedDate
    { get; set; }
    public string  NTO
    { get; set; }

    public string  MC
    { get; set; }

    public string  PELL
    { get; set; }

    public string  RVP
    { get; set; }

    public string  ALLERT
    { get; set; }

    public int ResultLetterSentTimes
    { get; set; }

    public string Status
    { get; set; }

    public string LastName
    { get; set; }

    public string FirstName
    {
        get;
        set;
    }

    public string MiddleName
    {
        get;
        set;
    }

    public DateTime BirthDate
    {
        get;
        set;
    }

    public string PriorCreditQuestion
    {
        get;
        set;
    }
    #endregion

    public Student GetStudentByStudentOID(string  SOID)
    {
       // Collection<Student> studentList = new Collection<Student>();
        Student student = null ;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Student_ByStudentOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                if (SOID == "" || SOID == null)
                {
                    command.Parameters.AddWithValue("@SOID", DBNull.Value );
                }
                else
                {
                    command.Parameters.AddWithValue("@SOID", SOID);
                }
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                  
                    while (dataReader.Read())
                   
                    {
                        student = createStudentObjectForStudentProfile(dataReader);
                       
                        
                    }
                }

            }
        }
        return student;
    }

    public int GetStudentOIDByBannerID(string SOID)
    {
        // Collection<Student> studentList = new Collection<Student>();
        int student = 0;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Student_ByStudentOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@SOID", SOID);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    //Question q = new Question();
                    // Student  student;
                    while (dataReader.Read())
                    {
                        student =Convert .ToInt32 ( dataReader ["StudentOID"]);


                    }
                }

            }
        }
        return student;
    }

    public Collection<Student> GetAllStudentByStudentOID()
    {
        Collection<Student> studentList = new Collection<Student>();
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "AllStudent_ByStudentOID";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                //command.Parameters.AddWithValue("@SOID", SOID);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    //Question q = new Question();
                    Student student;
                    while (dataReader.Read())
                    {
                        studentList.Add(createStudentObject(dataReader));

                    }
                }

            }
        }
        return studentList;
    }
        
    public Collection<Student> GetAllStudentByTermCodeofProgramEnrollment(string termCodeofProgramEnrollment)
    {
        Collection<Student> studentList = new Collection<Student>();
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                command.Connection = connection;
                command.CommandText = "{CALL AllStudent_ByTermCodeofProgramEnrollment(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@TermCodeofProgramEnrollment", termCodeofProgramEnrollment);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    //Question q = new Question();
                    Student student;
                    while (dataReader.Read())
                    {
                        studentList.Add(createStudentObject(dataReader));
                    }
                }

            }
        }
        return studentList;
    }


    public  Student  createStudentObject(IDataReader dataReader)
    {
         

         Student student=null ;
         //while (dataReader.Read())
         //{
             student = new Student();
             student.StudentOID = Convert.ToInt32(dataReader["StudentOID"]);
             string strName = Convert.ToString(dataReader["BannerStudentName"]);
             string[] strfname = strName.Split(',');
             string strFirstName = strfname[1];
             string strLastname = strfname[0];
             student.LastName = strLastname;
             student.FirstName = strFirstName;
             student.FullName = strName;
             student.StudentID = Convert.ToString(dataReader["BannerStudentIDNumber"]);
             student.ProgramEnrollment = Convert.ToString(dataReader["TermCodeofProgramEnrollment"]);
             student.TimeIndicator = Convert.ToString(dataReader["FullTimeOrPartTimeIndicator"]);
             student.CumulativeGPA = Convert.ToDouble(dataReader["CumulativeGPA"]);
             student.CreditAttempted = Convert.ToDouble(dataReader["CreditsAttempted"]);
             student.CreditEarned = Convert.ToDouble(dataReader["CreditsEarned"]);
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
             if (dataReader["FileCreationDate"] != null && dataReader["FileCreationDate"] != DBNull.Value)
             {
                 student.FileCreatedDate = Convert.ToDateTime(dataReader["FileCreationDate"]);
             }
             if (dataReader["Status"] != null && dataReader["Status"] != DBNull.Value)
             {
                 student.Status = Convert.ToString(dataReader["Status"]);
             }

             if (dataReader.GetSchemaTable().Columns.Contains("MiddleName"))
             {
                 if (dataReader["MiddleName"] != null && dataReader["MiddleName"] != DBNull.Value)
                 {
                     student.MiddleName = Convert.ToString(dataReader["MiddleName"]);
                 }
             }

             if (dataReader.GetSchemaTable().Columns.Contains("LastName"))
             {
                 if (dataReader["LastName"] != null && dataReader["LastName"] != DBNull.Value)
                 {
                     student.LastName = Convert.ToString(dataReader["LastName"]);
                 }
             }

             if (dataReader.GetSchemaTable().Columns.Contains("FirstName"))
             {
                 if (dataReader["FirstName"] != null && dataReader["FirstName"] != DBNull.Value)
                 {
                     student.FirstName = Convert.ToString(dataReader["FirstName"]);
                 }
             }

             if (dataReader.GetSchemaTable().Columns.Contains("BirthDate"))
             {
                 if (dataReader["BirthDate"] != null && dataReader["BirthDate"] != DBNull.Value)
                 {
                     student.BirthDate = Convert.ToDateTime(dataReader["BirthDate"]);
                 }
             }


             if (dataReader.GetSchemaTable().Columns.Contains("NTO"))
             {
                 student.NTO = Convert.ToString(dataReader["NTO"]);
             }
        
             if (dataReader.GetSchemaTable().Columns.Contains("MC"))
             {
                 student.MC = Convert.ToString(dataReader["MC"]);
             }
             if (dataReader.GetSchemaTable().Columns.Contains("PELL"))
             {
                 student.PELL = Convert.ToString(dataReader["PELL"]);
             }
             if (dataReader.GetSchemaTable().Columns.Contains("RVP"))
             {
                 student.RVP = Convert.ToString(dataReader["RVP"]);
             }
             if (dataReader.GetSchemaTable().Columns.Contains("ALERT"))
             {
                 student.ALLERT = Convert.ToString(dataReader["ALERT"]);
             }
          
         return student ;
    }

    public Collection<Student> GetAllStudentByResultLetterSentTimes(int resultLetterSentTimes)
    {
        Collection<Student> studentList = new Collection<Student>();
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                command.Connection = connection;
                command.CommandText = "{CALL AllStudent_ByResultLetterSentTimes(?)}";//
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@ResultLetterSentTimes", resultLetterSentTimes);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    //Question q = new Question();
                    while (dataReader.Read())
                    {
                      studentList.Add(  createStudentObject(dataReader));
                    }
                }

            }
        }
        return studentList;
    }

    public bool UpdateAllStudentByResultLetterSentTimes(int resultLetterSentTimes)
    {
        bool result = false;
        Collection<Student> studentList = new Collection<Student>();
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                command.Connection = connection;
                command.CommandText = "{CALL AllStudent_Update_ByResultLetterSentTimes(?)}";//
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@ResultLetterSentTimes", resultLetterSentTimes);
                //Open connection
                connection.Open();
                int n = command.ExecuteNonQuery();
                if (n > 0)
                    result = true;
                else
                    result = false;
            }
        }
        return result;
    }


    public Student GetStudentByStudentBannerID(string BannerID)
    {
        //Collection<Student> studentList = new Collection<Student>();
        Student student=null;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Student_ByStudentBannerID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@BannerID", BannerID);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    //Question q = new Question();
                    //if(dataReader.Read())
                   while (dataReader.Read())
                    {

                        student = createStudentObjectForNew(dataReader);

                    }
                }

            }
        }
        return student;
    }

    public Student GetStudentByBannerIDAndStudentName(string BannerID, string StudentName)
    {
        //Collection<Student> studentList = new Collection<Student>();
        Student student = null;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Student_ByBannerAndStudentName(?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@BannerID", BannerID);
                command.Parameters.AddWithValue("@StudentName", StudentName);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    
                    while (dataReader.Read())
                    {
                        student = createStudentObject(dataReader);

                    }
                }

            }
        }
        return student;
    }


    public Student GetValidStudentsForAssessment(string BannerID, string FirstName,string MiddleName,string LastName,string Term,string  dob)
   // public Student GetValidStudentsForAssessment(string BannerID, string StudentName)
    {
        //Collection<Student> studentList = new Collection<Student>();
        Student student = null;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL GetValidStudents_ForAssessment(?,?,?,?,?,?)}";
                //command.CommandText = "{CALL GetValidStudents_ForAssessment(?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                if (BannerID == "")
                {
                    command.Parameters.AddWithValue("@BannerID", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@BannerID", BannerID);
                }
                command.Parameters.AddWithValue("@FirstName", FirstName);
                if (MiddleName == "")
                {
                    command.Parameters.AddWithValue("@MiddleName", DBNull.Value);   
                }
                else
                {
                    command.Parameters.AddWithValue("@MiddleName", MiddleName);
                }
                command.Parameters.AddWithValue("@LastName", LastName);
                if (Term == "")
                {
                    command.Parameters.AddWithValue("@Term", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@Term", Term);
                }

                #region Unused
                //if (Program == "")
                //{

                //    command.Parameters.AddWithValue("@Program", DBNull.Value);
                //}
                //else
                //{
                //    command.Parameters.AddWithValue("@Program", Program);
                //}
                //if (creditQuestion  == "")
                //{

                //    command.Parameters.AddWithValue("@creditQuestion", DBNull.Value);
                //}
                //else
                //{
                //    command.Parameters.AddWithValue("@creditQuestion", creditQuestion);
                //}
                #endregion

                if (dob == "")
                {

                    command.Parameters.AddWithValue("@DOB", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@DOB", dob);
                }
                            


                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                   
                    while (dataReader.Read())
                    {
                        student = createStudentObjectForNew(dataReader);

                    }
                }

            }
        }
        return student;
    }

    public int  GetStudentOIDByStudentBannerID(string BannerOID)
    {
        //Collection<Student> studentList = new Collection<Student>();
        Student student = null;
        int StudentOID=0;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                //StudentOID_ByStudentBannerID
                command.Connection = connection;
                command.CommandText = "{CALL StudentOID_ByStudentBannerID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                if (BannerOID == "" || BannerOID == null)
                {
                    command.Parameters.AddWithValue("@BannerOID", DBNull .Value );
                }
                else
                {
                    command.Parameters.AddWithValue("@BannerOID", BannerOID);
                }

                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    //Question q = new Question();
                    dataReader.Read();
                    //while (dataReader.Read())
                    {
                        student = new Student();
                        try
                        {
                            StudentOID =  Convert.ToInt32(dataReader["StudentOID"]);
                        }
                        catch (Exception ex)
                        {
                            StudentOID = 0;
                        }
                        // studentList.Add(student);

                    }
                }

            }
        }
        return StudentOID;
    }

  
    public string GetStudentIDByStudentBannerOID(int BannerOID)
    {
        //Collection<Student> studentList = new Collection<Student>();
        Student student = null;
        string StudentID = null;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL StudentID_ByStudentBannerOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@BannerOID", BannerOID);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    //Question q = new Question();
                    if (dataReader.Read())
                    //while (dataReader.Read())
                    {
                        student = new Student();
                        StudentID = Convert.ToString(dataReader["BannerStudentIDNumber"]);
                        // studentList.Add(student);

                    }
                }

            }
        }
        return StudentID;
    }



    public bool UpdateStudent(Student std)
    { 
    bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                
                command.Connection = connection;
                command.CommandText = "{CALL Student_Update(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@StudentOID", std.StudentOID);
                command.Parameters.AddWithValue("@BannerStudentName", std.FullName);
                command.Parameters.AddWithValue("@BannerStudentIDNumber", std.StudentID);
                command.Parameters.AddWithValue("@TermCodeofProgramEnrollment", std.ProgramEnrollment);
                command.Parameters.AddWithValue("@FullTimeOrPartTimeIndicator", std.TimeIndicator);
                command.Parameters.AddWithValue("@CumulativeGPA", std.CumulativeGPA);
                command.Parameters.AddWithValue("@CreditsAttempted", std.CreditAttempted);
                command.Parameters.AddWithValue("@CreditsEarned", std.CreditEarned);
                command.Parameters.AddWithValue("@LatestCompassPrealgebraTestScore", std.PrealgebraTestScore);
                command.Parameters.AddWithValue("@LatestCompassAlgebraTestScore", std.CompassalgebraTestScore);
                command.Parameters.AddWithValue("@LatestCompassWritingTestScore", std.CompassWrittingTestScore);
                command.Parameters.AddWithValue("@LatestCompassReadingTestScore", std.CompassReadingTestScore);
                command.Parameters.AddWithValue("@LatestACTEnglishAssessmentScore", std.EnglishAssessmentScore);
                command.Parameters.AddWithValue("@LatestACTMathAssessmentScore", std.MathAssessmentScore);
                command.Parameters.AddWithValue("@LatestACTReadingAssessmentScore", std.ReadingAssessmentScore);
                command.Parameters.AddWithValue("@LatestACTScienceAssessmentScore", std.ScienceAssessmentScore);
                command.Parameters.AddWithValue("@LatestTestingDate", std.LatestTestingDate);
                command.Parameters.AddWithValue("@HighSchoolName", std.HighSchoolName);
                command.Parameters.AddWithValue("@HighSchoolGraduationDate", std.HighSchoolGraduationDate);
                command.Parameters.AddWithValue("@HomeTelephoneNumber", std.HomeTelephoneNumber);
                command.Parameters.AddWithValue("@MailingAddressLineOne", std.AddressOne);
                command.Parameters.AddWithValue("@MailingAddressLineTwo", std.AddressTwo);
                command.Parameters.AddWithValue("@MailingAddressLineThree", std.AddressThree);
                command.Parameters.AddWithValue("@City", std.City);
                command.Parameters.AddWithValue("@StateName", std.State);
                command.Parameters.AddWithValue("@ZipCode", std.ZIPCode);
                command.Parameters.AddWithValue("@EmailAddress", std.EmailAddress);
                command.Parameters.AddWithValue("@ImportDateFileCreationDate", std.ImportDate);
                command.Parameters.AddWithValue("@PreprogramIndicator", std.PreprogramIndicator);
                command.Parameters.AddWithValue("@MajorProgramEnrollmentName", std.MajorProgramEnrollment);
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

    public bool UpdatePendingStudent(Student std)
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL PendingStudent_Update(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;


                command.Parameters.AddWithValue("@StudentOID", std.StudentOID);
                command.Parameters.AddWithValue("@BannerStudentName", std.FullName);
                command.Parameters.AddWithValue("@BannerStudentIDNumber", std.StudentID);
                command.Parameters.AddWithValue("@TermCodeofProgramEnrollment", std.ProgramEnrollment);
                command.Parameters.AddWithValue("@FullTimeOrPartTimeIndicator", std.TimeIndicator);
                command.Parameters.AddWithValue("@CumulativeGPA", std.CumulativeGPA);
                command.Parameters.AddWithValue("@CreditsAttempted", std.CreditAttempted);
                command.Parameters.AddWithValue("@CreditsEarned", std.CreditEarned);
                command.Parameters.AddWithValue("@LatestCompassPrealgebraTestScore", std.PrealgebraTestScore);
                command.Parameters.AddWithValue("@LatestCompassAlgebraTestScore", std.CompassalgebraTestScore);
                command.Parameters.AddWithValue("@LatestCompassWritingTestScore", std.CompassWrittingTestScore);
                command.Parameters.AddWithValue("@LatestCompassReadingTestScore", std.CompassReadingTestScore);
                command.Parameters.AddWithValue("@LatestACTEnglishAssessmentScore", std.EnglishAssessmentScore);
                command.Parameters.AddWithValue("@LatestACTMathAssessmentScore", std.MathAssessmentScore);
                command.Parameters.AddWithValue("@LatestACTReadingAssessmentScore", std.ReadingAssessmentScore);
                command.Parameters.AddWithValue("@LatestACTScienceAssessmentScore", std.ScienceAssessmentScore);
                command.Parameters.AddWithValue("@LatestTestingDate", std.LatestTestingDate);
                command.Parameters.AddWithValue("@HighSchoolName", std.HighSchoolName);
                command.Parameters.AddWithValue("@HighSchoolGraduationDate", std.HighSchoolGraduationDate);
                command.Parameters.AddWithValue("@HomeTelephoneNumber", std.HomeTelephoneNumber);
                command.Parameters.AddWithValue("@MailingAddressLineOne", std.AddressOne);
                command.Parameters.AddWithValue("@MailingAddressLineTwo", std.AddressTwo);
                command.Parameters.AddWithValue("@MailingAddressLineThree", std.AddressThree);
                command.Parameters.AddWithValue("@City", std.City);
                command.Parameters.AddWithValue("@StateName", std.State);
                command.Parameters.AddWithValue("@ZipCode", std.ZIPCode);
                command.Parameters.AddWithValue("@EmailAddress", std.EmailAddress);
                command.Parameters.AddWithValue("@ImportDateFileCreationDate", std.ImportDate);
                command.Parameters.AddWithValue("@PreprogramIndicator", std.PreprogramIndicator);
                command.Parameters.AddWithValue("@MajorProgramEnrollmentName", std.MajorProgramEnrollment);
                command.Parameters.AddWithValue("@Status", std.Status);
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

    public bool AddStudent(Student std)
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                

                command.Connection = connection;
                command.CommandText = "{CALL Student_Insert(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;


               // command.Parameters.AddWithValue("@StudentOID", std.StudentOID);
                command.Parameters.AddWithValue("@BannerStudentName", std.FullName);
                command.Parameters.AddWithValue("@BannerStudentIDNumber", std.StudentID);
                command.Parameters.AddWithValue("@TermCodeofProgramEnrollment", std.ProgramEnrollment);
                command.Parameters.AddWithValue("@FullTimeOrPartTimeIndicator", std.TimeIndicator);
                command.Parameters.AddWithValue("@CumulativeGPA", std.CumulativeGPA);
                command.Parameters.AddWithValue("@CreditsAttempted", std.CreditAttempted);
                command.Parameters.AddWithValue("@CreditsEarned", std.CreditEarned);
                command.Parameters.AddWithValue("@LatestCompassPrealgebraTestScore", std.PrealgebraTestScore);
                command.Parameters.AddWithValue("@LatestCompassAlgebraTestScore", std.CompassalgebraTestScore);
                command.Parameters.AddWithValue("@LatestCompassWritingTestScore", std.CompassWrittingTestScore);
                command.Parameters.AddWithValue("@LatestCompassReadingTestScore", std.CompassReadingTestScore);
                command.Parameters.AddWithValue("@LatestACTEnglishAssessmentScore", std.EnglishAssessmentScore);
                command.Parameters.AddWithValue("@LatestACTMathAssessmentScore", std.MathAssessmentScore);
                command.Parameters.AddWithValue("@LatestACTReadingAssessmentScore", std.ReadingAssessmentScore);
                command.Parameters.AddWithValue("@LatestACTScienceAssessmentScore", std.ScienceAssessmentScore);
                //command.Parameters.AddWithValue("@LatestTestingDate", std.LatestTestingDate);
                command.Parameters.AddWithValue("@HighSchoolName", std.HighSchoolName);
                command.Parameters.AddWithValue("@HighSchoolGraduationDate", std.HighSchoolGraduationDate);
                command.Parameters.AddWithValue("@HomeTelephoneNumber", std.HomeTelephoneNumber);
                command.Parameters.AddWithValue("@MailingAddressLineOne", std.AddressOne);
                command.Parameters.AddWithValue("@MailingAddressLineTwo", std.AddressTwo);
                command.Parameters.AddWithValue("@MailingAddressLineThree", std.AddressThree);
                command.Parameters.AddWithValue("@City", std.City);
                command.Parameters.AddWithValue("@StateName", std.State);
                command.Parameters.AddWithValue("@ZipCode", std.ZIPCode);
                command.Parameters.AddWithValue("@EmailAddress", std.EmailAddress);
                //command.Parameters.AddWithValue("@ImportDateFileCreationDate", std.ImportDate);
                //command.Parameters.AddWithValue("@PreprogramIndicator", std.PreprogramIndicator);
                command.Parameters.AddWithValue("@MajorProgramEnrollmentName", std.MajorProgramEnrollment);
                command.Parameters.AddWithValue("@Status", std.Status );

                command.Parameters.AddWithValue("@FirstName", std.FirstName);
                command.Parameters.AddWithValue("@LastName", std.LastName);
                command.Parameters.AddWithValue("@MiddleName", std.MiddleName);
                command.Parameters.AddWithValue("@BirthDate", std.BirthDate);
                command.Parameters.AddWithValue("@PriorCreditQuestion", std.PriorCreditQuestion);
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

    public Student GetStudentByOID(int SOID)
    {
        
        Student student = this;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL GET_StudentByOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@SOID", SOID);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    //Question q = new Question();
                    // Student  student;
                    // while (dataReader.Read())
                    if (dataReader.Read())
                    {
                        //student = new Student();
                        student.StudentOID = Convert.ToInt32(dataReader["StudentOID"]);
                        string strName = Convert.ToString(dataReader["BannerStudentName"]);
                        string[] strfname = strName.Split(',');
                        string strFirstName = strfname[1];
                        string strLastname = strfname[0];
                        student.LastName = strLastname;
                        student.FirstName = strFirstName;
                        student.FullName = strName;
                        student.StudentID = Convert.ToString(dataReader["BannerStudentIDNumber"]);
                        student.ProgramEnrollment = Convert.ToString(dataReader["TermCodeofProgramEnrollment"]);
                        student.TimeIndicator = Convert.ToString(dataReader["FullTimeOrPartTimeIndicator"]);
                        student.CumulativeGPA = Convert.ToDouble(dataReader["CumulativeGPA"]);
                        student.CreditAttempted = Convert.ToDouble(dataReader["CreditsAttempted"]);
                        student.CreditEarned = Convert.ToDouble(dataReader["CreditsEarned"]);
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
                        if (dataReader["FileCreationDate"] != null && dataReader["FileCreationDate"] != DBNull.Value)
                        {
                            student.FileCreatedDate = Convert.ToDateTime(dataReader["FileCreationDate"]);
                        }
                        //if (dataReader["Status"] != null && dataReader["Status"] != DBNull.Value)
                        //{
                        //    student.Status = Convert.ToString(dataReader["Status"]);
                        //}
                        student.NTO = Convert.ToString(dataReader["NTO"]);
                        student.MC = Convert.ToString(dataReader["MC"]);
                        student.PELL = Convert.ToString(dataReader["PELL"]);
                        student.RVP = Convert.ToString(dataReader["RVP"]);
                        student.ALLERT = Convert.ToString(dataReader["ALLERT"]);
                        //student.Status = Convert.ToString(dataReader["Status"]);
                        // studentList.Add(student);

                    }
                }

            }
        }
        return student;
    }


    public string  GetFirstStudentID()
    {
        //Collection<Student> studentList = new Collection<Student>();

        string  StudentID = "0";
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL StudentFirstID}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                //command.Parameters.AddWithValue("@BannerOID", BannerOID);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    //Question q = new Question();
                    if (dataReader.Read())
                    //while (dataReader.Read())
                    {

                        StudentID = Convert.ToString(dataReader["BannerStudentIDNumber"]);
                        // studentList.Add(student);

                    }
                }

            }
        }
        return StudentID;
    }


    public Collection<Student> GetAllStudentsByISSDate()
    {
        Collection<Student> studentList = new Collection<Student>();
        Student student = null ;
       
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL StudentFirstID}";
                command.CommandType = CommandType.StoredProcedure;

                
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    
                   
                    while (dataReader.Read())
                    {
                        student = new Student();
                        student.RowID  = Convert.ToInt32(dataReader["BannerStudentIDNumber"]);
                        studentList.Add(student);

                    }
                }

            }
        }
        return studentList;
    }


    public int GetStudentOIDByRowID(int RowID)
    {
        //Collection<Student> studentList = new Collection<Student>();
        Student student = null;
        int StudentOID = 0;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                //StudentOID_ByStudentBannerID
                command.Connection = connection;
                command.CommandText = "{CALL StudentByRowID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@RowID", RowID);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    //Question q = new Question();
                    dataReader.Read();
                    //while (dataReader.Read())
                    {
                        student = new Student();
                        try
                        {
                            StudentOID = Convert.ToInt32(dataReader["StudentOID"]);
                        }
                        catch (Exception ex)
                        {
                            StudentOID = 0;
                        }
                        // studentList.Add(student);

                    }
                }

            }
        }
        return StudentOID;
    }

    public int GetStudentOIDByRowID_Profile(int RowID)
    {
        //Collection<Student> studentList = new Collection<Student>();
        Student student = null;
        int StudentOID = 0;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                //StudentOID_ByStudentBannerID
                command.Connection = connection;
                command.CommandText = "{CALL StudentByRowID_Profile(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@RowID", RowID);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    //Question q = new Question();
                    dataReader.Read();
                    //while (dataReader.Read())
                    {
                        student = new Student();
                        try
                        {
                            StudentOID = Convert.ToInt32(dataReader["StudentOID"]);
                        }
                        catch (Exception ex)
                        {
                            StudentOID = 0;
                        }
                        // studentList.Add(student);

                    }
                }

            }
        }
        return StudentOID;
    }

    public string  GetStudentByRowID(int RowID)
    {
       

        string  BannerID =null ;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL StudentByRowID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@RowID", RowID);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                   
                    if (dataReader.Read())
                   
                    {

                        BannerID = Convert.ToString(dataReader["BannerStudentIDNumber"]);
                      

                    }
                }

            }
        }
        return BannerID;
    }


    public string GetStudentByRowID_Profile(int RowID)
    {


        string BannerID = null;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL StudentByRowID_Profile(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@RowID", RowID);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {

                    if (dataReader.Read())
                    {

                        BannerID = Convert.ToString(dataReader["BannerStudentIDNumber"]);


                    }
                }

            }
        }
        return BannerID;
    }

    public int GetStudentRowIDByBannerID(string  BannerID)
    {
        
        int rowID = 0;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
               
                command.Connection = connection;
                command.CommandText = "{CALL StudentRowIDByBannerID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@SOID", BannerID);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                                        
                    while (dataReader.Read())
                    {
                        
                        try
                        {
                            rowID = Convert.ToInt32(dataReader["RowNum"]);
                        }
                        catch (Exception ex)
                        {
                            rowID = 0;
                        }
                      

                    }
                }

            }
        }
        return rowID;
    }



    public int GetNextStudentOID(DateTime dtNext)
    {
        //Collection<Student> studentList = new Collection<Student>();

        int StudentID =0;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Student_Next(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@nextISSDate", dtNext);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    //Question q = new Question();
                    if (dataReader.Read())
                    //while (dataReader.Read())
                    {

                        StudentID = Convert.ToInt32(dataReader["StudentOID"]);
                        // studentList.Add(student);

                    }
                }

            }
        }
        return StudentID;
    }

    public int GetPrevStudentOID(DateTime dtPrev)
    {
        //Collection<Student> studentList = new Collection<Student>();

        int StudentID = 0;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Student_Prev(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@prevISSDate", dtPrev);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    //Question q = new Question();
                    if (dataReader.Read())
                    //while (dataReader.Read())
                    {

                        StudentID = Convert.ToInt32(dataReader["StudentOID"]);
                        // studentList.Add(student);

                    }
                }

            }
        }
        return StudentID;
    }

    public Collection<Student> GetProgramInterst()
    {
        Student program = null;
        Collection<Student> ProgramList = new Collection<Student>();
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Student_GetAllProgramInterest}";
                command.CommandType = CommandType.StoredProcedure;

                
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                  
                    while (dataReader.Read())
                    {

                        program = new Student();
                        if (dataReader["MajorProgramEnrollmentName"] != DBNull.Value && dataReader["MajorProgramEnrollmentName"] != "")
                        {
                            program.MajorProgramEnrollment = Convert.ToString(dataReader["MajorProgramEnrollmentName"]);
                        }
                        ProgramList.Add(program);

                    }
                }

            }
        }
        return ProgramList;
    }


    public Collection<Student> GetTerms()
    {
        Student program = null;
        Collection<Student> ProgramList = new Collection<Student>();
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Student_GetAllTerms}";
                command.CommandType = CommandType.StoredProcedure;


                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {

                    while (dataReader.Read())
                    {

                        program = new Student();
                        if (dataReader["TermCodeofProgramEnrollment"] != DBNull.Value && dataReader["TermCodeofProgramEnrollment"] != "")
                        {
                            program.ProgramEnrollment = Convert.ToString(dataReader["TermCodeofProgramEnrollment"]);
                        }
                        ProgramList.Add(program);

                    }
                }

            }
        }
        return ProgramList;
    }

    //Addedd By Mominul
    public bool AddNewStudent(Student std)
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {


                command.Connection = connection;
                command.CommandText = "{CALL Student_Insert(?,?,?,?,?,?,?,?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@BannerStudentName", std.FullName);
                command.Parameters.AddWithValue("@BannerStudentIDNumber", std.StudentID);
                command.Parameters.AddWithValue("@TermCodeofProgramEnrollment", std.ProgramEnrollment);
                command.Parameters.AddWithValue("@MajorProgramEnrollmentName", std.MajorProgramEnrollment);
                command.Parameters.AddWithValue("@Status", std.Status);
                command.Parameters.AddWithValue("@FirstName", std.FirstName);
                command.Parameters.AddWithValue("@LastName", std.LastName);
                command.Parameters.AddWithValue("@MiddleName", std.MiddleName);
                
                if (std.BirthDate != null)
                {
                    command.Parameters.AddWithValue("@BirthDate", std.BirthDate);
                }
                else
                {
                    command.Parameters.AddWithValue("@BirthDate", DBNull.Value );
                }
                command.Parameters.AddWithValue("@PriorCreditQuestion", std.PriorCreditQuestion);
                command.Parameters.AddWithValue("@Email", std.EmailAddress);
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

    //Addedd By Mominul
    public bool UpdateOldStudent(Student std,string status)
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Student_Update_Old(?,?,?,?,?,?,?,?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@StudentOID", std.StudentOID);
                command.Parameters.AddWithValue("@BannerStudentName", std.FullName);
                command.Parameters.AddWithValue("@BannerStudentIDNumber", std.StudentID);
                command.Parameters.AddWithValue("@TermCodeofProgramEnrollment", std.ProgramEnrollment);
                command.Parameters.AddWithValue("@MajorProgramEnrollmentName", std.MajorProgramEnrollment);
                command.Parameters.AddWithValue("@BirthDate", std.BirthDate );
                command.Parameters.AddWithValue("@FirstName", std.FirstName );
                command.Parameters.AddWithValue("@LastName", std.LastName );
                command.Parameters.AddWithValue("@MiddleName", std.MiddleName);
                command.Parameters.AddWithValue("@PriorCredit", std.PriorCreditQuestion );
                if (status == null || status == "")
                {
                    command.Parameters.AddWithValue("@Status", DBNull.Value);
                    
                }
                else 
                {
                    command.Parameters.AddWithValue("@Status", status);
                }
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


    public bool UpdateTempStudent(Student std,int StudentOID)
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Student_Update_Temp(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@StudentOID", StudentOID );
                command.Parameters.AddWithValue("@BannerStudentIDNumber", std.StudentID );
                command.Parameters.AddWithValue("@BannerStudentName", std.FullName);
               
                command.Parameters.AddWithValue("@TermCodeofProgramEnrollment", std.ProgramEnrollment );
                command.Parameters.AddWithValue("@MajorProgramEnrollmentName", std.MajorProgramEnrollment);
                command.Parameters.AddWithValue("@FullTimeOrPartTimeIndicator", std.TimeIndicator);
                command.Parameters.AddWithValue("@CumulativeGPA", std.CumulativeGPA);
                command.Parameters.AddWithValue("@CreditsAttempted", std.CreditAttempted);
                command.Parameters.AddWithValue("@CreditsEarned", std.CreditEarned);
                command.Parameters.AddWithValue("@LatestCompassPrealgebraTestScore", std.PrealgebraTestScore);
                command.Parameters.AddWithValue("@LatestCompassAlgebraTestScore", std.CompassalgebraTestScore);
                command.Parameters.AddWithValue("@LatestCompassWritingTestScore", std.CompassWrittingTestScore);
                command.Parameters.AddWithValue("@LatestCompassReadingTestScore", std.CompassReadingTestScore);
                command.Parameters.AddWithValue("@LatestACTEnglishAssessmentScore", std.EnglishAssessmentScore);
                command.Parameters.AddWithValue("@LatestACTMathAssessmentScore", std.MathAssessmentScore);
                command.Parameters.AddWithValue("@LatestACTReadingAssessmentScore", std.ReadingAssessmentScore);
                command.Parameters.AddWithValue("@LatestACTScienceAssessmentScore", std.ScienceAssessmentScore);
                command.Parameters.AddWithValue("@LatestTestingDate", std.LatestTestingDate);
                command.Parameters.AddWithValue("@HighSchoolName", std.HighSchoolName);
                command.Parameters.AddWithValue("@HighSchoolGraduationDate", std.HighSchoolGraduationDate);
                command.Parameters.AddWithValue("@HomeTelephoneNumber", std.HomeTelephoneNumber);
                command.Parameters.AddWithValue("@MailingAddressLineOne", std.AddressOne);
                command.Parameters.AddWithValue("@MailingAddressLineTwo", std.AddressTwo);
                command.Parameters.AddWithValue("@MailingAddressLineThree", std.AddressThree);
                command.Parameters.AddWithValue("@City", std.City);
                command.Parameters.AddWithValue("@StateName", std.State);
                command.Parameters.AddWithValue("@ZipCode", std.ZIPCode);
                command.Parameters.AddWithValue("@EmailAddress", std.EmailAddress);
                command.Parameters.AddWithValue("@ImportDateFileCreationDate", std.ImportDate);
                command.Parameters.AddWithValue("@PreprogramIndicator", std.PreprogramIndicator);
                
                if (std.BirthDate != null)
                {
                    command.Parameters.AddWithValue("@BirthDate", std.BirthDate);
                }
                else
                {
                    command.Parameters.AddWithValue("@BirthDate", DBNull .Value );
                }
                command.Parameters.AddWithValue("@FirstName", std.FirstName);
                command.Parameters.AddWithValue("@LastName", std.LastName);
                if (std.MiddleName != null)
                {
                    command.Parameters.AddWithValue("@MiddleName", std.MiddleName);
                }
                else
                {
                    command.Parameters.AddWithValue("@MiddleName", DBNull.Value);
                }
                if (std.PriorCreditQuestion != null)
                {
                    command.Parameters.AddWithValue("@PriorCredit", std.PriorCreditQuestion);
                }
                else
                {
                    command.Parameters.AddWithValue("@PriorCredit", DBNull.Value);
                }
                //command.Parameters.AddWithValue("@Status", std.Status );
                command.Parameters.AddWithValue("@Status",DBNull .Value );
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

    public bool UpdateTempStudentinAnswer(string  BannerID,string TempID)
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Answer_Update_Temp(?,?)}";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@StudentOID", BannerID);
                command.Parameters.AddWithValue("@TemID", TempID);
               
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

    public Collection<Student> GetStudentsForMerge(string bannerID)
    {
        Collection<Student> studentList = new Collection<Student>();
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                command.Connection = connection;
                command.CommandText = "{CALL Students_GetStudentsForMerge(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@bannerID", bannerID);
                
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                   
                    while (dataReader.Read())
                    {
                        studentList.Add(createStudentObjectForNew(dataReader));
                    }
                }

            }
        }
        return studentList;
    }

    public Student GetStudentByStudentOID(int SOID)
    {

        Student student = this;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL GET_StudentByOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@SOID", SOID);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    
                    if (dataReader.Read())
                    {

                        student = createStudentObjectForNew(dataReader);

                    }
                }

            }
        }
        return student;
    }

    public Student createStudentObjectForNew(IDataReader dataReader)
    {


        Student student = new Student();
        student.StudentOID = Convert.ToInt32(dataReader["StudentOID"]);
        string strName = Convert.ToString(dataReader["BannerStudentName"]);
        string[] strfname = strName.Split(',');
        string strFirstName = strfname[1];
        string strLastname = strfname[0];
        student.LastName = strLastname;
        student.FirstName = strFirstName;
        student.FullName = strName;
        student.StudentID = Convert.ToString(dataReader["BannerStudentIDNumber"]);
        student.ProgramEnrollment = Convert.ToString(dataReader["TermCodeofProgramEnrollment"]);
        student.TimeIndicator = Convert.ToString(dataReader["FullTimeOrPartTimeIndicator"]);
        student.CumulativeGPA = Convert.ToDouble(dataReader["CumulativeGPA"]);
        student.CreditAttempted = Convert.ToDouble(dataReader["CreditsAttempted"]);
        student.CreditEarned = Convert.ToDouble(dataReader["CreditsEarned"]);
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
        if (dataReader["FileCreationDate"] != null && dataReader["FileCreationDate"] != DBNull.Value)
        {
            student.FileCreatedDate = Convert.ToDateTime(dataReader["FileCreationDate"]);
        }
        if (dataReader["Status"] != null && dataReader["Status"] != DBNull.Value)
        {
            student.Status = Convert.ToString(dataReader["Status"]);
        }
        if (dataReader["FirstName"] != null && dataReader["FirstName"] != DBNull.Value)
        {
            student.FirstName = Convert.ToString(dataReader["FirstName"]);
        }

        if (dataReader["LastName"] != null && dataReader["LastName"] != DBNull.Value)
        {
            student.LastName = Convert.ToString(dataReader["LastName"]);
        }
        if (dataReader["MiddleName"] != null && dataReader["MiddleName"] != DBNull.Value)
        {
            student.MiddleName = Convert.ToString(dataReader["MiddleName"]);
        }
        if (dataReader["BirthDate"] != null && dataReader["BirthDate"] != DBNull.Value)
        {
            student.BirthDate = Convert.ToDateTime(dataReader["BirthDate"]);
        }
        if (dataReader["PriorCreditQuestion"] != null && dataReader["PriorCreditQuestion"] != DBNull.Value)
        {
            student.PriorCreditQuestion = Convert.ToString(dataReader["PriorCreditQuestion"]);
        }

        return student;
    }

    public Student createStudentObjectForStudentProfile(IDataReader dataReader)
    {


        Student student = new Student();
        student.StudentOID = Convert.ToInt32(dataReader["StudentOID"]);
        string strName = Convert.ToString(dataReader["BannerStudentName"]);
        string[] strfname = strName.Split(',');
        string strFirstName = strfname[1];
        string strLastname = strfname[0];
        student.LastName = strLastname;
        student.FirstName = strFirstName;
        student.FullName = strName;
        student.StudentID = Convert.ToString(dataReader["BannerStudentIDNumber"]);
        student.ProgramEnrollment = Convert.ToString(dataReader["TermCodeofProgramEnrollment"]);
        student.TimeIndicator = Convert.ToString(dataReader["FullTimeOrPartTimeIndicator"]);
        student.CumulativeGPA = Convert.ToDouble(dataReader["CumulativeGPA"]);
        student.CreditAttempted = Convert.ToDouble(dataReader["CreditsAttempted"]);
        student.CreditEarned = Convert.ToDouble(dataReader["CreditsEarned"]);
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
        if (dataReader["FileCreationDate"] != null && dataReader["FileCreationDate"] != DBNull.Value)
        {
            student.FileCreatedDate = Convert.ToDateTime(dataReader["FileCreationDate"]);
        }
        if (dataReader["Status"] != null && dataReader["Status"] != DBNull.Value)
        {
            student.Status = Convert.ToString(dataReader["Status"]);
        }
        if (dataReader["FirstName"] != null && dataReader["FirstName"] != DBNull.Value)
        {
            student.FirstName = Convert.ToString(dataReader["FirstName"]);
        }

        if (dataReader["LastName"] != null && dataReader["LastName"] != DBNull.Value)
        {
            student.LastName = Convert.ToString(dataReader["LastName"]);
        }
        if (dataReader["MiddleName"] != null && dataReader["MiddleName"] != DBNull.Value)
        {
            student.MiddleName = Convert.ToString(dataReader["MiddleName"]);
        }
        if (dataReader["BirthDate"] != null && dataReader["BirthDate"] != DBNull.Value)
        {
            student.BirthDate = Convert.ToDateTime(dataReader["BirthDate"]);
        }
        if (dataReader["PriorCreditQuestion"] != null && dataReader["PriorCreditQuestion"] != DBNull.Value)
        {
            student.PriorCreditQuestion = Convert.ToString(dataReader["PriorCreditQuestion"]);
        }
       
            student.NTO = Convert.ToString(dataReader["NTO"]);
       
            student.MC = Convert.ToString(dataReader["MC"]);
       
            student.PELL = Convert.ToString(dataReader["PELL"]);
        
            student.RVP = Convert.ToString(dataReader["RVP"]);
        
            student.ALLERT = Convert.ToString(dataReader["ALERT"]);
        
        return student;
    }

    public Student GetValidStudentsForMerging(string BannerID, string FirstName, string MiddleName, string LastName, string Term, string dob)
    
    {
    
        Student student = null;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL GetValidStudents_ForMerge(?,?,?,?,?,?)}";
                //command.CommandText = "{CALL GetValidStudents_ForAssessment(?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                if (BannerID == "")
                {
                    command.Parameters.AddWithValue("@BannerID", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@BannerID", BannerID);
                }
                command.Parameters.AddWithValue("@FirstName", FirstName);
                if (MiddleName == "")
                {
                    command.Parameters.AddWithValue("@MiddleName", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@MiddleName", MiddleName);
                }
                command.Parameters.AddWithValue("@LastName", LastName);
                if (Term == "")
                {
                    command.Parameters.AddWithValue("@Term", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@Term", Term);
                }
                
                if (dob == "")
                {

                    command.Parameters.AddWithValue("@DOB", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@DOB", DBNull.Value);
                    //command.Parameters.AddWithValue("@DOB", dob);
                }



                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {

                    while (dataReader.Read())
                    {
                        student = new Student();
                        student = createStudentObjectForNew(dataReader);

                    }
                }

            }
        }
        return student;
    }

}
