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
    public class ExamBO
    {
        public ExamBO()
        { 
        
        }

        public Result ExamEntry(Exam oExam)
        {
            //new CLogger("Start ExamEntry ExamBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start ExamEntry ExamBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();

            try
            {
                ExamDAO oExamDAO = new ExamDAO();

                oResult = oExamDAO.ExamEntry(oExam);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Exam Entry..";

                //new CLogger("Exception ExamEntry ExamBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception ExamEntry ExamBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out ExamEntry ExamBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out ExamEntry ExamBO+BO", ELogLevel.Debug);

            return oResult;
        }
        public Result ExamGetFromDatabaseForSetSession()
        {
            //new CLogger("Start ExamGetFromDatabaseForSetSession ExamBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start ExamGetFromDatabaseForSetSession ExamBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();

            try
            {
                ExamDAO oExamDAO = new ExamDAO();

                oResult = oExamDAO.ExamGetFromDatabaseForSetSession();
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Exam Get..";

                //new CLogger("Exception ExamGetFromDatabaseForSetSession ExamBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception ExamGetFromDatabaseForSetSession ExamBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out ExamGetFromDatabaseForSetSession ExamBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out ExamGetFromDatabaseForSetSession ExamBO+BO", ELogLevel.Debug);

            return oResult;
        }
        public Result ExamModification(Exam oExam)
        {

            //new CLogger("Start ExamModification ExamBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start ExamModification ExamBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();

            try
            {
                ExamDAO oExamDAO = new ExamDAO();

                oResult = oExamDAO.ExamModification(oExam);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Exam Update..";

                //new CLogger("Exception ExamModification ExamBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception ExamModification ExamBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out ExamModification ExamBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out ExamModification ExamBO+BO", ELogLevel.Debug);

            return oResult;
        }
        public Result ExamDelte(Exam oExam)
        {
            //new CLogger("Start ExamDelte ExamBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start ExamDelte ExamBO+BO", ELogLevel.Debug);
            
            
            Result oResult = new Result();

            try
            {
                ExamDAO oExamDAO = new ExamDAO();

                oResult = oExamDAO.ExamDelte(oExam);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Exam Delete..";

                //new CLogger("Exception ExamDelte ExamBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception ExamDelte ExamBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out ExamDelte ExamBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out ExamDelte ExamBO+BO", ELogLevel.Debug);

            return oResult;
        
        }

        public Result ExamDeleteByStoredProcedure(Exam oExam, SystemUser oSystemUser) //r
        {
            Result oResult = new Result();

            try
            {
                ExamDAO oExamDAO = new ExamDAO();

                oResult = oExamDAO.ExamDeleteByStoredProcedure(oExam, oSystemUser);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Exam Delete..";
            }

            return oResult;
        }
    
    }
}
