using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.IO;
using Entity;
using Common;
using BO;

public partial class Controls_EvaluateCandidate : System.Web.UI.UserControl
{
    SystemUser oSystemUser = new SystemUser();
    Exam oExam = new Exam();

    List<CandidateForExam> oListCandidateForExamForGrid = new List<CandidateForExam>();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            LoadSystemUser();

            LoadSelectedExam();

            if (!IsPostBack)
            {
                LoadCandidatesAccordingToSystemUserForEvaluate();
            }
            else
            {
                //oListCandidateForGrid = (List<Candidate>)Utils.GetSession(SessionManager.csStoreGridView);
                oListCandidateForExamForGrid = (List<CandidateForExam>)this.ViewState[SessionManager.csStoreGridView];
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }

    private void LoadSelectedExam()
    {
        try
        {
            oExam = (Exam)Utils.GetSession(SessionManager.csSelectedExam);

            lbl_ExamName.Text = oExam.ExamName;
        }
        catch (Exception oEx)
        { 
        
        }
    }
    private void LoadSystemUser()
    {
        try
        {
            oSystemUser = (SystemUser)Utils.GetSession(SessionManager.csLoggedUser);
        }
        catch (Exception oEx)
        { 
        
        }
    }

    private void LoadCandidatesAccordingToSystemUserForEvaluate()
    {
        try
        {
            EvaluateProcessBO oEvaluateProcess = new EvaluateProcessBO();
            Result oResult = new Result();

            if (oExam != null && oSystemUser != null)
            {
                oResult = oEvaluateProcess.LoadCandidatesAccordingToSystemUserForEvaluate(oSystemUser, oExam);

                if (oResult.ResultIsSuccess)
                {
                    oListCandidateForExamForGrid = (List<CandidateForExam>)oResult.ResultObject;

                    //Utils.SetSession(SessionManager.csStoreGridView,oListCandidateForGrid);
                    this.ViewState.Add(SessionManager.csStoreGridView, oListCandidateForExamForGrid);

                    if (oSystemUser.SystemUserName.ToLower().Equals("administrator"))
                    {
                        oResult = oEvaluateProcess.EvaluateObjectiveAnswersForAllCandidateOfAnExma(oListCandidateForExamForGrid, oSystemUser, oExam);

                        if (oResult.ResultIsSuccess)
                        {
                            lbl_error.ForeColor = Color.Green;
                            lbl_error.Text = oResult.ResultMessage;
                        }
                        else
                        {
                            lbl_error.ForeColor = Color.Red;
                            lbl_error.Text = oResult.ResultMessage;
                        }
                    }


                    if (oListCandidateForExamForGrid.Count <= 0)
                    {
                        lbl_error.ForeColor = Color.Red;
                        lbl_error.Text = "No Candidate Found For Evaluation.";
                        tbl_candidateLink.Visible = false;
                    }
                    else
                    {
                        tbl_candidateLink.Visible = true;
                        gridCandidates.DataSource = oListCandidateForExamForGrid;
                        gridCandidates.DataBind();
                    }
                }
                else
                {
                    lbl_error.ForeColor = Color.Red;
                    lbl_error.Text = oResult.ResultMessage;
                }
            }
            else
            { 
                
            }
        }
        catch (Exception oEx)
        {
            lbl_error.ForeColor = Color.Red;
            lbl_error.Text = "LoadCandidatesAccordingToSystemUserForEvaluate Exception.";
        }
    }
    protected void gridCandidates_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CandidateForExam oCandidateForExam = e.Row.DataItem as CandidateForExam;

            if (oCandidateForExam != null)
            {
                HyperLink hyperCandidate = e.Row.FindControl("hyperCandidate") as HyperLink;
                Label lblCandidateName = e.Row.FindControl("lblCandidateName") as Label;

                if (hyperCandidate != null && lblCandidateName!=null)
                {
                    lblCandidateName.Text = oCandidateForExam.CandidateForExamCandidate.CandidateName;
                    hyperCandidate.Text = oCandidateForExam.CandidateForExamCandidate.CandidateCompositeID;

                    String sTempCandidateName = oCandidateForExam.CandidateForExamCandidate.CandidateName;
                    String sTempCandidateCompositeID = oCandidateForExam.CandidateForExamCandidate.CandidateCompositeID;
                    String sTempSystemUserName = oSystemUser.SystemUserName;

                    sTempCandidateName = sTempCandidateName.Replace(' ', '-');
                    sTempCandidateCompositeID = sTempCandidateCompositeID.Replace(' ', '-');
                    sTempSystemUserName = sTempSystemUserName.Replace(' ', '-');

                    //hyperCandidate.NavigateUrl = "~/MasterContainer.aspx?option=ExamineProcess&ExamID=" + oExam.ExamID.ToString() + "&CandidateID=" + oCandidate.CandidateCompositeID + "&CandidateName=" + oCandidate.CandidateName + "&QuestionGeneratorID=" + oSystemUser.SystemUserID.ToString() + "&QuestionGeneratorName=" + oSystemUser.SystemUserName;
                    hyperCandidate.NavigateUrl = "~/MasterContainer.aspx?option=ExamineProcess&ExamID=" + oExam.ExamID.ToString() + "&CandidateID=" + sTempCandidateCompositeID + "&CandidateName=" + sTempCandidateName + "&QuestionGeneratorID=" + oSystemUser.SystemUserID.ToString() + "&QuestionGeneratorName=" + sTempSystemUserName;
                }
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }
    protected void gridCandidates_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gridCandidates.PageIndex = e.NewPageIndex;

            gridCandidates.DataSource = oListCandidateForExamForGrid;
            gridCandidates.DataBind();
        }
        catch (Exception oEx)
        { 
        
        }
    }
}
