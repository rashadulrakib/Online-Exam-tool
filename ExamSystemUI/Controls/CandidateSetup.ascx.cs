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
using System.IO;
using Entity;
using Common;
using BO;
using System.Net.Mail;
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Utility;
using System.Net;
using log4net;
using log4net.Config;

public partial class Controls_CandidateSetup : System.Web.UI.UserControl
{
    Object oObject = null;

    static int valueStatus = int.MinValue;

    private static readonly ILog logger = LogManager.GetLogger(typeof(Controls_CandidateSetup));

    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_error.Text = String.Empty;

        try
        {
            //lblPercentage.Visible = false;

            log4net.Config.XmlConfigurator.Configure();

            LoadSelectedExam();
            
            if (!IsPostBack)
            {
                LoadSelectCGOrMark();

                btn_Setup.Attributes.Add("onclick", "ClearErrorLebel('"+lbl_error.ClientID+"')");
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
            lbl_ExamName.Text = oExam.ExamName;
        }
    }
    private void LoadSelectCGOrMark()
    {
        dr_SelectCGOrMark.Items.Clear();
        dr_SelectCGOrMark.Items.Add("CGPA");
        dr_SelectCGOrMark.Items.Add("Mark");
    }
    
    protected void btn_Setup_Click(object sender, EventArgs e)
    {
        try
        {
            Result oResult = new Result();
            Candidate oCandidate = new Candidate();
            CandidateForExam oCandidateForExam = new CandidateForExam();
            CandidateBO oCandidateBO = new CandidateBO();
            Exam oExam = new Exam();

            oExam =(Exam)Utils.GetSession(SessionManager.csSelectedExam);

            if (IsValidCandidateName(txt_CandidateName.Text) && IsValidLastResult(txt_MarkOrCG.Text) && IsValidLastResultRange(txt_MarkOrCG.Text,txtOutOf.Text) && IsValidLastInstitution(txt_LastInstitution.Text) && IsValidLastPassingYear(txt_LastPassingYear.Text) && IsValidCV(fup_ccandidateCv) && IsValidSelection(dr_SelectCGOrMark.SelectedValue) && IsBeforeExamStarted(oExam) && IsValidEmail(txt_Email.Text))
            {
                oCandidate.CandidateCompositeID = txt_CandidateName.Text + DateTime.Now.Ticks.ToString();
                //oCandidate.CadidateCandidateExam.CandiadteExamExam = oExam;
                oCandidate.CandiadteLastInstitution = txt_LastInstitution.Text;
                oCandidate.CandidateName = txt_CandidateName.Text;
                oCandidate.CandidateCvPath = oExam.ExamName + "\\" + oCandidate.CandidateCompositeID.ToString()+"_" + fup_ccandidateCv.FileName; // this partially CV path
                oCandidate.CandidateLastPassingYear = int.Parse(txt_LastPassingYear.Text);
                oCandidate.CandidateLastResult = float.Parse(txt_MarkOrCG.Text);
                oCandidate.CandidatePassword = txt_CandidateName.Text.Substring(0, 2) + "@123";
                oCandidate.CandidateEmail = txt_Email.Text;
                oCandidate.CandidateLastResultRange = float.Parse(txtOutOf.Text);
                oCandidate.LastResultTypaName = dr_SelectCGOrMark.SelectedValue;

                if (!Directory.Exists(DirectoryManager.csCandidateCVDirectory + oExam.ExamName + "\\"))
                {
                    Directory.CreateDirectory(DirectoryManager.csCandidateCVDirectory + oExam.ExamName + "\\");
                }
                
                if (IsValidCandidatePhoto(fup_CandidatePhoto))
                {
                    oCandidate.CandidatePicturePath = oExam.ExamName + "\\" + oCandidate.CandidateCompositeID.ToString() + "_" + fup_CandidatePhoto.FileName;

                    fup_CandidatePhoto.SaveAs(DirectoryManager.csCandidateCVDirectory + oCandidate.CandidatePicturePath);
                }

                oCandidateForExam.CadidateCandidateExam.CandiadteExamExam = oExam;
                oCandidateForExam.CandidateForExamCandidate = oCandidate;

                fup_ccandidateCv.SaveAs(DirectoryManager.csCandidateCVDirectory + oCandidate.CandidateCvPath);

                oResult = oCandidateBO.CandidateSetup(oCandidateForExam);

                if (oResult.ResultIsSuccess)
                {
                    if (rdoEmaiSendNotSend.SelectedValue.Equals("Send ID & Password"))
                    {
                        Object[] oObjArr = new Object[2];
                        oObjArr[0] = oExam;
                        oObjArr[1] = oCandidate;

                        object oObject = new object();
                        oObject = oObjArr;

                        ThreadPool.QueueUserWorkItem(new WaitCallback(SendMailToCandidate), oObject);
                    }
                    else if (rdoEmaiSendNotSend.SelectedValue.Equals("Dont Send"))
                    {

                    }
                    
                    lbl_error.ForeColor = Color.Green;
                    lbl_error.Text = oResult.ResultMessage;

                    clearControlValue();
                }
                else
                {
                    if(File.Exists(DirectoryManager.csCandidateCVDirectory + oCandidate.CandidateCvPath))
                    {
                        File.Delete(DirectoryManager.csCandidateCVDirectory + oCandidate.CandidateCvPath);
                    }
                    
                    lbl_error.ForeColor = Color.Red;
                    lbl_error.Text = oResult.ResultMessage;
                }
            }
            else
            {
                if (!IsBeforeExamStarted(oExam))
                {
                    lbl_error.ForeColor = Color.Red;
                    lbl_error.Text = "Setup Candidate Before Exam Started.";
                }
                else
                {
                    if (!IsValidLastPassingYear(txt_LastPassingYear.Text))
                    {
                        lbl_error.ForeColor = Color.Red;
                        lbl_error.Text = "Passing year must be less than or equal currnet year";
                    }
                    if(!IsValidCV(fup_ccandidateCv))
                    {
                        lbl_error.ForeColor = Color.Red;
                        lbl_error.Text = lbl_error.Text + "<br/>Valid CV Extension(.html,.htm,.doc,.pdf,.rtf)";
                    }
                    if (!IsValidCandidatePhoto(fup_CandidatePhoto))
                    {
                        lbl_error.ForeColor = Color.Red;
                        lbl_error.Text = lbl_error.Text + "<br/>Valid Photo Extension(.gif,.png,.jpg,.jpeg,.bmp)";
                    }
                    //if(!IsValidCV(fup_ccandidateCv) || !IsValidCandidatePhoto(fup_CandidatePhoto))
                    //{
                    //    lbl_error.ForeColor = Color.Red;
                    //    lbl_error.Text = "- is not allowed. Valid CV Extension(.html,.htm,.doc,.pdf,.rtf)" + "<br/>" + "- is not allowed. Valid Image Extension(.gif,.png,.jpg,.jpeg,.bmp)";
                    //}
                }
            }
        }
        catch (Exception oEx)
        {
            lbl_error.ForeColor = Color.Red;
            lbl_error.Text = "Exception occured during Candidate Setup.";
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

    private static void SendMailToCandidate(Object oObject)
    {
        logger.Info("start SendMailToCandidate + Controls_CandidateSetup");

        Object[] oObjectArr = (Object[])oObject;

        Exam oExam = oObjectArr[0] as Exam;
        Candidate oCandidate = oObjectArr[1] as Candidate;

        CandidateBO oCandidateBO = new CandidateBO();
        Result oResult = new Result();
        MailForCandidate oMailForCandidate = new MailForCandidate();

      
        /***************************send mail by  rakib***********/

        SmtpClient oSMTPClient = new SmtpClient();

        try
        {
            if (oCandidate.CandidateEmail.Length > 0)
            {
                //oResult = oCandidateBO.LoadMailForCandidate(typeof(MailForCandidate), "CofigurableXML\\MailForSendCandidate.xml");

                oResult = new MailUtil().LoadMail(typeof(MailForCandidate), "CofigurableXML\\MailForSendCandidate.xml");

                if (oResult.ResultIsSuccess)
                {
                    oMailForCandidate = (MailForCandidate)oResult.ResultObject;


                    oSMTPClient.Credentials = new NetworkCredential(WindowsLoginManager.csWindowsUserName, WindowsLoginManager.csWindowsPassword, WindowsLoginManager.csWindowsDomain);
                    oSMTPClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    oSMTPClient.Host = ServerManager.csSMTPServerAddress;
                    oSMTPClient.Port = (int)EPortManager.SMTPport;

                    MailMessage oTempMailMessage = new MailMessage();
                    oTempMailMessage.From = new MailAddress(oMailForCandidate.From);
                    oTempMailMessage.To.Add(oCandidate.CandidateEmail);
                    oTempMailMessage.Body = oMailForCandidate.BodyStart
                                            + "<br/>Please appear before ["
                                            + oExam.ExamDateWithStartingTime.ToString()
                                            + "] For Exam:"
                                            + oExam.ExamName
                                            + "<br/>Your Login ID is: "
                                            + oCandidate.CandidateEmail
                                            + "<br/>Your Password is: "
                                            + oCandidate.CandidatePassword
                                            + "<br/>"
                                            + oMailForCandidate.BodyEnd;
                    oTempMailMessage.Subject = oMailForCandidate.Subject;
                    oTempMailMessage.IsBodyHtml = true;

                    oSMTPClient.Send(oTempMailMessage);
                }
                else
                {
                    //handle, if mail is not sent to candidate
                }
            }
        }
        catch (SmtpFailedRecipientException oEXFailedRecipent)
        {
            logger.Info("SmtpFailedRecipientException class:Controls_CandidateSetup method:SendMailToCandidate" + oEXFailedRecipent.Message);
            
            valueStatus = 0;
        }
        catch (SmtpException oExSMTP)
        {
            logger.Info("SmtpException class:Controls_CandidateSetup method:SendMailToCandidate" + oExSMTP.Message);
            
            valueStatus = 1;
        }
        finally
        {
            oSMTPClient = null;
        }

        /*********************end send mail by rakib*****************/

        logger.Info("end SendMailToCandidate + Controls_CandidateSetup");
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

    private void clearControlValue()
    {
        txt_CandidateName.Text = String.Empty;
        txt_LastInstitution.Text = String.Empty;
        txt_LastPassingYear.Text = String.Empty;
        txt_MarkOrCG.Text = String.Empty;
        txt_Email.Text = String.Empty;
        txtOutOf.Text = String.Empty;

        LoadSelectCGOrMark();
    }

    private bool IsBeforeExamStarted(Exam oExam)
    {
        //if (DateTime.Now.AddHours(1f) >= oExam.ExamDateWithStartingTime) orginal
        if (DateTime.Now >= oExam.ExamDateWithStartingTime)
        {
            return false;
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

    private bool IsValidCandidateName(String sName)
    {
        sName = sName.Trim();

        if (sName.Length < 2 || sName.Length>100)
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
            Result oResult = new Result();

            DateTime oDateTime = new DateTime();
            
            GetCurrentTimeFromDataBase oGetCurrentTimeFromDataBase = new GetCurrentTimeFromDataBase();

            oResult = oGetCurrentTimeFromDataBase.GetCurrentTime("101");

            if (oResult.ResultIsSuccess)
            {
                oDateTime = DateTime.Parse(oResult.ResultObject.ToString());

                if (int.Parse(sPassingYear) > oDateTime.Year || int.Parse(sPassingYear) < 1000)
                {
                    return false;
                }
            }
            else
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
        sLastResult = sLastResult.Trim();
        
        float f = 0f;

        if (!float.TryParse(sLastResult, out f))
        {
            return false;
        }
        else
        {
            if (float.Parse(sLastResult) <= 0f)
            {
                return false;
            }
        }
        return true;
    }
}
