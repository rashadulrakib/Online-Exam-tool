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

public partial class Controls_QuestionEntry : System.Web.UI.UserControl
{
    Guid gSystemUserID = Guid.Empty;
    String sSystemUserName = String.Empty;
    String sSystemUserPassword = String.Empty;
            
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_error.Text = String.Empty;
        
        try
        {
            LoadSystemUser();

            if (!IsPostBack)
            {
                SelectQuestionSave.Attributes.Add("onchange", "SelecObjOrDes('" + SelectQuestionSave.ClientID + "','" + lbl_error.ClientID + "')");
                btn_Save.Attributes.Add("onclick", "StoreTxtBoxCheckBoxValue('" + HiddenStoreTxtBoxCheckBoxValue.ClientID + "','" + lbl_error.ClientID + "','" + SelectQuestionSave.ClientID + "')");
                //fupQuestiondiagram.Attributes.Add("onchange", "ShowValidQuestionImage('" + fupQuestiondiagram.ClientID + "','" + lbl_error.ClientID + "')");
                //tblMain.Attributes.Add("onload", "Initialization('"+imgQuestionDiagram.ClientID+"')");
               
                LoadCategoryfromSession();

                LoadLevelsFromDatabase();
            }

            //imgQuestionDiagram.Visible = false;
        }
        catch (Exception oEx)
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

                drDn_QuestionLevel.Items.Clear();
                drDn_QuestionLevel.Items.Add(new ListItem("[Select One]","[Select One]"));

                foreach (Level oLevelInList in oListLevel)
                {
                    drDn_QuestionLevel.Items.Add(new ListItem(oLevelInList.LevelName,oLevelInList.LevelID.ToString()));
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
  
    private void LoadCategoryfromSession()
    {
        List<Category> oListCategory = new List<Category>();
        
        oListCategory = (List<Category>)Utils.GetSession(SessionManager.csStoredCategory);

        dr_SaveCategory.Items.Clear();
        dr_SaveCategory.Items.Add("[Select One]");

        foreach (Category oCategory in oListCategory)
        {
            dr_SaveCategory.Items.Add(oCategory.CategoryName);
        }
    }
    private void LoadSystemUser()
    {
        SystemUser oSystemUser = new SystemUser();

        oSystemUser = (SystemUser)Utils.GetSession(SessionManager.csLoggedUser);

        gSystemUserID = oSystemUser.SystemUserID;
        sSystemUserName = oSystemUser.SystemUserName;
        sSystemUserPassword = oSystemUser.SystemUserPassword;
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        Result oResult = new Result();
        Question oQuestion = new Question();
        QuestionBO oQuestionBO = new QuestionBO();
        Objective oObjective = new Objective();
        
        List<Category> oListCategory = new List<Category>();
        List<Choice> oListOfChoices = new List<Choice>();

        String sHiddenFieldValue = String.Empty;
        String sChoiceWithCheck = String.Empty;
        String sTxtValue = String.Empty;
        String sChkValue = String.Empty;

        bool bflag = false;

        int iNextStartIndex=0;
        int Index = 0;

        try
        {
            if (IsValidSaveCategory(dr_SaveCategory.SelectedValue) && IsValidQuestionText(txt_SaveQuestion.Text) && IsValidPossibleAnswerTime(txt_PossibleAnswerTime.Text) && IsValidSystemUserID() && IsValidDefaultQuestionMark(txt_SaveDefaultMark.Text) && IsValidTypeName(SelectQuestionSave.Value) && IsValidQuestionLevel(drDn_QuestionLevel.SelectedValue))
            {
                oQuestion.QuestionText = txt_SaveQuestion.Text.Replace("'",String.Empty);
                oQuestion.QuestionCreator.SystemUserID = gSystemUserID;
                oQuestion.QuestionDefaultMark = float.Parse(txt_SaveDefaultMark.Text);
                oQuestion.QuestionIsUsed = false;
                oQuestion.QuestionPossibleAnswerTime = float.Parse(txt_PossibleAnswerTime.Text);
                oQuestion.QuestionLevel.LevelID = new Guid(drDn_QuestionLevel.SelectedValue);

                oListCategory = (List<Category>)Utils.GetSession(SessionManager.csStoredCategory);

                oQuestion.QuestionCategory.CategoryID = oListCategory[dr_SaveCategory.SelectedIndex - 1].CategoryID;
               
                if (SelectQuestionSave.Value.Equals("Objective"))
                {
                    //oQuestion.QuestionObjectiveType.TypeName = SelectQuestionSave.Value;
                    //populate the choices
                    //get the choices from, runtime created textboxes

                    oQuestion.QuestionQuestionType.QuestionTypeID = 0;

                    sHiddenFieldValue = HiddenStoreTxtBoxCheckBoxValue.Value.Replace("'", String.Empty);

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
                    }
                    else
                    {
                        lbl_error.ForeColor = Color.Red;
                        lbl_error.Text = "Enter Choices in corrected format.";
                    }
                }
                else if (SelectQuestionSave.Value.Equals("Descriptive"))
                {
                    oQuestion.QuestionQuestionType.QuestionTypeID = 1;

                    bflag = true;
                }

                if (bflag)
                {
                    oResult = oQuestionBO.QuestionEntry(oQuestion);

                    if (oResult.ResultIsSuccess)
                    {
                        lbl_error.ForeColor = Color.Green;
                        lbl_error.Text = oResult.ResultMessage;

                        clearControlValue();
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
                    lbl_error.Text = "Check All entered value.";
                }
                
            }
            else
            {
                lbl_error.ForeColor = Color.Red;
                lbl_error.Text = "Enter all value in corrected format.";
            }
        }
        catch (Exception oEx)
        {
            lbl_error.ForeColor = Color.Red;
            lbl_error.Text = "Exception occured during Question Entry.";
        }
    }

    private bool IsValidQuestionLevel(String sQuestionLevel)
    {
        if (sQuestionLevel.Equals("[Select One]"))
        {
            return false;
        }

        return true;
    }

    private void clearControlValue()
    {
        LoadSaveType();
        LoadCategoryfromSession();
        LoadLevelsFromDatabase();

        txt_PossibleAnswerTime.Text = String.Empty;
        txt_SaveDefaultMark.Text = String.Empty;
        txt_SaveQuestion.Text = String.Empty;
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

    private void LoadSaveType()
    {
        SelectQuestionSave.Items.Clear();
    
        SelectQuestionSave.Items.Add("[Select One]");
        SelectQuestionSave.Items.Add("Descriptive");
        SelectQuestionSave.Items.Add("Objective");
    }
    private bool IsValidSaveCategory(String sCategory)
    {
        if (sCategory.Equals("[Select One]"))
        {
            return false;
        }

        return true;
    }

    private bool IsValidQuestionText(String sQuestionText)
    {
        sQuestionText = sQuestionText.Trim();

        if (sQuestionText.Length < 1 || sQuestionText.Length > 1000)
        {
            return false;
        }

        //int i = 0;

        //for (i = 0; i < sQuestionText.Length; i++)
        //{
        //    if (sQuestionText[i].ToString().Equals("'"))
        //    {
        //        return false;
        //    }
        //}

        return true;
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
    private bool IsValidChoices(String sChoices)
    {
        int i = 0;
        
        sChoices = sChoices.Trim();

        if (sChoices.Length < 1)
        {
            return false;
        }

        //for (i = 0; i < sChoices.Length; i++)
        //{
        //    if (sChoices[i].ToString().Equals("'"))
        //    {
        //        return false;
        //    }
        //}

        return true;
    }
    
}
