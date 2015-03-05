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

public partial class Controls_CategoryModification : System.Web.UI.UserControl
{
    SystemUser oSystemUser = new SystemUser();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            LoadSystemUser();

            if (!IsPostBack)
            {
                if (oSystemUser.SystemUserName.ToLower().Equals("administrator"))
                {
                    LoadCategoriesToGrid();
                }
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }

    private void LoadSystemUser()
    {
        oSystemUser = (SystemUser)Utils.GetSession(SessionManager.csLoggedUser);
    }

    private void LoadCategoriesToGrid()
    {
        CategoryBO oCategoryBO = new CategoryBO();
        Result oResult = new Result();
        List<Category> oListCategory = new List<Category>();

        try
        {
            oListCategory = (List<Category>)Utils.GetSession(SessionManager.csStoredCategory);

            if (oListCategory.Count <= 0)
            {
                tblCategory.Visible = false;

                lbl_error.ForeColor = Color.Red;
                lbl_error.Text = "No Category Found.";
            }
            else
            {
                tblCategory.Visible = true;
                
                Grid_Categories.DataSource = oListCategory;
                Grid_Categories.DataBind();
            }

            
        }
        catch (Exception oEx)
        {
            lbl_error.ForeColor = Color.Red;
            lbl_error.Text = "Grid Load Exception.";
        }   
    }
    protected void Grid_Categories_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (oSystemUser.SystemUserName.ToLower().Equals("administrator"))
            {
                Category oTempCategory = e.Row.DataItem as Category;

                if (oTempCategory != null)
                {
                    Label lblCategoryName = e.Row.FindControl("lblCategoryName") as Label;

                    lblCategoryName.Text = oTempCategory.CategoryName;
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
    protected void Grid_Categories_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Grid_Categories.PageIndex = e.NewPageIndex;

            Grid_Categories.DataSource = (List<Category>)Utils.GetSession(SessionManager.csStoredCategory);
            Grid_Categories.DataBind();
        }
        catch (Exception oEx)
        {
            lbl_error.ForeColor = Color.Red;
            lbl_error.Text = "Data Bind Failed";
        }
    }
    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        List<Category> oListCategory = new List<Category>();

        CategoryBO oCategoryBO = new CategoryBO();
        Result oResult = new Result();

        Boolean bAnyChecked = false;

        try
        {
            oListCategory = (List<Category>)Utils.GetSession(SessionManager.csStoredCategory);

            int[] iArrCheck = new int[oListCategory.Count];

            for (int i = 0; i < iArrCheck.Length; i++)
            {
                iArrCheck[i] = 0;
            }

            foreach (GridViewRow oGridRow in Grid_Categories.Rows)
            {
                CheckBox oCheckBox = oGridRow.FindControl("deleteRec") as CheckBox;

                if (oCheckBox.Checked)
                {
                    bAnyChecked = true;

                    iArrCheck[Grid_Categories.PageIndex * Grid_Categories.PageSize + oGridRow.RowIndex] = 1;
                }
                //else
                //{
                //    iArrCheck[oGridRow.RowIndex] = 0;
                //}
            }

            if (bAnyChecked)
            {
                oResult = oCategoryBO.CategoryDelete(oListCategory, iArrCheck);

                if (oResult.ResultIsSuccess)
                {
                    Utils.SetSessionByCategoryList();

                    LoadCategoriesToGrid();

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
                lbl_error.Text = "Please Select a Category.";
            }
        }
        catch (Exception oEx)
        {
            lbl_error.ForeColor = Color.Red;
            lbl_error.Text = "Category Delete Exception.";
        }
    }
    protected void btn_Update_Click(object sender, EventArgs e)
    {
        List<Category> oListCategory = new List<Category>();

        CategoryBO oCategoryBO = new CategoryBO();
        Result oResult = new Result();

        Boolean bAnyChecked = false;

        try
        {
            oListCategory = (List<Category>)Utils.GetSession(SessionManager.csStoredCategory);

            int[] iArrCheck = new int[oListCategory.Count];

            for (int i = 0; i < iArrCheck.Length; i++)
            {
                iArrCheck[i] = 0;
            }

            foreach (GridViewRow oGridRow in Grid_Categories.Rows)
            {
                CheckBox oCheckBox = oGridRow.FindControl("deleteRec") as CheckBox;
                TextBox oTxtName = oGridRow.FindControl("txtCategoryName") as TextBox;

                if (oCheckBox.Checked)
                {
                    bAnyChecked = true;

                    iArrCheck[Grid_Categories.PageIndex * Grid_Categories.PageSize + oGridRow.RowIndex] = 1;

                    if (IsValidCategoryName(oTxtName.Text, oListCategory))
                    {
                        oListCategory[Grid_Categories.PageIndex * Grid_Categories.PageSize + oGridRow.RowIndex].CategoryName = oTxtName.Text;
                    }
                }
                //else
                //{
                //    iArrCheck[oGridRow.RowIndex] = 0;
                //}
            }

            if (bAnyChecked)
            {
                oResult = oCategoryBO.CategoryUpdate(oListCategory, iArrCheck);

                if (oResult.ResultIsSuccess)
                {
                    oListCategory = (List<Category>)oResult.ResultObject;

                    Utils.SetSession(SessionManager.csStoredCategory, oListCategory);

                    //if (oListCategory.Count <= 0)
                    //{
                    //    lbl_error.ForeColor = Color.Red;
                    //    lbl_error.Text = "No Category Found.";
                    //}

                    //Grid_Categories.DataSource = oListCategory;
                    //Grid_Categories.DataBind();
                    
                    LoadCategoriesToGrid();

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
                lbl_error.Text = "Please Select a Category.";
            }
        }
        catch (Exception oEx)
        {
            lbl_error.ForeColor = Color.Red;
            lbl_error.Text = "Category Update Exception.";
        }
    }

    private bool IsValidCategoryName(String sCategoryName,List<Category> oLocalListCategory)
    {
        int i = 0;

        sCategoryName = sCategoryName.Trim();

        foreach (Category oCategoryInList in oLocalListCategory)
        {
            if (oCategoryInList.CategoryName.Equals(sCategoryName))
            {
                return false;
            }
        }

        if (sCategoryName.Length < 1 || sCategoryName.Length > 50)
        {
            return false;
        }

        for (i = 0; i < sCategoryName.Length; i++)
        {
            if (sCategoryName[i].ToString() == "'")
            {
                return false;
            }
        }

        return true;
    }
}
