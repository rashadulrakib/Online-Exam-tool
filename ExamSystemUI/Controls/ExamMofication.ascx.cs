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
using BO;
using Common;

public partial class Controls_ExamMofication : System.Web.UI.UserControl
{
    String[] sArrConstraint = new String[4];
    
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadExamConstraint();
        
        lbl_error.Text = String.Empty;
        try
        {
            if (!IsPostBack)
            {
                LoadHour();
                LoadMinute();
                LoadAmPM();
                //LoadSelectionRadioList();
                LoadExamfromSession();

                btn_ExamDelete.Attributes.Add("onclick", "return confirmMsg('" + lbl_error.ClientID + "','" + dr_SelectExam.ClientID + "',this.form)");
                btn_ExamUpdate.Attributes.Add("onclick", "clientFunctionForUpdate('" + lbl_error.ClientID + "','" + HiddenFieldForDate.ClientID+ "')");
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }
    protected void btn_ExamUpdate_Click(object sender, EventArgs e)
    {
        List<Exam> oListExam = new List<Exam>();
        Exam oExam = new Exam();
        ExamBO oExamBO = new ExamBO();
        Result oResult = new Result();

        String sHiddenFieldDate = String.Empty;
        String sYear = String.Empty;
        String sMonth = String.Empty;
        String sDate = String.Empty;
        String sDateTime = String.Empty;

        DateTime oDateTime = new DateTime();

        try
        {
            if (!dr_SelectExam.SelectedValue.Equals("[Select One]"))
            {
                oListExam = (List<Exam>)Utils.GetSession(SessionManager.csStoredExam);
                
                oExam = oListExam[dr_SelectExam.SelectedIndex - 1];

                if (IsBeforeExamStarted(oExam))
                {
                    sHiddenFieldDate = HiddenFieldForDate.Value.Trim();
                    sYear = sHiddenFieldDate.Substring(0, sHiddenFieldDate.IndexOf('/', 0));
                    sHiddenFieldDate = sHiddenFieldDate.Substring(sHiddenFieldDate.IndexOf('/', 0) + 1);
                    sMonth = sHiddenFieldDate.Substring(0, sHiddenFieldDate.IndexOf('/', 0));
                    sHiddenFieldDate = sHiddenFieldDate.Substring(sHiddenFieldDate.IndexOf('/', 0) + 1);
                    sDate = sHiddenFieldDate;

                    if (sMonth.Length == 1)
                    {
                        sMonth = "0" + sMonth;
                    }

                    if (sDate.Length == 1)
                    {
                        sDate = "0" + sDate;
                    }

                    if (IsValidExamTotalMarks(txt_CurrentTotal.Text))
                    {
                        oExam.ExamTotalMarks = int.Parse(txt_CurrentTotal.Text);
                    }

                    if (IsValidYearMonthDateHour(sYear) && IsValidYearMonthDateHour(sMonth) && IsValidYearMonthDateHour(sDate) && IsValidYearMonthDateHour(dr_Hour.SelectedValue) && IsValidAMPM(dr_AMPM.SelectedValue))
                    {
                        sDateTime = sMonth + "/" + sDate + "/" + sYear + " " + dr_Hour.SelectedValue + ":" + dr_Minute.SelectedValue + " " + dr_AMPM.SelectedValue;

                        oDateTime = DateTime.Parse(sDateTime);

                        if (IsValidExamDateWithStartingTime(oDateTime))
                        {
                            oExam.ExamDateWithStartingTime = oDateTime;
                        }
                    }

                    if (IsValidExamDurationHour(txt_Duration.Text))
                    {
                        oExam.ExamDurationinHour = float.Parse(txt_Duration.Text);
                    }

                    //if (IsAnySelectedConstraint(rdo_SelectConstraint.SelectedValue))
                    //{
                    //    oExam.ExamConstraint = rdo_SelectConstraint.SelectedIndex + 1;  //0=Full Marking,1=Partial Marking, 2=Negative Marking
                    //}

                    if (!chk_Partial.Checked && !chk_Negative.Checked)
                    {
                        oExam.ExamConstraint = 0;
                    }
                    else if (chk_Partial.Checked && !chk_Negative.Checked)
                    {
                        oExam.ExamConstraint = 1;
                    }
                    else if (!chk_Partial.Checked && chk_Negative.Checked)
                    {
                        oExam.ExamConstraint = 2;
                    }
                    else if (chk_Partial.Checked && chk_Negative.Checked)
                    {
                        oExam.ExamConstraint = 3;
                    }

                    oResult = oExamBO.ExamModification(oExam);

                    if (oResult.ResultIsSuccess)
                    {
                        lbl_error.ForeColor = Color.Green;
                        lbl_error.Text = oResult.ResultMessage;

                        oListExam.RemoveAt(dr_SelectExam.SelectedIndex - 1);
                        oListExam.Insert(dr_SelectExam.SelectedIndex - 1, (Exam)oResult.ResultObject);

                        clearControlValue();

                        Utils.SetSession(SessionManager.csStoredExam, oListExam);

                        LoadExamfromSession();
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
                    lbl_error.Text = "Update Before Exam started.";
                }
            }
            else
            {
                lbl_error.ForeColor = Color.Red;
                lbl_error.Text = "Please Select an Exam.";
            }
        }
        catch (Exception oEx)
        {
            lbl_error.ForeColor = Color.Red;
            lbl_error.Text = "Exception occured during Exam Update.";
        }
    }

    private bool IsBeforeExamStarted(Exam oExam)
    {
        if (DateTime.Now >= oExam.ExamDateWithStartingTime)
        {
            return false;
        }

        return true;
    }
    protected void dr_SelectExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            List<Exam> oListExam = new List<Exam>();
            Exam oExam = new Exam();

            if (!dr_SelectExam.SelectedValue.Equals("[Select One]"))
            {
                oListExam = (List<Exam>)Utils.GetSession(SessionManager.csStoredExam);

                oExam = oListExam[dr_SelectExam.SelectedIndex - 1];

                txt_ExamName.Text = oExam.ExamName;
                txt_TotalMarks.Text = oExam.ExamTotalMarks.ToString();
                txt_LastDate.Text = oExam.ExamDateWithStartingTime.ToString();
                txt_LastDuration.Text = oExam.ExamDurationinHour.ToString();
                txt_LastConstraint.Text = sArrConstraint[oExam.ExamConstraint];

                if (oExam.ExamConstraint == 0)
                {
                    chk_Partial.Checked = false;
                    chk_Negative.Checked = false;
                }
                else  if (oExam.ExamConstraint == 1)
                { 
                    chk_Partial.Checked=true;
                    chk_Negative.Checked = false;
                }
                else if (oExam.ExamConstraint == 2)
                {
                    chk_Partial.Checked=false;
                    chk_Negative.Checked = true;
                }
                else if (oExam.ExamConstraint == 3)
                {
                    chk_Partial.Checked = true;
                    chk_Negative.Checked = true;
                }
            }
            else if (dr_SelectExam.SelectedValue.Equals("[Select One]"))
            { 
                clearControlValue();
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }

    private void clearControlValue()
    {
        txt_ExamName.Text = String.Empty;
        txt_CurrentTotal.Text = String.Empty;
        txt_TotalMarks.Text = String.Empty;
        txt_LastDate.Text = String.Empty;
        txt_Duration.Text = String.Empty;
        txt_LastDuration.Text = String.Empty;
        txt_LastConstraint.Text = String.Empty;
        chk_Negative.Checked = false;
        chk_Partial.Checked = false;

        LoadHour();
        LoadMinute();
        LoadAmPM();
    }

    private void LoadExamConstraint()
    {
        sArrConstraint[0] = "Full Marking";
        sArrConstraint[1] = "Partial Marking";
        sArrConstraint[2] = "Negative Marking";
        sArrConstraint[3] = "Partial Negative Marking";
    }
    private void LoadHour()
    {
        int i = 0;

        dr_Hour.Items.Clear();

        for (i = 0; i <= 12; i++)
        {
            if (i < 10)
            {
                dr_Hour.Items.Add("0" + i.ToString());
            }
            else
            {
                dr_Hour.Items.Add(i.ToString());
            }

        }
    }
    private void LoadMinute()
    {
        int i = 0;

        dr_Minute.Items.Clear();

        for (i = 0; i < 60; i++)
        {
            if (i < 10)
            {
                dr_Minute.Items.Add("0" + i.ToString());
            }
            else
            {
                dr_Minute.Items.Add(i.ToString());
            }

        }

    }
    private void LoadAmPM()
    {
        dr_AMPM.Items.Clear();
        dr_AMPM.Items.Add("AM");
        dr_AMPM.Items.Add("PM");
    }
    //private void LoadSelectionRadioList()
    //{
    //    rdo_SelectConstraint.Items.Clear();
    //    rdo_SelectConstraint.Items.Add("Partial Marking");
    //    rdo_SelectConstraint.Items.Add("Negative Marking");
    //}
    private void LoadExamfromSession()
    {
        List<Exam> oListExam = new List<Exam>();

        oListExam = (List<Exam>)Utils.GetSession(SessionManager.csStoredExam);

        dr_SelectExam.Items.Clear();

        dr_SelectExam.Items.Add("[Select One]");

        foreach (Exam oExam in oListExam)
        {
            dr_SelectExam.Items.Add(oExam.ExamName + " [" + oExam.ExamDateWithStartingTime+"]");
        }
    }
    private bool IsValidExamTotalMarks(String sExamTotalMarks)
    {
        int i = 0;

        sExamTotalMarks = sExamTotalMarks.Trim();

        if (!int.TryParse(sExamTotalMarks, out i))
        {
            return false;
        }
        else
        {
            if (int.Parse(sExamTotalMarks) <= 0)
            {
                return false;
            }
        }

        return true;
    }

    private bool IsValidYearMonthDateHour(String sYearMonthDateHour)
    {
        int i = 0;

        if (!int.TryParse(sYearMonthDateHour, out i))
        {
            return false;
        }
        else
        {
            if (int.Parse(sYearMonthDateHour) < 1)
            {
                return false;
            }
        }

        return true;
    }
    private bool IsValidAMPM(String sAMPM)
    {
        if (sAMPM.Equals("00"))
        {
            return false;
        }

        return true;
    }
    private bool IsValidExamDateWithStartingTime(DateTime dtExamDateWithStartingTime)
    {
        //if (dtExamDateWithStartingTime >= DateTime.Now.AddHours(1f)) //orginal constraint by Milton
        if (dtExamDateWithStartingTime >= DateTime.Now)
        {

        }
        else
        {
            return false;
        }

        return true;
    }
    private bool IsValidExamDurationHour(String sExamDurationinHour)
    {
        float f = .0f;

        sExamDurationinHour = sExamDurationinHour.Trim();

        if (!float.TryParse(sExamDurationinHour, out f))
        {
            return false;
        }
        else
        {
            if (float.Parse(sExamDurationinHour) <= 0f || float.Parse(sExamDurationinHour) > 5f)
            {
                return false;
            }
        }

        return true;
    }
    //private bool IsAnySelectedConstraint(String sSelectedConstraint)
    //{
    //    if (sSelectedConstraint.Equals("Partial Marking") || sSelectedConstraint.Equals("Negative Marking"))
    //    {
    //        return true;
    //    }
    //    return false;
    //}
    protected void btn_ExamDelete_Click(object sender, EventArgs e)
    {
        List<Exam> oListExam = new List<Exam>();
        Exam oExam = new Exam();
        ExamBO oExamBO = new ExamBO();
        Result oResult = new Result();

        try
        {
            if (!dr_SelectExam.SelectedValue.Equals("[Select One]"))
            {
                oListExam = (List<Exam>)Utils.GetSession(SessionManager.csStoredExam);

                oExam = oListExam[dr_SelectExam.SelectedIndex - 1];

                //oResult = oExamBO.ExamDelte(oExam);

                oResult = oExamBO.ExamDeleteByStoredProcedure(oExam, (SystemUser)Utils.GetSession(SessionManager.csLoggedUser));

                if (oResult.ResultIsSuccess)
                {
                    oListExam.RemoveAt(dr_SelectExam.SelectedIndex - 1);

                    clearControlValue();

                    Utils.SetSession(SessionManager.csStoredExam, oListExam);

                    LoadExamfromSession();
                    
                    lbl_error.ForeColor = Color.Green;
                    lbl_error.Text = oResult.ResultMessage;
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
                lbl_error.Text = "Please Select an Exam.";
            }
        }
        catch (Exception oEx)
        {
            lbl_error.ForeColor = Color.Red;
            lbl_error.Text = "Exception occured during Exam Delete.";
        }
    }
}
