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
/// Summary description for SessionManager
/// </summary>
public class SessionManager
{
    public SessionManager()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public const String csLoggedUser = "csLoggedUser";
    //public const String csFromPage = "csFromPage";
    public const String csSelectedExam = "csSelectedExam";
    public const String csLoadedPage = "csLoadedPage";
    public const String csStoredCategory = "csStoredCategory";
    public const String csStoredExam = "csStoredExam";
    public const String csStoreGridView = "csStoreGridView"; //This is only for grid view show for a page.. except (ExamFinish.aspx & ExamProcess.ascx use the csStoreGridView veriable) 
    public const String csExamProcess = "csExamProcess";
}
