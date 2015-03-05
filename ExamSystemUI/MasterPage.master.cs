using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using Entity;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            LoadUserTypeAndNameFromSession();
        }
        catch (Exception oEx)
        {
            Response.Redirect("Default.aspx");
        }
    }

    private void LoadUserTypeAndNameFromSession()
    {
        SystemUser oSystemUser = null;
        Candidate oCandidate = null;

        Object oObject = Utils.GetSession(SessionManager.csLoggedUser);
        Type oType = oObject.GetType();

        if (oType.Name.Equals(TypeManager.csSystemUser))
        {
            oSystemUser = new SystemUser();
            oSystemUser = (SystemUser)oObject;

            lbl_SystemUserName.Text = oSystemUser.SystemUserName;

            LoadMenuBarByPermission(oSystemUser, oCandidate);
        }
        //else if(oType.Name.Equals(TypeManager.csCandidate))
        //{
        //    oCandidate = new Candidate();
        //    oCandidate = (Candidate)oObject;

        //    LoadMenuBarByPermission(oSystemUser, oCandidate);
        //}
    }

    private void LoadMenuBarByPermission(SystemUser oSystemUser, Candidate oCandidate)
    {
        String sJSMenu = String.Empty;
        String sLoadedControlName = String.Empty;

        try
        {
            PanelHorizontalMenu.Controls.Clear();

            sLoadedControlName = Utils.GetSession(SessionManager.csLoadedPage).ToString();

            if (oSystemUser != null && oCandidate == null)
            {
                //lbl_ControlName.Text = sLoadedControlName.Substring(0,sLoadedControlName.IndexOf("."));

                if (oSystemUser.SystemUserName.ToLower().Equals(LoginManager.csAdministrator))
                {
                    if (sLoadedControlName.Equals(ControlManager.csSystemUserMainPage) || sLoadedControlName.Equals(ControlManager.csCategoryEntry) || sLoadedControlName.Equals(ControlManager.csCategoryModification) || sLoadedControlName.Equals(ControlManager.csChangePassword) || sLoadedControlName.Equals(ControlManager.csExamEntry) || sLoadedControlName.Equals(ControlManager.csQuestionModification) || sLoadedControlName.Equals(ControlManager.csSystemUserEntry) || sLoadedControlName.Equals(ControlManager.csSystemUserModification) || sLoadedControlName.Equals(ControlManager.csQuestionEntry) || sLoadedControlName.Equals(ControlManager.csExamMofication) || sLoadedControlName.Equals(ControlManager.csLabelEntryAndModification))
                    {
                        sJSMenu = File.ReadAllText(DirectoryManager.csJSMenuBarDirectory + FileManager.csAdminSystemUserMenuFile, System.Text.Encoding.Default);
                        PanelHorizontalMenu.Controls.Add(new LiteralControl(sJSMenu));
                    }
                    else if (sLoadedControlName.Equals(ControlManager.csCandidateSetup) || sLoadedControlName.Equals(ControlManager.csCandidateModification) || sLoadedControlName.Equals(ControlManager.csEvaluateCandidate) || sLoadedControlName.Equals(ControlManager.csExamineProcess) || sLoadedControlName.Equals(ControlManager.csQuestionSetup) || sLoadedControlName.Equals(ControlManager.csQuestionSetupRemove) || sLoadedControlName.Equals(ControlManager.csQuestionReportForAnExam) || sLoadedControlName.Equals(ControlManager.csResultView) || sLoadedControlName.Equals(ControlManager.csCandidateExtend))
                    {
                        sJSMenu = File.ReadAllText(DirectoryManager.csJSMenuBarDirectory + FileManager.csAdminSystemUserSetupMenuFile, System.Text.Encoding.Default);
                        PanelHorizontalMenu.Controls.Add(new LiteralControl(sJSMenu));
                    }
                }
                else //some controls will not shown for the Non Admin
                {
                    if (sLoadedControlName.Equals(ControlManager.csSystemUserMainPage) || sLoadedControlName.Equals(ControlManager.csChangePassword) || sLoadedControlName.Equals(ControlManager.csQuestionModification) || sLoadedControlName.Equals(ControlManager.csQuestionEntry))
                    {
                        sJSMenu = File.ReadAllText(DirectoryManager.csJSMenuBarDirectory + FileManager.csNonAdminSystemUserMenuFile, System.Text.Encoding.Default);
                        PanelHorizontalMenu.Controls.Add(new LiteralControl(sJSMenu));
                    }
                    else if (sLoadedControlName.Equals(ControlManager.csEvaluateCandidate) || sLoadedControlName.Equals(ControlManager.csExamineProcess) || sLoadedControlName.Equals(ControlManager.csQuestionSetup) || sLoadedControlName.Equals(ControlManager.csQuestionSetupRemove) || sLoadedControlName.Equals(ControlManager.csQuestionReportForAnExam) || sLoadedControlName.Equals(ControlManager.csResultView))
                    {
                        sJSMenu = File.ReadAllText(DirectoryManager.csJSMenuBarDirectory + FileManager.csNonAdminSystemUserSetupMenuFile, System.Text.Encoding.Default);
                        PanelHorizontalMenu.Controls.Add(new LiteralControl(sJSMenu));
                    }
                }
            }
        }
        catch (Exception oEx)
        { 
        
        }
        
    }
}
