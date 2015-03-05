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

public partial class Controls_ChangePassword : System.Web.UI.UserControl
{
    SystemUser oSystemUser = new SystemUser();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_error.Text = String.Empty;
        
        try
        {
            oSystemUser = Utils.GetSession(SessionManager.csLoggedUser) as SystemUser;
            
            //LoadOldPassword(oSystemUser);

            if (!IsPostBack)
            {
                btn_ChangePassword.Attributes.Add("onclick", "ClearErrorLebel('" + lbl_error.ClientID + "')");
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }
    protected void btn_ChangePassword_Click(object sender, EventArgs e)
    {
        Result oResult = new Result();
        SystemUserBO oSystemUserBO = new SystemUserBO();
        
        try
        {
            if (isValidPassword(txt_OldPassword.Text) && isValidPassword(txt_NewPassword.Text) && isValidPassword(txt_ConfirmPassword.Text))
            {
                if (txt_OldPassword.Text.Equals(oSystemUser.SystemUserPassword))
                {
                    if (txt_NewPassword.Text.Equals(txt_ConfirmPassword.Text))
                    {
                        oResult = oSystemUserBO.ChangePassword(oSystemUser, txt_NewPassword.Text, txt_ConfirmPassword.Text);

                        if (oResult.ResultIsSuccess)
                        {
                            lbl_error.ForeColor = Color.Green;
                            lbl_error.Text = oResult.ResultMessage;

                            Utils.SetSession(SessionManager.csLoggedUser, (SystemUser)oResult.ResultObject);
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
                        lbl_error.Text = "Your New Password & Confirm Password does not match.";
                    }
                }
                else
                {
                    lbl_error.ForeColor = Color.Red;
                    lbl_error.Text = "Your Old Password does not match.";
                }
            }
            else
            {
                lbl_error.ForeColor = Color.Red;
                lbl_error.Text = "Enter Password (' is not allowed).";
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }

    private bool isValidPassword(String sSystemUserPassword)
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

    private void LoadOldPassword(SystemUser oSystemUser)
    {
        try
        {
            //txt_OldPassword.Text = String.Empty;

            //for (int i = 0; i < oSystemUser.SystemUserPassword.Length; i++)
            //{
            //    txt_OldPassword.Text = txt_OldPassword.Text + "*";
            //}
        }
        catch (Exception oEx)
        { 
        
        }
    }
}
