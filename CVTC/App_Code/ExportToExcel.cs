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
using System.IO;

 
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

 

 

/// <summary>
/// Summary description for ExportToExcel
/// </summary>
public class ExportToExcel
{
	public ExportToExcel()
	{
		//
		// TODO: Add constructor logic here
		//
	}



    public static void ExportToSpreadsheet(DataTable table, string reportOIDStr, String reportType)
    {
        try
        {
            Reports report = null;
            if (!string.IsNullOrEmpty(reportOIDStr))
            {
                report = new Reports().GetReportByOID(Int32.Parse(reportOIDStr));
            }
            if ((report == null) || (table == null))
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
                            table.Columns.Remove(nameValue[0]);
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
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        tempStr = Convert.ToString(table.Rows[i][j]);
                        if (tempStr.Contains(','))
                        {
                            table.Rows[i][j] = tempStr.Replace(',', ' ');
                        }
                    }
                }

                HttpContext context = HttpContext.Current;
                context.Response.Clear();
                context.Response.ContentType = "text/csv";
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + report.ReportName +"_"+DateTime .Now .ToShortDateString ()+ ".csv");

                //Write a row for column names
                foreach (DataColumn dataColumn in table.Columns)
                context.Response.Write(dataColumn.ColumnName + ",");
                context.Response.Write(Environment.NewLine);

                //Write one row for each DataRow
                foreach (DataRow dataRow in table.Rows)
                {
                    for (int dataColumnCount = 0; dataColumnCount < table.Columns.Count; dataColumnCount++)
                        context.Response.Write(dataRow[dataColumnCount].ToString() + ",");
                    context.Response.Write(Environment.NewLine);
                }
                context.Response.End();
            }
            else if (reportType == "Excel")
            {
                DataGrid dtaFinal = new DataGrid();
                dtaFinal.DataSource = table;
                dtaFinal.DataBind();

                dtaFinal.HeaderStyle.ForeColor = System.Drawing.Color.White;
                dtaFinal.HeaderStyle.BackColor = System.Drawing.Color.DarkGray;
                dtaFinal.ItemStyle.BackColor = System.Drawing.Color.White;
                dtaFinal.AlternatingItemStyle.BackColor = System.Drawing.Color.AliceBlue;

                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                dtaFinal.RenderControl(hw);

                HttpContext context = HttpContext.Current;
                context.Response.Buffer = true;
                context.Response.Clear();

                context.Response.ContentType = "application/ms-excel";
                context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + report.ReportName + ".xls");

                context.Response.Write(sw.ToString());
                context.Response.Flush();
                context.Response.Close();
                context.Response.End();
            }
            else if (reportType == "PDF")
            {
                HttpResponse Response = HttpContext.Current.Response;
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + report.ReportName +"_"+DateTime .Now.ToShortDateString ()+ ".pdf");

                // step 1: creation of a document-object
                Document document = new Document(new Rectangle(880f, 612f), 20, 20, 30, 20);

                // step 2: we create a writer that listens to the document
                PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);



                document.AddTitle(report.ReportName);

                //Phrase phFooter = new Phrase(""); //empty phrase for page numbering
                //HeaderFooter footer = new HeaderFooter(phFooter, true);
                //document.Footer = footer;

                // step 3: we open the document
                document.Open();

                // step 4: we add content to the document


                ////document.Add(FormatHeaderPhrase(table.TableName));
                PdfPTable pdfTable = new PdfPTable(table.Columns.Count);
                pdfTable.DefaultCell.Padding = 3;
                pdfTable.WidthPercentage = 100; // percentage
                pdfTable.DefaultCell.BorderWidth = 2;
                pdfTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;

                foreach (DataColumn column in table.Columns)
                {
                    pdfTable.AddCell(column.ColumnName);
                }
                pdfTable.HeaderRows = 1;  // this is the end of the table header
                pdfTable.DefaultCell.BorderWidth = 1;
                Color altRow = new Color(242, 242, 242);
                int i = 0;
                foreach (DataRow row in table.Rows)
                {
                    i++;
                    if (i % 2 == 1)
                    {
                        pdfTable.DefaultCell.BackgroundColor = altRow;
                    }
                    foreach (object cell in row.ItemArray)
                    {
                        //assume toString produces valid output
                        ////pdfTable.AddCell(FormatPhrase(cell.ToString()));
                        pdfTable.AddCell(Convert.ToString(cell));
                    }
                    if (i % 2 == 1)
                    {
                        pdfTable.DefaultCell.BackgroundColor = Color.WHITE;
                    }
                }
                document.Add(pdfTable);

                // step 5: we close the document
                document.Close();
            }

        }
        catch (Exception ex)
        { }
    }
 
}
