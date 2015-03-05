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
    public class ResultViewBO
    {
        public ResultViewBO()
        { 
        }



        public Result LoadCategoriesWithTypeForAnExam(Exam oExam)
        {

            //new CLogger("Start LoadCategoriesWithTypeForAnExam ResultViewBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start LoadCategoriesWithTypeForAnExam ResultViewBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();
            ResultViewDAO oResultViewDAO = new ResultViewDAO();

            try
            {
                oResult = oResultViewDAO.LoadCategoriesWithTypeForAnExam(oExam);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "LoadCategoriesWithTypeForAnExam Exception..";

                //new CLogger("Exception LoadCategoriesWithTypeForAnExam ResultViewBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception LoadCategoriesWithTypeForAnExam ResultViewBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out LoadCategoriesWithTypeForAnExam ResultViewBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out LoadCategoriesWithTypeForAnExam ResultViewBO+BO", ELogLevel.Debug);

            return oResult;
        }

        public Result GetCandidateAVGMarksForTypeAndCategory(Exam oExam, List<CandidateMenu> oListCandidateMenu)
        {

            //new CLogger("Start GetCandidateAVGMarksForTypeAndCategory ResultViewBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Start GetCandidateAVGMarksForTypeAndCategory ResultViewBO+BO", ELogLevel.Debug);
            
            Result oResult = new Result();
            ResultViewDAO oResultViewDAO = new ResultViewDAO();

            try
            {
                oResult = oResultViewDAO.GetCandidateAVGMarksForTypeAndCategory(oExam, oListCandidateMenu);
            }
            catch (Exception oEx)
            {
                oResult.ResultIsSuccess = false;
                oResult.ResultException = oEx;
                oResult.ResultMessage = "GetCandidateAVGMarksForTypeAndCategory Exception..";

                //new CLogger("Exception GetCandidateAVGMarksForTypeAndCategory ResultViewBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Exception GetCandidateAVGMarksForTypeAndCategory ResultViewBO+BO", ELogLevel.Debug, oEx);
            }

            //new CLogger("Out GetCandidateAVGMarksForTypeAndCategory ResultViewBO+BO", FileNameManagerInDLL.sLogFileName, 1).WriteLog("Out GetCandidateAVGMarksForTypeAndCategory ResultViewBO+BO", ELogLevel.Debug);

            return oResult;
        }
    }
}
