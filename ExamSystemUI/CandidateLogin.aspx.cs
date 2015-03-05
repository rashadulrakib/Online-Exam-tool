using System;
using System.Data;
using System.Configuration;
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
using System.Text.RegularExpressions;

public partial class CandidateLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session.Abandon();
            }
        }
        catch (Exception oEx)
        {
            Response.Redirect("CandidateLogin.aspx");
        }
    }
    protected void btn_Login_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValidEmail(txt_LoginName.Text) && IsValidPassword(txt_Password.Text))
            {
                //Utils.csIsSystemUser = false;

                Candidate oCandidate = new Candidate();
                CandidateBO oCandidateBO = new CandidateBO();
                Result oResult = new Result();

                oCandidate.CandidateEmail = txt_LoginName.Text;
                oCandidate.CandidatePassword = txt_Password.Text;
                oCandidate.CandidateLoginTime = DateTime.Now;

                try
                {
                    oResult = oCandidateBO.CandidateLogin(oCandidate);

                    if (oResult.ResultIsSuccess)
                    {
                        Utils.SetSession(SessionManager.csLoggedUser, (CandidateForExam)oResult.ResultObject);

                        Response.Redirect(ContainerManager.csMasterContainerCandidate + "?option=" + OptionManager.csExamProcess);
                    }
                    else
                    {
                        lbl_Error.ForeColor = Color.Red;
                        lbl_Error.Text = oResult.ResultMessage;
                    }
                }
                catch (Exception oEx)
                {
                    lbl_Error.ForeColor = Color.Red;
                    lbl_Error.Text = "Login Failed.";
                }
            }
            else
            {
                lbl_Error.ForeColor = Color.Red;
                lbl_Error.Text = "Email & Password Required.";
            }

        }
        catch (Exception oEx2)
        {
            lbl_Error.ForeColor = Color.Red;
            lbl_Error.Text = "Login Failed.";
        }
       
    }

    //private Boolean IsValidLoginName(String sLoginName)
    //{
    //    int i = 0;

    //    sLoginName = sLoginName.Trim();

    //    if (sLoginName.Length < 1 || sLoginName.Length > 200)
    //    {
    //        return false;
    //    }

    //    for (i = 0; i < sLoginName.Length; i++)
    //    {
    //        if (sLoginName[i].ToString().Equals("'"))
    //        {
    //            return false;
    //        }
    //    }

    //    return true;
    //}

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
    private Boolean IsValidPassword(String sPassword)
    {
        int i = 0;

        sPassword = sPassword.Trim();

        if (sPassword.Length < 1 || sPassword.Length > 200)
        {
            return false;
        }

        for (i = 0; i < sPassword.Length; i++)
        {
            if (sPassword[i].ToString().Equals("'"))
            {
                return false;
            }
        }

        return true;
    }

    protected override void OnPreRender(EventArgs e)
    {
        txt_LoginName.Focus();

        base.OnPreRender(e);
    }

}
