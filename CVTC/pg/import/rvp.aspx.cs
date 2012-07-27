using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text;
using System.IO;


public partial class pg_import_rvp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // Initalize();
            //ExcelImport();
            // GridViewNTO.DataSource = ReadDryData(@"c:\test.xls");
            //GridViewNTO.DataBind();
            Label1.Text = "";
        }
    }


    private void Init()
    {


    }


    public System.Data.DataTable ReadDryData(string filename)
    {
        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath(filename) + ";Extended Properties=Excel 12.0;";
        string strSQL = "SELECT * FROM [Sheet1$]";
        OleDbConnection excelConnection = new OleDbConnection(connectionString);
        excelConnection.Open(); // this will open an Excel file
        OleDbCommand dbCommand = new OleDbCommand(strSQL, excelConnection);
        OleDbDataAdapter dataAdapter = new OleDbDataAdapter(dbCommand);
        // create data table
        System.Data.DataTable dTable = new System.Data.DataTable();
        dataAdapter.Fill(dTable);

        System.Data.DataTable dt = new System.Data.DataTable();
        //dt = dTable.Copy();
        //System.Data.DataRow nr;

        for (int i = 0; i < dTable.Rows.Count; i++)
        {
            if (dTable.Rows[i]["BannerID"] == null || dTable.Rows[i]["BannerID"].ToString() == "")
            {
                dTable.Rows.Remove(dTable.Rows[i]);
                i--;
            }
        }

        dTable.Columns[1].ColumnName = "Type";
        //string strConnection = @"Data Source=.\SQLEXPRESS; Initial Catalog=cvtc; User ID=cvtcuser; Password=cvtcuser";
        string strConnection = System.Configuration.ConfigurationManager.ConnectionStrings["cvtcConnectionString"].ConnectionString;
        SqlBulkCopy sqlBulk = new SqlBulkCopy(strConnection);
        sqlBulk.DestinationTableName = "NTO";
        sqlBulk.ColumnMappings.Add("BannerID", "BannerID");
        sqlBulk.ColumnMappings.Add("Type", "Type");
        sqlBulk.WriteToServer(dTable);

        return dTable;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (uplTheFile.PostedFile.FileName != string.Empty)
            {
                if (uplTheFile.PostedFile.FileName.Substring(uplTheFile.PostedFile.FileName.Length - 3, 3) == "csv" || uplTheFile.PostedFile.FileName.Substring(uplTheFile.PostedFile.FileName.Length - 3, 3) == "CSV")
                {
                uplTheFile.PostedFile.SaveAs(Server.MapPath("~/files/") + uplTheFile.Value);
                string fileName = uplTheFile.Value;
                string path = Server.MapPath("~/files/");
                GridViewNTO.DataSource = GetCSVData(path, fileName);
                GridViewNTO.DataBind();
                SaveToDB(path, fileName);
                Label1.Text = "Save Successfully";
                }
                else 
                {
                Label1.Text = "Invaild Data Formate . Please Choose a CSV File to Upload";
                }
            }
            else
            {
                Label1.Text = "Please Choose a CSV File to Upload";
            }
        }
        catch (Exception ex)
        {
            Label1.Text = "Error Occured to Save Data into Database, Please Try again";
        }
    }


    public static DataTable GetCSVData(string filePath, string fileName)
    {
        string fullPath = filePath + fileName;
        DataTable dataTable = new DataTable();
        try
        {
            using (StreamReader readFile = new StreamReader(fullPath))
            {
                string line;
                StringBuilder sb = new StringBuilder();
                string[] row;
                int counter = 0;
                int length = 0;
                while ((line = readFile.ReadLine()) != null)
                {
                    row = line.Split(',');

                    if (counter == 1)
                    {
                        length = row.Length;
                        DataRow dr1 = dataTable.NewRow();
                        for (int i = 0; i < length; i++)
                        {
                            try
                            {
                                dataTable.Columns.Add("Col_" + i.ToString());
                                dr1[i] = Convert.ToString(row[i]);

                            }
                            catch (Exception ex)
                            {
                                //dataTable.Columns.Add(row[i].ToString() + i.ToString());
                            }
                        }
                        dataTable.Rows.Add(dr1);
                    }
                    else
                    {

                        DataRow dr = dataTable.NewRow();
                        for (int i = 0; i < length; i++)
                        {
                            dr[i] = Convert.ToString(row[i]);

                        }

                        dataTable.Rows.Add(dr);
                    }

                    counter++;
                }

            }
        }
        catch (Exception ex)
        {

        }
        dataTable.Rows[0].Delete();
        return dataTable;
    }


    public void SaveToDB(string path, string fileName)
    {



        DataTable dtAlert = GetCSVData(path, fileName);
        string strConnstr = System.Configuration.ConfigurationManager.ConnectionStrings["cvtcConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(strConnstr);
        SqlCommand cmd;

        foreach (DataRow dataReader in dtAlert.Rows)
        {
            try
            {
                string bannerID = "";
                string type = "";
                if (dataReader[0] != null && dataReader[0] != "" && dataReader[0] != DBNull.Value)
                {
                    bannerID = Convert.ToString(dataReader[0]);

                }
                if (dataReader[1] != null && dataReader[1] != "" && dataReader[1] != DBNull.Value)
                {
                    type = Convert.ToString(dataReader[1]).Trim();

                }
                string RVP = "RVP";
                string insertQuery = "insert into NTO(BannerID,Type) values('" + bannerID + "','" + RVP + "')";
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = insertQuery;
                con.Open();

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ae)
                {
                    Label1.Text = "Error Occured During Saving data";
                }
            }

            catch (Exception ae)
            {
                Label1.Text = "Error Occured during establish  Connection";
            }
        }

        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    protected void LinkButtonNTO_Click(object sender, EventArgs e)
    {

        Response.AppendHeader("Content-Disposition", "attachment; filename=RVP.CSV");
        Response.TransmitFile(Server.MapPath("~/file/RVP.CSV"));
        Response.End();
    }

    public static DataTable GetCSVRows(string path, string fName)
    {
        string header = "No";
        string sql = string.Empty;
        DataTable dataTable = null;
        string pathOnly = string.Empty;
        string fileName = string.Empty;
        bool IsFirstRowHeader = true;

        try
        {

            pathOnly = path;
            fileName = fName;

            sql = @"SELECT * FROM [" + fileName + "]";

            if (IsFirstRowHeader)
            {
                header = "Yes";
            }

            using (OleDbConnection connection = new OleDbConnection(
                    @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathOnly +
                    ";Extended Properties=\"Text;HDR=" + header + "\""))
            {
                using (OleDbCommand command = new OleDbCommand(sql, connection))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                    {
                        dataTable = new DataTable();
                        //dataTable.Locale = CultureInfo.CurrentCulture;
                        adapter.Fill(dataTable);

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (dataTable.Rows[i]["BannerID"] == null || dataTable.Rows[i]["BannerID"].ToString() == "")
                            {
                                dataTable.Rows.Remove(dataTable.Rows[i]);
                                i--;
                            }
                        }



                        dataTable.Columns[1].ColumnName = "Type";
                        //string strConnection = @"Data Source=.\SQLEXPRESS; Initial Catalog=cvtc; User ID=cvtcuser; Password=cvtcuser";
                        //string strConnection = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();
                        string strConnection = System.Configuration.ConfigurationManager.ConnectionStrings["cvtcConnectionString"].ConnectionString;
                        SqlBulkCopy sqlBulk = new SqlBulkCopy(strConnection);
                        sqlBulk.DestinationTableName = "NTO";
                        sqlBulk.ColumnMappings.Add("BannerID", "BannerID");
                        sqlBulk.ColumnMappings.Add("Type", "Type");
                        sqlBulk.WriteToServer(dataTable);

                    }
                }
            }
        }
        finally
        {

        }

        return dataTable;
    }
}
