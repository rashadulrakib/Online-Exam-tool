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
using System.Text.RegularExpressions;

public partial class Controls_CandidateModification : System.Web.UI.UserControl
{
    SystemUser oSystemUser = new SystemUser();
    Exam oSelectedExam = new Exam();

    List<CandidateForExam> oListOfCandidateForExamForGrid = new List<CandidateForExam>();

    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_error.Text = String.Empty;

        try
        {
            LoadSystemUser();

            LoadSelectedExam();

            if (!IsPostBack)
            {
                LoadCandidatesFromDBToGrid();
            }
            else
            { 
                //retrive data from ViewState
                //oListOfCandidateForGrid = (List<Candidate>)Utils.GetSession(SessionManager.csStoreGridView);
                oListOfCandidateForExamForGrid = (List<CandidateForExam>)this.ViewState[SessionManager.csStoreGridView];
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
    private void LoadCandidatesFromDBToGrid()
    {
        CandidateBO oCandidateBO = new CandidateBO();
        Result oResult = new Result();
        List<CandidateForExam> oListCandidateForExam = new List<CandidateForExam>();

        try
        {
            oResult = oCandidateBO.ShowAllCandidates(oSelectedExam);
            
            if (oResult.ResultIsSuccess)
            {
                oListCandidateForExam = (List<CandidateForExam>)oResult.ResultObject;
                
                //oObjectOfListOfCandidate = oListCandidate;
                //this.ViewState["DataGrid"] = oObjectOfListOfCandidate;

                //Utils.SetSession(SessionManager.csStoreGridView, oListCandidate);
                this.ViewState.Add(SessionManager.csStoreGridView, oListCandidateForExam);

                if (oListCandidateForExam.Count <= 0)
                {
                    lbl_error.ForeColor = Color.Red;
                    lbl_error.Text = "No Candidate Found.";
                    tblCanModification.Visible = false;
                }
                else
                {
                    tblCanModification.Visible = true;
                    Grid_Candidates.DataSource = oListCandidateForExam;
                    Grid_Candidates.DataBind();
                }

                lblTotalCandidates.Text = oListCandidateForExam.Count.ToString();
            }
            else
            { 
            
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }
    protected void Grid_Candidates_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        CandidateForExam oCandidateForExam = e.Row.DataItem as CandidateForExam;

        String sCvPath = String.Empty;

        try
        {
            if (oCandidateForExam != null)
            {
                //Label lblCvPath = e.Row.FindControl("lblOldCVName") as Label;
                //lblCvPath.Text = oCandidate.CandidateCvPath.Substring(oCandidate.CandidateCvPath.IndexOf('_')+1);

                DropDownList dr_SelectCGOrMark = e.Row.FindControl("dr_SelectCGOrMark") as DropDownList;

                //if (!IsPostBack)
                {
                    if (dr_SelectCGOrMark != null)
                    {
                        dr_SelectCGOrMark.Items.Clear();
                        dr_SelectCGOrMark.Items.Add("CGPA");
                        dr_SelectCGOrMark.Items.Add("Mark");

                        if (oCandidateForExam.CandidateForExamCandidate.LastResultTypaName.Equals("CGPA"))
                        {
                            dr_SelectCGOrMark.SelectedIndex = 0;
                        }
                        else if (oCandidateForExam.CandidateForExamCandidate.LastResultTypaName.Equals("Mark"))
                        {
                            dr_SelectCGOrMark.SelectedIndex = 1;
                        }
                    }
                }
                //else
                { 
                
                }

                HyperLink oHyperLink = e.Row.FindControl("hyperLinkOldCV") as HyperLink;
                String sCvPathWithoutLastSlash = oCandidateForExam.CandidateForExamCandidate.CandidateCvPath.Substring(oCandidateForExam.CandidateForExamCandidate.CandidateCvPath.LastIndexOf('\\') + 1);
                oHyperLink.Text = sCvPathWithoutLastSlash.Substring(sCvPathWithoutLastSlash.IndexOf('_') + 1);
                sCvPath = oCandidateForExam.CandidateForExamCandidate.CandidateCvPath;
                sCvPath = sCvPath.Replace(' ', '-');
                oHyperLink.NavigateUrl = "~/ShowCV.aspx?CVPath=" + sCvPath;
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }
    protected void btn_Remove_Click(object sender, EventArgs e)
    {
        //List<CandidateForExam> oListCandidate = new List<CandidateForExam>();
        
        CandidateBO oCandidateBO = new CandidateBO();
        Result oResult = new Result();

        Boolean bAnyChecked = false;

        try
        {
            int[] iArrCheck = new int[oListOfCandidateForExamForGrid.Count];

            for (int i = 0; i < iArrCheck.Length; i++)
            {
                iArrCheck[i] = 0;
            }

            if (IsBeforeExamStarted(oSelectedExam))
            {
                foreach (GridViewRow oGridRow in Grid_Candidates.Rows)
                {
                    CheckBox oCheckBox = oGridRow.FindControl("deleteRec") as CheckBox;

                    if (oCheckBox.Checked)
                    {
                        bAnyChecked = true;

                        iArrCheck[Grid_Candidates.PageIndex * Grid_Candidates.PageSize + oGridRow.RowIndex] = 1;
                    }
                    //else
                    //{
                    //    iArrCheck[oGridRow.RowIndex] = 0;
                    //}
                }

                //oListCandidate = oListOfCandidateForGrid;

                if (bAnyChecked)
                {
                    oResult = oCandidateBO.RemoveCandidate(oListOfCandidateForExamForGrid, iArrCheck);

                    if (oResult.ResultIsSuccess)
                    {
                        oListOfCandidateForExamForGrid = (List<CandidateForExam>)oResult.ResultObject;

                        //Utils.SetSession(SessionManager.csStoreGridView, oListCandidate);
                        this.ViewState.Add(SessionManager.csStoreGridView, oListOfCandidateForExamForGrid);

                        if (oListOfCandidateForExamForGrid.Count <= 0)
                        {
                            tblCanModification.Visible = false;
                        }
                        else
                        {
                            tblCanModification.Visible = true;
                            Grid_Candidates.DataSource = oListOfCandidateForExamForGrid;
                            Grid_Candidates.DataBind();
                        }

                        lbl_error.ForeColor = Color.Green;
                        lbl_error.Text = oResult.ResultMessage;

                        lblTotalCandidates.Text = oListOfCandidateForExamForGrid.Count.ToString();
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
                    lbl_error.Text = "Please Select a Candidate.";
                }
            }
            else
            {
                lbl_error.ForeColor = Color.Red;
                lbl_error.Text = "Remove before Exam Started.";
            }
        }
        catch (Exception oEx)
        {
            lbl_error.ForeColor = Color.Red;
            lbl_error.Text = "Candidate Remove Exception.";
        }
    }
    protected void Grid_Candidates_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Grid_Candidates.PageIndex = e.NewPageIndex;

            Grid_Candidates.DataSource = oListOfCandidateForExamForGrid;
            Grid_Candidates.DataBind();
        }
        catch (Exception oEx)
        {
            lbl_error.ForeColor = Color.Red;
            lbl_error.Text = "Data Bind Failed.";
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
    protected void btn_Update_Click(object sender, EventArgs e)
    {
        List<CandidateForExam> oListCandidateForExam = new List<CandidateForExam>();
        
        CandidateBO oCandidateBO = new CandidateBO();
        Result oResult = new Result();

        Boolean bAnyChecked = false;

        try
        {
            oListCandidateForExam = oListOfCandidateForExamForGrid;

            int[] iArrCheck = new int[oListCandidateForExam.Count];

            for (int i = 0; i < iArrCheck.Length; i++)
            {
                iArrCheck[i] = 0;
            }

            foreach (GridViewRow oGridRow in Grid_Candidates.Rows)
            {
                CheckBox oCheckBox = oGridRow.FindControl("deleteRec") as CheckBox;
                TextBox oTxtPassword = oGridRow.FindControl("txtNewPassword") as TextBox;
                TextBox txtNewEmail = oGridRow.FindControl("txtNewEmail") as TextBox;
                TextBox oTxtName = oGridRow.FindControl("txtNewName") as TextBox;
                TextBox oTxtResult = oGridRow.FindControl("txtLastResult") as TextBox;
                TextBox oTxtInstitution = oGridRow.FindControl("txtLastInstitution") as TextBox;
                TextBox oTxtPassYear = oGridRow.FindControl("txtLastPassingYear") as TextBox;
                FileUpload oFupCv = oGridRow.FindControl("fupNewCVName") as FileUpload;

                DropDownList dr_SelectCGOrMark = oGridRow.FindControl("dr_SelectCGOrMark") as DropDownList;
                TextBox txtOutOf = oGridRow.FindControl("txtOutOf") as TextBox;
                FileUpload fup_CandidatePhoto = oGridRow.FindControl("fup_CandidatePhoto") as FileUpload;

                Label oLblCandidateID = oGridRow.FindControl("lblCandidateID") as Label;

                if (oCheckBox.Checked)
                {
                    bAnyChecked = true;

                    if (IsValidCandidatePassword(oTxtPassword.Text))
                    {
                        oListCandidateForExam[Grid_Candidates.PageIndex * Grid_Candidates.PageSize + oGridRow.RowIndex].CandidateForExamCandidate.CandidatePassword = oTxtPassword.Text;
                    }

                    if (IsValidCandidateName(oTxtName.Text))
                    {
                        oListCandidateForExam[Grid_Candidates.PageIndex * Grid_Candidates.PageSize + oGridRow.RowIndex].CandidateForExamCandidate.CandidateName = oTxtName.Text;
                    }

                    if (IsValidEmail(txtNewEmail.Text))
                    {
                        oListCandidateForExam[Grid_Candidates.PageIndex * Grid_Candidates.PageSize + oGridRow.RowIndex].CandidateForExamCandidate.CandidateEmail = txtNewEmail.Text;
                    }

                    if (IsValidLastResult(oTxtResult.Text))
                    {
                        oListCandidateForExam[Grid_Candidates.PageIndex * Grid_Candidates.PageSize + oGridRow.RowIndex].CandidateForExamCandidate.CandidateLastResult = float.Parse(oTxtResult.Text);
                    }

                    if (IsValidLastInstitution(oTxtInstitution.Text))
                    {
                        oListCandidateForExam[Grid_Candidates.PageIndex * Grid_Candidates.PageSize + oGridRow.RowIndex].CandidateForExamCandidate.CandiadteLastInstitution = oTxtInstitution.Text;
                    }

                    if (IsValidLastPassingYear(oTxtPassYear.Text))
                    {
                        oListCandidateForExam[Grid_Candidates.PageIndex * Grid_Candidates.PageSize + oGridRow.RowIndex].CandidateForExamCandidate.CandidateLastPassingYear = int.Parse(oTxtPassYear.Text);
                    }
                                       
                    if (IsValidCV(oFupCv))
                    {
                        if (!Directory.Exists(DirectoryManager.csCandidateCVDirectory + oSelectedExam.ExamName + "\\"))
                        {
                            Directory.CreateDirectory(DirectoryManager.csCandidateCVDirectory + oSelectedExam.ExamName + "\\");
                        }

                        oListCandidateForExam[Grid_Candidates.PageIndex * Grid_Candidates.PageSize + oGridRow.RowIndex].CandidateForExamCandidate.CandidateCvPath = oSelectedExam.ExamName + "\\" + oLblCandidateID.Text + "_" + oFupCv.FileName;

                        oFupCv.SaveAs(DirectoryManager.csCandidateCVDirectory + oListCandidateForExam[Grid_Candidates.PageIndex * Grid_Candidates.PageSize + oGridRow.RowIndex].CandidateForExamCandidate.CandidateCvPath);
                    }

                    
                    
                    
                    
                    
                    
                    if (IsValidSelection(dr_SelectCGOrMark.SelectedValue))
                    {
                        oListCandidateForExam[Grid_Candidates.PageIndex * Grid_Candidates.PageSize + oGridRow.RowIndex].CandidateForExamCandidate.LastResultTypaName = dr_SelectCGOrMark.SelectedValue;
                    }

                    if (IsValidLastResultRange(oTxtResult.Text, txtOutOf.Text))
                    {
                        oListCandidateForExam[Grid_Candidates.PageIndex * Grid_Candidates.PageSize + oGridRow.RowIndex].CandidateForExamCandidate.CandidateLastResultRange = float.Parse(txtOutOf.Text);
                    }

                    if (IsValidCandidatePhoto(fup_CandidatePhoto))
                    {
                        if (!Directory.Exists(DirectoryManager.csCandidateCVDirectory + oSelectedExam.ExamName + "\\"))
                        {
                            Directory.CreateDirectory(DirectoryManager.csCandidateCVDirectory + oSelectedExam.ExamName + "\\");
                        }

                        oListCandidateForExam[Grid_Candidates.PageIndex * Grid_Candidates.PageSize + oGridRow.RowIndex].CandidateForExamCandidate.CandidatePicturePath = oSelectedExam.ExamName + "\\" + oLblCandidateID.Text + "_" + fup_CandidatePhoto.FileName;

                        fup_CandidatePhoto.SaveAs(DirectoryManager.csCandidateCVDirectory + oListCandidateForExam[Grid_Candidates.PageIndex * Grid_Candidates.PageSize + oGridRow.RowIndex].CandidateForExamCandidate.CandidatePicturePath);
                    }

                    
                    iArrCheck[Grid_Candidates.PageIndex * Grid_Candidates.PageSize + oGridRow.RowIndex] = 1;
                }
                //else
                //{
                //    iArrCheck[oGridRow.RowIndex] = 0;
                //}
            }

            if (bAnyChecked)
            {
                oResult = oCandidateBO.UpdateCandidate(oListCandidateForExam, iArrCheck);

                if (oResult.ResultIsSuccess)
                {
                    LoadCandidatesFromDBToGrid();
                    
                    //oListCandidateForExam = (List<CandidateForExam>)oResult.ResultObject;

                    //this.ViewState.Add(SessionManager.csStoreGridView, oListCandidateForExam);

                    //oListOfCandidateForExamForGrid = oListCandidateForExam;

                    //Grid_Candidates.DataSource = oListCandidateForExam;
                    //Grid_Candidates.DataBind();

                    lbl_error.ForeColor = Color.Green;
                    lbl_error.Text = oResult.ResultMessage;

                    //lblTotalCandidates.Text = oListOfCandidateForExamForGrid.Count.ToString();


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
                lbl_error.Text = "Please Select a Candidate.";
            }
        }
        catch (Exception oEx)
        {
            lbl_error.ForeColor = Color.Red;
            lbl_error.Text = "Candidate Update Exception.";
        }
    }

    private bool IsValidCandidatePhoto(FileUpload oFileUpload)
    {
        if (!oFileUpload.HasFile)
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

        if (oFileUpload.FileName.ToLower().LastIndexOf(".gif") > 0 || oFileUpload.FileName.ToLower().LastIndexOf(".png") > 0 || oFileUpload.FileName.ToLower().LastIndexOf(".jpg") > 0 || oFileUpload.FileName.ToLower().LastIndexOf(".jpeg") > 0 || oFileUpload.FileName.ToLower().LastIndexOf(".bmp") > 0)
        {

        }
        else
        {
            return false;
        }

        return true;
    }
    private bool IsValidLastResultRange(string sLastResult, string sLastResultRange)
    {
        sLastResult = sLastResult.Trim();
        sLastResultRange = sLastResultRange.Trim();

        float f = 0f;

        if (!float.TryParse(sLastResultRange, out f))
        {
            return false;
        }
        else
        {
            if (float.Parse(sLastResultRange) < float.Parse(sLastResult))
            {
                return false;
            }
        }

        return true;
    }

    private bool IsValidSelection(string sSelectionValue)
    {
        if (sSelectionValue.Equals("[Select One]"))
        {
            return false;
        }

        return true;
    }

    private bool IsValidEmail(String sEmail)
    {
        sEmail = sEmail.Trim();

        Regex oRegex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");

        if (sEmail.Length < 1 || sEmail.Length > 100)
        {
            return false;
        }

        for (int i = 0; i < sEmail.Length; i++)
        {
            if (sEmail[i].ToString().Equals("'") || sEmail[i] == ' ')
            {
                return false;
            }
        }

        if (!oRegex.IsMatch(sEmail))
        {
            return false;
        }

        return true;
    }

    private bool IsValidCandidateName(String sName)
    {
        sName = sName.Trim();

        if (sName.Length < 1 || sName.Length>100)
        {
            return false;
        }

        for (int i = 0; i < sName.Length; i++)
        {
            if (sName[i].ToString().Equals("'") || sName[i].ToString().Equals("-"))
            {
                return false;
            }
        }

        return true;
    }
    private bool IsValidCandidatePassword(String sPassword)
    {
        sPassword = sPassword.Trim();

        if (sPassword.Length < 1 || sPassword.Length>200)
        {
            return false;
        }

        for (int i = 0; i < sPassword.Length; i++)
        {
            if (sPassword[i].ToString().Equals("'") || sPassword[i].ToString().Equals("-"))
            {
                return false;
            }
        }

        return true;
    }
    private bool IsValidLastInstitution(String sInstitution)
    {
        sInstitution = sInstitution.Trim();

        if (sInstitution.Length < 1 || sInstitution.Length>50)
        {
            return false;
        }

        for (int i = 0; i < sInstitution.Length; i++)
        {
            if (sInstitution[i].ToString().Equals("'"))
            {
                return false;
            }
        }

        return true;
    }
    private bool IsValidLastPassingYear(String sPassingYear)
    {
        int i = 0;

        if (!int.TryParse(sPassingYear, out i))
        {
            return false;
        }
        else
        {
            if (int.Parse(sPassingYear) > DateTime.Now.Year || int.Parse(sPassingYear) <= 0)
            {
                return false;
            }
        }

        return true;
    }
    private bool IsValidCV(FileUpload oFileUpload)
    {
        if (!oFileUpload.HasFile)
        {
            return false;
        }

        if (oFileUpload.FileName.ToLower().LastIndexOf(".htm") > 0 || oFileUpload.FileName.ToLower().LastIndexOf(".html") > 0 || oFileUpload.FileName.ToLower().LastIndexOf(".doc") > 0 || oFileUpload.FileName.ToLower().LastIndexOf(".pdf") > 0 || oFileUpload.FileName.ToLower().LastIndexOf(".rtf") > 0)
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
    private bool IsValidLastResult(String sLastResult)
    {
        float f = 0f;

        if (!float.TryParse(sLastResult, out f))
        {
            return false;
        }
        return true;
    }
}
