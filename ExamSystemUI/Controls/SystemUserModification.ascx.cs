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
using System.Threading;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Net;
using Utility;
using log4net;
using log4net.Config;

public partial class Controls_SystemUserModification : System.Web.UI.UserControl
{
    SystemUser oSystemUser = new SystemUser();

    List<SystemUser> oListSystemUserForGrid = new List<SystemUser>();

    static int valueStatus = int.MinValue;

    private static readonly ILog logger = LogManager.GetLogger(typeof(Controls_SystemUserModification));
    
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_error.Text = String.Empty;
        
        try
        {
            log4net.Config.XmlConfigurator.Configure();
            
            LoadSystemUser();

            if (!IsPostBack)
            {
                if (oSystemUser.SystemUserName.ToLower().Equals("administrator"))
                {
                    LoadSystemUsersToGrid(false,null);
                }
            }
            else
            {
                //oListSystemUserForGrid = (List<SystemUser>)Utils.GetSession(SessionManager.csStoreGridView);
                oListSystemUserForGrid = (List<SystemUser>)this.ViewState[SessionManager.csStoreGridView];
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }

    private void LoadSystemUsersToGrid(bool bSendMail, int[] iArrCheck)
    {
        SystemUserBO oSystemUserBO = new SystemUserBO();
        Result oResult = new Result();
        List<SystemUser> oListSystemUser = new List<SystemUser>();

        try
        {
            oResult = oSystemUserBO.ShowAllSystemUsers();

            if (oResult.ResultIsSuccess)
            {
                oListSystemUser = (List<SystemUser>)oResult.ResultObject;

                //Utils.SetSession(SessionManager.csStoreGridView, oListSystemUser);
                this.ViewState.Add(SessionManager.csStoreGridView, oListSystemUser);

                Grid_SystemUsers.DataSource = oListSystemUser;
                Grid_SystemUsers.DataBind();

                if (bSendMail && iArrCheck!=null)
                {
                    SendMailToSystemUserForUpdateNotifiaction(oListSystemUser, iArrCheck);
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
            lbl_error.ForeColor = Color.Red;
            lbl_error.Text = "Grid Load Exception.";
        }
    }

    private void LoadSystemUser()
    {
        oSystemUser = (SystemUser)Utils.GetSession(SessionManager.csLoggedUser);
    }
    protected void Grid_Questions_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (oSystemUser.SystemUserName.ToLower().Equals("administrator")) // if the system user == adminnistrator then the value will be bound to gridview
            {
                SystemUser oTempSystemUser = e.Row.DataItem as SystemUser;

                String sPassword = String.Empty;

                int i = 0;

                if (oTempSystemUser != null)
                {
                    //Label lblUserName = e.Row.FindControl("lblUserName") as Label;
                    //Label lblPassword = e.Row.FindControl("lblPassword") as Label;

                    TextBox txtUserName = e.Row.FindControl("txtUserName") as TextBox;
                    TextBox txtPassword = e.Row.FindControl("txtPassword") as TextBox;
                    TextBox txtEmail = e.Row.FindControl("txtEmail") as TextBox;

                    for (i = 0; i < oTempSystemUser.SystemUserPassword.Length; i++)
                    {
                        sPassword = sPassword + "*";
                    }

                    //lblPassword.Text = sPassword;
                    //lblUserName.Text = oTempSystemUser.SystemUserName;

                    txtUserName.Text = oTempSystemUser.SystemUserName;
                    txtPassword.Text = sPassword;
                    txtEmail.Text = oTempSystemUser.SystemUserEmail;

                    if (oTempSystemUser.SystemUserName.ToLower().Equals("administrator"))
                    {
                        txtUserName.ReadOnly = true;
                    }

                    //else
                    //{
                    //    txtPassword.ReadOnly = true;
                    //}
                }
            }
            else
            { 
            
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }
    protected void Grid_SystemUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Grid_SystemUsers.PageIndex = e.NewPageIndex;

            Grid_SystemUsers.DataSource = oListSystemUserForGrid;
            Grid_SystemUsers.DataBind();
        }
        catch (Exception oEx)
        {
            lbl_error.ForeColor = Color.Red;
            lbl_error.Text = "Data Bind Failed";
        }
    }
    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        List<SystemUser> oListSystemUser = new List<SystemUser>();

        SystemUserBO oSystemUserBO = new SystemUserBO();
        Result oResult = new Result();

        Boolean bAnyChecked = false;

        try
        {
            oListSystemUser = oListSystemUserForGrid;

            int[] iArrCheck = new int[oListSystemUser.Count];

            for (int i = 0; i < iArrCheck.Length; i++)
            {
                iArrCheck[i] = 0;
            }

            foreach (GridViewRow oGridRow in Grid_SystemUsers.Rows)
            {
                CheckBox oCheckBox = oGridRow.FindControl("deleteRec") as CheckBox;

                if (oCheckBox.Checked && !oListSystemUser[Grid_SystemUsers.PageIndex * Grid_SystemUsers.PageSize + oGridRow.RowIndex].SystemUserName.ToLower().Equals("administrator"))
                {
                    iArrCheck[Grid_SystemUsers.PageIndex * Grid_SystemUsers.PageSize + oGridRow.RowIndex] = 1;

                    bAnyChecked = true;
                }
                //else
                //{
                //    iArrCheck[oGridRow.RowIndex] = 0;
                //}
            }

            if (bAnyChecked)
            {
                oResult = oSystemUserBO.SystemUserDelete(oListSystemUser, iArrCheck);

                if (oResult.ResultIsSuccess)
                {
                    oListSystemUser = (List<SystemUser>)oResult.ResultObject;

                    //Utils.SetSession(SessionManager.csStoreGridView, oListSystemUser);
                    this.ViewState.Add(SessionManager.csStoreGridView, oListSystemUser);

                    Grid_SystemUsers.DataSource = oListSystemUser;
                    Grid_SystemUsers.DataBind();

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
                lbl_error.Text = "Please Select a System User Without Admin.";
            }
        }
        catch (Exception oEx)
        {
            lbl_error.ForeColor = Color.Red;
            lbl_error.Text = "SystemUser Delete Exception.";
        }
    }
    protected void btn_Update_Click(object sender, EventArgs e)
    {
        List<SystemUser> oListSystemUser = new List<SystemUser>();

        SystemUserBO oSystemUserBO = new SystemUserBO();
        Result oResult = new Result();

        Boolean bAnyChecked = false;

        try
        {
            oListSystemUser = oListSystemUserForGrid;

            int[] iArrCheck = new int[oListSystemUser.Count];

            for (int i = 0; i < iArrCheck.Length; i++)
            {
                iArrCheck[i] = 0;
            }

                foreach (GridViewRow oGridRow in Grid_SystemUsers.Rows)
                {
                    CheckBox oCheckBox = oGridRow.FindControl("deleteRec") as CheckBox;
                    TextBox oTxtPassword = oGridRow.FindControl("txtPassword") as TextBox;
                    TextBox oTxtName = oGridRow.FindControl("txtUserName") as TextBox;
                    TextBox oTxtEmail = oGridRow.FindControl("txtEmail") as TextBox;

                    if (oCheckBox.Checked)
                    {
                        bAnyChecked = true;

                        //if (oListSystemUser[oGridRow.RowIndex].SystemUserName.ToLower().Equals("administrator") && IsValidSystemUserPassword(oTxtPassword.Text))
                        if (IsValidSystemUserPassword(oTxtPassword.Text))
                        {
                            oListSystemUser[Grid_SystemUsers.PageIndex * Grid_SystemUsers.PageSize + oGridRow.RowIndex].SystemUserPassword = oTxtPassword.Text;
                        }

                        if (IsValidSystemUserName(oTxtName.Text, oListSystemUser))
                        {
                            oListSystemUser[Grid_SystemUsers.PageIndex * Grid_SystemUsers.PageSize + oGridRow.RowIndex].SystemUserName = oTxtName.Text;
                        }

                        if (ISvalidEmail(oTxtEmail.Text))
                        {
                            oListSystemUser[Grid_SystemUsers.PageIndex * Grid_SystemUsers.PageSize + oGridRow.RowIndex].SystemUserEmail = oTxtEmail.Text;
                        }

                        iArrCheck[Grid_SystemUsers.PageIndex * Grid_SystemUsers.PageSize + oGridRow.RowIndex] = 1;
                    }
                    //else
                    //{
                    //    iArrCheck[oGridRow.RowIndex] = 0;
                    //}
                }

            if (bAnyChecked)
            {
                oResult = oSystemUserBO.UpdateSystemUser(oListSystemUser, iArrCheck);

                if (oResult.ResultIsSuccess)
                {
                    //oListSystemUser = (List<SystemUser>)oResult.ResultObject;

                    ////Utils.SetSession(SessionManager.csStoreGridView, oListSystemUser);
                    //this.ViewState.Add(SessionManager.csStoreGridView, oListSystemUser);

                    //Grid_SystemUsers.DataSource = oListSystemUser;
                    //Grid_SystemUsers.DataBind();

                    lbl_error.ForeColor = Color.Green;
                    lbl_error.Text = oResult.ResultMessage;

                    LoadSystemUsersToGrid(true, iArrCheck);
                    
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
                lbl_error.Text = "Please Select a System User.";
            }
        }
        catch (Exception oEx)
        {
            lbl_error.ForeColor = Color.Red;
            lbl_error.Text = "System user Update Exception.";
        }
    }

    private bool ISvalidEmail(String sEmail)
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

    private void SendMailToSystemUserForUpdateNotifiaction(List<SystemUser> oListSystemUser, int[] iArrCheck)
    {
        try
        {
            CandidateBO oCandidateBO = new CandidateBO();
            Result oResult = new Result();
            MailForSystemUserModification oMailForSystemUserModification = new MailForSystemUserModification();

            //oResult = oCandidateBO.LoadMailForCandidate(typeof(MailForSystemUserModification), "CofigurableXML\\MailForSystemUserModification.xml");

            oResult = new MailUtil().LoadMail(typeof(MailForSystemUserModification), "CofigurableXML\\MailForSystemUserModification.xml");

            if (oResult.ResultIsSuccess)
            {
                oMailForSystemUserModification = (MailForSystemUserModification)oResult.ResultObject;
                
                for (int i = 0; i < iArrCheck.Length; i++)
                {
                    if (iArrCheck[i] == 1)
                    {
                        object oObject = new object();

                        object[] oArr = new object[2];

                        oArr[0] = oListSystemUser[i];

                        oArr[1] = oMailForSystemUserModification;

                        oObject = oArr;

                        ThreadPool.QueueUserWorkItem(new WaitCallback(SendMailToSystemUser), oObject);
                    }
                }
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }


    private static void SendMailToSystemUser(object oObject)
    {
        logger.Info("start SendMailToSystemUser + Controls_SystemUserModification");
        
        object[] oObjectArr = (object[])oObject;

        SystemUser oTempSystemUser = (SystemUser)oObjectArr[0];
        MailForSystemUserModification oMailForSystemUserModification = (MailForSystemUserModification)oObjectArr[1];

        //CandidateBO oCandidateBO = new CandidateBO();
        //Result oResult = new Result();
        //MailForSystemUserModification oMailForSystemUserModification= new MailForSystemUserModification();

        //try
        //{
        //    if (oTempSystemUser.SystemUserEmail.Length > 0)
        //    {
        //        oResult = oCandidateBO.LoadMailForCandidate(typeof(MailForSystemUserModification), "CofigurableXML\\MailForSystemUserModification.xml");

        //        if (oResult.ResultIsSuccess)
        //        {
        //            oMailForSystemUserModification = (MailForSystemUserModification)oResult.ResultObject;

        //            MailMessage oMailMessage = new MailMessage(oMailForSystemUserModification.From, oTempSystemUser.SystemUserEmail, oMailForSystemUserModification.Subject, oMailForSystemUserModification.BodyStart + "<br/>Your New Login ID is: " + oTempSystemUser.SystemUserName + "<br/>Your New Password is: " + oTempSystemUser.SystemUserPassword + "<br/>Your New Email is: " + oTempSystemUser.SystemUserEmail + "<br/>" + oMailForSystemUserModification.BodyEnd);

        //            oMailMessage.IsBodyHtml = true;

        //            SmtpClient oSmtpClient = new SmtpClient("mars.internal.pyxisnet.com", 25);

        //            //oSmtpClient.Send("rr@pyxisnet.com", oCandidate.CandidateEmail, "Invitation For Interview", "Please appear before [" + oExam.ExamDateWithStartingTime.ToString() + "] For Exam:" + oExam.ExamName + "<br />Your Login ID is: " + oCandidate.CandidateCompositeID + "<br />Your Password is: " + oCandidate.CandidatePassword);
        //            oSmtpClient.Send(oMailMessage);
        //        }
        //        else
        //        {
        //            //handle, if mail is not sent to candidate
        //        }
        //    }
        //}
        //catch (Exception oEx)
        //{
        //    //handle, if mail is not sent to candidate
        //}


        SmtpClient oSMTPClient = new SmtpClient();

        try
        {
            if (oTempSystemUser.SystemUserEmail.Length > 0)
            {
                //oResult = oCandidateBO.LoadMailForCandidate(typeof(MailForSystemUserModification), "CofigurableXML\\MailForSystemUserModification.xml");

                //if (oResult.ResultIsSuccess)
                {
                    //oMailForSystemUserModification = (MailForSystemUserModification)oResult.ResultObject;

                    oSMTPClient.Credentials = new NetworkCredential(WindowsLoginManager.csWindowsUserName, WindowsLoginManager.csWindowsPassword, WindowsLoginManager.csWindowsDomain);
                    oSMTPClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    oSMTPClient.Host = ServerManager.csSMTPServerAddress;
                    oSMTPClient.Port = (int)EPortManager.SMTPport;

                    MailMessage oTempMailMessage = new MailMessage();
                    oTempMailMessage.From = new MailAddress(oMailForSystemUserModification.From);
                    oTempMailMessage.To.Add(oTempSystemUser.SystemUserEmail);
                    oTempMailMessage.Body = oMailForSystemUserModification.BodyStart
                                            + "<br/>Your Login ID is: "
                                            + oTempSystemUser.SystemUserName
                                            + "<br/>Your Password is: "
                                            + oTempSystemUser.SystemUserPassword
                                            + "<br/>"
                                            + oMailForSystemUserModification.BodyEnd;
                    oTempMailMessage.Subject = oMailForSystemUserModification.Subject;
                    oTempMailMessage.IsBodyHtml = true;

                    oSMTPClient.Send(oTempMailMessage);
                }
                //else
                //{
                //    //handle, if mail is not sent to candidate
                //}
            }
        }
        catch (SmtpFailedRecipientException oEXFailedRecipent)
        {
            logger.Info("SmtpFailedRecipientException class:Controls_SystemUserModification method:SendMailToSystemUser" + oEXFailedRecipent.Message);
            
            valueStatus = 0;
        }
        catch (SmtpException oExSMTP)
        {
            logger.Info("SmtpException class:Controls_SystemUserModification method:SendMailToSystemUser" + oExSMTP.Message);
            
            valueStatus = 1;
        }
        finally
        {
            oSMTPClient = null;
        }

        logger.Info("end SendMailToSystemUser + Controls_SystemUserModification");
    }


    private bool IsValidSystemUserName(String sName, List<SystemUser> oLocalListSystemUser)
    {
        sName = sName.Trim();

        foreach (SystemUser oSystemUserInList in oLocalListSystemUser)
        {
            if (sName.Equals(oSystemUserInList.SystemUserName))
            {
                return false;
            }
        }

        if (sName.Length < 1 || sName.Length > 200)
        {
            return false;
        }

        for (int i = 0; i < sName.Length; i++)
        {
            if (sName[i].ToString().Equals("'") || sName[i] == '-')
            {
                return false;
            }
        }

        return true;
    }
    private bool IsValidSystemUserPassword(String sPassword)
    {
        sPassword = sPassword.Trim();

        if (sPassword.Length < 1 || sPassword.Length > 200)
        {
            return false;
        }

        for (int i = 0; i < sPassword.Length; i++)
        {
            if (sPassword[i].ToString().Equals("'"))
            {
                return false;
            }
        }

        return true;
    }
}
