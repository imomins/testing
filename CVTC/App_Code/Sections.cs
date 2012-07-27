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
/// Summary description for Sections
/// </summary>
public class Sections
{
    string connectionString;
	public Sections()
	{
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();
	}
    public int SectionsOID
    { get; set; }

    public int AssessmentOID
    { get; set; }

    public string  SectionsName
    { get; set; }

    public string Response
    { get; set; }
    
    public int TotalQuestion
    { get; set; }

     public int PassingTotal
    { get; set; }

    public int TotalFlag
    { get; set; }

    public int FlagPointTotal
    { get; set; }
    public decimal Score
    { get; set; }
    public int Flag
    { get; set; }
    public int Low
    { get; set; }
    public int Medium
    { get; set; }
    public int High
    { get; set; }
    public DateTime  CreatedDate
    { get; set; }

    public int CreatedBy
    { get; set; }

    public DateTime  LastModifiedDate
    { get; set; }

    public int LastModifiedBy
    { get; set; }

    public int NumberOfPrinted
    { get; set; }

    public Collection<Sections> GetSectionsByAssessmentOID(int AOID)
    {
        Collection<Sections > SectionsList = new Collection<Sections >();
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Sections_ByAssessmentOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@AOID", AOID);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    //Question q = new Question();
                    Sections sections;
                    while (dataReader.Read())
                    {
                        sections  = new Sections();
                        sections.SectionsOID = Convert.ToInt32(dataReader["SectionOID"]);
                        sections.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        sections.SectionsName = Convert.ToString(dataReader["SectionName"]);
                        sections.TotalQuestion = Convert.ToInt32(dataReader["TotalQuestion"]);
                        sections.PassingTotal = Convert.ToInt32(dataReader["PassingTotal"]);
                        sections.TotalFlag = Convert.ToInt32(dataReader["TotalFlag"]);
                        sections.FlagPointTotal = Convert.ToInt32(dataReader["FlagPointTotal"]);
                        sections.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        sections.CreatedBy = Convert.ToInt32(dataReader["CreatedBy"]);
                        sections.LastModifiedDate = Convert.ToDateTime(dataReader["LastModifiedDate"]);
                        sections.LastModifiedBy = Convert.ToInt32(dataReader["LastModifiedBy"]);
                        SectionsList.Add(sections);
                       
                    }
                }

            }
        }
        return SectionsList;
    }

    public Collection<Sections> GetSectionsWithScoreByAOID(int AOID,string  StdID)
    {
        Collection<Sections> SectionsList = new Collection<Sections>();
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL SectionWithScore_ByAOID(?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@AOID", AOID);
                command.Parameters.AddWithValue("@STDOID", StdID);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    //Question q = new Question();
                    Sections sections;
                    while (dataReader.Read())
                    {
                        sections = new Sections();
                        sections.SectionsOID = Convert.ToInt32(dataReader["SectionOID"]);
                        
                        sections.SectionsName = Convert.ToString(dataReader["SectionName"]);
                        sections.Score = Convert.ToDecimal(dataReader["Rank"]);
                        if (dataReader["CreatedDate"] != DBNull.Value)
                        {
                            sections.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                        }
                        if (dataReader["NumberOfPrinted"] != DBNull.Value)
                        {
                            sections.NumberOfPrinted = Convert.ToInt32(dataReader["NumberOfPrinted"]);
                        }
                        SectionsList.Add(sections);

                    }
                }

            }
        }
        return SectionsList;
    }

    public Collection<Sections> GetSectionsWithNoScore(int AOID, string  StdID)
    {
        Collection<Sections> SectionsList = new Collection<Sections>();
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL SectionWithNoScore_ByAOID(?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@AOID", AOID);
                command.Parameters.AddWithValue("@STDOID", StdID);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    //Question q = new Question();
                    Sections sections;
                    while (dataReader.Read())
                    {
                        sections = new Sections();
                        sections.SectionsOID = Convert.ToInt32(dataReader["SectionOID"]);

                        //sections.SectionsName = Convert.ToString(dataReader["Question"]);
                        sections.SectionsName = Convert.ToString(dataReader["Keyword"]);
                        sections.Response = Convert.ToString(dataReader["Response"]);
                        SectionsList.Add(sections);

                    }
                }

            }
        }
        return SectionsList;
    }

    public int  GetAssessmentOIDByAssessmentName(string AOID)
    {
        int AssesmentOID=0;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL AssessmentOID_BYName(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@AOID", AOID);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    //Question q = new Question();
                   
                  if(dataReader.Read())
                    {
                        if (dataReader["AssessmentOID"] != null && dataReader["AssessmentOID"] != DBNull.Value && dataReader["AssessmentOID"] != "")
                        {
                            AssesmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        }
                        
                    }
                }

            }
        }
        return AssesmentOID;
    }


    public string  GetFirstAssessmentName()
    {
        string  AssesmentName = "";
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Assessment_First }";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
               // command.Parameters.AddWithValue("@AOID", AOID);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    //Question q = new Question();

                    if (dataReader.Read())
                    {
                        if (dataReader["AssessmentName"] != null && dataReader["AssessmentName"] != DBNull.Value && dataReader["AssessmentName"] != "")
                        {
                            AssesmentName = Convert.ToString(dataReader["AssessmentName"]);
                        }

                    }
                }

            }
        }
        return AssesmentName;
    }

    public int GetAssessmentOIDBySectionOID(int SectionOID)
    {
        int AssesmentOID = 0;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL AssessmentOID_BYSectionOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@SectionOID", SectionOID);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    //Question q = new Question();

                    if (dataReader.Read())
                    {
                        if (dataReader["AssessmentOID"] != null && dataReader["AssessmentOID"] != DBNull.Value && dataReader["AssessmentOID"] != "")
                        {
                            AssesmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        }

                    }
                }

            }
        }
        return AssesmentOID;
    }


    public bool DeleteSectionBySectionOID(int SectionOID)
    {
        bool result = false;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Section_DeleteBySectionOID(?)}";
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
}
