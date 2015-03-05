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
using Utility;
using BO;
using System.Net.Mail;
using System.Threading;
using System.Diagnostics;
using System.Net;
using log4net;
using log4net.Config;

public partial class Controls_CandidateExtend : System.Web.UI.UserControl
{
    SystemUser oSystemUser = new SystemUser();
    Exam oSelectedExam = new Exam();

    List<CandidateForExam> oListOfCandidateForExamForGrid = new List<CandidateForExam>();

    static int valueStatus = int.MinValue;

    private static readonly ILog logger = LogManager.GetLogger(typeof(Controls_CandidateExtend));

    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_error.Text = String.Empty;

        grid_CandidatesOfExam.Visible = false;
        btn_AddCandidate.Visible = false;
        rdoEmaiSendNotSend.Visible = false;

        try
        {
            log4net.Config.XmlConfigurator.Configure();
            
            LoadSystemUser();

            LoadSelectedExam();

            if (!IsPostBack)
            {
                LoadExamfromSession();
            }
            else
            {
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
    protected void dr_SelectExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (!dr_SelectExam.SelectedValue.Equals("[Select One]"))
            {
                List<Exam> oListExam = new List<Exam>();

                oListExam = (List<Exam>)Utils.GetSession(SessionManager.csStoredExam);

                LoadCandidatesForAnExam(oListExam[dr_SelectExam.SelectedIndex-1]);
            }
            else
            {
                lbl_error.ForeColor = Color.Red;
                lbl_error.Text = "Please Select an Exam.";
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }

    private void LoadCandidatesForAnExam(Exam oExam)
    {
        try
        {
            Result oResult = new Result();
            CandidateBO oCandidateBO = new CandidateBO();

            oResult = oCandidateBO.ShowAllCandidates(oExam);

            List<CandidateForExam> oListCandidateForExam = (List<CandidateForExam>)oResult.ResultObject;

            this.ViewState.Add(SessionManager.csStoreGridView, oListCandidateForExam);
            
            if (oListCandidateForExam.Count <= 0)
            {
                lbl_error.ForeColor = Color.Red;
                lbl_error.Text = "No Candidate Found.";
            }
            else
            {
                grid_CandidatesOfExam.Visible = true;
                btn_AddCandidate.Visible = true;
                rdoEmaiSendNotSend.Visible = true;
                grid_CandidatesOfExam.DataSource = oListCandidateForExam;
                grid_CandidatesOfExam.DataBind();
            }

        }
        catch (Exception oEx)
        { 
        
        }
    }

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
    protected void grid_CandidatesOfExam_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grid_CandidatesOfExam.Visible = true;
            btn_AddCandidate.Visible = true;
            rdoEmaiSendNotSend.Visible = true;
            
            grid_CandidatesOfExam.PageIndex = e.NewPageIndex;

            grid_CandidatesOfExam.DataSource = oListOfCandidateForExamForGrid;
            grid_CandidatesOfExam.DataBind();
        }
        catch (Exception oEx)
        { 
        
        }
    }
    protected void grid_CandidatesOfExam_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void btn_AddCandidate_Click(object sender, EventArgs e)
    {
        try
        {
            Boolean bAnyChecked = false;

            Result oResult = new Result();
            CandidateBO oCandidateBO = new CandidateBO();

            int[] iArrCheck = new int[oListOfCandidateForExamForGrid.Count];

            for (int i = 0; i < iArrCheck.Length; i++)
            {
                iArrCheck[i] = 0;
            }

            if (IsBeforeExamStarted(oSelectedExam))
            {
                foreach (GridViewRow oGridRow in grid_CandidatesOfExam.Rows)
                {
                    CheckBox oCheckBox = oGridRow.FindControl("deleteRec") as CheckBox;

                    if (oCheckBox.Checked)
                    {
                        bAnyChecked = true;

                        iArrCheck[grid_CandidatesOfExam.PageIndex*grid_CandidatesOfExam.PageSize+oGridRow.RowIndex] = 1;
                    }
                    //else
                    //{
                    //    iArrCheck[oGridRow.RowIndex] = 0;
                    //}
                }

                if (bAnyChecked)
                {
                    oResult = oCandidateBO.AddCandidatesFromExistingCandidate(oListOfCandidateForExamForGrid, iArrCheck, oSelectedExam);

                    if (oResult.ResultIsSuccess)
                    {
                        lbl_error.ForeColor = Color.Green;
                        lbl_error.Text = oResult.ResultMessage;

                        if (rdoEmaiSendNotSend.SelectedValue.Equals("Send ID & Password"))
                        {
                            SendMailToListOfCandidate(oListOfCandidateForExamForGrid, iArrCheck, oSelectedExam);
                        }
                        else if (rdoEmaiSendNotSend.SelectedValue.Equals("Dont Send"))
                        {

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
                    lbl_error.Text = "Please select candidate to add.";
                }
            }
            else
            {
                lbl_error.ForeColor = Color.Red;
                lbl_error.Text = "Add Candidate before Exam Started.";
            }

            grid_CandidatesOfExam.Visible = true;
            btn_AddCandidate.Visible = true;
            rdoEmaiSendNotSend.Visible = true;

            grid_CandidatesOfExam.DataSource = oListOfCandidateForExamForGrid;
            grid_CandidatesOfExam.DataBind();

        }
        catch (Exception oEx)
        { 
            
        }
    }

    private void SendMailToListOfCandidate(List<CandidateForExam> oListOfCandidateForExamForGrid, int[] iArrCheck, Exam oExam)
    {
        try
        {
            Result oResult = new Result();
            MailForExtendCandidate oMailForExtendCandidate = new MailForExtendCandidate();
             

            //oResult = oCandidateBO.LoadMailForCandidate(typeof(MailForExtendCandidate), "CofigurableXML\\MailForExtendCandidate.xml");

            oResult = new MailUtil().LoadMail(typeof(MailForExtendCandidate), "CofigurableXML\\MailForExtendCandidate.xml");

            if (oResult.ResultIsSuccess)
            {
                oMailForExtendCandidate = (MailForExtendCandidate)oResult.ResultObject;

                for (int i = 0; i < iArrCheck.Length; i++)
                {
                    if (iArrCheck[i] == 1)
                    {
                        Object[] oObjArr = new Object[3];
                        oObjArr[0] = oExam;
                        oObjArr[1] = oListOfCandidateForExamForGrid[i].CandidateForExamCandidate;
                        oObjArr[2] = oMailForExtendCandidate;

                        object oObject = new object();
                        oObject = oObjArr;

                        ThreadPool.QueueUserWorkItem(new WaitCallback(SendMailToCandidate), oObject);
                    }
                }
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }

    private static void SendMailToCandidate(Object oObject)
    {
        logger.Info("start SendMailToCandidate + Controls_CandidateExtend");
        
        Object[] oObjectArr = (Object[])oObject;

        Exam oExam = oObjectArr[0] as Exam;
        Candidate oCandidate = oObjectArr[1] as Candidate;
        MailForExtendCandidate oMailForExtendCandidate = oObjectArr[2] as MailForExtendCandidate;

        //CandidateBO oCandidateBO = new CandidateBO();
        //Result oResult = new Result();
        //MailForExtendCandidate oMailForExtendCandidate = new MailForExtendCandidate();

        SmtpClient oSMTPClient = new SmtpClient();

        try
        {
            if (oCandidate.CandidateEmail.Length > 0)
            {
                //oResult = oCandidateBO.LoadMailForCandidate(typeof(MailForExtendCandidate), "CofigurableXML\\MailForExtendCandidate.xml");

                //if (oResult.ResultIsSuccess)
                //{
                //    oMailForExtendCandidate = (MailForExtendCandidate)oResult.ResultObject;


                    oSMTPClient.Credentials = new NetworkCredential(WindowsLoginManager.csWindowsUserName, WindowsLoginManager.csWindowsPassword, WindowsLoginManager.csWindowsDomain);
                    oSMTPClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    oSMTPClient.Host = ServerManager.csSMTPServerAddress;
                    oSMTPClient.Port = (int)EPortManager.SMTPport;

                    MailMessage oTempMailMessage = new MailMessage();
                    oTempMailMessage.From = new MailAddress(oMailForExtendCandidate.From);
                    oTempMailMessage.To.Add(oCandidate.CandidateEmail);
                    oTempMailMessage.Body = oMailForExtendCandidate.BodyStart
                                            + "<br/>Please appear before ["
                                            + oExam.ExamDateWithStartingTime.ToString()
                                            + "] For Exam:"
                                            + oExam.ExamName
                                            + "<br/>Your Login ID is: "
                                            + oCandidate.CandidateEmail
                                            + "<br/>Your Password is: "
                                            + oCandidate.CandidatePassword
                                            + "<br/>"
                                            + oMailForExtendCandidate.BodyEnd;
                    oTempMailMessage.Subject = oMailForExtendCandidate.Subject;
                    oTempMailMessage.IsBodyHtml = true;

                    oSMTPClient.Send(oTempMailMessage);
                //}
                //else
                //{
                    //handle, if mail is not sent to candidate
                //}
            }
        }
        catch (SmtpFailedRecipientException oEXFailedRecipent)
        {
            logger.Info("SmtpFailedRecipientException class:Controls_CandidateExtend method:SendMailToCandidate" + oEXFailedRecipent.Message);
            
            valueStatus = 0;
        }
        catch (SmtpException oExSMTP)
        {
            logger.Info("SmtpException class:Controls_CandidateExtend method:SendMailToCandidate" + oExSMTP.Message);
            
            valueStatus = 1;
        }
        finally
        {
            oSMTPClient = null;
        }
        logger.Info("end SendMailToCandidate + Controls_CandidateExtend");
    }

    private bool IsBeforeExamStarted(Exam oExam)
    {
        if (DateTime.Now >= oExam.ExamDateWithStartingTime)
        {
            return false;
        }

        return true;
    }
}
