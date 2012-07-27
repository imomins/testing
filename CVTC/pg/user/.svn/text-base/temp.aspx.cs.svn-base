using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;

public partial class pg_student_temp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [System.Web.Services.WebMethod]
    public static string GetMenu()
    {
        CVTCMenu menu = new CVTCMenu();
        Collection<CVTCMenu> MenuList = menu.GetAllMenu();

        // //{ id: "1", Name: "Main Menu", MenuId: "1", MenuName: "Menu1",
        //   //             level: "0", parent: "", isLeaf: false, expanded: false
        //     //       },
        //string data="[ ";
        //foreach (CVTCMenu m in MenuList)
        //{
        //    data += "{ id:'" + m.OID + "', Name:'" + m.NameMenu + "', MenuId:'" + m.MenuID + "', level:'" + m.MenuLevel + "', parent:'" + m.Parent + "', isLeaf:'"+m.IsLeave+"',expanded:'"+m.IsExpanded+"'},";
        //}
        //data = data.Substring(0,data.Length-1);
        //data+=" ] ";
        //string data = "[{ id: '1', Name: 'Main Menu', MenuId: '1', level: '0', parent: '', isLeaf: 'false', expanded: 'false'}]";
        //return data;
        //object o = new object();
        //o=(object)data;
        //return MenuList;
        //return MenuList.se
        return menu.BuildJQGridResults(MenuList,10,1,2);
    }
}
