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
    public class CandidateExamProcessBO
    {
        public CandidateExamProcessBO()
        { 
        
        }

        public Result SaveCandidateAnswers(CandidateForExam oCandidateForExam)
        {
            //new CLogger("Start SaveCandidateAnswers CandidateExamProcessBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start SaveCandidateAnswers CandidateExamProcessBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();

            CandidateExamProcessDAO oCandidateExamProcessDAO = new CandidateExamProcessDAO();

            try
            {
                oResult = oCandidateExamProcessDAO.SaveCandidateAnswers(oCandidateForExam);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception in Questions Save for Candidate..";
                oResult.ResultException = oEx;

                //new CLogger("Exception SaveCandidateAnswers CandidateExamProcessBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception SaveCandidateAnswers CandidateExamProcessBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out SaveCandidateAnswers CandidateExamProcessBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out SaveCandidateAnswers CandidateExamProcessBO+BO", ELogLevel.Debug);
            
            return oResult;
        }


        public Result LoadQuestionsForACandidateInExam(CandidateForExam oCandidateForExam)
        {
            //new CLogger("Start LoadQuestionsForACandidateInExam CandidateExamProcessBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start LoadQuestionsForACandidateInExam CandidateExamProcessBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();

            CandidateExamProcessDAO oCandidateExamProcessDAO = new CandidateExamProcessDAO();

            try
            {
                oResult = oCandidateExamProcessDAO.LoadQuestionsForACandidateInExam(oCandidateForExam);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception in Question Load For an Exam..";
                oResult.ResultException = oEx;

                //new CLogger("Exception LoadQuestionsForACandidateInExam CandidateExamProcessBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception LoadQuestionsForACandidateInExam CandidateExamProcessBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out LoadQuestionsForACandidateInExam CandidateExamProcessBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out LoadQuestionsForACandidateInExam CandidateExamProcessBO+BO", ELogLevel.Debug);

            return oResult;   
        }

        public Result LoadCategoriesWithType(CandidateForExam oCandidateForExam)
        {

            //new CLogger("Start LoadCategoriesWithType CandidateExamProcessBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start LoadCategoriesWithType CandidateExamProcessBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();

            CandidateExamProcessDAO oCandidateExamProcessDAO = new CandidateExamProcessDAO();

            try
            {
                oResult = oCandidateExamProcessDAO.LoadCategoriesWithType(oCandidateForExam);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception in Categories With Type Load For an Exam..";
                oResult.ResultException = oEx;

                //new CLogger("Exception LoadCategoriesWithType CandidateExamProcessBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception LoadCategoriesWithType CandidateExamProcessBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out LoadCategoriesWithType CandidateExamProcessBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out LoadCategoriesWithType CandidateExamProcessBO+BO", ELogLevel.Debug);

            return oResult;      
        }

        public Result LoadQuestionsForACandidateInExamByCategoryAndType(Category oCategory, QuestionType oQuestionType, CandidateForExam oCandidateForExam)
        {
            Result oResult = new Result();

            CandidateExamProcessDAO oCandidateExamProcessDAO = new CandidateExamProcessDAO();

            try
            {
                oResult = oCandidateExamProcessDAO.LoadQuestionsForACandidateInExamByCategoryAndType(oCategory, oQuestionType, oCandidateForExam);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception in Question Load by Category & Type For an Exam..";
                oResult.ResultException = oEx;
            }

            return oResult;   
        }
    }
}
