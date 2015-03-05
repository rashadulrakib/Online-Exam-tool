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
/// Summary description for FileManager
/// </summary>
public class FileManager
{
    public FileManager()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public const String csAdminSystemUserMenuFile = "AdminSystemUser.txt";
    public const String csNonAdminSystemUserMenuFile = "NonAdminSystemUser.txt";
    public const String csAdminSystemUserSetupMenuFile = "AdminSystemUserSetup.txt";
    public const String csNonAdminSystemUserSetupMenuFile = "NonAdminSystemUserSetup.txt";
    public const String csCandidateMenuFile = "Candidate.txt";
}
