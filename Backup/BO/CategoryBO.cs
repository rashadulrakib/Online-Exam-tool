using System;
using System.Collections.Generic;
using System.Text;
using Common;
using Entity;
using DAO;
//using Logging;
using Utility;

namespace BO
{
    public class CategoryBO
    {
        public CategoryBO()
        { 
        
        }

        public Result CategoryEntry(Category oCategory)
        {
            //new CLogger("Start CategoryEntry CategoryBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start CategoryEntry CategoryBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();

            try
            {
                CategoryDAO oCategoryDAO = new CategoryDAO();

                oResult = oCategoryDAO.CategoryEntry(oCategory);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Category Entry..";

                //new CLogger("Exception CategoryEntry CategoryBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception CategoryEntry CategoryBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out CategoryEntry CategoryBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out CategoryEntry CategoryBO+BO", ELogLevel.Debug);

            return oResult;
        }

        public Result CategoryGetFromDatabaseForSetSession()
        {

            //new CLogger("Start CategoryGetFromDatabaseForSetSession CategoryBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start CategoryGetFromDatabaseForSetSession CategoryBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();

            try
            {
                CategoryDAO oCategoryDAO = new CategoryDAO();
                oResult = oCategoryDAO.CategoryGetFromDatabaseForSetSession();
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Category Get..";

                //new CLogger("Exception CategoryGetFromDatabaseForSetSession CategoryBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception CategoryGetFromDatabaseForSetSession CategoryBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out CategoryGetFromDatabaseForSetSession CategoryBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out CategoryGetFromDatabaseForSetSession CategoryBO+BO", ELogLevel.Debug);

            return oResult;
        }


        public Result CategoryUpdate(List<Category> oListCategory, int[] iArrCheck)
        {

            //new CLogger("Start CategoryUpdate CategoryBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start CategoryUpdate CategoryBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();

            try
            {
                CategoryDAO oCategoryDAO = new CategoryDAO();
                oResult = oCategoryDAO.CategoryUpdate(oListCategory, iArrCheck);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Category Update..";

                //new CLogger("Exception CategoryUpdate CategoryBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception CategoryUpdate CategoryBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out CategoryUpdate CategoryBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out CategoryUpdate CategoryBO+BO", ELogLevel.Debug);

            return oResult;
        }

        public Result CategoryDelete(List<Category> oListCategory, int[] iArrCheck)
        {

            //new CLogger("Start CategoryDelete CategoryBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start CategoryDelete CategoryBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();

            try
            {
                CategoryDAO oCategoryDAO = new CategoryDAO();
                oResult = oCategoryDAO.CategoryDelete(oListCategory, iArrCheck);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Category Delete..";

                //new CLogger("Exception CategoryDelete CategoryBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception CategoryDelete CategoryBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out CategoryDelete CategoryBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out CategoryDelete CategoryBO+BO", ELogLevel.Debug);

            return oResult;
        }
    }
}
