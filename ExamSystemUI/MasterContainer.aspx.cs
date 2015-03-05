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
using Entity;

public partial class MasterContainer : System.Web.UI.Page
{
        
    protected void Page_Load(object sender, EventArgs e)
    {
        //plh_Control.Controls.Add(LoadControl("Controls/QuestionEntry.ascx"));

        try
        {
            
            LoadControlInContainer();
        }
        catch (Exception oEx)
        {
            Response.Redirect("Default.aspx");
        }
    }

    private void LoadControlInContainer()
    {
        String sOption = HttpContext.Current.Request.QueryString.Get("option");
        
        //if (Utils.csIsSystemUser)
        if(Utils.GetSession(SessionManager.csLoggedUser).GetType().Name.Equals(TypeManager.csSystemUser))
        {
            SystemUser oSystemUser = new SystemUser();

            Guid gSystemUserID = Guid.Empty;
            String sSystemUserName = String.Empty;
            String sSystemUserPassword = String.Empty;
            
            oSystemUser = (SystemUser)Utils.GetSession(SessionManager.csLoggedUser);

            gSystemUserID = oSystemUser.SystemUserID;
            sSystemUserName = oSystemUser.SystemUserName;
            sSystemUserPassword = oSystemUser.SystemUserPassword;
            
            if (sOption.Equals(OptionManager.csSystemUserMainPage))
            {
                Utils.SetSession(SessionManager.csLoadedPage, ControlManager.csSystemUserMainPage);
                plh_Control.Controls.Add(LoadControl("Controls/" + ControlManager.csSystemUserMainPage));
            }
            else if (sOption.Equals(OptionManager.csExamEntry) && sSystemUserName.ToLower().Equals(LoginManager.csAdministrator))
            {
                Utils.SetSession(SessionManager.csLoadedPage, ControlManager.csExamEntry);
                plh_Control.Controls.Add(LoadControl("Controls/" + ControlManager.csExamEntry));
            }
            else if (sOption.Equals(OptionManager.csDefault))
            {
                Utils.SetSession(SessionManager.csLoadedPage, ControlManager.csDefault);
                plh_Control.Controls.Add(LoadControl("Controls/" + ControlManager.csDefault));
            }
            else if (sOption.Equals(OptionManager.csSystemUserEntry) && sSystemUserName.ToLower().Equals(LoginManager.csAdministrator))
            {
                Utils.SetSession(SessionManager.csLoadedPage, ControlManager.csSystemUserEntry);
                plh_Control.Controls.Add(LoadControl("Controls/" + ControlManager.csSystemUserEntry));
            }
            else if (sOption.Equals(OptionManager.csCategoryEntry) && sSystemUserName.ToLower().Equals(LoginManager.csAdministrator))
            {
                Utils.SetSession(SessionManager.csLoadedPage, ControlManager.csCategoryEntry);
                plh_Control.Controls.Add(LoadControl("Controls/" + ControlManager.csCategoryEntry));
            }
            else if (sOption.Equals(OptionManager.csQuestionEntry))
            {
                Utils.SetSession(SessionManager.csLoadedPage, ControlManager.csQuestionEntry);
                plh_Control.Controls.Add(LoadControl("Controls/" + ControlManager.csQuestionEntry));
            }
            else if (sOption.Equals(OptionManager.csQuestionModification))
            {
                Utils.SetSession(SessionManager.csLoadedPage, ControlManager.csQuestionModification);
                plh_Control.Controls.Add(LoadControl("Controls/" + ControlManager.csQuestionModification));
            }
            else if (sOption.Equals(OptionManager.csChangePassword))
            {
                Utils.SetSession(SessionManager.csLoadedPage, ControlManager.csChangePassword);
                plh_Control.Controls.Add(LoadControl("Controls/" + ControlManager.csChangePassword));
            }
            else if (sOption.Equals(OptionManager.csCandidateSetup) && sSystemUserName.ToLower().Equals(LoginManager.csAdministrator))
            {
                Utils.SetSession(SessionManager.csLoadedPage, ControlManager.csCandidateSetup);
                plh_Control.Controls.Add(LoadControl("Controls/" + ControlManager.csCandidateSetup));
            }
            else if (sOption.Equals(OptionManager.csEvaluateCandidate))
            {
                Utils.SetSession(SessionManager.csLoadedPage, ControlManager.csEvaluateCandidate);
                plh_Control.Controls.Add(LoadControl("Controls/" + ControlManager.csEvaluateCandidate));
            }
            else if (sOption.Equals(OptionManager.csQuestionSetup))
            {
                Utils.SetSession(SessionManager.csLoadedPage, ControlManager.csQuestionSetup);
                plh_Control.Controls.Add(LoadControl("Controls/" + ControlManager.csQuestionSetup));
            }
            else if (sOption.Equals(OptionManager.csResultView))
            {
                Utils.SetSession(SessionManager.csLoadedPage, ControlManager.csResultView);
                plh_Control.Controls.Add(LoadControl("Controls/" + ControlManager.csResultView));
            }
            else if (sOption.Equals(OptionManager.csExamMofication) && sSystemUserName.ToLower().Equals(LoginManager.csAdministrator))
            {
                Utils.SetSession(SessionManager.csLoadedPage, ControlManager.csExamMofication);
                plh_Control.Controls.Add(LoadControl("Controls/" + ControlManager.csExamMofication));
            }
            else if (sOption.Equals(OptionManager.csQuestionSetupRemove))
            {
                Utils.SetSession(SessionManager.csLoadedPage, ControlManager.csQuestionSetupRemove);
                plh_Control.Controls.Add(LoadControl("Controls/" + ControlManager.csQuestionSetupRemove));
            }
            else if (sOption.Equals(OptionManager.csCandidateModification) && sSystemUserName.ToLower().Equals(LoginManager.csAdministrator))
            {
                Utils.SetSession(SessionManager.csLoadedPage, ControlManager.csCandidateModification);
                plh_Control.Controls.Add(LoadControl("Controls/" + ControlManager.csCandidateModification));
            }
            else if (sOption.Equals(OptionManager.csSystemUserModification) && sSystemUserName.ToLower().Equals(LoginManager.csAdministrator))
            {
                Utils.SetSession(SessionManager.csLoadedPage, ControlManager.csSystemUserModification);
                plh_Control.Controls.Add(LoadControl("Controls/" + ControlManager.csSystemUserModification));
            }
            else if (sOption.Equals(OptionManager.csCategoryModification) && sSystemUserName.ToLower().Equals(LoginManager.csAdministrator))
            {
                Utils.SetSession(SessionManager.csLoadedPage, ControlManager.csCategoryModification);
                plh_Control.Controls.Add(LoadControl("Controls/" + ControlManager.csCategoryModification));
            }
            else if (sOption.Equals(OptionManager.csExamineProcess))
            {
                Utils.SetSession(SessionManager.csLoadedPage, ControlManager.csExamineProcess);
                plh_Control.Controls.Add(LoadControl("Controls/" + ControlManager.csExamineProcess));
            }
            else if (sOption.Equals(OptionManager.csCandidateExtend))
            {
                Utils.SetSession(SessionManager.csLoadedPage, ControlManager.csCandidateExtend);
                plh_Control.Controls.Add(LoadControl("Controls/" + ControlManager.csCandidateExtend));
            }
            else if (sOption.Equals(OptionManager.csLabelEntryAndModification))
            {
                Utils.SetSession(SessionManager.csLoadedPage, ControlManager.csLabelEntryAndModification);
                plh_Control.Controls.Add(LoadControl("Controls/" + ControlManager.csLabelEntryAndModification));
            }
            else if (sOption.Equals(OptionManager.csQuestionReportForAnExam))
            {
                Utils.SetSession(SessionManager.csLoadedPage, ControlManager.csQuestionReportForAnExam);
                plh_Control.Controls.Add(LoadControl("Controls/" + ControlManager.csQuestionReportForAnExam));
            }
        }
    }
}
