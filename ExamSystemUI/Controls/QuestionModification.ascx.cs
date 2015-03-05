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

public partial class Controls_QuestionModification : System.Web.UI.UserControl
{
    Guid gSystemUserID = Guid.Empty;
    String sSystemUserName = String.Empty;
    String sSystemUserPassword = String.Empty;

    bool bIsViewButtonClicked = false;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_error.Text = String.Empty;
        pnl_questions.Visible = false;
        pnl_questions.ScrollBars = ScrollBars.None;
       

        try
        {
            LoadSystemUser();

            if (!IsPostBack)
            {
                LoadCategoryfromSession();
                //LoadQuestionType();
                lbl_Questions.Text = String.Empty;
                pnl_questions.Controls.Remove(Grid_Questions);
                HiddenForStoreSelectedRowIndex.Value = String.Empty;
                //btn_Update.Attributes.Add("onclick", "StoreTxtBoxCheckBoxValue('" + HiddenStoreTxtBoxCheckBoxValue.ClientID + "')");
                btn_Update.Attributes.Add("onclick", "StoreTxtBoxCheckBoxValue(0,'" + HiddenStoreTxtBoxCheckBoxValue.ClientID + "','"+lbl_error.ClientID+"')");
                btn_Delete.Attributes.Add("onclick", "return confirmMsg(this.form,'" + lbl_error.ClientID + "')");
                btn_View.Attributes.Add("onclick", "return ViewQuestion(this.form,'" + lbl_error.ClientID + "')");
            }
            else
            {
               
            }
        }
        catch (Exception oEx)
        { 
            
        }

    }
    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        Result oResult = new Result();
        QuestionBO oQuestionBO = new QuestionBO();

        List<Question> oListQuestion = new List<Question>();

        String sHiddenFiledValue = HiddenFieldForStoreChkBoxIndex.Value;
        String sCheckedRowIndex = String.Empty;

        //HiddenForStoreSelectedRowIndex.Value = String.Empty;
        
        try
        {
            if (!dr_Category.SelectedValue.Equals("[Select One]") && !dr_Type.SelectedValue.Equals("[Select One]") && !lbl_Questions.Text.Equals(String.Empty))
            {
                int iNextStartIndex = 0;
                int Index = 0;
                int i = 0;
                int j = 0;

                bool bAnySelectedQuestion = false;

                //oListQuestion = (List<Question>)Utils.GetSession(SessionManager.csStoreGridView);
                oListQuestion = (List<Question>)this.ViewState[SessionManager.csStoreGridView];

                int[] iarrChecked = new int[oListQuestion.Count];

                for (j = 0; j < iarrChecked.Length; j++)
                {
                    iarrChecked[j] = 0;
                }

                while (iNextStartIndex < sHiddenFiledValue.Length && sHiddenFiledValue.IndexOf(':', iNextStartIndex) >= 0)
                {
                    Index = sHiddenFiledValue.IndexOf(':', iNextStartIndex);

                    sCheckedRowIndex = sHiddenFiledValue.Substring(iNextStartIndex, Index - iNextStartIndex);

                    if (int.TryParse(sCheckedRowIndex, out i))
                    {
                        iarrChecked[Grid_Questions.PageIndex * Grid_Questions.PageSize + int.Parse(sCheckedRowIndex)] = 1; //marked

                        bAnySelectedQuestion = true;
                    }

                    iNextStartIndex = Index + 1;
                }

                if (bAnySelectedQuestion)
                {
                    oResult = oQuestionBO.DeleteQuestionList(oListQuestion, iarrChecked);

                    if (oResult.ResultIsSuccess)
                    {
                        oListQuestion = (List<Question>)oResult.ResultObject;

                        txt_SaveQuestion.Text = String.Empty;
                        txt_SaveDefaultMark.Text = String.Empty;
                        txt_CurrentDefaultMark.Text = String.Empty;
                        txt_CurrentpossibleTime.Text = String.Empty;
                        txt_LastPossibleTime.Text = String.Empty;

                        if (oListQuestion.Count > 0)
                        {
                            lbl_Questions.Text = "Questions";
                            Grid_Questions.DataSource = oListQuestion;
                            Grid_Questions.DataBind();

                            pnl_questions.Visible = true;
                            pnl_questions.ScrollBars = ScrollBars.Auto;
                            pnl_questions.Controls.Add(Grid_Questions);

                            if (oListQuestion.Count < iarrChecked.Length)
                            {
                                lbl_error.ForeColor = Color.Green;
                            }
                            else if (oListQuestion.Count == iarrChecked.Length)
                            {
                                lbl_error.ForeColor = Color.Red;
                            }

                            lbl_error.Text = oResult.ResultMessage;
                        }
                        else
                        {
                            lbl_error.ForeColor = Color.Green;
                            lbl_error.Text = oResult.ResultMessage;
                            
                            lbl_Questions.Text = String.Empty;
                            if (pnl_questions.Controls.Contains(Grid_Questions))
                            {
                                pnl_questions.Controls.Remove(Grid_Questions);
                            }
                        }

                        //Utils.SetSession(SessionManager.csStoreGridView, oListQuestion);
                        this.ViewState.Add(SessionManager.csStoreGridView, oListQuestion);
                    }
                    else
                    {
                        lbl_error.ForeColor = Color.Red;
                        lbl_error.Text = oResult.ResultMessage;
                    }
                }
                else
                {
                    lbl_error.ForeColor = Color.Red;
                    lbl_error.Text = "Please select question.";
                }

                if (oListQuestion.Count > 0)
                {
                    lbl_Questions.Text = "Questions";
                    Grid_Questions.DataSource = oListQuestion;
                    Grid_Questions.DataBind();
                    pnl_questions.Visible = true;
                    pnl_questions.ScrollBars = ScrollBars.Auto;
                    pnl_questions.Controls.Add(Grid_Questions);
                }

            }
            else
            {
                lbl_error.ForeColor = Color.Red;
                lbl_error.Text = "Select category & type first, then delete question.";
            }
        }
        catch (Exception oEx)
        {
            oResult.ResultMessage = "Exception occured during Question Delete.";
        }

    }
  
    private bool IsValidDefaultQuestionMark(String sDefaultQuestionMark)
    {
        float i = 0f;

        if (!float.TryParse(sDefaultQuestionMark, out i))
        {
            return false;
        }
        else
        {
            if (float.Parse(sDefaultQuestionMark) <= 0f)
            {
                return false;
            }
        }

        return true;
    }

    private bool IsValidPossibleAnswerTime(String sPossibleAnswerTime)
    {
        sPossibleAnswerTime = sPossibleAnswerTime.Trim();

        float i = 0f;

        if (!float.TryParse(sPossibleAnswerTime, out i))
        {
            return false;
        }
        else
        {
            if (float.Parse(sPossibleAnswerTime) <= 0f)
            {
                return false;
            }
        }

        return true;
    }

    private bool IsValidSaveCategory(String sCategory)
    {
        if (sCategory.Equals("[Select One]"))
        {
            return false;
        }

        return true;
    }
    private bool IsValidSystemUserID()
    {
        if (gSystemUserID.Equals(Guid.Empty))
        {
            return false;
        }

        return true;
    }
    private bool IsValidTypeName(String sType)
    {
        if (sType.Equals("[Select One]"))
        {
            return false;
        }

        return true;
    }
    private void LoadCategoryfromSession()
    {
        List<Category> oListCategory = new List<Category>();

        oListCategory = (List<Category>)Utils.GetSession(SessionManager.csStoredCategory);

        dr_Category.Items.Clear();
        dr_Category.Items.Add("[Select One]");

        foreach (Category oCategory in oListCategory)
        {
            dr_Category.Items.Add(oCategory.CategoryName);
        }
    }
    private void LoadQuestionType()
    {
        dr_Type.Items.Clear();

        dr_Type.Items.Add("[Select One]");
        dr_Type.Items.Add("Descriptive");
        dr_Type.Items.Add("Objective");
    }
    private void LoadSystemUser()
    {
        SystemUser oSystemUser = new SystemUser();

        oSystemUser = (SystemUser)Utils.GetSession(SessionManager.csLoggedUser);

        gSystemUserID = oSystemUser.SystemUserID;
        sSystemUserName = oSystemUser.SystemUserName;
        sSystemUserPassword = oSystemUser.SystemUserPassword;
    }

   
    protected void dr_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        HiddenForStoreSelectedRowIndex.Value = String.Empty;
        
        try
        {
            if (!dr_Category.SelectedValue.Equals("[Select One]") && !dr_Type.SelectedValue.Equals("[Select One]"))
            {
                List<Category> oListCategory = new List<Category>();

                oListCategory = (List<Category>)Utils.GetSession(SessionManager.csStoredCategory);

                Question oQuestion = new Question();
                Objective oObjective = new Objective();
                QuestionBO oQuestionBO = new QuestionBO();
                Result oResult = new Result();
                List<Question> oListQuestion = new List<Question>();

                oQuestion.QuestionCategory.CategoryID = oListCategory[dr_Category.SelectedIndex - 1].CategoryID;
                oQuestion.QuestionCreator.SystemUserID = gSystemUserID;
                oQuestion.QuestionCreator.SystemUserName = sSystemUserName;

                if (dr_Type.SelectedValue.Equals("Objective"))
                {
                    //For Show List of choice OR Answer is not necessary.
                    oQuestion.QuestionQuestionType.QuestionTypeID=0;
                }
                else if (dr_Type.SelectedValue.Equals("Descriptive"))
                {
                    oQuestion.QuestionQuestionType.QuestionTypeID = 1;
                }

                oResult = oQuestionBO.QuestionListShow(oQuestion);

                if (oResult.ResultIsSuccess)
                {
                    oListQuestion =(List<Question>)oResult.ResultObject;
                    //if objective then the list of choices and answers will be populated for each question

                    //Utils.SetSession(SessionManager.csStoreGridView, oListQuestion);
                    this.ViewState.Add(SessionManager.csStoreGridView, oListQuestion);
                                               
                    if (oListQuestion.Count > 0)
                    {
                        lbl_Questions.Text = "Questions";
                        Grid_Questions.DataSource = oListQuestion;
                        Grid_Questions.DataBind();

                        pnl_questions.Visible = true;
                        pnl_questions.ScrollBars = ScrollBars.Auto;
                        pnl_questions.Controls.Add(Grid_Questions);
                    }
                    else
                    {
                        Label oLblError = new Label();
                        oLblError.Text = "No Question Found.";
                        oLblError.ForeColor = Color.Red;

                        pnl_questions.Visible = true;
                        pnl_questions.Controls.Add(oLblError);

                        lbl_Questions.Text = String.Empty;
                    }

                }
                else
                {
                    lbl_error.ForeColor = Color.Red;
                    lbl_error.Text = oResult.ResultMessage;
                    lbl_Questions.Text = String.Empty;
                    txt_SaveQuestion.Text = String.Empty;
                    txt_SaveDefaultMark.Text = String.Empty;
                    txt_CurrentDefaultMark.Text = String.Empty;

                    if (pnl_questions.Controls.Contains(Grid_Questions))
                    {
                        pnl_questions.Controls.Remove(Grid_Questions);
                    }
                }
            }
            else
            {
                if (dr_Type.SelectedValue.Equals("[Select One]"))
                {
                    clearControlValue();
                }
            }
        }
        catch (Exception oEx)
        {
            lbl_error.ForeColor = Color.Red;
            lbl_error.Text = "Exception occured during Question List Show.";
        }
    }

    private void clearControlValue()
    {
        lbl_Questions.Text = String.Empty;
        txt_SaveQuestion.Text = String.Empty;
        txt_SaveDefaultMark.Text = String.Empty;
        txt_CurrentDefaultMark.Text = String.Empty;
        txt_LastPossibleTime.Text = String.Empty;
        txt_CurrentpossibleTime.Text = String.Empty;

        if (pnl_questions.Controls.Contains(Grid_Questions))
        {
            pnl_questions.Controls.Remove(Grid_Questions);
        }
    }
    
    protected void btn_View_Click(object sender, EventArgs e)
    {
        List<Question> oListQuestion = new List<Question>();
        List<Choice> oListChoice = new List<Choice>();

        String sHiddenFiledValue = HiddenFieldForStoreChkBoxIndex.Value;
        String sCheckedRowIndex = String.Empty;

        //HiddenForStoreSelectedRowIndex.Value = String.Empty;

        txt_CurrentpossibleTime.Text = String.Empty;
        txt_CurrentDefaultMark.Text = String.Empty;

        bIsViewButtonClicked = true;

        try
        {
            if (!dr_Category.SelectedValue.Equals("[Select One]") && !dr_Type.SelectedValue.Equals("[Select One]") && !lbl_Questions.Text.Equals(String.Empty))
            {
                int iNextStartIndex = 0;
                int Index = 0;
                int i = 0;
                int iCountSelectedRows = 0;
                int iSelectedRowIndex = 0;
                int iID = 0;
                int iTotalTxtBoxesInServer = 0;

                //oListQuestion = (List<Question>)Utils.GetSession(SessionManager.csStoreGridView);
                oListQuestion = (List<Question>)this.ViewState[SessionManager.csStoreGridView];

                while (iNextStartIndex < sHiddenFiledValue.Length && sHiddenFiledValue.IndexOf(':', iNextStartIndex) >= 0)
                {
                    Index = sHiddenFiledValue.IndexOf(':', iNextStartIndex);

                    sCheckedRowIndex = sHiddenFiledValue.Substring(iNextStartIndex, Index - iNextStartIndex);

                    if (int.TryParse(sCheckedRowIndex, out i))
                    {
                        iCountSelectedRows++;
                        iSelectedRowIndex = int.Parse(sCheckedRowIndex);
                    }

                    iNextStartIndex = Index + 1;
                }

                if (iCountSelectedRows == 1)
                {
                    txt_SaveQuestion.Text = oListQuestion[Grid_Questions.PageIndex * Grid_Questions.PageSize + iSelectedRowIndex].QuestionText;
                    txt_SaveDefaultMark.Text = oListQuestion[Grid_Questions.PageIndex * Grid_Questions.PageSize + iSelectedRowIndex].QuestionDefaultMark.ToString();
                    txt_LastPossibleTime.Text = oListQuestion[Grid_Questions.PageIndex * Grid_Questions.PageSize + iSelectedRowIndex].QuestionPossibleAnswerTime.ToString();

                    lblRightQlebelShow.Text = oListQuestion[Grid_Questions.PageIndex * Grid_Questions.PageSize + iSelectedRowIndex].QuestionLevel.LevelName;

                    //if (oListQuestion[iSelectedRowIndex].QuestionIsUsed)
                    //{
                    //    txt_CurrentDefaultMark.ReadOnly = true;
                    //    txt_CurrentpossibleTime.ReadOnly = true;
                    //}
                    //else if (!oListQuestion[iSelectedRowIndex].QuestionIsUsed)
                    //{
                    //    txt_CurrentDefaultMark.ReadOnly = false;
                    //    txt_CurrentpossibleTime.ReadOnly = false;
                    //}

                    HiddenForStoreSelectedRowIndex.Value = iSelectedRowIndex.ToString();

                    if (oListQuestion[Grid_Questions.PageIndex * Grid_Questions.PageSize + iSelectedRowIndex].QuestionQuestionType.QuestionTypeID == 0) // if objective then show the choices.
                    {
                        oListChoice = oListQuestion[Grid_Questions.PageIndex * Grid_Questions.PageSize + iSelectedRowIndex].QuestionObjectiveType.ListOfChoices;

                        iTotalTxtBoxesInServer = oListChoice.Count;

                        btn_Update.Attributes.Remove("onclick");
                        btn_Update.Attributes.Add("onclick", "StoreTxtBoxCheckBoxValue(" + iTotalTxtBoxesInServer + ",'" + HiddenStoreTxtBoxCheckBoxValue.ClientID + "','" + lbl_error.ClientID + "')");
                        //btn_Update.Attributes.Add("onclick", "StoreTxtBoxCheckBoxValue(0,'" + HiddenStoreTxtBoxCheckBoxValue.ClientID + "','" + lbl_error.ClientID + "')");

                        foreach (Choice oChoiceinList in oListChoice)
                        {
                            iID++;

                            if (oListQuestion[Grid_Questions.PageIndex * Grid_Questions.PageSize + iSelectedRowIndex].QuestionIsUsed)
                            {
                                pnl_Choices.Controls.Add(new LiteralControl("<input type='text' value='" + oChoiceinList.ChoiceName + "' id='" + iID.ToString() + "' readonly='readonly' onclick=" + '"'.ToString() + "AppendTextBox(" + iTotalTxtBoxesInServer + "," + 1 + "," + iID + ")" + '"'.ToString() + " />"));
                            }
                            else if (!oListQuestion[Grid_Questions.PageIndex * Grid_Questions.PageSize + iSelectedRowIndex].QuestionIsUsed)
                            {
                                pnl_Choices.Controls.Add(new LiteralControl("<input type='text' value='" + oChoiceinList.ChoiceName + "' id='" + iID.ToString() + "' onclick=" + '"'.ToString() + "AppendTextBox(" + iTotalTxtBoxesInServer + "," + 1 + "," + iID + ")" + '"'.ToString() + " />"));
                            }

                            if (oChoiceinList.ChoiceIsValid)
                            {
                                if (oListQuestion[Grid_Questions.PageIndex * Grid_Questions.PageSize + iSelectedRowIndex].QuestionIsUsed)
                                {
                                    pnl_Choices.Controls.Add(new LiteralControl("<input type='checkbox' checked='checked' disabled='disabled' id='CHE" + iID.ToString() + "' />"));
                                }
                                else if (!oListQuestion[Grid_Questions.PageIndex * Grid_Questions.PageSize + iSelectedRowIndex].QuestionIsUsed)
                                {
                                    pnl_Choices.Controls.Add(new LiteralControl("<input type='checkbox' checked='checked' id='CHE" + iID.ToString() + "' />"));
                                }
                            }
                            else
                            {
                                if (oListQuestion[Grid_Questions.PageIndex * Grid_Questions.PageSize + iSelectedRowIndex].QuestionIsUsed)
                                {
                                    pnl_Choices.Controls.Add(new LiteralControl("<input type='checkbox' disabled='disabled' id='CHE" + iID.ToString() + "' />"));
                                }
                                else if (!oListQuestion[Grid_Questions.PageIndex * Grid_Questions.PageSize + iSelectedRowIndex].QuestionIsUsed)
                                {
                                    pnl_Choices.Controls.Add(new LiteralControl("<input type='checkbox' id='CHE" + iID.ToString() + "' />"));
                                }
                            }

                            pnl_Choices.Controls.Add(new LiteralControl("<span id='ISV" + iID.ToString() + "'>Is Valid </span>"));

                            //if the question is used in question setup, then delete button will not be shown
                            //disabled=disabled
                            if (oListQuestion[Grid_Questions.PageIndex * Grid_Questions.PageSize + iSelectedRowIndex].QuestionIsUsed)
                            {
                                pnl_Choices.Controls.Add(new LiteralControl("<input type='button' value='Delete' id='BTN" + iID.ToString() + "' disabled='disabled' onclick=" + '"'.ToString() + "RemoveRow(this.id)" + '"'.ToString() + " />"));
                            }
                            else if (!oListQuestion[Grid_Questions.PageIndex * Grid_Questions.PageSize + iSelectedRowIndex].QuestionIsUsed)
                            {
                                pnl_Choices.Controls.Add(new LiteralControl("<input type='button' value='Delete' id='BTN" + iID.ToString() + "' onclick=" + '"'.ToString() + "RemoveRow(this.id)" + '"'.ToString() + " />"));
                            }

                            pnl_Choices.Controls.Add(new LiteralControl("<br id='BRI" + iID.ToString() + "'/>"));
                        }
                    }
                }
                else
                {
                    lbl_error.ForeColor = Color.Red;
                    lbl_error.Text = "Select Only one Question to View.";
                }

                if (oListQuestion.Count > 0)
                {
                    lbl_Questions.Text = "Questions";
                    Grid_Questions.DataSource = oListQuestion;
                    Grid_Questions.DataBind();
                    pnl_questions.Visible = true;
                    pnl_questions.ScrollBars = ScrollBars.Auto;
                    pnl_questions.Controls.Add(Grid_Questions);
                }
            }
            else
            {
                lbl_error.ForeColor = Color.Red;
                lbl_error.Text = "Select category & type first, then view question.";
            }

            //lbl_Questions.Text = String.Empty;
            //if (pnl_questions.Controls.Contains(Grid_Questions))
            //{
            //    pnl_questions.Controls.Remove(Grid_Questions);
            //}
        }
        catch (Exception oEx)
        {
            lbl_error.ForeColor = Color.Red;
            lbl_error.Text = "Exception occured during Question View.";
        }
    }
    private bool IsValidChoices(String sChoices)
    {
        int i = 0;

        sChoices = sChoices.Trim();

        //if (sChoices.Length < 1)
        //{
        //    return false;
        //}

        //for (i = 0; i < sChoices.Length; i++)
        //{
        //    if (sChoices[i].ToString().Equals("'"))
        //    {
        //        return false;
        //    }
        //}

        return true;
    }
    protected void btn_Update_Click(object sender, EventArgs e)
    {
        Result oResult = new Result();
        Question oQuestion = new Question();
        QuestionBO oQuestionBO = new QuestionBO();
        Objective oObjective = new Objective();

        List<Category> oListCategory = new List<Category>();
        List<Question> oListQuestion = new List<Question>();
        List<Choice> oListOfChoices = new List<Choice>();

        String sHiddenFieldValue = String.Empty;
        String sChoiceWithCheck = String.Empty;
        String sTxtValue = String.Empty;
        String sChkValue = String.Empty;

        bool bflag = false;

        int iNextStartIndex = 0;
        int Index = 0;
        int iSelectedRowIndex = -1000;
        int iOut=0;

        float fQuestionDefaultMark = 0f;
        float fQuestionPossibleTime = 0f;

        try
        {

            //oListQuestion = (List<Question>)Utils.GetSession(SessionManager.csStoreGridView);
            oListQuestion = (List<Question>)this.ViewState[SessionManager.csStoreGridView];
            
            if (IsValidSaveCategory(dr_Category.SelectedValue) && IsValidSystemUserID() && IsValidTypeName(dr_Type.SelectedValue) && !lbl_Questions.Text.Equals(String.Empty))
            {
                //oQuestion.QuestionCreator.SystemUserID = gSystemUserID;

                if (IsValidDefaultQuestionMark(txt_CurrentDefaultMark.Text))
                {
                    fQuestionDefaultMark = float.Parse(txt_CurrentDefaultMark.Text);
                }
                else if (IsValidDefaultQuestionMark(txt_SaveDefaultMark.Text))
                {
                    fQuestionDefaultMark = float.Parse(txt_SaveDefaultMark.Text);
                }

                if (IsValidPossibleAnswerTime(txt_CurrentpossibleTime.Text))
                {
                    fQuestionPossibleTime = float.Parse(txt_CurrentpossibleTime.Text);
                }
                else if (IsValidPossibleAnswerTime(txt_LastPossibleTime.Text))
                {
                    fQuestionPossibleTime = float.Parse(txt_LastPossibleTime.Text);
                }

                //oListCategory = (List<Category>)Utils.GetSession(SessionManager.csStoredCategory);

                //oQuestion.QuestionCategory.CategoryID = oListCategory[dr_Category.SelectedIndex - 1].CategoryID;

                
                if (int.TryParse(HiddenForStoreSelectedRowIndex.Value, out iOut))
                {
                    iSelectedRowIndex = int.Parse(HiddenForStoreSelectedRowIndex.Value);
                }
               

                if (iSelectedRowIndex >= 0 && fQuestionDefaultMark > 0f && fQuestionPossibleTime>0f)
                {
                    oQuestion = oListQuestion[Grid_Questions.PageIndex * Grid_Questions.PageSize + iSelectedRowIndex];

                    oQuestion.QuestionDefaultMark = fQuestionDefaultMark;
                    oQuestion.QuestionPossibleAnswerTime = fQuestionPossibleTime;

                    if (dr_Type.SelectedValue.Equals("Objective"))
                    {
                        oQuestion.QuestionQuestionType.QuestionTypeID=0;
                        //oQuestion.QuestionObjectiveType.TypeName = SelectQuestionSave.Value;
                        //populate the choices
                        //get the choices from, runtime created textboxes
                        sHiddenFieldValue = HiddenStoreTxtBoxCheckBoxValue.Value.Replace("'",String.Empty);

                        if (IsValidChoices(sHiddenFieldValue))
                        {
                            while (iNextStartIndex < sHiddenFieldValue.Length && sHiddenFieldValue.IndexOf('@', iNextStartIndex) >= 0)
                            {
                                Index = sHiddenFieldValue.IndexOf('@', iNextStartIndex);

                                sChoiceWithCheck = sHiddenFieldValue.Substring(iNextStartIndex, Index - iNextStartIndex);

                                sTxtValue = sChoiceWithCheck.Substring(0, sChoiceWithCheck.IndexOf(':', 0));

                                sChkValue = sChoiceWithCheck.Substring(sChoiceWithCheck.IndexOf(':', 0) + 1);

                                Choice oChoice = new Choice();

                                oChoice.ChoiceName = sTxtValue;
                                oChoice.ChoiceIsValid = Convert.ToBoolean(sChkValue);

                                if (!oChoice.ChoiceName.Equals(String.Empty) && oChoice.ChoiceName.Length<=1000)
                                {
                                    oListOfChoices.Add(oChoice);

                                    if (oChoice.ChoiceIsValid)
                                    {
                                        bflag = true;
                                    }
                                }

                                iNextStartIndex = Index + 1;
                            }

                            oObjective.ListOfChoices = oListOfChoices;

                            oQuestion.QuestionObjectiveType = oObjective;

                            //bflag = true;
                        }
                        else
                        {
                            lbl_error.ForeColor = Color.Red;
                            lbl_error.Text = "Enter Choices in corrected format.";
                        }
                    }
                    else if (dr_Type.SelectedValue.Equals("Descriptive"))
                    {
                        
                        //oQuestion.QuestionDescriptiveType.TypeName = SelectQuestionSave.Value;
                        oQuestion.QuestionQuestionType.QuestionTypeID = 1;

                        bflag = true;
                    }

                    if (bflag)
                    {
                        oResult = oQuestionBO.UpdateQuestion(oQuestion);

                        if (oResult.ResultIsSuccess)
                        {
                            oListQuestion.RemoveAt(Grid_Questions.PageIndex * Grid_Questions.PageSize + iSelectedRowIndex);
                            oListQuestion.Insert(Grid_Questions.PageIndex * Grid_Questions.PageSize + iSelectedRowIndex, (Question)oResult.ResultObject);

                            //Utils.SetSession(SessionManager.csStoreGridView, oListQuestion);
                            this.ViewState.Add(SessionManager.csStoreGridView, oListQuestion);

                            lbl_error.ForeColor = Color.Green;
                            lbl_error.Text = oResult.ResultMessage;

                            txt_SaveQuestion.Text = String.Empty;
                            txt_SaveDefaultMark.Text = String.Empty;
                            txt_CurrentDefaultMark.Text = String.Empty;
                            txt_LastPossibleTime.Text = String.Empty;
                            txt_CurrentpossibleTime.Text = String.Empty;

                        }
                        else
                        {
                            lbl_error.ForeColor = Color.Red;
                            lbl_error.Text = oResult.ResultMessage;
                        }
                    }
                    else
                    {
                        lbl_error.ForeColor = Color.Red;
                        lbl_error.Text = "Must View a Question. Or Check Any one";
                    }
                }
                else
                {
                    lbl_error.ForeColor = Color.Red;
                    lbl_error.Text = "Select a question and click view button.";
                }

                if (oListQuestion != null && oListQuestion.Count > 0)
                {
                    lbl_Questions.Text = "Questions";
                    Grid_Questions.DataSource = oListQuestion;
                    Grid_Questions.DataBind();
                    pnl_questions.Visible = true;
                    pnl_questions.ScrollBars = ScrollBars.Auto;
                    pnl_questions.Controls.Add(Grid_Questions);
                }
            }
            else
            {
                lbl_error.ForeColor = Color.Red;
                lbl_error.Text = "Select category & type first. Then select a question and click view button.";
            }

            HiddenForStoreSelectedRowIndex.Value = String.Empty;

            
            
        }
        catch (Exception oEx)
        {
            lbl_error.ForeColor = Color.Red;
            lbl_error.Text = "Exception occured during Question Update.";
        }
    }
    protected void Grid_Questions_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            HiddenForStoreSelectedRowIndex.Value = String.Empty;
            
            List<Question> oListQuestion = new List<Question>();

            oListQuestion = (List<Question>)this.ViewState[SessionManager.csStoreGridView];

            Grid_Questions.PageIndex = e.NewPageIndex;

            Grid_Questions.DataSource = oListQuestion;
            Grid_Questions.DataBind();
            pnl_questions.Visible = true;
            pnl_questions.ScrollBars = ScrollBars.Auto;
            pnl_questions.Controls.Add(Grid_Questions);
            
        }
        catch (Exception oEx)
        { 
        
        }
    }

    protected override void OnPreRender(EventArgs e)
    {

        try
        {
            String sHiddenFiledValue = HiddenFieldForStoreChkBoxIndex.Value;
            String sCheckedRowIndex = String.Empty;

            int iNextStartIndex = 0;
            int Index = 0;
            int iSelectedRowIndex = 0;
            int i = 0;

            if (bIsViewButtonClicked)
            {
                while (iNextStartIndex < sHiddenFiledValue.Length && sHiddenFiledValue.IndexOf(':', iNextStartIndex) >= 0)
                {
                    Index = sHiddenFiledValue.IndexOf(':', iNextStartIndex);

                    sCheckedRowIndex = sHiddenFiledValue.Substring(iNextStartIndex, Index - iNextStartIndex);

                    if (int.TryParse(sCheckedRowIndex, out i))
                    {
                        iSelectedRowIndex = int.Parse(sCheckedRowIndex);

                        CheckBox oCheckBox = Grid_Questions.Rows[iSelectedRowIndex].FindControl("deleteRec") as CheckBox;
                        if (oCheckBox != null)
                        {
                            oCheckBox.Checked = true;
                        }
                    }

                    iNextStartIndex = Index + 1;
                }

            }

            HiddenFieldForStoreChkBoxIndex.Value = String.Empty;
        }
        catch (Exception oEx)
        {

        }

        base.OnPreRender(e);
    }
    protected void dr_Category_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadQuestionType();

        lbl_Questions.Text = String.Empty;
    }
}
