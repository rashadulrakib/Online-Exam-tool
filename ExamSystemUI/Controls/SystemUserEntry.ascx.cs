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
using Common;
using Entity;
using BO;
using System.Net.Mail;
using System.Threading;
using System.Text.RegularExpressions;
using System.Net;
using Utility;
using log4net;
using log4net.Config;

public partial class Controls_SystemUserEntry : System.Web.UI.UserControl
{
    static int valueStatus = int.MinValue;

    private static readonly ILog logger = LogManager.GetLogger(typeof(Controls_SystemUserEntry));

    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_error.Text = String.Empty;

        try
        {
            log4net.Config.XmlConfigurator.Configure();

            if (!IsPostBack)
            {
                btn_CreateUser.Attributes.Add("onclick", "ClearErrorLebel('"+lbl_error.ClientID+"')");
            }
        }
        catch (Exception oEx)
        {
        }
    }
    protected void btn_CreateUser_Click(object sender, EventArgs e)
    {
        Result oResult = new Result();

        try
        {
            SystemUser oSystemUser = new SystemUser();
            SystemUserBO oSystemUserBO = new SystemUserBO();

            if (IsValidSystemUserName(txt_UserName.Text) && IsValidSystemUserPassword(txt_Password.Text)  && ISvalidEmail(txt_Email.Text))
            {
                oSystemUser.SystemUserName = txt_UserName.Text;
                oSystemUser.SystemUserPassword = txt_Password.Text;
                oSystemUser.SystemUserEmail = txt_Email.Text;

                oResult = oSystemUserBO.SystemUserEntry(oSystemUser);

                if (oResult.ResultIsSuccess)
                {
                    lbl_error.ForeColor = Color.Green;
                    lbl_error.Text = oResult.ResultMessage;

                    Object[] oObjArr = new Object[1];
                    oObjArr[0] = (SystemUser)oResult.ResultObject;

                    object oObject = new object();
                    oObject = oObjArr;

                    ThreadPool.QueueUserWorkItem(new WaitCallback(SendMailToSystemUser), oObject);

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
                lbl_error.Text = "UserName, Password, Email cannot be empty.";
            }
        }
        catch (Exception oEx)
        {
            lbl_error.ForeColor = Color.Red;
            lbl_error.Text = "System User Entry Failed.";
        }
    }

    private static void SendMailToSystemUser(Object oObject)
    {
        logger.Info("start SendMailToSystemUser + Controls_SystemUserEntry");

        Object[] oObjectArr = (Object[])oObject;

        SystemUser oSystemUser = oObjectArr[0] as SystemUser;

        CandidateBO oCandidateBO = new CandidateBO();
        Result oResult = new Result();
        MailForSystemUserEntry oMailForSystemUserEntry = new MailForSystemUserEntry();

        
        SmtpClient oSMTPClient = new SmtpClient();

        try
        {
            if (oSystemUser.SystemUserEmail.Length > 0)
            {
                //oResult = oCandidateBO.LoadMailForCandidate(typeof(MailForSystemUserEntry), "CofigurableXML\\MailForSystemUserEntry.xml");

                oResult = new MailUtil().LoadMail(typeof(MailForSystemUserEntry), "CofigurableXML\\MailForSystemUserEntry.xml");

                if (oResult.ResultIsSuccess)
                {
                    oMailForSystemUserEntry = (MailForSystemUserEntry)oResult.ResultObject;

                    oSMTPClient.Credentials = new NetworkCredential(WindowsLoginManager.csWindowsUserName, WindowsLoginManager.csWindowsPassword, WindowsLoginManager.csWindowsDomain);
                    oSMTPClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    oSMTPClient.Host = ServerManager.csSMTPServerAddress;
                    oSMTPClient.Port = (int)EPortManager.SMTPport;

                    MailMessage oTempMailMessage = new MailMessage();
                    oTempMailMessage.From = new MailAddress(oMailForSystemUserEntry.From);
                    oTempMailMessage.To.Add(oSystemUser.SystemUserEmail);
                    oTempMailMessage.Body = oMailForSystemUserEntry.BodyStart 
                                            + "<br/>Your Login ID is: " 
                                            + oSystemUser.SystemUserName 
                                            + "<br/>Your Password is: " 
                                            + oSystemUser.SystemUserPassword 
                                            + "<br/>" 
                                            + oMailForSystemUserEntry.BodyEnd;
                    oTempMailMessage.Subject = oMailForSystemUserEntry.Subject;
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
            logger.Info("SmtpFailedRecipientException class:Controls_SystemUserEntry method:SendMailToSystemUser" + oEXFailedRecipent.Message);
            
            valueStatus = 0;
        }
        catch (SmtpException oExSMTP)
        {
            logger.Info("SmtpException class:Controls_SystemUserEntry method:SendMailToSystemUser" + oExSMTP.Message);
            
            valueStatus = 1;
        }
        finally
        {
            oSMTPClient = null;
        }

        logger.Info("end SendMailToSystemUser + Controls_SystemUserEntry");
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

    private void clearControlValue()
    {
        txt_Password.Text = String.Empty;
        txt_UserName.Text = String.Empty;
        txt_Email.Text = String.Empty;
    }

    private bool IsValidSystemUserName(String sSystemUserName)
    {
        int i = 0;
        
        sSystemUserName = sSystemUserName.Trim();

        if (sSystemUserName.Length < 1 || sSystemUserName.Length > 200)
        {
            return false;
        }

        for (i = 0; i < sSystemUserName.Length; i++)
        {
            if (sSystemUserName[i].ToString().Equals("'") || sSystemUserName[i]=='-')
            {
                return false;
            }
        }

        return true;
    }
    private bool IsValidSystemUserPassword(String sSystemUserPassword)
    {
        int i = 0;
        
        sSystemUserPassword = sSystemUserPassword.Trim();

        if (sSystemUserPassword.Length < 1 || sSystemUserPassword.Length > 200)
        {
            
            return false;
        }

        for (i = 0; i < sSystemUserPassword.Length; i++)
        {
            if (sSystemUserPassword[i].ToString().Equals("'"))
            {
                return false;
            }
        }

        return true;
    }
}
