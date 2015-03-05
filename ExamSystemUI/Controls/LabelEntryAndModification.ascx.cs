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

public partial class Controls_LabelEntryAndModification : System.Web.UI.UserControl
{
    List<Level> oListForDataList = new List<Level>();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lbl_error.Text = String.Empty;
            
            if (!IsPostBack)
            {
                LoadLevelsToDataList();

                btn_delete.Attributes.Add("onclick","return PopWindow('" + listLevels.ClientID + "','"+lbl_error.ClientID+"')");
                btn_AddLevel.Attributes.Add("onclick", "ClearErrorLebel('" + lbl_error.ClientID + "')");
                btnUpdate.Attributes.Add("onclick", "ClearErrorLebel('" + lbl_error.ClientID + "')");
            }
            else
            { 
                oListForDataList = (List<Level>)this.ViewState[SessionManager.csStoreGridView];
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }

    private void LoadLevelsToDataList()
    {
        try
        {
            Result oResult = new Result();
            LevelBO oLevelBO = new LevelBO();

            oResult = oLevelBO.LoadAllLevels();

            listLevels.Items.Clear();

            if (oResult.ResultIsSuccess)
            {
                oListForDataList = (List<Level>)oResult.ResultObject;

                this.ViewState.Add(SessionManager.csStoreGridView, oListForDataList);

                //listLevels.DataSource = oListForDataList;
                //listLevels.DataBind();

                foreach (Level oLevelInList in oListForDataList)
                {
                    listLevels.Items.Add(new ListItem(oLevelInList.LevelName,oLevelInList.LevelName));
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
        
        }
    }
   
    protected void btnAdfromList_Click(object sender, EventArgs e)
    {
        try
        {
            if (listLevels.Items.Count>0)
            {
                if (listLevels.SelectedIndex >= 0)
                {
                    txtLavelName.Text = oListForDataList[listLevels.SelectedIndex].LevelName;
                    txtLevelDescription.Text = oListForDataList[listLevels.SelectedIndex].LevelDescription;
                }
                else
                {
                    lbl_error.ForeColor = Color.Red;
                    lbl_error.Text = "select a level.";
                }
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }
    protected void btn_AddLevel_Click(object sender, EventArgs e)
    {
        try
        {
            Result oResult = new Result();
            Level oLevel = new Level();
            LevelBO oLevelBO = new LevelBO();

            if (IsValidLevelName(txtLavelName.Text))
            {
                oLevel.LevelID = Guid.NewGuid();
                oLevel.LevelName = txtLavelName.Text;

                if (IsValidLevelDescription(txtLevelDescription.Text))
                {
                    oLevel.LevelDescription = txtLevelDescription.Text.Replace("'", String.Empty);
                }

                oResult = oLevelBO.LevelEntry(oLevel);

                if (oResult.ResultIsSuccess)
                {
                    lbl_error.ForeColor = Color.Green;
                    lbl_error.Text = oResult.ResultMessage;

                    LoadLevelsToDataList();

                    clearField();
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
                lbl_error.Text = "',- is not Allowed. Level must not empty";
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }

    private void clearField()
    {
        txtLavelName.Text = String.Empty;
        txtLevelDescription.Text = String.Empty;
    }

    private bool IsValidLevelDescription(string sLevelDescription)
    {
        sLevelDescription = sLevelDescription.Trim();

        if (sLevelDescription.Length > 1000)
        {
            return false;
        }

        //for (int i = 0; i < sLevelDescription.Length; i++)
        //{
        //    if (sLevelDescription[i].ToString().Equals("'"))
        //    {
        //        return false;
        //    }
        //}

        return true;
    }

    private bool IsValidLevelName(string sLevelName)
    {
        sLevelName = sLevelName.Trim();

        if (sLevelName.Length < 1 || sLevelName.Length > 200)
        {
            return false;
        }

        for (int i = 0; i < sLevelName.Length; i++)
        {
            if (sLevelName[i] == '-' || sLevelName[i].ToString().Equals("'"))
            {
                return false;
            }
        }

        return true;
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            Result oResult = new Result();

            LevelBO oLevelBO = new LevelBO();

            //Level oOldLevel = new Level();
            
            if (listLevels.Items.Count > 0)
            {
                if (listLevels.SelectedIndex >= 0)
                {
                    if (IsValidLevelName(txtLavelName.Text))
                    {
                        //oOldLevel = oListForDataList[listLevels.SelectedIndex];
                        
                        oListForDataList[listLevels.SelectedIndex].LevelName = txtLavelName.Text;

                        if (IsValidLevelDescription(txtLevelDescription.Text))
                        {
                            oListForDataList[listLevels.SelectedIndex].LevelDescription = txtLevelDescription.Text.Replace("'", String.Empty);
                        }

                        oResult = oLevelBO.LevelUpdate(oListForDataList[listLevels.SelectedIndex]);

                        if (oResult.ResultIsSuccess)
                        {
                            LoadLevelsToDataList();

                            clearField();

                            lbl_error.ForeColor = Color.Green;
                            lbl_error.Text = oResult.ResultMessage;
                        }
                        else
                        {
                            lbl_error.ForeColor = Color.Red;
                            lbl_error.Text = oResult.ResultMessage;

                            LoadLevelsToDataList();
                        }
                    }
                    else
                    {
                        lbl_error.ForeColor = Color.Red;
                        lbl_error.Text = "',- is not Allowed. Level must not empty";
                    }
                }
                else
                {
                    lbl_error.ForeColor = Color.Red;
                    lbl_error.Text = "select a level.";
                }
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        try
        {
            Result oResult = new Result();

            LevelBO oLevelBO = new LevelBO();

            if (listLevels.Items.Count > 0)
            {
                if (listLevels.SelectedIndex >= 0)
                {
                    oResult = oLevelBO.Delete(oListForDataList[listLevels.SelectedIndex]);

                    if (oResult.ResultIsSuccess)
                    {
                        lbl_error.ForeColor = Color.Green;
                        lbl_error.Text = oResult.ResultMessage;

                        LoadLevelsToDataList();

                        clearField();
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
                    lbl_error.Text = "select a level.";
                }
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }
}
