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

public partial class Controls_ExamProcess : System.Web.UI.UserControl
{
    List<CandidateAnswerQuestion> oListCandidateAnswerQuestion = new List<CandidateAnswerQuestion>();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                PrepareMenubar();
                //LoadQuestionsForACandidateInExam();
            }
            else
            {
                oListCandidateAnswerQuestion = (List<CandidateAnswerQuestion>)Utils.GetSession(SessionManager.csExamProcess);

                Grid_Answers.DataSource = oListCandidateAnswerQuestion;
                Grid_Answers.DataBind();
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }

    private void PrepareMenubar()
    {
        LoadCategoriesWithType();
    }

    private void LoadCategoriesWithType()
    {
        Result oResult = new Result();
        CandidateExamProcessBO oCandidateExamProcessBO = new CandidateExamProcessBO();

        List<CandidateMenu> oListCandidateMenu = new List<CandidateMenu>();

        try
        {
            CandidateForExam oCandidateForExam = Utils.GetSession(SessionManager.csLoggedUser) as CandidateForExam;

            lbl_CandidateName.Text = oCandidateForExam.CandidateForExamCandidate.CandidateName;
            lbl_ExamName.Text = oCandidateForExam.CadidateCandidateExam.CandiadteExamExam.ExamName;
            lbl_ExamDuration.Text = oCandidateForExam.CadidateCandidateExam.CandiadteExamExam.ExamDurationinHour.ToString();
            lbl_ExamTotal.Text = oCandidateForExam.CadidateCandidateExam.CandiadteExamExam.ExamTotalMarks.ToString();

            ShowExamConstraint(oCandidateForExam.CadidateCandidateExam.CandiadteExamExam);

            oResult = oCandidateExamProcessBO.LoadCategoriesWithType(oCandidateForExam);

            if (oResult.ResultIsSuccess)
            {
                oListCandidateMenu = oResult.ResultObject as List<CandidateMenu>;

                TreeNode oCategoryRoot = new TreeNode("Categories", "Categories");

                treeViewQuestions.Nodes.Add(oCategoryRoot);

                foreach (CandidateMenu oCandidateMenuInList in oListCandidateMenu) //CandidateMenu= Category Name
                {
                    TreeNode oCategoryName = new TreeNode(oCandidateMenuInList.CandidateMenuCategory.CategoryName, oCandidateMenuInList.CandidateMenuCategory.CategoryID.ToString());
                    
                    oCategoryRoot.ChildNodes.Add(oCategoryName);
                   
                    foreach (QuestionType oQuestionTypeInList in oCandidateMenuInList.CandidateMenuCategoryQuestionType)
                    {
                        TreeNode oTypeName = new TreeNode(oQuestionTypeInList.QuestionTypeName, oQuestionTypeInList.QuestionTypeName+":"+oQuestionTypeInList.QuestionTypeTotalQuestions.ToString());

                        oCategoryName.ChildNodes.Add(oTypeName);

                        
                        
                        LoadQuestionsRandomlyAccordingToCategoryAndType(oCandidateMenuInList.CandidateMenuCategory, oQuestionTypeInList);
                    
                    
                    }
                }

                SetoListCandidateAnswerQuestionIntoSession();

                if (treeViewQuestions.Nodes[0].ChildNodes[0].ChildNodes[0] != null)
                {
                    treeViewQuestions.Nodes[0].ChildNodes[0].ChildNodes[0].Selected = true;
                    //treeViewQuestions.Nodes[0].ChildNodes[0].Selected = true;
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

    private void SetoListCandidateAnswerQuestionIntoSession()
    {
        try
        {
            Utils.SetSession(SessionManager.csExamProcess, oListCandidateAnswerQuestion); // no need to set

            Grid_Answers.DataSource = oListCandidateAnswerQuestion; //no need
            Grid_Answers.DataBind(); //no need

            if (Grid_Answers.PageIndex == 0)
            {
                GridViewRow oGridViewRow = Grid_Answers.BottomPagerRow;

                LinkButton LinkButtonPrev = oGridViewRow.FindControl("LinkButtonPrev") as LinkButton;

                if (LinkButtonPrev != null)
                {
                    LinkButtonPrev.Enabled = false;
                }
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }

    private void LoadQuestionsRandomlyAccordingToCategoryAndType(Category oCategory, QuestionType oQuestionTypeInList)
    {
        try
        {
            Result oResult = new Result();
            CandidateExamProcessBO oCandidateExamProcessBO = new CandidateExamProcessBO();

            List<CandidateAnswerQuestion> oSubListCandidateAnswerQuestion = new List<CandidateAnswerQuestion>();

            CandidateForExam oCandidateForExam = Utils.GetSession(SessionManager.csLoggedUser) as CandidateForExam;

            oResult = oCandidateExamProcessBO.LoadQuestionsForACandidateInExamByCategoryAndType(oCategory,oQuestionTypeInList,oCandidateForExam);

            if (oResult.ResultIsSuccess)
            {
                oSubListCandidateAnswerQuestion = RandomizeQuestions((List<CandidateAnswerQuestion>)oResult.ResultObject);

                oListCandidateAnswerQuestion.AddRange(oSubListCandidateAnswerQuestion);
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

    //private void LoadQuestionsForACandidateInExam()
    //{
    //    Result oResult = new Result();
    //    CandidateExamProcessBO oCandidateExamProcessBO = new CandidateExamProcessBO();

    //    List<CandidateAnswerQuestion> oList = new List<CandidateAnswerQuestion>();

    //    try
    //    {
    //        CandidateForExam oCandidateForExam = Utils.GetSession(SessionManager.csLoggedUser) as CandidateForExam;

    //        oResult = oCandidateExamProcessBO.LoadQuestionsForACandidateInExam(oCandidateForExam);

    //        if (oResult.ResultIsSuccess)
    //        {
    //            oListCandidateAnswerQuestion = (List<CandidateAnswerQuestion>)oResult.ResultObject;



    //            //oListCandidateAnswerQuestion = RandomizeQuestions(oListCandidateAnswerQuestion);

                
                
    //            Utils.SetSession(SessionManager.csExamProcess, oListCandidateAnswerQuestion); // no need to set

    //            Grid_Answers.DataSource = oListCandidateAnswerQuestion; //no need
    //            Grid_Answers.DataBind(); //no need

    //            if (Grid_Answers.PageIndex == 0)
    //            {
    //                GridViewRow oGridViewRow = Grid_Answers.BottomPagerRow;

    //                LinkButton LinkButtonPrev = oGridViewRow.FindControl("LinkButtonPrev") as LinkButton;

    //                if (LinkButtonPrev != null)
    //                {
    //                    LinkButtonPrev.Enabled = false;
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception oEx)
    //    { 
        
    //    }
    //}

    private List<CandidateAnswerQuestion> RandomizeQuestions(List<CandidateAnswerQuestion> liCandidateExamQuestion)
    {
        List<CandidateAnswerQuestion> oListNewCandidateAnswerQuestion = new List<CandidateAnswerQuestion>();
        List<CandidateAnswerQuestion> oListBaseCandidateAnswerQuestion = new List<CandidateAnswerQuestion>();
        
        try
        {
            foreach (CandidateAnswerQuestion oCandidateAnswerQuestionInList  in liCandidateExamQuestion)
            {
                oListBaseCandidateAnswerQuestion.Add(oCandidateAnswerQuestionInList);
            }

            int iCurrrent = 0;
            while (oListNewCandidateAnswerQuestion.Count < oListBaseCandidateAnswerQuestion.Count)
            {
                Random r = new Random();
                iCurrrent = r.Next(0, oListBaseCandidateAnswerQuestion.Count);
                CandidateAnswerQuestion oTempCandidateAnswerQuestion = oListNewCandidateAnswerQuestion.Find(delegate(CandidateAnswerQuestion oCandidateAnswerQuestion) { return oCandidateAnswerQuestion.QuestionForCandidateAnswer.QuestionID == oListBaseCandidateAnswerQuestion[iCurrrent].QuestionForCandidateAnswer.QuestionID; });

                if (null==oTempCandidateAnswerQuestion)
                {
                    oListNewCandidateAnswerQuestion.Add(oListBaseCandidateAnswerQuestion[iCurrrent]);
                }
            }

        }
        catch (Exception oEx)
        { 
        
        }

        return oListNewCandidateAnswerQuestion;    
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

            int iPageIndexSum = 0;

            bool flag = false;

            TreeNode SelectedCategory = null;
            TreeNode SelectedType = null;

            int iCategoryCount = -1;
            int iTypeCount = -1;

            int iCategoryIndex = 0;
            int iTypeIndex = 0;

            foreach (TreeNode oCategory in treeViewQuestions.Nodes[0].ChildNodes)
            {
                iCategoryCount = iCategoryCount + 1;

                iTypeCount = -1;

                foreach (TreeNode oType in oCategory.ChildNodes)
                {
                    iTypeCount = iTypeCount + 1;

                    if (newPageIndex < iPageIndexSum)
                    {
                        flag = true;
                        break;
                    }
                    else
                    {
                        iCategoryIndex = iCategoryCount;
                        iTypeIndex = iTypeCount;

                        SelectedCategory = oCategory;
                        SelectedType = oType;
                    }

                    iPageIndexSum = iPageIndexSum + int.Parse(oType.Value.Substring(oType.Value.IndexOf(":") + 1));
                }

                if (flag)
                {
                    break;
                }
            }

            //if (SelectedCategory != null && SelectedType != null && iCategoryIndex>=0 && iTypeIndex>=0)
            if (SelectedCategory != null && SelectedType != null)
            {
                //SelectedCategory.Selected = true;
                SelectedType.Selected = true;

                //treeViewQuestions.Nodes[0].ChildNodes[iCategoryIndex].ChildNodes[iTypeIndex].Selected = true;
                //treeViewQuestions.Nodes[0].ChildNodes[iCategoryIndex].Selected = true;
            }


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

        try
        {
            int iNextStartIndex = 0;
            int Index = 0;
            int iChoiceCount=0;

            String sHiddenChoices = String.Empty;
            String sHiddenTextBox = String.Empty;
            String sChkValue = String.Empty;

            if (oCandidateAnswerQuestion.QuestionForCandidateAnswer.QuestionQuestionType.QuestionTypeID == 0)
            {
                sHiddenChoices = HiddenForGridChoices.Value;

                while (iNextStartIndex < sHiddenChoices.Length && sHiddenChoices.IndexOf('@', iNextStartIndex) >= 0)
                {
                    Index = sHiddenChoices.IndexOf('@', iNextStartIndex);

                    sChkValue = sHiddenChoices.Substring(iNextStartIndex, Index - iNextStartIndex);

                    oListCandidateAnswerQuestion[iQuestionIndex].QuestionForCandidateAnswer.QuestionObjectiveType.ListOfChoices[iChoiceCount].ChoiceIsValid = Convert.ToBoolean(sChkValue);

                    iChoiceCount = iChoiceCount + 1;

                    iNextStartIndex = Index + 1;
                }

                Utils.SetSession(SessionManager.csExamProcess, oListCandidateAnswerQuestion);
                //Utils.SetSession(Utils.GetSession(SessionManager.csLastCategoryAndExam).ToString(), oListCandidateAnswerQuestion);
                //this.ViewState.Add("Common", oListCandidateAnswerQuestion);
            }
            else if (oCandidateAnswerQuestion.QuestionForCandidateAnswer.QuestionQuestionType.QuestionTypeID == 1)
            {
                sHiddenTextBox = HiddenForGridTextBox.Value;

                Char[] cArr = new Char[sHiddenTextBox.Length];

                cArr = sHiddenTextBox.ToCharArray();

                for (int i = 0; i < cArr.Length; i++)
                {
                    if (cArr[i].ToString().Equals("'"))
                    {
                        cArr[i] = ' ';
                    }
                }

                sHiddenTextBox = new String(cArr);

                oListCandidateAnswerQuestion[iQuestionIndex].DescriptiveQuestionAnswerText = sHiddenTextBox;

                if (IsValidAttachedFile((FileUpload)Grid_Answers.Rows[0].FindControl("fupAnswerAttach")))
                {
                    oListCandidateAnswerQuestion[iQuestionIndex].ClientFileBytes = (Grid_Answers.Rows[0].FindControl("fupAnswerAttach") as FileUpload).FileBytes;
                    oListCandidateAnswerQuestion[iQuestionIndex].sAnswerAttachFilePath = (Grid_Answers.Rows[0].FindControl("fupAnswerAttach") as FileUpload).FileName;//HiddenForGetClientFileName.Value.Substring(HiddenForGetClientFileName.Value.LastIndexOf('\\') + 1, HiddenForGetClientFileName.Value.Length - HiddenForGetClientFileName.Value.LastIndexOf('\\') - 1); //Here sAnswerAttachFilePath is used to show only the client Answer File Name
                }

                Utils.SetSession(SessionManager.csExamProcess, oListCandidateAnswerQuestion);
                //Utils.SetSession(Utils.GetSession(SessionManager.csLastCategoryAndExam).ToString(), oListCandidateAnswerQuestion);
                //this.ViewState.Add("Common", oListCandidateAnswerQuestion);
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }
    protected void Grid_Answers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        CandidateAnswerQuestion oCandidateAnswerQuestion = e.Row.DataItem as CandidateAnswerQuestion;

        int iChoiceCount=0;

        int iQuestionNo=0;

        try
        {
            if (oCandidateAnswerQuestion != null)
            {
                Label lblQuestion = e.Row.FindControl("lblQuestion") as Label;
                Panel pnlDescriptiveOrObjective = e.Row.FindControl("pnl_DescriptiveOrObjective") as Panel;
                Label lblQuestionNo = e.Row.FindControl("lblQuestionNo") as Label;
                Label lblQuestionMark = e.Row.FindControl("lblQuestionMark") as Label;

                iQuestionNo = Grid_Answers.PageIndex+1;

                lblQuestionNo.Text = iQuestionNo.ToString() + "] of " + Grid_Answers.PageCount.ToString();
                lblQuestion.Text = Server.HtmlEncode(oCandidateAnswerQuestion.QuestionForCandidateAnswer.QuestionText);
                lblQuestionMark.Text = oCandidateAnswerQuestion.QuestionForCandidateAnswer.QuestionDefaultMark.ToString();

                if (oCandidateAnswerQuestion.QuestionForCandidateAnswer.QuestionQuestionType.QuestionTypeID == 0)
                {
                    TextBox txtTempDescriptive = pnlDescriptiveOrObjective.FindControl("txtTempDescriptive") as TextBox;
                    txtTempDescriptive.Visible = false;
                    FileUpload fupAnswerAttach = e.Row.FindControl("fupAnswerAttach") as FileUpload;
                    fupAnswerAttach.Visible = false;
                    Label lbllastAttachedFile = e.Row.FindControl("lbllastAttachedFile") as Label;
                    lbllastAttachedFile.Visible = false;

                    e.Row.FindControl("tdLastAttcahedFileContainer").Visible = false;
                    e.Row.FindControl("tdAttcahedFileFormatContainer").Visible = false;

                    Panel pnlObjectiveAnswer = new Panel();

                    pnlObjectiveAnswer.Width = Unit.Pixel(800);
                    pnlObjectiveAnswer.Height = Unit.Pixel(200);

                    pnlObjectiveAnswer.BorderStyle = BorderStyle.Solid;
                    pnlObjectiveAnswer.BorderWidth = Unit.Pixel(1);

                    Table tblChoces = new Table();

                    foreach (Choice oChoice in oCandidateAnswerQuestion.QuestionForCandidateAnswer.QuestionObjectiveType.ListOfChoices)
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
                        lblChoiceText.Text = Server.HtmlEncode(oChoice.ChoiceName + " ");
                        tblCellChoiceText.Controls.Add(lblChoiceText);

                        TableCell tblCellChkBox = new TableCell();
                        CheckBox chkIsValid = new CheckBox();
                        chkIsValid.ID = "chk" + iChoiceCount.ToString();
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

                    Grid_Answers.Attributes.Add("onclick", "getChoiceValue(" + iChoiceCount + ")");

                    pnlObjectiveAnswer.Controls.Add(tblChoces);

                    pnlDescriptiveOrObjective.Controls.Add(pnlObjectiveAnswer); //pnlObjectiveAnswer contains the choices
                }
                else if (oCandidateAnswerQuestion.QuestionForCandidateAnswer.QuestionQuestionType.QuestionTypeID == 1)
                {
                    e.Row.FindControl("tdLastAttcahedFileContainer").Visible = true;
                    e.Row.FindControl("tdAttcahedFileFormatContainer").Visible = true;
                    
                    TextBox txtTempDescriptive = pnlDescriptiveOrObjective.FindControl("txtTempDescriptive") as TextBox;
                    txtTempDescriptive.Visible = true;

                    FileUpload fupAnswerAttach = e.Row.FindControl("fupAnswerAttach") as FileUpload;
                    fupAnswerAttach.Visible = true;

                    Label lbllastAttachedFile = e.Row.FindControl("lbllastAttachedFile") as Label;
                    lbllastAttachedFile.Visible = true;

                    Grid_Answers.Attributes.Add("onclick", "getTextBoxValue('" + txtTempDescriptive.ClientID+ "')");
                    //HiddenForGetClientFileName is unnecessary
                    
                    //show the candidate Last answer
                    txtTempDescriptive.Text = oCandidateAnswerQuestion.DescriptiveQuestionAnswerText;
                    lbllastAttachedFile.Text = oCandidateAnswerQuestion.sAnswerAttachFilePath;
                    
                }
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }
    private bool IsValidAttachedFile(FileUpload oFileUpload)
    {
        if (!oFileUpload.HasFile)
        {
            return false;
        }

        //if (oFileUpload.FileName.ToLower().LastIndexOf(".htm") > 0 || oFileUpload.FileName.ToLower().LastIndexOf(".html") > 0 || oFileUpload.FileName.ToLower().LastIndexOf(".doc") > 0 || oFileUpload.FileName.ToLower().LastIndexOf(".pdf") > 0 || oFileUpload.FileName.ToLower().LastIndexOf(".rtf") > 0)
        if (oFileUpload.FileName.ToLower().LastIndexOf(".doc") > 0 || oFileUpload.FileName.ToLower().LastIndexOf(".vsd") > 0)
        {

        }
        else
        {
            return false;
        }

        for (int i = 0; i < oFileUpload.FileName.Length; i++)
        {
            if (oFileUpload.FileName[i] == '-' || oFileUpload.FileName[i].ToString().Equals("'"))
            {
                return false;
            }
        }

        return true;
    }
    protected void btn_Finish_Click(object sender, EventArgs e)
    {
        try
        {
            PopulateWithCurrentGridViewState(oListCandidateAnswerQuestion[Grid_Answers.PageIndex], Grid_Answers.PageIndex);

            /*Grid_Answers.DataSource = oListCandidateAnswerQuestion;
            Grid_Answers.DataBind();

            Candidate oCandidate = Utils.GetSession(SessionManager.csLoggedUser) as Candidate;

            oCandidate.CadidateCandidateExam.CandidateAnsweredQuestions = oListCandidateAnswerQuestion;

            oResult = oCandidateExamProcessBO.SaveCandidateAnswers(oCandidate);

            if (oResult.ResultIsSuccess)
            {
                lbl_error.ForeColor = Color.Green;
                lbl_error.Text = oResult.ResultMessage;
            }
            else
            {
                lbl_error.ForeColor = Color.Red;
                lbl_error.Text = oResult.ResultMessage;
            }*/

            Response.Redirect("ExamFinish.aspx");
        }
        catch (Exception oEx)
        { 
        
        }
        
    }

    protected void treeViewQuestions_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            TreeNode trNselected = treeViewQuestions.SelectedNode;

            //trNselected.Selected = true;

            PopulateWithCurrentGridViewState(oListCandidateAnswerQuestion[Grid_Answers.PageIndex], Grid_Answers.PageIndex);

            int iPageIndexSum = 0;

            if (trNselected.Text.Equals("Objective") || trNselected.Text.Equals("Descriptive"))
            {
                TreeNode RootNode = treeViewQuestions.Nodes[0];

                bool flag = false;

                foreach (TreeNode oCategoryName in RootNode.ChildNodes)
                {
                    foreach (TreeNode oTypeName in oCategoryName.ChildNodes)
                    {
                        if (oTypeName.ValuePath.Equals(trNselected.ValuePath))
                        {
                            flag = true;

                            break;
                        }
                        else
                        {
                            iPageIndexSum = iPageIndexSum + int.Parse(oTypeName.Value.Substring(oTypeName.Value.IndexOf(":")+1));
                        }
                    }

                    if (flag)
                    {
                        break;
                    }
                }
            }

            //this.ViewState.Add("LastPageIndex", iPageIndexSum);
            
            Grid_Answers.PageIndex = iPageIndexSum;

            Grid_Answers.DataSource = oListCandidateAnswerQuestion;
            Grid_Answers.DataBind();

            if (Grid_Answers.PageIndex == 0)
            {
                GridViewRow oGridViewRow = Grid_Answers.BottomPagerRow;

                LinkButton LinkButtonPrev = oGridViewRow.FindControl("LinkButtonPrev") as LinkButton;

                if (LinkButtonPrev != null)
                {
                    LinkButtonPrev.Enabled = false;
                }
            }
            else if (Grid_Answers.PageIndex == Grid_Answers.PageCount - 1)
            {
                GridViewRow oGridViewRow = Grid_Answers.BottomPagerRow;

                LinkButton LinkButtonNext = oGridViewRow.FindControl("LinkButtonNext") as LinkButton;

                if (LinkButtonNext != null)
                {
                    LinkButtonNext.Enabled = false;
                }
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }

    protected void Grid_Answers_PageIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Grid_Answers.PageIndex == 0)
            {
                GridViewRow oGridViewRow = Grid_Answers.BottomPagerRow;

                LinkButton LinkButtonPrev = oGridViewRow.FindControl("LinkButtonPrev") as LinkButton;

                if (LinkButtonPrev != null)
                {
                    LinkButtonPrev.Enabled = false;
                }
            }
            else if (Grid_Answers.PageIndex == Grid_Answers.PageCount - 1)
            {
                GridViewRow oGridViewRow = Grid_Answers.BottomPagerRow;

                LinkButton LinkButtonNext = oGridViewRow.FindControl("LinkButtonNext") as LinkButton;

                if (LinkButtonNext != null)
                {
                    LinkButtonNext.Enabled = false;
                }
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }
}
