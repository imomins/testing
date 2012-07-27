using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for Common
/// </summary>
public class Common
{
    public static void ShowDialog(Page page, string title, int width, int height, string divTag)
    {
        string script = "$(document).ready(function() {tb_show('";
        script += title + "', '#TB_inline?height=" + height.ToString();
        script += "&amp;width=" + width.ToString();
        script += "&amp;inlineId=" + divTag + "', null);});";

        ScriptManager.RegisterStartupScript(page, page.GetType(), "", script, true);
    }

    public static void ShowMessage(Page page, string msg)
    {
        
        ScriptManager.RegisterStartupScript(page, page.GetType(), "", "alert('Not Updated');", true);
    }

}
