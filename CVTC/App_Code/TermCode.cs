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
/// Summary description for Course
/// </summary>
[Serializable] 
public class TermCode
{
    string connectionString;
    public TermCode()
	{
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();
	}
     #region Public Properties
    public int TermCodeOID
    { get; set; }

    public int AssessmentOID
    { get; set; }

    public string TermCodeName
    { get; set; }

      #endregion

    public TermCode GetTermCodeByTermCodeOID(int TCOID)
    {
        TermCode termCode = new TermCode();;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL TermCode_ByTermCodeOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@TCOID", TCOID);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    //Question q = new Question();
                    
                    while (dataReader.Read())
                    {
                        termCode = new TermCode();
                        termCode.TermCodeOID = Convert.ToInt32(dataReader["TermCodeOID"]);
                        termCode.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        termCode.TermCodeName = Convert.ToString(dataReader["TermCodeName"]);
                         
                    }
                }

            }
        }
        return termCode;
    }

    public Collection<TermCode> GetTermCodeByAssessmentOID(int AOID)
    {
        Collection<TermCode> termCodeList = new Collection<TermCode>();
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL TermCode_ByAssessmentOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@AOID", AOID);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    //Question q = new Question();
                    TermCode termCode;
                    while (dataReader.Read())
                    {
                        termCode = new TermCode();
                        termCode.TermCodeOID = Convert.ToInt32(dataReader["TermCodeOID"]);
                        termCode.AssessmentOID = Convert.ToInt32(dataReader["AssessmentOID"]);
                        termCode.TermCodeName = Convert.ToString(dataReader["TermCodeName"]);
                        termCodeList.Add(termCode);
                    }
                }

            }
        }
        return termCodeList;
    }


    public bool UpdateTermCode(TermCode termCode)
     { 
     bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                command.Connection = connection;
                command.CommandText = "{CALL TermCode_Update(?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;
                
                command.Parameters.AddWithValue("@TermCodeOID", termCode.TermCodeOID);
                command.Parameters.AddWithValue("@AssessmentOID", termCode.AssessmentOID);
                command.Parameters.AddWithValue("@TermCodeName", termCode.TermCodeName);
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
    public bool AddTermCode(TermCode termCode)
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                command.Connection = connection;
                command.CommandText = "{CALL TermCode_insert(?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@AssessmentOID", termCode.AssessmentOID);
                command.Parameters.AddWithValue("@TermCodeName", termCode.TermCodeName);
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
    public bool DeleteTermCode(int termCode)
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                command.Connection = connection;
                command.CommandText = "{CALL TermCode_Delete(?)}";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@TermCodeOID", termCode);
                 
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
         
}
