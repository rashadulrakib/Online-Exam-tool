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
    public class SystemUserBO
    {
        //private static readonly ILog logger = LogManager.GetLogger(typeof(CandidateDAO));
        
        public SystemUserBO()
        { 
        
        }

        public Result SystemUserLogin(SystemUser oSystemUser)
        {
            //new CLogger("Start SystemUserLogin SystemUserBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start SystemUserLogin SystemUserBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();
            
            try
            {
                SystemUserDAO oSystemUserDAO = new SystemUserDAO();
                
                oResult = oSystemUserDAO.SystemUserLogin(oSystemUser);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Login Exception..";

                //new CLogger("Exception SystemUserLogin SystemUserBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception SystemUserLogin SystemUserBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out SystemUserLogin SystemUserBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out SystemUserLogin SystemUserBO+BO", ELogLevel.Debug);

            return oResult;
        }

        public Result SystemUserEntry(SystemUser oSystemUser)
        {
            //new CLogger("Start SystemUserEntry SystemUserBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start SystemUserEntry SystemUserBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();

            try
            {
                SystemUserDAO oSystemUserDAO = new SystemUserDAO();
                oResult = oSystemUserDAO.SystemUserEntry(oSystemUser);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "System User Entry Exception..";

                //new CLogger("Exception SystemUserEntry SystemUserBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception SystemUserEntry SystemUserBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out SystemUserEntry SystemUserBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out SystemUserEntry SystemUserBO+BO", ELogLevel.Debug);

            return oResult;
        }

        public Result ShowAllSystemUsers()
        {
            //new CLogger("Start ShowAllSystemUsers SystemUserBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start ShowAllSystemUsers SystemUserBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();

            try
            {
                SystemUserDAO oSystemUserDAO = new SystemUserDAO();
                oResult = oSystemUserDAO.ShowAllSystemUsers();
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "System User Entry Exception..";

                //new CLogger("Exception ShowAllSystemUsers SystemUserBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception ShowAllSystemUsers SystemUserBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out ShowAllSystemUsers SystemUserBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out ShowAllSystemUsers SystemUserBO+BO", ELogLevel.Debug);

            return oResult;
        }

        public Result SystemUserDelete(List<SystemUser> oListSystemUser, int[] iArrCheck)
        {

            //new CLogger("Start SystemUserDelete SystemUserBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start SystemUserDelete SystemUserBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();

            try
            {
                SystemUserDAO oSystemUserDAO = new SystemUserDAO();
                oResult = oSystemUserDAO.SystemUserDelete(oListSystemUser, iArrCheck);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "System User Delete Exception..";

                //new CLogger("Exception SystemUserDelete SystemUserBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception SystemUserDelete SystemUserBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out SystemUserDelete SystemUserBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out SystemUserDelete SystemUserBO+BO", ELogLevel.Debug);

            return oResult;
        }

        public Result UpdateSystemUser(List<SystemUser> oListSystemUser, int[] iArrCheck)
        {
            //new CLogger("Start UpdateSystemUser SystemUserBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start UpdateSystemUser SystemUserBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();

            try
            {
                SystemUserDAO oSystemUserDAO = new SystemUserDAO();
                oResult = oSystemUserDAO.UpdateSystemUser(oListSystemUser, iArrCheck);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "System User Update Exception..";

                //new CLogger("Exception UpdateSystemUser SystemUserBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception UpdateSystemUser SystemUserBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out UpdateSystemUser SystemUserBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out UpdateSystemUser SystemUserBO+BO", ELogLevel.Debug);

            return oResult;   
        }

        public Result ChangePassword(SystemUser oSystemUser, String sNewPassword, String sConfirmPassword)
        {
            Result oResult = new Result();

            try
            {
                SystemUserDAO oSystemUserDAO = new SystemUserDAO();
                oResult = oSystemUserDAO.ChangePassword(oSystemUser, sNewPassword, sConfirmPassword);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "System User ChangePassword Exception..";
            }

            return oResult;
        }
    }
}
