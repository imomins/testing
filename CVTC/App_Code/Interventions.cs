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
using System.Web.Script.Serialization;
using System.Collections.ObjectModel;
using System.Data.Odbc;

/// <summary>
/// Summary description for Interventions
/// </summary>
public class Interventions
{
    string connectionString;
	public Interventions()
	{
        connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();	
	}
    public int PrescriptionOID
    { get; set; }

     public int StudentOID
    { get; set; }

     public int UserOID
    { get; set; }

     public int DomainOID
    { get; set; }

     public int RiskOID
    { get; set; }
    public int InterventionOID
    { get; set; }
        

    //...For Intervention Table
    public string InterventionName
    { get; set; }

    public string UserName
    { get; set; }

    public string BannerID
    { get; set; }

    public string DomainName
    { get; set; }

    public string   LatestActionDate
    { get; set; }

    public string   LatestContact
    { get; set; }

     public int Internal
    { get; set; }

     public int Prescribed
    { get; set; }
     public string PrescribedULR
     { get; set; }

    public int Completed
    { get; set; }

    public int Email
    { get; set; }

    public int Telephone
    { get; set; }

    public int InPerson
    { get; set; }

     public int HandOff
    { get; set; }

    public string   CreatedDate
    { get; set; }

     public int   CreatedBy
    { get; set; }

     public string   LastModifiedDate
    { get; set; }

    public int   LastModifiedBy
    { get; set; }

    public string  Comment
    { get; set; }

    public int Urgent
    { get; set; }

    public int Participating
    { get; set; }

    public int Testing
    { get; set; }

    public string  ParticipatingURL
    { get; set; }

    public string CompletedURL
    { get; set; }
    public string HandOffURL
    { get; set; }
    public string EmailURL
    { get; set; }
    public string TelephoneURL
    { get; set; }
    public string InPersonURL
    { get; set; }

    public string FlagURL
    { get; set; }

    public string Unread
    { get; set; }

    public string StudentName
    { get; set; }

    public int AssessmentOID
    { get; set; }

    public string  AssessmentName
    { get; set; }


    public int AddInterventions()
    {
        //bool result = false;
        int OID = 0;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Interventions_insert(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                OdbcParameter returnParam = command.Parameters.Add("@OID", OdbcType.Int);
                //// set the direction flag so that it will be filled with the return value
                returnParam.Direction = ParameterDirection.ReturnValue;

                command.Parameters.AddWithValue("@RiskOID", RiskOID);

                OdbcParameter paramStudentOID = new OdbcParameter("@StudentOID", OdbcType.Int);
                paramStudentOID.Value = this.StudentOID;
                command.Parameters.Add(paramStudentOID);

               
                OdbcParameter paramUserOID = new OdbcParameter("@UserOID", OdbcType.Int);
                paramUserOID.Value = this.UserOID;
                command.Parameters.Add(paramUserOID);
                                
                OdbcParameter paramDomainOID = new OdbcParameter("@DomainOID", OdbcType.Int);
                paramDomainOID.Value = this.DomainOID;
                command.Parameters.Add(paramDomainOID);

                OdbcParameter paramInterventionOID = new OdbcParameter("@InterventionOID", OdbcType.Int);
                paramInterventionOID.Value = this.InterventionOID;
                command.Parameters.Add(paramInterventionOID);

                OdbcParameter paramLatestActionDate = new OdbcParameter("@LatestActionDate", OdbcType.DateTime);
                paramLatestActionDate.Value = this.LatestActionDate;
                command.Parameters.Add(paramLatestActionDate);

                OdbcParameter paramLatestContact = new OdbcParameter("@LatestContact", OdbcType.DateTime);
                paramLatestContact.Value = this.LatestContact;
                command.Parameters.Add(paramLatestContact);

                OdbcParameter paramInternal = new OdbcParameter("@Internal", OdbcType.Int);
                paramInternal.Value = this.Internal;
                command.Parameters.Add(paramInternal);

                OdbcParameter paramPrescribed = new OdbcParameter("@Prescribed", OdbcType.Int);
                paramPrescribed.Value = this.Prescribed;
                command.Parameters.Add(paramPrescribed);

                OdbcParameter paramCompleted = new OdbcParameter("@Completed", OdbcType.Int);
                paramCompleted.Value = this.Completed;
                command.Parameters.Add(paramCompleted);

                OdbcParameter paramEmail = new OdbcParameter("@Email", OdbcType.Int);
                paramEmail.Value = this.Email;
                command.Parameters.Add(paramEmail);

                OdbcParameter paramTelephone = new OdbcParameter("@Telephone", OdbcType.Int);
                paramTelephone.Value = this.Telephone;
                command.Parameters.Add(paramTelephone);

                OdbcParameter paramInPerson = new OdbcParameter("@InPerson", OdbcType.Int);
                paramInPerson.Value = this.InPerson;
                command.Parameters.Add(paramInPerson);

                OdbcParameter paramHandOff = new OdbcParameter("@HandOff", OdbcType.Int);
                paramHandOff.Value = this.HandOff;
                command.Parameters.Add(paramHandOff);

                
                //OdbcParameter paramLastModifiedBy = new OdbcParameter("@LastModifiedBy", OdbcType.Int);
                //paramLastModifiedBy.Value = this.LastModifiedBy;
                //command.Parameters.Add(paramLastModifiedBy);

                OdbcParameter paramComment = new OdbcParameter("@Comment", OdbcType.VarChar, 100);
                paramComment.Value = this.Comment;
                command.Parameters.Add(paramComment);

                OdbcParameter paramUrgent = new OdbcParameter("@Urgent", OdbcType.Int);
                paramUrgent.Value = this.Urgent;
                command.Parameters.Add(paramUrgent);

                OdbcParameter paramParticipating = new OdbcParameter("@Participating", OdbcType.Int);
                paramParticipating.Value = this.Participating;
                command.Parameters.Add(paramParticipating);

                OdbcParameter paramTesting = new OdbcParameter("@Testing", OdbcType.Int);
                paramTesting.Value = this.Testing;
                command.Parameters.Add(paramTesting);

                OdbcParameter paramCreatedBy = new OdbcParameter("@CreatedBy", OdbcType.Int);
                paramCreatedBy.Value = this.CreatedBy;
                command.Parameters.Add(paramCreatedBy);

                OdbcParameter paramAssessmentOID = new OdbcParameter("@AssessmentOID", OdbcType.Int);
                paramAssessmentOID.Value = this.AssessmentOID;
                command.Parameters.Add(paramAssessmentOID);


                connection.Open();
                int n = command.ExecuteNonQuery();
                //command.CommandText = "Assessment_RefUpdate";
                //command.ExecuteNonQuery();
                OID = (int)command.Parameters["@OID"].Value;
                PrescriptionOID = OID;
            }
        }

