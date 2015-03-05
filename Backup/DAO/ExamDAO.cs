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
    /// This class is used for Exam manipulation
    /// </summary>
    public class ExamDAO
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(ExamDAO));
        
        public ExamDAO()
        {
            log4net.Config.XmlConfigurator.Configure();
        }


        /// <summary>
        /// This method inserts an exam
        /// </summary>
        /// <param name="oExam"> It takes Exam Object </param>
        /// <returns> It returns Result Object </returns>
        public Result ExamEntry(Exam oExam) //r
        {
            logger.Info("Start ExamEntry ExamDAO+DAO");
            
            Result oResult = new Result();
            SqlDataReader oSqlDataReader = null;
            
            try
            {
                DAOUtil oDAOUtil = new DAOUtil();
                                
                Boolean flag = true;

                String sSelect = "select ExamID from EX_Exam where ExamName='" + oExam.ExamName + "' and ExamDateWithTime='" + oExam.ExamDateWithStartingTime + "'";
                String sInsert = String.Empty;
                List<String> oListOfInsert = new List<String>();

                oSqlDataReader = oDAOUtil.GetReader(sSelect);

                if (oSqlDataReader.HasRows)
                {
                    flag = false;
                }

                oSqlDataReader.Close();

                if (flag)
                {
                    oExam.ExamID = Guid.NewGuid();
                    sInsert = "insert into EX_Exam(ExamID,ExamName,ExamTotalMarks,ExamDateWithTime,ExamDuration,ExamConstraint) values('" + oExam.ExamID + "','" + oExam.ExamName + "','" + oExam.ExamTotalMarks + "','" + oExam.ExamDateWithStartingTime + "','" + oExam.ExamDurationinHour + "','" + oExam.ExamConstraint + "')";
                    oListOfInsert.Add(sInsert);

                    if (oDAOUtil.ExecuteNonQuery(oListOfInsert))
                    {
                        oResult.ResultObject = oExam;
                        oResult.ResultMessage = "Exam Entry Success...";
                        oResult.ResultIsSuccess = true;

                    }
                    else
                    {
                        oResult.ResultIsSuccess = false;
                        oResult.ResultMessage = "Exam Entry Failed...";
                    }
                }
                else
                {
                    oResult.ResultIsSuccess = false;
                    oResult.ResultMessage = oExam.ExamName + "is already scheduled at " + oExam.ExamDateWithStartingTime+"...";
                }
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Exam Entry...";

                logger.Info("Exception ExamEntry ExamDAO+DAO", oEx);
            }
            finally
            {
                if (oSqlDataReader!=null && !oSqlDataReader.IsClosed)
                {
                    oSqlDataReader.Close();
                }
            }

            logger.Info("End ExamEntry ExamDAO+DAO");
            
            return oResult;
        }

        /// <summary>
        /// This method load all the exams for set session
        /// </summary>
        /// <returns> It returns Result Object </returns>
        public Result ExamGetFromDatabaseForSetSession() //r
        {
            logger.Info("Start ExamGetFromDatabaseForSetSession ExamDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            String sSelectExam= String.Empty;
            
            SqlDataReader oSqlDataReader = null;

            List<Exam> oListoExam = new List<Exam>();

            sSelectExam = "select ExamID,ExamName,ExamTotalMarks,ExamDateWithTime,ExamDuration,ExamConstraint from EX_Exam order by ExamDateWithTime desc,ExamName asc";

            try
            {
                oSqlDataReader = oDAOUtil.GetReader(sSelectExam);

                while (oSqlDataReader.Read())
                {
                    Exam oExam = new Exam();

                    oExam.ExamID = new Guid(oSqlDataReader["ExamID"].ToString());
                    oExam.ExamName = oSqlDataReader["ExamName"].ToString();
                    oExam.ExamTotalMarks = int.Parse(oSqlDataReader["ExamTotalMarks"].ToString());
                    oExam.ExamDateWithStartingTime = DateTime.Parse(oSqlDataReader["ExamDateWithTime"].ToString());
                    oExam.ExamDurationinHour = float.Parse(oSqlDataReader["ExamDuration"].ToString());
                    oExam.ExamConstraint = int.Parse(oSqlDataReader["ExamConstraint"].ToString());

                    oListoExam.Add(oExam);
                }

                oSqlDataReader.Close();

                oResult.ResultObject = oListoExam;
                oResult.ResultMessage = "Exam Get success...";
                oResult.ResultIsSuccess = true;
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Exam Get...";

                logger.Info("Exception ExamGetFromDatabaseForSetSession ExamDAO+DAO", oEx);
            }
            finally
            {
                if (oSqlDataReader!=null && !oSqlDataReader.IsClosed)
                {
                    oSqlDataReader.Close();
                }
            }

            logger.Info("End ExamGetFromDatabaseForSetSession ExamDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This method updates an exam information
        /// </summary>
        /// <param name="oExam"> It takes Exam Object </param>
        /// <returns> It returns Result Object </returns>
        public Result ExamModification(Exam oExam) //r
        {
            logger.Info("Start ExamModification ExamDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            String sUpadte = String.Empty;
            String sSelect = String.Empty;

            List<String> oListString = new List<String>();

            SqlDataReader oSqlDataReader = null;

            Boolean flag = true;

            try
            {
                sSelect = "select ExamID from EX_QuestionGeneration where ExamID='" + oExam.ExamID + "'";
                
                oSqlDataReader = oDAOUtil.GetReader(sSelect);

                if (oSqlDataReader.HasRows)
                {
                    flag = false;
                }

                oSqlDataReader.Close();

                if (flag)
                {
                    sUpadte = "update EX_Exam set ExamTotalMarks='" + oExam.ExamTotalMarks + "',ExamDateWithTime='" + oExam.ExamDateWithStartingTime + "',ExamDuration='" + oExam.ExamDurationinHour + "',ExamConstraint='" + oExam.ExamConstraint + "' where ExamID='"+oExam.ExamID+"'";

                    oListString.Add(sUpadte);

                    if (oDAOUtil.ExecuteNonQuery(oListString))
                    {
                        oResult.ResultMessage = "Exam Update Success...";
                        oResult.ResultObject = oExam;
                        oResult.ResultIsSuccess = true;
                    }
                    else
                    {
                        oResult.ResultIsSuccess = false;
                        oResult.ResultMessage = "Exam Update Failed...";
                    }

                }
                else
                {
                    oResult.ResultIsSuccess = false;
                    oResult.ResultMessage = "The Exam is used for Setup Question...";
                }
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Exam Update...";

                logger.Info("Exception ExamModification ExamDAO+DAO", oEx);
            }
            finally
            {
                if (oSqlDataReader!=null && !oSqlDataReader.IsClosed)
                {
                    oSqlDataReader.Close();
                }
            }

            logger.Info("End ExamModification ExamDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This method deletes an exam if no question is setup for this exam
        /// </summary>
        /// <param name="oExam"> It takes Exam Object </param>
        /// <returns> It returns Result Object </returns>
        public Result ExamDelte(Exam oExam) //r
        {
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            String sDelete = String.Empty;
            String sSelect = String.Empty;

            List<String> oListString = new List<String>();

            SqlDataReader oSqlDataReader = null;

            Boolean flag = true;

            try
            {
                sSelect = "select ExamID from EX_QuestionGeneration where ExamID='" + oExam.ExamID + "'";

                oSqlDataReader = oDAOUtil.GetReader(sSelect);

                if (oSqlDataReader.HasRows)
                {
                    flag = false;
                }

                oSqlDataReader.Close();

                if (flag)
                {
                    sDelete = "delete from EX_Exam where ExamID='" + oExam.ExamID + "'";

                    oListString.Add(sDelete);

                    if (oDAOUtil.ExecuteNonQuery(oListString))
                    {
                        oResult.ResultMessage = "Exam Delete Success...";
                        oResult.ResultObject = oExam;
                        oResult.ResultIsSuccess = true;
                    }
                    else
                    {
                        oResult.ResultIsSuccess = false;
                        oResult.ResultMessage = "Exam Delete Failed...";
                    }
                }
                else
                {
                    oResult.ResultIsSuccess = false;
                    oResult.ResultMessage = "The Exam is used for Setup Question...";
                }
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Exam Delete...";
            }
            finally
            {
                if (oSqlDataReader!=null && !oSqlDataReader.IsClosed)
                {
                    oSqlDataReader.Close();
                }
            }

            return oResult;
        }

        /// <summary>
        /// This method deletes an exam if this exam is not used for any entity
        /// </summary>
        /// <param name="oExam"> It takes Exam Object </param>
        /// <param name="oSystemUser"> It takes SystemUser Object. Only administrator can delete an exam </param>
        /// <returns> It returns Result Object </returns>
        public Result ExamDeleteByStoredProcedure(Exam oExam,SystemUser oSystemUser) //r
        {
            logger.Info("Start ExamDeleteByStoredProcedure ExamDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            List<SqlCommand> oListSqlCommand = new List<SqlCommand>();
            List<int> oListInt = new List<int>();

            bool bExamDeleted = false;

            try
            {
                SqlCommand oSqlCommand = new SqlCommand("SP_ExamDelete");
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@ExamID", SqlDbType.UniqueIdentifier);
                oSqlCommand.Parameters["@ExamID"].Value = oExam.ExamID;
                oSqlCommand.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier);
                oSqlCommand.Parameters["@UserID"].Value = oSystemUser.SystemUserID;
                oSqlCommand.Parameters.Add("@UserName", SqlDbType.VarChar);
                oSqlCommand.Parameters["@UserName"].Value = oSystemUser.SystemUserName;
                oSqlCommand.Parameters.Add("@UserPassword", SqlDbType.VarChar);
                oSqlCommand.Parameters["@UserPassword"].Value = oSystemUser.SystemUserPassword;

                oListSqlCommand.Add(oSqlCommand);

                oListInt = oDAOUtil.ExecuteNonQueryForStoredProcedure(oListSqlCommand);

                if (oListInt.Count > 0)
                {
                    if (oListInt[0] > 0)
                    {
                        bExamDeleted = true;
                    }

                    if (bExamDeleted)
                    {
                        oResult.ResultMessage = "Exam Delete Success...";
                        oResult.ResultObject = oExam;
                        oResult.ResultIsSuccess = true;
                    }
                    else
                    {
                        oResult.ResultIsSuccess = false;
                        oResult.ResultMessage = "The Exam is used...";
                    }
                }
                else
                {
                    oResult.ResultIsSuccess = false;
                    oResult.ResultMessage = "Exam Delete Failed...";
                }
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Exam Delete...";

                logger.Info("Exception ExamDeleteByStoredProcedure ExamDAO+DAO", oEx);
            }

            logger.Info("End ExamDeleteByStoredProcedure ExamDAO+DAO");

            return oResult;
        }
    }
}
