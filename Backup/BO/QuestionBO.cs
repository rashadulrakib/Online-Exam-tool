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
    public class QuestionBO
    {
        public QuestionBO()
        { 
        
        }

        public Result QuestionEntry(Question oQuestion)
        {
            //new CLogger("Start QuestionEntry QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start QuestionEntry QuestionBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();
            QuestionDAO oQuestionDAO = new QuestionDAO();

            try
            {
                oResult = oQuestionDAO.QuestionEntry(oQuestion);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception occured during Question Entry..";
                oResult.ResultException = oEx;

                //new CLogger("Exception QuestionEntry QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception QuestionEntry QuestionBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out QuestionEntry QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out QuestionEntry QuestionBO+BO", ELogLevel.Debug);

            return oResult;
        }

        public Result QuestionListShow(Question oQuestion)
        {
            //new CLogger("Start QuestionListShow QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start QuestionListShow QuestionBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();
            QuestionDAO oQuestionDAO = new QuestionDAO();

            try
            {
                oResult = oQuestionDAO.QuestionListShow(oQuestion);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception occured during Question List Show..";
                oResult.ResultException = oEx;

                //new CLogger("Exception QuestionListShow QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception QuestionListShow QuestionBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out QuestionListShow QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out QuestionListShow QuestionBO+BO", ELogLevel.Debug);

            return oResult;
        }

        public Result QuestionListShowForSetup(Question oQuestion,Exam oExam)
        {
            //new CLogger("Start QuestionListShowForSetup QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start QuestionListShowForSetup QuestionBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();
            QuestionDAO oQuestionDAO = new QuestionDAO();

            try
            {
                oResult = oQuestionDAO.QuestionListShowForSetup(oQuestion,oExam);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception occured during QuestionListShowForSetup..";
                oResult.ResultException = oEx;

                //new CLogger("Exception QuestionListShowForSetup QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception QuestionListShowForSetup QuestionBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out QuestionListShowForSetup QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out QuestionListShowForSetup QuestionBO+BO", ELogLevel.Debug);

            return oResult;
        
        }

        public Result DeleteQuestionList(List<Question> oListQuestion, int[] iarrChecked)
        {

            //new CLogger("Start DeleteQuestionList QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start DeleteQuestionList QuestionBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();
            QuestionDAO oQuestionDAO = new QuestionDAO();

            try
            {
                oResult = oQuestionDAO.DeleteQuestionList(oListQuestion,iarrChecked);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception occured during Question Delete..";
                oResult.ResultException = oEx;

                //new CLogger("Exception DeleteQuestionList QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception DeleteQuestionList QuestionBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out DeleteQuestionList QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out DeleteQuestionList QuestionBO+BO", ELogLevel.Debug);

            return oResult;
        }
        public Result UpdateQuestion(Question oQuestion)
        {

            //new CLogger("Start UpdateQuestion QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start UpdateQuestion QuestionBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();
            QuestionDAO oQuestionDAO = new QuestionDAO();

            try
            {
                oResult = oQuestionDAO.UpdateQuestion(oQuestion);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception occured during Question Update..";
                oResult.ResultException = oEx;

                //new CLogger("Exception UpdateQuestion QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception UpdateQuestion QuestionBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out UpdateQuestion QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out UpdateQuestion QuestionBO+BO", ELogLevel.Debug);

            return oResult;
        }

        public Result QuestionSetup(List<QuestionSetup> oListQuestionSetup, int[] iarrChecked)
        {

            //new CLogger("Start QuestionSetup QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start QuestionSetup QuestionBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();
            QuestionDAO oQuestionDAO = new QuestionDAO();

            try
            {
                oResult = oQuestionDAO.QuestionSetup(oListQuestionSetup, iarrChecked);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception occured during Question Setup..";
                oResult.ResultException = oEx;

                //new CLogger("Exception QuestionSetup QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception QuestionSetup QuestionBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out QuestionSetup QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out QuestionSetup QuestionBO+BO", ELogLevel.Debug);

            return oResult;
        }

        public Result QuestionSetupRemove(List<QuestionSetup> oListQuestionSetup, int[] iarrChecked)
        {
            //new CLogger("Start QuestionSetupRemove QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start QuestionSetupRemove QuestionBO+BO", ELogLevel.Debug);
            
            
            Result oResult = new Result();
            QuestionDAO oQuestionDAO = new QuestionDAO();

            try
            {
                oResult = oQuestionDAO.QuestionSetupRemove(oListQuestionSetup, iarrChecked);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception occured during Question Setup Remove..";
                oResult.ResultException = oEx;

                //new CLogger("Exception QuestionSetupRemove QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception QuestionSetupRemove QuestionBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out QuestionSetupRemove QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out QuestionSetupRemove QuestionBO+BO", ELogLevel.Debug);

            return oResult;
        }

        public Result QuestionSetupListShow(QuestionSetup oQuestionSetup)
        {
            //new CLogger("Start QuestionSetupListShow QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start QuestionSetupListShow QuestionBO+BO", ELogLevel.Debug);
            
            
            Result oResult = new Result();
            QuestionDAO oQuestionDAO = new QuestionDAO();

            try
            {
                oResult = oQuestionDAO.QuestionSetupListShow(oQuestionSetup);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception occured during Question Setup List Show..";
                oResult.ResultException = oEx;

                //new CLogger("Exception QuestionSetupListShow QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception QuestionSetupListShow QuestionBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out QuestionSetupListShow QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out QuestionSetupListShow QuestionBO+BO", ELogLevel.Debug);

            return oResult;
        }

        public Result GetTotalLastSetupQuestionMark(Exam oExam)
        {

            //new CLogger("Start GetTotalLastSetupQuestionMark QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start GetTotalLastSetupQuestionMark QuestionBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();
            QuestionDAO oQuestionDAO = new QuestionDAO();

            try
            {
                oResult = oQuestionDAO.GetTotalLastSetupQuestionMark(oExam);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception occured during GetTotalLastSetupQuestionMark..";
                oResult.ResultException = oEx;

                //new CLogger("Exception GetTotalLastSetupQuestionMark QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception GetTotalLastSetupQuestionMark QuestionBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out GetTotalLastSetupQuestionMark QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out GetTotalLastSetupQuestionMark QuestionBO+BO", ELogLevel.Debug);

            return oResult;  
        }

        public Result GetTotalLastSetupQuestionTime(Exam oExam)
        {
            //new CLogger("Start GetTotalLastSetupQuestionTime QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start GetTotalLastSetupQuestionTime QuestionBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();
            QuestionDAO oQuestionDAO = new QuestionDAO();

            try
            {
                oResult = oQuestionDAO.GetTotalLastSetupQuestionTime(oExam);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception occured during GetTotalLastSetupQuestionTime..";
                oResult.ResultException = oEx;

                //new CLogger("Exception GetTotalLastSetupQuestionTime QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception GetTotalLastSetupQuestionTime QuestionBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out GetTotalLastSetupQuestionTime QuestionBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out GetTotalLastSetupQuestionTime QuestionBO+BO", ELogLevel.Debug);

            return oResult;  
        }

        public Result QuestionListShowForSetupByQuestionLevel(Question oQuestion, Exam oExam)
        {
            Result oResult = new Result();
            QuestionDAO oQuestionDAO = new QuestionDAO();
            
            try
            {
                oResult = oQuestionDAO.QuestionListShowForSetupByQuestionLevel(oQuestion, oExam);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception occured during QuestionListShowForSetupByQuestionLevel..";
                oResult.ResultException = oEx;
            }

            return oResult;
        }

        public Result QuestionSetupListShowByQuestionLevel(QuestionSetup oQuestionSetup)
        {
            Result oResult = new Result();
            QuestionDAO oQuestionDAO = new QuestionDAO();

            try
            {
                oResult = oQuestionDAO.QuestionSetupListShowByQuestionLevel(oQuestionSetup);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception occured during QuestionSetupListShowByQuestionLevel..";
                oResult.ResultException = oEx;
            }

            return oResult;
        }

        public Result LoadAllQuestionsOfAnExam(Exam oSelectedExam)
        {
            Result oResult = new Result();
            QuestionDAO oQuestionDAO = new QuestionDAO();

            try
            {
                oResult = oQuestionDAO.LoadAllQuestionsOfAnExam(oSelectedExam);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception occured during LoadAllQuestionsOfAnExam..";
                oResult.ResultException = oEx;
            }

            return oResult;
        }
    }
}
