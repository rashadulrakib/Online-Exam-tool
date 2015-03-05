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
using Entity;

public partial class MasterContainerCandidate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Utils.GetSession(SessionManager.csLoggedUser) == null)
            {
                Response.Redirect("CandidateLogin.aspx");
            }
            else
            { 
                LoadControlInContainer();
            }
        }
        catch (Exception oEx)
        {
            Response.Redirect("CandidateLogin.aspx");
        }
    }

    private void LoadControlInContainer()
    {
        String sOption = HttpContext.Current.Request.QueryString.Get("option");

        //if(!Utils.csIsSystemUser)

        try
        {
            if (((CandidateForExam)Utils.GetSession(SessionManager.csLoggedUser)).CandidateForExamCandidate.GetType().Name.Equals(TypeManager.csCandidate))
            {
                if (sOption.Equals(OptionManager.csExamProcess))
                {
                    Utils.SetSession(SessionManager.csLoadedPage, ControlManager.csExamProcess);
                    plh_ControlCandidate.Controls.Add(LoadControl("Controls/" + ControlManager.csExamProcess));
                }

            }
        }
        catch (Exception oEx)
        {
            Response.Redirect("CandidateLogin.aspx");
        }
    }
}
