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
    public class CandidateBO
    {
        
        
        public CandidateBO()
        { 
        
        }

        public Result CandidateSetup(CandidateForExam oCandidateForExam)
        {
            //new CLogger("Start CandidateSetup CandidateBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start CandidateSetup CandidateBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();

            try
            {
                CandidateDAO oCategoryDAO = new CandidateDAO();

                oResult = oCategoryDAO.CandidateSetup(oCandidateForExam);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Candidate Setup..";

                //new CLogger("Exception CandidateSetup CandidateBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception CandidateSetup CandidateBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out CandidateSetup CandidateBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out CandidateSetup CandidateBO+BO", ELogLevel.Debug); ;

            return oResult;
        }

        public Result CandidateLogin(Candidate oCandidate)
        {
            //new CLogger("Start CandidateLogin CandidateBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start CandidateLogin CandidateBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();
            CandidateDAO oCandidateDAO = new CandidateDAO();

            try
            {
                oResult = oCandidateDAO.CandidateLogin(oCandidate);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Candidate Login..";

                //new CLogger("Exception CandidateLogin CandidateBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception CandidateLogin CandidateBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out CandidateLogin CandidateBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out CandidateLogin CandidateBO+BO", ELogLevel.Debug); ;

            return oResult;
        }
        public Result ShowAllCandidates(Exam oExam)
        {
            //new CLogger("Start ShowAllCandidates CandidateBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start ShowAllCandidates CandidateBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();
            CandidateDAO oCandidateDAO = new CandidateDAO();

            try
            {
                oResult = oCandidateDAO.ShowAllCandidates(oExam);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Show All Candidates..";

                //new CLogger("Exception ShowAllCandidates CandidateBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception ShowAllCandidates CandidateBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out ShowAllCandidates CandidateBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out ShowAllCandidates CandidateBO+BO", ELogLevel.Debug); ;

            return oResult;
        }
        public Result RemoveCandidate(List<CandidateForExam> oListCandidateForExam, int[] iArrCheck)
        {
            //new CLogger("Start RemoveCandidate CandidateBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start RemoveCandidate CandidateBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();
            CandidateDAO oCandidateDAO = new CandidateDAO();

            try
            {
                oResult = oCandidateDAO.RemoveCandidate(oListCandidateForExam, iArrCheck);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Candidate Remove..";

                //new CLogger("Exception RemoveCandidate CandidateBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception RemoveCandidate CandidateBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out RemoveCandidate CandidateBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out RemoveCandidate CandidateBO+BO", ELogLevel.Debug); ;

            return oResult;
        }


        public Result UpdateCandidate(List<CandidateForExam> oListCandidateForExam, int[] iArrCheck)
        {
            //new CLogger("Start UpdateCandidate CandidateBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start UpdateCandidate CandidateBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();
            CandidateDAO oCandidateDAO = new CandidateDAO();

            try
            {
                oResult = oCandidateDAO.UpdateCandidate(oListCandidateForExam, iArrCheck);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during Candidate Update..";

                //new CLogger("Exception UpdateCandidate CandidateBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception UpdateCandidate CandidateBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out UpdateCandidate CandidateBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out UpdateCandidate CandidateBO+BO", ELogLevel.Debug); ;

            return oResult;
        }

        public Result AddCandidatesFromExistingCandidate(List<CandidateForExam> oListOfCandidateForExamForGrid, int[] iArrCheck, Exam oSelectedExam)
        {
            Result oResult = new Result();
            CandidateDAO oCandidateDAO = new CandidateDAO();

            try
            {
                oResult = oCandidateDAO.AddCandidatesFromExistingCandidate(oListOfCandidateForExamForGrid, iArrCheck, oSelectedExam);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "Exception occured during AddCandidatesFromExistingCandidate..";
            }

            return oResult;
        }

        ///// <summary>
        ///// This method deserialize an xml file to an object of a particular type. this is a general method
        ///// </summary>
        ///// <param name="oType"> It takes Type Parameter</param>
        ///// <param name="sFileName"> It takes a file name to be deserialized </param>
        ///// <returns> It returns Result Object </returns>
        //public Result LoadMailForCandidate(Type oType, String sFileName) // This method deserialize an xml file to an object of a particular type. this is a general method
        //{
        //    Result oResult = new Result();
        //    //typeof(MailForCandidate)
        //    try
        //    {
        //        oResult.ResultMessage = "LoadMailForCandidate Success..";
        //        oResult.ResultObject = new XMLUtil().GetConfigFile(oType, sFileName);
        //        oResult.ResultIsSuccess = true;
        //    }
        //    catch (Exception oEx)
        //    {
        //        oResult.ResultIsSuccess = false;
        //        oResult.ResultException = oEx;
        //        oResult.ResultMessage = "LoadMailForCandidate Exception..";
        //    }

        //    return oResult;
        //}
    }
}
