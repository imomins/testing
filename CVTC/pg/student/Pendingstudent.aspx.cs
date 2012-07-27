using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data.Odbc;
using System.Data;

public partial class pg_student_Pendingstudent : System.Web.UI.Page
{
    string connectionString;
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    [WebMethod]
    public string  CheckBannerID(string id)
    {
    connectionString = System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"].ToString();
    string  res=null;
    using (OdbcConnection connection = new OdbcConnection(connectionString))
    {
        using (OdbcCommand command = new OdbcCommand())
        {
            command.Connection = connection;
            command.CommandText = "{CALL Student_ByStudentBannerID(?)}";
            command.CommandType = CommandType.StoredProcedure;

            //Set Parameter Value
            command.Parameters.AddWithValue("@BannerID", id);

            connection.Open();
            using (OdbcDataReader dataReader = command.ExecuteReader())
            {

                if (dataReader.Read())
                {
                    
                    res = "Exists";
                }
            }
        }
    }

    return res;


    }

 
}
