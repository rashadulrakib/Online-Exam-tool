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
using System.IO;

public partial class AnswerFileShow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            String sWithLastSlash = HttpContext.Current.Request.QueryString.Get("WithLastSlash").Replace('-', ' ');
            String sQuestionIDWithUnderScode = HttpContext.Current.Request.QueryString.Get("QuestionIDWithUnderScode");
            String sOrginalAnswerFileName = HttpContext.Current.Request.QueryString.Get("OrginalAnswerFileName").Replace('-', ' ');

            HttpContext.Current.Response.ContentType = "application/unknown";

            if (sOrginalAnswerFileName.ToLower().LastIndexOf(".doc") > 0)
            {
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=Answer.doc");  
            }
            else if (sOrginalAnswerFileName.ToLower().LastIndexOf(".vsd") > 0)
            {
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=Answer.vsd");  
            }
            

            HttpContext.Current.Response.WriteFile(DirectoryManager.csCandidateCVDirectory + sWithLastSlash + sQuestionIDWithUnderScode + sOrginalAnswerFileName);

            //HttpContext.Current.Response.Write(File.ReadAllText(DirectoryManager.csCandidateCVDirectory + CVPath, System.Text.Encoding.Default));

            HttpContext.Current.Response.End();
        }
        catch (Exception oEx)
        { 
        
        }
    }
}
