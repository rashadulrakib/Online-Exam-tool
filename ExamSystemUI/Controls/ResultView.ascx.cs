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

public partial class Controls_ResultView : System.Web.UI.UserControl
{
    List<CandidateMenu> oListCandidateMenu = new List<CandidateMenu>();

    Exam oExam = new Exam();

    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oExam = (Exam)Utils.GetSession(SessionManager.csSelectedExam);

            lbl_ExamName.Text = oExam.ExamName;

            if (!IsPostBack)
            {
                LoadCategoriesWithTypeForAnExam();
            }
            else
            {
                //dt = (DataTable)Utils.GetSession(SessionManager.csStoreGridView);
                dt = (DataTable)this.ViewState[SessionManager.csStoreGridView];
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }

    private void LoadCategoriesWithTypeForAnExam()
    {
        Result oResult = new Result();
        ResultViewBO oResultViewBO = new ResultViewBO();

        try
        {
            oResult = oResultViewBO.LoadCategoriesWithTypeForAnExam(oExam);

            if (oResult.ResultIsSuccess)
            {
                oListCandidateMenu = (List<CandidateMenu>)oResult.ResultObject;

                /*int iHeaderCellCount=0;

                Table oTableMain = new Table();
                TableRow oTableRowMain = new TableRow();

                foreach (CandidateMenu oCandidateMenuInList in oListCandidateMenu)
                {
                    Table oTable = new Table();

                    TableRow oTableRowCategory = new TableRow();
                    TableCell oTableCellCategory = new TableCell();
                    oTableCellCategory.Text = oCandidateMenuInList.CandidateMenuCategory.CategoryName;
                    oTableRowCategory.Cells.Add(oTableCellCategory);

                    TableRow oTableRowType = new TableRow();
                    foreach (QuestionType oQuestionType in oCandidateMenuInList.CandidateMenuCategoryQuestionType)
                    {
                        TableCell oTableCellType = new TableCell();
                        oTableCellType.Text = oQuestionType.QuestionTypeName;
                        oTableRowType.Cells.Add(oTableCellType);

                        TableCell oTableCellSpace = new TableCell();
                        oTableCellSpace.Width = Unit.Pixel(2);
                        oTableRowType.Cells.Add(oTableCellSpace);
                    }

                    oTable.Rows.Add(oTableRowCategory);
                    oTable.Rows.Add(oTableRowType);

                    grid_Results.HeaderRow.Cells[iHeaderCellCount++].Controls.Add(oTable);
                }

                grid_Results.DataSource = oListCandidateMenu;
                grid_Results.DataBind();*/

                /*foreach (CandidateMenu oCandidateMenuInList in oListCandidateMenu)
                {
                    TemplateField bfield = new TemplateField();



                    //Initalize the DataField value.

                    bfield.HeaderTemplate = new GridViewTemplate(ListItemType.Header, col.ColumnName);



                    //Initialize the HeaderText field value.

                    bfield.ItemTemplate = new GridViewTemplate(ListItemType.Item, col.ColumnName);



                    //Add the newly created bound field to the GridView.

                    GrdDynamic.Columns.Add(bfield);
                }

                grid_Results.DataSource = dt;
                grid_Results.DataBind();*/

                oResult = oResultViewBO.GetCandidateAVGMarksForTypeAndCategory(oExam, oListCandidateMenu); // this is orginally total

                if (oResult.ResultIsSuccess)
                {
                    dt = (DataTable)oResult.ResultObject;

                    //prepareDataTable(oCandidateResultsView);

                    //Utils.SetSession(SessionManager.csStoreGridView, dt);
                    this.ViewState.Add(SessionManager.csStoreGridView, dt);

                    if (dt.Rows.Count <= 0)
                    {
                        lbl_error.ForeColor = Color.Red;
                        lbl_error.Text = "No Candidate found.";
                        tblResultView.Visible = false;
                    }
                    else
                    {
                        tblResultView.Visible = true;
                        grid_Results.DataSource = dt;
                        grid_Results.DataBind();

                        //crRVCandidateResults.ReportSource = dt;

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
                lbl_error.Text = oResult.ResultMessage;
            }
        }
        catch (Exception oEx)
        { 
        
        }
    }

    //private void prepareDataTable(CandidateResultsView oCandidateResultsView)
    //{
    //    int i = 0;
        
    //    DataColumn dcolName = new DataColumn("Name:",typeof(System.String));
    //    dt.Columns.Add(dcolName);

    //    foreach (CandidateMenu oCandidateMenuInList in oCandidateResultsView.ListOfListCandidateResults[0]) //columns
    //    {
    //        foreach (QuestionType oQuestionTypeInList in oCandidateMenuInList.CandidateMenuCategoryQuestionType) //columns
    //        {
    //            DataColumn dcolCategory = new DataColumn(oCandidateMenuInList.CandidateMenuCategory.CategoryName + ":" + oQuestionTypeInList.QuestionTypeName + ":", typeof(System.Double));
    //            dt.Columns.Add(dcolCategory); 
    //        }
    //    }

    //    DataColumn dcolTotal = new DataColumn("Total:", typeof(System.Double));
    //    dt.Columns.Add(dcolTotal);

    //    for (i = 0; i < oCandidateResultsView.ListCandidates.Count; i++) //rows
    //    {
    //        //DataRow drow = dt.NewRow();

    //        ////Initialize the row data.
    //        //drow[NAME] = "Row-" + Convert.ToString((nIndex + 1));

    //        ////Add the row to the datatable.
    //        //dt.Rows.Add(drow);
    //        DataRow drow = dt.NewRow();
            
    //        drow["Name:"] = oCandidateResultsView.ListCandidates[i].CandidateName;

    //        float fTotalMarks = 0f;

    //        foreach (CandidateMenu oCandidateMenuInList in oCandidateResultsView.ListOfListCandidateResults[i]) 
    //        {
    //            foreach (QuestionType oQuestionTypeInList in oCandidateMenuInList.CandidateMenuCategoryQuestionType) //columns
    //            {
    //                drow[oCandidateMenuInList.CandidateMenuCategory.CategoryName + ":" + oQuestionTypeInList.QuestionTypeName + ":"] = oQuestionTypeInList.AVGQuestionTypeMark;
                    
    //                fTotalMarks = fTotalMarks + oQuestionTypeInList.AVGQuestionTypeMark;
    //            }
    //        }

    //        drow["Total:"] = fTotalMarks;

    //        dt.Rows.Add(drow);
    //    }
    //}
    protected void grid_Results_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void grid_Results_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grid_Results.PageIndex = e.NewPageIndex;

            grid_Results.DataSource = dt;
            grid_Results.DataBind();
        }
        catch (Exception oEx)
        { 
        
        }
    }
}
