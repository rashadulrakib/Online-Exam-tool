using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for DirectoryManager
/// </summary>
public class DirectoryManager
{
    public DirectoryManager()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static String csJSMenuBarDirectory = AppDomain.CurrentDomain.BaseDirectory + "MenuTextFiles\\";
    public static String csCandidateCVDirectory = "C:\\Exams\\";
}
