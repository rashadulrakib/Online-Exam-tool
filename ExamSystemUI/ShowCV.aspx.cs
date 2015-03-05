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

public partial class ShowCV : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            String CVPath = HttpContext.Current.Request.QueryString.Get("CVPath");

            CVPath = CVPath.Replace('-', ' ');

            //byte[] bArr = File.ReadAllBytes(DirectoryManager.csCandidateCVDirectory + CVPath);

            if (CVPath.ToLower().LastIndexOf(".pdf") > 0)
            {
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=test.pdf");
            }
            else
            {
                HttpContext.Current.Response.ContentType = "application/unknown";

                if (CVPath.ToLower().LastIndexOf(".htm") > 0)
                {
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=test.htm");
                }
                if (CVPath.ToLower().LastIndexOf(".html") > 0)
                {
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=test.html");
                }
                if (CVPath.ToLower().LastIndexOf(".doc") > 0)
                {
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=test.doc");
                }
                if (CVPath.ToLower().LastIndexOf(".rtf") > 0)
                {
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=test.rtf");
                }
               
            }

            
            

            HttpContext.Current.Response.WriteFile(DirectoryManager.csCandidateCVDirectory + CVPath);
            
            //HttpContext.Current.Response.Write(File.ReadAllText(DirectoryManager.csCandidateCVDirectory + CVPath, System.Text.Encoding.Default));

            HttpContext.Current.Response.End();
        }
        catch (Exception oEx)
        { 
        
        }
    }
}