        return OID;
    }


    public void  AddDomainInterventions()
    {
        //bool result = false;
        int OID = 0;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL DomainInterventions_insert(?,?)}";
                command.CommandType = CommandType.StoredProcedure;

               
                OdbcParameter paramDomainOID = new OdbcParameter("@DomainOID", OdbcType.Int);
                paramDomainOID.Value = this.DomainOID;
                command.Parameters.Add(paramDomainOID);

                OdbcParameter paramInterventionName = new OdbcParameter("@InterventionName", OdbcType.VarChar,100);
                paramInterventionName.Value = this.InterventionName;
                command.Parameters.Add(paramInterventionName);

                
                connection.Open();
                int n = command.ExecuteNonQuery();
               
            }
        }

       
    }

    public void AddDomain(string DomaiName)
    {
        //bool result = false;
        int OID = 0;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Domain_insert(?)}";
                command.CommandType = CommandType.StoredProcedure;

                OdbcParameter paramInterventionName = new OdbcParameter("@DomainName", OdbcType.VarChar, 100);
                paramInterventionName.Value = DomaiName;
                command.Parameters.Add(paramInterventionName);


                connection.Open();
                int n = command.ExecuteNonQuery();

            }
        }


    }

    public Collection <Interventions> GetInterventionByStudentOID(int SOID, string pageIndex, string numberOfRows,int UserOID)
        {
            //Interventions u = null;
            Collection<Interventions> invList = new Collection<Interventions>();
            // string connectionString = @"Data Source=.\SQLEXPRESS; Initial Catalog=cvtc; User ID=cvtcuser; Password=cvtcuser";
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                using (OdbcCommand command = new OdbcCommand())
                {

                    command.Connection = connection;
                    command.CommandText = "{CALL Intervations_ByStudentOID(?,?,?)}";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@SOID", SOID);

                    OdbcParameter paramPageIndex = new OdbcParameter("@PageIndex", OdbcType.Int);
                    paramPageIndex.Value = Convert.ToInt32(pageIndex);
                    command.Parameters.Add(paramPageIndex);

                    OdbcParameter paramNumberOfRows = new OdbcParameter("@NumberOfRows", OdbcType.Int);
                    paramNumberOfRows.Value = Convert.ToInt32(numberOfRows);
                    command.Parameters.Add(paramNumberOfRows);

                    connection.Open();
                    using (OdbcDataReader dataReader = command.ExecuteReader())
                    {
                        Interventions u;
                        while (dataReader.Read())
                        {
                            u = new Interventions();
                            u.PrescriptionOID = (int)dataReader["PrescriptionOID"];
                            u.StudentOID = (int)dataReader["StudentOID"];
                            u.UserOID = (int)dataReader["UserOID"];
                            u.UserName = (string )dataReader["UserName"];
                            u.DomainOID = (int)dataReader["DomainOID"];
                            u.DomainName = (string)dataReader["DomainName"];
                            u.InterventionOID = (int)dataReader["InterventionOID"];
                            u.InterventionName = (string )dataReader["InterventionName"];
                            u.Participating = (int)dataReader["Participating"];
                            if (dataReader["LatestActionDate"] != null)
                            {
                                u.LatestActionDate = Convert.ToDateTime(dataReader["LatestActionDate"]).ToShortDateString();
                            }
                            if (dataReader["LatestContact"] != null)
                            {
                                u.LatestContact = Convert.ToDateTime(dataReader["LatestContact"]).ToShortDateString();
                            }
                            u.Internal = (int)dataReader["Internal"];
                            u.Prescribed = (int)dataReader["Prescribed"];
                            u.Completed = (int)dataReader["Completed"];
                            u.Email = (int)dataReader["Email"];
                            u.Telephone = (int)dataReader["Telephone"];
                            u.InPerson = (int)dataReader["InPerson"];
                            u.HandOff = (int)dataReader["HandOff"];
                            if (dataReader["CreatedDate"] != null)
                            {
                                u.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]).ToShortDateString();
                            }
                            u.CreatedBy = (int)dataReader["CreatedBy"];
                            if (dataReader["LastModifiedDate"] != null)
                            {
                                u.LastModifiedDate = Convert.ToDateTime(dataReader["LastModifiedDate"]).ToShortDateString();
                            }
                            u.LastModifiedBy = (int)dataReader["LastModifiedBy"];
                            u.Comment = (string)dataReader["Comment"];
                            u.BannerID = (string)dataReader["BannerID"];
                            u.PrescribedULR = (u.Prescribed == 1) ? "~/images/Actions-dialog-ok-icon.png" : "~/images/X-Au-Blu-icon.png";
                            u.CompletedURL = (u.Completed == 1) ? "~/images/Actions-dialog-ok-icon.png" : "~/images/X-Au-Blu-icon.png";
                            u.HandOffURL = (u.HandOff == 1) ? "~/images/Actions-dialog-ok-icon.png" : "~/images/X-Au-Blu-icon.png";
                            u.EmailURL = (u.Email == 1) ? "~/images/Actions-dialog-ok-icon.png" : "~/images/X-Au-Blu-icon.png";
                            u.TelephoneURL = (u.Telephone == 1) ? "~/images/Actions-dialog-ok-icon.png" : "~/images/X-Au-Blu-icon.png";
                            u.InPersonURL = (u.InPerson == 1) ? "~/images/Actions-dialog-ok-icon.png" : "~/images/X-Au-Blu-icon.png";
                            u.ParticipatingURL = (u.Participating == 1) ? "~/images/Actions-dialog-ok-icon.png" : "~/images/X-Au-Blu-icon.png";


                            u.Unread =dataReader ["Unread"].ToString ();
                            if (u.HandOff == 1 && !CheckUser(u.PrescriptionOID ,UserOID ))
                            {
                                u.FlagURL = "~/images/flag.png";
                            }
                            else if (u.HandOff == 0 && !CheckUser(u.PrescriptionOID, UserOID))
                            {
                                u.FlagURL = "~/images/flag.png";
                            }
                            else
                            {
                                u.FlagURL = "~/images/blank.png";
                            }
                            //u.PrescribedULR = (u.Prescribed == 1) ? "~/images/Actions-dialog-ok-icon.pngs" : "~/images/X-Au-Blu-icon.png";
                            //u.PrescribedULR = (u.Prescribed == 1) ? "~/images/Actions-dialog-ok-icon.pngs" : "~/images/X-Au-Blu-icon.png";

                            invList.Add(u );

                        }
                    }

                }
            }
            return invList;
        }


    private bool CheckUser(int poid,int UserOID)
    {
        bool returnType = false;
        
        try
        {
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                using (OdbcCommand command = new OdbcCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "{CALL PrescriptionStatus_Check(?,?)}";
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.AddWithValue("@UserOID", UserOID);
                    command.Parameters.AddWithValue("@PrescriptionOID", poid);
                    connection.Open();

                    using (OdbcDataReader dataReader = command.ExecuteReader())
                    {


                        if (dataReader.Read())
                        {
                            returnType = true;
                        }
                    }

                }
            }


        }
        catch (Exception ax)
        {

        }
        return returnType;
    }

    public Interventions GetInterventionByRiskOID(int ROID, string StudentBannerID, string pageIndex, string numberOfRows,int UserOID)
    {
        Interventions u = null;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Intervations_ByRiskOID(?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                //command.Parameters.AddWithValue("@ROID", ROID);

                OdbcParameter paramROID = new OdbcParameter("@ROID", OdbcType.Int);
                paramROID.Value = Convert.ToInt32(ROID);
                command.Parameters.Add(paramROID);


               // command.Parameters.AddWithValue("@StudentBannerID", StudentBannerID);

                OdbcParameter paramStudentBannerID = new OdbcParameter("@StudentBannerID", OdbcType.VarChar, 20);
                paramStudentBannerID.Value = StudentBannerID;
                command.Parameters.Add(paramStudentBannerID);


                OdbcParameter paramPageIndex = new OdbcParameter("@PageIndex", OdbcType.Int);
                paramPageIndex.Value = Convert.ToInt32(pageIndex);
                command.Parameters.Add(paramPageIndex);

                OdbcParameter paramNumberOfRows = new OdbcParameter("@NumberOfRows", OdbcType.Int);
                paramNumberOfRows.Value = Convert.ToInt32(numberOfRows);
                command.Parameters.Add(paramNumberOfRows);

                connection.Open();
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                  
                    while (dataReader.Read())
                    {
                        u = new Interventions();
                        u.RiskOID = (int)dataReader["RiskOID"];
                        u.PrescriptionOID = (int)dataReader["PrescriptionOID"];
                        u.StudentOID = (int)dataReader["StudentOID"];
                        u.UserOID = (int)dataReader["UserOID"];
                        u.UserName = (string)dataReader["UserName"];
                        u.DomainOID = (int)dataReader["DomainOID"];
                        u.DomainName = (string)dataReader["DomainName"];
                        u.InterventionOID = (int)dataReader["InterventionOID"];
                        u.InterventionName = (string)dataReader["InterventionName"];
                        if (dataReader["LatestActionDate"] != null)
                        {
                            u.LatestActionDate = Convert.ToDateTime(dataReader["LatestActionDate"]).ToShortDateString();
                        }
                        if (dataReader["LatestContact"] != null)
                        {
                            u.LatestContact = Convert.ToDateTime(dataReader["LatestContact"]).ToShortDateString();
                        }
                        u.Internal = (int)dataReader["Internal"];
                        u.Prescribed = (int)dataReader["Prescribed"];
                        u.Completed = (int)dataReader["Completed"];
                        u.Email = (int)dataReader["Email"];
                        u.Telephone = (int)dataReader["Telephone"];
                        u.InPerson = (int)dataReader["InPerson"];
                        u.HandOff = (int)dataReader["HandOff"];
                        u.Participating = (int)dataReader["Participating"];

                        if (dataReader["CreatedDate"] != null)
                        {
                            u.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]).ToShortDateString();
                        }
                        u.CreatedBy = (int)dataReader["CreatedBy"];
                        if (dataReader["LastModifiedDate"] != null)
                        {
                            u.LastModifiedDate = Convert.ToDateTime(dataReader["LastModifiedDate"]).ToShortDateString();
                        }
                        u.LastModifiedBy = (int)dataReader["LastModifiedBy"];
                        u.Comment = (string)dataReader["Comment"];
                        if (dataReader["BannerID"] != null && dataReader["BannerID"] != "" && dataReader["BannerID"]!=DBNull .Value )
                        {
                        u.BannerID = (string)dataReader["BannerID"];
                        }
                        u.PrescribedULR = (u.Prescribed == 1) ? "~/images/Actions-dialog-ok-icon.png" : "~/images/X-Au-Blu-icon.png";
                        u.CompletedURL = (u.Completed == 1) ? "~/images/Actions-dialog-ok-icon.png" : "~/images/X-Au-Blu-icon.png";
                        u.HandOffURL = (u.HandOff == 1) ? "~/images/Actions-dialog-ok-icon.png" : "~/images/X-Au-Blu-icon.png";
                        u.EmailURL = (u.Email == 1) ? "~/images/Actions-dialog-ok-icon.png" : "~/images/X-Au-Blu-icon.png";
                        u.TelephoneURL = (u.Telephone == 1) ? "~/images/Actions-dialog-ok-icon.png" : "~/images/X-Au-Blu-icon.png";
                        u.InPersonURL = (u.InPerson == 1) ? "~/images/Actions-dialog-ok-icon.png" : "~/images/X-Au-Blu-icon.png";
                        u.ParticipatingURL = (u.Participating == 1) ? "~/images/Actions-dialog-ok-icon.png" : "~/images/X-Au-Blu-icon.png";
                        u.Unread = dataReader["Unread"].ToString();

                        //u.FlagURL = (u.HandOff == 1 && u.Unread == "yes") ? "~/images/flag.png" : "";



                        if (u.HandOff == 1 && !CheckUser(u.PrescriptionOID, UserOID))
                        {
                            u.FlagURL = "~/images/flag.png";
                        }
                        else if (u.HandOff == 0 && !CheckUser(u.PrescriptionOID, UserOID))
                        {
                            u.FlagURL = "~/images/flag.png";
                        }
                        else
                        {
                            u.FlagURL = "";
                        }
                        
                        //u.PrescribedULR = (u.Prescribed == 1) ? "~/images/Actions-dialog-ok-icon.pngs" : "~/images/X-Au-Blu-icon.png";
                        //u.PrescribedULR = (u.Prescribed == 1) ? "~/images/Actions-dialog-ok-icon.pngs" : "~/images/X-Au-Blu-icon.png";

                        //invList.Add(u);

                    }
                }

            }
        }
        return u;
    }

    public Collection<Interventions> GetInterventionByRiskOIDAndSOID(int ROID,int SOID, string pageIndex, string numberOfRows)
    {
        //Interventions u = null;
        Collection<Interventions> invList = new Collection<Interventions>();
        // string connectionString = @"Data Source=.\SQLEXPRESS; Initial Catalog=cvtc; User ID=cvtcuser; Password=cvtcuser";
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Intervations_ByRiskOIDAndSOID(?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ROID", ROID);
                command.Parameters.AddWithValue("@SOID", SOID);

                OdbcParameter paramPageIndex = new OdbcParameter("@PageIndex", OdbcType.Int);
                paramPageIndex.Value = Convert.ToInt32(pageIndex);
                command.Parameters.Add(paramPageIndex);

                OdbcParameter paramNumberOfRows = new OdbcParameter("@NumberOfRows", OdbcType.Int);
                paramNumberOfRows.Value = Convert.ToInt32(numberOfRows);
                command.Parameters.Add(paramNumberOfRows);

                connection.Open();
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    Interventions u;
                    while (dataReader.Read())
                    {
                        u = new Interventions();
                        u.PrescriptionOID = (int)dataReader["PrescriptionOID"];
                        u.StudentOID = (int)dataReader["StudentOID"];
                        u.UserOID = (int)dataReader["UserOID"];
                        u.UserName = (string)dataReader["UserName"];
                        u.DomainOID = (int)dataReader["DomainOID"];
                        u.DomainName = (string)dataReader["DomainName"];
                        u.InterventionOID = (int)dataReader["InterventionOID"];
                        u.InterventionName = (string)dataReader["InterventionName"];
                        if (dataReader["LatestActionDate"] != null)
                        {
                            u.LatestActionDate = Convert.ToDateTime(dataReader["LatestActionDate"]).ToShortDateString();
                        }
                        if (dataReader["LatestContact"] != null)
                        {
                            u.LatestContact = Convert.ToDateTime(dataReader["LatestContact"]).ToShortDateString();
                        }
                        u.Internal = (int)dataReader["Internal"];
                        u.Prescribed = (int)dataReader["Prescribed"];
                        u.Completed = (int)dataReader["Completed"];
                        u.Email = (int)dataReader["Email"];
                        u.Telephone = (int)dataReader["Telephone"];
                        u.InPerson = (int)dataReader["InPerson"];
                        u.HandOff = (int)dataReader["HandOff"];
                        if (dataReader["CreatedDate"] != null)
                        {
                            u.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]).ToShortDateString();
                        }
                        u.CreatedBy = (int)dataReader["CreatedBy"];
                        if (dataReader["LastModifiedDate"] != null)
                        {
                            u.LastModifiedDate = Convert.ToDateTime(dataReader["LastModifiedDate"]).ToShortDateString();
                        }
                        u.LastModifiedBy = (int)dataReader["LastModifiedBy"];
                        u.Comment = (string)dataReader["Comment"];
                        if (dataReader["BannerID"] != null && dataReader["BannerID"] != "" && dataReader["BannerID"] != DBNull.Value)
                        {
                            u.BannerID = (string)dataReader["BannerID"];
                        }
                        u.PrescribedULR = (u.Prescribed == 1) ? "~/images/Actions-dialog-ok-icon.png" : "~/images/X-Au-Blu-icon.png";
                        u.CompletedURL = (u.Completed == 1) ? "~/images/Actions-dialog-ok-icon.png" : "~/images/X-Au-Blu-icon.png";
                        u.HandOffURL = (u.HandOff == 1) ? "~/images/Actions-dialog-ok-icon.png" : "~/images/X-Au-Blu-icon.png";
                        u.EmailURL = (u.Email == 1) ? "~/images/Actions-dialog-ok-icon.png" : "~/images/X-Au-Blu-icon.png";
                        u.TelephoneURL = (u.Telephone == 1) ? "~/images/Actions-dialog-ok-icon.png" : "~/images/X-Au-Blu-icon.png";
                        u.InPersonURL = (u.InPerson == 1) ? "~/images/Actions-dialog-ok-icon.png" : "~/images/X-Au-Blu-icon.png";
                        u.FlagURL = (u.HandOff == 1) ? "~/images/flag.png" : "";
                        //u.PrescribedULR = (u.Prescribed == 1) ? "~/images/Actions-dialog-ok-icon.pngs" : "~/images/X-Au-Blu-icon.png";
                        //u.PrescribedULR = (u.Prescribed == 1) ? "~/images/Actions-dialog-ok-icon.pngs" : "~/images/X-Au-Blu-icon.png";

                        invList.Add(u);

                    }
                }

            }
        }
        return invList;
    }

    public Collection<Interventions> GetAllIntervention(string numberOfRows, string pageIndex, string sortColumnName, string sortOrderBy, out int totalRecords, int UserOID)
    {
        //Interventions u = null;
        Collection<Interventions> invList = new Collection<Interventions>();        
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL SelectjqGrid_Prescription(?,?,?,?,?,?)}";
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

                OdbcParameter paramUserOID = new OdbcParameter("@UserOID", OdbcType.Int);
                paramUserOID.Value = Convert.ToInt32(UserOID);
                command.Parameters.Add(paramUserOID);

                OdbcParameter paramTotalRecords = new OdbcParameter("@TotalRecords", OdbcType.Int);
                totalRecords = 0;
                paramTotalRecords.Value = totalRecords;
                paramTotalRecords.Direction = ParameterDirection.Output;
                command.Parameters.Add(paramTotalRecords);

                connection.Open();
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    Interventions u;
                    while (dataReader.Read())
                    {
                        u = new Interventions();
                        u.PrescriptionOID = (int)dataReader["PrescriptionOID"];
                        u.StudentOID = (int)dataReader["StudentOID"];
                        u.UserOID = (int)dataReader["UserOID"];
                        //u.UserName = (string)dataReader["UserName"];
                        u.DomainOID = (int)dataReader["DomainOID"];
                        //u.DomainName = (string)dataReader["DomainName"];
                        u.InterventionOID = (int)dataReader["InterventionOID"];
                        //u.InterventionName = (string)dataReader["InterventionName"];
                        if (dataReader["LatestActionDate"] != null)
                        {
                            u.LatestActionDate = Convert.ToDateTime(dataReader["LatestActionDate"]).ToShortDateString();
                        }
                        if (dataReader["LatestContact"] != null)
                        {
                            u.LatestContact = Convert.ToDateTime(dataReader["LatestContact"]).ToShortDateString();
                        }
                        u.Internal = (int)dataReader["Internal"];
                        u.Prescribed = (int)dataReader["Prescribed"];
                        u.Completed = (int)dataReader["Completed"];
                        u.Email = (int)dataReader["Email"];
                        u.Telephone = (int)dataReader["Telephone"];
                        u.InPerson = (int)dataReader["InPerson"];
                        u.HandOff = (int)dataReader["HandOff"];
                        if (dataReader["CreatedDate"] != null)
                        {
                            u.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]).ToShortDateString();
                        }
                        u.CreatedBy = (int)dataReader["CreatedBy"];
                        if (dataReader["LastModifiedDate"] != null)
                        {
                            u.LastModifiedDate = Convert.ToDateTime(dataReader["LastModifiedDate"]).ToShortDateString();
                        }
                        u.LastModifiedBy = (int)dataReader["LastModifiedBy"];
                        u.Comment = (string)dataReader["Comment"];
                        if (dataReader["BannerStudentName"] != DBNull.Value && dataReader["BannerStudentName"] != "")
                        {
                            u.DomainName = (string)dataReader["BannerStudentName"];
                        }
                        
                        invList.Add(u);

                    }
                }

                totalRecords = (int)paramTotalRecords.Value;

            }
        }
        return invList;
    }

    public Collection<Interventions> GetAllIntervention(string numberOfRows, string pageIndex, string sortColumnName, string sortOrderBy, out int totalRecords)
    {
        //Interventions u = null;
        Collection<Interventions> invList = new Collection<Interventions>();
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Intervention_Search(?,?,?,?,?)}";
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
                    Interventions u;
                    while (dataReader.Read())
                    {
                        u = new Interventions();
                        u.PrescriptionOID = (int)dataReader["PrescriptionOID"];
                        u.StudentOID = (int)dataReader["StudentOID"];
                        u.UserOID = (int)dataReader["UserOID"];
                        u.UserName = (string)dataReader["UserName"];
                        u.DomainOID = (int)dataReader["DomainOID"];
                        u.DomainName = (string)dataReader["DomainName"];
                        u.InterventionOID = (int)dataReader["InterventionOID"];
                        u.InterventionName = (string)dataReader["InterventionName"];
                        if (dataReader["LatestActionDate"] != null)
                        {
                            u.LatestActionDate = Convert.ToDateTime(dataReader["LatestActionDate"]).ToShortDateString();
                        }
                        if (dataReader["LatestContact"] != null)
                        {
                            u.LatestContact = Convert.ToDateTime(dataReader["LatestContact"]).ToShortDateString();
                        }
                        u.Internal = (int)dataReader["Internal"];
                        u.Prescribed = (int)dataReader["Prescribed"];
                        u.Completed = (int)dataReader["Completed"];
                        u.Email = (int)dataReader["Email"];
                        u.Telephone = (int)dataReader["Telephone"];
                        u.InPerson = (int)dataReader["InPerson"];
                        u.HandOff = (int)dataReader["HandOff"];
                        if (dataReader["CreatedDate"] != null)
                        {
                            u.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]).ToShortDateString();
                        }
                        u.CreatedBy = (int)dataReader["CreatedBy"];
                        if (dataReader["LastModifiedDate"] != null)
                        {
                            u.LastModifiedDate = Convert.ToDateTime(dataReader["LastModifiedDate"]).ToShortDateString();
                        }
                        u.LastModifiedBy = (int)dataReader["LastModifiedBy"];
                        if (dataReader["Comment"] != DBNull.Value || dataReader["Comment"] != null || dataReader["Comment"] != "")
                        {
                            u.Comment = (string)dataReader["Comment"];
                        }
                        if (dataReader["BannerID"] != DBNull.Value || dataReader["BannerID"] != null || dataReader["BannerID"] != "")
                        {
                            u.BannerID = Convert .ToString (dataReader["BannerID"]);
                            
                        }
                        if (dataReader["BannerStudentName"] != DBNull.Value || dataReader["BannerStudentName"] != null || dataReader["BannerStudentName"] != "")
                        {
                            u.StudentName = Convert.ToString(dataReader["BannerStudentName"]);
                        }

                        invList.Add(u);

                    }
                }

                totalRecords = (int)paramTotalRecords.Value;

            }
        }
        return invList;
    }

    public Collection<Interventions> SearchIntervention(string numberOfRows, string pageIndex, string sortColumnName, string sortOrderBy, out int totalRecords, DateTime datestamp, string bannerId, string participating, DateTime actionDate, string sponsor, string  criteriaType,  string outcomeType, string urgent, string Internal, string prescribed, string completed, string  email, string telephone, string inPerson, string handoff, DateTime contactDate, string  comment)
    {
        datestamp = ((datestamp == null) || (datestamp == Convert.ToDateTime("1/1/0001"))) ? Convert.ToDateTime("1/1/1900") : datestamp;
        bannerId = (bannerId == null) ? "" : bannerId;

        if (participating == null) participating = "-1";
        else if (participating.ToLower() == "no") participating = "0";
        else if (participating.ToLower() == "yes") participating = "1";
        else participating = "-1";

        actionDate = ((actionDate == null) || (actionDate == Convert.ToDateTime("1/1/0001"))) ? Convert.ToDateTime("1/1/1900") : actionDate;
        sponsor = (sponsor == null) ? "" : sponsor;
        criteriaType = (criteriaType == null) ? "" : criteriaType;
        outcomeType = (outcomeType == null) ? "" : outcomeType;

        if (urgent == null) urgent = "-1";
        else if (urgent.ToLower() == "no") urgent = "0";
        else if (urgent.ToLower() == "yes") urgent = "1";
        else urgent = "-1";

        if (Internal == null) Internal = "-1";
        else if (Internal.ToLower() == "no") Internal = "0";
        else if (Internal.ToLower() == "yes") Internal = "1";
        else Internal = "-1";

        if (prescribed == null) prescribed = "-1";
        else if (prescribed.ToLower() == "no") prescribed = "0";
        else if (prescribed.ToLower() == "yes") prescribed = "1";
        else prescribed = "-1";

        if (completed == null) completed = "-1";
        else if (completed.ToLower() == "no") completed = "0";
        else if (completed.ToLower() == "yes") completed = "1";
        else completed = "-1";

        if (email == null) email = "-1";
        else if (email.ToLower() == "no") email = "0";
        else if (email.ToLower() == "yes") email = "1";
        else email = "-1";

        if (telephone == null) telephone = "-1";
        else if (telephone.ToLower() == "no") telephone = "0";
        else if (telephone.ToLower() == "yes") telephone = "1";
        else telephone = "-1";

        if (inPerson == null) inPerson = "-1";
        else if (inPerson.ToLower() == "no") inPerson = "0";
        else if (inPerson.ToLower() == "yes") inPerson = "1";
        else inPerson = "-1";

        if (handoff == null) handoff = "-1";
        else if (handoff.ToLower() == "no") handoff = "0";
        else if (handoff.ToLower() == "yes") handoff = "1";
        else handoff = "-1";

        contactDate = ((contactDate == null) || (contactDate == Convert.ToDateTime("1/1/0001"))) ? Convert.ToDateTime("1/1/1900") : contactDate;
        comment = (comment == null) ? "" : comment;
         

        Collection<Interventions> invList = new Collection<Interventions>();
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {
                command.Connection = connection;
                command.CommandText = "{CALL Intervention_Search_Param(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)}";
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


                OdbcParameter paramDatestamp = new OdbcParameter("@Datestamp", OdbcType.DateTime);
                paramDatestamp.Value = datestamp;
                command.Parameters.Add(paramDatestamp);

                OdbcParameter paramBannerId = new OdbcParameter("@BannerId", OdbcType.VarChar, 12);
                paramBannerId.Value = bannerId;
                command.Parameters.Add(paramBannerId);

                OdbcParameter paramParticipating = new OdbcParameter("@Participating", OdbcType.Int);
                paramParticipating.Value = Convert.ToInt32(participating);
                command.Parameters.Add(paramParticipating);


                OdbcParameter paramActionDate = new OdbcParameter("@ActionDate", OdbcType.DateTime);
                paramActionDate.Value = actionDate;
                command.Parameters.Add(paramActionDate);

                OdbcParameter paramSponsor = new OdbcParameter("@Sponsor", OdbcType.VarChar, 12);
                paramSponsor.Value = sponsor;
                command.Parameters.Add(paramSponsor);

                OdbcParameter paramCriteriaType = new OdbcParameter("@CriteriaType", OdbcType.VarChar, 12);
                paramCriteriaType.Value = criteriaType;
                command.Parameters.Add(paramCriteriaType);

                OdbcParameter paramOutcomeType = new OdbcParameter("@OutcomeType", OdbcType.VarChar, 12);
                paramOutcomeType.Value = outcomeType;
                command.Parameters.Add(paramOutcomeType);
                                
                OdbcParameter paramUrgent = new OdbcParameter("@Urgent", OdbcType.Int);
                paramUrgent.Value = Convert.ToInt32(urgent);
                command.Parameters.Add(paramUrgent);

                OdbcParameter paramInternal = new OdbcParameter("@Internal", OdbcType.Int);
                paramInternal.Value = Convert.ToInt32(Internal);
                command.Parameters.Add(paramInternal);

                OdbcParameter paramPrescribed = new OdbcParameter("@Prescribed", OdbcType.Int);
                paramPrescribed.Value = Convert.ToInt32(prescribed);
                command.Parameters.Add(paramPrescribed);

                OdbcParameter paramCompleted = new OdbcParameter("@Completed", OdbcType.Int);
                paramCompleted.Value = Convert.ToInt32(completed);
                command.Parameters.Add(paramCompleted);

                OdbcParameter paramEmail = new OdbcParameter("@Email", OdbcType.Int);
                paramEmail.Value = Convert.ToInt32(email);
                command.Parameters.Add(paramEmail);

                OdbcParameter paramTelephone = new OdbcParameter("@Telephone", OdbcType.Int);
                paramTelephone.Value = Convert.ToInt32(telephone);
                command.Parameters.Add(paramTelephone);

                OdbcParameter paramInPerson = new OdbcParameter("@InPerson", OdbcType.Int);
                paramInPerson.Value = Convert.ToInt32(inPerson);
                command.Parameters.Add(paramInPerson);

                OdbcParameter paramHandoff = new OdbcParameter("@Handoff", OdbcType.Int);
                paramHandoff.Value = Convert.ToInt32(handoff);
                command.Parameters.Add(paramHandoff);

                OdbcParameter paramContactDate = new OdbcParameter("@ContactDate", OdbcType.DateTime);
                paramContactDate.Value = contactDate;
                command.Parameters.Add(paramContactDate);

                OdbcParameter paramComment = new OdbcParameter("@Comment", OdbcType.VarChar, 100);
                paramComment.Value = comment;
                command.Parameters.Add(paramComment);

                connection.Open();
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    Interventions u;
                    while (dataReader.Read())
                    {
                        u = new Interventions();
                        u.PrescriptionOID = (int)dataReader["PrescriptionOID"];
                        u.StudentOID = (int)dataReader["StudentOID"];
                        u.UserOID = (int)dataReader["UserOID"];
                        u.UserName = (string)dataReader["UserName"];
                        u.DomainOID = (int)dataReader["DomainOID"];
                        u.DomainName = (string)dataReader["DomainName"];
                        u.InterventionOID = (int)dataReader["InterventionOID"];
                        u.InterventionName = (string)dataReader["InterventionName"];
                        if (dataReader["LatestActionDate"] != null)
                        {
                            u.LatestActionDate = Convert.ToDateTime(dataReader["LatestActionDate"]).ToShortDateString();
                        }
                        if (dataReader["LatestContact"] != null)
                        {
                            u.LatestContact = Convert.ToDateTime(dataReader["LatestContact"]).ToShortDateString();
                        }
                        u.Internal = (int)dataReader["Internal"];
                        u.Prescribed = (int)dataReader["Prescribed"];
                        u.Completed = (int)dataReader["Completed"];
                        u.Email = (int)dataReader["Email"];
                        u.Telephone = (int)dataReader["Telephone"];
                        u.InPerson = (int)dataReader["InPerson"];
                        u.HandOff = (int)dataReader["HandOff"];
                        if (dataReader["CreatedDate"] != null)
                        {
                            u.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]).ToShortDateString();
                        }
                        u.CreatedBy = (int)dataReader["CreatedBy"];
                        if (dataReader["LastModifiedDate"] != null)
                        {
                            u.LastModifiedDate = Convert.ToDateTime(dataReader["LastModifiedDate"]).ToShortDateString();
                        }
                        u.LastModifiedBy = (int)dataReader["LastModifiedBy"];
                        u.Comment = (string)dataReader["Comment"];
                        if (dataReader["BannerID"] != DBNull.Value || dataReader["BannerID"] != null || dataReader["BannerID"] != "")
                        {
                            u.BannerID = Convert.ToString(dataReader["BannerID"]);

                        }
                        if (dataReader["BannerStudentName"] != DBNull.Value || dataReader["BannerStudentName"] != null || dataReader["BannerStudentName"] != "")
                        {
                            u.StudentName = Convert.ToString(dataReader["BannerStudentName"]);
                        }


                        invList.Add(u);

                    }
                }

                totalRecords = (int)paramTotalRecords.Value;

            }
        }
        return invList;
    }


    public int GetInterventionOIDByInterventionName(string Name)
    {
        //Collection<Student> studentList = new Collection<Student>();
        //Student student = null;
        int InterventionOID=0;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL InterventionOID_ByInterventionName(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@Name", Name);
                //Open connection
                connection.Open();
                //Read using reader
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    //Question q = new Question();
                    dataReader.Read();
                    //while (dataReader.Read())
                    {
                        //student = new Student();
                        InterventionOID = Convert.ToInt32(dataReader["InterventionOID"]);
                        // studentList.Add(student);

                    }
                }

            }
        }
        return InterventionOID;
    }

    public Interventions GetInterventionByOID(int POID)
    {
        Interventions intervention=null;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Prescription_GetByOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@poid", POID);

                connection.Open();
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        intervention = new Interventions();
                        intervention.PrescriptionOID = (int)dataReader["PrescriptionOID"];
                        intervention.StudentOID = (int)dataReader["StudentOID"];
                        intervention.UserOID = (int)dataReader["UserOID"];
                        intervention.UserName = (string)dataReader["UserName"];
                        intervention.DomainOID = (int)dataReader["DomainOID"];
                        intervention.DomainName = (string)dataReader["DomainName"];
                        intervention.InterventionOID = (int)dataReader["InterventionOID"];
                        intervention.InterventionName = (string)dataReader["InterventionName"];
                        if (dataReader["LatestActionDate"] != null)
                        {
                            intervention.LatestActionDate = Convert.ToDateTime(dataReader["LatestActionDate"]).ToShortDateString();
                        }
                        if (dataReader["LatestContact"] != null)
                        {
                            intervention.LatestContact = Convert.ToDateTime(dataReader["LatestContact"]).ToShortDateString();
                        }
                        intervention.Urgent = (int)dataReader["Urgent"];
                         intervention.Internal = (int)dataReader["Internal"];
                        intervention.Prescribed = (int)dataReader["Prescribed"];
                        intervention.Completed = (int)dataReader["Completed"];
                        intervention.Participating = (int)dataReader["Participating"];
                        intervention.Email = (int)dataReader["Email"];
                        intervention.Telephone = (int)dataReader["Telephone"];
                        intervention.InPerson = (int)dataReader["InPerson"];
                        intervention.HandOff = (int)dataReader["HandOff"];
                        if (dataReader["CreatedDate"] != null)
                        {
                            intervention.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]).ToShortDateString();
                        }
                        intervention.CreatedBy = (int)dataReader["CreatedBy"];
                        if (dataReader["LastModifiedDate"] != null)
                        {
                            intervention.LastModifiedDate = Convert.ToDateTime(dataReader["LastModifiedDate"]).ToShortDateString();
                        }
                        intervention.LastModifiedBy = (int)dataReader["LastModifiedBy"];
                        intervention.Comment = (string)dataReader["Comment"];
                        intervention.Testing = (int)dataReader["Testing"];
                        intervention.AssessmentOID = (int)dataReader["AssessmentOID"];

                    }
                }

            }
        }
        return intervention;
    }


    public Collection <Interventions> GetInterventionByStudentOID(int SOID)
    {
        Collection<Interventions> _list = new Collection<Interventions>();
        Interventions intervention=null ;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Prescription_GetBySOID(?)}";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@soid", SOID);

                connection.Open();
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                   
                    while  (dataReader.Read())
                    {
                        intervention = new Interventions();
                        intervention.PrescriptionOID = (int)dataReader["PrescriptionOID"];
                        intervention.StudentOID = (int)dataReader["StudentOID"];
                        intervention.UserOID = (int)dataReader["UserOID"];
                        intervention.UserName = (string)dataReader["UserName"];
                        intervention.DomainOID = (int)dataReader["DomainOID"];
                        intervention.DomainName = (string)dataReader["DomainName"];
                        intervention.InterventionOID = (int)dataReader["InterventionOID"];
                        intervention.InterventionName = (string)dataReader["InterventionName"];
                        if (dataReader["LatestActionDate"] != null)
                        {
                            intervention.LatestActionDate = Convert.ToDateTime(dataReader["LatestActionDate"]).ToShortDateString();
                        }
                        if (dataReader["LatestContact"] != null)
                        {
                            intervention.LatestContact = Convert.ToDateTime(dataReader["LatestContact"]).ToShortDateString();
                        }
                        intervention.Internal = (int)dataReader["Internal"];
                        intervention.Prescribed = (int)dataReader["Prescribed"];
                        intervention.Completed = (int)dataReader["Completed"];
                        intervention.Email = (int)dataReader["Email"];
                        intervention.Telephone = (int)dataReader["Telephone"];
                        intervention.InPerson = (int)dataReader["InPerson"];
                        intervention.HandOff = (int)dataReader["HandOff"];
                        if (dataReader["CreatedDate"] != null)
                        {
                            intervention.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]).ToShortDateString();
                        }
                        intervention.CreatedBy = (int)dataReader["CreatedBy"];
                        if (dataReader["LastModifiedDate"] != null)
                        {
                            intervention.LastModifiedDate = Convert.ToDateTime(dataReader["LastModifiedDate"]).ToShortDateString();
                        }
                        intervention.LastModifiedBy = (int)dataReader["LastModifiedBy"];
                        intervention.Comment = (string)dataReader["Comment"];
                        intervention.Participating = (int)dataReader["Participating"];
                        intervention.Urgent = (int)dataReader["Urgent"];
                        intervention.Testing = (int)dataReader["Testing"];
                        _list.Add(intervention);
                    }
                }

            }
        }
        return _list;
    }

    public Collection<Interventions> GetInterventionByStudentOID(int SOID,int RiskOID)
    {
        Collection<Interventions> _list = new Collection<Interventions>();
        Interventions intervention = null;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Prescription_GetBySOID_Print(?,?)}";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@soid", SOID);
                command.Parameters.AddWithValue("@RiskOID", RiskOID);

                connection.Open();
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {

                    while (dataReader.Read())
                    {
                        intervention = new Interventions();
                        intervention.PrescriptionOID = (int)dataReader["PrescriptionOID"];
                        intervention.StudentOID = (int)dataReader["StudentOID"];
                        intervention.UserOID = (int)dataReader["UserOID"];
                        intervention.UserName = (string)dataReader["UserName"];
                        intervention.DomainOID = (int)dataReader["DomainOID"];
                        intervention.DomainName = (string)dataReader["DomainName"];
                        intervention.InterventionOID = (int)dataReader["InterventionOID"];
                        intervention.InterventionName = (string)dataReader["InterventionName"];
                        if (dataReader["LatestActionDate"] != null)
                        {
                            intervention.LatestActionDate = Convert.ToDateTime(dataReader["LatestActionDate"]).ToShortDateString();
                        }
                        if (dataReader["LatestContact"] != null)
                        {
                            intervention.LatestContact = Convert.ToDateTime(dataReader["LatestContact"]).ToShortDateString();
                        }
                        intervention.Internal = (int)dataReader["Internal"];
                        intervention.Prescribed = (int)dataReader["Prescribed"];
                        intervention.Completed = (int)dataReader["Completed"];
                        intervention.Email = (int)dataReader["Email"];
                        intervention.Telephone = (int)dataReader["Telephone"];
                        intervention.InPerson = (int)dataReader["InPerson"];
                        intervention.HandOff = (int)dataReader["HandOff"];
                        if (dataReader["CreatedDate"] != null)
                        {
                            intervention.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]).ToShortDateString();
                        }
                        intervention.CreatedBy = (int)dataReader["CreatedBy"];
                        if (dataReader["LastModifiedDate"] != null)
                        {
                            intervention.LastModifiedDate = Convert.ToDateTime(dataReader["LastModifiedDate"]).ToShortDateString();
                        }
                        intervention.LastModifiedBy = (int)dataReader["LastModifiedBy"];
                        intervention.Comment = (string)dataReader["Comment"];
                        intervention.Participating = (int)dataReader["Participating"];
                        intervention.Urgent = (int)dataReader["Urgent"];
                        intervention.Testing = (int)dataReader["Testing"];
                        _list.Add(intervention);
                    }
                }

            }
        }
        return _list;
    }

    public Collection<Interventions> GetInterventionByAssOID(int SOID, string AssessmentName)
    {
        Collection<Interventions> _list = new Collection<Interventions>();
        Interventions intervention = null;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Prescription_GetByAssName(?,?)}";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@soid", SOID);
                command.Parameters.AddWithValue("@assName", AssessmentName);

                connection.Open();
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {

                    while (dataReader.Read())
                    {
                        intervention = new Interventions();
                        intervention.PrescriptionOID = (int)dataReader["PrescriptionOID"];
                        intervention.StudentOID = (int)dataReader["StudentOID"];
                        intervention.UserOID = (int)dataReader["UserOID"];
                        intervention.UserName = (string)dataReader["UserName"];
                        intervention.DomainOID = (int)dataReader["DomainOID"];
                        intervention.DomainName = (string)dataReader["DomainName"];
                        intervention.InterventionOID = (int)dataReader["InterventionOID"];
                        intervention.InterventionName = (string)dataReader["InterventionName"];
                        if (dataReader["LatestActionDate"] != null)
                        {
                            intervention.LatestActionDate = Convert.ToDateTime(dataReader["LatestActionDate"]).ToShortDateString();
                        }
                        if (dataReader["LatestContact"] != null)
                        {
                            intervention.LatestContact = Convert.ToDateTime(dataReader["LatestContact"]).ToShortDateString();
                        }
                        intervention.Internal = (int)dataReader["Internal"];
                        intervention.Prescribed = (int)dataReader["Prescribed"];
                        intervention.Completed = (int)dataReader["Completed"];
                        intervention.Email = (int)dataReader["Email"];
                        intervention.Telephone = (int)dataReader["Telephone"];
                        intervention.InPerson = (int)dataReader["InPerson"];
                        intervention.HandOff = (int)dataReader["HandOff"];
                        if (dataReader["CreatedDate"] != null)
                        {
                            intervention.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]).ToShortDateString();
                        }
                        intervention.CreatedBy = (int)dataReader["CreatedBy"];
                        if (dataReader["LastModifiedDate"] != null)
                        {
                            intervention.LastModifiedDate = Convert.ToDateTime(dataReader["LastModifiedDate"]).ToShortDateString();
                        }
                        intervention.LastModifiedBy = (int)dataReader["LastModifiedBy"];
                        intervention.Comment = (string)dataReader["Comment"];
                        intervention.Urgent = (int)dataReader["Urgent"];
                        intervention.Testing = (int)dataReader["Testing"];
                        intervention.Participating = (int)dataReader["Participating"];
                        _list.Add(intervention);
                    }
                }

            }
        }
        return _list;
    }

    public Collection<Interventions> GetInterventionByAssOID(int SOID, int riskOid,int aoid)
    {
        Collection<Interventions> _list = new Collection<Interventions>();
        Interventions intervention = null;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Prescription_GetByAssNamePrint(?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@soid", SOID);
                command.Parameters.AddWithValue("@riskoid", riskOid);
                command.Parameters.AddWithValue("@aoid", aoid);

                connection.Open();
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {

                    while (dataReader.Read())
                    {
                        intervention = new Interventions();
                        intervention.PrescriptionOID = (int)dataReader["PrescriptionOID"];
                        intervention.StudentOID = (int)dataReader["StudentOID"];
                        intervention.UserOID = (int)dataReader["UserOID"];
                        intervention.UserName = (string)dataReader["UserName"];
                        intervention.DomainOID = (int)dataReader["DomainOID"];
                        intervention.DomainName = (string)dataReader["DomainName"];
                        intervention.InterventionOID = (int)dataReader["InterventionOID"];
                        intervention.InterventionName = (string)dataReader["InterventionName"];
                        if (dataReader["LatestActionDate"] != null)
                        {
                            intervention.LatestActionDate = Convert.ToDateTime(dataReader["LatestActionDate"]).ToShortDateString();
                        }
                        if (dataReader["LatestContact"] != null)
                        {
                            intervention.LatestContact = Convert.ToDateTime(dataReader["LatestContact"]).ToShortDateString();
                        }
                        intervention.Internal = (int)dataReader["Internal"];
                        intervention.Prescribed = (int)dataReader["Prescribed"];
                        intervention.Completed = (int)dataReader["Completed"];
                        intervention.Email = (int)dataReader["Email"];
                        intervention.Telephone = (int)dataReader["Telephone"];
                        intervention.InPerson = (int)dataReader["InPerson"];
                        intervention.HandOff = (int)dataReader["HandOff"];
                        if (dataReader["CreatedDate"] != null)
                        {
                            intervention.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]).ToShortDateString();
                        }
                        intervention.CreatedBy = (int)dataReader["CreatedBy"];
                        if (dataReader["LastModifiedDate"] != null)
                        {
                            intervention.LastModifiedDate = Convert.ToDateTime(dataReader["LastModifiedDate"]).ToShortDateString();
                        }
                        intervention.LastModifiedBy = (int)dataReader["LastModifiedBy"];
                        intervention.Comment = (string)dataReader["Comment"];
                        intervention.Urgent = (int)dataReader["Urgent"];
                        intervention.Testing = (int)dataReader["Testing"];
                        intervention.Participating = (int)dataReader["Participating"];
                        intervention.AssessmentName = (string)dataReader["AssessmentName"];
                        _list.Add(intervention);
                    }
                }

            }
        }
        return _list;
    }



    public Interventions GetInterventionByRiskOID(int ROID)
    {
        Interventions intervention = null;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Prescription_GetByRiskOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@roid", ROID);

                connection.Open();
                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        intervention = new Interventions();
                        intervention.PrescriptionOID = (int)dataReader["PrescriptionOID"];
                        intervention.RiskOID = (int)dataReader["RiskOID"];
                        intervention.StudentOID = (int)dataReader["StudentOID"];
                        intervention.UserOID = (int)dataReader["UserOID"];
                        intervention.UserName = (string)dataReader["UserName"];
                        intervention.Comment = (string)dataReader["Comment"];
                        intervention.DomainOID = (int)dataReader["DomainOID"];
                        intervention.DomainName = (string)dataReader["DomainName"];
                        intervention.InterventionOID = (int)dataReader["InterventionOID"];
                        intervention.InterventionName = (string)dataReader["InterventionName"];
                        if (dataReader["LatestActionDate"] != null)
                        {
                            intervention.LatestActionDate = Convert.ToDateTime(dataReader["LatestActionDate"]).ToShortDateString();
                        }
                        if (dataReader["LatestContact"] != null)
                        {
                            intervention.LatestContact = Convert.ToDateTime(dataReader["LatestContact"]).ToShortDateString();
                        }
                        intervention.Urgent = (int)dataReader["Urgent"];
                        intervention.Participating = (int)dataReader["Participating"];
                        intervention.Testing = (int)dataReader["Testing"];
                        intervention.Internal = (int)dataReader["Internal"];
                        intervention.Prescribed = (int)dataReader["Prescribed"];
                        intervention.Completed = (int)dataReader["Completed"];
                        intervention.Email = (int)dataReader["Email"];
                        intervention.Telephone = (int)dataReader["Telephone"];
                        intervention.InPerson = (int)dataReader["InPerson"];
                        intervention.HandOff = (int)dataReader["HandOff"];
                        if (dataReader["CreatedDate"] != null)
                        {
                            intervention.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]).ToShortDateString();
                        }
                        intervention.CreatedBy = (int)dataReader["CreatedBy"];
                        if (dataReader["LastModifiedDate"] != null)
                        {
                            intervention.LastModifiedDate = Convert.ToDateTime(dataReader["LastModifiedDate"]).ToShortDateString();
                        }
                        intervention.LastModifiedBy = (int)dataReader["LastModifiedBy"];


                    }
                }

            }
        }
        return intervention;
    }

    public bool UpdateInterventions()
    {
        bool result = false;

        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Interventions_Update(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)}";
                command.CommandType = CommandType.StoredProcedure;



                command.Parameters.AddWithValue("@PrescriptionOID", PrescriptionOID);
                command.Parameters.AddWithValue("@RiskOID", RiskOID);

                OdbcParameter paramStudentOID = new OdbcParameter("@StudentOID", OdbcType.Int);
                paramStudentOID.Value = this.StudentOID;
                command.Parameters.Add(paramStudentOID);

                
                OdbcParameter paramUserOID = new OdbcParameter("@UserOID", OdbcType.Int);
                paramUserOID.Value = this.UserOID;
                command.Parameters.Add(paramUserOID);

                OdbcParameter paramDomainOID = new OdbcParameter("@DomainOID", OdbcType.Int);
                paramDomainOID.Value = this.DomainOID;
                command.Parameters.Add(paramDomainOID);

                OdbcParameter paramInterventionOID = new OdbcParameter("@InterventionOID", OdbcType.Int);
                paramInterventionOID.Value = this.InterventionOID;
                command.Parameters.Add(paramInterventionOID);

                OdbcParameter paramLatestActionDate = new OdbcParameter("@LatestActionDate", OdbcType.DateTime);
                paramLatestActionDate.Value = this.LatestActionDate;
                command.Parameters.Add(paramLatestActionDate);

                OdbcParameter paramLatestContact = new OdbcParameter("@LatestContact", OdbcType.DateTime);
                paramLatestContact.Value = this.LatestContact;
                command.Parameters.Add(paramLatestContact);

                OdbcParameter paramInternal = new OdbcParameter("@Internal", OdbcType.Int);
                paramInternal.Value = this.Internal;
                command.Parameters.Add(paramInternal);

                OdbcParameter paramPrescribed = new OdbcParameter("@Prescribed", OdbcType.Int);
                paramPrescribed.Value = this.Prescribed;
                command.Parameters.Add(paramPrescribed);

                OdbcParameter paramCompleted = new OdbcParameter("@Completed", OdbcType.Int);
                paramCompleted.Value = this.Completed;
                command.Parameters.Add(paramCompleted);

                OdbcParameter paramEmail = new OdbcParameter("@Email", OdbcType.Int);
                paramEmail.Value = this.Email;
                command.Parameters.Add(paramEmail);

                OdbcParameter paramTelephone = new OdbcParameter("@Telephone", OdbcType.Int);
                paramTelephone.Value = this.Telephone;
                command.Parameters.Add(paramTelephone);

                OdbcParameter paramInPerson = new OdbcParameter("@InPerson", OdbcType.Int);
                paramInPerson.Value = this.InPerson;
                command.Parameters.Add(paramInPerson);

                OdbcParameter paramHandOff = new OdbcParameter("@HandOff", OdbcType.Int);
                paramHandOff.Value = this.HandOff;
                command.Parameters.Add(paramHandOff);

                //OdbcParameter paramCreatedBy = new OdbcParameter("@CreatedBy", OdbcType.Int);
                //paramCreatedBy.Value = this.CreatedBy;
                //command.Parameters.Add(paramCreatedBy);

                //OdbcParameter paramLastModifiedBy = new OdbcParameter("@LastModifiedBy", OdbcType.Int);
                //paramLastModifiedBy.Value = this.LastModifiedBy;
                //command.Parameters.Add(paramLastModifiedBy);

                OdbcParameter paramComment = new OdbcParameter("@Comment", OdbcType.VarChar, 100);
                paramComment.Value = this.Comment;
                command.Parameters.Add(paramComment);

                OdbcParameter paramUrgent = new OdbcParameter("@Urgent", OdbcType.Int);
                paramUrgent.Value = this.Urgent;
                command.Parameters.Add(paramUrgent);

                OdbcParameter paramParticipating = new OdbcParameter("@Participating", OdbcType.Int);
                paramParticipating.Value = this.Participating;
                command.Parameters.Add(paramParticipating);

                OdbcParameter paramTesting = new OdbcParameter("@Testing", OdbcType.Int);
                paramTesting.Value = this.Testing;
                command.Parameters.Add(paramTesting);

                OdbcParameter paramAssessmentOID = new OdbcParameter("@AssessmentOID", OdbcType.Int);
                paramAssessmentOID.Value = this.AssessmentOID;
                command.Parameters.Add(paramAssessmentOID);


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

    public Collection<Interventions> GetInterventionByDomainName(string DomainName)
    {
        //Collection<Student> studentList = new Collection<Student>();
        //Student student = null;
        Collection<Interventions> invList = new Collection<Interventions>();
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Intervention_ByDomainName(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Set Parameter Value
                command.Parameters.AddWithValue("@Name", DomainName);
                //Open connection
                connection.Open();
                //Read using reader

                using (OdbcDataReader dataReader = command.ExecuteReader())
                {
                    Interventions u;
                    //Question q = new Question();
                    //dataReader.Read();
                    invList.Clear();
                    while (dataReader.Read())
                    {
                        //student = new Student();
                        u = new Interventions();
                        u.InterventionName = Convert.ToString(dataReader["InterventionName"]);
                        invList.Add(u);

                    }
                }

            }
        }
        return invList;
    }

    public int GetAssessmentOIDByPrescriptionOID(string POID)
    {
        //Assessment ass = null;
        int strAssOID = 0;
        using (OdbcConnection connection = new OdbcConnection(connectionString))
        {
            using (OdbcCommand command = new OdbcCommand())
            {

                command.Connection = connection;
                command.CommandText = "{CALL Assessment_BYPOID(?)}";
                command.CommandType = CommandType.StoredProcedure;

                //Parameter Setting
                command.Parameters.AddWithValue("@POID", POID);

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
  
}

