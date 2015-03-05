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
/// Summary description for LoginManager
/// </summary>
public class LoginManager
{
    public LoginManager()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public const String csExaminer = "Examiner";
    public const String csCandidate = "Candidate";
    public const String csAdministrator = "administrator";
}
