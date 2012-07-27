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
public class Chart
{
    string connectionString;
    public Chart()
	{
        
		//
		// TODO: Add constructor logic here
		//
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();
	}
    
    #region Public Properties
    
    public string Label1
    { get; set; }

    public string Label2
    { get; set; }

    public string Label3
    { get; set; }


    public string Label4
    { get; set; }

    public string Label5
    { get; set; }

    public string Label6
    { get; set; }


    public string Label7
    { get; set; }

    public string Label8
    { get; set; }

    public string Label9
    { get; set; }


    public string Label10
    { get; set; }

    public string Label11
    { get; set; }

    public string Label12
    { get; set; }


    public string Label13
    { get; set; }

    public string Label14
    { get; set; }

    public string Label15
    { get; set; }



    public string Label16
    { get; set; }

    public string Label17
    { get; set; }

    public string Label18
    { get; set; }


    public string Label19
    { get; set; }

    public string Label20
    { get; set; }

    public string Label21
    { get; set; }


    public string Label22
    { get; set; }

    public string Label23
    { get; set; }

    public string Label24
    { get; set; }


   
    #endregion

    #region Public Method
      

    public Chart GetAllLabels()
    {
        //Answer_BySOIDAndAOID
        Chart ch = null;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Chart_GETLabels}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                //command.Parameters.AddWithValue("@BannerID", BannerID);
                
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {


                    if (dataReader.Read())
                    {
                        ch = new Chart();

                        ch.Label1 = dataReader["maxPrealgebra"].ToString();
                        ch.Label2 = dataReader["minPrealgebra"].ToString();
                        ch.Label3 = dataReader["avgPrealgebra"].ToString();

                        ch.Label4 = dataReader["maxAlgebra"].ToString();
                        ch.Label5 = dataReader["minAlgebra"].ToString();
                        ch.Label6 = dataReader["avgAlgebra"].ToString();

                        ch.Label7 = dataReader["maxWritting"].ToString();
                        ch.Label8 = dataReader["minWritting"].ToString();
                        ch.Label9 = dataReader["avgWritting"].ToString();

                        ch.Label10 = dataReader["maxReading"].ToString();
                        ch.Label11 = dataReader["minReading"].ToString();
                        ch.Label12 = dataReader["avgReading"].ToString();

                        ch.Label13 = dataReader["maxAssessmentEnglish"].ToString();
                        ch.Label14 = dataReader["minAssessmentEnglish"].ToString();
                        ch.Label15 = dataReader["avgAssessmentEnglish"].ToString();

                        ch.Label16 = dataReader["maxAssessmentMath"].ToString();
                        ch.Label17 = dataReader["minAssessmentMath"].ToString();
                        ch.Label18 = dataReader["avgAssessmentMath"].ToString();

                        ch.Label19 = dataReader["maxReadingAssessment"].ToString();
                        ch.Label20 = dataReader["minReadingAssessment"].ToString();
                        ch.Label21 = dataReader["avgReadingAssessment"].ToString();

                        ch.Label22 = dataReader["maxScienceAssessment"].ToString();
                        ch.Label23 = dataReader["minScienceAssessment"].ToString();
                        ch.Label24 = dataReader["avgScienceAssessment"].ToString();

                        


                    }
                }

            }
        }
        return ch;

    }

  
    #endregion


}




