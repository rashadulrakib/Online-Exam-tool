using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Common;
using Entity;
using Utility;
using log4net;
using log4net.Config;
//using Logging;


namespace DAO
{
    /// <summary>
    /// This class is used for Candidate exam process manipulation
    /// </summary>
    public class CandidateExamProcessDAO
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(CandidateExamProcessDAO));
        
        public CandidateExamProcessDAO()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// This Method save all answers of questions for an exam.
        /// This method is called when a candidate finish an exam
        /// </summary>
        /// <param name="oCandidateForExam"> It takes CandidateForExam Object </param>
        /// <returns> It returns Result Object </returns>
        public Result SaveCandidateAnswers(CandidateForExam oCandidateForExam)
        {
            //new CLogger("Start SaveCandidateAnswers CandidateExamProcessDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start SaveCandidateAnswers CandidateExamProcessDAO+DAO", ELogLevel.Debug);

            logger.Info("Start SaveCandidateAnswers CandidateExamProcessDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            String sInsert = String.Empty;
            String sDesOrObj = String.Empty;

            List<String> oListString = new List<String>();

            try
            {
                foreach (CandidateAnswerQuestion oCandidateAnswerQuestion in oCandidateForExam.CadidateCandidateExam.CandidateAnsweredQuestions)
                {
                    if (oCandidateAnswerQuestion.QuestionForCandidateAnswer.QuestionQuestionType.QuestionTypeID == 0)
                    {
                        sDesOrObj = "Objective@";

                        foreach (Choice oChoice in oCandidateAnswerQuestion.QuestionForCandidateAnswer.QuestionObjectiveType.ListOfChoices)
                        {
                            sDesOrObj = sDesOrObj + oChoice.ChoiceIsValid.ToString()+":";
                        }
                    }
                    else
                    {
                        sDesOrObj = "Descriptive@"+oCandidateAnswerQuestion.DescriptiveQuestionAnswerText+":";
                    }

                    sInsert = "insert into EX_CandidateExam(ExamID,QuestionID,CandidateID,AnswerStringOrBits,AnswerAttachmentPath) values('" + oCandidateForExam.CadidateCandidateExam.CandiadteExamExam.ExamID + "','" + oCandidateAnswerQuestion.QuestionForCandidateAnswer.QuestionID + "','" + oCandidateForExam.CandidateForExamCandidate.CandidateCompositeID + "','" + sDesOrObj + "','" + oCandidateAnswerQuestion.sAnswerAttachFilePath + "')";
                    oListString.Add(sInsert);
                }    

                if (oDAOUtil.ExecuteNonQuery(oListString))
                {
                    oResult.ResultIsSuccess = true;
                    oResult.ResultObject = oCandidateForExam;
                    oResult.ResultMessage = "Candidate Answer Save Success...";
                }
                else
                {
                    oResult.ResultIsSuccess = false;
                    oResult.ResultMessage = "Candidate Answer Save Failed...";
                }
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception in Candidate Answer Save...";
                oResult.ResultException = oEx;

                logger.Info("Exception SaveCandidateAnswers CandidateExamProcessDAO+DAO", oEx);

                //new CLogger("Exception SaveCandidateAnswers CandidateExamProcessDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception SaveCandidateAnswers CandidateExamProcessDAO+DAO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out SaveCandidateAnswers CandidateExamProcessDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out SaveCandidateAnswers CandidateExamProcessDAO+DAO", ELogLevel.Debug); ;

            logger.Info("End SaveCandidateAnswers CandidateExamProcessDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This Method Load Questions of an exam For A Candidate.
        /// </summary>
        /// <param name="oCandidateForExam"> It takes CandidateForExam Object </param>
        /// <returns> It returns Result Object </returns>
        public Result LoadQuestionsForACandidateInExam(CandidateForExam oCandidateForExam)
        {
            //new CLogger("Start LoadQuestionsForACandidateInExam CandidateExamProcessDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start LoadQuestionsForACandidateInExam CandidateExamProcessDAO+DAO", ELogLevel.Debug);

            logger.Info("Start LoadQuestionsForACandidateInExam CandidateExamProcessDAO+DAO");

            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            SqlDataReader oSqlDataReader = null;

            String sSelect = String.Empty;
            String sChoiceSelect = String.Empty;

            List<Question> oListQuestion = new List<Question>();
            List<CandidateAnswerQuestion> oListCandidateAnswerQuestion = new List<CandidateAnswerQuestion>();

            int i = 0;

            try
            {
                //sSelect = "select EX_Question.QuestionID,EX_Question.QuestionText,EX_Question.QuestionCreatorID,EX_Question.QuestionDefaultMark,EX_Question.QuestionCategoryID,EX_Question.QuestionTypeID from EX_QuestionGeneration inner join EX_Question on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID where EX_QuestionGeneration.ExamID='" + oCandidate.CadidateCandidateExam.CandiadteExamExam.ExamID + "' order by EX_Question.QuestionCategoryID, EX_Question.QuestionTypeID asc";
                
                //sSelect = "select EX_Question.QuestionID,EX_Question.QuestionText,EX_Question.QuestionCreatorID,EX_QuestionGeneration.SetupQuestionMark,EX_Question.QuestionCategoryID,EX_Question.QuestionTypeID"
                //+" from EX_QuestionGeneration inner join EX_Question on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID"
                //+" where EX_QuestionGeneration.ExamID='" + oCandidate.CadidateCandidateExam.CandiadteExamExam.ExamID + "' order by EX_Question.QuestionCategoryID, EX_Question.QuestionTypeID asc";

                sSelect = "select EX_Question.QuestionID,EX_Question.QuestionText,EX_Question.QuestionCreatorID,EX_QuestionGeneration.SetupQuestionMark,EX_Question.QuestionCategoryID,EX_Question.QuestionTypeID"
                +" from EX_QuestionGeneration inner join EX_Question on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID"
                +" where EX_QuestionGeneration.ExamID='" + oCandidateForExam.CadidateCandidateExam.CandiadteExamExam.ExamID + "'"
                +" order by EX_Question.QuestionCategoryID, EX_Question.QuestionTypeID asc";

                oSqlDataReader = oDAOUtil.GetReader(sSelect);

                while (oSqlDataReader.Read())
                {
                    Question oQuestion = new Question();

                    oQuestion.QuestionID = new Guid(oSqlDataReader["QuestionID"].ToString());
                    oQuestion.QuestionText = oSqlDataReader["QuestionText"].ToString();
                    oQuestion.QuestionCreator.SystemUserID = new Guid(oSqlDataReader["QuestionCreatorID"].ToString());
                    oQuestion.QuestionDefaultMark = float.Parse(oSqlDataReader["SetupQuestionMark"].ToString());
                    oQuestion.QuestionCategory.CategoryID = int.Parse(oSqlDataReader["QuestionCategoryID"].ToString());
                    oQuestion.QuestionQuestionType.QuestionTypeID = int.Parse(oSqlDataReader["QuestionTypeID"].ToString());

                    oListQuestion.Add(oQuestion);
                }

                oSqlDataReader.Close();

                foreach (Question oQuestionForChoice in oListQuestion)
                {
                    if (oQuestionForChoice.QuestionQuestionType.QuestionTypeID == 0)
                    {
                        sChoiceSelect = "select ObjectiveAnswer,ObjectiveAnswerIsValid from EX_Objective where ObjectiveQuestionID='" + oQuestionForChoice.QuestionID + "'";

                        List<Choice> oListChoice = new List<Choice>();

                        oSqlDataReader = oDAOUtil.GetReader(sChoiceSelect);

                        while (oSqlDataReader.Read())
                        {
                            //prepare the choices for a particular questtion......
                            //and populate oListQuestion
                            Choice oChoice = new Choice();

                            oChoice.ChoiceName = oSqlDataReader["ObjectiveAnswer"].ToString();
                            //oChoice.ChoiceIsValid = Convert.ToBoolean(oSqlDataReader["ObjectiveAnswerIsValid"].ToString());
                            oChoice.ChoiceIsValid = false;

                            oListChoice.Add(oChoice);
                        }

                        oSqlDataReader.Close();

                        oQuestionForChoice.QuestionObjectiveType.ListOfChoices = oListChoice;
                    }
                }


                for (i = 0; i < oListQuestion.Count; i++)
                {
                    CandidateAnswerQuestion oCandidateAnswerQuestion = new CandidateAnswerQuestion();
                    oCandidateAnswerQuestion.QuestionForCandidateAnswer = oListQuestion[i];
                    oListCandidateAnswerQuestion.Add(oCandidateAnswerQuestion);
                }

                oResult.ResultObject = oListCandidateAnswerQuestion;
                oResult.ResultMessage = "Load Question for Exam Success...";
                oResult.ResultIsSuccess = true;
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception in Question Load for Exam...";
                oResult.ResultException = oEx;

                logger.Info("Exception LoadQuestionsForACandidateInExam CandidateExamProcessDAO+DAO", oEx);

                //new CLogger("Exception LoadQuestionsForACandidateInExam CandidateExamProcessDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception LoadQuestionsForACandidateInExam CandidateExamProcessDAO+DAO", ELogLevel.Debug, oEx);
            }
            finally
            {
                if (oSqlDataReader!=null && !oSqlDataReader.IsClosed)
                {
                    oSqlDataReader.Close();
                }
            }

            //new CLogger("Out LoadQuestionsForACandidateInExam CandidateExamProcessDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out LoadQuestionsForACandidateInExam CandidateExamProcessDAO+DAO", ELogLevel.Debug); ;

            logger.Info("End LoadQuestionsForACandidateInExam CandidateExamProcessDAO+DAO");

            return oResult;    
        }

        /// <summary>
        /// This Method Load Question Categories With Type of an exam For A Candidate.
        /// For example: (Category: Math,IQ,English,etc...) (Only Two Type: Objective, Descriptive) 
        /// </summary>
        /// <param name="oCandidateForExam"> It takes CandidateForExam Object </param>
        /// <returns> It returns Result Object </returns>
        public Result LoadCategoriesWithType(CandidateForExam oCandidateForExam)
        {

            //new CLogger("Start LoadCategoriesWithType CandidateExamProcessDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start LoadCategoriesWithType CandidateExamProcessDAO+DAO", ELogLevel.Debug);

            logger.Info("Start LoadCategoriesWithType CandidateExamProcessDAO+DAO");

            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            SqlDataReader oSqlDataReader = null;

            String sSelect = String.Empty;

            List<CandidateMenu> oListCandidateMenu = new List<CandidateMenu>();

            try
            {
                sSelect = "select distinct EX_Category.CategoryID, EX_Category.CategoryName from"
                +" EX_Category inner join EX_Question on EX_Category.CategoryID=EX_Question.QuestionCategoryID"
                +" inner join EX_QuestionGeneration on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID"
                + " where EX_QuestionGeneration.ExamID='" + oCandidateForExam.CadidateCandidateExam.CandiadteExamExam.ExamID + "'"
                +" order by EX_Category.CategoryID asc";

                oSqlDataReader = oDAOUtil.GetReader(sSelect);

                while (oSqlDataReader.Read())
                {
                    CandidateMenu oCandidateMenu = new CandidateMenu();

                    oCandidateMenu.CandidateMenuCategory.CategoryID = int.Parse(oSqlDataReader["CategoryID"].ToString());
                    oCandidateMenu.CandidateMenuCategory.CategoryName = oSqlDataReader["CategoryName"].ToString();

                    oListCandidateMenu.Add(oCandidateMenu);
                }

                oSqlDataReader.Close();

                foreach (CandidateMenu oCandidateMenuInList in oListCandidateMenu)
                {
                    sSelect = "select distinct EX_QuestionType.TypeID, EX_QuestionType.TypeName"
                    +" from EX_QuestionType inner join EX_Question on EX_QuestionType.TypeID=EX_Question.QuestionTypeID"
                    + " inner join EX_QuestionGeneration on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID where EX_QuestionGeneration.ExamID='" + oCandidateForExam.CadidateCandidateExam.CandiadteExamExam.ExamID + "' and EX_Question.QuestionCategoryID ='" + oCandidateMenuInList.CandidateMenuCategory.CategoryID + "' order by EX_QuestionType.TypeID asc";

                    List<QuestionType> oListQuestionType = new List<QuestionType>();
                    
                    oSqlDataReader = oDAOUtil.GetReader(sSelect);

                    while (oSqlDataReader.Read())
                    {
                        QuestionType oQuestionType = new QuestionType();

                        oQuestionType.QuestionTypeID = int.Parse(oSqlDataReader["TypeID"].ToString());
                        oQuestionType.QuestionTypeName = oSqlDataReader["TypeName"].ToString();

                        oListQuestionType.Add(oQuestionType);
                    }

                    oSqlDataReader.Close();

                    foreach (QuestionType oQuestionTypeInList in oListQuestionType)
                    {
                        sSelect = "select count(EX_Question.QuestionID) as TotalQuestions from EX_Question inner join EX_QuestionGeneration on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID where EX_QuestionGeneration.ExamID='" + oCandidateForExam.CadidateCandidateExam.CandiadteExamExam.ExamID + "' and EX_Question.QuestionCategoryID ='" + oCandidateMenuInList.CandidateMenuCategory.CategoryID + "' and EX_Question.QuestionTypeID='" + oQuestionTypeInList.QuestionTypeID + "'";

                        oQuestionTypeInList.QuestionTypeTotalQuestions = int.Parse(oDAOUtil.GetExecuteScalar(sSelect).ToString());
                    }

                    oCandidateMenuInList.CandidateMenuCategoryQuestionType = oListQuestionType;
                }

                oResult.ResultObject = oListCandidateMenu;
                oResult.ResultMessage = "Candidate Menu Category with type Load Success...";
                oResult.ResultIsSuccess = true;
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception in Candidate Menu Category with type Load for Candidate...";
                oResult.ResultException = oEx;

                logger.Info("Exception LoadCategoriesWithType CandidateExamProcessDAO+DAO", oEx);

                //new CLogger("Exception LoadCategoriesWithType CandidateExamProcessDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception LoadCategoriesWithType CandidateExamProcessDAO+DAO", ELogLevel.Debug, oEx);
            }
            finally
            {
                if (oSqlDataReader!=null && !oSqlDataReader.IsClosed)
                {
                    oSqlDataReader.Close();
                }
            }

            //new CLogger("Out LoadCategoriesWithType CandidateExamProcessDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out LoadCategoriesWithType CandidateExamProcessDAO+DAO", ELogLevel.Debug); ;

            logger.Info("End LoadCategoriesWithType CandidateExamProcessDAO+DAO");

            return oResult;
        }


        /// <summary>
        /// This Method Load Questions according to category, type for an exam.
        /// </summary>
        /// <param name="oCategory"> It takes Category Object </param>
        /// <param name="oQuestionType"> It takes QuestionType Object </param>
        /// <param name="oCandidateForExam"> It takes CandidateForExam Object </param>
        /// <returns> It returns Result Object </returns>
        public Result LoadQuestionsForACandidateInExamByCategoryAndType(Category oCategory, QuestionType oQuestionType, CandidateForExam oCandidateForExam)
        {
            logger.Info("Start LoadQuestionsForACandidateInExamByCategoryAndType CandidateExamProcessDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            SqlDataReader oSqlDataReader = null;

            String sSelect = String.Empty;
            String sChoiceSelect = String.Empty;

            List<Question> oListQuestion = new List<Question>();
            List<CandidateAnswerQuestion> oListCandidateAnswerQuestion = new List<CandidateAnswerQuestion>();

            int i = 0;

            try
            {
                //sSelect = "select EX_Question.QuestionID,EX_Question.QuestionText,EX_Question.QuestionCreatorID,EX_Question.QuestionDefaultMark,EX_Question.QuestionCategoryID,EX_Question.QuestionTypeID from EX_QuestionGeneration inner join EX_Question on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID where EX_QuestionGeneration.ExamID='" + oCandidate.CadidateCandidateExam.CandiadteExamExam.ExamID + "' order by EX_Question.QuestionCategoryID, EX_Question.QuestionTypeID asc";

                //sSelect = "select EX_Question.QuestionID,EX_Question.QuestionText,EX_Question.QuestionCreatorID,EX_QuestionGeneration.SetupQuestionMark,EX_Question.QuestionCategoryID,EX_Question.QuestionTypeID"
                //+" from EX_QuestionGeneration inner join EX_Question on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID"
                //+" where EX_QuestionGeneration.ExamID='" + oCandidate.CadidateCandidateExam.CandiadteExamExam.ExamID + "' order by EX_Question.QuestionCategoryID, EX_Question.QuestionTypeID asc";

                sSelect = "select EX_Question.QuestionID,EX_Question.QuestionText,"
                +" EX_Question.QuestionCreatorID,EX_QuestionGeneration.SetupQuestionMark"
                +" ,EX_Question.QuestionCategoryID,EX_Question.QuestionTypeID"
                +" from EX_QuestionGeneration inner join EX_Question on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID"
                +" where EX_QuestionGeneration.ExamID='" + oCandidateForExam.CadidateCandidateExam.CandiadteExamExam.ExamID + "'"
                +" and EX_Question.QuestionCategoryID='" + oCategory.CategoryID + "'"
                +" and EX_Question.QuestionTypeID='" + oQuestionType.QuestionTypeID + "'";
                

                oSqlDataReader = oDAOUtil.GetReader(sSelect);

                while (oSqlDataReader.Read())
                {
                    Question oQuestion = new Question();

                    oQuestion.QuestionID = new Guid(oSqlDataReader["QuestionID"].ToString());
                    oQuestion.QuestionText = oSqlDataReader["QuestionText"].ToString();
                    oQuestion.QuestionCreator.SystemUserID = new Guid(oSqlDataReader["QuestionCreatorID"].ToString());
                    oQuestion.QuestionDefaultMark = float.Parse(oSqlDataReader["SetupQuestionMark"].ToString());
                    oQuestion.QuestionCategory.CategoryID = int.Parse(oSqlDataReader["QuestionCategoryID"].ToString());
                    oQuestion.QuestionQuestionType.QuestionTypeID = int.Parse(oSqlDataReader["QuestionTypeID"].ToString());

                    oListQuestion.Add(oQuestion);
                }

                oSqlDataReader.Close();

                foreach (Question oQuestionForChoice in oListQuestion)
                {
                    if (oQuestionForChoice.QuestionQuestionType.QuestionTypeID == 0)
                    {
                        sChoiceSelect = "select ObjectiveAnswer,ObjectiveAnswerIsValid from EX_Objective where ObjectiveQuestionID='" + oQuestionForChoice.QuestionID + "'";

                        List<Choice> oListChoice = new List<Choice>();

                        oSqlDataReader = oDAOUtil.GetReader(sChoiceSelect);

                        while (oSqlDataReader.Read())
                        {
                            //prepare the choices for a particular questtion......
                            //and populate oListQuestion
                            Choice oChoice = new Choice();

                            oChoice.ChoiceName = oSqlDataReader["ObjectiveAnswer"].ToString();
                            //oChoice.ChoiceIsValid = Convert.ToBoolean(oSqlDataReader["ObjectiveAnswerIsValid"].ToString());
                            oChoice.ChoiceIsValid = false;

                            oListChoice.Add(oChoice);
                        }

                        oSqlDataReader.Close();

                        oQuestionForChoice.QuestionObjectiveType.ListOfChoices = oListChoice;
                    }
                }


                for (i = 0; i < oListQuestion.Count; i++)
                {
                    CandidateAnswerQuestion oCandidateAnswerQuestion = new CandidateAnswerQuestion();
                    oCandidateAnswerQuestion.QuestionForCandidateAnswer = oListQuestion[i];
                    oListCandidateAnswerQuestion.Add(oCandidateAnswerQuestion);
                }

                oResult.ResultObject = oListCandidateAnswerQuestion;
                oResult.ResultMessage = "Load Question for Exam Success...";
                oResult.ResultIsSuccess = true;
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception in Question Load for Exam...";
                oResult.ResultException = oEx;

                logger.Info("Exception LoadQuestionsForACandidateInExamByCategoryAndType CandidateExamProcessDAO+DAO", oEx);
            }
            finally
            {
                if (oSqlDataReader != null && !oSqlDataReader.IsClosed)
                {
                    oSqlDataReader.Close();
                }
            }

            logger.Info("End LoadQuestionsForACandidateInExamByCategoryAndType CandidateExamProcessDAO+DAO");

            return oResult;    
        }
    }
}
