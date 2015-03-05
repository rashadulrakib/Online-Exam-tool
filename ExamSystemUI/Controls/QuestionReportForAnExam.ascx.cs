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
using Entity;
using Common;
using BO;

public partial class Controls_QuestionReportForAnExam : System.Web.UI.UserControl
{
    Exam oSelectedExam = new Exam();
    List<QuestionSetup> oListQuestionForGrid = new List<QuestionSetup>();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            LoadSelectedExam();
            
            if (!IsPostBack)
            {
                LoadAllQuestionsOfAnExamToGrid();
            }
            else
            {
                oListQuestionForGrid = (List<QuestionSetup>)this.ViewState[SessionManager.csStoreGridView];
            }
        }
        catch (Exception oEx)
        { 
            
        }
    }

    private void LoadAllQuestionsOfAnExamToGrid()
    {
        try
        {
            QuestionBO oQuestionBO = new QuestionBO();
            Result oResult = new Result();

            oResult = oQuestionBO.LoadAllQuestionsOfAnExam(oSelectedExam);

            if (oResult.ResultIsSuccess)
            {
                oListQuestionForGrid = (List<QuestionSetup>)oResult.ResultObject;

                this.ViewState.Add(SessionManager.csStoreGridView, oListQuestionForGrid);

                if (oListQuestionForGrid.Count > 0)
                {
                    Grid_AllQuestionsOfExam.DataSource = oListQuestionForGrid;
                    Grid_AllQuestionsOfExam.DataBind();

                    lblTotalQuestions.Text = oListQuestionForGrid.Count.ToString();
                }
                else
                {
                    lbl_error.ForeColor = Color.Red;
                    lbl_error.Text = "No Question found.";
                }
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

    private void LoadSelectedExam()
    {
        try
        {
            oSelectedExam = (Exam)Utils.GetSession(SessionManager.csSelectedExam);
            lbl_ExamName.Text = oSelectedExam.ExamName;
        }
        catch (Exception oEx)
        {

        }
    }


    protected void Grid_AllQuestionsOfExam_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Grid_AllQuestionsOfExam.PageIndex = e.NewPageIndex;

            Grid_AllQuestionsOfExam.DataSource = oListQuestionForGrid;
            Grid_AllQuestionsOfExam.DataBind();
        }
        catch (Exception oEx)
        { 
        
        }
    }
    protected void Grid_AllQuestionsOfExam_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            QuestionSetup oQuestionSetup = e.Row.DataItem as QuestionSetup;

            int iChoiceCount = 0;

            //int iQuestionNo = 0;

            if (oQuestionSetup != null)
            {
                Label lblQuestion = e.Row.FindControl("lblQuestion") as Label;
                lblQuestion.Text = Server.HtmlEncode(oQuestionSetup.QuestionSetupQuestion.QuestionText);

                Label lblDisplayObjective = e.Row.FindControl("lblDisplayObjective") as Label;
                

                Panel pnl_ObjectiveAnswer = e.Row.FindControl("pnl_ObjectiveAnswer") as Panel;

                if (oQuestionSetup.QuestionSetupQuestion.QuestionQuestionType.QuestionTypeID == 0)
                {
                    pnl_ObjectiveAnswer.Visible = true;
                    lblDisplayObjective.Visible = true;

                    Table tblChoces = new Table();

                    foreach (Choice oChoice in oQuestionSetup.QuestionSetupQuestion.QuestionObjectiveType.ListOfChoices)
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
                        //lblChoiceText.Text = oChoice.ChoiceName + " ";
                        tblCellChoiceText.Controls.Add(lblChoiceText);

                        TableCell tblCellChkBox = new TableCell();
                        CheckBox chkIsValid = new CheckBox();
                        chkIsValid.ID = "chk" + iChoiceCount.ToString();
                        chkIsValid.Enabled = false;
                        if (oChoice.ChoiceIsValid)
                        {
                            chkIsValid.Checked = oChoice.ChoiceIsValid;
                        }
                        tblCellChkBox.Controls.Add(chkIsValid);

                        tblRow.Cells.Add(tblCellChoiceNumber);
                        tblRow.Cells.Add(tblCellChoiceText);
                        tblRow.Cells.Add(tblCellChkBox);

                        tblChoces.Rows.Add(tblRow);
                    }

                    pnl_ObjectiveAnswer.Controls.Add(tblChoces);
                }
                else
                {
                    lblDisplayObjective.Visible = false;
                    pnl_ObjectiveAnswer.Visible = false;
                }
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }
}
