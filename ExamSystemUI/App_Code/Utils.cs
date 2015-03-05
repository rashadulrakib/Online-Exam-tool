using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Common;
using BO;
using Entity;

/// <summary>
/// Summary description for Utils
/// </summary>
public class Utils
{

    public Utils()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    //public static bool csIsSystemUser = false;

    public static void SetSession(string sSessionName, object oValue)
    {
        HttpContext.Current.Session[sSessionName] = oValue;
    }

    public static object GetSession(string sSessionName)
    {
        if (HttpContext.Current.Session[sSessionName] != null)
            return HttpContext.Current.Session[sSessionName];
        else
            return null;
    }

    public static Result SetSessionByCategoryList()
    {
        CategoryBO oCategoryBO = new CategoryBO();
        Result oResult = new Result();

        List<Category> oListCategory = new List<Category>();

        try
        {
            oResult = oCategoryBO.CategoryGetFromDatabaseForSetSession();

            if (oResult.ResultIsSuccess)
            {
                oListCategory = (List<Category>)oResult.ResultObject;

                Utils.SetSession(SessionManager.csStoredCategory, oListCategory);
            }
        }
        catch (Exception oEx)
        {
            oResult.ResultIsSuccess = false;
            oResult.ResultException = oEx;
            oResult.ResultMessage = "Exception occured during Set Session By CategoryList.";
        }

        return oResult;
    }

    public static Result SetSessionByExamList()
    {
        ExamBO oExamBO = new ExamBO();
        Result oResult = new Result();

        List<Exam> oListExam = new List<Exam>();

        try
        {
            oResult = oExamBO.ExamGetFromDatabaseForSetSession();

            if (oResult.ResultIsSuccess)
            {
                oListExam = (List<Exam>)oResult.ResultObject;

                Utils.SetSession(SessionManager.csStoredExam, oListExam);
            }
        }
        catch (Exception oEx)
        {
            oResult.ResultIsSuccess = false;
            oResult.ResultException = oEx;
            oResult.ResultMessage = "Exception occured during Set Session By ExamList.";
        }

        return oResult;
    }

}
