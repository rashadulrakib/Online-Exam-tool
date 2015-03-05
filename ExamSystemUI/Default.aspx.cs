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

public partial class _Default : System.Web.UI.Page 
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
            Response.Redirect("Default.aspx");
        }
    }
    
    protected void btn_Login_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValidLoginName(txt_LoginName.Text) && IsValidPassword(txt_Password.Text))
            {
                SystemUser oSystemUser = new SystemUser();
                SystemUserBO oSystemUserBO = new SystemUserBO();
                Result oResult = new Result();

                oSystemUser.SystemUserName = txt_LoginName.Text;
                oSystemUser.SystemUserPassword = txt_Password.Text;

                try
                {
                    oResult = oSystemUserBO.SystemUserLogin(oSystemUser);

                    if (oResult.ResultIsSuccess)
                    {
                        //Utils.csIsSystemUser = true;
                        
                        Utils.SetSession(SessionManager.csLoggedUser, oResult.ResultObject);
                        //Utils.SetSession(SessionManager.csFromPage, PageNameManager.csDefault);

                        Response.Redirect(ContainerManager.csMasterContainer+"?option="+OptionManager.csSystemUserMainPage);
                    }
                    else
                    {
                        lbl_Error.ForeColor = Color.Red;
                        lbl_Error.Text = oResult.ResultMessage;
                    }
                }
                catch (Exception oEx1)
                {
                    lbl_Error.ForeColor = Color.Red;
                    lbl_Error.Text = "Login Exception.";
                }
            }
            else
            {
                lbl_Error.ForeColor = Color.Red;
                lbl_Error.Text = "Login Name & Password Required.";
            }
        }
        catch (Exception oEx2)
        {
            lbl_Error.ForeColor = Color.Red;
            lbl_Error.Text = "Login Exception.";
        }
    }

    private Boolean IsValidLoginName(String sLoginName)
    {
        int i=0;
        
        sLoginName = sLoginName.Trim();

        if (sLoginName.Length < 1 | sLoginName.Length > 200)
        {
            return false;
        }

        for (i = 0; i < sLoginName.Length; i++)
        {
            if (sLoginName[i].ToString().Equals("'"))
            {
                return false;
            }
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
