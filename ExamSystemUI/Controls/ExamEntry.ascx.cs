using System;
using System.Data;
using System.Configuration;
using System.Collections;
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


public partial class Controls_ExamEntry : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_error.Text = String.Empty;
        try
        {
            if (!IsPostBack)
            {
                LoadHour();
                LoadMinute();
                LoadAmPM();
                //LoadSelectionRadioList();

                btn_ExamEntry.Attributes.Add("onclick", "ClearErrorLebel('" + lbl_error.ClientID + "')");
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }

    private void LoadHour()
    {
        int i=0;
        
        dr_Hour.Items.Clear();
        
        for (i = 0; i <= 12; i++)
        {
            if (i < 10)
            {
                dr_Hour.Items.Add("0"+i.ToString());
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
    protected void btn_ExamEntry_Click(object sender, EventArgs e)
    {
        try
        {
            Exam oExam = new Exam();
            ExamBO oExamBO = new ExamBO();
            Result oResult = new Result();
            DateTime oDateTime = new DateTime();

            String sDateTime = String.Empty;

            String sHiddenFieldDate = HiddenFieldForDate.Value.Trim();
            String sYear = sHiddenFieldDate.Substring(0, sHiddenFieldDate.IndexOf('/', 0));
            sHiddenFieldDate = sHiddenFieldDate.Substring(sHiddenFieldDate.IndexOf('/', 0) + 1);
            String sMonth = sHiddenFieldDate.Substring(0, sHiddenFieldDate.IndexOf('/', 0));
            sHiddenFieldDate = sHiddenFieldDate.Substring(sHiddenFieldDate.IndexOf('/', 0) + 1);
            String sDate = sHiddenFieldDate;

            if (sMonth.Length == 1)
            {
                sMonth = "0" + sMonth;
            }

            if (sDate.Length == 1)
            {
                sDate = "0" + sDate;
            }

            if (IsValidExamName(txt_ExamName.Text) && IsValidExamTotalMarks(txt_TotalMarks.Text) && IsValidExamDurationHour(txt_Duration.Text) && IsValidYearMonthDateHour(sYear, "Y") && IsValidYearMonthDateHour(sMonth, "M") && IsValidYearMonthDateHour(sDate, "D") && IsValidYearMonthDateHour(dr_Hour.SelectedValue, "H") && IsValidAMPM(dr_AMPM.SelectedValue))
            {
                sDateTime = sMonth + "/" + sDate + "/" + sYear + " " + dr_Hour.SelectedValue + ":" + dr_Minute.SelectedValue + " " + dr_AMPM.SelectedValue;

                //oDateTime = DateTime.ParseExact(sDateTime, "MM/dd/yyyy hh:mm tt", null);

                oDateTime = DateTime.Parse(sDateTime);

                if (IsValidExamDateWithStartingTime(oDateTime))
                {
                    oExam.ExamName = txt_ExamName.Text;
                    oExam.ExamDurationinHour = float.Parse(txt_Duration.Text);
                    oExam.ExamTotalMarks = int.Parse(txt_TotalMarks.Text);
                    oExam.ExamDateWithStartingTime = oDateTime;
                     //&& IsValidConstraint(rdo_SelectConstraint.SelectedValue)
                    //if (IsAnySelectedConstraint(rdo_SelectConstraint.SelectedValue)) //if no constrained selected then default constraint =0
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

                    oResult = oExamBO.ExamEntry(oExam);

                    if (oResult.ResultIsSuccess)
                    {
                        lbl_error.ForeColor = Color.Green;
                        lbl_error.Text = oResult.ResultMessage;

                        Utils.SetSessionByExamList();

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
                    lbl_error.Text = "Exam Date should be greater than curren DateTime(+1 Hour).";
                }
                
            }
            else
            {
                lbl_error.ForeColor = Color.Red;
                lbl_error.Text = "Enter all value in corrected format.";
            }
            
            //LoadSelectionRadioList();
        }
        catch (Exception oEx)
        {
            lbl_error.ForeColor = Color.Red;
            lbl_error.Text = "Exception occured during Exam Entry.";
        }
    }

    private void clearControlValue()
    {
        txt_ExamName.Text = String.Empty;
        txt_TotalMarks.Text = String.Empty;
        txt_Duration.Text = String.Empty;

        chk_Negative.Checked = false;
        chk_Partial.Checked = false;

        LoadHour();
        LoadMinute();
        LoadAmPM();
    }

    private bool IsValidYearMonthDateHour(String sYearMonthDateHour, String sYearMonthDateHourFlag)
    {
        /*int i=0;

        bool bAllZero= true;

        if (sYearMonthDateFlag.Equals("Y"))
        {
            if (sYearMonthDate.Length!=4)
            {
                return false;
            }
        }
        else if (sYearMonthDateFlag.Equals("M"))
        {
            if (sYearMonthDate.Length!=2)
            {
                return false;
            }
        }
        else if (sYearMonthDateFlag.Equals("D"))
        {
            if (sYearMonthDate.Length!=2)
            {
                return false;
            }
        }

        for (i = 0; i < sYearMonthDate.Trim().Length; i++)
        {
            if (sYearMonthDate[i] >= '0' && sYearMonthDate[i] <= '9')
            {
                if (sYearMonthDate[i]!='0')
                {
                    bAllZero = false;
                }
            }
            else
            {
                return false;
            }
        }

        if (bAllZero)
        {
            return false;
        }*/

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
            
            
            if (sYearMonthDateHourFlag.Equals("Y"))
            {
                
            }
            else if (sYearMonthDateHourFlag.Equals("M"))
            {
                
            }
            else if (sYearMonthDateHourFlag.Equals("D"))
            {
                
            }
            else if (sYearMonthDateHourFlag.Equals("H"))
            {

            }
        }

        return true;
    }

    private bool IsValidExamName(String sExamName)
    {
        int i=0;
        
        sExamName = sExamName.Trim();

        if (sExamName.Length < 1 || sExamName.Length > 100)
        {
            return false;
        }

        for (i = 0; i < sExamName.Length; i++)
        {
            if (sExamName[i].ToString().Equals("'"))
            {
                return false;
            }
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
            if (float.Parse(sExamDurationinHour) <= 0f || float.Parse(sExamDurationinHour)>5f)
            {
                return false;
            }
        }

        return true;
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

    private bool IsValidAMPM(String sAMPM)
    {
        if (sAMPM.Equals("00"))
        {
            return false;
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
}
