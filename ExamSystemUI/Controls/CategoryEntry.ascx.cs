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
using Common;
using BO;

public partial class Controls_CategoryEntry : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_error.Text = String.Empty;

        try
        {
            if (!IsPostBack)
            {
                btn_Save.Attributes.Add("onclick", "ClearErrorLebel('" + lbl_error.ClientID + "')");
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        Category oCategory = new Category();
        Result oResult = new Result();
        CategoryBO oCategoryBO = new CategoryBO();

        try
        {
            if (IsValidCategoryName(txt_CategoryName.Text))
            {
                oCategory.CategoryName = txt_CategoryName.Text;

                oResult = oCategoryBO.CategoryEntry(oCategory);

                if (oResult.ResultIsSuccess)
                {
                    lbl_error.ForeColor = Color.Green;
                    lbl_error.Text = oResult.ResultMessage;

                    clearControlValue();

                    Utils.SetSessionByCategoryList();
                    //LoadCategoryfromSession();
                }
                else
                {
                    lbl_error.ForeColor = Color.Red;
                    lbl_error.Text = oResult.ResultMessage;
                }
            }
            else
            {
                if (txt_CategoryName.Text.Trim().ToLower().Equals("objective") || txt_CategoryName.Text.Trim().ToLower().Equals("descriptive"))
                {
                    lbl_error.ForeColor = Color.Red;
                    lbl_error.Text = "[objective/descriptive] can not be Category Name.";
                }
                else
                {
                    lbl_error.ForeColor = Color.Red;
                    lbl_error.Text = "Category name can not be empty( -, ' not allowed).";
                }
            }
        }
        catch (Exception oEx)
        {
            lbl_error.ForeColor = Color.Red;
            lbl_error.Text = "Exception occured during Category Entry.";
        }
    }

    private void clearControlValue()
    {
        txt_CategoryName.Text = String.Empty;
    }
   
    private bool IsValidCategoryName(String sCategoryName)
    {
        int i = 0;
        
        sCategoryName = sCategoryName.Trim();

        if (sCategoryName.Length < 1 || sCategoryName.Length > 50)
        {
            return false;
        }

        if (sCategoryName.ToLower().Equals("objective") || sCategoryName.ToLower().Equals("descriptive"))
        {
            return false;
        }

        for (i = 0; i < sCategoryName.Length; i++)
        {
            if (sCategoryName[i].ToString() == "'" || sCategoryName[i]=='-')
            {
                return false;
            }
        }
        
        return true;
    }
}
