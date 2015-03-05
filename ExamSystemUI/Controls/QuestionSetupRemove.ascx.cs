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

public partial class Controls_QuestionSetupRemove : System.Web.UI.UserControl
{
    SystemUser oSystemUser = new SystemUser();

    Object oObject = null;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_error.Text = String.Empty;
        lbl_ShowExamConstraint.Text = String.Empty;

        pnl_questions.Visible = false;
        pnl_questions.ScrollBars = ScrollBars.None;

        lblQuestionLevel.Visible = false;
        drdnSelectQuestionLevel.Visible = false;

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

                pnl_questions.Controls.Remove(Grid_Questions);

                btn_Remove.Attributes.Add("onclick", "return RemoveQuestion(this.form,'"+lbl_error.ClientID+"')");
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
            if (oExam.ExamConstraint == 0)
            {
                lbl_ShowExamConstraint.Text = "Full Marking.";
            }
            else if (oExam.ExamConstraint == 1)
            {
                lbl_ShowExamConstraint.Text = "Partial Marking.";
            }
            else if (oExam.ExamConstraint == 2)
            {
                lbl_ShowExamConstraint.Text = "Negative Marking.";
            }
            else if (oExam.ExamConstraint == 3)
            {
                lbl_ShowExamConstraint.Text = "Partial Negative Marking.";
            }

