using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Common;
using Entity;
using Utility;
//using Logging;
using log4net;
using log4net.Config;

namespace DAO
{
    /// <summary>
    /// This class is used for Candidate manipulation
    /// </summary>
    public class CandidateDAO
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(CandidateDAO));
        
        public CandidateDAO()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// This Method Setup Candidate for an exam.
        /// </summary>
        /// <param name="oCandidateForExam"> It takes CandidateForExam Object </param>
        /// <returns> It returns Result Object </returns>
        public Result CandidateSetup(CandidateForExam oCandidateForExam) //r
        {
            //new CLogger("Start CandidateSetup CandidateDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start CandidateSetup CandidateDAO+DAO", ELogLevel.Debug);

            logger.Info("Start CandidateSetup CandidateDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            String sInsert = String.Empty;

            //List<String> oListString = new List<String>();


            SqlCommand oSqlCommand = null;

            List<SqlCommand> oListSqlCommand = new List<SqlCommand>();
            List<int> oListInt = new List<int>();

            try
            {
                //sInsert = "DECLARE @rows int SET @rows = (SELECT  max(CandidateIDInt) from EX_Candidate) if @rows is null insert into EX_Candidate(ExamID,CompositeCandidateID,CandidatePassword,Name,LastResult,LastInstitution,LastPassingYear,CvPath,CandidateID) values('" + oCandidate.CadidateCandidateExam.CandiadteExamExam.ExamID + "','" + oCandidate.CandidateName + "_1','" + oCandidate.CandidateName + "_1','" + oCandidate.CandidateName + "','" + oCandidate.CandidateLastResult + "','" + oCandidate.CandiadteLastInstitution + "','" + oCandidate.CandidateLastPassingYear + "','" + oCandidate.CandidateCvPath + "','" + oCandidate.CandidateID + "')  else insert into EX_Candidate(ExamID,CompositeCandidateID,CandidatePassword,Name,LastResult,LastInstitution,LastPassingYear,CvPath,CandidateID) values('" + oCandidate.CadidateCandidateExam.CandiadteExamExam.ExamID + "','" + oCandidate.CandidateName + "_'" + "+Convert(varchar(50),@rows+1),'" + oCandidate.CandidateName + "_'" + "+Convert(varchar(50),@rows+1),'" + oCandidate.CandidateName + "','" + oCandidate.CandidateLastResult + "','" + oCandidate.CandiadteLastInstitution + "','" + oCandidate.CandidateLastPassingYear + "','" + oCandidate.CandidateCvPath + "','" + oCandidate.CandidateID + "')";     //values('abc' + Convert(varchar(50),@rows+1))";

                //sInsert = "insert into EX_Candidate(CompositeCandidateID,CandidatePassword,Name,LastResult,LastInstitution,LastPassingYear,CvPath,EmailAddress,LastResultRange,LastResultTypeName,CandidatePicturePath) values('" + oCandidateForExam.CandidateForExamCandidate.CandidateCompositeID + "','" + oCandidateForExam.CandidateForExamCandidate.CandidatePassword + "','" + oCandidateForExam.CandidateForExamCandidate.CandidateName + "','" + oCandidateForExam.CandidateForExamCandidate.CandidateLastResult + "','" + oCandidateForExam.CandidateForExamCandidate.CandiadteLastInstitution + "','" + oCandidateForExam.CandidateForExamCandidate.CandidateLastPassingYear + "','" + oCandidateForExam.CandidateForExamCandidate.CandidateCvPath + "','" + oCandidateForExam.CandidateForExamCandidate.CandidateEmail + "','" + oCandidateForExam.CandidateForExamCandidate.CandidateLastResultRange + "','" + oCandidateForExam.CandidateForExamCandidate.LastResultTypaName + "','" + oCandidateForExam.CandidateForExamCandidate.CandidatePicturePath + "')";
                //oListString.Add(sInsert);
                //sInsert = "insert into EX_CandidateForExam(CandidateID,ExamID) values('" + oCandidateForExam.CandidateForExamCandidate.CandidateCompositeID + "','" + oCandidateForExam.CadidateCandidateExam.CandiadteExamExam.ExamID + "')";
                //oListString.Add(sInsert);

                //if (oDAOUtil.ExecuteNonQuery(oListString))
                //{
                //    oResult.ResultMessage = "Candidate Setup Success...";
                //    oResult.ResultObject = oCandidateForExam;
                //    oResult.ResultIsSuccess = true;
                //}
                //else
                //{
                //    oResult.ResultIsSuccess = false;
                //    oResult.ResultMessage = "Candidate Setup Failed...";
                //}

                oSqlCommand = new SqlCommand("SP_CandidateSetup");
                oSqlCommand.CommandType = CommandType.StoredProcedure;

                oSqlCommand.Parameters.Add("@CompositeCandidateID", SqlDbType.VarChar);
                oSqlCommand.Parameters["@CompositeCandidateID"].Value = oCandidateForExam.CandidateForExamCandidate.CandidateCompositeID;

                oSqlCommand.Parameters.Add("@CandidatePassword", SqlDbType.VarChar);
                oSqlCommand.Parameters["@CandidatePassword"].Value = oCandidateForExam.CandidateForExamCandidate.CandidatePassword;

                oSqlCommand.Parameters.Add("@Name", SqlDbType.VarChar);
                oSqlCommand.Parameters["@Name"].Value = oCandidateForExam.CandidateForExamCandidate.CandidateName;

                oSqlCommand.Parameters.Add("@LastResult", SqlDbType.Float);
                oSqlCommand.Parameters["@LastResult"].Value = oCandidateForExam.CandidateForExamCandidate.CandidateLastResult;

                oSqlCommand.Parameters.Add("@LastInstitution", SqlDbType.VarChar);
                oSqlCommand.Parameters["@LastInstitution"].Value = oCandidateForExam.CandidateForExamCandidate.CandiadteLastInstitution;

                oSqlCommand.Parameters.Add("@LastPassingYear", SqlDbType.Int);
                oSqlCommand.Parameters["@LastPassingYear"].Value = oCandidateForExam.CandidateForExamCandidate.CandidateLastPassingYear;

                oSqlCommand.Parameters.Add("@CvPath", SqlDbType.VarChar);
                oSqlCommand.Parameters["@CvPath"].Value = oCandidateForExam.CandidateForExamCandidate.CandidateCvPath;

                oSqlCommand.Parameters.Add("@EmailAddress", SqlDbType.VarChar);
                oSqlCommand.Parameters["@EmailAddress"].Value = oCandidateForExam.CandidateForExamCandidate.CandidateEmail;

                oSqlCommand.Parameters.Add("@LastResultRange", SqlDbType.Float);
                oSqlCommand.Parameters["@LastResultRange"].Value = oCandidateForExam.CandidateForExamCandidate.CandidateLastResultRange;

                oSqlCommand.Parameters.Add("@LastResultTypeName", SqlDbType.VarChar);
                oSqlCommand.Parameters["@LastResultTypeName"].Value = oCandidateForExam.CandidateForExamCandidate.LastResultTypaName;

                oSqlCommand.Parameters.Add("@CandidatePicturePath", SqlDbType.VarChar);
                oSqlCommand.Parameters["@CandidatePicturePath"].Value = oCandidateForExam.CandidateForExamCandidate.CandidatePicturePath;
                
                
                
                oSqlCommand.Parameters.Add("@CandidateID", SqlDbType.VarChar);
                oSqlCommand.Parameters["@CandidateID"].Value = oCandidateForExam.CandidateForExamCandidate.CandidateCompositeID;

                oSqlCommand.Parameters.Add("@ExamID", SqlDbType.UniqueIdentifier);
                oSqlCommand.Parameters["@ExamID"].Value = oCandidateForExam.CadidateCandidateExam.CandiadteExamExam.ExamID;

                oListSqlCommand.Add(oSqlCommand);

                oListInt = oDAOUtil.ExecuteNonQueryForStoredProcedure(oListSqlCommand);

                if (oListInt.Count > 0)
                {
                    if (oListInt[0] > 0 )
                    {
                        oResult.ResultMessage = "Candidate Setup Success...";
                        oResult.ResultObject = oCandidateForExam;
                        oResult.ResultIsSuccess = true;
                    }
                    else
                    {
                        oResult.ResultIsSuccess = false;
                        oResult.ResultMessage = "Duplicate Candidate Email not allowed...";
                    }
                }
                else
                {
                    oResult.ResultIsSuccess = false;
                    oResult.ResultMessage = "Candidate Setup Failed...";
                }
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Candidate Setup Exception...";

                logger.Info("Exception CandidateSetup CandidateDAO+DAO", oEx);

                //new CLogger("Exception CandidateSetup CandidateDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception CandidateSetup CandidateDAO+DAO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out CandidateSetup CandidateDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out CandidateSetup CandidateDAO+DAO", ELogLevel.Debug); ;

            logger.Info("End CandidateSetup CandidateDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This Method varifies CandidateLogin. If he is not appeared for an exam then Login is success. 
        /// He must login between an exam duration
        /// </summary>
        /// <param name="oCandidate"> It takes Candidate Object </param>
        /// <returns> It returns Result Object </returns>
        public Result CandidateLogin(Candidate oCandidate)
        {
            //new CLogger("Start CandidateLogin CandidateDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start CandidateLogin CandidateDAO+DAO", ELogLevel.Debug);

            logger.Info("Start CandidateLogin CandidateDAO+DAO");
            
            Result oResult = new Result();
            //Candidate oPopulatedCandidate = new Candidate();
            CandidateForExam oCandidateForExam = new CandidateForExam();
            Exam oPopulatedExam = new Exam();
            DAOUtil oDAOUtil = new DAOUtil();

            String sSelect = String.Empty;

            SqlDataReader oSqlDataReader = null;

            bool bIsCandidateFound = false;
            bool bIsBetweenLoginTime = false;

            //sSelect = "select EX_Exam.ExamDateWithTime,EX_Exam.ExamDuration,EX_Candidate.ExamID,EX_Candidate.CompositeCandidateID,EX_Candidate.CandidatePassword,EX_Candidate.Name,EX_Candidate.LastResult,EX_Candidate.LastInstitution,EX_Candidate.LastPassingYear,EX_Candidate.CvPath,EX_Candidate.CandidateIDInt,EX_Candidate.CandidateID from EX_Candidate,EX_Exam where ExamID in (select ExamID from EX_Candidate where EX_Candidate.CompositeCandidateID='" + oCandidate.CandidateCompositeID + "' and EX_Candidate.CandidatePassword='" + oCandidate.CandidatePassword + "')";

            //sSelect = "select EX_Exam.ExamName,EX_Exam.ExamDateWithTime,EX_Exam.ExamDuration,EX_Exam.ExamTotalMarks,EX_Exam.ExamConstraint,EX_Exam.ExamID,"
            //+" EX_Candidate.CompositeCandidateID,EX_Candidate.CandidatePassword,EX_Candidate.Name,EX_Candidate.LastResult,EX_Candidate.LastInstitution,EX_Candidate.LastPassingYear,EX_Candidate.CvPath"
            //+" from EX_Candidate inner join EX_CandidateForExam on EX_Candidate.CompositeCandidateID=EX_CandidateForExam.CandidateID"
            //+" inner join EX_Exam on EX_CandidateForExam.ExamID=EX_Exam.ExamID"
            //+" where EX_Candidate.EmailAddress='" + oCandidate.CandidateEmail + "' and EX_Candidate.CandidatePassword='" + oCandidate.CandidatePassword + "'"
            //+" and EX_Candidate.CompositeCandidateID not in" 
            //+" (select EX_CandidateExam.CandidateID from"
            //+" EX_Candidate inner join EX_CandidateForExam on EX_Candidate.CompositeCandidateID =EX_CandidateForExam.CandidateID"
            //+" inner join EX_CandidateExam on EX_CandidateForExam.ExamID=EX_CandidateExam.ExamID"
            //+" where EX_Candidate.EmailAddress='" + oCandidate.CandidateEmail + "')";

            sSelect = " select EX_Exam.ExamName,EX_Exam.ExamDateWithTime,EX_Exam.ExamDuration,EX_Exam.ExamTotalMarks,"
            + " EX_Exam.ExamConstraint,EX_Exam.ExamID, EX_Candidate.CompositeCandidateID,EX_Candidate.CandidatePassword,"
            + " EX_Candidate.Name,EX_Candidate.LastResult,EX_Candidate.LastInstitution,EX_Candidate.LastPassingYear,EX_Candidate.CvPath"
            + " from EX_Candidate inner join EX_CandidateForExam on EX_Candidate.CompositeCandidateID=EX_CandidateForExam.CandidateID"
            + " inner join EX_Exam on EX_CandidateForExam.ExamID=EX_Exam.ExamID"
            + " where EX_Candidate.EmailAddress='" + oCandidate.CandidateEmail + "' and"
            + " EX_Candidate.CandidatePassword='" + oCandidate.CandidatePassword + "' and EX_CandidateForExam.CandidateID not in"
            + " (select EX_CandidateExam.CandidateID from EX_CandidateExam where"
            + " EX_CandidateExam.ExamID=EX_CandidateForExam.ExamID and EX_CandidateExam.CandidateID=EX_CandidateForExam.CandidateID)";

            try
            {
                DateTime oDateTime = DateTime.MinValue;
                float fDuration = 0f;
                Guid gExamID = Guid.Empty;

                oSqlDataReader = oDAOUtil.GetReader(sSelect);

                while (oSqlDataReader.Read())
                {
                    oDateTime =  DateTime.Parse(oSqlDataReader["ExamDateWithTime"].ToString());
                    fDuration = float.Parse(oSqlDataReader["ExamDuration"].ToString());
                    gExamID = new Guid(oSqlDataReader["ExamID"].ToString());

                    oPopulatedExam.ExamID = gExamID;
                    oPopulatedExam.ExamConstraint = int.Parse(oSqlDataReader["ExamConstraint"].ToString());
                    oPopulatedExam.ExamTotalMarks = int.Parse(oSqlDataReader["ExamTotalMarks"].ToString());
                    oPopulatedExam.ExamDateWithStartingTime = oDateTime;
                    oPopulatedExam.ExamDurationinHour = fDuration;
                    oPopulatedExam.ExamName = oSqlDataReader["ExamName"].ToString();

                    oCandidateForExam.CadidateCandidateExam.CandiadteExamExam = oPopulatedExam;
                    
                    oCandidateForExam.CandidateForExamCandidate.CandidateCompositeID = oSqlDataReader["CompositeCandidateID"].ToString();
                    oCandidateForExam.CandidateForExamCandidate.CandidatePassword = oSqlDataReader["CandidatePassword"].ToString();
                    oCandidateForExam.CandidateForExamCandidate.CandidateName = oSqlDataReader["Name"].ToString();
                    oCandidateForExam.CandidateForExamCandidate.CandidateLastResult = float.Parse(oSqlDataReader["LastResult"].ToString());
                    oCandidateForExam.CandidateForExamCandidate.CandiadteLastInstitution = oSqlDataReader["LastInstitution"].ToString();
                    oCandidateForExam.CandidateForExamCandidate.CandidateLastPassingYear = int.Parse(oSqlDataReader["LastPassingYear"].ToString());
                    oCandidateForExam.CandidateForExamCandidate.CandidateCvPath = oSqlDataReader["CvPath"].ToString();

                    if (oDateTime != null && fDuration > 0f)
                    {
                        TimeSpan oTimeSpan = new TimeSpan();

                        oTimeSpan = oCandidate.CandidateLoginTime - oDateTime;

                        Double dTimeDifference = oTimeSpan.TotalHours;

                        if ((float)dTimeDifference < fDuration && (float)dTimeDifference >= 0f)
                        {
                            //oResult.ResultMessage = "Candidate Login Success...";
                            //oResult.ResultObject = oCandidateForExam;
                            //oResult.ResultIsSuccess = true;
                            bIsCandidateFound = true;
                            bIsBetweenLoginTime = true;
                            break;
                        }
                        //else
                        //{
                        //    bIsCandidateFound = true;
                        //}
                    }
                }
                
                oSqlDataReader.Close();


                if (bIsCandidateFound && bIsBetweenLoginTime)
                {
                    oResult.ResultMessage = "Candidate Login Success...";
                    oResult.ResultObject = oCandidateForExam;
                    oResult.ResultIsSuccess = true;
                }
                else
                {
                    oResult.ResultIsSuccess = false;
                    oResult.ResultMessage = "Candidate not found...";
                }
                //else if (bIsCandidateFound && !bIsBetweenLoginTime)
                //{
                //    oResult.ResultIsSuccess = false;
                //    oResult.ResultMessage = "Login Between Exam Time...";
                //}
                //else if (!bIsCandidateFound)
                //{
                //    oResult.ResultIsSuccess = false;
                //    oResult.ResultMessage = "Candidate not found...";
                //}


                //if (oDateTime != null && fDuration > 0f)
                //{
                //    TimeSpan oTimeSpan = new TimeSpan();

                //    oTimeSpan = oCandidate.CandidateLoginTime - oDateTime;

                //    Double dTimeDifference=oTimeSpan.TotalHours;

                //    if ((float)dTimeDifference < fDuration && (float)dTimeDifference >= 0f)
                //    {
                //        oResult.ResultMessage = "Candidate Login Success...";
                //        oResult.ResultObject = oCandidateForExam;
                //        oResult.ResultIsSuccess = true;
                //    }
                //    else
                //    {
                //        oResult.ResultIsSuccess = false;
                //        oResult.ResultMessage = "Login between Exam time...";
                //    }
                //}
                //else
                //{
                //    oResult.ResultIsSuccess = false;
                //    oResult.ResultMessage = "Candidate not found...";
                //}
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Candidate Login Exception...";

                logger.Info("Exception CandidateLogin CandidateDAO+DAO", oEx);

                //new CLogger("Exception CandidateLogin CandidateDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception CandidateLogin CandidateDAO+DAO", ELogLevel.Debug, oEx);
            }
            finally
            {
                if (oSqlDataReader!=null && !oSqlDataReader.IsClosed)
                {
                    oSqlDataReader.Close();
                }
            }

            //new CLogger("Out CandidateLogin CandidateDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out CandidateLogin CandidateDAO+DAO", ELogLevel.Debug); ;

            logger.Info("End CandidateLogin CandidateDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This Method Show All Candidates with their information for an Exam. 
        /// </summary>
        /// <param name="oExam"> It takes Exam Object </param>
        /// <returns> It returns Result Object </returns>
        public Result ShowAllCandidates(Exam oExam)
        {
            //new CLogger("Start ShowAllCandidates CandidateDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start ShowAllCandidates CandidateDAO+DAO", ELogLevel.Debug);

            logger.Info("Start ShowAllCandidates CandidateDAO+DAO");

            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            List<CandidateForExam> oListCandidateForExam = new List<CandidateForExam>();
            
            String sSelect = String.Empty;

            SqlDataReader oSqlDataReader = null;

            try
            {
                sSelect = "select EX_CandidateForExam.ExamID,CompositeCandidateID,CandidatePassword,Name,"
                + " LastResult,LastResultRange,LastResultTypeName,LastInstitution,LastPassingYear,CvPath,EmailAddress,CandidatePicturePath"
                +" from EX_Candidate inner join EX_CandidateForExam on EX_Candidate.CompositeCandidateID=EX_CandidateForExam.CandidateID"
                +" where EX_CandidateForExam.ExamID='" + oExam.ExamID + "'";

                oSqlDataReader = oDAOUtil.GetReader(sSelect);

                while (oSqlDataReader.Read())
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
                    oCandidateForExam.CandidateForExamCandidate.CandidateEmail = oSqlDataReader["EmailAddress"].ToString();

                    oCandidateForExam.CandidateForExamCandidate.CandidateLastResultRange = float.Parse(oSqlDataReader["LastResultRange"].ToString());
                    oCandidateForExam.CandidateForExamCandidate.LastResultTypaName = oSqlDataReader["LastResultTypeName"].ToString();
                    oCandidateForExam.CandidateForExamCandidate.CandidatePicturePath = oSqlDataReader["CandidatePicturePath"].ToString();

                    oListCandidateForExam.Add(oCandidateForExam);
                }

                oSqlDataReader.Close();

                oResult.ResultObject = oListCandidateForExam;
                oResult.ResultMessage = "Candidate Load Success...";
                oResult.ResultIsSuccess = true;
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultMessage = "Candidate Load Failed...";
                oResult.ResultException = oEx;

                logger.Info("Exception ShowAllCandidates CandidateDAO+DAO", oEx);

                //new CLogger("Exception ShowAllCandidates CandidateDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception ShowAllCandidates CandidateDAO+DAO", ELogLevel.Debug, oEx);
            }
            finally
            {
                if (oSqlDataReader!=null && !oSqlDataReader.IsClosed)
                {
                    oSqlDataReader.Close();
                }
            }

            //new CLogger("Out ShowAllCandidates CandidateDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out ShowAllCandidates CandidateDAO+DAO", ELogLevel.Debug);

            logger.Info("End ShowAllCandidates CandidateDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This Method remove a candidate if he has not appeared for an exam. 
        /// </summary>
        /// <param name="oListCandidateForExam"> It takes List<CandidateForExam> Object </param>
        /// <param name="iArrCheck"> It is an array of Integer which indicates the marked candidates to remove</param>
        /// <returns> It returns Result Object </returns>
        public Result RemoveCandidate(List<CandidateForExam> oListCandidateForExam, int[] iArrCheck)
        {
            //new CLogger("Start RemoveCandidate CandidateDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start RemoveCandidate CandidateDAO+DAO", ELogLevel.Debug);

            logger.Info("Start RemoveCandidate CandidateDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            List<CandidateForExam> oListCandidateForExam1 = new List<CandidateForExam>();

            List<String> oListString = new List<String>();

            String sDelete = String.Empty;

            int i=0;

            try
            {
                for (i = 0; i < iArrCheck.Length; i++)
                {
                    if (iArrCheck[i] == 1)
                    {
                        sDelete = "delete from EX_CandidateForExam where"
                        + " EX_CandidateForExam.ExamID='" + oListCandidateForExam[i].CadidateCandidateExam.CandiadteExamExam.ExamID + "'"
                        + " and EX_CandidateForExam.CandidateID='" + oListCandidateForExam[i].CandidateForExamCandidate.CandidateCompositeID + "'"
                        + " and EX_CandidateForExam.CandidateID not in (select EX_CandidateExam.CandidateID"
                        + " from EX_CandidateExam where EX_CandidateExam.ExamID='" + oListCandidateForExam[i].CadidateCandidateExam.CandiadteExamExam.ExamID + "'"
                        + " and EX_CandidateExam.CandidateID='" + oListCandidateForExam[i].CandidateForExamCandidate.CandidateCompositeID + "')"
                        + " ; delete from EX_Candidate where CompositeCandidateID='" + oListCandidateForExam[i].CandidateForExamCandidate.CandidateCompositeID + "'"
                        + " and CompositeCandidateID not in"
                        + " (select CandidateID from EX_CandidateForExam where CandidateID='" + oListCandidateForExam[i].CandidateForExamCandidate.CandidateCompositeID + "')";

                        oListString.Add(sDelete);

                        oListCandidateForExam1.Add(null);
                    }
                    else
                    {
                        oListCandidateForExam1.Add(oListCandidateForExam[i]);
                    }
                }

                oListCandidateForExam1.RemoveAll(delegate(CandidateForExam oCandidateForExam) { if (oCandidateForExam != null) { return false; } return true; });

                if (oDAOUtil.ExecuteNonQuery(oListString))
                {
                    oResult.ResultObject = oListCandidateForExam1;
                    oResult.ResultMessage = "Candidate Remove Success...";
                    oResult.ResultIsSuccess = true;
                }
                else
                {
                    oResult.ResultIsSuccess = false;
                    oResult.ResultMessage = "Candidate Delete Failed...";
                }
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Candidate Remove...";

                logger.Info("Exception RemoveCandidate CandidateDAO+DAO", oEx);

                //new CLogger("Exception RemoveCandidate CandidateDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception RemoveCandidate CandidateDAO+DAO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out RemoveCandidate CandidateDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out RemoveCandidate CandidateDAO+DAO", ELogLevel.Debug);

            logger.Info("End RemoveCandidate CandidateDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This Method update candidate informations
        /// </summary>
        /// <param name="oListCandidateForExam"> It takes List<CandidateForExam> Object </param>
        /// <param name="iArrCheck"> It is an array of Integer which indicates the marked candidates to Update</param>
        /// <returns> It returns Result Object </returns>
        public Result UpdateCandidate(List<CandidateForExam> oListCandidateForExam, int[] iArrCheck)
        {

            //new CLogger("Start UpdateCandidate CandidateDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start UpdateCandidate CandidateDAO+DAO", ELogLevel.Debug);

            logger.Info("Start UpdateCandidate CandidateDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            //List<String> oListString = new List<String>();

            //String sUpdate = String.Empty;

            List<SqlCommand> oListSqlCommand = new List<SqlCommand>();
            List<int> oListInt = new List<int>();

            int i=0;

            try
            {
                for (i = 0; i < iArrCheck.Length; i++)
                {
                    if (iArrCheck[i] == 1)
                    {
                        //sUpdate = "if not exists(select EmailAddress from EX_Candidate"
                        //+ " where EmailAddress='" + oListCandidateForExam[i].CandidateForExamCandidate.CandidateEmail + "')"
                        //+ " update EX_Candidate set CandidatePassword='" + oListCandidateForExam[i].CandidateForExamCandidate.CandidatePassword + "'"
                        //+ " ,Name='" + oListCandidateForExam[i].CandidateForExamCandidate.CandidateName + "'"
                        //+ " ,LastResult='" + oListCandidateForExam[i].CandidateForExamCandidate.CandidateLastResult + "'"
                        //+ " ,LastInstitution='" + oListCandidateForExam[i].CandidateForExamCandidate.CandiadteLastInstitution + "'"
                        //+ " ,LastPassingYear='" + oListCandidateForExam[i].CandidateForExamCandidate.CandidateLastPassingYear + "'"
                        //+ " ,CvPath='" + oListCandidateForExam[i].CandidateForExamCandidate.CandidateCvPath + "'"
                        //+ " ,EmailAddress='" + oListCandidateForExam[i].CandidateForExamCandidate.CandidateEmail + "'"
                        //+ " ,LastResultRange='" + oListCandidateForExam[i].CandidateForExamCandidate.CandidateLastResultRange + "'"
                        //+ " ,LastResultTypeName='" + oListCandidateForExam[i].CandidateForExamCandidate.LastResultTypaName + "'"
                        //+ " ,CandidatePicturePath='" + oListCandidateForExam[i].CandidateForExamCandidate.CandidatePicturePath + "'"
                        //+ " where CompositeCandidateID='" + oListCandidateForExam[i].CandidateForExamCandidate.CandidateCompositeID + "'";
                       

                        SqlCommand oSqlCommand = new SqlCommand("SP_CandidateUpdate");
                        oSqlCommand.CommandType = CommandType.StoredProcedure;

                        oSqlCommand.Parameters.Add("@CompositeCandidateID", SqlDbType.VarChar);
                        oSqlCommand.Parameters["@CompositeCandidateID"].Value = oListCandidateForExam[i].CandidateForExamCandidate.CandidateCompositeID;

                        oSqlCommand.Parameters.Add("@CandidatePassword", SqlDbType.VarChar);
                        oSqlCommand.Parameters["@CandidatePassword"].Value = oListCandidateForExam[i].CandidateForExamCandidate.CandidatePassword;

                        oSqlCommand.Parameters.Add("@Name", SqlDbType.VarChar);
                        oSqlCommand.Parameters["@Name"].Value = oListCandidateForExam[i].CandidateForExamCandidate.CandidateName;

                        oSqlCommand.Parameters.Add("@LastResult", SqlDbType.Float);
                        oSqlCommand.Parameters["@LastResult"].Value = oListCandidateForExam[i].CandidateForExamCandidate.CandidateLastResult;

                        oSqlCommand.Parameters.Add("@LastInstitution", SqlDbType.VarChar);
                        oSqlCommand.Parameters["@LastInstitution"].Value = oListCandidateForExam[i].CandidateForExamCandidate.CandiadteLastInstitution;

                        oSqlCommand.Parameters.Add("@LastPassingYear", SqlDbType.Int);
                        oSqlCommand.Parameters["@LastPassingYear"].Value = oListCandidateForExam[i].CandidateForExamCandidate.CandidateLastPassingYear;

                        oSqlCommand.Parameters.Add("@CvPath", SqlDbType.VarChar);
                        oSqlCommand.Parameters["@CvPath"].Value = oListCandidateForExam[i].CandidateForExamCandidate.CandidateCvPath;

                        oSqlCommand.Parameters.Add("@EmailAddress", SqlDbType.VarChar);
                        oSqlCommand.Parameters["@EmailAddress"].Value = oListCandidateForExam[i].CandidateForExamCandidate.CandidateEmail;

                        oSqlCommand.Parameters.Add("@LastResultRange", SqlDbType.Float);
                        oSqlCommand.Parameters["@LastResultRange"].Value = oListCandidateForExam[i].CandidateForExamCandidate.CandidateLastResultRange;

                        oSqlCommand.Parameters.Add("@LastResultTypeName", SqlDbType.VarChar);
                        oSqlCommand.Parameters["@LastResultTypeName"].Value = oListCandidateForExam[i].CandidateForExamCandidate.LastResultTypaName;

                        oSqlCommand.Parameters.Add("@CandidatePicturePath", SqlDbType.VarChar);
                        oSqlCommand.Parameters["@CandidatePicturePath"].Value = oListCandidateForExam[i].CandidateForExamCandidate.CandidatePicturePath;

                        oListSqlCommand.Add(oSqlCommand);

                    }
                }

                oListInt = oDAOUtil.ExecuteNonQueryForStoredProcedure(oListSqlCommand);

                if (oListInt.Count > 0)
                {
                    oResult.ResultObject = oListCandidateForExam;
                    oResult.ResultMessage = "Candidate Update Success(if email not updtaed, then it is duplicate)...";
                    oResult.ResultIsSuccess = true;
                }
                else
                {
                    oResult.ResultIsSuccess = false;
                    oResult.ResultMessage = "Candidate Update Fail...";
                }

                //if (oDAOUtil.ExecuteNonQuery(oListString))
                //{
                //    oResult.ResultObject = oListCandidateForExam;
                //    oResult.ResultMessage = "Candidate Update Success(if email not updtaed, then it is duplicate)...";
                //    oResult.ResultIsSuccess = true;
                //}
                //else
                //{
                //    oResult.ResultIsSuccess = false;
                //    oResult.ResultMessage = "Candidate Update Fail...";
                //}
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Candidate Update Exception...";

                logger.Info("Exception UpdateCandidate CandidateDAO+DAO", oEx);

                //new CLogger("Exception UpdateCandidate CandidateDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception UpdateCandidate CandidateDAO+DAO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out UpdateCandidate CandidateDAO+DAO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out UpdateCandidate CandidateDAO+DAO", ELogLevel.Debug);

            logger.Info("End UpdateCandidate CandidateDAO+DAO");

            return oResult;
        }

        /// <summary>
        /// This Method add candidates from existing candidates for a Selected Exam.
        /// The candidates to be add for Selected Exam, the Selected Exam duration can not overlap with other exams.
        /// </summary>
        /// <param name="oListCandidateForExam"> It takes List<CandidateForExam> Object </param>
        /// <param name="iArrCheck"> It is an array of Integer which indicates the marked candidates to add for the Selected Exam</param>
        /// <param name="oSelectedExam"> It takes Exam Object </param>
        /// <returns> It returns Result Object </returns>
        public Result AddCandidatesFromExistingCandidate(List<CandidateForExam> oListOfCandidateForExamForGrid, int[] iArrCheck, Exam oSelectedExam)
        {
            logger.Info("Start AddCandidatesFromExistingCandidate CandidateDAO+DAO");
            
            Result oResult = new Result();
            DAOUtil oDAOUtil = new DAOUtil();

            List<String> oListString = new List<String>();

            String sInsert = String.Empty;
            String sSelect = String.Empty;

            SqlDataReader oSqlDataReader = null;

            int i = 0;

            try
            {

                for (i = 0; i < iArrCheck.Length; i++)
                {
                    if (iArrCheck[i] == 1)
                    {
                        sSelect = "select EX_Exam.ExamID,EX_Exam.ExamName,EX_Exam.ExamTotalMarks,EX_Exam.ExamDateWithTime,"
                        +" EX_Exam.ExamDuration,EX_Exam.ExamConstraint"
                        +" from EX_CandidateForExam inner join EX_Exam on EX_CandidateForExam.ExamID=EX_Exam.ExamID"
                        +" where EX_CandidateForExam.CandidateID='" + oListOfCandidateForExamForGrid[i].CandidateForExamCandidate.CandidateCompositeID + "'";

                        oSqlDataReader = oDAOUtil.GetReader(sSelect);

                        bool bNoMissMatch = true; // MissMatch with other exam duration

                        while (oSqlDataReader.Read())
                        {
                            DateTime oDateTimeExamStartTime =DateTime.Parse(oSqlDataReader["ExamDateWithTime"].ToString());
                            float fExamDuration = float.Parse(oSqlDataReader["ExamDuration"].ToString());
                            DateTime oDateTimeExamEndTime = oDateTimeExamStartTime.AddHours(fExamDuration);

                            if (oSelectedExam.ExamDateWithStartingTime >= oDateTimeExamStartTime && oSelectedExam.ExamDateWithStartingTime <= oDateTimeExamEndTime)
                            {
                                bNoMissMatch = false;

                                break;
                            }
                        }
                        
                        oSqlDataReader.Close();

                        if (bNoMissMatch)
                        {
                            sInsert = "if not exists(select EX_CandidateForExam.CandidateID from EX_CandidateForExam where"
                            + " EX_CandidateForExam.CandidateID='" + oListOfCandidateForExamForGrid[i].CandidateForExamCandidate.CandidateCompositeID + "' and EX_CandidateForExam.ExamID='" + oSelectedExam.ExamID + "')"
                            + " insert into EX_CandidateForExam(CandidateID,ExamID) values('" + oListOfCandidateForExamForGrid[i].CandidateForExamCandidate.CandidateCompositeID + "','" + oSelectedExam.ExamID + "')";

                            oListString.Add(sInsert);
                        }
                    }
                }

                if (oListString.Count >0 )
                {
                    if (oDAOUtil.ExecuteNonQuery(oListString))
                    {
                        oResult.ResultObject = oListOfCandidateForExamForGrid;
                        oResult.ResultMessage = "Successfully added the selected candidates...";
                        oResult.ResultIsSuccess = true;
                    }
                    else
                    {
                        oResult.ResultIsSuccess = false;
                        oResult.ResultMessage = "AddCandidatesFromExistingCandidate Fail...";
                    }
                }
                else
                {
                    oResult.ResultIsSuccess = false;
                    oResult.ResultMessage = "None of the selected candidates are set up...";
                }
                
                
            }
            catch(Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "AddCandidatesFromExistingCandidate Exception...";

                logger.Info("Exception AddCandidatesFromExistingCandidate CandidateDAO+DAO", oEx);
            }
            finally
            {
                if (oSqlDataReader != null && !oSqlDataReader.IsClosed)
                {
                    oSqlDataReader.Close();
                }
            }

            logger.Info("End AddCandidatesFromExistingCandidate CandidateDAO+DAO");

            return oResult;
        }
    }
}
