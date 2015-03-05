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

public partial class Controls_SystemUserMainPage : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_error.Text = String.Empty;

        try
        {
            if (!IsPostBack)
            {
                Session.Remove(SessionManager.csSelectedExam);
                
                Utils.SetSessionByCategoryList();
                Utils.SetSessionByExamList();
                LoadExamfromSession();
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
    protected void btn_Go_Click(object sender, EventArgs e)
    {
        try
        {
            //if (!dr_SelectExam.Text.Equals("[Select One]") && (rdo_SelectConstraint.SelectedValue.Equals("Partial Marking") || rdo_SelectConstraint.SelectedValue.Equals("Negative Marking")))
            if (!dr_SelectExam.SelectedValue.Equals("[Select One]"))
            {
                int iSelectedExamIndex = dr_SelectExam.SelectedIndex - 1;
                Exam oExam = new Exam();
                List<Exam> oListExam = new List<Exam>();
                oListExam = (List<Exam>)Utils.GetSession(SessionManager.csStoredExam);
                oExam = oListExam[iSelectedExamIndex];
                //oExam.ExamConstraint = rdo_SelectConstraint.SelectedIndex + 1; //0=Full Marking,1=Partial Marking, 2=Negative Marking
                Utils.SetSession(SessionManager.csSelectedExam, oExam);

                Response.Redirect(ContainerManager.csMasterContainer + "?option=" + OptionManager.csQuestionSetup);
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
   
}
