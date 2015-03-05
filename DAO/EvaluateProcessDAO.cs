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
    /// This class is used for Candidate Evaluation
    /// </summary>
    public class EvaluateProcessDAO
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(EvaluateProcessDAO));
        
        public EvaluateProcessDAO()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// This method Load Candidates According to SystemUser to evaluate candidates
        /// That means, if a system user setup questions for an exam then he can evaluate candidates 
        /// Administrator can always evaluate candidates.
        /// </summary>
        /// <param name="oSystemUser"> It takes SystemUser Object </param>
        /// <param name="oExam"> It takes Exam Object </param>
        /// <returns> It returns Result Object </returns>
        public Result LoadCandidatesAccordingToSystemUserForEvaluate(SystemUser oSystemUser, Exam oExam)
        {

            //new CLogger("Start LoadCandidatesAccordingToSystemUserForEvaluate EvaluateProcessDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start LoadCandidatesAccordingToSystemUserForEvaluate EvaluateProcessDAO+DAO", ELogLevel.Debug);

            logger.Info("Start LoadCandidatesAccordingToSystemUserForEvaluate EvaluateProcessDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            SqlDataReader oSqlDataReader = null;

            List<CandidateForExam> oListCandidateForExam = new List<CandidateForExam>();

            String sSelect = String.Empty;

            try
            {
                if (oSystemUser.SystemUserName.ToLower().Equals("administrator"))
                {
                    //sSelect = "select distinct EX_Candidate.ExamID,EX_Candidate.CompositeCandidateID,EX_Candidate.CandidatePassword,EX_Candidate.Name,EX_Candidate.LastResult,EX_Candidate.LastInstitution,EX_Candidate.LastPassingYear,CvPath from EX_Candidate inner join EX_CandidateExam on EX_Candidate.CompositeCandidateID = EX_CandidateExam.CandidateID where EX_Candidate.ExamID='" + oExam.ExamID + "' and EX_Candidate.ExamID in (select EX_QuestionGeneration.ExamID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='" + oExam.ExamID + "')";
                    sSelect = "select distinct EX_CandidateForExam.ExamID,EX_Candidate.CompositeCandidateID,EX_Candidate.CandidatePassword,EX_Candidate.Name,EX_Candidate.LastResult,EX_Candidate.LastInstitution,EX_Candidate.LastPassingYear,EX_Candidate.CvPath"
                    + " from EX_Candidate inner join EX_CandidateForExam on EX_Candidate.CompositeCandidateID=EX_CandidateForExam.CandidateID"
                    + " inner join EX_CandidateExam on EX_CandidateForExam.ExamID=EX_CandidateExam.ExamID where EX_CandidateExam.ExamID='" + oExam.ExamID + "'"
                    + " and EX_CandidateForExam.CandidateID=EX_CandidateExam.CandidateID"
                    + " and EX_CandidateExam.ExamID in (select EX_QuestionGeneration.ExamID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='" + oExam.ExamID + "')";
                }
                else
                {
                    //sSelect = "select distinct EX_Candidate.ExamID,EX_Candidate.CompositeCandidateID,EX_Candidate.CandidatePassword,EX_Candidate.Name,EX_Candidate.LastResult,EX_Candidate.LastInstitution,EX_Candidate.LastPassingYear,CvPath from EX_Candidate inner join EX_CandidateExam on EX_Candidate.CompositeCandidateID = EX_CandidateExam.CandidateID where EX_Candidate.ExamID='" + oExam.ExamID + "' and EX_Candidate.ExamID in (select EX_QuestionGeneration.ExamID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='" + oExam.ExamID + "' and EX_QuestionGeneration.GeneratorID='" + oSystemUser.SystemUserID + "')";
                    sSelect = "select distinct EX_CandidateForExam.ExamID,EX_Candidate.CompositeCandidateID,EX_Candidate.CandidatePassword,EX_Candidate.Name,EX_Candidate.LastResult,EX_Candidate.LastInstitution,EX_Candidate.LastPassingYear,EX_Candidate.CvPath"
                    + " from EX_Candidate inner join EX_CandidateForExam on EX_Candidate.CompositeCandidateID=EX_CandidateForExam.CandidateID"
                    + " inner join EX_CandidateExam on EX_CandidateForExam.ExamID=EX_CandidateExam.ExamID where EX_CandidateExam.ExamID='" + oExam.ExamID + "'"
                    + " and EX_CandidateForExam.CandidateID=EX_CandidateExam.CandidateID"
                    + " and EX_CandidateExam.ExamID in (select EX_QuestionGeneration.ExamID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='" + oExam.ExamID + "' and EX_QuestionGeneration.GeneratorID='" + oSystemUser.SystemUserID + "')";
                }

                oSqlDataReader = oDAOUtil.GetReader(sSelect);

                while(oSqlDataReader.Read())
                {
                    CandidateForExam oCandidateForExam = new CandidateForExam();

                    oCandidateForExam.CadidateCandidateExam.CandiadteExamExam.ExamID = new Guid(oSqlDataReader["ExamID"].ToString());
                    oCandidateForExam.CandidateForExamCandidate.CandidateCompositeID = oSqlDataReader["CompositeCandidateID"].ToString();
                    oCandidateForExam.CandidateForExamCandidate.CandidatePassword = oSqlDataReader["CandidatePassword"].ToString();
                    oCandidateForExam.CandidateForExamCandidate.CandidateName = oSqlDataReader["Name"].ToString();
                    oCandidateForExam.CandidateForExamCandidate.CandidateLastResult = float.Parse(oSqlDataReader["LastResult"].ToString());
                    oCandidateForExam.CandidateForExamCandidate.CandiadteLastInstitution = oSqlDataReader["LastInstitution"].ToString();
                    oCandidateForExam.CandidateForExamCandidate.CandidateLastPassingYear = int.Parse(oSqlDataReader["LastPassingYear"].ToString());
                    oCandidateForExam.CandidateForExamCandidate.CandidateCvPath = oSqlDataReader["CvPath"].ToString();

                    oListCandidateForExam.Add(oCandidateForExam);
                }

                oSqlDataReader.Close();

                oResult.ResultObject = oListCandidateForExam;
                oResult.ResultMessage = "LoadCandidatesAccordingToSystemUserForEvaluate Success...";
                oResult.ResultIsSuccess = true;
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "LoadCandidatesAccordingToSystemUserForEvaluate Exception...";

                logger.Info("Exception LoadCandidatesAccordingToSystemUserForEvaluate EvaluateProcessDAO+DAO", oEx);

                //new CLogger("Exception LoadCandidatesAccordingToSystemUserForEvaluate EvaluateProcessDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception LoadCandidatesAccordingToSystemUserForEvaluate EvaluateProcessDAO+DAO", ELogLevel.Debug, oEx);
            }
            finally
            {
                if (oSqlDataReader!=null && !oSqlDataReader.IsClosed)
                {
                    oSqlDataReader.Close();
                }
            }

            //new CLogger("Out LoadCandidatesAccordingToSystemUserForEvaluate EvaluateProcessDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out LoadCandidatesAccordingToSystemUserForEvaluate EvaluateProcessDAO+DAO", ELogLevel.Debug); ;

            logger.Info("End LoadCandidatesAccordingToSystemUserForEvaluate EvaluateProcessDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This method Load questions at the time of candidate evaluation, if that system user setup questions.
        /// The system user can see the questions which are setup by him
        /// Administrator can always see all questions
        /// </summary>
        /// <param name="sCandidateID"> It takes string Object </param>
        /// <param name="oExam"> It takes Exam Object </param>
        /// <param name="oSystemUser"> It takes SystemUser Object </param>
        /// <param name="flagForEvaluation"> It takes Boolean Object.It is only to show the Objective answer Name </param>
        /// <returns> It returns Result Object </returns>
        public Result LoadQuestionsForACandidateWhichSetupByAParticularUser(string sCandidateID, Exam oExam, SystemUser oSystemUser, Boolean flagForEvaluation)
        {
            //new CLogger("Start LoadQuestionsForACandidateWhichSetupByAParticularUser EvaluateProcessDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start LoadQuestionsForACandidateWhichSetupByAParticularUser EvaluateProcessDAO+DAO", ELogLevel.Debug);

            logger.Info("Start LoadQuestionsForACandidateWhichSetupByAParticularUser EvaluateProcessDAO+DAO");

            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            SqlDataReader oSqlDataReader = null;

            String sSelect = String.Empty;
            String sTempAnswer = String.Empty;
            String sObtainMark = String.Empty;
            
            List<CandidateAnswerQuestion> oListCandidateAnswerQuestion = new List<CandidateAnswerQuestion>();

            float f =0f;

            try
            {
                if (oSystemUser.SystemUserName.ToLower().Equals("administrator"))
                {
                    //sSelect = "select EX_CandidateExam.AnswerStringOrBits from EX_CandidateExam inner join EX_QuestionGeneration on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID where EX_CandidateExam.CandidateID='" + sCandidateID + "' and EX_CandidateExam.ExamID='"+oExam.ExamID+"' and EX_CandidateExam.QuestionID in (select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='" + oExam.ExamID + "')";
                    //sSelect = "select EX_CandidateExam.AnswerStringOrBits,EX_Question.QuestionID,EX_Question.QuestionText,EX_Question.QuestionDefaultMark,EX_Question.QuestionTypeID,EX_Question.QuestionCategoryID from EX_CandidateExam inner join EX_QuestionGeneration on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID inner join EX_Question on EX_Question.QuestionID = EX_QuestionGeneration.QuestionID where EX_CandidateExam.CandidateID='" + sCandidateID + "' and EX_CandidateExam.ExamID='" + oExam.ExamID + "' and EX_CandidateExam.QuestionID in (select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='" + oExam.ExamID + "') order by EX_Question.QuestionCategoryID,EX_Question.QuestionTypeID asc";
                    //sSelect = "select distinct EX_CandidateExam.AnswerStringOrBits,EX_CandidateExam.ObtainMark, EX_Question.QuestionID,EX_Question.QuestionText,EX_Question.QuestionDefaultMark,EX_Question.QuestionTypeID,EX_Question.QuestionCategoryID from EX_Question inner join EX_QuestionGeneration on EX_Question.QuestionID = EX_QuestionGeneration.QuestionID inner join EX_CandidateExam on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID where EX_CandidateExam.CandidateID='" + sCandidateID + "' and EX_CandidateExam.ExamID='" + oExam.ExamID + "' and EX_CandidateExam.QuestionID in (select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='" + oExam.ExamID + "') order by EX_Question.QuestionCategoryID,EX_Question.QuestionTypeID asc";
                    sSelect = "select distinct EX_CandidateExam.AnswerStringOrBits,EX_CandidateExam.ObtainMark,"
                    +" EX_CandidateExam.AnswerAttachmentPath, EX_Question.QuestionID,"
                    +" EX_Question.QuestionText,EX_QuestionGeneration.SetupQuestionMark,"
                    +" EX_Question.QuestionTypeID,EX_Question.QuestionCategoryID"
                    +" from EX_Question inner join EX_QuestionGeneration on"
                    +" EX_Question.QuestionID = EX_QuestionGeneration.QuestionID"
                    +" inner join EX_CandidateExam on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID"
                    +" where EX_CandidateExam.CandidateID='" + sCandidateID + "'"
                    +" and EX_QuestionGeneration.ExamID='" + oExam.ExamID + "'"
                    +" and EX_CandidateExam.ExamID='" + oExam.ExamID + "' and"
                    +" EX_CandidateExam.QuestionID in"
                    +" (select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration"
                    +" where EX_QuestionGeneration.ExamID='" + oExam.ExamID + "')"
                    +" order by EX_Question.QuestionCategoryID,EX_Question.QuestionTypeID asc";
                }
                else
                {
                    //sSelect = "select EX_CandidateExam.AnswerStringOrBits from EX_CandidateExam inner join EX_QuestionGeneration on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID where EX_CandidateExam.CandidateID='" + sCandidateID + "' and EX_CandidateExam.ExamID='" + oExam.ExamID + "' and EX_CandidateExam.QuestionID in (select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='" + oExam.ExamID + "' and EX_QuestionGeneration.GeneratorID='" + oSystemUser.SystemUserID + "')";
                    //sSelect = "select EX_CandidateExam.AnswerStringOrBits,EX_Question.QuestionID,EX_Question.QuestionText,EX_Question.QuestionDefaultMark,EX_Question.QuestionTypeID,EX_Question.QuestionCategoryID from EX_CandidateExam inner join EX_QuestionGeneration on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID inner join EX_Question on EX_Question.QuestionID = EX_QuestionGeneration.QuestionID where EX_CandidateExam.CandidateID='" + sCandidateID + "' and EX_CandidateExam.ExamID='" + oExam.ExamID + "' and EX_CandidateExam.QuestionID in (select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='" + oExam.ExamID + "' and EX_QuestionGeneration.GeneratorID='" + oSystemUser.SystemUserID + "') order by EX_Question.QuestionCategoryID,EX_Question.QuestionTypeID asc";
                    //sSelect = "select distinct EX_CandidateExam.AnswerStringOrBits,EX_CandidateExam.ObtainMark, EX_Question.QuestionID,EX_Question.QuestionText,EX_Question.QuestionDefaultMark,EX_Question.QuestionTypeID,EX_Question.QuestionCategoryID from EX_Question inner join EX_QuestionGeneration on EX_Question.QuestionID = EX_QuestionGeneration.QuestionID inner join EX_CandidateExam on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID where EX_CandidateExam.CandidateID='" + sCandidateID + "' and EX_CandidateExam.ExamID='" + oExam.ExamID + "' and EX_CandidateExam.QuestionID in (select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='" + oExam.ExamID + "' and EX_QuestionGeneration.GeneratorID='" + oSystemUser.SystemUserID + "') order by EX_Question.QuestionCategoryID,EX_Question.QuestionTypeID asc";
                    sSelect = "select distinct EX_CandidateExam.AnswerStringOrBits,EX_CandidateExam.ObtainMark,"
                    +" EX_CandidateExam.AnswerAttachmentPath, EX_Question.QuestionID,"
                    +" EX_Question.QuestionText,EX_QuestionGeneration.SetupQuestionMark,"
                    +" EX_Question.QuestionTypeID,EX_Question.QuestionCategoryID"
                    +" from EX_Question inner join EX_QuestionGeneration on EX_Question.QuestionID = EX_QuestionGeneration.QuestionID"
                    +" inner join EX_CandidateExam on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID"
                    +" where EX_CandidateExam.CandidateID='" + sCandidateID + "'"
                    +" and EX_QuestionGeneration.ExamID='" + oExam.ExamID + "'"
                    +" and EX_CandidateExam.ExamID='" + oExam.ExamID + "' and"
                    +" EX_CandidateExam.QuestionID in"
                    +" (select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration"
                    +" where EX_QuestionGeneration.ExamID='" + oExam.ExamID + "' and"
                    +" EX_QuestionGeneration.GeneratorID='" + oSystemUser.SystemUserID + "')"
                    +" order by EX_Question.QuestionCategoryID,EX_Question.QuestionTypeID asc";
                }

                oSqlDataReader = oDAOUtil.GetReader(sSelect);

                while (oSqlDataReader.Read())
                {
                    CandidateAnswerQuestion oCandidateAnswerQuestion = new CandidateAnswerQuestion();

                    sTempAnswer = oSqlDataReader["AnswerStringOrBits"].ToString();

                    sObtainMark = oSqlDataReader["ObtainMark"].ToString();

                    if (float.TryParse(sObtainMark, out f))
                    {
                        oCandidateAnswerQuestion.ObtainMark = float.Parse(sObtainMark);
                    }
                    else
                    {
                        oCandidateAnswerQuestion.ObtainMark = 0f;
                    }

                    oCandidateAnswerQuestion.sAnswerAttachFilePath = oSqlDataReader["AnswerAttachmentPath"].ToString();
                    oCandidateAnswerQuestion.QuestionForCandidateAnswer.QuestionID = new Guid(oSqlDataReader["QuestionID"].ToString());
                    oCandidateAnswerQuestion.QuestionForCandidateAnswer.QuestionText = oSqlDataReader["QuestionText"].ToString();
                    oCandidateAnswerQuestion.QuestionForCandidateAnswer.QuestionDefaultMark = float.Parse(oSqlDataReader["SetupQuestionMark"].ToString());
                    oCandidateAnswerQuestion.QuestionForCandidateAnswer.QuestionCategory.CategoryID = int.Parse(oSqlDataReader["QuestionCategoryID"].ToString());
                    
                    //Objective@True:True:False:True:
                    //Descriptive@IQ Des Exam:
                    //DES@IQ
                    String sTempObjective = String.Empty;
                    String sCheck = String.Empty;
                    List<Choice> oListAnswers = new List<Choice>();
                    int iColonIndex = 0;

                    if (sTempAnswer.IndexOf("Objective@", 0)>=0)
                    {
                        sTempObjective = sTempAnswer.Substring(sTempAnswer.IndexOf("@",0)+1);

                        while (sTempObjective != String.Empty)
                        {
                            iColonIndex = sTempObjective.IndexOf(":");

                            if(iColonIndex>=0)
                            {
                                sCheck = sTempObjective.Substring(0, iColonIndex);

                                Choice oChoice = new Choice();

                                oChoice.ChoiceIsValid = Boolean.Parse(sCheck);

                                oListAnswers.Add(oChoice);

                                if (iColonIndex + 1 < sTempObjective.Length)
                                {
                                    sTempObjective = sTempObjective.Substring(iColonIndex + 1, sTempObjective.Length - iColonIndex - 1);
                                }
                                else
                                {
                                    sTempObjective = String.Empty;  
                                }
                            }
                        }

                        oCandidateAnswerQuestion.QuestionForCandidateAnswer.QuestionObjectiveType.ListOfAnswers = oListAnswers;
                        oCandidateAnswerQuestion.QuestionForCandidateAnswer.QuestionQuestionType.QuestionTypeID = 0;
                    }
                    else if (sTempAnswer.IndexOf("Descriptive@", 0)>=0)
                    {
                        oCandidateAnswerQuestion.DescriptiveQuestionAnswerText = sTempAnswer.Substring(sTempAnswer.IndexOf("@",0)+1,sTempAnswer.Length-(sTempAnswer.IndexOf("@",0)+1)-1);
                        oCandidateAnswerQuestion.QuestionForCandidateAnswer.QuestionQuestionType.QuestionTypeID = 1;
                    }

                    oListCandidateAnswerQuestion.Add(oCandidateAnswerQuestion);
                }

                oSqlDataReader.Close();

                //this is for show the Objective answer Name
                if (!flagForEvaluation)
                {
                    foreach (CandidateAnswerQuestion oCandidateAnswerQuestionInList in oListCandidateAnswerQuestion)
                    {
                        if (oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionQuestionType.QuestionTypeID == 0)
                        {
                            if (oSystemUser.SystemUserName.ToLower().Equals("administrator"))
                            {
                                sSelect = "select EX_Objective.ObjectiveQuestionID,EX_Objective.ObjectiveAnswer,"
                                + " EX_Objective.ObjectiveAnswerIsValid"
                                + " from EX_Objective where"
                                + " EX_Objective.ObjectiveQuestionID='" + oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionID + "'";
                                
                            }
                            else
                            {
                                sSelect = "select EX_Objective.ObjectiveQuestionID,EX_Objective.ObjectiveAnswer,"
                                + " EX_Objective.ObjectiveAnswerIsValid"
                                + " from EX_Objective where"
                                + " EX_Objective.ObjectiveQuestionID='" + oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionID + "'";
                            }

                            //String sCheckValid = String.Empty;
                            //String sCheckName = String.Empty;

                            List<Choice> oListChoices = new List<Choice>();

                            oSqlDataReader = oDAOUtil.GetReader(sSelect);

                            int iChoiceCounter = -1;

                            while (oSqlDataReader.Read())
                            {
                                iChoiceCounter = iChoiceCounter + 1;

                                Choice oChoice = new Choice();
                                oChoice.ChoiceIsValid = Boolean.Parse(oSqlDataReader["ObjectiveAnswerIsValid"].ToString());

                                oListChoices.Add(oChoice);
                                //oChoice.ChoiceName = oSqlDataReader["ObjectiveAnswer"].ToString();

                                oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionObjectiveType.ListOfAnswers[iChoiceCounter].ChoiceName = oSqlDataReader["ObjectiveAnswer"].ToString();




                            }


                            oSqlDataReader.Close();

                            oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionObjectiveType.ListOfChoices = oListChoices;
                        }
                    }
                }

                oResult.ResultObject = oListCandidateAnswerQuestion;
                oResult.ResultMessage = "LoadQuestionsForACandidateWhichSetupByAParticularUser Success...";
                oResult.ResultIsSuccess = true;


            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "LoadQuestionsForACandidateWhichSetupByAParticularUser Exception...";

                logger.Info("Exception LoadQuestionsForACandidateWhichSetupByAParticularUser EvaluateProcessDAO+DAO", oEx);

                //new CLogger("Exception LoadQuestionsForACandidateWhichSetupByAParticularUser EvaluateProcessDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception LoadQuestionsForACandidateWhichSetupByAParticularUser EvaluateProcessDAO+DAO", ELogLevel.Debug, oEx);
            }
            finally
            {
                if (oSqlDataReader!=null && !oSqlDataReader.IsClosed)
                {
                    oSqlDataReader.Close();
                }
            }

            //new CLogger("Out LoadQuestionsForACandidateWhichSetupByAParticularUser EvaluateProcessDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out LoadQuestionsForACandidateWhichSetupByAParticularUser EvaluateProcessDAO+DAO", ELogLevel.Debug); ;

            logger.Info("End LoadQuestionsForACandidateWhichSetupByAParticularUser EvaluateProcessDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This method evaluate objective answers for all candidate of an exam.
        /// Only the administrator can call this method.
        /// It evaluates automatically
        /// </summary>
        /// <param name="oListCandidateForExamForGrid"> It takes List<CandidateForExam> Object </param>
        /// <param name="oExam"> It takes Exam Object </param>
        /// <param name="oSystemUser"> It takes SystemUser Object </param>
        /// <returns> It returns Result Object </returns>
        public Result EvaluateObjectiveAnswersForAllCandidateOfAnExma(List<CandidateForExam> oListCandidateForExamForGrid, SystemUser oSystemUser, Exam oExam)
        {
            //new CLogger("Start EvaluateObjectiveAnswersForAllCandidateOfAnExma EvaluateProcessDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start EvaluateObjectiveAnswersForAllCandidateOfAnExma EvaluateProcessDAO+DAO", ELogLevel.Debug);

            logger.Info("Start EvaluateObjectiveAnswersForAllCandidateOfAnExma EvaluateProcessDAO+DAO");

            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            SqlDataReader oSqlDataReader = null;

            String sSelect = String.Empty;

            try
            {
                foreach (CandidateForExam oCandidateForExamInList in oListCandidateForExamForGrid)
                {
                    List<CandidateAnswerQuestion> oListCandidateAnswerQuestion = new List<CandidateAnswerQuestion>();

                    oResult = LoadQuestionsForACandidateWhichSetupByAParticularUser(oCandidateForExamInList.CandidateForExamCandidate.CandidateCompositeID, oExam, oSystemUser, true);

                    if (oResult.ResultIsSuccess)
                    {
                        oListCandidateAnswerQuestion = (List<CandidateAnswerQuestion>)oResult.ResultObject;

                        foreach (CandidateAnswerQuestion oCandidateAnswerQuestionInList in oListCandidateAnswerQuestion)
                        {
                            if (oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionQuestionType.QuestionTypeID == 0)
                            {
                                if (oSystemUser.SystemUserName.ToLower().Equals("administrator"))
                                {
                                    //sSelect = "select EX_Objective.ObjectiveQuestionID,EX_Objective.ObjectiveAnswer,EX_Objective.ObjectiveAnswerIsValid from EX_Objective inner join EX_Question on EX_Objective.ObjectiveQuestionID = EX_Question.QuestionID inner join EX_QuestionGeneration on EX_Question.QuestionID = EX_QuestionGeneration.QuestionID inner join EX_CandidateExam on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID where EX_CandidateExam.QuestionID='" + oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionID + "' and EX_CandidateExam.CandidateID='" + oCandidateInList.CandidateCompositeID + "' and EX_CandidateExam.ExamID='" + oExam.ExamID + "' and EX_CandidateExam.QuestionID in (select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration where EX_QuestionGeneration.ExamID='" + oExam.ExamID + "' and EX_QuestionGeneration.GeneratorID='" + oSystemUser.SystemUserID + "')";
                                    //sSelect = "select distinct EX_Objective.ObjectiveQuestionID,EX_Objective.ObjectiveAnswer,"
                                    //+" EX_Objective.ObjectiveAnswerIsValid from EX_Objective inner join EX_Question"
                                    //+" on EX_Objective.ObjectiveQuestionID = EX_Question.QuestionID inner join EX_QuestionGeneration"
                                    //+" on EX_Question.QuestionID = EX_QuestionGeneration.QuestionID inner join EX_CandidateExam"
                                    //+" on EX_CandidateExam.QuestionID=EX_QuestionGeneration.QuestionID"
                                    //+" where EX_CandidateExam.QuestionID='" + oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionID + "'"
                                    //+" and EX_CandidateExam.CandidateID='" + oCandidateForExamInList.CandidateForExamCandidate.CandidateCompositeID + "'"
                                    //+" and EX_CandidateExam.ExamID='" + oExam.ExamID + "' and EX_CandidateExam.QuestionID"
                                    //+" in (select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration"
                                    //+" where EX_QuestionGeneration.ExamID='" + oExam.ExamID + "')";

                                    sSelect = "select EX_Objective.ObjectiveQuestionID,EX_Objective.ObjectiveAnswer,"
                                    + " EX_Objective.ObjectiveAnswerIsValid from EX_Objective inner join EX_CandidateExam"
                                    + " on EX_CandidateExam.QuestionID=EX_Objective.ObjectiveQuestionID"
                                    + " where EX_CandidateExam.QuestionID='" + oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionID + "'"
                                    + " and EX_CandidateExam.CandidateID='" + oCandidateForExamInList.CandidateForExamCandidate.CandidateCompositeID + "'"
                                    + " and EX_CandidateExam.ExamID='" + oExam.ExamID + "' and EX_CandidateExam.QuestionID"
                                    + " in (select EX_QuestionGeneration.QuestionID from EX_QuestionGeneration"
                                    + " where EX_QuestionGeneration.ExamID='" + oExam.ExamID + "')";
                                }

                                String sCheckValid = String.Empty;
                                String sCheckName = String.Empty;

                                List<Choice> oListChoices = new List<Choice>();

                                oSqlDataReader = oDAOUtil.GetReader(sSelect);

                                while (oSqlDataReader.Read())
                                {
                                    Choice oChoice = new Choice();

                                    oChoice.ChoiceIsValid = Boolean.Parse(oSqlDataReader["ObjectiveAnswerIsValid"].ToString());
                                    oChoice.ChoiceName = oSqlDataReader["ObjectiveAnswer"].ToString();

                                    oListChoices.Add(oChoice);
                                }

                                oSqlDataReader.Close();

                                oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionObjectiveType.ListOfChoices = oListChoices;
                            }
                        }

                        oCandidateForExamInList.CadidateCandidateExam.CandidateAnsweredQuestions = oListCandidateAnswerQuestion;
                    }
                }

                oResult.ResultObject = oListCandidateForExamForGrid;
                oResult.ResultMessage = "Objective answers are evaluated for all candidate of this exam...";
                oResult.ResultIsSuccess = true;
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured at evaluation of objective answers for all candidate of this exam...";

                logger.Info("Exception EvaluateObjectiveAnswersForAllCandidateOfAnExma EvaluateProcessDAO+DAO", oEx);

                //new CLogger("Exception EvaluateObjectiveAnswersForAllCandidateOfAnExma EvaluateProcessDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception EvaluateObjectiveAnswersForAllCandidateOfAnExma EvaluateProcessDAO+DAO", ELogLevel.Debug, oEx);
            }
            finally
            {
                if (oSqlDataReader!=null && !oSqlDataReader.IsClosed)
                {
                    oSqlDataReader.Close();
                }
            }

            //new CLogger("Out EvaluateObjectiveAnswersForAllCandidateOfAnExma EvaluateProcessDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out EvaluateObjectiveAnswersForAllCandidateOfAnExma EvaluateProcessDAO+DAO", ELogLevel.Debug); ;

            logger.Info("End EvaluateObjectiveAnswersForAllCandidateOfAnExma EvaluateProcessDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This method Save ObjectiveAnswer Marks For All Candidate Of An Exam
        /// </summary>
        /// <param name="oListCandidateForExamForGrid"> It takes List<CandidateForExam> Object </param>
        /// <param name="oExam"> It takes Exam Object </param>
        /// <param name="oSystemUser"> It takes SystemUser Object </param>
        /// <returns> It returns Result Object </returns>
        public Result SaveObjectiveAnswerMarksForAllCandidateOfAnExam(List<CandidateForExam> oListCandidateForExamForEvaluate, SystemUser oSystemUser, Exam oExam)
        {
            //new CLogger("Start SaveObjectiveAnswerMarksForAllCandidateOfAnExam EvaluateProcessDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start SaveObjectiveAnswerMarksForAllCandidateOfAnExam EvaluateProcessDAO+DAO", ELogLevel.Debug);

            logger.Info("Start SaveObjectiveAnswerMarksForAllCandidateOfAnExam EvaluateProcessDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            String sUpdate = String.Empty;

            List<String> oListString = new List<String>();

            try
            {
                foreach (CandidateForExam oCandidateForExamInList in oListCandidateForExamForEvaluate)
                {
                    foreach (CandidateAnswerQuestion oCandidateAnswerQuestionInList in oCandidateForExamInList.CadidateCandidateExam.CandidateAnsweredQuestions)
                    {
                        if (oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionQuestionType.QuestionTypeID == 0)
                        {
                            sUpdate = "update EX_CandidateExam set ObtainMark='" + oCandidateAnswerQuestionInList.ObtainMark + "' where ExamID='" + oExam.ExamID + "' and CandidateID='" + oCandidateForExamInList.CandidateForExamCandidate.CandidateCompositeID + "' and QuestionID='" + oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionID + "'";

                            oListString.Add(sUpdate);
                        }
                    }
                }

                if (oDAOUtil.ExecuteNonQuery(oListString))
                {
                    oResult.ResultObject = oListCandidateForExamForEvaluate;
                    oResult.ResultMessage = "Objective answers are saved for all candidate of this exam...";
                    oResult.ResultIsSuccess = true;
                }
                else
                {
                    oResult.ResultIsSuccess = false;
                    oResult.ResultMessage = "Saving objective answers for all candidate of this exam Failed...";
                }
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception at saving objective answers for all candidate of this exam...";

                logger.Info("Exception SaveObjectiveAnswerMarksForAllCandidateOfAnExam EvaluateProcessDAO+DAO", oEx);

                //new CLogger("Exception SaveObjectiveAnswerMarksForAllCandidateOfAnExam EvaluateProcessDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception SaveObjectiveAnswerMarksForAllCandidateOfAnExam EvaluateProcessDAO+DAO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out SaveObjectiveAnswerMarksForAllCandidateOfAnExam EvaluateProcessDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out SaveObjectiveAnswerMarksForAllCandidateOfAnExam EvaluateProcessDAO+DAO", ELogLevel.Debug); ;

            logger.Info("End SaveObjectiveAnswerMarksForAllCandidateOfAnExam EvaluateProcessDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This method Save DescriptiveAnswer Marks For Candidates For A SystemUser Of An Exam
        /// </summary>
        /// <param name="sCandidateID"> It takes String Object </param>
        /// <param name="oListCandidateAnswerQuestion"> It takes List<CandidateAnswerQuestion> Object </param>
        /// <param name="oExam"> It takes Exam Object </param>
        /// <param name="oSystemUser"> It takes SystemUser Object </param>
        /// <returns> It returns Result Object </returns>
        public Result SaveDescriptiveAnswerMarksForCandidatesForASystemUserOfAnExam(String sCandidateID, List<CandidateAnswerQuestion> oListCandidateAnswerQuestion, Exam oExam, SystemUser oSystemUser)
        {

            //new CLogger("Start SaveDescriptiveAnswerMarksForCandidatesForASystemUserOfAnExam EvaluateProcessDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start SaveDescriptiveAnswerMarksForCandidatesForASystemUserOfAnExam EvaluateProcessDAO+DAO", ELogLevel.Debug);

            logger.Info("Start SaveDescriptiveAnswerMarksForCandidatesForASystemUserOfAnExam EvaluateProcessDAO+DAO");

            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            String sUpdate = String.Empty;

            List<String> oListString = new List<String>();

            try
            {
                foreach (CandidateAnswerQuestion oCandidateAnswerQuestionInList in oListCandidateAnswerQuestion)
                {
                    if (oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionQuestionType.QuestionTypeID == 1)
                    {
                        sUpdate = "update EX_CandidateExam set ObtainMark='" + oCandidateAnswerQuestionInList.ObtainMark + "' where ExamID='" + oExam.ExamID + "' and CandidateID='" + sCandidateID + "' and QuestionID='" + oCandidateAnswerQuestionInList.QuestionForCandidateAnswer.QuestionID + "'";

                        oListString.Add(sUpdate);
                    }
                }

                if (oDAOUtil.ExecuteNonQuery(oListString))
                {
                    oResult.ResultObject = oListCandidateAnswerQuestion;
                    oResult.ResultMessage = "Descriptive answers are evaluated...";
                    oResult.ResultIsSuccess = true;
                }
                else
                {
                    oResult.ResultIsSuccess = false;
                    oResult.ResultMessage = "Descriptive answers are evaluation failed...";
                }
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "SaveDescriptiveAnswerMarksForCandidatesForASystemUserOfAnExam Exception...";

                logger.Info("Exception SaveDescriptiveAnswerMarksForCandidatesForASystemUserOfAnExam EvaluateProcessDAO+DAO", oEx);
                //new CLogger("Exception SaveDescriptiveAnswerMarksForCandidatesForASystemUserOfAnExam EvaluateProcessDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception SaveDescriptiveAnswerMarksForCandidatesForASystemUserOfAnExam EvaluateProcessDAO+DAO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out SaveDescriptiveAnswerMarksForCandidatesForASystemUserOfAnExam EvaluateProcessDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out SaveDescriptiveAnswerMarksForCandidatesForASystemUserOfAnExam EvaluateProcessDAO+DAO", ELogLevel.Debug); ;

            logger.Info("End SaveDescriptiveAnswerMarksForCandidatesForASystemUserOfAnExam EvaluateProcessDAO+DAO");

            return oResult;
        }
    }
}
