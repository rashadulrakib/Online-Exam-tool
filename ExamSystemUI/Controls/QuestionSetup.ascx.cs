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

public partial class Controls_QuestionSetup : System.Web.UI.UserControl
{
    Guid gSystemUserID = Guid.Empty;
    String sSystemUserName = String.Empty;
    String sSystemUserPassword = String.Empty;

    SystemUser oSystemUser = new SystemUser();

    Object oObject = null;

    bool bIsViewButtonClicked = false;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_error.Text = String.Empty;
        txt_SaveQuestion.Text = String.Empty;
        lbl_ShowExamConstraint.Text = String.Empty;
        lbl_ExamName.Text = String.Empty;
        
        pnl_questions.Visible = false;
        pnl_questions.ScrollBars = ScrollBars.None;
        Grid_Questions.Visible = false;
        
        
        lbl_RandomQuestions.Visible = false;
        txt_RandomQuestions.Visible = false;
        lblQuestionLevel.Visible = false;
        drdnSelectQuestionLevel.Visible = false;
        lblRightQlebelShow.Visible = false;
        lblRightQuestionLevelShow.Visible = false;
        lblRandomOutOf.Visible = false;
        txtRandomOutOf.Visible = false;
        btn_View.Visible = true;

        try
        {
            LoadSystemUser();

            LoadSelectedExam();
            
            if (!IsPostBack)
            {
                //LoadSelectionDropdownList();
                LoadCategoryfromSession();
                //LoadQuestionType();
                lbl_Questions.Text = String.Empty;
                //pnl_questions.Controls.Remove(Grid_Questions);

                btn_Setup.Attributes.Add("onclick", "return SetupQuestion(this.form,'"+lbl_error.ClientID+"')");
                btn_View.Attributes.Add("onclick", "return ViewQuestion(this.form,'" + lbl_error.ClientID + "')");


            }
        }
        catch (Exception oEx)
        { 
        
        }
    }

    private void LoadSelectedExam()
    {
        oObject = Utils.GetSession(SessionManager.csSelectedExam);

        Exam oExam = new Exam();

        oExam = (Exam)oObject;

        if (oObject != null)
        {
            if(oExam.ExamConstraint==0)
            {
                lbl_ShowExamConstraint.Text = "Full Marking.";
            }
            else if(oExam.ExamConstraint==1)
            {
                lbl_ShowExamConstraint.Text = "Partial Marking.";
            }
            else if(oExam.ExamConstraint==2)
            {
                lbl_ShowExamConstraint.Text = "Negative Marking.";
            }
            else if (oExam.ExamConstraint == 3)
            {
                lbl_ShowExamConstraint.Text = "Partial Negative Marking.";
            }

            lbl_ExamName.Text = oExam.ExamName;
            lbl_ExamTotalMark.Text = oExam.ExamTotalMarks.ToString();
            float fduration = oExam.ExamDurationinHour * 60f;
            lbl_ExamDuration.Text = fduration.ToString();
            
        }
   
    }
    private void LoadSystemUser()
    {
        oSystemUser = (SystemUser)Utils.GetSession(SessionManager.csLoggedUser);

        gSystemUserID = oSystemUser.SystemUserID;
        sSystemUserName = oSystemUser.SystemUserName;
        sSystemUserPassword = oSystemUser.SystemUserPassword;
    }
    private void LoadSelectionDropdownList()
    {
        //rdo_QuestionSelection.Items.Clear();
        //rdo_QuestionSelection.Items.Add("Random");
        //rdo_QuestionSelection.Items.Add("From List");

        dr_ListOrRandom.Items.Clear();
        dr_ListOrRandom.Items.Add("[Select One]");
        dr_ListOrRandom.Items.Add("Random");
        dr_ListOrRandom.Items.Add("From List");
        dr_ListOrRandom.Items.Add("By Question Level");
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
   
    protected void btn_Setup_Click(object sender, EventArgs e)
    {
        String sHiddenFiledValue = HiddenFieldForStoreChkBoxIndex.Value;
        String sHiddenFiledMark = HiddenFieldForStoreTxtBoxMark.Value;
        String sCheckedRowIndex = String.Empty;
        String sNewRowMark = String.Empty;
                
        Exam oExam = new Exam();
        Result oResult = new Result();
        QuestionBO oQuestionBO = new QuestionBO();

        List<QuestionSetup> oListQuestionSetupForShow = new List<QuestionSetup>();
        List<QuestionSetup> oListQuestionSetup = new List<QuestionSetup>();

        float fTotalMarks = 0f;
        float fTotalTime = 0f;

        bool IsRandomSelection = false;
        //bool IsSetupSuccessByLevelOrFromList = false;
        bool IsBylevelSelection = false;

        try
        {
            if (oObject!= null)
            {
                oExam = (Exam)oObject;

                //oExam.ExamConstraint="some value from the UI";
                //update EX_Exam in the DAO by the oExam.ExamConstraint.
                //oExam.ExamConstraint is defaultly =0;

                if(IsBeforeExamStarted(oExam))
                {
                    if (dr_ListOrRandom.SelectedValue.Equals("By Question Level"))
                    {
                        IsBylevelSelection = true;
                    }
                    
                    if (!dr_Category.SelectedValue.Equals("[Select One]") && !dr_Type.SelectedValue.Equals("[Select One]") && !lbl_Questions.Text.Equals(String.Empty) && (dr_ListOrRandom.SelectedValue.Equals("From List") || (dr_ListOrRandom.SelectedValue.Equals("By Question Level") && !drdnSelectQuestionLevel.SelectedValue.Equals("[Select One]"))))
                    {
                        int iNextStartIndex = 0;
                        int Index = 0;
                        int i = 0;
                        int j = 0;
                        float f = 0f;
                        Boolean flag = false;

                        //oListQuestionSetupForShow = (List<QuestionSetup>)Utils.GetSession(SessionManager.csStoreGridView);
                        oListQuestionSetupForShow = (List<QuestionSetup>)this.ViewState[SessionManager.csStoreGridView];

                        int[] iarrChecked = new int[oListQuestionSetupForShow.Count];
                        float[] iarrNewMark = new float[oListQuestionSetupForShow.Count];

                        for (j = 0; j < iarrChecked.Length; j++)
                        {
                            iarrChecked[j] = 0;
                            iarrNewMark[j] = oListQuestionSetupForShow[j].QuestionSetupMark;
                        }

                        while (iNextStartIndex < sHiddenFiledValue.Length && sHiddenFiledValue.IndexOf(':', iNextStartIndex) >= 0)
                        {
                            Index = sHiddenFiledValue.IndexOf(':', iNextStartIndex);

                            sCheckedRowIndex = sHiddenFiledValue.Substring(iNextStartIndex, Index - iNextStartIndex);

                            if (int.TryParse(sCheckedRowIndex, out i))
                            {
                                iarrChecked[Grid_Questions.PageIndex * Grid_Questions.PageSize + int.Parse(sCheckedRowIndex)] = 1; //marked
                                flag = true;
                            }

                            iNextStartIndex = Index + 1;
                        }

                        if (flag)
                        {
                            iNextStartIndex = 0;
                            Index = 0;


                            //get the marks from the text boxes in the GridView
                            i = 0;
                            while (iNextStartIndex < sHiddenFiledMark.Length && sHiddenFiledMark.IndexOf(':', iNextStartIndex) >= 0)
                            {
                                Index = sHiddenFiledMark.IndexOf(':', iNextStartIndex);

                                sNewRowMark = sHiddenFiledMark.Substring(iNextStartIndex, Index - iNextStartIndex);

                                if (float.TryParse(sNewRowMark, out f))
                                {
                                    iarrNewMark[Grid_Questions.PageIndex * Grid_Questions.PageSize + i] = float.Parse(sNewRowMark); //marked
                                }
                                //else
                                //{
                                //    iarrNewMark[Grid_Questions.PageIndex * Grid_Questions.PageSize + i] = oListQuestionSetupForShow[Grid_Questions.PageIndex * Grid_Questions.PageSize + i].QuestionSetupMark;
                                //}

                                i++;
                                iNextStartIndex = Index + 1;
                            }


                            oResult = oQuestionBO.GetTotalLastSetupQuestionMark(oExam);

                            if (oResult.ResultIsSuccess)
                            {
                                fTotalMarks = (float)oResult.ResultObject;
                                
                                //populate the List of QuestionSetupObject

                                i = 0;
                                foreach (QuestionSetup oQuestionSetupInList in oListQuestionSetupForShow)
                                {
                                    QuestionSetup oQuestionSetup = new QuestionSetup();

                                    oQuestionSetup.QuestionSetupExam = oExam;
                                    oQuestionSetup.QuestionSetupGenerationTime = DateTime.Now;
                                    oQuestionSetup.QuestionSetupSystemUser = oSystemUser;
                                    oQuestionSetup.QuestionSetupMark = iarrNewMark[i];

                                    if (!oQuestionSetupInList.QuestionSetupQuestion.QuestionIsUsed && iarrChecked[i]==1)
                                    {
                                        fTotalMarks = fTotalMarks + iarrNewMark[i];
                                    }

                                    oQuestionSetup.QuestionSetupQuestion = oQuestionSetupInList.QuestionSetupQuestion;

                                    oListQuestionSetup.Add(oQuestionSetup);

                                    i++;
                                }

                                if (fTotalMarks <= (float)oExam.ExamTotalMarks)
                                {
                                    oResult = oQuestionBO.GetTotalLastSetupQuestionTime(oExam);

                                    if (oResult.ResultIsSuccess)
                                    {
                                        //sum the fTotalTime by the Grid's possible time for each question

                                        fTotalTime = (float)oResult.ResultObject;

                                        i=0;
                                        
                                        foreach (QuestionSetup oQuestionSetupInList in oListQuestionSetupForShow)
                                        {
                                            if (!oQuestionSetupInList.QuestionSetupQuestion.QuestionIsUsed && iarrChecked[i] == 1)
                                            {
                                                fTotalTime = fTotalTime + oQuestionSetupInList.QuestionSetupQuestion.QuestionPossibleAnswerTime;
                                            }

                                            i++;
                                        }

                                        if (fTotalTime <= (float)(oExam.ExamDurationinHour * 60f))
                                        {
                                            //now call the BO to insert question 
                                            oResult = oQuestionBO.QuestionSetup(oListQuestionSetup, iarrChecked);

                                            if (oResult.ResultIsSuccess)
                                            {
                                                lbl_error.ForeColor = Color.Green;
                                                lbl_error.Text = oResult.ResultMessage;

                                                i = 0;

                                                foreach (QuestionSetup oQuestionSetupInList in oListQuestionSetup)
                                                {
                                                    oListQuestionSetupForShow[i].QuestionSetupMark = oQuestionSetupInList.QuestionSetupMark;

                                                    i++;
                                                }

                                                //Utils.SetSession(SessionManager.csStoreGridView, oListQuestionSetupForShow);
                                                this.ViewState.Add(SessionManager.csStoreGridView, oListQuestionSetupForShow);

                                                //LoadCategoryfromSession();
                                                //LoadQuestionType();
                                                //LoadSelectionDropdownList();

                                                //IsSetupSuccessByLevelOrFromList = true;
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
                                            lbl_error.Text = "Total Time OverFlow.";  
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
                                    lbl_error.ForeColor = Color.Red;
                                    lbl_error.Text = "Total Marks OverFlow.";  
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
                            lbl_error.ForeColor = Color.Red;
                            lbl_error.Text = "Select a Question please.";
                        }
                    }
                    else if (!dr_Category.SelectedValue.Equals("[Select One]") && !dr_Type.SelectedValue.Equals("[Select One]") && lbl_Questions.Text.Equals(String.Empty) && dr_ListOrRandom.SelectedValue.Equals("Random"))
                    {

                        IsRandomSelection = true;
                        
                        oListQuestionSetupForShow = (List<QuestionSetup>)this.ViewState[SessionManager.csStoreGridView];
                        
                        int j = 0;
                        int iTotalRandomQuestions = 0;
                        int iFullfillTotalRandomnumber = 0;
                        int iNextRandomIndex = -1000;

                        int[] iarrChecked = new int[oListQuestionSetupForShow.Count];
                        int[] iarrRandomIndexChecked = new int[oListQuestionSetupForShow.Count];

                        for (j = 0; j < iarrChecked.Length; j++)
                        {
                            iarrChecked[j] = 0;
                            iarrRandomIndexChecked[j] = 0;

                        }

                        if (IsValidPositiveInteger(txt_RandomQuestions.Text))
                        {
                            if (int.Parse(txt_RandomQuestions.Text) <= oListQuestionSetupForShow.Count)
                            {
                                iTotalRandomQuestions = int.Parse(txt_RandomQuestions.Text);

                                while (iFullfillTotalRandomnumber < iTotalRandomQuestions)
                                {
                                    iNextRandomIndex = new Random().Next(0, oListQuestionSetupForShow.Count);

                                    if (iarrRandomIndexChecked[iNextRandomIndex] == 0)
                                    {
                                        iFullfillTotalRandomnumber = iFullfillTotalRandomnumber + 1;
                                        iarrRandomIndexChecked[iNextRandomIndex] = 1;
                                        iarrChecked[iNextRandomIndex] = 1;
                                    }
                                }

                                if (iFullfillTotalRandomnumber == iTotalRandomQuestions)
                                {
                                    foreach (QuestionSetup oQuestionSetupInList in oListQuestionSetupForShow)
                                    {
                                        oQuestionSetupInList.QuestionSetupExam = oExam;
                                        oQuestionSetupInList.QuestionSetupGenerationTime = DateTime.Now;
                                        oQuestionSetupInList.QuestionSetupSystemUser = oSystemUser;
                                    }
                                    
                                    
                                    oResult = oQuestionBO.QuestionSetup(oListQuestionSetupForShow, iarrChecked);

                                    if (oResult.ResultIsSuccess)
                                    {
                                        lbl_error.ForeColor = Color.Green;
                                        lbl_error.Text = oResult.ResultMessage;

                                        //Utils.SetSession(SessionManager.csStoreGridView, oListQuestionSetupForShow);
                                        this.ViewState.Add(SessionManager.csStoreGridView, oListQuestionSetupForShow);

                                        //LoadCategoryfromSession();
                                        //LoadQuestionType();
                                        //LoadSelectionDropdownList();
                                    }
                                    else
                                    {
                                        lbl_error.ForeColor = Color.Red;
                                        lbl_error.Text = oResult.ResultMessage;
                                    }
                                }

                                lbl_RandomQuestions.Visible = true;
                                txt_RandomQuestions.Visible = true;

                                lblRandomOutOf.Visible = true;
                                txtRandomOutOf.Visible = true;

                                btn_View.Visible = false;

                            }
                            else
                            {
                                lbl_error.ForeColor = Color.Red;
                                lbl_error.Text = "Random question number must less than Total question Number";

                                lbl_RandomQuestions.Visible = true;
                                txt_RandomQuestions.Visible = true;

                                lblRandomOutOf.Visible = true;
                                txtRandomOutOf.Visible = true;

                                btn_View.Visible = false;
                            }
                        }
                        else
                        {
                            lbl_error.ForeColor = Color.Red;
                            lbl_error.Text = "Random question number must greater than zero ";

                            lbl_RandomQuestions.Visible = true;
                            txt_RandomQuestions.Visible = true;

                            lblRandomOutOf.Visible = true;
                            txtRandomOutOf.Visible = true;

                            btn_View.Visible = false;
                        }
                    }
                    else
                    {
                        lbl_error.ForeColor = Color.Red;
                        lbl_error.Text = "Please Select Category,Type & SelectionType."; 
                        
                        if (dr_ListOrRandom.SelectedValue.Equals("By Question Level") && drdnSelectQuestionLevel.SelectedValue.Equals("[Select One]"))
                        {
                            lbl_error.Text = "Please Select Category,Type,SelectionType & Question Level."; 
                        }
                        
                        
                    }
                }
                else
                {
                    lbl_error.ForeColor = Color.Red;
                    lbl_error.Text = "Can not setup Question After Exam Started.";
                }
            }
            else
            {
                lbl_error.ForeColor = Color.Red;
                lbl_error.Text = "Select an Exam please.";
            }

            //rdo_QuestionSelection.Items[0].Selected = false;
            //rdo_QuestionSelection.Items[1].Selected = false;

            if (!IsRandomSelection)
            {
                if (IsBylevelSelection)
                {
                    lblQuestionLevel.Visible = true;
                    drdnSelectQuestionLevel.Visible = true;
                }
                
                if (oListQuestionSetupForShow.Count > 0)
                {
                    lbl_Questions.Text = "Questions";
                    
                    Grid_Questions.Visible = true;

                    //List<Question> oListQuestionForGridViewBind = new List<Question>();

                    //foreach (QuestionSetup oQuestionSetupInList in oListQuestionSetupForShow)
                    //{
                    //    oListQuestionForGridViewBind.Add(oQuestionSetupInList.QuestionSetupQuestion);
                    //}

                    Grid_Questions.DataSource = oListQuestionSetupForShow;

                    //Grid_Questions.DataSource = oListQuestionForGridViewBind;
                    Grid_Questions.DataBind();
                    
                    pnl_questions.Visible = true;
                    pnl_questions.ScrollBars = ScrollBars.Auto;
                    //pnl_questions.Controls.Add(Grid_Questions);
                }
                else
                {
                    lbl_Questions.Text = String.Empty;
                }
            }
            

            //LoadSelectionDropdownList();


            //lbl_Questions.Text = String.Empty;

            //if (pnl_questions.Controls.Contains(Grid_Questions))
            //{
            //    pnl_questions.Controls.Remove(Grid_Questions);
            //}

        }
        catch (Exception oEx)
        { 
        
        }
    }

    private bool IsValidPositiveInteger(String sInteger)
    {
        int i = 0;

        if (!int.TryParse(sInteger, out i))
        {
            return false;
        }
        else
        {
            if (int.Parse(sInteger) <= 0)
            {
                return false;
            }
        }

        return true;
    }

    private bool IsBeforeExamStarted(Exam oExam)
    {
        if (DateTime.Now >= oExam.ExamDateWithStartingTime)
        {
            return false;
        }

        return true;
    }
   
    protected void btn_View_Click(object sender, EventArgs e)
    {
        List<QuestionSetup> oListQuestionSetupForShow = new List<QuestionSetup>();
        List<Choice> oListChoice = new List<Choice>();

        String sHiddenFiledValue = HiddenFieldForStoreChkBoxIndex.Value;
        String sCheckedRowIndex = String.Empty;

        bIsViewButtonClicked = true;

        try
        {
            if (!dr_Category.SelectedValue.Equals("[Select One]") && !dr_Type.SelectedValue.Equals("[Select One]") && !lbl_Questions.Text.Equals(String.Empty) && (dr_ListOrRandom.SelectedValue.Equals("From List") || (dr_ListOrRandom.SelectedValue.Equals("By Question Level") && !drdnSelectQuestionLevel.SelectedValue.Equals("[Select One]"))))
            {
                if (dr_ListOrRandom.SelectedValue.Equals("By Question Level"))
                {
                    lblQuestionLevel.Visible = true;
                    drdnSelectQuestionLevel.Visible = true;
                }
                
                int iNextStartIndex = 0;
                int Index = 0;
                int i = 0;
                int iCountSelectedRows = 0;
                int iSelectedRowIndex = 0;
                int iID = 0;

                //oListQuestionSetupForShow = (List<QuestionSetup>)Utils.GetSession(SessionManager.csStoreGridView);
                oListQuestionSetupForShow = (List<QuestionSetup>)this.ViewState[SessionManager.csStoreGridView];


                //if (oListQuestionSetupForShow.Count > 0)
                //{
                //    pnl_questions.Visible = true;
                //    Grid_Questions.Visible = true;
                //}


                while (iNextStartIndex < sHiddenFiledValue.Length && sHiddenFiledValue.IndexOf(':', iNextStartIndex) >= 0)
                {
                    Index = sHiddenFiledValue.IndexOf(':', iNextStartIndex);

                    sCheckedRowIndex = sHiddenFiledValue.Substring(iNextStartIndex, Index - iNextStartIndex);

                    if (int.TryParse(sCheckedRowIndex, out i))
                    {
                        iCountSelectedRows++;
                        iSelectedRowIndex = int.Parse(sCheckedRowIndex);

                        //CheckBox oCheckBox = Grid_Questions.Rows[iSelectedRowIndex].FindControl("deleteRec") as CheckBox;
                        //if (oCheckBox != null)
                        //{
                        //    oCheckBox.Checked = true;
                        //}
                    }

                    iNextStartIndex = Index + 1;
                }

                if (iCountSelectedRows == 1)
                {
                    txt_SaveQuestion.Text = oListQuestionSetupForShow[Grid_Questions.PageIndex * Grid_Questions.PageSize + iSelectedRowIndex].QuestionSetupQuestion.QuestionText;

                    lblRightQlebelShow.Visible = true;
                    lblRightQuestionLevelShow.Visible = true;

                    lblRightQlebelShow.Text = oListQuestionSetupForShow[Grid_Questions.PageIndex * Grid_Questions.PageSize + iSelectedRowIndex].QuestionSetupQuestion.QuestionLevel.LevelName;

                    //if (!oListQuestion[iSelectedRowIndex].QuestionIsDescriptive) // if objective then show the choices.
                    if (oListQuestionSetupForShow[Grid_Questions.PageIndex * Grid_Questions.PageSize + iSelectedRowIndex].QuestionSetupQuestion.QuestionQuestionType.QuestionTypeID == 0)
                    {
                        oListChoice = oListQuestionSetupForShow[Grid_Questions.PageIndex * Grid_Questions.PageSize + iSelectedRowIndex].QuestionSetupQuestion.QuestionObjectiveType.ListOfChoices;

                        foreach (Choice oChoiceinList in oListChoice)
                        {
                            iID++;

                           
                            pnl_Choices.Controls.Add(new LiteralControl("<input type='text' value='" + oChoiceinList.ChoiceName + "' id='" + iID.ToString() + "' readonly='readonly' />"));
                           
                            
                            if (oChoiceinList.ChoiceIsValid)
                            {
                                pnl_Choices.Controls.Add(new LiteralControl("<input type='checkbox' checked='checked' disabled='disabled' id='CHE" + iID.ToString() + "' />"));
                            }
                            else
                            {
                                pnl_Choices.Controls.Add(new LiteralControl("<input type='checkbox' disabled='disabled' id='CHE" + iID.ToString() + "' />"));
                            }

                            pnl_Choices.Controls.Add(new LiteralControl("<span id='ISV" + iID.ToString() + "'> Is Valid </span>"));

                            
                            pnl_Choices.Controls.Add(new LiteralControl("<input type='button' value='Delete' id='BTN" + iID.ToString() + "' disabled='disabled' />"));
                            

                            pnl_Choices.Controls.Add(new LiteralControl("<br id='BRI" + iID.ToString() + "'/>"));
                        }
                    }
                }
                else
                {
                    lbl_error.ForeColor = Color.Red;
                    lbl_error.Text = "Select Only one Question to View.";
                }
            }

            if (oListQuestionSetupForShow.Count > 0)
            {
                lbl_Questions.Text = "Questions";

                pnl_questions.Visible = true;
                pnl_questions.ScrollBars = ScrollBars.Auto;

                Grid_Questions.Visible = true;

                //List<Question> oListQuestionForGridViewBind = new List<Question>();

                //foreach (QuestionSetup oQuestionSetupInList in oListQuestionSetupForShow)
                //{
                //    oListQuestionForGridViewBind.Add(oQuestionSetupInList.QuestionSetupQuestion);
                //}

                Grid_Questions.DataSource = oListQuestionSetupForShow;

                //Grid_Questions.DataSource = oListQuestionForGridViewBind;
                Grid_Questions.DataBind();
                

                //pnl_questions.Controls.Add(Grid_Questions);
            }
            else
            {
                lbl_Questions.Text = String.Empty;
            }

            //lbl_Questions.Text = String.Empty;
            //if (pnl_questions.Controls.Contains(Grid_Questions))
            //{
            //    pnl_questions.Controls.Remove(Grid_Questions);
            //}

            //LoadSelectionDropdownList();
        }
        catch (Exception oEx)
        {
            lbl_error.ForeColor = Color.Red;
            lbl_error.Text = "Exception occured during Question View.";
        }
    }
    protected void dr_ListOrRandom_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (dr_ListOrRandom.SelectedValue.Equals("[Select One]"))
            {
                lbl_Questions.Text = String.Empty;

                Grid_Questions.Visible = false;
                pnl_questions.Visible = false;
                //if (pnl_questions.Controls.Contains(Grid_Questions))
                //{
                //    pnl_questions.Controls.Remove(Grid_Questions);
                //}
            }
            else if (dr_ListOrRandom.SelectedValue.Equals("Random"))
            {
                if (!dr_Category.SelectedValue.Equals("[Select One]") && !dr_Type.SelectedValue.Equals("[Select One]"))
                {
                    List<Category> oListCategory = new List<Category>();

                    oListCategory = (List<Category>)Utils.GetSession(SessionManager.csStoredCategory);

                    Question oQuestion = new Question();
                    QuestionBO oQuestionBO = new QuestionBO();
                    Result oResult = new Result();
                    List<QuestionSetup> oListQuestionSetup = new List<QuestionSetup>();

                    oQuestion.QuestionCategory.CategoryID = oListCategory[dr_Category.SelectedIndex - 1].CategoryID;
                    oQuestion.QuestionCreator.SystemUserID = gSystemUserID;
                    oQuestion.QuestionCreator.SystemUserName = sSystemUserName;

                    if (dr_Type.SelectedValue.Equals("Objective"))
                    {
                        oQuestion.QuestionQuestionType.QuestionTypeID = 0;
                    }
                    else if (dr_Type.SelectedValue.Equals("Descriptive"))
                    {
                        oQuestion.QuestionQuestionType.QuestionTypeID = 1;
                    }

                    if (oObject != null)
                    {
                        oResult = oQuestionBO.QuestionListShowForSetup(oQuestion, (Exam)oObject);

                        if (oResult.ResultIsSuccess)
                        {
                            oListQuestionSetup = (List<QuestionSetup>)oResult.ResultObject;
                            //if objective then the list of choices and answers will be populated for each question

                            //Utils.SetSession(SessionManager.csStoreGridView, oListQuestionSetup);
                            this.ViewState.Add(SessionManager.csStoreGridView, oListQuestionSetup);

                            if (oListQuestionSetup.Count > 0)
                            {
                                lbl_Questions.Text = String.Empty;


                                Grid_Questions.Visible = false;
                                pnl_questions.Visible = false;

                                //if (pnl_questions.Controls.Contains(Grid_Questions))
                                //{
                                //    pnl_questions.Controls.Remove(Grid_Questions);
                                //}
                                
                                
                                //Grid_Questions.DataSource = oListQuestionSetup;
                                //Grid_Questions.DataBind();
                                //pnl_questions.Visible = true;
                                //pnl_questions.ScrollBars = ScrollBars.Auto;
                                //pnl_questions.Controls.Add(Grid_Questions);

                                lbl_RandomQuestions.Visible = true;
                                txt_RandomQuestions.Visible = true;

                                lblRandomOutOf.Visible = true;
                                txtRandomOutOf.Visible = true;

                                txtRandomOutOf.Text = oListQuestionSetup.Count.ToString();

                                btn_View.Visible = false;
                            }
                            else
                            {
                                Label oLblError = new Label();
                                oLblError.Text = "No Question Found.";
                                oLblError.ForeColor = Color.Red;

                                ////pnl_questions.Visible = true;
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
                            //pnl_questions.Visible = true;

                            Grid_Questions.Visible = false;
                            pnl_questions.Visible = false;

                            //if (pnl_questions.Controls.Contains(Grid_Questions))
                            //{
                            //    pnl_questions.Controls.Remove(Grid_Questions);
                            //}
                        }
                    }

                }
                else
                {
                    lbl_Questions.Text = String.Empty;

                    //pnl_questions.Visible = true;

                    Grid_Questions.Visible = false;
                    pnl_questions.Visible = false;

                    //if (pnl_questions.Controls.Contains(Grid_Questions))
                    //{
                    //    pnl_questions.Controls.Remove(Grid_Questions);
                    //}
                }
            }
            else if (dr_ListOrRandom.SelectedValue.Equals("From List"))
            {
                try
                {
                    if (!dr_Category.SelectedValue.Equals("[Select One]") && !dr_Type.SelectedValue.Equals("[Select One]"))
                    {
                        List<Category> oListCategory = new List<Category>();

                        oListCategory = (List<Category>)Utils.GetSession(SessionManager.csStoredCategory);

                        Question oQuestion = new Question();
                        QuestionBO oQuestionBO = new QuestionBO();
                        Result oResult = new Result();
                        List<QuestionSetup> oListQuestionSetup = new List<QuestionSetup>();

                        oQuestion.QuestionCategory.CategoryID = oListCategory[dr_Category.SelectedIndex - 1].CategoryID;
                        oQuestion.QuestionCreator.SystemUserID = gSystemUserID;
                        oQuestion.QuestionCreator.SystemUserName = sSystemUserName;

                        if (dr_Type.SelectedValue.Equals("Objective"))
                        {
                            oQuestion.QuestionQuestionType.QuestionTypeID = 0;
                        }
                        else if (dr_Type.SelectedValue.Equals("Descriptive"))
                        {
                            oQuestion.QuestionQuestionType.QuestionTypeID = 1;
                        }

                        if (oObject != null)
                        {
                            oResult = oQuestionBO.QuestionListShowForSetup(oQuestion,(Exam)oObject);

                            if (oResult.ResultIsSuccess)
                            {
                                oListQuestionSetup = (List<QuestionSetup>)oResult.ResultObject;
                                //if objective then the list of choices and answers will be populated for each question

                                //Utils.SetSession(SessionManager.csStoreGridView, oListQuestionSetup);
                                this.ViewState.Add(SessionManager.csStoreGridView, oListQuestionSetup);

                                if (oListQuestionSetup.Count > 0)
                                {
                                    lbl_Questions.Text = "Questions";

                                    Grid_Questions.Visible = true;


                                    //List<Question> oListQuestionForGridViewBind = new List<Question>();

                                    //foreach (QuestionSetup oQuestionSetupInList in oListQuestionSetup)
                                    //{
                                    //    oListQuestionForGridViewBind.Add(oQuestionSetupInList.QuestionSetupQuestion);
                                    //}


                                    Grid_Questions.DataSource = oListQuestionSetup;

                                    //Grid_Questions.DataSource = oListQuestionForGridViewBind;
                                    Grid_Questions.DataBind();
                                    
                                    pnl_questions.Visible = true;
                                    pnl_questions.ScrollBars = ScrollBars.Auto;
                                    //pnl_questions.Controls.Add(Grid_Questions);
                                }
                                else
                                {
                                    Label oLblError = new Label();
                                    oLblError.Text = "No Question Found.";
                                    oLblError.ForeColor = Color.Red;

                                    ////pnl_questions.Visible = true;
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


                                Grid_Questions.Visible = false;
                                pnl_questions.Visible = false;


                                //if (pnl_questions.Controls.Contains(Grid_Questions))
                                //{
                                //    pnl_questions.Controls.Remove(Grid_Questions);
                                //}
                            }
                        }
                        
                    }
                    else
                    {
                        lbl_Questions.Text = String.Empty;

                        Grid_Questions.Visible = false;
                        pnl_questions.Visible = false;

                        //pnl_questions.Visible = true;
                        //if (pnl_questions.Controls.Contains(Grid_Questions))
                        //{
                        //    pnl_questions.Controls.Remove(Grid_Questions);
                        //}
                    }
                    //rdo_QuestionSelection.Items[1].Selected = false;

                    //LoadSelectionDropdownList();
                }
                catch (Exception oEx1)
                {
                    lbl_error.ForeColor = Color.Red;
                    lbl_error.Text = "Exception occured during Question List Show.";
                }

            }
            else if (dr_ListOrRandom.SelectedValue.Equals("By Question Level"))
            {
                if (!dr_Category.SelectedValue.Equals("[Select One]") && !dr_Type.SelectedValue.Equals("[Select One]"))
                {
                    lblQuestionLevel.Visible = true;
                    drdnSelectQuestionLevel.Visible = true;

                    lbl_Questions.Text = String.Empty;

                    LoadLevelsFromDatabase();
                }
                else
                {
                    lbl_Questions.Text = String.Empty;

                    pnl_questions.Visible = false;
                    Grid_Questions.Visible = false;
                }
            }
            

        }
        catch (Exception oEx2)
        {

        }
    }

    private void LoadLevelsFromDatabase()
    {
        try
        {
            Result oResult = new Result();
            LevelBO oLevelBO = new LevelBO();

            List<Level> oListLevel = new List<Level>();

            oResult = oLevelBO.LoadAllLevels();

            if (oResult.ResultIsSuccess)
            {
                oListLevel = (List<Level>)oResult.ResultObject;

                drdnSelectQuestionLevel.Items.Clear();
                drdnSelectQuestionLevel.Items.Add(new ListItem("[Select One]", "[Select One]"));

                foreach (Level oLevelInList in oListLevel)
                {
                    drdnSelectQuestionLevel.Items.Add(new ListItem(oLevelInList.LevelName, oLevelInList.LevelID.ToString()));
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

    protected void Grid_Questions_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            QuestionSetup oQuestionSetup = e.Row.DataItem as QuestionSetup;

            if (oQuestionSetup!= null)
            {
                //if (oQuestion.QuestionIsUsed)
                //{
                //    //txtSetupQuestionMark
                //    TextBox txtSetupQuestionMark = e.Row.FindControl("txtSetupQuestionMark") as TextBox;
                //    txtSetupQuestionMark.Enabled = false;
                //}
                Label lblQuestion = e.Row.FindControl("lblQuestion") as Label;
                lblQuestion.Text = Server.HtmlEncode(oQuestionSetup.QuestionSetupQuestion.QuestionText);

                Label lblOrginalMark = e.Row.FindControl("lblOrginalMark") as Label;
                lblOrginalMark.Text = oQuestionSetup.QuestionSetupQuestion.QuestionDefaultMark.ToString();

                //Label lblDefaultQuestionMark = e.Row.FindControl("lblDefaultQuestionMark") as Label;
                //lblDefaultQuestionMark.Text = oQuestionSetup.QuestionSetupMark.ToString();

                TextBox txtSetupQuestionMark = e.Row.FindControl("txtSetupQuestionMark") as TextBox;
                txtSetupQuestionMark.Text = oQuestionSetup.QuestionSetupMark.ToString();

                Label lblPossibleTime = e.Row.FindControl("lblPossibleTime") as Label;
                lblPossibleTime.Text = oQuestionSetup.QuestionSetupQuestion.QuestionPossibleAnswerTime.ToString();
            }
        }
        catch (Exception oEx)
        {

        }
    }
    protected void Grid_Questions_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        try
        {
            //HiddenForStoreSelectedRowIndex.Value = String.Empty;

            HiddenFieldForStoreChkBoxIndex.Value = String.Empty;
            
            if (dr_ListOrRandom.SelectedValue.Equals("By Question Level"))
            {
                lblQuestionLevel.Visible = true;
                drdnSelectQuestionLevel.Visible = true;
            }
            

            List<QuestionSetup> oListQuestionSetupForShow = new List<QuestionSetup>();

            oListQuestionSetupForShow = (List<QuestionSetup>)this.ViewState[SessionManager.csStoreGridView];

            Grid_Questions.Visible = true;

            Grid_Questions.PageIndex = e.NewPageIndex;

            
            
            //List<Question> oListQuestionForGridViewBind = new List<Question>();

            //foreach (QuestionSetup oQuestionSetupInList in oListQuestionSetupForShow)
            //{
            //    oListQuestionForGridViewBind.Add(oQuestionSetupInList.QuestionSetupQuestion);
            //}



            Grid_Questions.DataSource = oListQuestionSetupForShow;

            //Grid_Questions.DataSource = oListQuestionForGridViewBind;
            Grid_Questions.DataBind();
            pnl_questions.Visible = true;
            pnl_questions.ScrollBars = ScrollBars.Auto;
            //pnl_questions.Controls.Add(Grid_Questions);
        }
        catch (Exception oEx)
        { 
        
        }
    }
    protected void drdnSelectQuestionLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (!dr_Category.SelectedValue.Equals("[Select One]") && !dr_Type.SelectedValue.Equals("[Select One]") && dr_ListOrRandom.SelectedValue.Equals("By Question Level") && !drdnSelectQuestionLevel.SelectedValue.Equals("[Select One]"))
            {
                lblQuestionLevel.Visible = true;
                drdnSelectQuestionLevel.Visible = true;
                
                List<Category> oListCategory = new List<Category>();

                oListCategory = (List<Category>)Utils.GetSession(SessionManager.csStoredCategory);

                Question oQuestion = new Question();
                QuestionBO oQuestionBO = new QuestionBO();
                Result oResult = new Result();
                List<QuestionSetup> oListQuestionSetup = new List<QuestionSetup>();

                oQuestion.QuestionCategory.CategoryID = oListCategory[dr_Category.SelectedIndex - 1].CategoryID;
                oQuestion.QuestionCreator.SystemUserID = gSystemUserID;
                oQuestion.QuestionCreator.SystemUserName = sSystemUserName;
                oQuestion.QuestionLevel.LevelID = new Guid(drdnSelectQuestionLevel.SelectedValue);

                if (dr_Type.SelectedValue.Equals("Objective"))
                {
                    oQuestion.QuestionQuestionType.QuestionTypeID = 0;
                }
                else if (dr_Type.SelectedValue.Equals("Descriptive"))
                {
                    oQuestion.QuestionQuestionType.QuestionTypeID = 1;
                }

                if (oObject != null)
                {
                    oResult = oQuestionBO.QuestionListShowForSetupByQuestionLevel(oQuestion, (Exam)oObject);

                    if (oResult.ResultIsSuccess)
                    {
                        oListQuestionSetup = (List<QuestionSetup>)oResult.ResultObject;
                        //if objective then the list of choices and answers will be populated for each question

                        //Utils.SetSession(SessionManager.csStoreGridView, oListQuestionSetup);
                        this.ViewState.Add(SessionManager.csStoreGridView, oListQuestionSetup);

                        if (oListQuestionSetup.Count > 0)
                        {
                            lbl_Questions.Text = "Questions";

                            Grid_Questions.Visible = true;

                            //List<Question> oListQuestionForGridViewBind = new List<Question>();

                            //foreach (QuestionSetup oQuestionSetupInList in oListQuestionSetup)
                            //{
                            //    oListQuestionForGridViewBind.Add(oQuestionSetupInList.QuestionSetupQuestion);
                            //}

                            Grid_Questions.DataSource = oListQuestionSetup;

                            //Grid_Questions.DataSource = oListQuestionForGridViewBind;
                            Grid_Questions.DataBind();
                            pnl_questions.Visible = true;
                            pnl_questions.ScrollBars = ScrollBars.Auto;
                            //pnl_questions.Controls.Add(Grid_Questions);
                        }
                        else
                        {
                            Label oLblError = new Label();
                            oLblError.Text = "No Question Found.";
                            oLblError.ForeColor = Color.Red;

                            ////pnl_questions.Visible = true;
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
                        //pnl_questions.Visible = true;

                        Grid_Questions.Visible = false;
                        pnl_questions.Visible = false;

                        //if (pnl_questions.Controls.Contains(Grid_Questions))
                        //{
                        //    pnl_questions.Controls.Remove(Grid_Questions);
                        //}
                    }
                }
            }
            else
            {
                lbl_Questions.Text = String.Empty;

                //pnl_questions.Visible = true;
                //if (pnl_questions.Controls.Contains(Grid_Questions))
                //{
                //    pnl_questions.Controls.Remove(Grid_Questions);
                //}

                Grid_Questions.Visible = false;
                pnl_questions.Visible = false;
            }
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

            //if (Grid_Questions.Visible)
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
        lbl_Questions.Text = String.Empty;
        
        LoadQuestionType();
    }
    protected void dr_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbl_Questions.Text = String.Empty;
        
        LoadSelectionDropdownList();
    }
}
