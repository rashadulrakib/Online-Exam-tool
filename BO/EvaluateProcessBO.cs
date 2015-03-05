using System;
using System.Collections.Generic;
using System.Text;
using Common;
using Entity;
using DAO;
//using Logging;
using Utility;
using log4net;
using log4net.Config;

namespace BO
{
    public class EvaluateProcessBO
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(EvaluateProcessBO));
        
        public EvaluateProcessBO()
        {
            log4net.Config.XmlConfigurator.Configure();
        }



        public Result LoadCandidatesAccordingToSystemUserForEvaluate(SystemUser oSystemUser, Exam oExam)
        {
            //new CLogger("Start LoadCandidatesAccordingToSystemUserForEvaluate EvaluateProcessBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start LoadCandidatesAccordingToSystemUserForEvaluate EvaluateProcessBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();
            EvaluateProcessDAO oEvaluateProcessDAO = new EvaluateProcessDAO();

            try
            {
                oResult = oEvaluateProcessDAO.LoadCandidatesAccordingToSystemUserForEvaluate(oSystemUser, oExam);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "LoadCandidatesAccordingToSystemUserForEvaluate Exception..";

                //new CLogger("Exception LoadCandidatesAccordingToSystemUserForEvaluate EvaluateProcessBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception LoadCandidatesAccordingToSystemUserForEvaluate EvaluateProcessBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out LoadCandidatesAccordingToSystemUserForEvaluate EvaluateProcessBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out LoadCandidatesAccordingToSystemUserForEvaluate EvaluateProcessBO+BO", ELogLevel.Debug);

            return oResult;
        }

        public Result LoadQuestionsForACandidateWhichSetupByAParticularUser(string sCandidateID, Exam oExam, SystemUser oSystemUser, Boolean flagForEvaluation)
        {
            //new CLogger("Start LoadQuestionsForACandidateWhichSetupByAParticularUser EvaluateProcessBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start LoadQuestionsForACandidateWhichSetupByAParticularUser EvaluateProcessBO+BO", ELogLevel.Debug);
            
            
            Result oResult = new Result();
            EvaluateProcessDAO oEvaluateProcessDAO = new EvaluateProcessDAO();

            try
            {
                oResult = oEvaluateProcessDAO.LoadQuestionsForACandidateWhichSetupByAParticularUser(sCandidateID, oExam, oSystemUser,flagForEvaluation);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "LoadQuestionsForACandidateWhichSetupByAParticularUser Exception..";

                //new CLogger("Exception LoadQuestionsForACandidateWhichSetupByAParticularUser EvaluateProcessBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception LoadQuestionsForACandidateWhichSetupByAParticularUser EvaluateProcessBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out LoadQuestionsForACandidateWhichSetupByAParticularUser EvaluateProcessBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out LoadQuestionsForACandidateWhichSetupByAParticularUser EvaluateProcessBO+BO", ELogLevel.Debug);

            return oResult;
        }

        //public Result EvaluateObjectiveAnswersBeforeShow(List<CandidateAnswerQuestion> oListCandidateAnswerQuestion, Exam oExam, String sCandidateID)
        //{
        //    Result oResult = new Result();

        //    try
        //    {
        //        foreach (CandidateAnswerQuestion oCandidateAnswerQuestionInList in oListCandidateAnswerQuestion)
        //        {
                    
        //        }
        //    }
        //    catch (Exception oEx)
        //    {
        //        oResult.ResultIsSuccess = false;
        //        oResult.ResultException = oEx;
        //        oResult.ResultMessage = "EvaluateObjectiveAnswersBeforeShow Exception..";
        //    }

        //    return oResult;
        //}


        /// <summary>
        /// This Method Evaluates ObjectiveAnswers For All Candidate Of An Exam
        /// It evaluates the objective answers according to exam constraint
        /// </summary>
        /// <param name="oListCandidateForExamForGrid"> It takes List<SqlCommand> </param>
        /// <param name="oSystemUser"> It takes SystemUser Object </param>
        /// <param name="oExam"> It takes Exam Object </param>
        /// <returns> return Result Object </returns>
        
        public Result EvaluateObjectiveAnswersForAllCandidateOfAnExma(List<CandidateForExam> oListCandidateForExamForGrid, SystemUser oSystemUser, Exam oExam)
        {
            //new CLogger("Start EvaluateObjectiveAnswersForAllCandidateOfAnExma EvaluateProcessBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start EvaluateObjectiveAnswersForAllCandidateOfAnExma EvaluateProcessBO+BO", ELogLevel.Debug);

            logger.Info("Start EvaluateObjectiveAnswersForAllCandidateOfAnExma EvaluateProcessBO+BO");
            
            Result oResult = new Result();
            EvaluateProcessDAO oEvaluateProcessDAO = new EvaluateProcessDAO();

            List<CandidateForExam> oListCandidateForExamForEvaluate = new List<CandidateForExam>();

            int i=0;

            try
            {
                oResult = oEvaluateProcessDAO.EvaluateObjectiveAnswersForAllCandidateOfAnExma(oListCandidateForExamForGrid, oSystemUser, oExam);

                if (oResult.ResultIsSuccess)
                {
                    oListCandidateForExamForEvaluate = (List<CandidateForExam>)oResult.ResultObject;

                    foreach (CandidateForExam oCandidateForExamInList in oListCandidateForExamForEvaluate)
                    {
                        foreach (CandidateAnswerQuestion oCandidateAnswerQuestionInList in oCandidateForExamInList.CadidateCandidateExam.CandidateAnsweredQuestions)
                        {
                            if (oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionQuestionType.QuestionTypeID == 0)
                            { 
                                if(oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionObjectiveType.ListOfAnswers.Count == oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionObjectiveType.ListOfChoices.Count)
                                {
                                    if (oExam.ExamConstraint == 0 || oExam.ExamConstraint == 2) //full, negative
                                    {
                                        Boolean bMatch = true;

                                        for (i = 0; i < oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionObjectiveType.ListOfAnswers.Count; i++)
                                        {
                                            if (oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionObjectiveType.ListOfAnswers[i].ChoiceIsValid == oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionObjectiveType.ListOfChoices[i].ChoiceIsValid)
                                            {

                                            }
                                            else
                                            {
                                                bMatch = false;
                                                break;
                                            }
                                        }

                                        if (bMatch)
                                        {
                                            oCandidateAnswerQuestionInList.ObtainMark = oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionDefaultMark;
                                        }
                                        else
                                        {
                                            if (oExam.ExamConstraint == 0)
                                            {
                                                oCandidateAnswerQuestionInList.ObtainMark = 0f;
                                            }
                                            else if (oExam.ExamConstraint == 2)
                                            {
                                                oCandidateAnswerQuestionInList.ObtainMark = (-1f) * (oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionDefaultMark);
                                            }
                                        }
                                    }
                                    else if(oExam.ExamConstraint == 1) //partial
                                    {
                                        int iMatchCount = 0;
                                        int iTotalValid = 0;

                                        for (i = 0; i < oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionObjectiveType.ListOfAnswers.Count; i++)
                                        {
                                            if (oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionObjectiveType.ListOfAnswers[i].ChoiceIsValid==true &&  oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionObjectiveType.ListOfChoices[i].ChoiceIsValid==true)
                                            {
                                                iMatchCount = iMatchCount + 1;
                                            }
                                            if (oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionObjectiveType.ListOfChoices[i].ChoiceIsValid == true)
                                            {
                                                iTotalValid = iTotalValid + 1;
                                            }
                                        }

                                        if (iTotalValid > 0)
                                        {
                                            oCandidateAnswerQuestionInList.ObtainMark = ((float)iMatchCount / (float)iTotalValid) * (oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionDefaultMark);
                                        }
                                        //else
                                        //{
                                        //    if (iTotalValid == 0 && iMatchCount==0)
                                        //    {
                                        //        oCandidateAnswerQuestionInList.ObtainMark = (oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionDefaultMark);
                                        //    }
                                        //    else if (iTotalValid == 0 && iMatchCount>0)
                                        //    {
                                        //        oCandidateAnswerQuestionInList.ObtainMark = 0f;
                                        //    }
                                        //}
                                    }
                                    else if (oExam.ExamConstraint == 3) //partial negative
                                    {
                                        int iMatchCount = 0;
                                        int iTotalValid = 0;
                                        int iNotMatchCount = 0;

                                        for (i = 0; i < oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionObjectiveType.ListOfAnswers.Count; i++)
                                        {
                                            if (oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionObjectiveType.ListOfAnswers[i].ChoiceIsValid == true && oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionObjectiveType.ListOfChoices[i].ChoiceIsValid == true)
                                            {
                                                iMatchCount = iMatchCount + 1;
                                            }
                                            else if (oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionObjectiveType.ListOfAnswers[i].ChoiceIsValid == true && oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionObjectiveType.ListOfChoices[i].ChoiceIsValid == false)
                                            {
                                                iNotMatchCount = iNotMatchCount + 1;
                                            }

                                            if (oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionObjectiveType.ListOfChoices[i].ChoiceIsValid == true)
                                            {
                                                iTotalValid = iTotalValid + 1;
                                            }
                                        }

                                        if (iTotalValid > 0)
                                        {
                                            oCandidateAnswerQuestionInList.ObtainMark = ((float)iMatchCount / (float)iTotalValid) * (oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionDefaultMark) - (float)iNotMatchCount * (1f / (float)iTotalValid);
                                        }
                                        //else
                                        //{
                                        //    if (iTotalValid == 0 && iMatchCount == 0)
                                        //    {
                                        //        oCandidateAnswerQuestionInList.ObtainMark = (oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionDefaultMark);
                                        //    }
                                        //    else if (iTotalValid == 0 && iMatchCount > 0)
                                        //    {
                                        //        oCandidateAnswerQuestionInList.ObtainMark = 0f;
                                        //    }
                                        //}
                                    }
                                }
                            }
                        }        
                    }

                    oResult = oEvaluateProcessDAO.SaveObjectiveAnswerMarksForAllCandidateOfAnExam(oListCandidateForExamForEvaluate, oSystemUser, oExam);
                }
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "EvaluateObjectiveAnswersForAllCandidateOfAnExma Exception.." + oEx.ToString();

                logger.Info("Exception EvaluateObjectiveAnswersForAllCandidateOfAnExma EvaluateProcessBO+BO", oEx);

                //new CLogger("Exception EvaluateObjectiveAnswersForAllCandidateOfAnExma EvaluateProcessBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception EvaluateObjectiveAnswersForAllCandidateOfAnExma EvaluateProcessBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out EvaluateObjectiveAnswersForAllCandidateOfAnExma EvaluateProcessBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out EvaluateObjectiveAnswersForAllCandidateOfAnExma EvaluateProcessBO+BO", ELogLevel.Debug);

            logger.Info("End EvaluateObjectiveAnswersForAllCandidateOfAnExma EvaluateProcessBO+BO");

            return oResult;
        }

        public Result SaveDescriptiveAnswerMarksForCandidatesForASystemUserOfAnExam(String sCandidateID, List<CandidateAnswerQuestion> oListCandidateAnswerQuestion, Exam oExam, SystemUser oSystemUser)
        {

            //new CLogger("Start SaveDescriptiveAnswerMarksForCandidatesForASystemUserOfAnExam EvaluateProcessBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start SaveDescriptiveAnswerMarksForCandidatesForASystemUserOfAnExam EvaluateProcessBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();
            EvaluateProcessDAO oEvaluateProcessDAO = new EvaluateProcessDAO();

            try
            {
                oResult = oEvaluateProcessDAO.SaveDescriptiveAnswerMarksForCandidatesForASystemUserOfAnExam(sCandidateID,oListCandidateAnswerQuestion, oExam, oSystemUser);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "SaveDescriptiveAnswerMarksForCandidatesForASystemUserOfAnExam Exception..";

                //new CLogger("Exception SaveDescriptiveAnswerMarksForCandidatesForASystemUserOfAnExam EvaluateProcessBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception SaveDescriptiveAnswerMarksForCandidatesForASystemUserOfAnExam EvaluateProcessBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out SaveDescriptiveAnswerMarksForCandidatesForASystemUserOfAnExam EvaluateProcessBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out SaveDescriptiveAnswerMarksForCandidatesForASystemUserOfAnExam EvaluateProcessBO+BO", ELogLevel.Debug);

            return oResult;
        }
    }
}
