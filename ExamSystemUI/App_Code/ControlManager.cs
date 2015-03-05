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
/// Summary description for ControlManager
/// </summary>
public class ControlManager
{
    public ControlManager()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public const String csSystemUserEntry = "SystemUserEntry.ascx";
    public const String csSystemUserMainPage = "SystemUserMainPage.ascx";
    public const String csExamEntry= "ExamEntry.ascx";
    public const String csDefault = "Default.aspx";
    public const String csCategoryEntry = "CategoryEntry.ascx";
    public const String csQuestionEntry = "QuestionEntry.ascx";
    public const String csQuestionModification = "QuestionModification.ascx";
    //public const String csSystemUserMainSetUpPage = "SystemUserMainSetUpPage.ascx";
    public const String csCandidateSetup = "CandidateSetup.ascx";
    public const String csEvaluateCandidate = "EvaluateCandidate.ascx";
    public const String csQuestionSetup = "QuestionSetup.ascx";
    public const String csResultView = "ResultView.ascx";
    public const String csChangePassword = "ChangePassword.ascx";
    public const String csExamMofication = "ExamMofication.ascx";
    public const String csExamProcess = "ExamProcess.ascx";
    public const String csQuestionSetupRemove = "QuestionSetupRemove.ascx";
    public const String csCandidateModification = "CandidateModification.ascx";
    public const String csSystemUserModification = "SystemUserModification.ascx";
    public const String csCategoryModification = "CategoryModification.ascx";
    public const String csExamineProcess = "ExamineProcess.ascx";
    public const String csCandidateExtend = "CandidateExtend.ascx";
    public const String csLabelEntryAndModification = "LabelEntryAndModification.ascx";
    public const String csQuestionReportForAnExam = "QuestionReportForAnExam.ascx";
}
