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

public partial class pg_intervention_ViewInterventionReport : System.Web.UI.Page
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
            MenuURL = "pg/intervention/ViewInterventionReport.aspx?ReportOID=" + reportOIDStr;
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
            #region Variable
            string reportname = Convert.ToString(Session["reportname"]);
            string numberOfRows = Convert.ToString(Session["numberOfRows"]);
            string pageIndex = Convert.ToString(Session["pageIndex"]);

            string sortColumnName = Convert.ToString(Session["sortColumnName"]);
            string sortOrderBy = Convert.ToString(Session["sortOrderBy"]);
                     

            DateTime datestamp= Convert.ToDateTime(Session["datestamp"]);
            string bannerId = Convert.ToString(Session["bannerId"]);
            string participating= Convert.ToString(Session["participating"]);
            DateTime actionDate=Convert.ToDateTime(Session["actionDate"]);
            string sponsor = Convert.ToString(Session["sponsor"]);

            string criteriaType = Convert.ToString(Session["criteriaType"]);
            string outcomeType= Convert.ToString(Session["outcomeType"]);
            string urgent= Convert.ToString(Session["urgent"]);
            string Internal= Convert.ToString(Session["Internal"]);
            string prescribed= Convert.ToString(Session["prescribed"]);
            string completed= Convert.ToString(Session["completed"]);
            string email= Convert.ToString(Session["email"]);
            string telephone= Convert.ToString(Session["telephone"]);
            string inPerson= Convert.ToString(Session["inPerson"]);
            string handoff= Convert.ToString(Session["handoff"]);
            DateTime contactDate=Convert.ToDateTime(Session["contactDate"]);
            string comment= Convert.ToString(Session["comment"]);
            int totalRecords;
            #endregion

            Collection<Interventions> invList = SearchIntervention(numberOfRows, pageIndex, sortColumnName, sortOrderBy, out totalRecords, datestamp,bannerId,participating,actionDate,sponsor, criteriaType,outcomeType, urgent,Internal,prescribed, completed, email, telephone,inPerson,handoff,contactDate,comment);

            //DataTable interventionReportDt = Session["InterventionReportDt"] as DataTable;
            DataTable interventionReportDt = ConvertListToDataTable(invList);
            string reportOIDStr = Request.QueryString["ReportOID"].ToString();

            ExportToExcel.ExportToSpreadsheet(interventionReportDt, reportOIDStr, "CSV");
        }
        catch (Exception ex)
        { }
    }


  


    protected void ButtonExcel_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable interventionReportDt = Session["InterventionReportDt"] as DataTable;
            string reportOIDStr = Request.QueryString["ReportOID"].ToString();

            ExportToExcel.ExportToSpreadsheet(interventionReportDt, reportOIDStr, "Excel");
        }
        catch (Exception ex)
        { }
    }

    protected void ButtonPDF_Click(object sender, EventArgs e)
    {
        try
        {
            #region Variable
            string reportname = Convert.ToString(Session["reportname"]);
            string numberOfRows = Convert.ToString(Session["numberOfRows"]);
            string pageIndex = Convert.ToString(Session["pageIndex"]);

            string sortColumnName = Convert.ToString(Session["sortColumnName"]);
            string sortOrderBy = Convert.ToString(Session["sortOrderBy"]);
            
            DateTime datestamp = Convert.ToDateTime(Session["datestamp"]);
            string bannerId = Convert.ToString(Session["bannerId"]);
            string participating = Convert.ToString(Session["participating"]);
            DateTime actionDate = Convert.ToDateTime(Session["actionDate"]);
            string sponsor = Convert.ToString(Session["sponsor"]);

            string criteriaType = Convert.ToString(Session["criteriaType"]);
            string outcomeType = Convert.ToString(Session["outcomeType"]);
            string urgent = Convert.ToString(Session["urgent"]);
            string Internal = Convert.ToString(Session["Internal"]);
            string prescribed = Convert.ToString(Session["prescribed"]);
            string completed = Convert.ToString(Session["completed"]);
            string email = Convert.ToString(Session["email"]);
            string telephone = Convert.ToString(Session["telephone"]);
            string inPerson = Convert.ToString(Session["inPerson"]);
            string handoff = Convert.ToString(Session["handoff"]);
            DateTime contactDate = Convert.ToDateTime(Session["contactDate"]);
            string comment = Convert.ToString(Session["comment"]);
            int totalRecords;
            #endregion

            Collection<Interventions> invList = SearchIntervention(numberOfRows, pageIndex, sortColumnName, sortOrderBy, out totalRecords, datestamp, bannerId, participating, actionDate, sponsor, criteriaType, outcomeType, urgent, Internal, prescribed, completed, email, telephone, inPerson, handoff, contactDate, comment);

            //DataTable interventionReportDt = Session["InterventionReportDt"] as DataTable;
            DataTable interventionReportDt = ConvertListToDataTable(invList);
            string reportOIDStr = Request.QueryString["ReportOID"].ToString();

            ExportToExcel.ExportToSpreadsheet(interventionReportDt, reportOIDStr, "PDF");
        }
        catch (Exception ex)
        { }
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


    protected void ButtonDelete_Click(object sender, EventArgs e)
    {
        DeleteReport(MenuURL);
        Response.Redirect("../student/ReportDelete.aspx", false);
    }

    private DataTable ConvertListToDataTable(Collection<Interventions> interventions)
    {
        DataTable dt = new DataTable();
        try
        {
            dt.Columns.Add("PrescriptionOID");
            dt.Columns.Add("Datestamp");
            dt.Columns.Add("Banner Id");
            dt.Columns.Add("Participating");
            dt.Columns.Add("Action Date");
            dt.Columns.Add("Sponsor");
            dt.Columns.Add("Criteria Type");
            dt.Columns.Add("Outcome Type");
            dt.Columns.Add("Urgent");
            dt.Columns.Add("Internal");
            dt.Columns.Add("Prescribed");
            dt.Columns.Add("Completed");
            dt.Columns.Add("Email");
            dt.Columns.Add("Telephone");
            dt.Columns.Add("In Person");
            dt.Columns.Add("Hand off");
            dt.Columns.Add("Contact Date");
            dt.Columns.Add("Comment");


            foreach (Interventions intervention in interventions)
            {
                DataRow row = dt.NewRow();
                row["PrescriptionOID"] = intervention.PrescriptionOID.ToString();
                row["Datestamp"] = intervention.LatestContact;
                row["Banner Id"] = intervention.BannerID;
                row["Participating"] = (intervention.Participating == 1) ? "yes" : "no";
                row["Action Date"] = intervention.LatestActionDate;
                row["Sponsor"] = intervention.UserName;
                row["Criteria Type"] = intervention.DomainName;
                row["Outcome Type"] = intervention.InterventionName;
                row["Urgent"] = (intervention.Urgent == 1) ? "yes" : "no";//"<img border='0' src='~/images/Actions-dialog-ok-icon.png' alt='' />" : "<img border='0' src='~/images/X-Au-Blu-icon.png' alt='' />";
                row["Internal"] = (intervention.Internal == 1) ? "yes" : "no";//"<img border='0' src='~/images/Actions-dialog-ok-icon.png' alt='' />" : "<img border='0' src='~/images/X-Au-Blu-icon.png' alt='' />";
                row["Prescribed"] = (intervention.Prescribed == 1) ? "yes" : "no";//"<img border='0' src='~/images/Actions-dialog-ok-icon.png' alt='' />" : "<img border='0' src='~/images/X-Au-Blu-icon.png' alt='' />";
                row["Completed"] = (intervention.Completed == 1) ? "yes" : "no";//"<img border='0' src='~/images/Actions-dialog-ok-icon.png' alt='' />" : "<img border='0' src='~/images/X-Au-Blu-icon.png' alt='' />";
                row["Email"] = (intervention.Email == 1) ? "yes" : "no";//"<img border='0' src='~/images/Actions-dialog-ok-icon.png' alt='' />" : "<img border='0' src='~/images/X-Au-Blu-icon.png' alt='' />";
                row["Telephone"] = (intervention.Telephone == 1) ? "yes" : "no";//"<img border='0' src='~/images/Actions-dialog-ok-icon.png' alt='' />" : "<img border='0' src='~/images/X-Au-Blu-icon.png' alt='' />";
                row["In Person"] = (intervention.InPerson == 1) ? "yes" : "no";//"<img border='0' src='~/images/Actions-dialog-ok-icon.png' alt='' />" : "<img border='0' src='~/images/X-Au-Blu-icon.png' alt='' />";
                row["Hand off"] = (intervention.HandOff == 1) ? "yes" : "no";//"<img border='0' src='~/images/Actions-dialog-ok-icon.png' alt='' />" : "<img border='0' src='~/images/X-Au-Blu-icon.png' alt='' />";
                row["Contact Date"] = intervention.LatestContact;
                row["Comment"] = intervention.Comment;
                dt.Rows.Add(row);
            }
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public Collection<Interventions> SearchIntervention(string numberOfRows, string pageIndex, string sortColumnName, string sortOrderBy, out int totalRecords, DateTime datestamp, string bannerId, string participating, DateTime actionDate, string sponsor, string criteriaType, string outcomeType, string urgent, string Internal, string prescribed, string completed, string email, string telephone, string inPerson, string handoff, DateTime contactDate, string comment)
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
}
