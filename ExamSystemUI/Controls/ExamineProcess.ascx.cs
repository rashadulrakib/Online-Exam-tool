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

public partial class Controls_ExamineProcess : System.Web.UI.UserControl
{
    List<CandidateAnswerQuestion> oListCandidateAnswerQuestion = new List<CandidateAnswerQuestion>();

    SystemUser oSystemUser = new SystemUser();
    Exam oExam = new Exam();

    String sCandidateName = HttpContext.Current.Request.QueryString.Get("CandidateName");
    String sCandidateID = HttpContext.Current.Request.QueryString.Get("CandidateID");
    String sQuestionGeneratorID = HttpContext.Current.Request.QueryString.Get("QuestionGeneratorID");

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sCandidateName = sCandidateName.Replace('-', ' ');
            sCandidateID = sCandidateID.Replace('-', ' ');
            
            LoadSystemUser();

            LoadSelectedExam();
            
            if (!IsPostBack)
            {
                LoadQuestionsForACandidateWhichSetupByAParticularUser();
            }
            else
            {
                //oListCandidateAnswerQuestion = (List<CandidateAnswerQuestion>)Utils.GetSession(SessionManager.csStoreGridView);
                oListCandidateAnswerQuestion = (List<CandidateAnswerQuestion>)this.ViewState[SessionManager.csStoreGridView];
            }
        }
        catch (Exception oEx)
        { 
            
        }
    }

    private void LoadSelectedExam()
    {
        oExam = (Exam)Utils.GetSession(SessionManager.csSelectedExam);

        

    }
    private void LoadSystemUser()
    {
        oSystemUser = (SystemUser)Utils.GetSession(SessionManager.csLoggedUser);
    }

    private void LoadQuestionsForACandidateWhichSetupByAParticularUser()
    {
        try
        {
            lbl_CandidateName.Text = sCandidateName;
            lbl_ExamName.Text = oExam.ExamName;
            lbl_ExamTotal.Text = oExam.ExamTotalMarks.ToString();
            lbl_ExamStartTime.Text = oExam.ExamDateWithStartingTime.ToString();
            ShowExamConstraint(oExam);

            EvaluateProcessBO oEvaluateProcessBO = new EvaluateProcessBO();
            Result oResult = new Result();

            oResult = oEvaluateProcessBO.LoadQuestionsForACandidateWhichSetupByAParticularUser(sCandidateID, oExam, oSystemUser,false);

            if (oResult.ResultIsSuccess)
            {
                oListCandidateAnswerQuestion = (List<CandidateAnswerQuestion>)oResult.ResultObject;

                //Utils.SetSession(SessionManager.csStoreGridView, oListCandidateAnswerQuestion);
                this.ViewState.Add(SessionManager.csStoreGridView, oListCandidateAnswerQuestion);

                if (oListCandidateAnswerQuestion.Count <= 0)
                {
                    lbl_error.ForeColor = Color.Red;
                    lbl_error.Text = "No Question Found.";
                }
                
                Grid_Answers.DataSource = oListCandidateAnswerQuestion;
                Grid_Answers.DataBind();
            }
            else
            {
                lbl_error.ForeColor = Color.Red;
                lbl_error.Text = oResult.ResultMessage;
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }

    private void ShowExamConstraint(Exam oExam)
    {
        if (oExam.ExamConstraint == 0)
        {
            lbl_ExamConstraint.Text = "Full Marking.";
        }
        else if (oExam.ExamConstraint == 1)
        {
            lbl_ExamConstraint.Text = "Partial Marking.";
        }
        else if (oExam.ExamConstraint == 2)
        {
            lbl_ExamConstraint.Text = "Negative Marking.";
        }
        else if (oExam.ExamConstraint == 3)
        {
            lbl_ExamConstraint.Text = "Partial Negative Marking.";
        }
    }
    protected void Grid_Answers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        CandidateAnswerQuestion oCandidateAnswerQuestion = e.Row.DataItem as CandidateAnswerQuestion;

        int iChoiceCount = 0;

        try
        {
            if (oCandidateAnswerQuestion != null)
            {
                Label lblQuestion = e.Row.FindControl("lblQuestion") as Label;
                Panel pnlDescriptiveOrObjective = e.Row.FindControl("pnl_DescriptiveOrObjective") as Panel;
                Label lblSetupMark = e.Row.FindControl("lblSetupMark") as Label;
                
                lblQuestion.Text = Server.HtmlEncode(oCandidateAnswerQuestion.QuestionForCandidateAnswer.QuestionText);
                lblSetupMark.Text = oCandidateAnswerQuestion.QuestionForCandidateAnswer.QuestionDefaultMark.ToString();

                if (oCandidateAnswerQuestion.QuestionForCandidateAnswer.QuestionQuestionType.QuestionTypeID == 0)
                {
                    TextBox txtTempDescriptive = pnlDescriptiveOrObjective.FindControl("txtTempDescriptive") as TextBox;
                    txtTempDescriptive.Visible = false;

                    TextBox txtObtain = e.Row.FindControl("txtObtainMark") as TextBox;
                    txtObtain.Enabled = false;
                    txtObtain.Text = oCandidateAnswerQuestion.ObtainMark.ToString();

                    HyperLink hyperAnswerFilePath = e.Row.FindControl("hyperAnswerFilePath") as HyperLink;
                    hyperAnswerFilePath.Visible = false;

                    Label lbl_AnswerAttachFile = e.Row.FindControl("lbl_AnswerAttachFile") as Label;
                    lbl_AnswerAttachFile.Visible = false;

                    Panel pnlObjectiveAnswer = new Panel();

                    pnlObjectiveAnswer.Width = Unit.Pixel(800);
                    pnlObjectiveAnswer.Height = Unit.Pixel(200);

                    pnlObjectiveAnswer.BorderStyle = BorderStyle.Solid;
                    pnlObjectiveAnswer.BorderWidth = Unit.Pixel(1);

                    Table tblChoces = new Table();

                    int iChoiceNumber = 0;

                    foreach (Choice oChoice in oCandidateAnswerQuestion.QuestionForCandidateAnswer.QuestionObjectiveType.ListOfAnswers)
                    {
                        iChoiceCount = iChoiceCount + 1;

                        TableRow tblRow = new TableRow();

                        TableCell tblCellChoiceNumber = new TableCell();
                        Label lblChoiceNumber = new Label();
                        if (iChoiceCount < 10)
                        {
                            lblChoiceNumber.Text = "0" + iChoiceCount.ToString() + ") ";
                        }
                        else
                        {
                            lblChoiceNumber.Text = iChoiceCount.ToString() + ") ";
                        }
                        tblCellChoiceNumber.Controls.Add(lblChoiceNumber);

                        TableCell tblCellChoiceText = new TableCell();
                        Label lblChoiceText = new Label();
                        lblChoiceText.Text =Server.HtmlEncode(oChoice.ChoiceName + " ");
                        tblCellChoiceText.Controls.Add(lblChoiceText);

                        TableCell tblCellChkBox = new TableCell();
                        CheckBox chkIsValid = new CheckBox();
                        chkIsValid.ID = "chk" + iChoiceCount.ToString();
                        if (oChoice.ChoiceIsValid)
                        {
                            chkIsValid.Checked = oChoice.ChoiceIsValid;
                        }
                        chkIsValid.Enabled = false;
                        tblCellChkBox.Controls.Add(chkIsValid);

                        TableCell tblCellTxtOrginalAnswer = new TableCell();
                        Label lblTxtOrginalAnswer = new Label();
                        lblTxtOrginalAnswer.Text = " Orginal Choice ";
                        tblCellTxtOrginalAnswer.Controls.Add(lblTxtOrginalAnswer);

                        TableCell tblCellOrginalChoice = new TableCell();
                        CheckBox chkIsValidChoice = new CheckBox();
                        chkIsValidChoice.Checked = oCandidateAnswerQuestion.QuestionForCandidateAnswer.QuestionObjectiveType.ListOfChoices[iChoiceNumber].ChoiceIsValid;
                        chkIsValidChoice.Enabled = false;
                        tblCellOrginalChoice.Controls.Add(chkIsValidChoice);


                        tblRow.Cells.Add(tblCellChoiceNumber);
                        tblRow.Cells.Add(tblCellChoiceText);
                        tblRow.Cells.Add(tblCellChkBox);
                        tblRow.Cells.Add(tblCellTxtOrginalAnswer);
                        tblRow.Cells.Add(tblCellOrginalChoice);

                        iChoiceNumber = iChoiceNumber + 1;

                        tblChoces.Rows.Add(tblRow);
                    }

                    //Grid_Answers.Attributes.Add("onclick", "getChoiceValue(" + iChoiceCount + ")");

                    pnlObjectiveAnswer.Controls.Add(tblChoces);

                    pnlDescriptiveOrObjective.Controls.Add(pnlObjectiveAnswer); //pnlObjectiveAnswer contains the choices
                }
                else if (oCandidateAnswerQuestion.QuestionForCandidateAnswer.QuestionQuestionType.QuestionTypeID == 1)
                {
                    TextBox txtTempDescriptive = pnlDescriptiveOrObjective.FindControl("txtTempDescriptive") as TextBox;
                    txtTempDescriptive.Visible = true;

                    TextBox txtObtain = e.Row.FindControl("txtObtainMark") as TextBox;
                    txtObtain.Enabled = true;

                    HyperLink hyperAnswerFilePath = e.Row.FindControl("hyperAnswerFilePath") as HyperLink;
                    hyperAnswerFilePath.Visible = true;

                    Label lbl_AnswerAttachFile = e.Row.FindControl("lbl_AnswerAttachFile") as Label;
                    lbl_AnswerAttachFile.Visible = true;

                    //Grid_Answers.Attributes.Add("onclick", "getTextBoxValue('" + txtTempDescriptive.ClientID + "')");

                    //show the candidate Last answer
                    String sWithLastSlash = oCandidateAnswerQuestion.sAnswerAttachFilePath.Substring(0, oCandidateAnswerQuestion.sAnswerAttachFilePath.LastIndexOf('\\') + 1);
                    String sAnswerFileName = oCandidateAnswerQuestion.sAnswerAttachFilePath.Substring(oCandidateAnswerQuestion.sAnswerAttachFilePath.LastIndexOf('\\') + 1);
                    String sQuestionIDWithUnderScode = sAnswerFileName.Substring(0, sAnswerFileName.IndexOf('_')+1);
                    String sOrginalAnswerFileName = sAnswerFileName.Substring(sAnswerFileName.IndexOf('_') + 1);

                    sWithLastSlash = sWithLastSlash.Replace(' ', '-');
                    sOrginalAnswerFileName = sOrginalAnswerFileName.Replace(' ', '-');

                    hyperAnswerFilePath.Text = sOrginalAnswerFileName;
                    hyperAnswerFilePath.NavigateUrl = "~/AnswerFileShow.aspx?WithLastSlash=" + sWithLastSlash + "&QuestionIDWithUnderScode=" + sQuestionIDWithUnderScode + "&OrginalAnswerFileName=" + sOrginalAnswerFileName;

                    txtTempDescriptive.Text = oCandidateAnswerQuestion.DescriptiveQuestionAnswerText;
                    txtObtain.Text = oCandidateAnswerQuestion.ObtainMark.ToString();
                }
            }
        }
        catch (Exception oEx)
        {

        }
    }
    protected void Grid_Answers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            PopulateWithCurrentGridViewState(oListCandidateAnswerQuestion[Grid_Answers.PageIndex], Grid_Answers.PageIndex);

            int newPageIndex = 0;

            newPageIndex = e.NewPageIndex;
            newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
            newPageIndex = newPageIndex >= Grid_Answers.PageCount ? Grid_Answers.PageCount - 1 : newPageIndex;

            Grid_Answers.PageIndex = newPageIndex;

            Grid_Answers.DataSource = oListCandidateAnswerQuestion;
            Grid_Answers.DataBind();
        }
        catch (Exception oEx)
        { 
        
        }
    }

    private void PopulateWithCurrentGridViewState(CandidateAnswerQuestion oCandidateAnswerQuestion, int iQuestionIndex)
    {
        //ctl00_CPH_MainCandidate_ctl00_Grid_Answers_ctl02_txtTempDescriptive

        //ctl00_CPH_MainCandidate_ctl00_Grid_Answers_ctl02_chk1
        //ctl00_CPH_MainCandidate_ctl00_Grid_Answers_ctl02_chk2

        //ctl00_CPH_MainCandidate_ctl00_HiddenForGridChoices
        //ctl00_CPH_MainCandidate_ctl00_HiddenForGridTextBox
        float fMark = 0f;
        float f = 0f;

        try
        {
            if (oCandidateAnswerQuestion.QuestionForCandidateAnswer.QuestionQuestionType.QuestionTypeID == 1)
            {
                TextBox txtObtainMark = Grid_Answers.Rows[0].FindControl("txtObtainMark") as TextBox;

                if(float.TryParse(txtObtainMark.Text,out f))
                {
                    fMark = float.Parse(txtObtainMark.Text);

                    if (fMark >= 0f && fMark <= oCandidateAnswerQuestion.QuestionForCandidateAnswer.QuestionDefaultMark)
                    {
                        oListCandidateAnswerQuestion[iQuestionIndex].ObtainMark = fMark;    
                    }
                }

                //Utils.SetSession(SessionManager.csStoreGridView, oListCandidateAnswerQuestion);
                this.ViewState.Add(SessionManager.csStoreGridView, oListCandidateAnswerQuestion);
                
                //Utils.SetSession(Utils.GetSession(SessionManager.csLastCategoryAndExam).ToString(), oListCandidateAnswerQuestion);
                //this.ViewState.Add("Common", oListCandidateAnswerQuestion);
            }
        }
        catch (Exception oEx)
        {

        }
    }
    protected void btn_Evaluation_Click(object sender, EventArgs e)
    {
        try
        {
            Result oResult = new Result();
            EvaluateProcessBO oEvaluateProcessBO = new EvaluateProcessBO();
            
            PopulateWithCurrentGridViewState(oListCandidateAnswerQuestion[Grid_Answers.PageIndex], Grid_Answers.PageIndex);

            Grid_Answers.DataSource = oListCandidateAnswerQuestion;
            Grid_Answers.DataBind();

            oResult = oEvaluateProcessBO.SaveDescriptiveAnswerMarksForCandidatesForASystemUserOfAnExam(sCandidateID,oListCandidateAnswerQuestion, oExam, oSystemUser);

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
        catch (Exception oEx)
        {
            lbl_error.ForeColor = Color.Red;
            lbl_error.Text = "SaveDescriptiveAnswerMarksForCandidatesForASystemUserOfAnExam Exception.";
        }
    }
}