            lbl_ExamName.Text = oExam.ExamName;
        }
    }
    private void LoadSystemUser()
    {
        oSystemUser = (SystemUser)Utils.GetSession(SessionManager.csLoggedUser);
    }
    private void LoadSelectionDropdownList()
    {
        //rdo_QuestionSelection.Items.Clear();
        //rdo_QuestionSelection.Items.Add("Random");
        //rdo_QuestionSelection.Items.Add("From List");

        dr_ListOrRandom.Items.Clear();
        dr_ListOrRandom.Items.Add("[Select One]");
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
    private bool IsBeforeExamStarted(Exam oExam)
    {
        if (DateTime.Now >= oExam.ExamDateWithStartingTime)
        {
            return false;
        }

        return true;
    }
   
    protected void btn_Remove_Click(object sender, EventArgs e)
    {
        String sHiddenFiledValue = HiddenFieldForStoreChkBoxIndex.Value;
        String sCheckedRowIndex = String.Empty;

        Exam oExam = new Exam();
        Result oResult = new Result();
        QuestionBO oQuestionBO = new QuestionBO();

        List<QuestionSetup> oListQuestionSetup = new List<QuestionSetup>();
        //List<Question> oListQuestion = new List<Question>();

        try
        {
            if (oObject != null)
            {
                oExam = (Exam)oObject;

                if (IsBeforeExamStarted(oExam))
                {
                    //if (!dr_Category.SelectedValue.Equals("[Select One]") && !dr_Type.SelectedValue.Equals("[Select One]") && !lbl_Questions.Text.Equals(String.Empty) && dr_ListOrRandom.SelectedValue.Equals("From List"))

                    if (dr_ListOrRandom.SelectedValue.Equals("By Question Level"))
                    {
                        lblQuestionLevel.Visible = true;
                        drdnSelectQuestionLevel.Visible = true;
                    }

                    if (!dr_Category.SelectedValue.Equals("[Select One]") && !dr_Type.SelectedValue.Equals("[Select One]") && !lbl_Questions.Text.Equals(String.Empty) && (dr_ListOrRandom.SelectedValue.Equals("From List") || (dr_ListOrRandom.SelectedValue.Equals("By Question Level") && !drdnSelectQuestionLevel.SelectedValue.Equals("[Select One]"))))
                    {
                        int iNextStartIndex = 0;
                        int Index = 0;
                        int i = 0;
                        int j = 0;
                        Boolean flag = false;

                       

                        //oListQuestionSetup = (List<QuestionSetup>)Utils.GetSession(SessionManager.csStoreGridView);
                        oListQuestionSetup = (List<QuestionSetup>)this.ViewState[SessionManager.csStoreGridView];

                        int[] iarrChecked = new int[oListQuestionSetup.Count];

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
                                flag = true;
                            }

                            iNextStartIndex = Index + 1;
                        }

                        if (flag)
                        {
                            oResult = oQuestionBO.QuestionSetupRemove(oListQuestionSetup, iarrChecked);

                            if (oResult.ResultIsSuccess)
                            {
                                oListQuestionSetup = (List<QuestionSetup>)oResult.ResultObject;

                                lbl_error.ForeColor = Color.Green;
                                lbl_error.Text = oResult.ResultMessage;
                                
                                if (oListQuestionSetup.Count > 0)
                                {
                                    lbl_Questions.Text = "Questions";

                                    //foreach (QuestionSetup oQuestionSetupInList in oListQuestionSetup)
                                    //{
                                    //    oListQuestion.Add(oQuestionSetupInList.QuestionSetupQuestion);
                                    //}



                                    Grid_Questions.DataSource = oListQuestionSetup;
                                    Grid_Questions.DataBind();

                                    pnl_questions.Visible = true;
                                    pnl_questions.ScrollBars = ScrollBars.Auto;
                                    pnl_questions.Controls.Add(Grid_Questions);

                                    
                                }
                                else
                                {
                                    lbl_Questions.Text = String.Empty;
                                    if (pnl_questions.Controls.Contains(Grid_Questions))
                                    {
                                        pnl_questions.Controls.Remove(Grid_Questions);
                                    }
                                }

                                //LoadCategoryfromSession();
                                //LoadQuestionType();
                                //LoadSelectionDropdownList();

                                //Utils.SetSession(SessionManager.csStoreGridView, oListQuestionSetup);
                                this.ViewState.Add(SessionManager.csStoreGridView, oListQuestionSetup);
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
                            lbl_error.Text = "Select a Question Please.";
                            lbl_Questions.Text = String.Empty;
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
                    lbl_error.Text = "Remove before exam started.";
                }
            }
            else
            {
                lbl_error.ForeColor = Color.Red;
                lbl_error.Text = "Please Select an Exam.";
            }

            if (oListQuestionSetup.Count > 0)
            {
                lbl_Questions.Text = "Questions";

                Grid_Questions.DataSource = oListQuestionSetup;
                Grid_Questions.DataBind();

                pnl_questions.Visible = true;
                pnl_questions.ScrollBars = ScrollBars.Auto;
                pnl_questions.Controls.Add(Grid_Questions);
            }

            //LoadSelectionDropdownList();
        }
        catch(Exception oEx)
        {
            lbl_error.ForeColor = Color.Red;
            lbl_error.Text = "Exception occured during Question Setup remove.";
        }
    }
    protected void dr_ListOrRandom_SelectedIndexChanged(object sender, EventArgs e)
    {
        Exam oExam = new Exam();

        try
        {
            if (dr_ListOrRandom.SelectedValue.Equals("[Select One]"))
            {
                lbl_Questions.Text = String.Empty;
            }
            else if (dr_ListOrRandom.SelectedValue.Equals("From List"))
            {
                try
                {
                    if (oObject != null)
                    {
                        oExam = (Exam)oObject;

                        if (!dr_Category.SelectedValue.Equals("[Select One]") && !dr_Type.SelectedValue.Equals("[Select One]"))
                        {
                            List<Category> oListCategory = new List<Category>();

                            oListCategory = (List<Category>)Utils.GetSession(SessionManager.csStoredCategory);

                            Question oQuestion = new Question();
                            QuestionSetup oQuestionSetup = new QuestionSetup();
                            QuestionBO oQuestionBO = new QuestionBO();
                            Result oResult = new Result();
                            List<QuestionSetup> oListQuestionSetup = new List<QuestionSetup>();
                            //List<Question> oListQuestion = new List<Question>();

                            oQuestion.QuestionCategory = oListCategory[dr_Category.SelectedIndex - 1];

                            if (dr_Type.SelectedValue.Equals("Objective"))
                            {
                                oQuestion.QuestionQuestionType.QuestionTypeID = 0;
                            }
                            else if (dr_Type.SelectedValue.Equals("Descriptive"))
                            {
                                oQuestion.QuestionQuestionType.QuestionTypeID = 1;
                            }

                            oQuestionSetup.QuestionSetupExam = oExam;
                            oQuestionSetup.QuestionSetupQuestion = oQuestion;
                            oQuestionSetup.QuestionSetupSystemUser = oSystemUser;

                            oResult = oQuestionBO.QuestionSetupListShow(oQuestionSetup);

                            if (oResult.ResultIsSuccess)
                            {
                                oListQuestionSetup = (List<QuestionSetup>)oResult.ResultObject;
                                //if objective then the list of choices and answers will be populated for each question

                                //Utils.SetSession(SessionManager.csStoreGridView, oListQuestionSetup);
                                this.ViewState.Add(SessionManager.csStoreGridView, oListQuestionSetup);

                                if (oListQuestionSetup.Count > 0)
                                {
                                    lbl_Questions.Text = "Questions";

                                    //foreach (QuestionSetup oQuestionSetupInList in oListQuestionSetup)
                                    //{
                                    //    oListQuestion.Add(oQuestionSetupInList.QuestionSetupQuestion);
                                    //}

                                    Grid_Questions.DataSource = oListQuestionSetup;
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

                                if (pnl_questions.Controls.Contains(Grid_Questions))
                                {
                                    pnl_questions.Controls.Remove(Grid_Questions);
                                }
                            }
                        }
                        else
                        {
                            //if (dr_Type.SelectedValue.Equals("[Select One]"))
                            //{
                            //    lbl_Questions.Text = String.Empty;

                            //    if (pnl_questions.Controls.Contains(Grid_Questions))
                            //    {
                            //        pnl_questions.Controls.Remove(Grid_Questions);
                            //    }
                            //}
                            lbl_Questions.Text = String.Empty;

                            lbl_error.ForeColor = Color.Red;
                            lbl_error.Text = "Please Select Category,Type & SelectionType.";
                        }
                        //rdo_QuestionSelection.Items[1].Selected = false;
                    }
                    else
                    {
                        lbl_error.ForeColor = Color.Red;
                        lbl_error.Text = "Please Select an Exam.";
                    }

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
                try
                {
                    if (oObject != null)
                    {
                        oExam = (Exam)oObject;

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
                catch (Exception oEx3)
                { 
                
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

            if(oQuestionSetup!=null)
            {
                Label lblQuestion = e.Row.FindControl("lblQuestion") as Label;
                lblQuestion.Text = Server.HtmlEncode(oQuestionSetup.QuestionSetupQuestion.QuestionText);

                Label lblSetupMark = e.Row.FindControl("lblSetupMark") as Label;
                lblSetupMark.Text = oQuestionSetup.QuestionSetupMark.ToString();
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
            if (dr_ListOrRandom.SelectedValue.Equals("By Question Level"))
            {
                lblQuestionLevel.Visible = true;
                drdnSelectQuestionLevel.Visible = true;
            }
            
            
            List<QuestionSetup> oListQuestionSetupForShow = new List<QuestionSetup>();

            oListQuestionSetupForShow = (List<QuestionSetup>)this.ViewState[SessionManager.csStoreGridView];

            Grid_Questions.PageIndex = e.NewPageIndex;

            Grid_Questions.DataSource = oListQuestionSetupForShow;
            Grid_Questions.DataBind();
            pnl_questions.Visible = true;
            pnl_questions.ScrollBars = ScrollBars.Auto;
            pnl_questions.Controls.Add(Grid_Questions);
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
                QuestionSetup oQuestionSetup = new QuestionSetup();
                QuestionBO oQuestionBO = new QuestionBO();
                Result oResult = new Result();
                List<QuestionSetup> oListQuestionSetup = new List<QuestionSetup>();

                oQuestion.QuestionCategory.CategoryID = oListCategory[dr_Category.SelectedIndex - 1].CategoryID;
                oQuestion.QuestionCreator = oSystemUser;
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

                    oQuestionSetup.QuestionSetupExam = (Exam)oObject;
                    oQuestionSetup.QuestionSetupQuestion = oQuestion;

                    oResult = oQuestionBO.QuestionSetupListShowByQuestionLevel(oQuestionSetup);

                    if (oResult.ResultIsSuccess)
                    {
                        oListQuestionSetup = (List<QuestionSetup>)oResult.ResultObject;
                        //if objective then the list of choices and answers will be populated for each question

                        //Utils.SetSession(SessionManager.csStoreGridView, oListQuestionSetup);
                        this.ViewState.Add(SessionManager.csStoreGridView, oListQuestionSetup);

                        if (oListQuestionSetup.Count > 0)
                        {
                            lbl_Questions.Text = "Questions";
                            Grid_Questions.DataSource = oListQuestionSetup;
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
                        if (pnl_questions.Controls.Contains(Grid_Questions))
                        {
                            pnl_questions.Controls.Remove(Grid_Questions);
                        }
                    }
                }
            }
            else
            {
                lbl_Questions.Text = String.Empty;

                //pnl_questions.Visible = true;
                if (pnl_questions.Controls.Contains(Grid_Questions))
                {
                    pnl_questions.Controls.Remove(Grid_Questions);
                }
            }
        }
        catch (Exception oEx)
        {

        }
    }

    //protected override void OnPreRender(EventArgs e)
    //{
    //    try
    //    {
    //        String sHiddenFiledValue = HiddenFieldForStoreChkBoxIndex.Value;
    //        String sCheckedRowIndex = String.Empty;

    //        int iNextStartIndex = 0;
    //        int Index = 0;
    //        int iSelectedRowIndex = 0;
    //        int i = 0;

    //        //if (Grid_Questions.Visible)
    //        //{
    //            while (iNextStartIndex < sHiddenFiledValue.Length && sHiddenFiledValue.IndexOf(':', iNextStartIndex) >= 0)
    //            {
    //                Index = sHiddenFiledValue.IndexOf(':', iNextStartIndex);

    //                sCheckedRowIndex = sHiddenFiledValue.Substring(iNextStartIndex, Index - iNextStartIndex);

    //                if (int.TryParse(sCheckedRowIndex, out i))
    //                {
    //                    iSelectedRowIndex = int.Parse(sCheckedRowIndex);

    //                    CheckBox oCheckBox = Grid_Questions.Rows[iSelectedRowIndex].FindControl("deleteRec") as CheckBox;
    //                    if (oCheckBox != null)
    //                    {
    //                        oCheckBox.Checked = true;
    //                    }
    //                }

    //                iNextStartIndex = Index + 1;
    //            }

    //        //}

    //        HiddenFieldForStoreChkBoxIndex.Value = String.Empty;
    //    }
    //    catch (Exception oEx)
    //    {

    //    }

    //    base.OnPreRender(e);
    //}
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
