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

namespace DAO
{
    /// <summary>
    /// This class is used for Question manipulation
    /// </summary>
    public class QuestionDAO
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(QuestionDAO));
        
        public QuestionDAO()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// This method inserts a Question
        /// </summary>
        /// <param name="oQuestion"> It takes Question Object </param>
        /// <returns> It returns Result Object </returns>
        public Result QuestionEntry(Question oQuestion) //r
        {
            logger.Info("Start QuestionEntry QuestionDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            String sInsert = String.Empty;
            List<String> oListInsert = new List<String>();

            int i=0;
            int iBit = 0;

            try
            {
                oQuestion.QuestionID = Guid.NewGuid();

                sInsert = "insert into EX_Question(QuestionID,QuestionText,QuestionCreatorID,QuestionDefaultMark,QuestionCategoryID,QuestionTypeID,QuestionPossibleAnswerTime,QuestionLabelID) values('" + oQuestion.QuestionID + "','" + oQuestion.QuestionText + "','" + oQuestion.QuestionCreator.SystemUserID + "','" + oQuestion.QuestionDefaultMark + "','" + oQuestion.QuestionCategory.CategoryID + "','" + oQuestion.QuestionQuestionType.QuestionTypeID + "','" + oQuestion.QuestionPossibleAnswerTime+"','"+oQuestion.QuestionLevel.LevelID + "')";
                oListInsert.Add(sInsert);

                if (oQuestion.QuestionQuestionType.QuestionTypeID == 0)
                {
                    for (i = 0; i < oQuestion.QuestionObjectiveType.ListOfChoices.Count; i++)
                    {
                        iBit = 0;

                        if (oQuestion.QuestionObjectiveType.ListOfChoices[i].ChoiceIsValid)
                        {
                            iBit = 1;
                        }

                        sInsert = "insert into EX_Objective(ObjectiveQuestionID,ObjectiveAnswer,ObjectiveAnswerIsValid) values('" + oQuestion.QuestionID + "','" + oQuestion.QuestionObjectiveType.ListOfChoices[i].ChoiceName + "','" + iBit + "')";
                        oListInsert.Add(sInsert);
                    }
                }
                    
                if (oDAOUtil.ExecuteNonQuery(oListInsert))
                {
                    oResult.ResultObject = oQuestion;
                    oResult.ResultMessage = "Question Entry Success...";
                    oResult.ResultIsSuccess = true;
                }
                else
                {
                    oResult.ResultIsSuccess = false;
                    oResult.ResultMessage = "Question Entry Failed...";
                }
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception occured during Question Entry...";
                oResult.ResultException = oEx;

                logger.Info("Exception QuestionEntry QuestionDAO+DAO", oEx);
            }

            logger.Info("End QuestionEntry QuestionDAO+DAO");

            return oResult;
        }

//        public Result QuestionListShowForSetup(Question oQuestion)
//        {
//            Result oResult = new Result();
//            DAOUtil oDAOUtil = new DAOUtil();

//            SqlDataReader oSqlDataReader = null;

//            String sSelect = String.Empty;
//            String sChoiceSelect = String.Empty;

//            List<QuestionSetup> oListQuestionSetup = new List<QuestionSetup>();

//            try
//            {
//                if (oQuestion.QuestionCreator.SystemUserName.ToLower().Equals("administrator"))
//                {
//                    sSelect = "select distinct EX_Question.QuestionID as EntryQuestionID,EX_QuestionGeneration.QuestionID as GeneratedQuestionID,EX_QuestionGeneration.SetupQuestionMark,QuestionText,QuestionCreatorID,QuestionDefaultMark,QuestionCategoryID,QuestionTypeID,EX_Question.QuestionPossibleAnswerTime from EX_Question left join EX_QuestionGeneration on EX_Question.QuestionID=EX_QuestionGeneration.QuestionID where EX_Question.QuestionCategoryID='" + oQuestion.QuestionCategory.CategoryID + "' and EX_Question.QuestionTypeID='" + oQuestion.QuestionQuestionType.QuestionTypeID + "'";
//                }
//                else
//                {
//                    sSelect = "select distinct EX_Question.QuestionID as EntryQuestionID,EX_QuestionGeneration.QuestionID as GeneratedQuestionID,EX_QuestionGeneration.SetupQuestionMark,QuestionText,QuestionCreatorID,QuestionDefaultMark,QuestionCategoryID,QuestionTypeID,EX_Question.QuestionPossibleAnswerTime from EX_Question left join EX_QuestionGeneration on EX_Question.QuestionID=EX_QuestionGeneration.QuestionID where EX_Question.QuestionCreatorID='" + oQuestion.QuestionCreator.SystemUserID + "' and EX_Question.QuestionCategoryID='" + oQuestion.QuestionCategory.CategoryID + "' and EX_Question.QuestionTypeID='" + oQuestion.QuestionQuestionType.QuestionTypeID + "'";
//                }

//                oSqlDataReader = oDAOUtil.GetReader(sSelect);

//                while (oSqlDataReader.Read())
//                {
//                    QuestionSetup oPopulatedQuestionSetup = new QuestionSetup();

//                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionID = new Guid(oSqlDataReader["EntryQuestionID"].ToString());
//                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionText = oSqlDataReader["QuestionText"].ToString();
//                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionCreator.SystemUserID = new Guid(oSqlDataReader["QuestionCreatorID"].ToString());
//                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionDefaultMark = float.Parse(oSqlDataReader["QuestionDefaultMark"].ToString());
//                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionCategory.CategoryID = int.Parse(oSqlDataReader["QuestionCategoryID"].ToString());
//                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionQuestionType.QuestionTypeID = int.Parse(oSqlDataReader["QuestionTypeID"].ToString());
//                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionPossibleAnswerTime = float.Parse(oSqlDataReader["QuestionPossibleAnswerTime"].ToString());
//                    oPopulatedQuestionSetup.QuestionSetupMark = oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionDefaultMark;
//                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionIsUsed = false;

//                    if (oSqlDataReader["GeneratedQuestionID"].ToString().Length > 0)
//                    {
//                        oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionIsUsed = true;
//                    }

//                    if (oSqlDataReader["SetupQuestionMark"].ToString().Length > 0)
//                    {
//                        oPopulatedQuestionSetup.QuestionSetupMark = float.Parse(oSqlDataReader["SetupQuestionMark"].ToString());
//                    }

//                    oListQuestionSetup.Add(oPopulatedQuestionSetup);
//                }

//                oSqlDataReader.Close();

//                if (oQuestion.QuestionQuestionType.QuestionTypeID == 0) //enum should be used here
//                {
//                    foreach (QuestionSetup oQuestionSetupInList in oListQuestionSetup)
//                    {
//                        sChoiceSelect = "select ObjectiveAnswer,ObjectiveAnswerIsValid from EX_Objective where ObjectiveQuestionID='" + oQuestionSetupInList.QuestionSetupQuestion.QuestionID + "'";

//                        List<Choice> oListChoice = new List<Choice>();

//                        oSqlDataReader = oDAOUtil.GetReader(sChoiceSelect);

//                        while (oSqlDataReader.Read())
//                        {
//                            //prepare the choices for a particular questtion......
//                            //and populate oListQuestion
//                            Choice oChoice = new Choice();

//                            oChoice.ChoiceName = oSqlDataReader["ObjectiveAnswer"].ToString();
//                            oChoice.ChoiceIsValid = Convert.ToBoolean(oSqlDataReader["ObjectiveAnswerIsValid"].ToString());

//                            oListChoice.Add(oChoice);
//                        }

//                        oSqlDataReader.Close();

//                        oQuestionSetupInList.QuestionSetupQuestion.QuestionObjectiveType.ListOfChoices = oListChoice;
//                    }
//                }

//                oResult.ResultObject = oListQuestionSetup;
//                oResult.ResultMessage = "QuestionListShowForSetup Success...";
//                oResult.ResultIsSuccess = true;
//            }
//            catch (Exception oEx)
//            {
//                oResult.ResultIsSuccess = false;
//                oResult.ResultMessage = "Exception occured during QuestionListShowForSetup...";
//                oResult.ResultException = oEx;
//            }
//            finally
//            {
//                if (oSqlDataReader!=null && !oSqlDataReader.IsClosed)
//                {
//                    oSqlDataReader.Close();
//                }
//            }
            
//            return oResult;
//        }

        /// <summary>
        /// This method shows question list for setup questions
        /// </summary>
        /// <param name="oQuestion"> It takes Question Object </param>
        /// <param name="oExam"> It takes Exam Object </param>
        /// <returns> It returns Result Object </returns>
        public Result QuestionListShowForSetup(Question oQuestion, Exam oExam)
        {
            logger.Info("Start QuestionListShowForSetup QuestionDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            SqlDataReader oSqlDataReader = null;

            String sSelect = String.Empty;
            String sChoiceSelect = String.Empty;

            List<QuestionSetup> oListQuestionSetup = new List<QuestionSetup>();

            try
            {
                if (oQuestion.QuestionCreator.SystemUserName.ToLower().Equals("administrator"))
                {
                    //sSelect = "select distinct EX_Question.QuestionID as EntryQuestionID,EX_QuestionGeneration.QuestionID as GeneratedQuestionID,EX_QuestionGeneration.SetupQuestionMark,QuestionText,QuestionCreatorID,QuestionDefaultMark,QuestionCategoryID,QuestionTypeID,EX_Question.QuestionPossibleAnswerTime from EX_Question left join EX_QuestionGeneration on EX_Question.QuestionID=EX_QuestionGeneration.QuestionID where EX_Question.QuestionCategoryID='" + oQuestion.QuestionCategory.CategoryID + "' and EX_Question.QuestionTypeID='" + oQuestion.QuestionQuestionType.QuestionTypeID + "'";


                    sSelect = "select distinct QuestionID,QuestionText,QuestionCreatorID,QuestionDefaultMark,QuestionCategoryID,"
                    + " QuestionTypeID,QuestionPossibleAnswerTime,QuestionLabelID,EX_Label.LabelName,EX_Label.LabelPrerequisite"
                    + " from EX_Question inner join EX_Label on EX_Question.QuestionLabelID=EX_Label.LabelID where QuestionCategoryID='" + oQuestion.QuestionCategory.CategoryID + 
                    "' and QuestionTypeID='" + oQuestion.QuestionQuestionType.QuestionTypeID + "'";
                }
                else
                {
                    //sSelect = "select distinct EX_Question.QuestionID as EntryQuestionID,EX_QuestionGeneration.QuestionID as GeneratedQuestionID,EX_QuestionGeneration.SetupQuestionMark,QuestionText,QuestionCreatorID,QuestionDefaultMark,QuestionCategoryID,QuestionTypeID,EX_Question.QuestionPossibleAnswerTime from EX_Question left join EX_QuestionGeneration on EX_Question.QuestionID=EX_QuestionGeneration.QuestionID where EX_Question.QuestionCreatorID='" + oQuestion.QuestionCreator.SystemUserID + "' and EX_Question.QuestionCategoryID='" + oQuestion.QuestionCategory.CategoryID + "' and EX_Question.QuestionTypeID='" + oQuestion.QuestionQuestionType.QuestionTypeID + "'";

                    sSelect = "select distinct QuestionID,QuestionText,QuestionCreatorID,QuestionDefaultMark,QuestionCategoryID,"
                    + " QuestionTypeID,QuestionPossibleAnswerTime,QuestionLabelID,EX_Label.LabelName,EX_Label.LabelPrerequisite"
                    + " from EX_Question inner join EX_Label on EX_Question.QuestionLabelID=EX_Label.LabelID where QuestionCategoryID='" + oQuestion.QuestionCategory.CategoryID +
                    "' and QuestionTypeID='" + oQuestion.QuestionQuestionType.QuestionTypeID + "'";

                }

                oSqlDataReader = oDAOUtil.GetReader(sSelect);

                while (oSqlDataReader.Read())
                {
                    QuestionSetup oPopulatedQuestionSetup = new QuestionSetup();

                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionID = new Guid(oSqlDataReader["QuestionID"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionText = oSqlDataReader["QuestionText"].ToString();
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionCreator.SystemUserID = new Guid(oSqlDataReader["QuestionCreatorID"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionDefaultMark = float.Parse(oSqlDataReader["QuestionDefaultMark"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionCategory.CategoryID = int.Parse(oSqlDataReader["QuestionCategoryID"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionQuestionType.QuestionTypeID = int.Parse(oSqlDataReader["QuestionTypeID"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionPossibleAnswerTime = float.Parse(oSqlDataReader["QuestionPossibleAnswerTime"].ToString());

                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionLevel.LevelID = new Guid(oSqlDataReader["QuestionLabelID"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionLevel.LevelName = oSqlDataReader["LabelName"].ToString();
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionLevel.LevelDescription = oSqlDataReader["LabelPrerequisite"].ToString();

                    oPopulatedQuestionSetup.QuestionSetupMark = oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionDefaultMark;
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionIsUsed = false;

                    //if (oSqlDataReader["GeneratedQuestionID"].ToString().Length > 0)
                    //{
                    //    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionIsUsed = true;
                    //}

                    //if (oSqlDataReader["SetupQuestionMark"].ToString().Length > 0)
                    //{
                    //    oPopulatedQuestionSetup.QuestionSetupMark = float.Parse(oSqlDataReader["SetupQuestionMark"].ToString());
                    //}

                    oListQuestionSetup.Add(oPopulatedQuestionSetup);
                }

                oSqlDataReader.Close();

                if (oQuestion.QuestionQuestionType.QuestionTypeID == 0) //enum should be used here
                {
                    foreach (QuestionSetup oQuestionSetupInList in oListQuestionSetup)
                    {
                        sChoiceSelect = "select ObjectiveAnswer,ObjectiveAnswerIsValid from EX_Objective where ObjectiveQuestionID='" + oQuestionSetupInList.QuestionSetupQuestion.QuestionID + "'";

                        List<Choice> oListChoice = new List<Choice>();

                        oSqlDataReader = oDAOUtil.GetReader(sChoiceSelect);

                        while (oSqlDataReader.Read())
                        {
                            //prepare the choices for a particular questtion......
                            //and populate oListQuestion
                            Choice oChoice = new Choice();

                            oChoice.ChoiceName = oSqlDataReader["ObjectiveAnswer"].ToString();
                            oChoice.ChoiceIsValid = Convert.ToBoolean(oSqlDataReader["ObjectiveAnswerIsValid"].ToString());

                            oListChoice.Add(oChoice);
                        }

                        oSqlDataReader.Close();

                        oQuestionSetupInList.QuestionSetupQuestion.QuestionObjectiveType.ListOfChoices = oListChoice;
                    }
                }


                foreach (QuestionSetup oQuestionSetupInList in oListQuestionSetup)
                {
                    sSelect = "select distinct EX_QuestionGeneration.QuestionID as GeneratedQuestionID,EX_QuestionGeneration.SetupQuestionMark from EX_QuestionGeneration where EX_QuestionGeneration.QuestionID='" + oQuestionSetupInList.QuestionSetupQuestion.QuestionID + "' and EX_QuestionGeneration.ExamID='" + oExam.ExamID + "'";

                    oSqlDataReader = oDAOUtil.GetReader(sSelect);

                    if (oSqlDataReader.HasRows)
                    {
                        oQuestionSetupInList.QuestionSetupQuestion.QuestionIsUsed = true;

                        while (oSqlDataReader.Read())
                        {
                            if (oSqlDataReader["SetupQuestionMark"].ToString().Length > 0)
                            {
                                oQuestionSetupInList.QuestionSetupMark = float.Parse(oSqlDataReader["SetupQuestionMark"].ToString());
                            }
                        }
                    }

                    oSqlDataReader.Close();
                }


                oResult.ResultObject = oListQuestionSetup;
                oResult.ResultMessage = "QuestionListShowForSetup Success...";
                oResult.ResultIsSuccess = true;
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception occured during QuestionListShowForSetup...";
                oResult.ResultException = oEx;

                logger.Info("Exception QuestionListShowForSetup QuestionDAO+DAO", oEx);
            }
            finally
            {
                if (oSqlDataReader != null && !oSqlDataReader.IsClosed)
                {
                    oSqlDataReader.Close();
                }
            }

            logger.Info("End QuestionListShowForSetup QuestionDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This method shows question list.
        /// Administrator can see all questions.
        /// Other system user can see questions which are setup by him 
        /// </summary>
        /// <param name="oQuestion"> It takes Question Object </param>
        /// <returns> It returns Result Object </returns>
        public Result QuestionListShow(Question oQuestion) 
        {
            logger.Info("Start QuestionListShow QuestionDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            List<Question> oListQuestion = new List<Question>();

            SqlDataReader oSqlDataReader = null;

            String sSelect = String.Empty;
            String sChoiceSelect = String.Empty;

            try
            {
                if (oQuestion.QuestionCreator.SystemUserName.ToLower().Equals("administrator"))
                {
                    //sSelect = "select distinct EX_Question.QuestionID as EntryQuestionID,EX_QuestionGeneration.QuestionID as GeneratedQuestionID,QuestionText,QuestionCreatorID,"
                    //+" QuestionDefaultMark,QuestionCategoryID,QuestionTypeID,EX_Question.QuestionPossibleAnswerTime"
                    //+" from EX_Question left join EX_QuestionGeneration on EX_Question.QuestionID=EX_QuestionGeneration.QuestionID"
                    //+" where EX_Question.QuestionCategoryID='" + oQuestion.QuestionCategory.CategoryID + "' and EX_Question.QuestionTypeID='" + oQuestion.QuestionQuestionType.QuestionTypeID + "'";

                    sSelect = "select distinct EX_Question.QuestionID as EntryQuestionID,EX_QuestionGeneration.QuestionID as GeneratedQuestionID,QuestionText,QuestionCreatorID,"
                    + " QuestionDefaultMark,QuestionCategoryID,QuestionTypeID,EX_Question.QuestionPossibleAnswerTime,"
                    + " EX_Question.QuestionLabelID,EX_Label.LabelName,EX_Label.LabelPrerequisite"
                    + " from EX_Label inner join EX_Question on EX_Label.LabelID=EX_Question.QuestionLabelID left join EX_QuestionGeneration on EX_Question.QuestionID=EX_QuestionGeneration.QuestionID"
                    + " where EX_Question.QuestionCategoryID='" + oQuestion.QuestionCategory.CategoryID + "' and EX_Question.QuestionTypeID='" + oQuestion.QuestionQuestionType.QuestionTypeID + "'";

                }
                else
                {
                    //sSelect = "select distinct EX_Question.QuestionID as EntryQuestionID,EX_QuestionGeneration.QuestionID as GeneratedQuestionID,QuestionText,QuestionCreatorID,"
                    //+" QuestionDefaultMark,QuestionCategoryID,QuestionTypeID,EX_Question.QuestionPossibleAnswerTime"
                    //+" from EX_Question left join EX_QuestionGeneration on EX_Question.QuestionID=EX_QuestionGeneration.QuestionID"
                    //+" where EX_Question.QuestionCreatorID='" + oQuestion.QuestionCreator.SystemUserID + "' and EX_Question.QuestionCategoryID='" + oQuestion.QuestionCategory.CategoryID 
                    //+"' and EX_Question.QuestionTypeID='" + oQuestion.QuestionQuestionType.QuestionTypeID + "'";

                    sSelect = "select distinct EX_Question.QuestionID as EntryQuestionID,EX_QuestionGeneration.QuestionID as GeneratedQuestionID,QuestionText,QuestionCreatorID,"
                   + " QuestionDefaultMark,QuestionCategoryID,QuestionTypeID,EX_Question.QuestionPossibleAnswerTime,"
                   + " EX_Question.QuestionLabelID,EX_Label.LabelName,EX_Label.LabelPrerequisite"
                   + " from EX_Label inner join EX_Question on EX_Label.LabelID=EX_Question.QuestionLabelID left join EX_QuestionGeneration on EX_Question.QuestionID=EX_QuestionGeneration.QuestionID"
                   + " where EX_Question.QuestionCreatorID='" + oQuestion.QuestionCreator.SystemUserID 
                   + "' and EX_Question.QuestionCategoryID='" + oQuestion.QuestionCategory.CategoryID + "' and EX_Question.QuestionTypeID='" + oQuestion.QuestionQuestionType.QuestionTypeID + "'";
                }

                oSqlDataReader = oDAOUtil.GetReader(sSelect);

                while (oSqlDataReader.Read())
                {
                    Question oPopulatedQuestion = new Question();

                    oPopulatedQuestion.QuestionID = new Guid(oSqlDataReader["EntryQuestionID"].ToString());
                    oPopulatedQuestion.QuestionText = oSqlDataReader["QuestionText"].ToString();
                    oPopulatedQuestion.QuestionCreator.SystemUserID = new Guid(oSqlDataReader["QuestionCreatorID"].ToString());
                    oPopulatedQuestion.QuestionDefaultMark = float.Parse(oSqlDataReader["QuestionDefaultMark"].ToString());
                    oPopulatedQuestion.QuestionCategory.CategoryID = int.Parse(oSqlDataReader["QuestionCategoryID"].ToString());
                    oPopulatedQuestion.QuestionQuestionType.QuestionTypeID = int.Parse(oSqlDataReader["QuestionTypeID"].ToString());
                    oPopulatedQuestion.QuestionPossibleAnswerTime = float.Parse(oSqlDataReader["QuestionPossibleAnswerTime"].ToString());

                    oPopulatedQuestion.QuestionLevel.LevelID = new Guid(oSqlDataReader["QuestionLabelID"].ToString());
                    oPopulatedQuestion.QuestionLevel.LevelName = oSqlDataReader["LabelName"].ToString();
                    oPopulatedQuestion.QuestionLevel.LevelDescription = oSqlDataReader["LabelPrerequisite"].ToString();

                    oPopulatedQuestion.QuestionIsUsed = false;

                    if (oSqlDataReader["GeneratedQuestionID"].ToString().Length>0)
                    {
                        oPopulatedQuestion.QuestionIsUsed = true;
                    }

                    oListQuestion.Add(oPopulatedQuestion);
                }

                oSqlDataReader.Close();

                if (oQuestion.QuestionQuestionType.QuestionTypeID==0) //enum should be used here
                {
                    foreach (Question oQuestionForChoice in oListQuestion)
                    {
                        sChoiceSelect = "select ObjectiveAnswer,ObjectiveAnswerIsValid from EX_Objective where ObjectiveQuestionID='"+oQuestionForChoice.QuestionID+"'";

                        List<Choice> oListChoice = new List<Choice>();
                        
                        oSqlDataReader = oDAOUtil.GetReader(sChoiceSelect);

                        while (oSqlDataReader.Read())
                        { 
                            //prepare the choices for a particular questtion......
                            //and populate oListQuestion
                            Choice oChoice = new Choice();
                            
                            oChoice.ChoiceName = oSqlDataReader["ObjectiveAnswer"].ToString();
                            oChoice.ChoiceIsValid = Convert.ToBoolean(oSqlDataReader["ObjectiveAnswerIsValid"].ToString());

                            oListChoice.Add(oChoice);
                        }

                        oSqlDataReader.Close();

                        oQuestionForChoice.QuestionObjectiveType.ListOfChoices = oListChoice;
                    }
                }

                oResult.ResultObject = oListQuestion;
                oResult.ResultMessage = "Question List Show Success...";
                oResult.ResultIsSuccess = true;
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception occured during Question List Show...";
                oResult.ResultException = oEx;

                logger.Info("Exception QuestionListShow QuestionDAO+DAO", oEx);
            }
            finally
            {
                if (oSqlDataReader!=null && !oSqlDataReader.IsClosed)
                {
                    oSqlDataReader.Close();
                }
            }

            logger.Info("End QuestionListShow QuestionDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This method deletes questions
        /// </summary>
        /// <param name="oListQuestion"> It takes List<Question> Object </param>
        /// <param name="iarrChecked"> It takes integer array indicates the marked questions </param>
        /// <returns> It returns Result Object </returns>
        public Result DeleteQuestionList(List<Question> oListQuestion, int[] iarrChecked) //r
        {
            logger.Info("Start DeleteQuestionList QuestionDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            List<String> oListString = new List<String>();
            String sDelete = String.Empty;

            List<Question> oListQuestion1 = new List<Question>();

            int i = 0;
            
            try
            {
                for (i = 0; i < iarrChecked.Length; i++)
                {
                    if (iarrChecked[i] == 1 && !oListQuestion[i].QuestionIsUsed)
                    {
                        // Before delete, check that, is it used at the time of SETUP Question.
                        // If it is used, then u cannot delete it.
                        
                        if (oListQuestion[i].QuestionQuestionType.QuestionTypeID==1)
                        {
                            sDelete = "delete from EX_Question where QuestionID='" + oListQuestion[i].QuestionID + "'";
                            oListQuestion1.Add(null);
                            oListString.Add(sDelete);
                           
                        }
                        else
                        {
                            //first delete from EX_Objective, then from EX_Question
                            sDelete = "delete from EX_Objective where ObjectiveQuestionID='" + oListQuestion[i].QuestionID+"'";
                            oListString.Add(sDelete);
                            //sDelete = "delete from EX_Question where QuestionID='" + oListQuestion[i].QuestionID + "' and QuestionTypeName='" + oListQuestion[i].QuestionObjectiveType.TypeName + "'";
                            sDelete = "delete from EX_Question where QuestionID='" + oListQuestion[i].QuestionID + "'";
                            oListQuestion1.Add(null);
                            oListString.Add(sDelete);
                        }

                    }
                    else
                    {
                        oListQuestion1.Add(oListQuestion[i]);
                    }
                }

                oListQuestion1.RemoveAll(delegate(Question oQuestion) { if (oQuestion != null) { return false; } return true; });

                if (oDAOUtil.ExecuteNonQuery(oListString))
                {
                    oResult.ResultObject = oListQuestion1;

                    if (oListQuestion1.Count < oListQuestion.Count)
                    {
                        oResult.ResultMessage = "Question Delete Success...";
                    }
                    else if(oListQuestion1.Count==oListQuestion.Count)
                    {
                        oResult.ResultMessage = "Your selected questions are used...";
                    }
                    
                    oResult.ResultIsSuccess = true;
                }
                else
                {
                    oResult.ResultIsSuccess = false;
                    oResult.ResultMessage = "Question Delete Failed...";
                }
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Question Delete...";

                logger.Info("Exception DeleteQuestionList QuestionDAO+DAO", oEx);
            }

            logger.Info("End DeleteQuestionList QuestionDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This method Update a Question
        /// </summary>
        /// <param name="oQuestion"> It takes Question Object </param>
        /// <returns> It returns Result Object </returns>
        public Result UpdateQuestion(Question oQuestion) //r
        {
            logger.Info("Start UpdateQuestion QuestionDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            List<String> oListString = new List<String>();
            String sString = String.Empty;

            int iBit = 0;
            
            try
            {
                sString = "update EX_Question set QuestionDefaultMark='" + oQuestion.QuestionDefaultMark + "',QuestionPossibleAnswerTime='"+oQuestion.QuestionPossibleAnswerTime +"' where QuestionID='" + oQuestion.QuestionID + "'";
                oListString.Add(sString);
                
                //if (!oQuestion.QuestionIsDescriptive)
                if (oQuestion.QuestionQuestionType.QuestionTypeID==0)
                {
                    sString = "delete from EX_Objective where ObjectiveQuestionID='" + oQuestion.QuestionID + "'";
                    oListString.Add(sString);

                    foreach (Choice oChoice in oQuestion.QuestionObjectiveType.ListOfChoices)
                    {
                        iBit = 0;

                        if (oChoice.ChoiceIsValid)
                        {
                            iBit = 1;
                        }

                        sString = "insert into EX_Objective(ObjectiveQuestionID,ObjectiveAnswer,ObjectiveAnswerIsValid) values('" + oQuestion.QuestionID + "','" + oChoice.ChoiceName + "','" + iBit + "')";
                        oListString.Add(sString);
                    }
                }

                if (oDAOUtil.ExecuteNonQuery(oListString))
                {
                    oResult.ResultObject = oQuestion;
                    oResult.ResultMessage = "Question Update Success...";
                    oResult.ResultIsSuccess = true;
                }
                else
                {
                    oResult.ResultIsSuccess = false;
                    oResult.ResultMessage = "Question Update Failed...";
                }
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Question Update...";

                logger.Info("Exception UpdateQuestion QuestionDAO+DAO", oEx);
            }

            logger.Info("End UpdateQuestion QuestionDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This method setup the selected questions for an exam 
        /// </summary>
        /// <param name="oListQuestionSetup"> It takes List<QuestionSetup> Object </param>
        /// <param name="iarrChecked"> It takes integer array to indicates the marked questions </param>
        /// <returns> It returns Result Object </returns>
        public Result QuestionSetup(List<QuestionSetup> oListQuestionSetup, int[] iarrChecked) //r
        {
            logger.Info("Start QuestionSetup QuestionDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            String sSelect = String.Empty;
            String sInsert = String.Empty;
            String sUpdate = String.Empty;

            List<String> oListStringInsertUpdate = new List<String>();

            List<QuestionSetup> oListSuccessQuestionSetup = new List<QuestionSetup>();

            SqlDataReader oSqlDataReader = null;

            int i = 0;

            try
            {
                foreach (QuestionSetup oQuestionSetup in oListQuestionSetup)
                {
                    if (iarrChecked[i] == 1)
                    {
                        Boolean flag = true;

                        sSelect = "select ExamID,QuestionID from EX_QuestionGeneration where ExamID='" + oQuestionSetup.QuestionSetupExam.ExamID + "' and QuestionID='" + oQuestionSetup.QuestionSetupQuestion.QuestionID + "'";

                        oSqlDataReader = oDAOUtil.GetReader(sSelect);
                        if (oSqlDataReader.HasRows)
                        {
                            flag = false;
                        }
                        oSqlDataReader.Close();

                        if (flag)
                        {
                            sInsert = "insert into EX_QuestionGeneration(ExamID,QuestionID,SetupQuestionMark,GeneratorID,GenerationTime) values('" + oQuestionSetup.QuestionSetupExam.ExamID + "','" + oQuestionSetup.QuestionSetupQuestion.QuestionID + "','" + oQuestionSetup.QuestionSetupMark + "','" + oQuestionSetup.QuestionSetupSystemUser.SystemUserID + "','" + oQuestionSetup.QuestionSetupGenerationTime + "')";
                            oListStringInsertUpdate.Add(sInsert);

                            oListSuccessQuestionSetup.Add(oQuestionSetup);
                        }

                    }
                    i++;
                }

                if (oListStringInsertUpdate.Count > 0) // any question is setup
                {
                    if (oDAOUtil.ExecuteNonQuery(oListStringInsertUpdate))
                    {
                        oResult.ResultMessage = "Question Setup Success...";
                        oResult.ResultObject = oListSuccessQuestionSetup;
                        oResult.ResultIsSuccess = true;
                    }
                    else
                    {
                        oResult.ResultIsSuccess = false;
                        oResult.ResultMessage = "Question Setup Failed...";
                    }
                }
                else
                {
                    oResult.ResultIsSuccess = false;
                    oResult.ResultMessage = "Questions are all ready been Setup...";
                }
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Question Setup...";

                logger.Info("Exception QuestionSetup QuestionDAO+DAO", oEx);
            }
            finally
            {
                if (oSqlDataReader!=null && !oSqlDataReader.IsClosed)
                {
                    oSqlDataReader.Close();
                }
            }

            logger.Info("End QuestionSetup QuestionDAO+DAO");
            
            return oResult;
        }

        /// <summary>
        /// This method remove the selected questions for an exam 
        /// </summary>
        /// <param name="oListQuestionSetup"> It takes List<QuestionSetup> Object </param>
        /// <param name="iarrChecked"> It takes integer array to indicates the marked questions </param>
        /// <returns> It returns Result Object </returns>
        public Result QuestionSetupRemove(List<QuestionSetup> oListQuestionSetup, int[] iarrChecked)
        {
            logger.Info("Start QuestionSetupRemove QuestionDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            List<String> oListString = new List<String>();
            String sDelete = String.Empty;

            List<QuestionSetup> oListQuestionSetup1 = new List<QuestionSetup>();

            try
            {
                int i = 0;

                for (i = 0; i < iarrChecked.Length; i++)
                {
                    if (iarrChecked[i] == 1)
                    {
                        sDelete = "delete from EX_QuestionGeneration where ExamID='" + oListQuestionSetup[i].QuestionSetupExam.ExamID + "' and QuestionID='" + oListQuestionSetup[i].QuestionSetupQuestion.QuestionID + "' and GeneratorID='" + oListQuestionSetup[i].QuestionSetupSystemUser.SystemUserID + "'";
                        oListQuestionSetup1.Add(null);
                        oListString.Add(sDelete);
                    }
                    else
                    {
                        oListQuestionSetup1.Add(oListQuestionSetup[i]);
                    }
                }

                oListQuestionSetup1.RemoveAll(delegate(QuestionSetup oQuestionSetup) { if (oQuestionSetup != null) { return false; } return true; });

                if (oDAOUtil.ExecuteNonQuery(oListString))
                {
                    oResult.ResultObject = oListQuestionSetup1;
                    oResult.ResultMessage = "Question Setup Remove Success...";
                    oResult.ResultIsSuccess = true;
                }
                else
                {
                    oResult.ResultIsSuccess = false;
                    oResult.ResultMessage = "Question Setup Remove Failed...";
                }
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception occured during Question Setup Remove...";
                oResult.ResultException = oEx;

                logger.Info("Exception QuestionSetupRemove QuestionDAO+DAO", oEx);
            }

            logger.Info("End QuestionSetupRemove QuestionDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This method show the questions which are setup for an exm.
        /// </summary>
        /// <param name="oQuestionSetup"> It takes QuestionSetup Object </param>
        /// <returns> It returns Result Object </returns>
        public Result QuestionSetupListShow(QuestionSetup oQuestionSetup)
        {
            logger.Info("Start QuestionSetupListShow QuestionDAO+DAO");

            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            String sSelect = String.Empty;

            List<QuestionSetup> oListQuestionSetup = new List<QuestionSetup>();

            SqlDataReader oSqlDataReader = null;

            try
            {
                if (oQuestionSetup.QuestionSetupSystemUser.SystemUserName.Equals("administrator"))
                {
                    sSelect = "select EX_QuestionGeneration.ExamID,EX_QuestionGeneration.QuestionID,EX_QuestionGeneration.SetupQuestionMark,EX_QuestionGeneration.GeneratorID,EX_Question.QuestionText,EX_Question.QuestionTypeID,EX_Question.QuestionCategoryID,EX_Question.QuestionPossibleAnswerTime from EX_QuestionGeneration inner join EX_Question on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID where EX_QuestionGeneration.ExamID='" + oQuestionSetup.QuestionSetupExam.ExamID + "' and EX_Question.QuestionTypeID='" + oQuestionSetup.QuestionSetupQuestion.QuestionQuestionType.QuestionTypeID + "' and EX_Question.QuestionCategoryID='" + oQuestionSetup.QuestionSetupQuestion.QuestionCategory.CategoryID + "'";
                }
                else
                {
                    sSelect = "select EX_QuestionGeneration.ExamID,EX_QuestionGeneration.QuestionID,EX_QuestionGeneration.SetupQuestionMark,EX_QuestionGeneration.GeneratorID,EX_Question.QuestionText,EX_Question.QuestionTypeID,EX_Question.QuestionCategoryID,EX_Question.QuestionPossibleAnswerTime from EX_QuestionGeneration inner join EX_Question on EX_QuestionGeneration.QuestionID=EX_Question.QuestionID where EX_QuestionGeneration.ExamID='" + oQuestionSetup.QuestionSetupExam.ExamID + "' and EX_QuestionGeneration.GeneratorID='" + oQuestionSetup.QuestionSetupSystemUser.SystemUserID + "' and EX_Question.QuestionTypeID='" + oQuestionSetup.QuestionSetupQuestion.QuestionQuestionType.QuestionTypeID + "' and EX_Question.QuestionCategoryID='" + oQuestionSetup.QuestionSetupQuestion.QuestionCategory.CategoryID + "'";
                }

                oSqlDataReader = oDAOUtil.GetReader(sSelect);

                while (oSqlDataReader.Read())
                {
                    QuestionSetup oPopulatedQuestionSetup = new QuestionSetup();

                    oPopulatedQuestionSetup.QuestionSetupExam.ExamID = new Guid(oSqlDataReader["ExamID"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionID = new Guid(oSqlDataReader["QuestionID"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupMark = float.Parse(oSqlDataReader["SetupQuestionMark"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupSystemUser.SystemUserID = new Guid(oSqlDataReader["GeneratorID"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionText = oSqlDataReader["QuestionText"].ToString();
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionQuestionType.QuestionTypeID = int.Parse(oSqlDataReader["QuestionTypeID"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionCategory.CategoryID = int.Parse(oSqlDataReader["QuestionCategoryID"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionPossibleAnswerTime = float.Parse(oSqlDataReader["QuestionPossibleAnswerTime"].ToString());

                    oListQuestionSetup.Add(oPopulatedQuestionSetup);
                }
                
                oSqlDataReader.Close();

                oResult.ResultObject = oListQuestionSetup;
                oResult.ResultMessage = "Question Setup List Show success...";
                oResult.ResultIsSuccess = true;
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Question Setup List Show...";

                logger.Info("Exception QuestionSetupListShow QuestionDAO+DAO", oEx);
            }
            finally
            {
                if (oSqlDataReader!=null && !oSqlDataReader.IsClosed)
                {
                    oSqlDataReader.Close();
                }
            }

            logger.Info("End QuestionSetupListShow QuestionDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This method show the questions according to question level.
        /// And these questions are already been setup.
        /// </summary>
        /// <param name="oQuestionSetup"> It takes QuestionSetup Object </param>
        /// <returns> It returns Result Object </returns>
        public Result QuestionSetupListShowByQuestionLevel(QuestionSetup oQuestionSetup)
        {
            logger.Info("Start QuestionSetupListShowByQuestionLevel QuestionDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            String sSelect = String.Empty;

            List<QuestionSetup> oListQuestionSetup = new List<QuestionSetup>();

            SqlDataReader oSqlDataReader = null;

            try
            {
                if (oQuestionSetup.QuestionSetupQuestion.QuestionCreator.SystemUserName.Equals("administrator"))
                {
                    sSelect = "select EX_QuestionGeneration.ExamID,EX_QuestionGeneration.QuestionID,EX_QuestionGeneration.SetupQuestionMark,EX_QuestionGeneration.GeneratorID,"
                    +" EX_Question.QuestionText,EX_Question.QuestionTypeID,EX_Question.QuestionCategoryID,EX_Question.QuestionPossibleAnswerTime,"
                    +" EX_Question.QuestionLabelID,EX_Label.LabelName,EX_Label.LabelPrerequisite"
                    +" from EX_Label inner join EX_Question on EX_Label.LabelID=EX_Question.QuestionLabelID inner join"
                    + " EX_QuestionGeneration on EX_Question.QuestionID=EX_QuestionGeneration.QuestionID"
                    +" where EX_QuestionGeneration.ExamID='" + oQuestionSetup.QuestionSetupExam.ExamID + "' and EX_Question.QuestionTypeID='" + oQuestionSetup.QuestionSetupQuestion.QuestionQuestionType.QuestionTypeID + "'"
                    +" and EX_Question.QuestionCategoryID='" + oQuestionSetup.QuestionSetupQuestion.QuestionCategory.CategoryID + "'"
                    +" and EX_Question.QuestionLabelID='" + oQuestionSetup.QuestionSetupQuestion.QuestionLevel.LevelID+"'";
                }
                else
                {
                    sSelect = "select EX_QuestionGeneration.ExamID,EX_QuestionGeneration.QuestionID,EX_QuestionGeneration.SetupQuestionMark,EX_QuestionGeneration.GeneratorID,"
                        + " EX_Question.QuestionText,EX_Question.QuestionTypeID,EX_Question.QuestionCategoryID,EX_Question.QuestionPossibleAnswerTime,"
                        + " EX_Question.QuestionLabelID,EX_Label.LabelName,EX_Label.LabelPrerequisite"
                        + " from EX_Label inner join EX_Question on EX_Label.LabelID=EX_Question.QuestionLabelID inner join"
                        + " EX_QuestionGeneration on EX_Question.QuestionID=EX_QuestionGeneration.QuestionID"
                        +" where EX_QuestionGeneration.ExamID='" + oQuestionSetup.QuestionSetupExam.ExamID + "' and EX_QuestionGeneration.GeneratorID='" + oQuestionSetup.QuestionSetupQuestion.QuestionCreator.SystemUserID + "'"
                        +" and EX_Question.QuestionTypeID='" + oQuestionSetup.QuestionSetupQuestion.QuestionQuestionType.QuestionTypeID + "' and EX_Question.QuestionCategoryID='" + oQuestionSetup.QuestionSetupQuestion.QuestionCategory.CategoryID + "'"
                        + " and EX_Question.QuestionLabelID='" + oQuestionSetup.QuestionSetupQuestion.QuestionLevel.LevelID+"'";
                }

                oSqlDataReader = oDAOUtil.GetReader(sSelect);

                while (oSqlDataReader.Read())
                {
                    QuestionSetup oPopulatedQuestionSetup = new QuestionSetup();

                    oPopulatedQuestionSetup.QuestionSetupExam.ExamID = new Guid(oSqlDataReader["ExamID"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionID = new Guid(oSqlDataReader["QuestionID"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupMark = float.Parse(oSqlDataReader["SetupQuestionMark"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupSystemUser.SystemUserID = new Guid(oSqlDataReader["GeneratorID"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionText = oSqlDataReader["QuestionText"].ToString();
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionQuestionType.QuestionTypeID = int.Parse(oSqlDataReader["QuestionTypeID"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionCategory.CategoryID = int.Parse(oSqlDataReader["QuestionCategoryID"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionPossibleAnswerTime = float.Parse(oSqlDataReader["QuestionPossibleAnswerTime"].ToString());

                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionLevel.LevelID = new Guid(oSqlDataReader["QuestionLabelID"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionLevel.LevelName = oSqlDataReader["LabelName"].ToString();
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionLevel.LevelDescription = oSqlDataReader["LabelPrerequisite"].ToString();

                    oListQuestionSetup.Add(oPopulatedQuestionSetup);
                }

                oSqlDataReader.Close();

                oResult.ResultObject = oListQuestionSetup;
                oResult.ResultMessage = "QuestionSetupListShowByQuestionLevel success...";
                oResult.ResultIsSuccess = true;
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during QuestionSetup ListShow ByQuestionLevel...";

                logger.Info("Exception QuestionSetupListShowByQuestionLevel QuestionDAO+DAO", oEx);
            }
            finally
            {
                if (oSqlDataReader != null && !oSqlDataReader.IsClosed)
                {
                    oSqlDataReader.Close();
                }
            }

            logger.Info("End QuestionSetupListShowByQuestionLevel QuestionDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This method get total marks of questions which are set up for an exam
        /// </summary>
        /// <param name="oExam"> It takes Exam Object </param>
        /// <returns> It returns Result Object </returns>
        public Result GetTotalLastSetupQuestionMark(Exam oExam)
        {
            logger.Info("Start GetTotalLastSetupQuestionMark QuestionDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            String sSelect = String.Empty;

            Object oObject = new Object();

            float fTotalMark = 0f;

            try
            {
                sSelect = "select sum(SetupQuestionMark) from EX_QuestionGeneration where ExamID='" + oExam.ExamID+ "'";

                oObject = oDAOUtil.GetExecuteScalar(sSelect);

                if (oObject.ToString().Length > 0)
                {
                    fTotalMark = float.Parse(oObject.ToString());  
                }

                oResult.ResultObject = fTotalMark;
                oResult.ResultMessage = "GetTotalLastSetupQuestionMark Success...";
                oResult.ResultIsSuccess = true;
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during GetTotalLastSetupQuestionMark...";

                logger.Info("Exception GetTotalLastSetupQuestionMark QuestionDAO+DAO", oEx);
            }

            logger.Info("End GetTotalLastSetupQuestionMark QuestionDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This method get total possible time for answering questions which are set up for an exam
        /// </summary>
        /// <param name="oExam"> It takes Exam Object </param>
        /// <returns> It returns Result Object </returns>
        public Result GetTotalLastSetupQuestionTime(Exam oExam)
        {
            logger.Info("Start GetTotalLastSetupQuestionTime QuestionDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            String sSelect = String.Empty;

            Object oObject = new Object();

            float fTotalTime = 0f;

            try
            {
                sSelect = "select sum(EX_Question.QuestionPossibleAnswerTime) from EX_Question inner join EX_QuestionGeneration on EX_Question.QuestionID = EX_QuestionGeneration.QuestionID where EX_QuestionGeneration.ExamID='" + oExam.ExamID + "'";

                oObject = oDAOUtil.GetExecuteScalar(sSelect);

                if (oObject.ToString().Length > 0)
                {
                    fTotalTime = float.Parse(oObject.ToString());
                }

                oResult.ResultObject = fTotalTime;
                oResult.ResultMessage = "GetTotalLastSetupQuestionTime Success...";
                oResult.ResultIsSuccess = true;
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during GetTotalLastSetupQuestionTime...";

                logger.Info("Exception GetTotalLastSetupQuestionTime QuestionDAO+DAO", oEx);
            }

            logger.Info("End GetTotalLastSetupQuestionTime QuestionDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This method show the questions according to category,type,questionlevel.
        /// And these questions are shown to setup them for an exam
        /// </summary>
        /// <param name="oQuestion"> It takes Question Object </param>
        /// <param name="oExam"> It takes Exam Object </param>
        /// <returns> It returns Result Object </returns>
        public Result QuestionListShowForSetupByQuestionLevel(Question oQuestion, Exam oExam)
        {
            logger.Info("Start QuestionListShowForSetupByQuestionLevel QuestionDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            SqlDataReader oSqlDataReader = null;

            String sSelect = String.Empty;
            String sChoiceSelect = String.Empty;

            List<QuestionSetup> oListQuestionSetup = new List<QuestionSetup>();

            try
            {
                //sSelect = "select distinct QuestionID,QuestionText,QuestionCreatorID,QuestionDefaultMark,QuestionCategoryID,"
                //    + " QuestionTypeID,QuestionPossibleAnswerTime,QuestionLabelID,EX_Label.LabelName,EX_Label.LabelPrerequisite"
                //    + " from EX_Question left join EX_Label on EX_Question.QuestionLabelID=EX_Label.LabelID where QuestionCategoryID='" + oQuestion.QuestionCategory.CategoryID +
                //    "' and QuestionTypeID='" + oQuestion.QuestionQuestionType.QuestionTypeID + "'";
                
                if (oQuestion.QuestionCreator.SystemUserName.ToLower().Equals("administrator"))
                {
                    //sSelect = "select distinct EX_Question.QuestionID as EntryQuestionID,EX_QuestionGeneration.QuestionID as GeneratedQuestionID,EX_QuestionGeneration.SetupQuestionMark,QuestionText,QuestionCreatorID,QuestionDefaultMark,QuestionCategoryID,QuestionTypeID,EX_Question.QuestionPossibleAnswerTime from EX_Question left join EX_QuestionGeneration on EX_Question.QuestionID=EX_QuestionGeneration.QuestionID where EX_Question.QuestionCategoryID='" + oQuestion.QuestionCategory.CategoryID + "' and EX_Question.QuestionTypeID='" + oQuestion.QuestionQuestionType.QuestionTypeID + "'";

                    //sSelect = "select distinct QuestionID,QuestionText,QuestionCreatorID,QuestionDefaultMark,QuestionCategoryID," +
                    //"QuestionTypeID,QuestionPossibleAnswerTime,QuestionLabelID from EX_Question where QuestionCategoryID='" + oQuestion.QuestionCategory.CategoryID +
                    //"' and QuestionTypeID='" + oQuestion.QuestionQuestionType.QuestionTypeID + "' and QuestionLabelID='"+oQuestion.QuestionLevel.LevelID+"'";

                    sSelect = "select distinct QuestionID,QuestionText,QuestionCreatorID,QuestionDefaultMark,QuestionCategoryID,"
                        + " QuestionTypeID,QuestionPossibleAnswerTime,QuestionLabelID,EX_Label.LabelName,EX_Label.LabelPrerequisite"
                        + " from EX_Question inner join EX_Label on EX_Question.QuestionLabelID=EX_Label.LabelID where QuestionCategoryID='" + oQuestion.QuestionCategory.CategoryID +
                        "' and QuestionTypeID='" + oQuestion.QuestionQuestionType.QuestionTypeID + "' and QuestionLabelID='" + oQuestion.QuestionLevel.LevelID + "'";
                }
                else
                {
                    //sSelect = "select distinct EX_Question.QuestionID as EntryQuestionID,EX_QuestionGeneration.QuestionID as GeneratedQuestionID,EX_QuestionGeneration.SetupQuestionMark,QuestionText,QuestionCreatorID,QuestionDefaultMark,QuestionCategoryID,QuestionTypeID,EX_Question.QuestionPossibleAnswerTime from EX_Question left join EX_QuestionGeneration on EX_Question.QuestionID=EX_QuestionGeneration.QuestionID where EX_Question.QuestionCreatorID='" + oQuestion.QuestionCreator.SystemUserID + "' and EX_Question.QuestionCategoryID='" + oQuestion.QuestionCategory.CategoryID + "' and EX_Question.QuestionTypeID='" + oQuestion.QuestionQuestionType.QuestionTypeID + "'";

                    //sSelect = "select distinct QuestionID,QuestionText,QuestionCreatorID,QuestionDefaultMark,QuestionCategoryID," +
                    //"QuestionTypeID,QuestionPossibleAnswerTime,QuestionLabelID from EX_Question where QuestionCategoryID='" + oQuestion.QuestionCategory.CategoryID +
                    //"' and QuestionTypeID='" + oQuestion.QuestionQuestionType.QuestionTypeID + "' and QuestionLabelID='" + oQuestion.QuestionLevel.LevelID + "'";

                    sSelect = "select distinct QuestionID,QuestionText,QuestionCreatorID,QuestionDefaultMark,QuestionCategoryID,"
                        + " QuestionTypeID,QuestionPossibleAnswerTime,QuestionLabelID,EX_Label.LabelName,EX_Label.LabelPrerequisite"
                        + " from EX_Question inner join EX_Label on EX_Question.QuestionLabelID=EX_Label.LabelID where QuestionCategoryID='" + oQuestion.QuestionCategory.CategoryID +
                        "' and QuestionTypeID='" + oQuestion.QuestionQuestionType.QuestionTypeID + "' and QuestionLabelID='" + oQuestion.QuestionLevel.LevelID + "'";
                }

                oSqlDataReader = oDAOUtil.GetReader(sSelect);

                while (oSqlDataReader.Read())
                {
                    QuestionSetup oPopulatedQuestionSetup = new QuestionSetup();

                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionID = new Guid(oSqlDataReader["QuestionID"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionText = oSqlDataReader["QuestionText"].ToString();
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionCreator.SystemUserID = new Guid(oSqlDataReader["QuestionCreatorID"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionDefaultMark = float.Parse(oSqlDataReader["QuestionDefaultMark"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionCategory.CategoryID = int.Parse(oSqlDataReader["QuestionCategoryID"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionQuestionType.QuestionTypeID = int.Parse(oSqlDataReader["QuestionTypeID"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionPossibleAnswerTime = float.Parse(oSqlDataReader["QuestionPossibleAnswerTime"].ToString());

                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionLevel.LevelID = new Guid(oSqlDataReader["QuestionLabelID"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionLevel.LevelName = oSqlDataReader["LabelName"].ToString();
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionLevel.LevelDescription = oSqlDataReader["LabelPrerequisite"].ToString();
                    
                    oPopulatedQuestionSetup.QuestionSetupMark = oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionDefaultMark;
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionIsUsed = false;

                    //if (oSqlDataReader["GeneratedQuestionID"].ToString().Length > 0)
                    //{
                    //    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionIsUsed = true;
                    //}

                    //if (oSqlDataReader["SetupQuestionMark"].ToString().Length > 0)
                    //{
                    //    oPopulatedQuestionSetup.QuestionSetupMark = float.Parse(oSqlDataReader["SetupQuestionMark"].ToString());
                    //}

                    oListQuestionSetup.Add(oPopulatedQuestionSetup);
                }

                oSqlDataReader.Close();

                if (oQuestion.QuestionQuestionType.QuestionTypeID == 0) //enum should be used here
                {
                    foreach (QuestionSetup oQuestionSetupInList in oListQuestionSetup)
                    {
                        sChoiceSelect = "select ObjectiveAnswer,ObjectiveAnswerIsValid from EX_Objective where ObjectiveQuestionID='" + oQuestionSetupInList.QuestionSetupQuestion.QuestionID + "'";

                        List<Choice> oListChoice = new List<Choice>();

                        oSqlDataReader = oDAOUtil.GetReader(sChoiceSelect);

                        while (oSqlDataReader.Read())
                        {
                            //prepare the choices for a particular questtion......
                            //and populate oListQuestion
                            Choice oChoice = new Choice();

                            oChoice.ChoiceName = oSqlDataReader["ObjectiveAnswer"].ToString();
                            oChoice.ChoiceIsValid = Convert.ToBoolean(oSqlDataReader["ObjectiveAnswerIsValid"].ToString());

                            oListChoice.Add(oChoice);
                        }

                        oSqlDataReader.Close();

                        oQuestionSetupInList.QuestionSetupQuestion.QuestionObjectiveType.ListOfChoices = oListChoice;
                    }
                }


                foreach (QuestionSetup oQuestionSetupInList in oListQuestionSetup)
                {
                    sSelect = "select distinct EX_QuestionGeneration.QuestionID as GeneratedQuestionID,EX_QuestionGeneration.SetupQuestionMark from EX_QuestionGeneration where EX_QuestionGeneration.QuestionID='" + oQuestionSetupInList.QuestionSetupQuestion.QuestionID + "' and EX_QuestionGeneration.ExamID='" + oExam.ExamID + "'";

                    oSqlDataReader = oDAOUtil.GetReader(sSelect);

                    if (oSqlDataReader.HasRows)
                    {
                        oQuestionSetupInList.QuestionSetupQuestion.QuestionIsUsed = true;

                        while (oSqlDataReader.Read())
                        {
                            if (oSqlDataReader["SetupQuestionMark"].ToString().Length > 0)
                            {
                                oQuestionSetupInList.QuestionSetupMark = float.Parse(oSqlDataReader["SetupQuestionMark"].ToString());
                            }
                        }
                    }

                    oSqlDataReader.Close();
                }


                oResult.ResultObject = oListQuestionSetup;
                oResult.ResultMessage = "QuestionListShowForSetupByQuestionLevel Success...";
                oResult.ResultIsSuccess = true;
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception occured during QuestionListShowForSetupByQuestionLevel...";
                oResult.ResultException = oEx;

                logger.Info("Exception QuestionListShowForSetupByQuestionLevel QuestionDAO+DAO", oEx);
            }
            finally
            {
                if (oSqlDataReader != null && !oSqlDataReader.IsClosed)
                {
                    oSqlDataReader.Close();
                }
            }

            logger.Info("End QuestionListShowForSetupByQuestionLevel QuestionDAO+DAO");
            
            return oResult;
        }

        public Result LoadAllQuestionsOfAnExam(Exam oSelectedExam)
        {
            logger.Info("Start LoadAllQuestionsOfAnExam QuestionDAO+DAO");

            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            SqlDataReader oSqlDataReader = null;

            String sSelect = String.Empty;

            List<QuestionSetup> oListQuestionSetup = new List<QuestionSetup>();

            try
            {
                sSelect="select distinct EX_QuestionGeneration.ExamID,"
                +" EX_QuestionGeneration.SetupQuestionMark,"
                +" EX_QuestionGeneration.GeneratorID,"
                +" EX_Question.QuestionID,"
                +" EX_Question.QuestionText,EX_Question.QuestionTypeID,"
                +" EX_Question.QuestionPossibleAnswerTime,"
                +" EX_Label.LabelID,"
                +" EX_Label.LabelName,"
                +" EX_Category.CategoryID,EX_Category.CategoryName"
                +" from EX_QuestionGeneration,EX_Question,EX_Category,EX_Label"
                +" where EX_Question.QuestionLabelID=EX_Label.LabelID"
                +" and EX_Question.QuestionCategoryID=EX_Category.CategoryID"
                +" and EX_Question.QuestionID=EX_QuestionGeneration.QuestionID"
                +" and EX_QuestionGeneration.ExamID='" + oSelectedExam.ExamID + "'"
                +" order by EX_Category.CategoryName,EX_Label.LabelName,EX_Question.QuestionTypeID asc";

                oSqlDataReader = oDAOUtil.GetReader(sSelect);

                while (oSqlDataReader.Read())
                {
                    QuestionSetup oPopulatedQuestionSetup = new QuestionSetup();

                    oPopulatedQuestionSetup.QuestionSetupExam.ExamID = new Guid(oSqlDataReader["ExamID"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupMark = float.Parse(oSqlDataReader["SetupQuestionMark"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupSystemUser.SystemUserID = new Guid(oSqlDataReader["GeneratorID"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionID = new Guid(oSqlDataReader["QuestionID"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionText = oSqlDataReader["QuestionText"].ToString();
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionQuestionType.QuestionTypeID = int.Parse(oSqlDataReader["QuestionTypeID"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionPossibleAnswerTime = float.Parse(oSqlDataReader["QuestionPossibleAnswerTime"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionLevel.LevelID = new Guid(oSqlDataReader["LabelID"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionLevel.LevelName = oSqlDataReader["LabelName"].ToString();
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionCategory.CategoryID = int.Parse(oSqlDataReader["CategoryID"].ToString());
                    oPopulatedQuestionSetup.QuestionSetupQuestion.QuestionCategory.CategoryName = oSqlDataReader["CategoryName"].ToString();

                    oListQuestionSetup.Add(oPopulatedQuestionSetup);
                }

                oSqlDataReader.Close();


                foreach (QuestionSetup oQuestionSetupInList in oListQuestionSetup)
                {
                    if (oQuestionSetupInList.QuestionSetupQuestion.QuestionQuestionType.QuestionTypeID == 0)
                    {
                        sSelect = "select EX_Objective.ObjectiveQuestionID,EX_Objective.ObjectiveAnswer,"
                        +" EX_Objective.ObjectiveAnswerIsValid from EX_Objective"
                        + " where EX_Objective.ObjectiveQuestionID = '" + oQuestionSetupInList.QuestionSetupQuestion.QuestionID + "'";

                        List<Choice> oListChoice = new List<Choice>();
                        
                        oSqlDataReader = oDAOUtil.GetReader(sSelect);

                        while (oSqlDataReader.Read())
                        {
                            Choice oChoice = new Choice();
                            
                            oChoice.ChoiceName = oSqlDataReader["ObjectiveAnswer"].ToString();
                            oChoice.ChoiceIsValid = Convert.ToBoolean(oSqlDataReader["ObjectiveAnswerIsValid"].ToString());

                            oListChoice.Add(oChoice);
                        }

                        oSqlDataReader.Close();

                        oQuestionSetupInList.QuestionSetupQuestion.QuestionObjectiveType.ListOfChoices = oListChoice;
                    }
   
                }


                oResult.ResultObject = oListQuestionSetup;
                oResult.ResultMessage = "LoadAllQuestionsOfAnExam success...";
                oResult.ResultIsSuccess = true;

            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Exception occured during LoadAllQuestionsOfAnExam...";
                oResult.ResultException = oEx;

                logger.Info("Exception LoadAllQuestionsOfAnExam QuestionDAO+DAO", oEx);
            }
            finally
            {
                if (oSqlDataReader != null && !oSqlDataReader.IsClosed)
                {
                    oSqlDataReader.Close();
                }
            }
            
            logger.Info("End LoadAllQuestionsOfAnExam QuestionDAO+DAO");

            return oResult;
        }
    }
}
