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
using System.IO;
using Entity;
using Common;
using BO;

public partial class MasterPageCandidate : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CandidateForExam oCandidateForExam = (CandidateForExam)Utils.GetSession(SessionManager.csLoggedUser);

            //MainBody.Attributes.Add("onload", "ShowTime('"+oCandidate.CadidateCandidateExam.CandiadteExamExam.ExamDateWithStartingTime.AddHours(oCandidate.CadidateCandidateExam.CandiadteExamExam.ExamDurationinHour)+"')");

            DateTime oStrtTime = oCandidateForExam.CadidateCandidateExam.CandiadteExamExam.ExamDateWithStartingTime;
            float duration = oCandidateForExam.CadidateCandidateExam.CandiadteExamExam.ExamDurationinHour;
            DateTime oEndTime = oStrtTime.AddHours(duration);

            DateTime oCandidateStartTime = DateTime.Now;
            TimeSpan oRemainTimeSpan = oEndTime.Subtract(oCandidateStartTime);

            int RemainSeconds = (int)oRemainTimeSpan.TotalSeconds;

            MainBody.Attributes.Add("onload", "showTime(" + RemainSeconds + ")");
        }
        catch (Exception oEx)
        {
            Response.Redirect("CandidateLogin.aspx");
        }
    }
}
